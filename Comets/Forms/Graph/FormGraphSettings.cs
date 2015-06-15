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

			domMonthStart.Items.AddRange(FormOrbitViewer.MonthDomainUpDownItems);
			domMonthEnd.Items.AddRange(FormOrbitViewer.MonthDomainUpDownItems);

			GraphSettings = settings;

			if (GraphSettings == null)
			{
				AddNewGraph = true;

				DateTime dt = DateTime.Now.AddDays(-20);
				numYearStart.Value = dt.Year;
				domMonthStart.SelectedIndex = 13 - dt.Month;
				numDayStart.Value = 1;

				dt = dt.AddMonths(1);
				numYearEnd.Value = dt.Year;
				domMonthEnd.SelectedIndex = 13 - dt.Month;
				numDayEnd.Value = DateTime.DaysInMonth(dt.Year, dt.Month);

				int offset = 180;

				numDaysFromTStart.Value = -offset;
				numDaysFromTStop.Value = offset;
			}
			else
			{
				if (GraphSettings.DateRange)
					rbRangeDate.Checked = true;
				else
					rbRangeDaysFromT.Checked = true;

				numYearStart.Value = GraphSettings.DateStart.Year;
				domMonthStart.SelectedIndex = 13 - GraphSettings.DateStart.Month;
				numDayStart.Value = GraphSettings.DateStart.Day;

				numYearEnd.Value = GraphSettings.DateStop.Year;
				domMonthEnd.SelectedIndex = 13 - GraphSettings.DateStop.Month;
				numDayEnd.Value = GraphSettings.DateStop.Day;

				numDaysFromTStart.Value = GraphSettings.DaysFromTStartValue;
				numDaysFromTStop.Value = GraphSettings.DaysFromTStopValue;
			}
		}

		#endregion

		#region Form_Load

		private void FormMagnitudeSettings_Load(object sender, EventArgs e)
		{
			cbComet.DisplayMember = "full";
			cbComet.DataSource = FormMain.UserList;

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

		#region RadioButton_CheckedChanged

		private void rbRangeDate_CheckedChanged(object sender, EventArgs e)
		{
			pnlRangeDate.Enabled = rbRangeDate.Checked;
		}

		private void rbRangeDaysFromT_CheckedChanged(object sender, EventArgs e)
		{
			pnlRangeDaysFromT.Enabled = rbRangeDaysFromT.Checked;
		}

		#endregion

		#region Date control

		private void timespanStartCommon_ValueChanged(object sender, EventArgs e)
		{
			bool mChanged = (sender as Control).Name == domMonthStart.Name;
			bool yChanged = (sender as Control).Name == numYearStart.Name;

			int y = (int)numYearStart.Value;
			int m = 13 - domMonthStart.SelectedIndex;
			int d = (int)numDayStart.Value;
			int dmax = (int)numDayStart.Maximum;

			int[] newDate = Utils.ControlDateTime(y, m, d, dmax, 0, 0, mChanged, yChanged);

			if (newDate[6] == 1)
			{
				numDayStart.Maximum = newDate[3] + 1;

				numDayStart.Value = newDate[2];
				domMonthStart.SelectedIndex = 13 - newDate[1];
				numYearStart.Value = newDate[0];
			}
		}

		private void timespanEndCommon_ValueChanged(object sender, EventArgs e)
		{
			bool mChanged = (sender as Control).Name == domMonthEnd.Name;
			bool yChanged = (sender as Control).Name == numYearEnd.Name;

			int y = (int)numYearEnd.Value;
			int m = 13 - domMonthEnd.SelectedIndex;
			int d = (int)numDayEnd.Value;
			int dmax = (int)numDayEnd.Maximum;

			int[] newDate = Utils.ControlDateTime(y, m, d, dmax, 0, 0, mChanged, yChanged);

			if (newDate[6] == 1)
			{
				numDayEnd.Maximum = newDate[3] + 1;

				numDayEnd.Value = newDate[2];
				domMonthEnd.SelectedIndex = 13 - newDate[1];
				numYearEnd.Value = newDate[0];
			}
		}

		#endregion

		#region btnPlotGraph_Click

		private async void btnPlotGraph_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				ATime start, stop, dateStart, dateStop, daysFromTStart, daysFromTStop;
				Comet comet;

				if (GraphSettings == null)
					GraphSettings = new GraphSettings();

				try
				{
					int syr = (int)numYearStart.Value;
					int smo = 13 - domMonthStart.SelectedIndex;
					int sdy = (int)numDayStart.Value;

					int eyr = (int)numYearEnd.Value;
					int emo = 13 - domMonthEnd.SelectedIndex;
					int edy = (int)numDayEnd.Value;

					dateStart = new ATime(syr, smo, sdy, 0, 0, 0, GraphSettings.Location.Timezone);
					dateStop = new ATime(eyr, emo, edy, 0, 0, 0, GraphSettings.Location.Timezone);

					comet = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

					int before = (int)numDaysFromTStart.Value;
					int after = (int)numDaysFromTStop.Value;

					double startJd = comet.T + before; //negativan broj
					double stopJd = comet.T + after;

					daysFromTStart = new ATime(startJd, GraphSettings.Location.Timezone);
					daysFromTStop = new ATime(stopJd, GraphSettings.Location.Timezone);

					start = rbRangeDate.Checked ? dateStart : daysFromTStart;
					stop = rbRangeDate.Checked ? dateStop : daysFromTStop;

					if (stop < start)
					{
						MessageBox.Show("End date is less than start date");
						return;
					}
				}
				catch
				{
					MessageBox.Show("Invalid date");
					return;
				}

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

				GraphSettings.DaysFromTStartValue = (int)numDaysFromTStart.Value;
				GraphSettings.DaysFromTStopValue = (int)numDaysFromTStop.Value;

				if (rbDate.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.Date;
				if (rbJulianDay.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay;
				if (rbJulianDay2.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay2;
				if (rbDaysFromT.Checked)
					GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.DaysFromT;

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
