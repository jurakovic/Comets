using Comets.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using Comets.Helpers;
using System.Threading.Tasks;

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

                string[] tMin = GraphSettings.MinText.Split('.');
                string[] tMax = GraphSettings.MaxText.Split('.');
                string[] tFromT = GraphSettings.DaysFromTText.Split(':');

                txtStartYear.Text = tMin[0];
                txtStartMonth.Text = tMin[1];
                txtStartDay.Text = tMin[2];

                txtEndYear.Text = tMax[0];
                txtEndMonth.Text = tMax[1];
                txtEndDay.Text = tMax[2];

                txtStartDaysFromT.Text = tFromT[0];
                txtEndDaysFromT.Text = tFromT[1];
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
                DateTime min, max;
                int minYr, minMo, minDy, maxYr, maxMo, maxDy, before, after;

                try
                {
                    if (rbRangeDate.Checked)
                    {
                        minYr = Convert.ToInt32(txtStartYear.Text);
                        minMo = Convert.ToInt32(txtStartMonth.Text);
                        minDy = Convert.ToInt32(txtStartDay.Text);

                        maxYr = Convert.ToInt32(txtEndYear.Text);
                        maxMo = Convert.ToInt32(txtEndMonth.Text);
                        maxDy = Convert.ToInt32(txtEndDay.Text);

                        min = new DateTime(minYr, minMo, minDy, 0, 0, 0);
                        max = new DateTime(maxYr, maxMo, maxDy, 0, 0, 0);
                    }
                    else
                    {
                        Comet c = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

                        before = Convert.ToInt32(txtStartDaysFromT.Text);
                        after = Convert.ToInt32(txtEndDaysFromT.Text);

                        double jdMin = c.T + before; //negativan broj
                        double jdMax = c.T + after;

                        int[] minDate = EphemerisHelper.jdtocd(jdMin);
                        minYr = minDate[0];
                        minMo = minDate[1];
                        minDy = minDate[2];

                        int[] maxDate = EphemerisHelper.jdtocd(jdMax);
                        maxYr = maxDate[0];
                        maxMo = maxDate[1];
                        maxDy = maxDate[2];

                        min = new DateTime(minYr, minMo, minDy, 0, 0, 0);
                        max = new DateTime(maxYr, maxMo, maxDy, 0, 0, 0);
                    }

                    if (max < min)
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

                GraphSettings.MinLocalJD = EphemerisHelper.jd(min);
                GraphSettings.MaxLocalJD = EphemerisHelper.jd(max);

                min = min.AddHours(-GraphSettings.Location.Timezone);
                max = max.AddHours(-GraphSettings.Location.Timezone);

                if (GraphSettings.Location.DST)
                {
                    min = min.AddHours(-1);
                    max = max.AddHours(-1);
                }

                GraphSettings.MinUtcJD = EphemerisHelper.jd(min);
                GraphSettings.MaxUtcJD = EphemerisHelper.jd(max);

                int interval = 0;
                double totalDays = (max - min).TotalDays;

                //double interval = totalDays / 99.5;

                if (totalDays < 365)
                    interval = 1;
                else if (totalDays < 10 * 365)
                    interval = 2;
                else if (totalDays < 50 * 365)
                    interval = 5;
                else if (totalDays < 100 * 365)
                    interval = 15;
                else if (totalDays < 500 * 365)
                    interval = 50;
                else
                {
                    MessageBox.Show("Too large range...");
                    return;
                }

                GraphSettings.Interval = interval;

                GraphSettings.DateRange = rbRangeDate.Checked;

                GraphSettings.MinText = txtStartYear.Text + "." + txtStartMonth.Text + "." + txtStartDay.Text;
                GraphSettings.MaxText = txtEndYear.Text + "." + txtEndMonth.Text + "." + txtEndDay.Text;
                GraphSettings.DaysFromTText = txtStartDaysFromT.Text + ":" + txtEndDaysFromT.Text;

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
