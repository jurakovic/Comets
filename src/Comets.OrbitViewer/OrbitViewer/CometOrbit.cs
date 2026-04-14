using System;
using System.Collections.Generic;

namespace Comets.OrbitViewer
{
	public class CometOrbit
	{
		#region Const

		public const int OrbitDivisionCount = 500;
		private const double MaxOrbitAU = 500.0;
		private const double Tolerance = 1.0E-16;

		#endregion

		#region Properties

		private List<Xyz> _orbit;

		/// <summary>
		/// Actual number of sampled orbit points (varies with eccentricity under adaptive sampling).
		/// </summary>
		public int PointCount => _orbit?.Count ?? 0;

		#endregion

		#region Constructor

		public CometOrbit(OVComet comet)
		{
			_orbit = new List<Xyz>();

			if (comet.e < 1.0 - Tolerance)
				GetOrbitEllip(comet);
			else if (comet.e > 1.0 + Tolerance)
				GetOrbitHyper(comet);
			else
				GetOrbitPara(comet);

			Matrix vec = comet.VectorConstant;
			Matrix prec = Matrix.PrecMatrix(comet.EquinoxJD, Astro.JD2000);

			for (int i = 0; i < _orbit.Count; i++)
			{
				_orbit[i] = _orbit[i].Rotate(vec).Rotate(prec);
			}
		}

		#endregion

		#region GetAt

		/// <summary>
		/// Get Orbit Point
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Xyz GetAt(int index)
		{
			return _orbit[index];
		}

		#endregion

		#region GetOrbitEllip

		/// <summary>
		/// Elliptical Orbit — curvature-adaptive sampling.
		///
		/// Uses the Celestia curvature formula k = √w / (sin²E + w·cos²E)^(3/2) with dynamic
		/// kMax = max(20, (1/w)^(2/3)) so that near-parabolic orbits (w ≈ 0) automatically receive
		/// finer pericenter sampling without any per-object tuning.
		///
		/// For orbits whose aphelion exceeds MaxOrbitAU the arc is sampled symmetrically around
		/// pericenter (E = 0).  For closed orbits the two-leg strategy is used: both legs depart
		/// outward from pericenter so the high-curvature zone is always at the start of each leg,
		/// where the adaptive stepper naturally takes fine steps.
		/// </summary>
		private void GetOrbitEllip(OVComet comet)
		{
			double a = comet.q / (1.0 - comet.e);
			double w = 1.0 - comet.e * comet.e;
			double sqrtW = Math.Sqrt(w);
			double kMax = Math.Min(50.0, Math.Max(20.0, Math.Pow(1.0 / w, 2.0 / 3.0)));

			if (a * (1.0 + comet.e) > MaxOrbitAU)
			{
				// Near-parabolic open arc: sample [-E_max, +E_max] with adaptive step.
				double cosE = Math.Max(-1.0, Math.Min(1.0, (1.0 - MaxOrbitAU / a) / comet.e));
				double E_max = Math.Acos(cosE);
				double dE = 2.0 * E_max / OrbitDivisionCount;

				double E = -E_max;
				while (E < E_max)
				{
					double sinE = Math.Sin(E);
					double cosEv = Math.Cos(E);
					_orbit.Add(new Xyz(a * (cosEv - comet.e), a * sqrtW * sinE, 0.0));

					double k = sqrtW / Math.Pow(sinE * sinE + w * cosEv * cosEv, 1.5);
					E += dE / Math.Max(Math.Min(k, kMax), 1.0);
				}
				// Explicit endpoint at E_max (last step overshoots; add exact boundary).
				_orbit.Add(new Xyz(a * (Math.Cos(E_max) - comet.e), a * sqrtW * Math.Sin(E_max), 0.0));
			}
			else
			{
				// Closed orbit: two-leg approach — each leg departs outward from pericenter (E=0)
				// so the high-curvature zone is at the START of each leg where steps are finest.
				//
				// Inbound leg (E: 0 → −π) is buffered then emitted in reverse (−π → 0).
				// Outbound leg (E: 0 → +π) is emitted directly.
				// A closing point at aphelion (E = +π) completes the loop.
				double dE = 2.0 * Math.PI / OrbitDivisionCount;

				var inbound = new List<Xyz>();
				{
					double E = 0.0;
					while (E > -Math.PI)
					{
						double sinE = Math.Sin(E);
						double cosE = Math.Cos(E);
						double k = sqrtW / Math.Pow(sinE * sinE + w * cosE * cosE, 1.5);
						double step = dE / Math.Max(Math.Min(k, kMax), 1.0);
						E -= step;
						if (E < -Math.PI) E = -Math.PI;
						inbound.Add(new Xyz(a * (Math.Cos(E) - comet.e), a * sqrtW * Math.Sin(E), 0.0));
					}
				}
				for (int i = inbound.Count - 1; i >= 0; i--)
					_orbit.Add(inbound[i]);

				{
					double E = 0.0;
					while (E < Math.PI)
					{
						double sinE = Math.Sin(E);
						double cosE = Math.Cos(E);
						_orbit.Add(new Xyz(a * (cosE - comet.e), a * sqrtW * sinE, 0.0));

						double k = sqrtW / Math.Pow(sinE * sinE + w * cosE * cosE, 1.5);
						E += dE / Math.Max(Math.Min(k, kMax), 1.0);
					}
				}
				// Closing point at aphelion (E = π).
				_orbit.Add(new Xyz(a * (-1.0 - comet.e), 0.0, 0.0));
			}
		}

		#endregion

		#region GetOrbitHyper

		/// <summary>
		/// Hyperbolic Orbit — curvature-adaptive sampling.
		///
		/// Uses k = b / (sinh²F + b²·cosh²F)^(3/2) with dynamic kMax = max(20, (1/b)^(2/3)).
		/// </summary>
		private void GetOrbitHyper(OVComet comet)
		{
			double a = comet.q / (comet.e - 1.0);
			double b = Math.Sqrt(comet.e * comet.e - 1.0);
			double kMax = Math.Min(50.0, Math.Max(20.0, Math.Pow(1.0 / b, 2.0 / 3.0)));
			double coshEmax = (MaxOrbitAU / a + 1.0) / comet.e;
			double E_max = coshEmax > 1.0 ? UdMath.arccosh(Math.Min(coshEmax, 1.0e6)) : 0.0;
			double dE = 2.0 * E_max / OrbitDivisionCount;

			double E = -E_max;
			while (E < E_max)
			{
				double sinhE = Math.Sinh(E);
				double coshE = Math.Cosh(E);
				_orbit.Add(new Xyz(a * (comet.e - coshE), a * b * sinhE, 0.0));

				double k = b / Math.Pow(sinhE * sinhE + b * b * coshE * coshE, 1.5);
				E += dE / Math.Max(Math.Min(k, kMax), 1.0);
			}
			// Explicit endpoint at E_max.
			_orbit.Add(new Xyz(a * (comet.e - Math.Cosh(E_max)), a * b * Math.Sinh(E_max), 0.0));
		}

		#endregion

		#region GetOrbitPara

		/// <summary>
		/// Parabolic Orbit — curvature-adaptive sampling in D = tan(ν/2) parameter space.
		///
		/// Uses k = kMax / (1 + D²)^(3/4) with dynamic kMax = max(20, D_max^(4/3)).
		/// The (3/4) exponent gives step ∝ D^(3/2) for large D, producing constant relative
		/// chord error (sag/distance) across all distances — uniform visual quality from
		/// pericenter out to the bounding radius.
		/// </summary>
		private void GetOrbitPara(OVComet comet)
		{
			double D_max = Math.Sqrt(Math.Max(MaxOrbitAU / comet.q - 1.0, 0.0));
			double kMax = Math.Min(50.0, Math.Max(20.0, Math.Pow(D_max, 4.0 / 3.0)));
			double dD = 2.0 * D_max / OrbitDivisionCount;

			double D = -D_max;
			while (D < D_max)
			{
				_orbit.Add(new Xyz(comet.q * (1.0 - D * D), 2.0 * comet.q * D, 0.0));

				double k = kMax / Math.Pow(1.0 + D * D, 0.75);
				D += dD / Math.Max(Math.Min(k, kMax), 1.0);
			}
			// Explicit endpoint at D_max.
			_orbit.Add(new Xyz(comet.q * (1.0 - D_max * D_max), 2.0 * comet.q * D_max, 0.0));
		}

		#endregion
	}
}
