using System;

namespace Comets.OrbitViewer
{
	public class CometOrbit
	{
		#region Const

		public const int OrbitDivisionCount = 500;
		private const double MaxOrbitAU = 150.0;
		private const double Tolerance = 1.0E-16;

		#endregion

		#region Properties

		/// <summary>
		/// Actual orbit data
		/// </summary>
		private Xyz[] Orbit { get; set; }

		#endregion

		#region Constructor

		public CometOrbit(OVComet comet)
		{
			Orbit = new Xyz[OrbitDivisionCount + 1];

			if (comet.e < 1.0 - Tolerance)
				GetOrbitEllip(comet);
			else if (comet.e > 1.0 + Tolerance)
				GetOrbitHyper(comet);
			else
				GetOrbitPara(comet);

			Matrix vec = comet.VectorConstant;
			Matrix prec = Matrix.PrecMatrix(comet.EquinoxJD, Astro.JD2000);

			for (int i = 0; i <= OrbitDivisionCount; i++)
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
			double a = comet.q / (1.0 - comet.e);
			double ae2 = -2.0 * a * comet.e;
			double t = Math.Sqrt(1.0 - comet.e * comet.e);

			if (a * (1.0 + comet.e) > MaxOrbitAU)
			{
				double dE = Math.Acos((1.0 - MaxOrbitAU / a) / comet.e) / ((OrbitDivisionCount / 2) * (OrbitDivisionCount / 2));
				int idx1, idx2;
				idx1 = idx2 = OrbitDivisionCount / 2;

				for (int i = 0; i <= (OrbitDivisionCount / 2); i++)
				{
					double E = dE * i * i;
					double rCosNu = a * (Math.Cos(E) - comet.e);
					double rSinNu = a * t * Math.Sin(E);
					Orbit[idx1++] = new Xyz(rCosNu, rSinNu, 0.0);
					Orbit[idx2--] = new Xyz(rCosNu, -rSinNu, 0.0);
				}
			}
			else
			{
				int idx1, idx2, idx3, idx4;
				idx1 = 0;
				idx2 = idx3 = OrbitDivisionCount / 2;
				idx4 = OrbitDivisionCount;

				double E = 0.0;
				for (int i = 0; i <= (OrbitDivisionCount / 4); i++, E += (2.0 * Math.PI / OrbitDivisionCount))
				{
					double rCosNu = a * (Math.Cos(E) - comet.e);
					double rSinNu = a * t * Math.Sin(E);
					Orbit[idx1++] = new Xyz(rCosNu, rSinNu, 0.0);
					Orbit[idx2--] = new Xyz(ae2 - rCosNu, rSinNu, 0.0);
					Orbit[idx3++] = new Xyz(ae2 - rCosNu, -rSinNu, 0.0);
					Orbit[idx4--] = new Xyz(rCosNu, -rSinNu, 0.0);
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
			idx1 = idx2 = OrbitDivisionCount / 2;
			double t = Math.Sqrt(comet.e * comet.e - 1.0);
			double a = comet.q / (comet.e - 1.0);
			double dF = UdMath.arccosh((MaxOrbitAU + a) / (a * comet.e)) / (OrbitDivisionCount / 2);

			double F = 0.0;
			for (int i = 0; i <= (OrbitDivisionCount / 2); i++, F += dF)
			{
				double rCosNu = a * (comet.e - Math.Cosh(F));
				double rSinNu = a * t * Math.Sinh(F);
				Orbit[idx1++] = new Xyz(rCosNu, rSinNu, 0.0);
				Orbit[idx2--] = new Xyz(rCosNu, -rSinNu, 0.0);
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
			idx1 = idx2 = OrbitDivisionCount / 2;
			double dV = (Math.Atan(Math.Sqrt(MaxOrbitAU / comet.q - 1.0)) * 2.0) / (OrbitDivisionCount / 2);

			double V = 0.0;
			for (int i = 0; i <= (OrbitDivisionCount / 2); i++, V += dV)
			{
				double tanNu2 = Math.Sin(V / 2.0) / Math.Cos(V / 2.0);
				double rCosNu = comet.q * (1.0 - tanNu2 * tanNu2);
				double rSinNU = 2.0 * comet.q * tanNu2;
				Orbit[idx1++] = new Xyz(rCosNu, rSinNU, 0.0);
				Orbit[idx2--] = new Xyz(rCosNu, -rSinNU, 0.0);
			}
		}

		#endregion
	}
}
