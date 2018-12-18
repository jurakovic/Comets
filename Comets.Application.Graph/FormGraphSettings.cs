using Comets.Core;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.Graph
{
	public partial class FormGraphSettings : Form
	{
		#region Events

		public Action<int> OnProgressBegin;
		public Action OnProgressEnd;

		#endregion

		#region Properties

		public GraphSettings GraphSettings { get; private set; }

		private IProgress<int> Progress { get; set; }

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormGraphSettings(FilterCollection filters, IProgress<int> progress, GraphSettings settings = null)
		{
			InitializeComponent();
			this.Progress = progress;

			selectCometControl.OnSelectedCometChanged += OnSelectedCometChanged;
			selectCometControl.OnCometsFiltered += OnCometsFiltered;

			if (settings == null)
			{
				modeControl.IsMultipleMode = false;
				modeControl.CometCount = CommonManager.UserCollection.Count;

				int offset = 180;
				timespanControl.SelectedDateStart = CommonManager.DefaultDateStart;
				timespanControl.SelectedDateEnd = CommonManager.DefaultDateEnd;
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

				timespanControl.SelectedDateStart = settings.Start;
				timespanControl.SelectedDateEnd = settings.Stop;
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
				valueRangeControl.ValidateData();
				timespanControl.ValidateData();

				Comet comet = selectCometControl.SelectedComet;
				GraphSettings settings = this.GraphSettings;

				settings.SelectedComet = comet;
				settings.Location = CommonManager.Settings.Location;

				settings.DateRange = timespanControl.DateRange;

				settings.IsMultipleMode = modeControl.IsMultipleMode;

				settings.Start = timespanControl.DateStart;
				settings.Stop = timespanControl.DateEnd;
				settings.Interval = timespanControl.Interval;

				settings.DateStart = timespanControl.SelectedDateStart;
				settings.DateStop = timespanControl.SelectedDateEnd;

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

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					OnProgressBegin?.Invoke(settings.Comets.Count);

				cts = new CancellationTokenSource();

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(settings, this.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					cts = null;
					settings.Ephemerides.Clear();
					OnProgressEnd();
					return;
				}
				catch
				{
					cts = null;
					throw;
				}

				if (settings.AddNew && settings.SelectedComet != null)
				{
					FormGraph fg = new FormGraph(settings, this.Progress);
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
			this.GraphSettings.Comets = comets;
			this.GraphSettings.Filters = filters;
			this.GraphSettings.SortProperty = sortProperty;
			this.GraphSettings.SortAscending = sortAscending;
			this.modeControl.CometCount = comets.Count;
		}

		#endregion

		#endregion
	}
}
