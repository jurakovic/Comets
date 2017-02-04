using Comets.BusinessLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Comets.BusinessLayer.Business
{
	public class SettingsBase
	{
		#region Properties

		public Location Location { get; set; }
		public CometCollection Comets { get; set; }
		public Comet SelectedComet { get; set; }
		public bool IsMultipleMode { get; set; }
		public FilterCollection Filters { get; set; }
		public string OrderProperty { get; set; }
		public bool OrderAscending { get; set; }
		public DateTime Start { get; set; }
		public DateTime Stop { get; set; }
		public double Interval { get; set; }
		public Dictionary<Comet, List<Ephemeris>> Ephemerides { get; set; }
		public bool AddNew { get; set; }

		public bool MaxSunDistChecked { get; set; }
		public double? MaxSunDistValue { get; set; }

		public bool MaxEarthDistChecked { get; set; }
		public double? MaxEarthDistValue { get; set; }

		public bool MinMagnitudeChecked { get; set; }
		public double? MinMagnitudeValue { get; set; }

		public bool MaxMagnitudeChecked { get; set; }
		public double? MaxMagnitudeValue { get; set; }

		#endregion

		#region Methods

		public static bool ValidateCalculationAmount(SettingsBase settings)
		{
			bool retval = true;

			int iterationCount = (int)((settings.Stop.JD() - settings.Start.JD()) / settings.Interval);

			if (settings.IsMultipleMode)
				iterationCount *= settings.Comets.Count;

			if (iterationCount > 100000)
			{
				DialogResult dr = MessageBox.Show(
					"Calculation using selected settings could take a while, depending on your PC performances.\n\nAre you sure you want to continue?\t\t\t",
					"Comets", 
					//iterationCount.ToString(),
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (dr == DialogResult.No)
					retval = false;
			}

			return retval;
		}

		#endregion
	}
}
