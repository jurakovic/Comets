using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.Graph
{
	public partial class FormGraphSettings : ThemeForm
	{
		#region Events

		public event Action<int> OnProgressBegin;
		public event Action OnProgressEnd;

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
				timespanControl.DateStart = CommonManager.DefaultDateStart;
				timespanControl.DateEnd = CommonManager.DefaultDateEnd;
			}
			else
			{
				if (settings.Filters == null)
					settings.Filters = filters;

				timespanControl.DateStart = settings.Start;
				timespanControl.DateEnd = settings.Stop;
				timespanControl.PerihelionDate = EphemerisManager.JDToDateTimeSafe(settings.SelectedComet?.Tn);

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
			selectCometControl.DataBind(settings.SelectedComet, settings.IsMultipleMode);
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
			if ((selectCometControl.SelectedComet != null || selectCometControl.IsSelectedAll) && cts == null)
			{
				valueRangeControl.ValidateData();
				timespanControl.ValidateData();

				GraphSettings settings = this.GraphSettings;
				settings.Comets = selectCometControl.Comets;
				settings.Filters = selectCometControl.Filters;
				settings.SortProperty = selectCometControl.SortProperty;
				settings.SortAscending = selectCometControl.SortAscending;

				Comet comet = selectCometControl.SelectedComet;
				settings.SelectedComet = comet;
				settings.Location = CommonManager.Settings.Location;

				settings.IsMultipleMode = selectCometControl.IsSelectedAll;

				decimal totalDays = timespanControl.DateEnd.JD() - timespanControl.DateStart.JD();
				decimal interval = 0.0m;

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

				settings.Start = timespanControl.DateStart;
				settings.Stop = timespanControl.DateEnd;
				settings.Interval = interval;

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

				if (!SettingsBase.ValidateCalculationAmount(settings))
					return;

				if (settings.Ephemerides == null)
					settings.Ephemerides = new Dictionary<Comet, List<Ephemeris>>();

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

				if (settings.AddNew)
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
		}

		#endregion

		#endregion
	}
}
