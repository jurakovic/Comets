
namespace Comets.Classes
{
    public class GraphSettings : CommonSettings
    {
        #region Enum

        public enum DateFormatEnum { Date, JulianDay, JulianDay2, DaysFromT };

        #endregion

        #region Properties

        public DateFormatEnum DateFormat { get; set; }
        public bool DateRange { get; set; }
        public string Dates { get; set; }
        public string DaysFromT { get; set; }
        public double MinimumMagnitude { get; set; }
        public double MaximumMagnitude { get; set; }

        #endregion

        #region Constructor

        public GraphSettings()
        {

        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return "Graph - " + base.Comet.full;
        }

        #endregion
    }
}
