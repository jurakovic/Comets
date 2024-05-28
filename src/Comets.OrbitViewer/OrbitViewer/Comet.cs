using Comets.Core;
using System;
using System.Drawing;

namespace Comets.OrbitViewer
{
	public class OVComet
	{
		#region Const

		private const double EPSILON = 1e-10;

		#endregion

		#region Properties

		/// <summary>
		/// Object name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Epoch
		/// </summary>
		public decimal T { get; private set; }

		/// <summary>
		/// Eccentricity
		/// </summary>
		public double e { get; private set; }

		/// <summary>
		/// Perihelion distance
		/// </summary>
		public double q { get; private set; }

		/// <summary>
		/// Argument of pericenter
		/// </summary>
		public double w { get; private set; }

		/// <summary>
		/// Ascending node
		/// </summary>
		public double N { get; private set; }

		/// <summary>
		/// Inclination
		/// </summary>
		public double i { get; private set; }

		/// <summary>
		/// Absolute magnitude
		/// </summary>
		public double g { get; private set; }

		/// <summary>
		/// Slope parameter
		/// </summary>
		public double k { get; private set; }

		/// <summary>
		/// Equinox (eg. 2000)
		/// </summary>
		private double Equinox { get; set; }

		/// <summary>
		/// Equinox in ATime
		/// </summary>
		private ATime ATimeEquinox { get; set; }

		/// <summary>
		/// Equinox Julian date
		/// </summary>
		public double EquinoxJD { get { return this.ATimeEquinox.JD; } }

		/// <summary>
		/// Vector constant
		/// </summary>
		public Matrix VectorConstant { get; private set; }

		/// <summary>
		/// Comet location on Panel
		/// </summary>
		public Point PanelLocation { get; set; }

		/// <summary>
		/// Is Comet marked
		/// </summary>
		public bool IsMarked { get; set; }

		/// <summary>
		/// Is Comet shown on panel
		/// </summary>
		public bool IsVisible { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="comet"></param>
		public OVComet(Comet comet)
		{
			this.Name = comet.full;
			this.T = comet.T;
			this.e = comet.e;
			this.q = comet.q;
			this.w = comet.w * Math.PI / 180.0;
			this.N = comet.N * Math.PI / 180.0;
			this.i = comet.i * Math.PI / 180.0;
			this.g = comet.g;
			this.k = comet.k;
			this.Equinox = 2000.0;

			int eqYear = (int)Math.Floor(this.Equinox);
			double eqM = (this.Equinox - (double)eqYear) * 12.0;
			int eqMonth = (int)Math.Floor(eqM);
			double eqDay = (eqM - (double)eqMonth) * 30.0;
			this.ATimeEquinox = new ATime(eqYear, eqMonth, eqDay, 0.0);

			this.VectorConstant = Matrix.VectorConstant(w, N, i, this.ATimeEquinox);
		}

		#endregion

		#region CometStatusEllip

		/// <summary>
		/// Get Position on Orbital Plane for Elliptical Orbit
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		private Xyz CometStatusEllip(double jd)
		{
			// src: stellarium Orbit.cpp, KeplerOrbit::InitEll
			double a = this.q / (1.0 - this.e); // semimajor axis
			double M = Astro.Gauss * (jd - (double)this.T) / (Math.Sqrt(a) * a);

			M = M % (2 * Math.PI);  // Mean Anomaly [0...2pi]
			if (M < 0.0) M += 2.0 * Math.PI;

			double E = M + 0.85 * this.e * Math.Sign(Math.Sin(M));
			int escape = 0;
			for (; ; )
			{
				double Ep = E;
				double f2 = this.e * Math.Sin(E);
				double f = E - f2 - M;
				double f1 = 1.0 - this.e * Math.Cos(E);
				E += (-5.0 * f) / (f1 + Math.Sign(f1) * Math.Sqrt(Math.Abs(16.0 * f1 * f1 - 20.0 * f * f2)));
				if (Math.Abs(E - Ep) < EPSILON) break;
				if (++escape > 10) break;
			}

			double h1 = this.q * Math.Sqrt((1.0 + this.e) / (1.0 - this.e));
			double rCosNu = a * (Math.Cos(E) - this.e);
			double rSinNu = h1 * Math.Sin(E);

			return new Xyz(rCosNu, rSinNu, 0.0);
		}

		#endregion

		#region CometStatusPara

		/// <summary>
		/// Get Position on Orbital Plane for Parabolic Orbit
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		private Xyz CometStatusPara(double jd)
		{
			// src: cdc cu_planet.pas, TPlanet.OrbRect
			double w1 = 3.649116245E-2 * (jd - (double)this.T) / (q * Math.Sqrt(q));
			double s1 = 0.0;
			for (; ; )
			{
				double s0 = s1;
				s1 = (2.0 * s0 * s0 * s0 + w1) / (3.0 * (s0 * s0 + 1.0));
				if (Math.Abs(s1 - s0) < EPSILON) break;
			}

			double s = s1;
			double v = 2.0 * Math.Atan(s);
			double r = q * (1.0 + s * s);

			double rCosNu = r * Math.Cos(v);
			double rSinNu = r * Math.Sin(v);

			return new Xyz(rCosNu, rSinNu, 0.0);
		}

		#endregion

		#region CometStatusHyper

		/// <summary>
		/// Get Position on Orbital Plane for Nearly Parabolic Orbit
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		private Xyz CometStatusHyper(double jd)
		{
			// src: stellarium Orbit.cpp, KeplerOrbit::InitHyp
			double a = this.q / (e - 1.0);
			double period = Math.Pow((this.q / (this.e - 1.0)), 1.5);
			double n = Astro.Gauss / period;
			double M = (jd - (double)this.T) * n;

			double E = Math.Sign(M) * Math.Log(2.0 * Math.Abs(M) / this.e + 1.85);
			for (; ; )
			{
				double Ep = E;
				double f2 = this.e * Math.Sinh(E);
				double f = f2 - E - M;
				double f1 = this.e * Math.Cosh(E) - 1.0;
				E += (-5.0 * f) / (f1 + Math.Sign(f1) * Math.Sqrt(Math.Abs(16.0 * f1 * f1 - 20.0 * f * f2)));
				if (Math.Abs(E - Ep) < EPSILON) break;
			}

			double rCosNu = a * (this.e - Math.Cosh(E));
			double rSinNu = a * Math.Sqrt(this.e * this.e - 1.0) * Math.Sinh(E);

			return new Xyz(rCosNu, rSinNu, 0.0);
		}

		#endregion

		#region GetPos

		/// <summary>
		/// Get Position in Heliocentric Equatorial Coordinates 2000.0
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		public Xyz GetPos(double jd)
		{
			Xyz xyz;

			if (this.e < 1.0)
				xyz = CometStatusEllip(jd);
			else if (this.e > 1.0)
				xyz = CometStatusHyper(jd);
			else
				xyz = CometStatusPara(jd);

			xyz = xyz.Rotate(this.VectorConstant);
			Matrix mtxPrec = Matrix.PrecMatrix(this.ATimeEquinox.JD, Astro.JD2000);
			return xyz.Rotate(mtxPrec);
		}

		#endregion
	}
}
