using Comets.Core;
using System;
using System.Drawing;

namespace Comets.OrbitViewer
{
	public class OVComet
	{
		#region Const

		private const int MaxApproximations = 80;
		private const double Tolerance = 1.0E-12;

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
			// src: https://github.com/Stellarium/stellarium/blob/master/src/core/modules/Orbit.cpp, KeplerOrbit::InitEll

			if (this.q == 0.0)
				throw new ArithmeticException();

			double EPSILON = 1e-10;

			double a = this.q / (1.0 - this.e); // semimajor axis
			double M = Astro.Gauss * (jd - (double)this.T) / (Math.Sqrt(a) * a);

			M = M % (2 * Math.PI);  // Mean Anomaly [0...2pi]
			if (M < 0.0) M += 2.0 * Math.PI;
			//	GZ: Comet orbits are quite often near-parabolic, where this may still only converge slowly.
			//	Better always use Laguerre-Conway. See Heafner, Ch. 5.3
			//	Ouch! https://bugs.launchpad.net/stellarium/+bug/1465112 ==>It seems we still need an escape counter!
			//      Debug line in test case fabs(E-Ep) indicates it usually takes 2-3, occasionally up to 6 cycles.
			//	It seems safe to assume 10 should not be exceeded. N.B.: A GPU fixed-loopcount implementation could go for 8 passes.
			double E = M + 0.85 * e * Math.Sign(Math.Sin(M));
			int escape = 0;
			for (; ; )
			{
				double Ep = E;
				double f2 = e * Math.Sin(E);
				double f = E - f2 - M;
				double f1 = 1.0 - e * Math.Cos(E);
				E += (-5.0 * f) / (f1 + Math.Sign(f1) * Math.Sqrt(Math.Abs(16.0 * f1 * f1 - 20.0 * f * f2)));
				if (Math.Abs(E - Ep) < EPSILON)
				{
					break;
				}
				if (++escape > 10)
				{
					break;
				}
			}
			//	Note: q=a*(1-e)
			double h1 = q * Math.Sqrt((1.0 + e) / (1.0 - e));  // elsewhere: a sqrt(1-e²)     ... q / (1-e) sqrt( (1+e)(1-e)) = q sqrt((1+e)/(1-e))
			double rCosNu = a * (Math.Cos(E) - e);
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
			// src: https://github.com/Stellarium/stellarium/blob/master/src/core/modules/Orbit.cpp, KeplerOrbit::InitPar

			if (this.q == 0.0)
				throw new ArithmeticException();

			double M = (jd - (double)this.T) * Math.Sqrt(Astro.Gauss / (2.0 * this.q * this.q * this.q));
			M = M % (2 * Math.PI);

			double W = 1.5 * M;
			double Y = Math.Cbrt(W + Math.Sqrt(W * W + 1.0));
			double tanNu2 = Y - 1.0 / Y; // Heafner (5.5.8) has an error here, writes (Y-1)/Y.
			double rCosNu = this.q * (1.0 - tanNu2 * tanNu2);
			double rSinNu = 2.0 * this.q * tanNu2;

			return new Xyz(rCosNu, rSinNu, 0.0);
		}

		#endregion

		#region CometStatusNearPara

		/// <summary>
		/// Get Position on Orbital Plane for Nearly Parabolic Orbit
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		private Xyz CometStatusHyper(double jd)
		{
			// src: https://github.com/Stellarium/stellarium/blob/master/src/core/modules/Orbit.cpp, KeplerOrbit::InitHyp

			if (this.q == 0.0)
				throw new ArithmeticException();

			double EPSILON = 1e-10;
			double a = q / (e - 1.0);

			double period = Math.Pow((q / (e - 1.0)), 1.5);

			double meanMotion = 0.9856076686 / period;
			meanMotion *= (Math.PI / 180.0);

			double M = (jd - (double)this.T) * meanMotion;

			double E = Math.Sign(M) * Math.Log(2.0 * Math.Abs(M) / e + 1.85);
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
		/// <param name="JD"></param>
		/// <returns></returns>
		public Xyz GetPos(double JD)
		{
			Xyz xyz = null;

			bool exception = true;
			do
			{
				try
				{
					// CometStatus' may be throw ArithmeticException
					if (this.e < 1.0)
					{
						xyz = CometStatusEllip(JD);
					}
					else if (this.e > 1.0)
					{
						xyz = CometStatusHyper(JD);
					}
					else
					{
						xyz = CometStatusPara(JD);
					}

					exception = false;
				}
				catch
				{
					//Hale-Bopp often throws exception, so instead get position for day earlier
					--JD;
					exception = true;
				}
			} while (exception);

			xyz = xyz.Rotate(this.VectorConstant);
			Matrix mtxPrec = Matrix.PrecMatrix(this.ATimeEquinox.JD, Astro.JD2000);
			return xyz.Rotate(mtxPrec);
		}

		#endregion
	}
}
