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
			txtMinMag.Tag = new ValNum(-20, 40, 2);
			txtMaxMag.Tag = new ValNum(-20, 40, 2);

			GraphSettings = settings;

			if (GraphSettings == null)
			{
				DateStart = FormMain.DefaultDateStart;
				DateEnd = FormMain.DefaultDateEnd;

				btnTimespanDaysFromTDefault_Click(null, null);
				rbRangeDate.Checked = true;
			}
			else
			{
				if (GraphSettings.Filters == null)
					GraphSettings.Filters = filters;

				rbtnSingle.Checked = !GraphSettings.IsMultipleMode;
				rbtnMultiple.Checked = GraphSettings.IsMultipleMode;

				DateStart = GraphSettings.DateStart;
				DateEnd = GraphSettings.DateStop;

				txtDaysFromTStart.Text = GraphSettings.DaysFromTStartValue.ToString();
				txtDaysFromTStop.Text = GraphSettings.DaysFromTStopValue.ToString();

				if (GraphSettings.DateRange)
					rbRangeDate.Checked = true;
				else
					rbRangeDaysFromT.Checked = true;

				pnlMagnitudeColor.BackColor = GraphSettings.MagnitudeColor;

				cbxNowLine.Checked = GraphSettings.NowLineChecked;
				pnlNowLineColor.BackColor = GraphSettings.NowLineColor;

				cbxPerihelionLine.Checked = GraphSettings.PerihelionLineChecked;
				pnlPerihLineColor.BackColor = GraphSettings.PerihelionLineColor;

				cbxAntialiasing.Checked = GraphSettings.AntialiasingChecked;

				txtMinMag.Text = GraphSettings.MinGraphMagnitudeValue != null ? GraphSettings.MinGraphMagnitudeValue.Value.ToString() : String.Empty;
				cbxMinMag.Checked = GraphSettings.MinGraphMagnitudeChecked;

				txtMaxMag.Text = GraphSettings.MaxGraphMagnitudeValue != null ? GraphSettings.MaxGraphMagnitudeValue.Value.ToString() : String.Empty;
				cbxMaxMag.Checked = GraphSettings.MaxGraphMagnitudeChecked;
			}
		}

		#endregion

		#region FormGraphSettings_Load

		private void FormGraphSettings_Load(object sender, EventArgs e)
		{
			if (GraphSettings == null)
			{
				GraphSettings = new GraphSettings();
				GraphSettings.Comets = new CometCollection(FormMain.UserCollection);
				GraphSettings.Filters = FormMain.Filters;
				GraphSettings.SortProperty = FormMain.SortProperty;
				GraphSettings.SortAscending = FormMain.SortAscending;
				GraphSettings.AddNew = true;
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
				Comet c = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", Utils.JDToDateTime(c.Tn).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
				lblPerihDist.Text = String.Format("Perihelion distance:          {0:0.000000} AU", c.q);
				lblPeriod.Text = String.Format("Period:                              {0}", c.P < 10000 ? c.P.ToString("0.000000") + " years" : "-");
			}
		}

		#endregion

		#region btnFilter_Click

		private void btnFilter_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(
				GraphSettings.Comets,
				GraphSettings.Filters,
				GraphSettings.SortProperty,
				GraphSettings.SortAscending,
				true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					GraphSettings.Comets = fdb.Comets;
					GraphSettings.Filters = fdb.Filters;
					GraphSettings.SortProperty = fdb.SortProperty;
					GraphSettings.SortAscending = fdb.SortAscending;

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
				{
					pnl.BackColor = cd.Color;
				}
			}
		}

		#endregion

		#region Timespan

		private void btnStartDate_Click(object sender, EventArgs e)
		{
			DateStart = ShowFormDateTime(FormMain.DefaultDateStart, DateStart, GetT());
		}

		private void btnEndDate_Click(object sender, EventArgs e)
		{
			DateEnd = ShowFormDateTime(FormMain.DefaultDateEnd, DateEnd, GetT());
		}

		private DateTime ShowFormDateTime(DateTime def, DateTime current, double? jd)
		{
			using (FormDateTime fdt = new FormDateTime(def, current, jd))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					current = fdt.SelectedDateTime;
			}

			rbRangeDate.Checked = true;
			return current;
		}

		private double? GetT()
		{
			double? T = null;

			if (cbComet.SelectedIndex >= 0)
				T = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex).Tn;

			return T;
		}

		private void txtDaysFromTCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtDaysFromTCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtDaysFromTCommon_TextChanged(object sender, EventArgs e)
		{
			rbRangeDaysFromT.Checked = true;
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
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtMinMag_TextChanged(object sender, EventArgs e)
		{
			cbxMinMag.Checked = txtMinMag.TextLength > 0;
		}

		private void txtMaxMag_TextChanged(object sender, EventArgs e)
		{
			cbxMaxMag.Checked = txtMaxMag.TextLength > 0;
		}

		#endregion

		#region btnOk_Click

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				if (cbxMinMag.Checked && txtMinMag.TextLength == 0)
				{
					MessageBox.Show("Please enter Minimum magnitude value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMaxMag.Checked && txtMaxMag.TextLength == 0)
				{
					MessageBox.Show("Please enter Maximum magnitude value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMinMag.Checked && cbxMaxMag.Checked && txtMinMag.Double() >= txtMaxMag.Double())
				{
					MessageBox.Show("Minimum magnitude value must be lower than Maximum magnitude value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				int before = txtDaysFromTStart.Int();
				int after = txtDaysFromTStop.Int();

				Comet comet = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);
				double startJd = comet.Tn + before; //negativan broj
				double stopJd = comet.Tn + after;

				DateTime daysFromTStart = Utils.JDToDateTime(startJd).ToLocalTime();
				DateTime daysFromTStop = Utils.JDToDateTime(stopJd).ToLocalTime();

				DateTime start = rbRangeDate.Checked ? DateStart : daysFromTStart;
				DateTime stop = rbRangeDate.Checked ? DateEnd : daysFromTStop;

				if (stop < start)
				{
					MessageBox.Show("End date is less than start date\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				double interval = 0.0;
				double totalDays = stop.JD() - start.JD();

				if (totalDays <= 100)
					interval = totalDays / 100.0;
				else if (totalDays < 365)
					interval = 1;
				else if (totalDays < 10 * 365.25)
					interval = 2;
				else if (totalDays < 50 * 365.25)
					interval = 5;
				else if (totalDays < 100 * 365.25)
					interval = 15;
				else if (totalDays < 200 * 365.25)
					interval = 30;
				else if (totalDays < 300 * 365.25)
					interval = 40;
				else
				{
					MessageBox.Show("Timespan must be less than 300 years.\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				GraphSettings.SelectedComet = comet;
				GraphSettings.Location = FormMain.Settings.Location;

				GraphSettings.DateRange = rbRangeDate.Checked;

				GraphSettings.IsMultipleMode = rbtnMultiple.Checked;

				GraphSettings.Start = start;
				GraphSettings.Stop = stop;
				GraphSettings.Interval = interval;

				GraphSettings.DateStart = DateStart;
				GraphSettings.DateStop = DateEnd;

				GraphSettings.DaysFromTStartValue = txtDaysFromTStart.Int();
				GraphSettings.DaysFromTStopValue = txtDaysFromTStop.Int();

				GraphSettings.MagnitudeColor = pnlMagnitudeColor.BackColor;

				GraphSettings.NowLineChecked = cbxNowLine.Checked;
				GraphSettings.NowLineColor = pnlNowLineColor.BackColor;

				GraphSettings.PerihelionLineChecked = cbxPerihelionLine.Checked;
				GraphSettings.PerihelionLineColor = pnlPerihLineColor.BackColor;
				
				GraphSettings.AntialiasingChecked = cbxAntialiasing.Checked;

				GraphSettings.MinGraphMagnitudeChecked = cbxMinMag.Checked;
				GraphSettings.MinGraphMagnitudeValue = txtMinMag.TextLength > 0 ? (double?)txtMinMag.Double() : null;

				GraphSettings.MaxGraphMagnitudeChecked = cbxMaxMag.Checked;
				GraphSettings.MaxGraphMagnitudeValue = txtMaxMag.TextLength > 0 ? (double?)txtMaxMag.Double() : null;

				if (!FormMain.Settings.IgnoreLongCalculationWarning && !SettingsBase.ValidateCalculationAmount(GraphSettings))
					return;

				if (GraphSettings.Results == null)
					GraphSettings.Results = new Dictionary<Comet, List<EphemerisResult>>();

				FormMain main = this.Owner as FormMain;

				if (GraphSettings.IsMultipleMode && GraphSettings.Comets.Count > 1)
					main.SetProgressMaximumValue(GraphSettings.Comets.Count);

				cts = new CancellationTokenSource();

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(GraphSettings, FormMain.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					cts = null;
					GraphSettings.Results.Clear();
					main.HideProgress();
					return;
				}

				if (GraphSettings.AddNew && GraphSettings.SelectedComet != null)
				{
					FormGraph fg = new FormGraph(GraphSettings);
					fg.MdiParent = this.Owner;
					fg.WindowState = FormWindowState.Maximized;
					fg.LoadGraph();
					fg.Show();
				}
				else if (GraphSettings.Results != null && GraphSettings.Results.Count > 0)
				{
					FormGraph fg = this.Owner.ActiveMdiChild as FormGraph;
					fg.GraphSettings = this.GraphSettings;
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
			cbComet.DisplayMember = "full";
			cbComet.DataSource = GraphSettings.Comets;

			if (GraphSettings.Comets.Count > 0)
			{
				if (GraphSettings.SelectedComet != null && GraphSettings.Comets.Contains(GraphSettings.SelectedComet))
				{
					cbComet.SelectedIndex = GraphSettings.Comets.IndexOf(GraphSettings.SelectedComet);
				}
				else
				{
					//comet with nearest perihelion date
					Comet c = GraphSettings.Comets.OrderBy(x => Math.Abs(x.Tn - DateTime.Now.JD())).First();
					cbComet.SelectedIndex = GraphSettings.Comets.IndexOf(c);
				}
			}

			lblMultipleCount.Text = GraphSettings.Comets.Count + " comets";
		}

		#endregion
	}
}
