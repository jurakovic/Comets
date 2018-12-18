using System;
using System.Drawing;

namespace Comets.Core
{
	public class GraphSettings : SettingsBase
	{
		#region Enum

		public enum ChartType { Magnitude, SunDistance, EarthDistance };

		#endregion

		#region Properties

		public DateTime DateStart { get; set; }
		public DateTime DateStop { get; set; }
		public int DaysBeforeT { get; set; }
		public int DaysAfterT { get; set; }
		public bool DateRange { get; set; }
		public Color MagnitudeColor { get; set; }
		public bool PerihelionLineChecked { get; set; }
		public Color PerihelionLineColor { get; set; }
		public bool NowLineChecked { get; set; }
		public Color NowLineColor { get; set; }
		public bool AntialiasingChecked { get; set; }

		public ChartType GraphChartType { get; set; }

		public bool MinGraphValueChecked { get; set; }
		public double? MinGraphValue { get; set; }

		public bool MaxGraphValueChecked { get; set; }
		public double? MaxGraphValue { get; set; }

		#endregion

		#region Constructor

		public GraphSettings()
		{

		}

		#endregion

		#region ToString

		public override string ToString()
		{
			string retVal = String.Empty;

			switch (GraphChartType)
			{
				case ChartType.Magnitude:
					retVal = "Magnitude graph - ";
					break;
				case ChartType.SunDistance:
					retVal = "Sun distance graph - ";
					break;
				case ChartType.EarthDistance:
					retVal = "Earth distance graph - ";
					break;
			}

			if (IsMultipleMode)
				retVal += Comets.Count + " comets";
			else
				retVal += SelectedComet.full;

			return retVal;
		}

		#endregion
	}
}
