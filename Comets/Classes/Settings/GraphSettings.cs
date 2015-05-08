
namespace Comets.Classes
{
    public class GraphSettings : CommonSettings
    {
        #region Enum

        public enum DateFormatEnum { Date, JulianDay, JulianDay2, DaysFromT };

        #endregion

        #region Properties

        public DateFormatEnum DateFormat { get; set; }
        public double MinimumMagnitude { get; set; }
        public double MaximumMagnitude { get; set; }

        #endregion
    }
}
