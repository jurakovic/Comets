using System;

namespace Comets.OrbitViewer
{
	public class CometOrbit
	{
		#region Const

		private const double MaxOrbitAU = 150.0;
		private const double Tolerance = 1.0E-16;

		#endregion

		#region Properties

		/// <summary>
		/// Actual orbit data
		/// </summary>
		private Xyz[] Orbit { get; set; }

		/// <summary>
		/// Number of division
		/// </summary>
		public int Division { get; private set; }

		#endregion

		#region Constructor

		public CometOrbit(OVComet comet, int division)
		{
			this.Division = division;
			Orbit = new Xyz[division + 1];

			if (comet.e < 1.0 - Tolerance)
			{
				GetOrbitEllip(comet);
			}
			else if (comet.e > 1.0 + Tolerance)
			{
				GetOrbitHyper(comet);
			}
			else
			{
				GetOrbitPara(comet);
			}

			Matrix vec = comet.VectorConstant;
			Matrix prec = Matrix.PrecMatrix(comet.EquinoxJD, Astro.JD2000);

			for (int i = 0; i <= division; i++)
			{
				Orbit[i] = Orbit[i].Rotate(vec).Rotate(prec);
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
			return this.Orbit[index];
		}

		#endregion

		#region GetOrbitEllip

		/// <summary>
		/// Elliptical Orbit
		/// </summary>
		/// <param name="comet"></param>
		private void GetOrbitEllip(OVComet comet)
		{
			double axis = comet.q / (1.0 - comet.e);
			double ae2 = -2.0 * axis * comet.e;
			double t = Math.Sqrt(1.0 - comet.e * comet.e);

			if (axis * (1.0 + comet.e) > MaxOrbitAU)
			{
				double dE = Math.Acos((1.0 - MaxOrbitAU / axis) / comet.e) / ((this.Division / 2) * (this.Division / 2));
				int idx1, idx2;
				idx1 = idx2 = this.Division / 2;

				for (int i = 0; i <= (this.Division / 2); i++)
				{
					double E = dE * i * i;
					double RCosV = axis * (Math.Cos(E) - comet.e);
					double RSinV = axis * t * Math.Sin(E);
					Orbit[idx1++] = new Xyz(RCosV, RSinV, 0.0);
					Orbit[idx2--] = new Xyz(RCosV, -RSinV, 0.0);
				}
			}
			else
			{
				int idx1, idx2, idx3, idx4;
				idx1 = 0;
				idx2 = idx3 = this.Division / 2;
				idx4 = this.Division;

				double E = 0.0;
				for (int i = 0; i <= (this.Division / 4); i++, E += (2.0 * Math.PI / this.Division))
				{
					double RCosV = axis * (Math.Cos(E) - comet.e);
					double RSinV = axis * t * Math.Sin(E);
					Orbit[idx1++] = new Xyz(RCosV, RSinV, 0.0);
					Orbit[idx2--] = new Xyz(ae2 - RCosV, RSinV, 0.0);
					Orbit[idx3++] = new Xyz(ae2 - RCosV, -RSinV, 0.0);
					Orbit[idx4--] = new Xyz(RCosV, -RSinV, 0.0);
				}
			}
		}

		#endregion

		#region GetOrbitHyper

		/// <summary>
		/// Hyperbolic Orbit
		/// </summary>
		/// <param name="comet"></param>
		private void GetOrbitHyper(OVComet comet)
		{
			int idx1, idx2;
			idx1 = idx2 = this.Division / 2;
			double t = Math.Sqrt(comet.e * comet.e - 1.0);
			double axis = comet.q / (comet.e - 1.0);
			double dF = UdMath.arccosh((MaxOrbitAU + axis) / (axis * comet.e)) / (this.Division / 2);
			
			double F = 0.0;
			for (int i = 0; i <= (this.Division / 2); i++, F += dF)
			{
				double RCosV = axis * (comet.e - UdMath.cosh(F));
				double RSinV = axis * t * UdMath.sinh(F);
				Orbit[idx1++] = new Xyz(RCosV, RSinV, 0.0);
				Orbit[idx2--] = new Xyz(RCosV, -RSinV, 0.0);
			}
		}

		#endregion

		#region GetOrbitPara

		/// <summary>
		/// Parabolic Orbit
		/// </summary>
		/// <param name="comet"></param>
		private void GetOrbitPara(OVComet comet)
		{
			int idx1, idx2;
			idx1 = idx2 = this.Division / 2;
			double dV = (Math.Atan(Math.Sqrt(MaxOrbitAU / comet.q - 1.0)) * 2.0) / (this.Division / 2);
			
			double V = 0.0;
			for (int i = 0; i <= (this.Division / 2); i++, V += dV)
			{
				double TanV2 = Math.Sin(V / 2.0) / Math.Cos(V / 2.0);
				double RCosV = comet.q * (1.0 - TanV2 * TanV2);
				double RSinV = 2.0 * comet.q * TanV2;
				Orbit[idx1++] = new Xyz(RCosV, RSinV, 0.0);
				Orbit[idx2--] = new Xyz(RCosV, -RSinV, 0.0);
			}
		}

		#endregion
	}
}