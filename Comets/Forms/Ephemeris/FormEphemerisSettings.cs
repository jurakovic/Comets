using Comets.Classes;
using Comets.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisSettings : Form
    {
        #region Properties

        private EphemerisSettings EphemerisSettings { get; set; }
        private bool AddNewEphemeris { get; set; }

        #endregion

        #region Constructor

        public FormEphemerisSettings(EphemerisSettings settings = null)
        {
            InitializeComponent();

            EphemerisSettings = settings;

            if (EphemerisSettings == null)
            {
                AddNewEphemeris = true;

                DateTime dt = DateTime.Now.AddDays(-20);
                txtStartYear.Text = dt.Year.ToString();
                txtStartMonth.Text = dt.Month.ToString("00");
                txtStartDay.Text = "01";
                txtStartHour.Text = "22";
                txtStartMin.Text = "00";

                dt = dt.AddMonths(1);
                txtEndYear.Text = dt.Year.ToString();
                txtEndMonth.Text = dt.Month.ToString("00");
                txtEndDay.Text = DateTime.DaysInMonth(dt.Year, dt.Month).ToString("00"); 
                txtEndHour.Text = "22";
                txtEndMin.Text = "00";

                txtIntervalDay.Text = "1";
                txtIntervalHour.Text = "00";
                txtIntervalMin.Text = "00";
            }
            else
            {
                txtStartYear.Text = EphemerisSettings.Start.Year.ToString();
                txtStartMonth.Text = EphemerisSettings.Start.Month.ToString("00");
                txtStartDay.Text = EphemerisSettings.Start.Day.ToString("00");
                txtStartHour.Text = EphemerisSettings.Start.Hour.ToString("00");
                txtStartMin.Text = "00";

                txtEndYear.Text = EphemerisSettings.Stop.Year.ToString();
                txtEndMonth.Text = EphemerisSettings.Stop.Month.ToString("00");
                txtEndDay.Text = EphemerisSettings.Stop.Day.ToString("00");
                txtEndHour.Text = EphemerisSettings.Stop.Hour.ToString("00");
                txtEndMin.Text = "00";

                int intD, intH, intM;
                intD = (int)EphemerisSettings.Interval;
                double hh = (EphemerisSettings.Interval - intD) * 24;
                intH = (int)hh;
                double min = hh - intH;
                intM = (int)Math.Round(min * 60, 0);

                txtIntervalDay.Text = intD.ToString();
                txtIntervalHour.Text = intH.ToString("00");
                txtIntervalMin.Text = intM.ToString("00");

                radioLocalTime.Checked = EphemerisSettings.LocalTime;
                radioUnivTime.Checked = !EphemerisSettings.LocalTime;
                chRA.Checked = EphemerisSettings.RA;
                chDec.Checked = EphemerisSettings.Dec;
                chEcLon.Checked = EphemerisSettings.EcLon;
                chEcLat.Checked = EphemerisSettings.EcLat;
                chHelioDist.Checked = EphemerisSettings.HelioDist;
                chGeoDist.Checked = EphemerisSettings.GeoDist;
                chAlt.Checked = EphemerisSettings.Alt;
                chAz.Checked = EphemerisSettings.Az;
                chElong.Checked = EphemerisSettings.Elongation;
                chMag.Checked = EphemerisSettings.Magnitude;
            }
        }

        #endregion

        #region Form_Load

        private void FormEphemerisSettings_Load(object sender, EventArgs e)
        {
            cbComet.DisplayMember = "full";
            cbComet.DataSource = FormMain.UserList;

            if (EphemerisSettings != null)
                if (FormMain.UserList.Contains(EphemerisSettings.Comet))
                    cbComet.Text = EphemerisSettings.Comet.full;
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

        #region btnCalcEphem_Click

        private async void btnCalcEphem_Click(object sender, EventArgs e)
        {
            if (cbComet.SelectedIndex >= 0)
            {
                DateTime start, stop;
                int syr, smo, sdy, shr, smi, eyr, emo, edy, ehr, emi;
                double ind, inm, inh;
                
                try
                {
                    syr = Convert.ToInt32(txtStartYear.Text);
                    smo = Convert.ToInt32(txtStartMonth.Text);
                    sdy = Convert.ToInt32(txtStartDay.Text);
                    shr = Convert.ToInt32(txtStartHour.Text);
                    smi = Convert.ToInt32(txtStartMin.Text);

                    eyr = Convert.ToInt32(txtEndYear.Text);
                    emo = Convert.ToInt32(txtEndMonth.Text);
                    edy = Convert.ToInt32(txtEndDay.Text);
                    ehr = Convert.ToInt32(txtEndHour.Text);
                    emi = Convert.ToInt32(txtEndMin.Text);

                    ind = Convert.ToDouble(txtIntervalDay.Text);
                    inh = Convert.ToDouble(txtIntervalHour.Text);
                    inm = Convert.ToDouble(txtIntervalMin.Text);

                    start = new DateTime(syr, smo, sdy, shr, smi, 0);
                    stop = new DateTime(eyr, emo, edy, ehr, emi, 0);

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

                if (EphemerisSettings == null)
                    EphemerisSettings = new EphemerisSettings();

                EphemerisSettings.Comet = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

                EphemerisSettings.Start = start;
                EphemerisSettings.Stop = stop;
                EphemerisSettings.Interval = ind  + (inh + (inm / 60.0)) / 24;

                EphemerisSettings.LocalTime = radioLocalTime.Checked;
                EphemerisSettings.RA = chRA.Checked;
                EphemerisSettings.Dec = chDec.Checked;
                EphemerisSettings.EcLon = chEcLon.Checked;
                EphemerisSettings.EcLat = chEcLat.Checked;
                EphemerisSettings.HelioDist = chHelioDist.Checked;
                EphemerisSettings.GeoDist = chGeoDist.Checked;
                EphemerisSettings.Alt = chAlt.Checked;
                EphemerisSettings.Az = chAz.Checked;
                EphemerisSettings.Elongation = chElong.Checked;
                EphemerisSettings.Magnitude = chMag.Checked;

                EphemerisSettings.Results = new List<EphemerisResult>();

                await EphemerisHelper.CalculateEphemeris(EphemerisSettings);

                this.Close();
            }
        }

        #endregion

        #region Form_Closing

        private void FormEphemerisSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AddNewEphemeris && EphemerisSettings != null)
            {
                FormMain.ChildCount++;

                FormMain main = this.Owner as FormMain;
                main.AddWindowItem(EphemerisSettings.ToString());

                FormEphemeris fe = new FormEphemeris(EphemerisSettings, FormMain.ChildCount);
                fe.MdiParent = main;
                fe.WindowState = FormWindowState.Maximized;
                fe.Show();
            }
            else if (EphemerisSettings != null && EphemerisSettings.Results.Any())
            {
                FormMain main = this.Owner as FormMain;
                FormEphemeris fe = main.ActiveMdiChild as FormEphemeris;

                fe.EphemerisSettings = this.EphemerisSettings;
                fe.LoadResults();
                main.RenameWindowItem((int)fe.Tag, fe.Text);
            }
        }

        #endregion
    }
}
