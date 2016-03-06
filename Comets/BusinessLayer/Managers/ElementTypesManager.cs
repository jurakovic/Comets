
namespace Comets.BusinessLayer.Managers
{
	public static class ElementTypesManager
	{
		#region TypeEnum

		public enum Type
		{
			MPC = 0,
			SkyMap = 1,
			Guide = 2,
			xephem = 3,
			HomePlanet = 4,
			MyStars = 5,
			TheSky = 6,
			StarryNight = 7,
			DeepSpace = 8,
			PCTCS = 9,
			EarthCenteredUniverse = 10,
			DanceOfThePlanets = 11,
			MegaStarV4 = 12,
			SkyChartIII = 13,
			VoyagerII = 14,
			SkyTools = 15,
			//Autostar = 16, //same as TheSky
			SpaceEngine = 17,
			Celestia = 18,
			CometForWindows = 19,
			NASA = 20,

			NoFileSelected = 51,
			FileNotFound = 52,
			EmptyFile = 53,
			Unknown = 54
		}

		#endregion

		#region TypeName

		public static string[] TypeName =
		{
			"MPC (Soft00Cmt)",
			"SkyMap (Soft01Cmt)",
			"Guide (Soft02Cmt)",
			"xephem (Soft03Cmt)",
			"Home Planet (Soft04Cmt)",
			"MyStars! (Soft05Cmt)",
			"TheSky (Soft06Cmt)",
			"Starry Night (Soft07Cmt)",
			"Deep Space (Soft08Cmt)",
			"PC-TCS (Soft09Cmt)",
			"Earth Centered Universe (Soft10Cmt)",
			"Dance of the Planets (Soft11Cmt)",
			"MegaStar V4.x (Soft12Cmt)",
			"SkyChart III (Soft13Cmt)",
			"Voyager II (Soft14Cmt)",
			"SkyTools (Soft15Cmt)",
			"Autostar (Soft16Cmt)",
			"SpaceEngine",
			"Celestia",
			"Comet for Windows",
			"NASA (ELEMENTS.COMET)"
		};

		#endregion

		#region Software

		public static string[] Software =
		{
			"MPC",
			"SkyMap",
			"Guide",
			"xephem",
			"Home Planet",
			"MyStars!",
			"TheSky",
			"Starry Night",
			"Deep Space",
			"PC-TCS",
			"Earth Centered Universe",
			"Dance of the Planets",
			"MegaStar V4.x",
			"SkyChart III",
			"Voyager II",
			"SkyTools",
			"Autostar",
			"SpaceEngine",
			"Celestia",
			"Comet for Windows",
			"NASA"
		};

		#endregion

		#region ExtensionFilters

		public static string[] ExtensionFilters =
		{
			"Text Documents (*.txt)|*.txt|",        // MPC
			"Text Documents (*.txt)|*.txt|",        // SkyMap
			"Text Documents (*.txt)|*.txt|",        // Guide
			"Text Documents (*.txt)|*.txt|",        // xephem
			"CSV files (*.csv)|*.csv|",             // Home Planet
			"Text Documents (*.txt)|*.txt|",        // MyStars!
			"Text Documents (*.txt)|*.txt|",        // TheSky
			"Text Documents (*.txt)|*.txt|",        // Starry Night
			"Text Documents (*.txt)|*.txt|",        // Deep Space
			"Text Documents (*.txt)|*.txt|",        // PC-TCS
			"Text Documents (*.txt)|*.txt|",        // Earth Centered Universe
			"Text Documents (*.txt)|*.txt|",        // Dance of the Planets
			"Text Documents (*.txt)|*.txt|",        // MegaStar V4.x
			"Text Documents (*.txt)|*.txt|",        // SkyChart III
			"Text Documents (*.txt)|*.txt|",        // Voyager II
			"Text Documents (*.txt)|*.txt|",        // SkyTools
			"Text Documents (*.txt)|*.txt|",        // Autostar
			"SpaceEngine (*.sc)|*.sc|",             // SpaceEngine
			"Celestia (*.ssc)|*.ssc|",              // Celestia
			"Dat files (*.dat)|*.dat|",             // Comet for Windows
			"COMET files (*.comet)|*.comet|"        // NASA
		};

		#endregion
	}
}
