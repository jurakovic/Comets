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

		public Color BackgroundColor { get; set; } = Color.White;
		public Color TextColor { get; set; } = Color.Black;
		public Color MagnitudeColor { get; set; } = Color.Red;
		public bool PerihelionLineChecked { get; set; } = true;
		public Color PerihelionLineColor { get; set; } = Color.RoyalBlue;
		public bool NowLineChecked { get; set; } = true;
		public Color NowLineColor { get; set; } = Color.LimeGreen;
		public bool AntialiasingChecked { get; set; }

		public ChartType GraphChartType { get; set; } = ChartType.Magnitude;

		public bool MinGraphValueChecked { get; set; }
		public double? MinGraphValue { get; set; }

		public bool MaxGraphValueChecked { get; set; } = true;
		public double? MaxGraphValue { get; set; } = 12.0;

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
