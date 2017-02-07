using Comets.BusinessLayer.Business;
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
			if (this.q == 0.0)
				throw new ArithmeticException();

			double axis = this.q / (1.0 - this.e);
			double M = Astro.Gauss * (jd - (double)this.T) / (Math.Sqrt(axis) * axis);
			double E1 = M + this.e * Math.Sin(M);
			int count = MaxApproximations;

			if (this.e < 0.6)
			{
				double fE2;
				do
				{
					fE2 = E1;
					E1 = M + this.e * Math.Sin(fE2);
				} while (Math.Abs(E1 - fE2) > Tolerance && --count > 0);
			}
			else
			{
				double dv;
				do
				{
					double dv1 = (M + this.e * Math.Sin(E1) - E1);
					double dv2 = (1.0 - this.e * Math.Cos(E1));
					if (Math.Abs(dv1) < Tolerance || Math.Abs(dv2) < Tolerance)
					{
						break;
					}
					dv = dv1 / dv2;
					E1 += dv;
				} while (Math.Abs(dv) > Tolerance && --count > 0);
			}

			if (count == 0)
				throw new ArithmeticException();

			double X = axis * (Math.Cos(E1) - this.e);
			double Y = axis * Math.Sqrt(1.0 - this.e * this.e) * Math.Sin(E1);

			return new Xyz(X, Y, 0.0);
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
			if (this.q == 0.0)
				throw new ArithmeticException();

			double N = Astro.Gauss * (jd - (double)this.T) / (Math.Sqrt(2.0) * this.q * Math.Sqrt(this.q));
			double tanV2 = N;
			double oldTanV2, tan2V2;
			int count = MaxApproximations;

			do
			{
				oldTanV2 = tanV2;
				tan2V2 = tanV2 * tanV2;
				tanV2 = (tan2V2 * tanV2 * 2.0 / 3.0 + N) / (1.0 + tan2V2);
			} while (Math.Abs(tanV2 - oldTanV2) > Tolerance && --count > 0);

			if (count == 0)
				throw new ArithmeticException();

			tan2V2 = tanV2 * tanV2;
			double X = this.q * (1.0 - tan2V2);
			double Y = 2.0 * this.q * tanV2;

			return new Xyz(X, Y, 0.0);
		}

		#endregion

		#region CometStatusNearPara

		/// <summary>
		/// Get Position on Orbital Plane for Nearly Parabolic Orbit
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		private Xyz CometStatusNearPara(double jd)
		{
			if (this.q == 0.0)
				throw new ArithmeticException();

			double A = Math.Sqrt((1.0 + 9.0 * this.e) / 10.0);
			double B = 5.0 * (1 - this.e) / (1.0 + 9.0 * this.e);
			double A1, B1, X1, A0, B0, X0, N;
			A1 = B1 = X1 = 1.0;
			int count1 = MaxApproximations;

			do
			{
				A0 = A1;
				B0 = B1;
				N = B0 * A * Astro.Gauss * (jd - (double)this.T) / (Math.Sqrt(2.0) * this.q * Math.Sqrt(this.q));
				int count2 = MaxApproximations;
				do
				{
					X0 = X1;
					double temp = X0 * X0;
					X1 = (temp * X0 * 2.0 / 3.0 + N) / (1.0 + temp);
				} while (Math.Abs(X1 - X0) > Tolerance && --count2 > 0);
				if (count2 == 0)
				{
					throw new ArithmeticException();
				}
				A1 = B * X1 * X1;
				B1 = (-3.809524e-03 * A1 - 0.017142857) * A1 * A1 + 1.0;
			} while (Math.Abs(A1 - A0) > Tolerance && --count1 > 0);

			if (count1 == 0)
				throw new ArithmeticException();

			double C1 = ((0.12495238 * A1 + 0.21714286) * A1 + 0.4) * A1 + 1.0;
			double D1 = ((0.00571429 * A1 + 0.2) * A1 - 1.0) * A1 + 1.0;
			double TanV2 = Math.Sqrt(5.0 * (1.0 + this.e) / (1.0 + 9.0 * this.e)) * C1 * X1;
			double X = this.q * D1 * (1.0 - TanV2 * TanV2);
			double Y = 2.0 * this.q * D1 * TanV2;
			return new Xyz(X, Y, 0.0);
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
					if (this.e < 0.995)
					{
						xyz = CometStatusEllip(JD);
					}
					else if (Math.Abs(this.e - 1.0) < Tolerance)
					{
						xyz = CometStatusPara(JD);
					}
					else
					{
						xyz = CometStatusNearPara(JD);
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