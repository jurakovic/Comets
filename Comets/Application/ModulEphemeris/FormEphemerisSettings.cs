using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.ModulEphemeris
{
	public partial class FormEphemerisSettings : Form
	{
		#region Properties

		public EphemerisSettings EphemerisSettings { get; private set; }

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormEphemerisSettings(FilterCollection filters, EphemerisSettings settings = null)
		{
			InitializeComponent();

			selectCometControl.OnSelectedCometChanged += OnSelectedCometChanged;
			selectCometControl.OnCometsFiltered += OnCometsFiltered;

			if (settings == null)
			{
				modeControl.IsMultipleMode = false;
				modeControl.CometCount = CommonManager.UserCollection.Count;

				timespanControl.DateStart = CommonManager.DefaultDateStart;
				timespanControl.DateEnd = CommonManager.DefaultDateEnd;
				timespanControl.DayInterval = 1;
				timespanControl.HourInterval = 0;
				timespanControl.MinuteInterval = 0;
			}
			else
			{
				if (settings.Filters == null)
					settings.Filters = filters;

				modeControl.IsMultipleMode = settings.IsMultipleMode;
				modeControl.CometCount = settings.Comets.Count;

				timespanControl.DateStart = settings.Start;
				timespanControl.DateEnd = settings.Stop;
				timespanControl.DayInterval = settings.TimeSpan.Day;
				timespanControl.HourInterval = settings.TimeSpan.Hour;
				timespanControl.MinuteInterval = settings.TimeSpan.Minute;
				timespanControl.PerihelionDate = EphemerisManager.JDToLocalDateTimeSafe(settings.SelectedComet?.Tn);

				outputDataControl.LocalTime = settings.LocalTime;
				outputDataControl.RA = settings.RA;
				outputDataControl.Dec = settings.Dec;
				outputDataControl.EcLon = settings.EcLon;
				outputDataControl.EcLat = settings.EcLat;
				outputDataControl.HelioDist = settings.HelioDist;
				outputDataControl.GeoDist = settings.GeoDist;
				outputDataControl.Alt = settings.Alt;
				outputDataControl.Az = settings.Az;
				outputDataControl.Elongation = settings.Elongation;
				outputDataControl.Magnitude = settings.Magnitude;

				requirementsControl.MaxSunDistValue = settings.MaxSunDistValue;
				requirementsControl.MaxSunDistChecked = settings.MaxSunDistChecked;
				requirementsControl.MaxEarthDistValue = settings.MaxEarthDistValue;
				requirementsControl.MaxEarthDistChecked = settings.MaxEarthDistChecked;
				requirementsControl.MinMagnitudeValue = settings.MinMagnitudeValue;
				requirementsControl.MinMagnitudeChecked = settings.MinMagnitudeChecked;

				this.EphemerisSettings = settings;
			}
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormEphemerisSettings_Load(object sender, EventArgs e)
		{
			EphemerisSettings settings;

			if (this.EphemerisSettings == null)
			{
				settings = new EphemerisSettings();
				settings.Comets = new CometCollection(CommonManager.UserCollection);
				settings.Filters = CommonManager.Filters;
				settings.SortProperty = CommonManager.SortProperty;
				settings.SortAscending = CommonManager.SortAscending;
				settings.AddNew = true;

				this.EphemerisSettings = settings;
			}

			settings = this.EphemerisSettings;

			selectCometControl.Comets = settings.Comets;
			selectCometControl.Filters = settings.Filters;
			selectCometControl.SortProperty = settings.SortProperty;
			selectCometControl.SortAscending = settings.SortAscending;
			selectCometControl.DataBind(settings.SelectedComet);
		}

		private void FormEphemerisSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (cts != null && cts.IsCancellationRequested)
				e.Cancel = true;
		}

		#endregion

		#region Button

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (selectCometControl.SelectedComet != null)
			{
				if (requirementsControl.MaxSunDistChecked && requirementsControl.MaxSunDistValue == null)
				{
					MessageBox.Show("Please enter Maximum Sun distance value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (requirementsControl.MaxEarthDistChecked && requirementsControl.MaxEarthDistValue == null)
				{
					MessageBox.Show("Please enter Maximum Earth distance value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (requirementsControl.MinMagnitudeChecked && requirementsControl.MinMagnitudeValue == null)
				{
					MessageBox.Show("Please enter Minimum magnitude value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (timespanControl.DateEnd <= timespanControl.DateStart)
				{
					MessageBox.Show("End date must be greather than start date\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if ((timespanControl.DateEnd - timespanControl.DateStart).TotalDays > 300 * 365.25)
				{
					MessageBox.Show("Timespan must be less than 300 years.\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				decimal ind = timespanControl.DayInterval;
				decimal inh = timespanControl.HourInterval;
				decimal inm = timespanControl.MinuteInterval;

				decimal interval = ind + (inh + (inm / 60.0m)) / 24.0m;

				if (interval == 0.0m)
					interval = 1.0m;

				EphemerisSettings settings = this.EphemerisSettings;

				settings.SelectedComet = selectCometControl.SelectedComet;
				settings.Location = CommonManager.Settings.Location;

				settings.IsMultipleMode = modeControl.IsMultipleMode;

				settings.Start = timespanControl.DateStart;
				settings.Stop = timespanControl.DateEnd;
				settings.Interval = interval;

				settings.TimeSpan = new ATimeSpan(0, 0, (int)ind, (int)inh, (int)inm, 0);

				settings.LocalTime = outputDataControl.LocalTime;
				settings.RA = outputDataControl.RA;
				settings.Dec = outputDataControl.Dec;
				settings.EcLon = outputDataControl.EcLon;
				settings.EcLat = outputDataControl.EcLat;
				settings.HelioDist = outputDataControl.HelioDist;
				settings.GeoDist = outputDataControl.GeoDist;
				settings.Alt = outputDataControl.Alt;
				settings.Az = outputDataControl.Az;
				settings.Elongation = outputDataControl.Elongation;
				settings.Magnitude = outputDataControl.Magnitude;

				settings.MaxSunDistChecked = requirementsControl.MaxSunDistChecked;
				settings.MaxSunDistValue = requirementsControl.MaxSunDistValue;

				settings.MaxEarthDistChecked = requirementsControl.MaxEarthDistChecked;
				settings.MaxEarthDistValue = requirementsControl.MaxEarthDistValue;

				settings.MinMagnitudeChecked = requirementsControl.MinMagnitudeChecked;
				settings.MinMagnitudeValue = requirementsControl.MinMagnitudeValue;

				if (!CommonManager.Settings.IgnoreLongCalculationWarning && !SettingsBase.ValidateCalculationAmount(settings))
					return;

				if (settings.Ephemerides == null)
					settings.Ephemerides = new Dictionary<Comet, List<Ephemeris>>();

				FormMain main = this.Owner as FormMain;

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					main.SetProgressMaximumValue(settings.Comets.Count);

				cts = new CancellationTokenSource();

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(settings, FormMain.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					cts = null;
					settings.Ephemerides.Clear();
					main.HideProgress();
					return;
				}

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					main.SetProgressMaximumValue(settings.Ephemerides.Count);

				FormEphemeris fe = null;

				try
				{
					if (settings.AddNew)
					{
						fe = new FormEphemeris(settings) { Owner = this.Owner };
						fe.MdiParent = this.Owner;
						fe.WindowState = FormWindowState.Maximized;
						await fe.LoadResultsAsync(cts.Token);
						fe.Show();
					}
					else
					{
						fe = this.Owner.ActiveMdiChild as FormEphemeris;
						fe.EphemerisSettings = settings;
						await fe.LoadResultsAsync(cts.Token);
					}
				}
				catch (OperationCanceledException)
				{
					cts = null;
					settings.Ephemerides.Clear();
					main.HideProgress();

					if (settings.AddNew)
						fe.Dispose();

					return;
				}

				main.HideProgress();

				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (cts != null)
				cts.Cancel();
			else
				this.Close();
		}

		#endregion

		#region Events

		private void OnCometsFiltered(int cometCount)
		{
			modeControl.CometCount = cometCount;
		}

		private void OnSelectedCometChanged(DateTime? perihelionDate)
		{
			timespanControl.PerihelionDate = perihelionDate;
		}

		#endregion

		#endregion
	}
}
