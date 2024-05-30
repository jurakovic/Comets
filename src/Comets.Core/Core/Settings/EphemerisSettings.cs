using System;

namespace Comets.Core
{
	public class EphemerisSettings : SettingsBase
	{
		#region Properties

		public TimeSpan TimeSpan { get; set; } = new TimeSpan(1, 0, 0, 0);
		public bool LocalTime { get; set; } = true;
		public bool RA { get; set; } = true;
		public bool Dec { get; set; } = true;
		public bool EcLon { get; set; }
		public bool EcLat { get; set; }
		public bool HelioDist { get; set; } = true;
		public bool GeoDist { get; set; } = true;
		public bool Alt { get; set; } = true;
		public bool Az { get; set; } = true;
		public bool Elongation { get; set; } = true;
		public bool Magnitude { get; set; } = true;

		#endregion

		#region Constructor

		public EphemerisSettings()
		{
			base.MinMagnitudeChecked = true;
			base.MinMagnitudeValue = 12.0;
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
