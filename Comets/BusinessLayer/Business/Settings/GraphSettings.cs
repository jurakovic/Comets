using System;

namespace Comets.BusinessLayer.Business
{
	public class GraphSettings : SettingsBase
	{
		#region Enum

		public enum DateFormatEnum { Date, JulianDay, JulianDay2, DaysFromT };

		#endregion

		#region Properties

		public DateTime DateStart { get; set; }
		public DateTime DateStop { get; set; }
		public int DaysFromTStartValue { get; set; }
		public int DaysFromTStopValue { get; set; }
		public DateFormatEnum DateFormat { get; set; }
		public bool DateRange { get; set; }
		public bool PerihelionLine { get; set; }
		public bool NowLine { get; set; }
		public bool Antialiasing { get; set; }

		public bool MinGraphMagnitudeChecked { get; set; }
		public double? MinGraphMagnitudeValue { get; set; }

		public bool MaxGraphMagnitudeChecked { get; set; }
		public double? MaxGraphMagnitudeValue { get; set; }

		#endregion

		#region Constructor

		public GraphSettings()
		{

		}

		#endregion

		#region ToString

		public override string ToString()
		{
			string retVal = "Graph - ";

			if (IsMultipleMode)
				retVal += Comets.Count + " comets";
			else
				retVal += SelectedComet.full;

			return retVal;
		}

		#endregion
	}
}
