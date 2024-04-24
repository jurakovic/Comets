using System;

namespace Comets.OrbitViewer
{
	public class PlanetOrbit
	{
		#region Const

		public const int OrbitDivisionCount = 300;

		#endregion

		#region Properties

		public Object Planet { get; private set; }
		private double JD { get; set; }
		private Xyz[] Orbit { get; set; }

		#endregion

		#region Constructor

		public PlanetOrbit(Object planet, ATime atime)
		{
			this.Planet = planet;
			this.JD = atime.JD;
			PlanetElm planetElm = new PlanetElm(planet, atime);
			this.Orbit = new Xyz[OrbitDivisionCount + 1];
			DoGetPlanetOrbit(planetElm);
			Matrix vec = Matrix.VectorConstant(planetElm.w * Math.PI / 180.0,
											   planetElm.N * Math.PI / 180.0,
											   planetElm.i * Math.PI / 180.0,
											   atime);
			Matrix prec = Matrix.PrecMatrix(atime.JD, 2451512.5);
			for (int i = 0; i <= OrbitDivisionCount; i++)
			{
				this.Orbit[i] = this.Orbit[i].Rotate(vec).Rotate(prec);
			}
		}

		#endregion

		#region DoGetPlanetOrbit

		private void DoGetPlanetOrbit(PlanetElm planetElm)
		{
			double ae2 = -2.0 * planetElm.a * planetElm.e;
			double t = Math.Sqrt(1.0 - planetElm.e * planetElm.e);
			int xp1 = 0;
			int xp2 = OrbitDivisionCount / 2;
			int xp3 = OrbitDivisionCount / 2;
			int xp4 = OrbitDivisionCount;
			double E = 0.0;
			for (int i = 0; i <= (OrbitDivisionCount / 4); i++, E += (360.0 / OrbitDivisionCount))
			{
				double rcosv = planetElm.a * (UdMath.udcos(E) - planetElm.e);
				double rsinv = planetElm.a * t * UdMath.udsin(E);
				this.Orbit[xp1++] = new Xyz(rcosv, rsinv, 0.0);
				this.Orbit[xp2--] = new Xyz(ae2 - rcosv, rsinv, 0.0);
				this.Orbit[xp3++] = new Xyz(ae2 - rcosv, -rsinv, 0.0);
				this.Orbit[xp4--] = new Xyz(rcosv, -rsinv, 0.0);
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
	}
}