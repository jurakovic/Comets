
namespace Comets.OrbitViewer
{
	public static class Planet
	{
		#region Const

		private static double R_JD_START = 2433282.5;	// 1950.0
		private static double R_JD_END = 2473459.5;		// 2060.0

		#endregion

		#region GetPos

		/// <summary>
		/// Get Planet Position in Ecliptic Coordinates (Equinox Date)
		/// </summary>
		/// <param name="planetNo"></param>
		/// <param name="atime"></param>
		/// <returns></returns>
		public static Xyz GetPos(Object planet, ATime atime)
		{
			if (R_JD_START < atime.JD && atime.JD < R_JD_END)
			{
				return PlanetExp.GetPos(planet, atime);
			}
			else
			{
				PlanetElm planetElm = new PlanetElm(planet, atime);
				return planetElm.GetPos();
			}
		}

		#endregion
	}
}