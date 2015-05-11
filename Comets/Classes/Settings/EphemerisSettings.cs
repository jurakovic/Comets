
namespace Comets.Classes
{
    public class EphemerisSettings : CommonSettings
    {
        #region Properties

        public string IntervalText { get; set; }

        public bool LocalTime { get; set; }
        public bool RA { get; set; }
        public bool Dec { get; set; }
        public bool EcLon { get; set; }
        public bool EcLat { get; set; }
        public bool HelioDist { get; set; }
        public bool GeoDist { get; set; }
        public bool Alt { get; set; }
        public bool Az { get; set; }
        public bool Elongation { get; set; }
        public bool Magnitude { get; set; }

        #endregion

        #region Constructor

        public EphemerisSettings()
        {

        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return "Ephemeris - " + base.Comet.full;
        }

        #endregion
    }
}
