using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cometary_Workshop
{
    public partial class FormEphemSettings : Form
    {
        public FormEphemSettings()
        {
            InitializeComponent();
        }

        private void FormEphemSettings_Load(object sender, EventArgs e)
        {

            dtPickerStartDate.Value = dtPickerStartDate.Value.AddHours(-DateTime.Now.Hour);
            dtPickerStartDate.Value = dtPickerStartDate.Value.AddMinutes(-DateTime.Now.Minute);
            dtPickerStartDate.Value = dtPickerStartDate.Value.AddHours(21);
            dtPickerStopDate.Value = dtPickerStartDate.Value;
            dtPickerStopDate.Value = dtPickerStopDate.Value.AddMonths(1);

            comboLat.SelectedIndex = 0;
            comboLon.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;

            double latSec = Convert.ToDouble(tbLatSec.Text) / 60;
            double latMin = (latSec + Convert.ToDouble(tbLatMin.Text)) / 60;
            double latDeg = Convert.ToDouble(tbLatDeg.Text) + latMin;

            double lonSec = Convert.ToDouble(tbLonSec.Text) / 60;
            double lonMin = (lonSec + Convert.ToDouble(tbLonMin.Text)) / 60;
            double lonDeg = Convert.ToDouble(tbLonDeg.Text) + lonMin;

            if (comboLat.SelectedIndex == 1) latDeg = -latDeg;
            if (comboLon.SelectedIndex == 1) lonDeg = -lonDeg;

            double timezone;
            timezone = Convert.ToDouble(tbTz.Text.Substring(1, 2));
            timezone += Convert.ToDouble(tbTz.Text.Substring(4, 2)) / 60;
            if (tbTz.Text[0] == '-') timezone = -timezone;

            bool dst = chDst.Checked;

            double h1, h2;
            h1 = dtPickerStartDate.Value.Hour + dtPickerStartDate.Value.Minute / 60;
            h2 = dtPickerStopDate.Value.Hour + dtPickerStopDate.Value.Minute / 60;

            int startHour = (int)((h1 / (double)24) * 10000);
            int stopHour = (int)((h2 / (double)24) * 10000);

            double startJD, stopJD;
            startJD = Comet.GregToJul(dtPickerStartDate.Value.Year, dtPickerStartDate.Value.Month,
                dtPickerStartDate.Value.Day, startHour);
            stopJD = Comet.GregToJul(dtPickerStopDate.Value.Year, dtPickerStopDate.Value.Month,
                dtPickerStopDate.Value.Day, stopHour);

            double interval = Convert.ToDouble(tbIntervalDay.Text) + (Convert.ToDouble(tbIntervalHour.Text) / 24) * 10000;

            Form1.obs = new Location(name, timezone, dst, latDeg, lonDeg, startJD, stopJD, interval);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeZone tz = TimeZone.CurrentTimeZone;
            TimeSpan offset = tz.GetUtcOffset(DateTime.Now);
            string off = offset.ToString();
            off = off.Substring(0, off.LastIndexOf(':'));

            if (off[0] != '-') off = "+" + off;

            chDst.Checked = tz.IsDaylightSavingTime(DateTime.Now);
            tbTz.Text = off;            
        }

        private void chDst_CheckedChanged(object sender, EventArgs e)
        {
            int newhour;
            int hour = Convert.ToInt32(tbTz.Text.Substring(1, 2));
            char sign = tbTz.Text[0];

            if (sign == '+') { newhour = chDst.Checked ? hour + 1 : hour - 1; }
            else { newhour = chDst.Checked ? hour - 1 : hour + 1; }

            if (newhour < 0) { newhour = -newhour; sign = '-'; }
            else if (newhour == 0) { sign = '+'; }

            tbTz.Text = sign + newhour.ToString("00") + tbTz.Text.Substring(3, 3);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
