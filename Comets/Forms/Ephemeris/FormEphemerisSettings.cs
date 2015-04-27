using Comets.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisSettings : Form
    {
        public FormEphemerisSettings()
        {
            InitializeComponent();
        }

        private void FormEphemerisSettings_Load(object sender, EventArgs e)
        {
            cbComet.DisplayMember = "full";
            cbComet.DataSource = FormMain.userList;

            DateTime dt = DateTime.Now.AddHours(1);
            tbStartYear.Text = dt.Year.ToString();
            tbStartMonth.Text = dt.Month.ToString("00");
            tbStartDay.Text = dt.Day.ToString("00");
            tbStartHour.Text = dt.Hour.ToString("00");
            tbStartMin.Text = "00";

            dt = dt.AddDays(15);
            tbEndYear.Text = dt.Year.ToString();
            tbEndMonth.Text = dt.Month.ToString("00");
            tbEndDay.Text = dt.Day.ToString("00");
            tbEndHour.Text = dt.Hour.ToString("00");
            tbEndMin.Text = "00";

            tbIntervalDay.Text = "1";
            tbIntervalHour.Text = "00";
            tbIntervalMin.Text = "00";
        }

        private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComet.SelectedIndex >= 0)
            {
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                Comet c = FormMain.userList.ElementAt(cbComet.SelectedIndex);

                lblPerihDate.Text = "Perihelion date:                " + c.Ty.ToString() + " " + month[c.Tm - 1] + " " + c.Td.ToString("00") + "." + c.Th.ToString("0000");
                lblPerihDist.Text = "Perihelion distance:          " + c.q.ToString("0.000000") + " AU";
                lblPeriod.Text = (c.P < 10000 && c.e < 0.98) ? "Period:                              " + c.P.ToString("0.000000") + " years" : "Period:                              -";
            } 
        }

        private void btnCalcEphem_Click(object sender, EventArgs e)
        {
            // TO DO

            //FormMain.EphemerisResult = sb.ToString();
            this.Close();
        }
    }
}
