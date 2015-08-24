using System;
using System.Drawing;

namespace Comets.BusinessLayer.Business
{
	public class GraphSettings : SettingsBase
	{
		#region Properties

		public DateTime DateStart { get; set; }
		public DateTime DateStop { get; set; }
		public int DaysFromTStartValue { get; set; }
		public int DaysFromTStopValue { get; set; }
		public bool DateRange { get; set; }
		public Color MagnitudeColor { get; set; }
		public bool PerihelionLineChecked { get; set; }
		public Color PerihelionLineColor { get; set; }
		public bool NowLineChecked { get; set; }
		public Color NowLineColor { get; set; }
		public bool AntialiasingChecked { get; set; }

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
