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

                int offset = 45;

                DateTime dt = DateTime.Now.AddDays(-offset);
                tbStartYear.Text = dt.Year.ToString();
                tbStartMonth.Text = dt.Month.ToString("00");
                tbStartDay.Text = dt.Day.ToString("00");

                dt = DateTime.Now.AddDays(offset);
                tbEndYear.Text = dt.Year.ToString();
                tbEndMonth.Text = dt.Month.ToString("00");
                tbEndDay.Text = dt.Day.ToString("00");

                tbStartDaysFromT.Text = (-offset).ToString();
                tbEndDaysFromT.Text = offset.ToString();

                //TO DO
            }
            else
            {
                //TO DO
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
                        syr = Convert.ToInt32(tbStartYear.Text);
                        smo = Convert.ToInt32(tbStartMonth.Text);
                        sdy = Convert.ToInt32(tbStartDay.Text);

                        eyr = Convert.ToInt32(tbEndYear.Text);
                        emo = Convert.ToInt32(tbEndMonth.Text);
                        edy = Convert.ToInt32(tbEndDay.Text);

                        start = new DateTime(syr, smo, sdy, 0, 0, 0);
                        stop = new DateTime(eyr, emo, edy, 0, 0, 0);
                    }
                    else
                    {
                        Comet c = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

                        before = Convert.ToInt32(tbStartDaysFromT.Text);
                        after = Convert.ToInt32(tbEndDaysFromT.Text);

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
                GraphSettings.Interval = (stop - start).TotalDays / 99.5; //da podijelimo vrijeme na 100 jednakih dijelova

                if (rbDate.Checked)
                    GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.Date;
                if (rbJulianDay.Checked)
                    GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay;
                if (rbJulianDay2.Checked)
                    GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.JulianDay2;
                if (rbDaysFromT.Checked)
                    GraphSettings.DateFormat = Classes.GraphSettings.DateFormatEnum.DaysFromT;

                GraphSettings.Results = new List<EphemerisResult>();

                await Task.Run(() => EphemerisHelper.CalculateEphemeris(GraphSettings));

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
                main.AddWindowItem("bla bla");

                FormMagnitudeGraph fmg = new FormMagnitudeGraph(GraphSettings, FormMain.ChildCount);
                fmg.MdiParent = main;
                fmg.WindowState = FormWindowState.Maximized;
                fmg.Show();
            }
            else if (GraphSettings != null && GraphSettings.Results.Any())
            {
                //TO DO
            }
        }

        #endregion
    }
}
