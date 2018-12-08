using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.ModulGraph
{
	public partial class FormGraphSettings : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; private set; }

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormGraphSettings(FilterCollection filters, GraphSettings settings = null)
		{
			InitializeComponent();

			selectCometControl.OnSelectedCometChanged += OnSelectedCometChanged;
			selectCometControl.OnCometsFiltered += OnCometsFiltered;

			if (settings == null)
			{
				modeControl.IsMultipleMode = false;
				modeControl.CometCount = CommonManager.UserCollection.Count;

				int offset = 180;
				timespanControl.DateStart = CommonManager.DefaultDateStart;
				timespanControl.DateEnd = CommonManager.DefaultDateEnd;
				timespanControl.DaysBeforeT = -offset;
				timespanControl.DaysAfterT = offset;
				timespanControl.DateRange = true;
			}
			else
			{
				if (settings.Filters == null)
					settings.Filters = filters;

				modeControl.IsMultipleMode = settings.IsMultipleMode;
				modeControl.CometCount = settings.Comets.Count;

				timespanControl.DateStart = settings.Start;
				timespanControl.DateEnd = settings.Stop;
				timespanControl.DaysBeforeT = settings.DaysBeforeT;
				timespanControl.DaysAfterT = settings.DaysAfterT;
				timespanControl.DateRange = settings.DateRange;
				timespanControl.PerihelionDate = EphemerisManager.JDToLocalDateTimeSafe(settings.SelectedComet?.Tn);

				chartTypeControl.ChartType = settings.GraphChartType;

				chartOptionsControl.MagnitudeColor = settings.MagnitudeColor;
				chartOptionsControl.NowLineChecked = settings.NowLineChecked;
				chartOptionsControl.NowLineColor = settings.NowLineColor;
				chartOptionsControl.PerihelionLineChecked = settings.PerihelionLineChecked;
				chartOptionsControl.PerihelionLineColor = settings.PerihelionLineColor;
				chartOptionsControl.AntialiasingChecked = settings.AntialiasingChecked;

				valueRangeControl.MinValue = settings.MinGraphValue;
				valueRangeControl.MinValueChecked = settings.MinGraphValueChecked;
				valueRangeControl.MaxValue = settings.MaxGraphValue;
				valueRangeControl.MaxValueChecked = settings.MaxGraphValueChecked;

				this.GraphSettings = settings;
			}
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormGraphSettings_Load(object sender, EventArgs e)
		{
			GraphSettings settings;

			if (this.GraphSettings == null)
			{
				settings = new GraphSettings();
				settings.Comets = new CometCollection(CommonManager.UserCollection);
				settings.Filters = CommonManager.Filters;
				settings.SortProperty = CommonManager.SortProperty;
				settings.SortAscending = CommonManager.SortAscending;
				settings.AddNew = true;

				this.GraphSettings = settings;
			}
			settings = this.GraphSettings;

			selectCometControl.Comets = settings.Comets;
			selectCometControl.Filters = settings.Filters;
			selectCometControl.SortProperty = settings.SortProperty;
			selectCometControl.SortAscending = settings.SortAscending;
			selectCometControl.DataBind(settings.SelectedComet);
		}

		private void FormGraphSettings_FormClosing(object sender, FormClosingEventArgs e)
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
				if (valueRangeControl.MinValueChecked && valueRangeControl.MinValue == null)
				{
					MessageBox.Show("Enter Minimum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (valueRangeControl.MaxValueChecked && valueRangeControl.MaxValue == null)
				{
					MessageBox.Show("Enter Maximum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (valueRangeControl.MinValueChecked && valueRangeControl.MaxValueChecked && valueRangeControl.MinValue.GetValueOrDefault() >= valueRangeControl.MaxValue.GetValueOrDefault())
				{
					MessageBox.Show("Minimum value must be lower than Maximum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				int before = timespanControl.DaysBeforeT;
				int after = timespanControl.DaysAfterT;

				Comet comet = selectCometControl.SelectedComet;
				decimal startJd = comet.Tn + before; //negativan broj
				decimal stopJd = comet.Tn + after;

				DateTime dateBefore = EphemerisManager.JDToDateTime(startJd).ToLocalTime();
				DateTime dateAfter = EphemerisManager.JDToDateTime(stopJd).ToLocalTime();

				DateTime start = timespanControl.DateRange ? timespanControl.DateStart : dateBefore;
				DateTime stop = timespanControl.DateRange ? timespanControl.DateEnd : dateAfter;

				if (stop <= start)
				{
					MessageBox.Show("End date must be greather than start date\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				decimal interval = 0.0m;
				decimal totalDays = stop.JD() - start.JD();

				if (totalDays <= 100)
					interval = totalDays / 100.0m;
				else if (totalDays < 365)
					interval = 1;
				else if (totalDays < 10 * 365.25m)
					interval = 2;
				else if (totalDays < 50 * 365.25m)
					interval = 5;
				else if (totalDays < 100 * 365.25m)
					interval = 15;
				else if (totalDays < 200 * 365.25m)
					interval = 30;
				else if (totalDays < 300 * 365.25m)
					interval = 40;
				else
				{
					MessageBox.Show("Timespan must be less than 300 years.\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				GraphSettings settings = this.GraphSettings;

				settings.SelectedComet = comet;
				settings.Location = CommonManager.Settings.Location;

				settings.DateRange = timespanControl.DateRange;

				settings.IsMultipleMode = modeControl.IsMultipleMode;

				settings.Start = start;
				settings.Stop = stop;
				settings.Interval = interval;

				settings.DateStart = timespanControl.DateStart;
				settings.DateStop = timespanControl.DateEnd;

				settings.DaysBeforeT = timespanControl.DaysBeforeT;
				settings.DaysAfterT = timespanControl.DaysAfterT;

				settings.GraphChartType = chartTypeControl.ChartType;

				settings.MagnitudeColor = chartOptionsControl.MagnitudeColor;

				settings.NowLineChecked = chartOptionsControl.NowLineChecked;
				settings.NowLineColor = chartOptionsControl.NowLineColor;

				settings.PerihelionLineChecked = chartOptionsControl.PerihelionLineChecked;
				settings.PerihelionLineColor = chartOptionsControl.PerihelionLineColor;

				settings.AntialiasingChecked = chartOptionsControl.AntialiasingChecked;

				settings.MinGraphValueChecked = valueRangeControl.MinValueChecked;
				settings.MinGraphValue = valueRangeControl.MinValue;

				settings.MaxGraphValueChecked = valueRangeControl.MaxValueChecked;
				settings.MaxGraphValue = valueRangeControl.MaxValue;

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

				if (settings.AddNew && settings.SelectedComet != null)
				{
					FormGraph fg = new FormGraph(settings);
					fg.MdiParent = this.Owner;
					fg.WindowState = FormWindowState.Maximized;
					fg.LoadGraph();
					fg.Show();
				}
				else if (settings.Ephemerides != null && settings.Ephemerides.Count > 0)
				{
					FormGraph fg = this.Owner.ActiveMdiChild as FormGraph;
					fg.GraphSettings = settings;
					fg.LoadGraph();
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
