using System;

namespace Comets.Classes
{
    public class EphemerisSettings
    {
        public Location Location { get; set; }
        public Comet Comet { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public double Interval { get; set; }
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
        public string EphemerisResult { get; set; }

        public EphemerisSettings()
        {

        }

        public override string ToString()
        {
            return "Ephemeris - " + Comet.full;
        }
    }
}
