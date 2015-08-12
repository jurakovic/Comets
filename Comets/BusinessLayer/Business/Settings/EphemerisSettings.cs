using Comets.OrbitViewer;

namespace Comets.BusinessLayer.Business
{
	public class EphemerisSettings : SettingsBase
	{
		#region Properties

		public ATimeSpan TimeSpan { get; set; }
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
			string retVal = "Ephemeris - ";

			if (IsMultipleMode)
				retVal += Comets.Count + " comets";
			else
				retVal += SelectedComet.full;

			return retVal;
		}

		#endregion
	}
}
