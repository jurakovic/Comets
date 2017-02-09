using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application.ModulGraph
{
	public partial class FormGraphSettings : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; private set; }

		private DateTime _start;
		private DateTime DateStart
		{
			get { return _start; }
			set
			{
				_start = value;
				btnStartDate.Text = _start.ToString(FormMain.DateTimeFormat);
			}
		}

		private DateTime _end;
		private DateTime DateEnd
		{
			get { return _end; }
			set
			{
				_end = value;
				btnEndDate.Text = _end.ToString(FormMain.DateTimeFormat);
			}
		}

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormGraphSettings(FilterCollection filters, GraphSettings settings = null)
		{
			InitializeComponent();

			txtDaysFromTStart.Tag = new ValNum(-3653, -1);
			txtDaysFromTStop.Tag = new ValNum(1, 3653);
			txtMinValue.Tag = ValNum.VMagnitude;
			txtMaxValue.Tag = ValNum.VMagnitude;

			if (settings == null)
			{
				DateStart = CommonManager.DefaultDateStart;
				DateEnd = CommonManager.DefaultDateEnd;

				btnTimespanDaysFromTDefault_Click(null, null);
				rbtnRangeDate.Checked = true;
			}
			else
			{
				if (settings.Filters == null)
					settings.Filters = filters;

				rbtnSingle.Checked = !settings.IsMultipleMode;
				rbtnMultiple.Checked = settings.IsMultipleMode;

				DateStart = settings.DateStart;
				DateEnd = settings.DateStop;

				txtDaysFromTStart.Text = settings.DaysFromTStartValue.ToString();
				txtDaysFromTStop.Text = settings.DaysFromTStopValue.ToString();

				if (settings.DateRange)
					rbtnRangeDate.Checked = true;
				else
					rbtnRangeDaysFromT.Checked = true;

				switch (settings.GraphChartType)
				{
					case GraphSettings.ChartType.Magnitude:
						rbtnMagnitude.Checked = true;
						break;
					case GraphSettings.ChartType.SunDistance:
						rbtnSunDistance.Checked = true;
						break;
					case GraphSettings.ChartType.EarthDistance:
						rbtnEarthDistance.Checked = true;
						break;
				}

				pnlMagnitudeColor.BackColor = settings.MagnitudeColor;

				cbxNowLine.Checked = settings.NowLineChecked;
				pnlNowLineColor.BackColor = settings.NowLineColor;

				cbxPerihelionLine.Checked = settings.PerihelionLineChecked;
				pnlPerihLineColor.BackColor = settings.PerihelionLineColor;

				cbxAntialiasing.Checked = settings.AntialiasingChecked;

				txtMinValue.Text = settings.MinGraphValue != null ? settings.MinGraphValue.Value.ToString() : String.Empty;
				cbxMinValue.Checked = settings.MinGraphValueChecked;

				txtMaxValue.Text = settings.MaxGraphValue != null ? settings.MaxGraphValue.Value.ToString() : String.Empty;
				cbxMaxValue.Checked = settings.MaxGraphValueChecked;

				this.GraphSettings = settings;
			}
		}

		#endregion

		#region FormGraphSettings_Load

		private void FormGraphSettings_Load(object sender, EventArgs e)
		{
			if (this.GraphSettings == null)
			{
				GraphSettings settings = new GraphSettings();
				settings.Comets = new CometCollection(CommonManager.UserCollection);
				settings.Filters = CommonManager.Filters;
				settings.SortProperty = CommonManager.SortProperty;
				settings.SortAscending = CommonManager.SortAscending;
				settings.AddNew = true;

				this.GraphSettings = settings;
			}

			BindCollection();
		}

		#endregion

		#region FormGraphSettings_FormClosing

		private void FormGraphSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (cts != null && cts.IsCancellationRequested)
				e.Cancel = true;
		}

		#endregion

		#region cbComet_SelectedIndexChanged

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				Comet c = this.GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", EphemerisManager.JDToDateTime(c.Tn).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
				lblPerihDist.Text = String.Format("Perihelion distance:          {0:0.000000} AU", c.q);
				lblPeriod.Text = String.Format("Period:                              {0}", c.P < 10000 ? c.P.ToString("0.000000") + " years" : "-");
			}
		}

		#endregion

		#region btnFilter_Click

		private void btnFilter_Click(object sender, EventArgs e)
		{
			GraphSettings sett = this.GraphSettings;

			using (FormDatabase fdb = new FormDatabase(sett.Comets, sett.Filters, sett.SortProperty, sett.SortAscending, true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					sett.Comets = fdb.Comets;
					sett.Filters = fdb.Filters;
					sett.SortProperty = fdb.SortProperty;
					sett.SortAscending = fdb.SortAscending;

					BindCollection();
				}
			}
		}

		#endregion

		#region Chart options

		private void pnColorCommon_Click(object sender, EventArgs e)
		{
			Panel pnl = sender as Panel;

			using (ColorDialog cd = new ColorDialog())
			{
				cd.Color = pnl.BackColor;
				cd.FullOpen = true;

				if (cd.ShowDialog() == DialogResult.OK)
					pnl.BackColor = cd.Color;
			}
		}

		#endregion

		#region Timespan

		private void btnStartDate_Click(object sender, EventArgs e)
		{
			DateStart = ShowFormDateTime(CommonManager.DefaultDateStart, DateStart, GetT());
		}

		private void btnEndDate_Click(object sender, EventArgs e)
		{
			DateEnd = ShowFormDateTime(CommonManager.DefaultDateEnd, DateEnd, GetT());
		}

		private DateTime ShowFormDateTime(DateTime def, DateTime current, decimal? jd)
		{
			using (FormDateTime fdt = new FormDateTime(def, current, jd))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					current = fdt.SelectedDateTime;
			}

			rbtnRangeDate.Checked = true;
			return current;
		}

		private decimal? GetT()
		{
			return this.GraphSettings.Comets.ElementAtOrDefault(cbComet.SelectedIndex)?.Tn;
		}

		private void txtDaysFromTCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtDaysFromTCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtDaysFromTCommon_TextChanged(object sender, EventArgs e)
		{
			rbtnRangeDaysFromT.Checked = true;
		}

		private void btnTimespanDaysFromTDefault_Click(object sender, EventArgs e)
		{
			int offset = 180;
			txtDaysFromTStart.Text = (-offset).ToString();
			txtDaysFromTStop.Text = offset.ToString();
		}

		#endregion

		#region Magnitude

		private void txtMagCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtMinMag_TextChanged(object sender, EventArgs e)
		{
			cbxMinValue.Checked = txtMinValue.TextLength > 0;
		}

		private void txtMaxMag_TextChanged(object sender, EventArgs e)
		{
			cbxMaxValue.Checked = txtMaxValue.TextLength > 0;
		}

		#endregion

		#region btnOk_Click

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				if (cbxMinValue.Checked && txtMinValue.TextLength == 0)
				{
					MessageBox.Show("Enter Minimum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMaxValue.Checked && txtMaxValue.TextLength == 0)
				{
					MessageBox.Show("Enter Maximum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMinValue.Checked && cbxMaxValue.Checked && txtMinValue.Double() >= txtMaxValue.Double())
				{
					MessageBox.Show("Minimum value must be lower than Maximum value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				int before = txtDaysFromTStart.Int();
				int after = txtDaysFromTStop.Int();

				Comet comet = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);
				decimal startJd = comet.Tn + before; //negativan broj
				decimal stopJd = comet.Tn + after;

				DateTime daysFromTStart = EphemerisManager.JDToDateTime(startJd).ToLocalTime();
				DateTime daysFromTStop = EphemerisManager.JDToDateTime(stopJd).ToLocalTime();

				DateTime start = rbtnRangeDate.Checked ? DateStart : daysFromTStart;
				DateTime stop = rbtnRangeDate.Checked ? DateEnd : daysFromTStop;

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

				settings.DateRange = rbtnRangeDate.Checked;

				settings.IsMultipleMode = rbtnMultiple.Checked;

				settings.Start = start;
				settings.Stop = stop;
				settings.Interval = interval;

				settings.DateStart = DateStart;
				settings.DateStop = DateEnd;

				settings.DaysFromTStartValue = txtDaysFromTStart.Int();
				settings.DaysFromTStopValue = txtDaysFromTStop.Int();

				if (rbtnMagnitude.Checked)
					settings.GraphChartType = GraphSettings.ChartType.Magnitude;
				else if (rbtnSunDistance.Checked)
					settings.GraphChartType = GraphSettings.ChartType.SunDistance;
				else// if (rbtnEarthDistance.Checked)
					settings.GraphChartType = GraphSettings.ChartType.EarthDistance;

				settings.MagnitudeColor = pnlMagnitudeColor.BackColor;

				settings.NowLineChecked = cbxNowLine.Checked;
				settings.NowLineColor = pnlNowLineColor.BackColor;

				settings.PerihelionLineChecked = cbxPerihelionLine.Checked;
				settings.PerihelionLineColor = pnlPerihLineColor.BackColor;

				settings.AntialiasingChecked = cbxAntialiasing.Checked;

				settings.MinGraphValueChecked = cbxMinValue.Checked;
				settings.MinGraphValue = txtMinValue.TextLength > 0 ? (double?)txtMinValue.Double() : null;

				settings.MaxGraphValueChecked = cbxMaxValue.Checked;
				settings.MaxGraphValue = txtMaxValue.TextLength > 0 ? (double?)txtMaxValue.Double() : null;

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

		#endregion

		#region btnCancel_Click

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (cts != null)
				cts.Cancel();
			else
				this.Close();
		}

		#endregion

		#region BindCollection

		private void BindCollection()
		{
			GraphSettings settings = this.GraphSettings;

			cbComet.DisplayMember = CometManager.PropertyEnum.full.ToString();
			cbComet.DataSource = settings.Comets;

			if (settings.Comets.Count > 0)
			{
				if (settings.SelectedComet != null && settings.Comets.Contains(settings.SelectedComet))
				{
					cbComet.SelectedIndex = settings.Comets.IndexOf(settings.SelectedComet);
				}
				else
				{
					//comet with nearest perihelion date
					decimal jdNow = DateTime.Now.JD();
					Comet c = settings.Comets.OrderBy(x => Math.Abs(x.Tn - jdNow)).First();
					cbComet.SelectedIndex = settings.Comets.IndexOf(c);
				}
			}

			lblMultipleCount.Text = settings.Comets.Count + " comets";
		}

		#endregion
	}
}
