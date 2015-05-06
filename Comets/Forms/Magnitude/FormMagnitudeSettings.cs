using Comets.Classes;
using System;
using System.Windows.Forms;

namespace Comets.Forms.Magnitude
{
    public partial class FormMagnitudeSettings : Form
    {
        #region Properties

        public GraphSettings GraphSettings { get; set; }

        #endregion

        #region Constructor

        public FormMagnitudeSettings(GraphSettings graphSettings =  null)
        {
            InitializeComponent();
        }

        #endregion

        #region Form_Load

        private void FormMagnitudeSettings_Load(object sender, EventArgs e)
        {
            int offset = 90;

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

        private void btnPlotGraph_Click(object sender, EventArgs e)
        {
            // TO DO

            this.Close();
        }

        #endregion

        #region Form_Closing

        private void FormMagnitudeSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.ChildCount++;

            FormMain main = this.Owner as FormMain;
            main.AddWindowItem("bla bla");

            FormMagnitudeGraph fmg = new FormMagnitudeGraph(GraphSettings, FormMain.ChildCount);
            fmg.MdiParent = main;
            fmg.WindowState = FormWindowState.Maximized;
            fmg.Show();
        }

        #endregion
    }
}
