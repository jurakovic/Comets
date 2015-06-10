
namespace Comets.OrbitViewer
{
	public static class Planet
	{
		#region Const

		public const int SUN = 0;
		public const int MERCURY = 1;
		public const int VENUS = 2;
		public const int EARTH = 3;
		public const int MARS = 4;
		public const int JUPITER = 5;
		public const int SATURN = 6;
		public const int URANUS = 7;
		public const int NEPTUNE = 8;
		public const int PLUTO = 9;

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
		public static Xyz GetPos(int planetNo, ATime atime)
		{
			if (R_JD_START < atime.JD && atime.JD < R_JD_END)
			{
				return PlanetExp.GetPos(planetNo, atime);
			}
			else
			{
				PlanetElm planetElm = new PlanetElm(planetNo, atime);
				return planetElm.GetPos();
			}
		}

		#endregion
	}
}