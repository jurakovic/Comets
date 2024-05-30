using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Comets.Core
{
	public class SettingsBase
	{
		#region Properties

		public Location Location { get; set; }
		public CometCollection Comets { get; set; }
		public Comet SelectedComet { get; set; }
		public bool IsMultipleMode { get; set; }
		public FilterCollection Filters { get; set; }
		public string SortProperty { get; set; }
		public bool SortAscending { get; set; }
		public DateTime Start { get; set; }
		public DateTime Stop { get; set; }
		public decimal Interval { get; set; }
		public Dictionary<Comet, List<Ephemeris>> Ephemerides { get; set; }
		public bool AddNew { get; set; }

		public bool MaxSunDistChecked { get; set; }
		public double? MaxSunDistValue { get; set; }

		public bool MaxEarthDistChecked { get; set; }
		public double? MaxEarthDistValue { get; set; }

		public bool MinMagnitudeChecked { get; set; }
		public double? MinMagnitudeValue { get; set; }

		#endregion

		#region Methods

		public static bool ValidateCalculationAmount(SettingsBase settings)
		{
			bool retval = true;

			if (CommonManager.Settings.ShowLongCalculationConfirmation)
			{
				int iterationCount = (int)((settings.Stop.JD() - settings.Start.JD()) / settings.Interval);

				if (settings.IsMultipleMode)
					iterationCount *= settings.Comets.Count;

				if (iterationCount > 100000)
				{
					retval = MessageBox.Show(
						"Calculation using selected settings could take a while, depending on your PC performances.\n\nAre you sure you want to continue?\t\t\t",
						"Comets",
						//iterationCount.ToString(),
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question) == DialogResult.Yes;
				}
			}

			return retval;
		}

		public static bool ValidateCometDelete(string cometName)
		{
			bool retval = true;

			if (CommonManager.Settings.ShowDeleteCometConfirmation)
				retval = MessageBox.Show(String.Format("Do you really want to delete {0}\t\t\t", cometName), "Comets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

			return retval;
		}

		#endregion
	}
}
