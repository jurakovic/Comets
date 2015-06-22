using Comets.Classes;
using Comets.Forms.Orbit;
using Comets.Helpers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Forms.Graph
{
	public partial class FormGraphSettings : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; set; }
		public bool AddNewGraph { get; set; }

		#endregion

		#region Constructor

		public FormGraphSettings(GraphSettings settings = null)
		{
			InitializeComponent();

			txtYearStart.Tag = LeMiMa.LYear;
			txtMonthStart.Tag = LeMiMa.LMonth;
			txtDayStart.Tag = LeMiMa.LDay;

			txtYearEnd.Tag = LeMiMa.LYear;
			txtMonthEnd.Tag = LeMiMa.LMonth;
			txtDayEnd.Tag = LeMiMa.LDay;

			txtDaysFromTStart.Tag = new LeMiMa(4, -3653, -1);
			txtDaysFromTStop.Tag = new LeMiMa(4, 1, 3653);

			GraphSettings = settings;

			if (GraphSettings == null)
			{
				AddNewGraph = true;

				btnTimespanDateDefault_Click(null, null);
				btnTimespanDaysFromTDefault_Click(null, null);
			}
			else
			{
				if (GraphSettings.DateRange)
					rbRangeDate.Checked = true;
				else
					rbRangeDaysFromT.Checked = true;

				txtYearStart.Text = GraphSettings.DateStart.Year.ToString();
				txtMonthStart.Text = GraphSettings.DateStart.Month.ToString();
				txtDayStart.Text = GraphSettings.DateStart.Day.ToString();

				txtYearEnd.Text = GraphSettings.DateStop.Year.ToString();
				txtMonthEnd.Text = GraphSettings.DateStop.Month.ToString();
				txtDayEnd.Text = GraphSettings.DateStop.Day.ToString();

				txtDaysFromTStart.Text = GraphSettings.DaysFromTStartValue.ToString();
				txtDaysFromTStop.Text = GraphSettings.DaysFromTStopValue.ToString();

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

		#region Form_Load

		private void FormMagnitudeSettings_Load(object sender, EventArgs e)
		{
			cbComet.DisplayMember = "full";
			cbComet.DataSource = FormMain.UserList;

			if (FormMain.UserList.Count == FormMain.MainList.Count)
			{
				//select comet with nearest perihelion date
				Comet c = FormMain.UserList.Where(x => x.T - EphemerisHelper.jd(DateTime.Now) > 0).OrderBy(y => y.T).First();
				cbComet.SelectedIndex = FormMain.UserList.IndexOf(c);
			}

			if (GraphSettings != null)
				if (FormMain.UserList.Contains(GraphSettings.Comet))
					cbComet.Text = GraphSettings.Comet.full;
		}

		#endregion

		#region cbComet_SelectedIndexChanged

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				Comet c = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = "Perihelion date:                " + c.Ty.ToString() + " " + Comet.Month[c.Tm - 1] + " " + c.Td.ToString("00") + "." + c.Th.ToString("0000");
				lblPerihDist.Text = "Perihelion distance:          " + c.q.ToString("0.000000") + " AU";
				lblPeriod.Text = c.P < 10000 ? "Period:                              " + c.P.ToString("0.000000") + " years" : "Period:                              -";
			}
		}

		#endregion

		#region Date control

		private void txtCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtYearMonthStart_TextChanged(object sender, EventArgs e)
		{
			if (txtMonthStart.Text.Length > 0 && txtYearStart.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(Convert.ToInt32(txtYearStart.Text), Convert.ToInt32(txtMonthStart.Text));

				LeMiMa o = txtDayStart.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				txtDayStart.Tag = n;

				if (txtDayStart.Text.Length > 0 && Convert.ToInt32(txtDayStart.Text) > n.Max)
					txtDayStart.Text = n.Max.ToString();
			}
		}

		private void txtYearMonthEnd_TextChanged(object sender, EventArgs e)
		{
			if (txtMonthEnd.Text.Length > 0 && txtYearEnd.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(Convert.ToInt32(txtYearEnd.Text), Convert.ToInt32(txtMonthEnd.Text));

				LeMiMa o = txtDayEnd.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				txtDayEnd.Tag = n;

				if (txtDayEnd.Text.Length > 0 && Convert.ToInt32(txtDayEnd.Text) > n.Max)
					txtDayEnd.Text = n.Max.ToString();
			}
		}

		#endregion

		#region txtMagCommon_KeyPress

		private void txtMagCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 2, 2, -20, 40);
		}

		#endregion

		#region btnTimespanDefault

		private void btnTimespanDateDefault_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20);
			txtYearStart.Text = dt.Year.ToString();
			txtMonthStart.Text = dt.Month.ToString();
			txtDayStart.Text = "1";

			dt = dt.AddMonths(1);
			txtYearEnd.Text = dt.Year.ToString();
			txtMonthEnd.Text = dt.Month.ToString();
			txtDayEnd.Text = DateTime.DaysInMonth(dt.Year, dt.Month).ToString();
		}

		private void btnTimespanDaysFromTDefault_Click(object sender, EventArgs e)
		{
			int offset = 180;
			txtDaysFromTStart.Text = (-offset).ToString();
			txtDaysFromTStop.Text = offset.ToString();
		}

		#endregion

		#region btnPlotGraph_Click

		private async void btnPlotGraph_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				ATime start, stop, dateStart, dateStop, daysFromTStart, daysFromTStop;
				Comet comet;

				try
				{
					int syr = Convert.ToInt32(txtYearStart.Text);
					int smo = Convert.ToInt32(txtMonthStart.Text);
					int sdy = Convert.ToInt32(txtDayStart.Text);
							  
					int eyr = Convert.ToInt32(txtYearEnd.Text);
					int emo = Convert.ToInt32(txtMonthEnd.Text);
					int edy = Convert.ToInt32(txtDayEnd.Text);

					dateStart = new ATime(syr, smo, sdy, 0, 0, 0, FormMain.Settings.Location.Timezone);
					dateStop = new ATime(eyr, emo, edy, 0, 0, 0, FormMain.Settings.Location.Timezone);

					comet = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

					int before = Convert.ToInt32(txtDaysFromTStart.Text);
					int after = Convert.ToInt32(txtDaysFromTStop.Text);

					double startJd = comet.T + before; //negativan broj
					double stopJd = comet.T + after;

					daysFromTStart = new ATime(startJd, FormMain.Settings.Location.Timezone);
					daysFromTStop = new ATime(stopJd, FormMain.Settings.Location.Timezone);

					start = rbRangeDate.Checked ? dateStart : daysFromTStart;
					stop = rbRangeDate.Checked ? dateStop : daysFromTStop;

					if (stop < start)
					{
						MessageBox.Show("End date is less than start date\t");
						return;
					}
				}
				catch
				{
					MessageBox.Show("Invalid date\t\t\t");
					return;
				}

				if (GraphSettings == null)
					GraphSettings = new GraphSettings();

				GraphSettings.Comet = comet;

				double interval = 0.0;
				double totalDays = stop.JD - start.JD;

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

				GraphSettings.DateStart = dateStart;
				GraphSettings.DateStop = dateStop;

				GraphSettings.DaysFromTStartValue = Convert.ToInt32(txtDaysFromTStart.Text);
				GraphSettings.DaysFromTStopValue = Convert.ToInt32(txtDaysFromTStop.Text);

				if (rbDate.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.Date;
				if (rbJulianDay.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay;
				if (rbJulianDay2.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay2;
				if (rbDaysFromT.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.DaysFromT;

				GraphSettings.PerihelionLine = cbxPerihelionLine.Checked;
				GraphSettings.NowLine = cbxNowLine.Checked;
				GraphSettings.Antialiasing = cbxAntialiasing.Checked;

				GraphSettings.MinMagnitudeChecked = cbxMinMag.Checked;
				GraphSettings.MinMagnitudeValue = Convert.ToDouble(txtMinMag.Text.Trim());

				GraphSettings.MaxMagnitudeChecked = cbxMaxMag.Checked;
				GraphSettings.MaxMagnitudeValue = Convert.ToDouble(txtMaxMag.Text.Trim());

				GraphSettings.Results = new List<EphemerisResult>();

				await EphemerisHelper.CalculateEphemeris(GraphSettings);

				this.Close();
			}
		}

		#endregion

		#region Form_Closing

		private void FormMagnitudeSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (AddNewGraph && GraphSettings != null)
			{
				FormMain.ChildCount++;

				FormMain main = this.Owner as FormMain;
				main.AddWindowItem(GraphSettings.ToString());

				FormGraph fg = new FormGraph(GraphSettings, FormMain.ChildCount);
				fg.MdiParent = main;
				fg.WindowState = FormWindowState.Maximized;
				fg.Show();
			}
			else if (GraphSettings != null && GraphSettings.Results.Any())
			{
				FormMain main = this.Owner as FormMain;
				FormGraph fg = main.ActiveMdiChild as FormGraph;

				fg.GraphSettings = this.GraphSettings;
				fg.LoadGraph();
				main.RenameWindowItem((int)fg.Tag, fg.Text);
			}
		}

		#endregion
	}
}
