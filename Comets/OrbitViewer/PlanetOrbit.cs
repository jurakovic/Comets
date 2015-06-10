using System;

namespace Comets.OrbitViewer
{
	public class PlanetOrbit
	{
		#region Properties

		private int PlanetNo { get; set; }
		private double JD { get; set; }
		public int Division { get; private set; }
		private Xyz[] Orbit { get; set; }

		#endregion

		#region Constructor

		public PlanetOrbit(int planetNo, ATime atime, int division)
		{
			this.PlanetNo = planetNo;
			this.JD = atime.JD;
			this.Division = division;
			PlanetElm planetElm = new PlanetElm(planetNo, atime);
			this.Orbit = new Xyz[division + 1];
			DoGetPlanetOrbit(planetElm);
			Matrix vec = Matrix.VectorConstant(planetElm.w * Math.PI / 180.0,
											   planetElm.N * Math.PI / 180.0,
											   planetElm.i * Math.PI / 180.0,
											   atime);
			Matrix prec = Matrix.PrecMatrix(atime.JD, 2451512.5);
			for (int i = 0; i <= division; i++)
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
			int xp2 = (this.Division / 2);
			int xp3 = (this.Division / 2);
			int xp4 = this.Division;
			double E = 0.0;
			for (int i = 0; i <= (this.Division / 4); i++, E += (360.0 / this.Division))
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