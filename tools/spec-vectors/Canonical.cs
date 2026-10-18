using Comets.Core.Managers;
using System;
using System.Reflection;

namespace SpecVectors
{
	/// <summary>
	/// Reference implementation of the parts of the canonical engine (docs/spec/04-astronomy.md)
	/// that do not already exist in the desktop sources.
	/// <para>
	/// Comet and Sun positions are NOT reimplemented here: the canonical engine is the desktop
	/// ephemeris engine itself, so those are taken straight from EphemerisManager (through
	/// reflection, because the functions are private). Only the planets are new: they follow
	/// Paul Schlyter's "Computing planetary positions" mean elements, the same model family the
	/// ephemeris engine already uses for the Sun.
	/// </para>
	/// </summary>
	public static class Canonical
	{
		public const double DayZeroJD = 2451543.5; // 1999 Dec 31.0 (= 2000 Jan 0.0)
		const double DEG2RAD = Math.PI / 180.0;
		const double RAD2DEG = 180.0 / Math.PI;
		const double EPSILON = 1e-10;

		public enum Planet { Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune }

		public sealed class Elements
		{
			public double N, i, w, a, e, M; // degrees, AU
		}

		static readonly MethodInfo _sunXyz = typeof(EphemerisManager).GetMethod("SunXyz", BindingFlags.NonPublic | BindingFlags.Static);
		static readonly MethodInfo _cometXyz = typeof(EphemerisManager).GetMethod("CometXyz", BindingFlags.NonPublic | BindingFlags.Static);

		/// <summary>Desktop EphemerisManager.SunXyz: geocentric ecliptic-of-date Sun (x, y, z, r, lon, 0).</summary>
		public static double[] SunXyz(decimal jd) => (double[])_sunXyz.Invoke(null, new object[] { jd });

		/// <summary>Desktop EphemerisManager.CometXyz: heliocentric ecliptic-of-date comet (x, y, z, r, lon, lat).</summary>
		public static double[] CometXyz(decimal T, double q, double e, double w, double N, double i, decimal jd)
			=> (double[])_cometXyz.Invoke(null, new object[] { T, q, e, w, N, i, jd });

		/// <summary>Schlyter mean elements, ecliptic and equinox of date, d = JD - 2451543.5.</summary>
		public static Elements GetElements(Planet p, double d)
		{
			switch (p)
			{
				case Planet.Mercury: return new Elements { N = 48.3313 + 3.24587E-5 * d, i = 7.0047 + 5.00E-8 * d, w = 29.1241 + 1.01444E-5 * d, a = 0.387098, e = 0.205635 + 5.59E-10 * d, M = 168.6562 + 4.0923344368 * d };
				case Planet.Venus: return new Elements { N = 76.6799 + 2.46590E-5 * d, i = 3.3946 + 2.75E-8 * d, w = 54.8910 + 1.38374E-5 * d, a = 0.723330, e = 0.006773 - 1.302E-9 * d, M = 48.0052 + 1.6021302244 * d };
				case Planet.Earth: return new Elements { N = 0.0, i = 0.0, w = 282.9404 + 4.70935E-5 * d + 180.0, a = 1.000000, e = 0.016709 - 1.151E-9 * d, M = 356.0470 + 0.9856002585 * d };
				case Planet.Mars: return new Elements { N = 49.5574 + 2.11081E-5 * d, i = 1.8497 - 1.78E-8 * d, w = 286.5016 + 2.92961E-5 * d, a = 1.523688, e = 0.093405 + 2.516E-9 * d, M = 18.6021 + 0.5240207766 * d };
				case Planet.Jupiter: return new Elements { N = 100.4542 + 2.76854E-5 * d, i = 1.3030 - 1.557E-7 * d, w = 273.8777 + 1.64505E-5 * d, a = 5.20256, e = 0.048498 + 4.469E-9 * d, M = 19.8950 + 0.0830853001 * d };
				case Planet.Saturn: return new Elements { N = 113.6634 + 2.38980E-5 * d, i = 2.4886 - 1.081E-7 * d, w = 339.3939 + 2.97661E-5 * d, a = 9.55475, e = 0.055546 - 9.499E-9 * d, M = 316.9670 + 0.0334442282 * d };
				case Planet.Uranus: return new Elements { N = 74.0005 + 1.3978E-5 * d, i = 0.7733 + 1.9E-8 * d, w = 96.6612 + 3.0565E-5 * d, a = 19.18171 - 1.55E-8 * d, e = 0.047318 + 7.45E-9 * d, M = 142.5905 + 0.011725806 * d };
				case Planet.Neptune: return new Elements { N = 131.7806 + 3.0173E-5 * d, i = 1.7700 - 2.55E-7 * d, w = 272.8461 - 6.027E-6 * d, a = 30.05826 + 3.313E-8 * d, e = 0.008606 + 2.15E-9 * d, M = 260.2471 + 0.005995147 * d };
				default: throw new ArgumentOutOfRangeException(nameof(p));
			}
		}

		/// <summary>
		/// The elliptic Kepler solver of EphemerisManager.CometXyz, lifted out so planets use the
		/// very same iteration. M in radians, any value; returns E in radians.
		/// </summary>
		public static double SolveKeplerElliptic(double M, double e)
		{
			M = M % (2 * Math.PI);
			if (M < 0.0) M += 2.0 * Math.PI;

			double E = M + 0.85 * e * Math.Sign(Math.Sin(M));
			int escape = 0;
			for (; ; )
			{
				double Ep = E;
				double f2 = e * Math.Sin(E);
				double f = E - f2 - M;
				double f1 = 1.0 - e * Math.Cos(E);
				E += (-5.0 * f) / (f1 + Math.Sign(f1) * Math.Sqrt(Math.Abs(16.0 * f1 * f1 - 20.0 * f * f2)));
				if (Math.Abs(E - Ep) < EPSILON) break;
				if (++escape > 10) break;
			}
			return E;
		}

		/// <summary>Longitude and latitude perturbations in degrees (Schlyter section 10).</summary>
		public static (double dLon, double dLat) Perturbations(Planet p, double d)
		{
			double Mj = GetElements(Planet.Jupiter, d).M;
			double Ms = GetElements(Planet.Saturn, d).M;
			double Mu = GetElements(Planet.Uranus, d).M;

			double S(double deg) => Math.Sin(deg * DEG2RAD);
			double C(double deg) => Math.Cos(deg * DEG2RAD);

			switch (p)
			{
				case Planet.Jupiter:
					return (
						-0.332 * S(2 * Mj - 5 * Ms - 67.6)
						- 0.056 * S(2 * Mj - 2 * Ms + 21)
						+ 0.042 * S(3 * Mj - 5 * Ms + 21)
						- 0.036 * S(Mj - 2 * Ms)
						+ 0.022 * C(Mj - Ms)
						+ 0.023 * S(2 * Mj - 3 * Ms + 52)
						- 0.016 * S(Mj - 5 * Ms - 69),
						0.0);
				case Planet.Saturn:
					return (
						+0.812 * S(2 * Mj - 5 * Ms - 67.6)
						- 0.229 * C(2 * Mj - 4 * Ms - 2)
						+ 0.119 * S(Mj - 2 * Ms - 3)
						+ 0.046 * S(2 * Mj - 6 * Ms - 69)
						+ 0.014 * S(Mj - 3 * Ms + 32),
						-0.020 * C(2 * Mj - 4 * Ms - 2)
						+ 0.018 * S(2 * Mj - 6 * Ms - 49));
				case Planet.Uranus:
					return (
						+0.040 * S(Ms - 2 * Mu + 6)
						+ 0.035 * S(Ms - 3 * Mu + 33)
						- 0.015 * S(Mj - Mu + 20),
						0.0);
				default:
					return (0.0, 0.0);
			}
		}

		/// <summary>Position in the orbital plane from mean anomaly: (xv, yv) in AU.</summary>
		public static (double xv, double yv) OrbitalPlane(Elements el)
		{
			double E = SolveKeplerElliptic(el.M * DEG2RAD, el.e);
			double xv = el.a * (Math.Cos(E) - el.e);
			double yv = el.a * Math.Sqrt(1.0 - el.e * el.e) * Math.Sin(E);
			return (xv, yv);
		}

		/// <summary>Rotates an orbital-plane point (x, y, 0) to ecliptic of date with N, i, w in degrees.</summary>
		public static (double x, double y, double z) ToEcliptic(double xv, double yv, double N, double i, double w)
		{
			double v = Math.Atan2(yv, xv) * RAD2DEG;
			double r = Math.Sqrt(xv * xv + yv * yv);
			double x = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double y = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double z = r * (Sind(v + w) * Sind(i));
			return (x, y, z);
		}

		/// <summary>
		/// Canonical heliocentric ecliptic-of-date planet position (AU).
		/// The Earth is the negated desktop Sun position, so the 3D view and the ephemeris
		/// share one Earth.
		/// </summary>
		public static (double x, double y, double z) PlanetPosition(Planet p, decimal jd)
		{
			if (p == Planet.Earth)
			{
				double[] s = SunXyz(jd);
				return (-s[0], -s[1], -s[2]);
			}

			double d = (double)(jd - (decimal)DayZeroJD);
			Elements el = GetElements(p, d);
			(double xv, double yv) = OrbitalPlane(el);
			(double x, double y, double z) = ToEcliptic(xv, yv, el.N, el.i, el.w);

			var (dLon, dLat) = Perturbations(p, d);
			if (dLon != 0.0 || dLat != 0.0)
			{
				double r = Math.Sqrt(x * x + y * y + z * z);
				double lon = Math.Atan2(y, x) * RAD2DEG + dLon;
				double lat = Math.Atan2(z, Math.Sqrt(x * x + y * y)) * RAD2DEG + dLat;
				x = r * Cosd(lat) * Cosd(lon);
				y = r * Cosd(lat) * Sind(lon);
				z = r * Sind(lat);
			}

			return (x, y, z);
		}

		static double Sind(double a) => Math.Sin(a * DEG2RAD);
		static double Cosd(double a) => Math.Cos(a * DEG2RAD);
	}
}
