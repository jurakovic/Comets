
namespace Comets.Classes
{
	public class GraphSettings : CommonSettings
	{
		#region Enum

		public enum DateFormatEnum { Date, JulianDay, JulianDay2, DaysFromT };

		#endregion

		#region Properties

		public int DaysFromTStart { get; set; }
		public int DaysFromTStop { get; set; }
		public DateFormatEnum DateFormat { get; set; }
		public bool DateRange { get; set; }
		public double MinMagnitude { get; set; }
		public double MaxMagnitude { get; set; }

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
