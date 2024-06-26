﻿using Comets.Core;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.Ephemeris
{
	public partial class FormEphemerisSettings : ThemeForm
	{
		#region Events

		public event Action<int> OnProgressBegin;
		public event Action OnProgressEnd;

		#endregion

		#region Properties

		public EphemerisSettings EphemerisSettings { get; private set; }
		private IProgress<int> Progress { get; set; }

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormEphemerisSettings(FilterCollection filters, IProgress<int> progress, EphemerisSettings settings = null)
		{
			InitializeComponent();
			this.Progress = progress;
			this.EphemerisSettings = settings;

			if (this.EphemerisSettings != null && this.EphemerisSettings.Filters == null)
				this.EphemerisSettings.Filters = filters;

			selectCometControl.OnSelectedCometChanged += OnSelectedCometChanged;
			selectCometControl.OnCometsFiltered += OnCometsFiltered;
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormEphemerisSettings_Load(object sender, EventArgs e)
		{
			EphemerisSettings settings = this.EphemerisSettings;

			if (settings == null)
			{
				settings = new EphemerisSettings();
				settings.Comets = new CometCollection(CommonManager.UserCollection);
				settings.Filters = CommonManager.Filters;
				settings.SortProperty = CommonManager.SortProperty;
				settings.SortAscending = CommonManager.SortAscending;
				settings.Start = CommonManager.DefaultDateStart;
				settings.Stop = CommonManager.DefaultDateEnd;
				settings.AddNew = true;

				this.EphemerisSettings = settings;
			}

			selectCometControl.Comets = settings.Comets;
			selectCometControl.Filters = settings.Filters;
			selectCometControl.SortProperty = settings.SortProperty;
			selectCometControl.SortAscending = settings.SortAscending;
			selectCometControl.DataBind(settings.SelectedComet, settings.IsMultipleMode);

			timespanControl.DateStart = settings.Start;
			timespanControl.DateEnd = settings.Stop;
			timespanControl.PerihelionDate = EphemerisManager.JDToDateTimeSafe(settings.SelectedComet?.Tn);
			intervalControl.DayInterval = settings.TimeSpan.Days;
			intervalControl.HourInterval = settings.TimeSpan.Hours;
			intervalControl.MinuteInterval = settings.TimeSpan.Minutes;

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
			if ((selectCometControl.SelectedComet != null || selectCometControl.IsSelectedAll) && cts == null)
			{
				requirementsControl.ValidateData();
				timespanControl.ValidateData();

				decimal ind = intervalControl.DayInterval;
				decimal inh = intervalControl.HourInterval;
				decimal inm = intervalControl.MinuteInterval;

				decimal interval = ind + (inh + (inm / 60.0m)) / 24.0m;

				if (interval == 0.0m)
					interval = 1.0m;

				EphemerisSettings settings = this.EphemerisSettings;
				settings.Comets = selectCometControl.Comets;
				settings.Filters = selectCometControl.Filters;
				settings.SortProperty = selectCometControl.SortProperty;
				settings.SortAscending = selectCometControl.SortAscending;

				settings.SelectedComet = selectCometControl.SelectedComet;
				settings.Location = CommonManager.Settings.Location;
				settings.IsMultipleMode = selectCometControl.IsSelectedAll;

				settings.Start = timespanControl.DateStart;
				settings.Stop = timespanControl.DateEnd;
				settings.Interval = interval;

				settings.TimeSpan = new TimeSpan((int)ind, (int)inh, (int)inm, 0);

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

				if (!SettingsBase.ValidateCalculationAmount(settings))
					return;

				if (settings.Ephemerides == null)
					settings.Ephemerides = new Dictionary<Comet, List<Core.Ephemeris>>();

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					OnProgressBegin(settings.Comets.Count);

				cts = new CancellationTokenSource();

				void ResetState()
				{
					cts = null;
					settings.Ephemerides.Clear();
					OnProgressEnd();
				}

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(settings, this.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					ResetState();
					return;
				}
				catch
				{
					ResetState();
					throw;
				}

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					OnProgressBegin(settings.Ephemerides.Count);

				FormEphemeris fe = null;

				try
				{
					if (settings.AddNew)
					{
						fe = new FormEphemeris(settings, this.Progress) { Owner = this.Owner };
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
					ResetState();

					if (settings.AddNew)
						fe.Dispose();

					return;
				}
				catch
				{
					ResetState();
					throw;
				}

				OnProgressEnd();

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

		private void OnSelectedCometChanged(DateTime? perihelionDate)
		{
			this.timespanControl.PerihelionDate = perihelionDate;
		}

		private void OnCometsFiltered(CometCollection comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			this.EphemerisSettings.Comets = comets;
			this.EphemerisSettings.Filters = filters;
			this.EphemerisSettings.SortProperty = sortProperty;
			this.EphemerisSettings.SortAscending = sortAscending;
		}

		#endregion

		#endregion
	}
}
