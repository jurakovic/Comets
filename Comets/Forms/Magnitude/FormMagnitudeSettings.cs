using Comets.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using Comets.Helpers;
using System.Threading.Tasks;

namespace Comets.Forms.Magnitude
{
    public partial class FormMagnitudeSettings : Form
    {
        #region Properties

        public GraphSettings GraphSettings { get; set; }
        public bool AddNewGraph { get; set; }

        #endregion

        #region Constructor

        public FormMagnitudeSettings(GraphSettings settings = null)
        {
            InitializeComponent();

            GraphSettings = settings;

            if (GraphSettings == null)
            {
                AddNewGraph = true;

                DateTime dt = DateTime.Now.AddDays(-20);
                txtStartYear.Text = dt.Year.ToString();
                txtStartMonth.Text = dt.Month.ToString("00");
                txtStartDay.Text = "01";

                dt = dt.AddMonths(1);
                txtEndYear.Text = dt.Year.ToString();
                txtEndMonth.Text = dt.Month.ToString("00");
                txtEndDay.Text = DateTime.DaysInMonth(dt.Year, dt.Month).ToString("00");

                int offset = 180;

                txtStartDaysFromT.Text = (-offset).ToString();
                txtEndDaysFromT.Text = offset.ToString();
            }
            else
            {
                if (GraphSettings.DateRange)
                    rbRangeDate.Checked = true;
                else
                    rbRangeDaysFromT.Checked = true;

                string[] dates = GraphSettings.Dates.Split(':');
                string[] ds = dates[0].Split('.');
                string[] de = dates[1].Split('.');

                txtStartYear.Text = ds[0];
                txtStartMonth.Text = ds[1];
                txtStartDay.Text = ds[2];

                txtEndYear.Text = de[0];
                txtEndMonth.Text = de[1];
                txtEndDay.Text = de[2];

                string[] daysfromt = GraphSettings.DaysFromT.Split(':');
                txtStartDaysFromT.Text = daysfromt[0];
                txtEndDaysFromT.Text = daysfromt[1];
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
                lblPeriod.Text = (c.P < 10000 && c.e < 0.98) ? "Period:                              " + c.P.ToString("0.000000") + " years" : "Period:                              -";
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

        #region btnPlotGraph_Click

        private async void btnPlotGraph_Click(object sender, EventArgs e)
        {
            if (cbComet.SelectedIndex >= 0)
            {
                DateTime start, stop;
                int syr, smo, sdy, eyr, emo, edy, before, after;

                try
                {
                    if (rbRangeDate.Checked)
                    {
                        syr = Convert.ToInt32(txtStartYear.Text);
                        smo = Convert.ToInt32(txtStartMonth.Text);
                        sdy = Convert.ToInt32(txtStartDay.Text);

                        eyr = Convert.ToInt32(txtEndYear.Text);
                        emo = Convert.ToInt32(txtEndMonth.Text);
                        edy = Convert.ToInt32(txtEndDay.Text);

                        start = new DateTime(syr, smo, sdy, 0, 0, 0);
                        stop = new DateTime(eyr, emo, edy, 0, 0, 0);
                    }
                    else
                    {
                        Comet c = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

                        before = Convert.ToInt32(txtStartDaysFromT.Text);
                        after = Convert.ToInt32(txtEndDaysFromT.Text);

                        double jdStart = c.T + before; //negativan broj
                        double jdStop = c.T + after;

                        int[] startDate = EphemerisHelper.jdtocd(jdStart);
                        syr = startDate[0];
                        smo = startDate[1];
                        sdy = startDate[2];

                        int[] stopDate = EphemerisHelper.jdtocd(jdStop);
                        eyr = stopDate[0];
                        emo = stopDate[1];
                        edy = stopDate[2];

                        start = new DateTime(syr, smo, sdy, 0, 0, 0);
                        stop = new DateTime(eyr, emo, edy, 0, 0, 0);
                    }

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

                if (GraphSettings == null)
                    GraphSettings = new GraphSettings();

                GraphSettings.Comet = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

                GraphSettings.Start = start;
                GraphSettings.Stop = stop;

                int interval = 0;
                double totalDays = (stop - start).TotalDays;

                if ((stop - start).TotalDays < 365)
                    interval = 1;
                else if ((stop - start).TotalDays < 10 * 365)
                    interval = 2;
                else if ((stop - start).TotalDays < 50 * 365)
                    interval = 3;
                else if ((stop - start).TotalDays < 100 * 365)
                    interval = 10;
                else if ((stop - start).TotalDays < 500 * 365)
                    interval = 15;
                else
                {
                    MessageBox.Show("Too large range...");
                    return;
                }

                double test = totalDays / 99.5;
                //GraphSettings.Interval = totalDays / 99.5; //da podijelimo vrijeme na 100 jednakih dijelova

                GraphSettings.Interval = interval;

                GraphSettings.DateRange = rbRangeDate.Checked;

                GraphSettings.Dates =
                    txtStartYear.Text + "." + txtStartMonth.Text + "." + txtStartDay.Text + ":" +
                    txtEndYear.Text + "." + txtEndMonth.Text + "." + txtEndDay.Text;

                GraphSettings.DaysFromT = txtStartDaysFromT.Text + ":" + txtEndDaysFromT.Text;

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

                FormMagnitudeGraph fmg = new FormMagnitudeGraph(GraphSettings, FormMain.ChildCount);
                fmg.MdiParent = main;
                fmg.WindowState = FormWindowState.Maximized;
                fmg.Show();
            }
            else if (GraphSettings != null && GraphSettings.Results.Any())
            {
                FormMain main = this.Owner as FormMain;
                FormMagnitudeGraph fmg = main.ActiveMdiChild as FormMagnitudeGraph;

                fmg.GraphSettings = this.GraphSettings;
                fmg.LoadGraph();
                main.RenameWindowItem((int)fmg.Tag, fmg.Text);
            }
        }

        #endregion
    }
}
