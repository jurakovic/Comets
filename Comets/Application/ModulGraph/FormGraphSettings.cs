using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
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

		#endregion

		#region Constructor

		public FormGraphSettings(FilterCollection filters, GraphSettings settings = null)
		{
			InitializeComponent();

			txtDaysFromTStart.Tag = new LeMiMa(4, -3653, -1);
			txtDaysFromTStop.Tag = new LeMiMa(4, 1, 3653);

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

				DateStart = GraphSettings.DateStart;
				DateEnd = GraphSettings.DateStop;

				txtDaysFromTStart.Text = GraphSettings.DaysFromTStartValue.ToString();
				txtDaysFromTStop.Text = GraphSettings.DaysFromTStopValue.ToString();

				if (GraphSettings.DateRange)
					rbRangeDate.Checked = true;
				else
					rbRangeDaysFromT.Checked = true;

				cbxPerihelionLine.Checked = GraphSettings.PerihelionLine;
				cbxNowLine.Checked = GraphSettings.NowLine;
				cbxAntialiasing.Checked = GraphSettings.Antialiasing;

				cbxMinMag.Checked = GraphSettings.MinMagnitudeChecked;
				txtMinMag.Text = GraphSettings.MinMagnitudeValue.ToString();

				cbxMaxMag.Checked = GraphSettings.MaxMagnitudeChecked;
				txtMaxMag.Text = GraphSettings.MaxMagnitudeValue.ToString();
			}
		}

		#endregion

		#region FormGraphSettings_Load

		private void FormGraphSettings_Load(object sender, EventArgs e)
		{
			if (GraphSettings == null)
			{
				GraphSettings = new GraphSettings();
				GraphSettings.Comets = FormMain.UserList.ToList();
				GraphSettings.Filters = FormMain.Filters;
				GraphSettings.SortProperty = FormMain.SortProperty;
				GraphSettings.SortAscending = FormMain.SortAscending;
				GraphSettings.AddNew = true;
			}

			BindList();
		}

		#endregion

		#region FormGraphSettings_FormClosing

		private void FormGraphSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (GraphSettings.AddNew && GraphSettings.SelectedComet != null)
			{
				FormGraph fg = new FormGraph(GraphSettings);
				fg.MdiParent = this.Owner;
				fg.WindowState = FormWindowState.Maximized;
				fg.Show();
			}
			else if (GraphSettings.Results != null && GraphSettings.Results.Any())
			{
				FormGraph fg = this.Owner.ActiveMdiChild as FormGraph;
				fg.GraphSettings = this.GraphSettings;
				fg.LoadGraph();
			}
		}

		#endregion

		#region cbComet_SelectedIndexChanged

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				Comet c = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", Utils.JDToDateTime(c.T).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
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
				}

				BindList();
			}
		}

		#endregion

		#region Date control

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
				T = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex).T;

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

		#region txtMagCommon_KeyPress

		private void txtMagCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 2, 2, -20, 40);
		}

		#endregion

		#region btnOk_Click

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				int before = txtDaysFromTStart.Int();
				int after = txtDaysFromTStop.Int();

				Comet comet = GraphSettings.Comets.ElementAt(cbComet.SelectedIndex);
				double startJd = comet.T + before; //negativan broj
				double stopJd = comet.T + after;

				DateTime daysFromTStart = Utils.JDToDateTime(startJd).ToLocalTime();
				DateTime daysFromTStop = Utils.JDToDateTime(stopJd).ToLocalTime();

				DateTime start = rbRangeDate.Checked ? DateStart : daysFromTStart;
				DateTime stop = rbRangeDate.Checked ? DateEnd : daysFromTStop;

				if (stop < start)
				{
					MessageBox.Show(
						"End date is less than start date\t\t\t",
						"Comets",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					return;
				}

				GraphSettings.SelectedComet = comet;
				GraphSettings.Location = FormMain.Settings.Location;

				double interval = 0.0;
				double totalDays = stop.JD() - start.JD();

				//double interval = totalDays / 99.5;

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
				else if (totalDays < 500 * 365.25)
					interval = 50;
				else
				{
					MessageBox.Show("Too large range...");
					return;
				}

				GraphSettings.DateRange = rbRangeDate.Checked;

				GraphSettings.Start = start;
				GraphSettings.Stop = stop;
				GraphSettings.Interval = interval;

				GraphSettings.DateStart = DateStart;
				GraphSettings.DateStop = DateEnd;

				GraphSettings.DaysFromTStartValue = txtDaysFromTStart.Int();
				GraphSettings.DaysFromTStopValue = txtDaysFromTStop.Int();

				if (rbDate.Checked)
					GraphSettings.DateFormat = GraphSettings.DateFormatEnum.Date;
				if (rbJulianDay.Checked)
					GraphSettings.DateFormat = GraphSettings.DateFormatEnum.JulianDay;
				if (rbJulianDay2.Checked)
					GraphSettings.DateFormat = GraphSettings.DateFormatEnum.JulianDay2;
				if (rbDaysFromT.Checked)
					GraphSettings.DateFormat = GraphSettings.DateFormatEnum.DaysFromT;

				GraphSettings.PerihelionLine = cbxPerihelionLine.Checked;
				GraphSettings.NowLine = cbxNowLine.Checked;
				GraphSettings.Antialiasing = cbxAntialiasing.Checked;

				GraphSettings.MinMagnitudeChecked = cbxMinMag.Checked;
				GraphSettings.MinMagnitudeValue = txtMinMag.Double();

				GraphSettings.MaxMagnitudeChecked = cbxMaxMag.Checked;
				GraphSettings.MaxMagnitudeValue = txtMaxMag.Double();

				GraphSettings.Results = new List<EphemerisResult>();

				await EphemerisManager.CalculateEphemerisAsync(GraphSettings);

				this.Close();
			}
		}

		#endregion

		#region BindList

		private void BindList()
		{
			cbComet.DisplayMember = "full";
			cbComet.DataSource = GraphSettings.Comets;

			if (GraphSettings.Comets.Any())
			{
				if (GraphSettings.SelectedComet != null && GraphSettings.Comets.Contains(GraphSettings.SelectedComet))
				{
					cbComet.SelectedIndex = GraphSettings.Comets.IndexOf(GraphSettings.SelectedComet);
				}
				else
				{
					//comet with nearest perihelion date
					Comet c = GraphSettings.Comets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First();
					cbComet.SelectedIndex = GraphSettings.Comets.IndexOf(c);
				}
			}
		}

		#endregion
	}
}
