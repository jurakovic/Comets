
namespace Comets.Classes
{
    public static class ElementTypes
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
            Autostar = 16,
            Celestia = 17,
            CometForWindows = 18,
            NASA = 19,

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
            "Celestia",
            "Comet for Windows",
            "NASA"
        };

        #endregion
    }
}
