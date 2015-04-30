using Comets.Classes;
using Comets.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisSettings : Form
    {
        private string EphemerisResult { get; set; }

        public FormEphemerisSettings()
        {
            InitializeComponent();

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

        private void FormEphemerisSettings_Load(object sender, EventArgs e)
        {
            EphemerisResult = null;

            cbComet.DisplayMember = "full";
            cbComet.DataSource = FormMain.userList;
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

        private async void btnCalcEphem_Click(object sender, EventArgs e)
        {
            if (cbComet.SelectedIndex >= 0)
            {
                //local start, stop
                DateTime locStart;
                DateTime locStop;

                try
                {
                    locStart = new DateTime(Convert.ToInt32(tbStartYear.Text),
                                            Convert.ToInt32(tbStartMonth.Text),
                                            Convert.ToInt32(tbStartDay.Text),
                                            Convert.ToInt32(tbStartHour.Text),
                                            Convert.ToInt32(tbStartMin.Text), 0);

                    locStop = new DateTime(Convert.ToInt32(tbEndYear.Text),
                                            Convert.ToInt32(tbEndMonth.Text),
                                            Convert.ToInt32(tbEndDay.Text),
                                            Convert.ToInt32(tbEndHour.Text),
                                            Convert.ToInt32(tbEndMin.Text), 0);
                }
                catch
                {
                    MessageBox.Show("Invalid date");
                    return;
                }

                DateTime utcStart = locStart.AddHours(-FormMain.Settings.Timezone);
                DateTime utcStop = locStop.AddHours(-FormMain.Settings.Timezone);

                if (FormMain.Settings.DST)
                {
                    utcStart = utcStart.AddHours(-1);
                    utcStop = utcStop.AddHours(-1);
                }

                double jday = EphemHelper.jd(utcStart.Year, utcStart.Month, utcStart.Day, utcStart.Hour, utcStart.Minute, utcStart.Second);
                double jdmax = EphemHelper.jd(utcStop.Year, utcStop.Month, utcStop.Day, utcStop.Hour, utcStop.Minute, utcStop.Second);
                double locjday = EphemHelper.jd(locStart.Year, locStart.Month, locStart.Day, locStart.Hour, locStart.Minute, locStart.Second);

                double interval = Convert.ToDouble(tbIntervalDay.Text) + (Convert.ToDouble(tbIntervalHour.Text) + (Convert.ToDouble(tbIntervalMin.Text) / 60.0)) / 24;

                StringBuilder sb = new StringBuilder();

                sb.Append(radioLocalTime.Checked ? "     Local Time  " : " Universal Time  ");
                if (chRA.Checked) sb.Append("   R.A.   ");
                if (chDec.Checked) sb.Append("   Dec   ");
                if (chAlt.Checked) sb.Append("   Alt  ");
                if (chAz.Checked) sb.Append("   Az   ");
                if (chEcLon.Checked) sb.Append(" Ecl.Lon ");
                if (chEcLat.Checked) sb.Append(" Ecl.Lat ");
                if (chElong.Checked) sb.Append("   Elong. ");
                if (chHelioDist.Checked) sb.Append("    r    ");
                if (chGeoDist.Checked) sb.Append("    d    ");
                if (chMag.Checked) sb.AppendLine(" Mag.");

                Comet c = FormMain.userList.ElementAt(cbComet.SelectedIndex);

                DateTime begin = DateTime.Now;

                await Task.Run(() =>
                {
                    StringBuilder line = new StringBuilder();

                    while (jday <= jdmax)
                    {
                        double[] dat = EphemHelper.CometAlt(c, jday, FormMain.Settings);
                        double alt = dat[0];
                        double az = dat[1];
                        //double ha = dat[2];
                        double ra = dat[3];
                        double dec = dat[4] - (dat[4] > 180.0 ? 360 : 0);
                        double eclon = EphemHelper.rev(dat[5]);
                        double eclat = dat[6];
                        double ill = dat[7];
                        double r = dat[8];
                        double dist = dat[9];
                        double mag = dat[10];

                        double[] sundat = EphemHelper.SunAlt(jday, FormMain.Settings);
                        double sunra = sundat[3];
                        double sundec = sundat[4] - (sundat[4] > 180.0 ? 360 : 0);

                        double[] sep = EphemHelper.separation(ra, sunra, dec, sundec);
                        double elong = sep[0];
                        double pa = sep[1];

                        line.Clear();

                        line.Append(radioLocalTime.Checked ? EphemHelper.dateString(locjday) : EphemHelper.dateString(jday));
                        if (chRA.Checked) line.Append("  " + EphemHelper.hmsstring(ra / 15.0));
                        if (chDec.Checked) line.Append("  " + EphemHelper.anglestring(dec, false, true));
                        if (chAlt.Checked) line.Append("  " + EphemHelper.fixnum(alt, 5, 1) + "°");
                        if (chAz.Checked) line.Append(" " + EphemHelper.fixnum(az, 6, 1) + "°");
                        if (chEcLon.Checked) line.Append("  " + EphemHelper.anglestring(eclon, true, true));
                        if (chEcLat.Checked) line.Append("  " + EphemHelper.anglestring(eclat, false, true));
                        if (chElong.Checked) line.Append(" " + EphemHelper.fixnum(elong, 6, 1) + "°" + (pa >= 180 ? " W" : " E"));
                        if (chHelioDist.Checked) line.Append(" " + EphemHelper.fixnum(r, 8, 4));
                        if (chGeoDist.Checked) line.Append(" " + EphemHelper.fixnum(dist, 8, 4));
                        if (chMag.Checked) line.Append(" " + EphemHelper.fixnum(mag, 4, 1));

                        sb.AppendLine(line.ToString());

                        jday += interval;
                        locjday += interval;
                    }
                });

                EphemerisResult = sb.ToString();

                this.Close();
            }
        }

        private void FormEphemerisSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.EphemerisResult = this.EphemerisResult;
        }
    }
}
