
namespace Comets.OrbitViewer
{
	public static class Planet
	{
		#region Const

		public const int Sun = 0;
		public const int Mercury = 1;
		public const int Venus = 2;
		public const int Earth = 3;
		public const int Mars = 4;
		public const int Jupiter = 5;
		public const int Saturn = 6;
		public const int Uranus = 7;
		public const int Neptune = 8;
		public const int Pluto = 9;

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