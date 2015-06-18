using Comets.Classes;
using Comets.Forms.Orbit;
using Comets.Helpers;
using Comets.OrbitViewer;
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
				numYearStart.Value = dt.Year;
				numMonthStart.Value = dt.Month;
				numDayStart.Value = 1;
				numHourStart.Value = 22;
				numMinStart.Value = 0;

				dt = dt.AddMonths(1);
				numYearEnd.Value = dt.Year;
				numMonthEnd.Value = dt.Month;
				numDayEnd.Value = DateTime.DaysInMonth(dt.Year, dt.Month);
				numHourEnd.Value = 22;
				numMinEnd.Value = 0;

				numDayInterval.Value = 1;
				numHourInterval.Value = 0;
				numMinInterval.Value = 0;
			}
			else
			{
				ATime dt = EphemerisSettings.Start;
				numYearStart.Value = dt.Year;
				numMonthStart.Value = dt.Month;
				numDayStart.Value = dt.Day;
				numHourStart.Value = dt.Hour;
				numMinStart.Value = dt.Minute;

				dt = EphemerisSettings.Stop;
				numYearEnd.Value = dt.Year;
				numMonthEnd.Value = dt.Month;
				numDayEnd.Value = dt.Day;
				numHourEnd.Value = dt.Hour;
				numMinEnd.Value = dt.Minute;

				numDayInterval.Value = EphemerisSettings.TimeSpan.Day;
				numHourInterval.Value = EphemerisSettings.TimeSpan.Hour;
				numMinInterval.Value = EphemerisSettings.TimeSpan.Minute;

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
				lblPeriod.Text = c.P < 10000 ? "Period:                              " + c.P.ToString("0.000000") + " years" : "Period:                              -";
			}
		}

		#endregion

		#region btnCalcEphem_Click

		private async void btnCalcEphem_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				ATime start, stop;
				double interval;
				double ind, inm, inh;

				try
				{
					int syr = (int)numYearStart.Value;
					int smo = (int)numMonthStart.Value;
					int sdy = (int)numDayStart.Value;
					int shr = (int)numHourStart.Value;
					int smi = (int)numMinStart.Value;

					int eyr = (int)numYearEnd.Value;
					int emo = (int)numMonthEnd.Value;
					int edy = (int)numDayEnd.Value;
					int ehr = (int)numHourEnd.Value;
					int emi = (int)numMinEnd.Value;

					ind = (int)numDayInterval.Value;
					inh = (int)numHourInterval.Value;
					inm = (int)numMinInterval.Value;

					start = new ATime(syr, smo, sdy, shr, smi, 0, FormMain.Settings.Location.Timezone);
					stop = new ATime(eyr, emo, edy, ehr, emi, 0, FormMain.Settings.Location.Timezone);
					interval = ind + (inh + (inm / 60.0)) / 24;

					if (interval == 0.0)
						interval = 1.0;

					if (stop < start)
					{
						MessageBox.Show("End date is less than start date\t\t");
						return;
					}
				}
				catch
				{
					MessageBox.Show("Invalid date\t\t\t");
					return;
				}

				if (EphemerisSettings == null)
					EphemerisSettings = new EphemerisSettings();

				EphemerisSettings.Comet = FormMain.UserList.ElementAt(cbComet.SelectedIndex);

				EphemerisSettings.Start = start;
				EphemerisSettings.Stop = stop;
				EphemerisSettings.Interval = interval;

				EphemerisSettings.TimeSpan = new ATimeSpan(0, 0, (int)ind, (int)inh, (int)inm, 0);

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

		#region Common_ValueChanged

		private void timespanStartCommon_ValueChanged(object sender, EventArgs e)
		{
			bool mChanged = (sender as Control).Name == numMonthStart.Name;
			bool yChanged = (sender as Control).Name == numYearStart.Name;

			int y = (int)numYearStart.Value;
			int m = (int)numMonthStart.Value;
			int d = (int)numDayStart.Value;
			int dmax = (int)numDayStart.Maximum;
			int hh = (int)numHourStart.Value;
			int mm = (int)numMinStart.Value;

			int[] newDate = Utils.ControlDateTime(y, m, d, dmax, hh, mm, mChanged, yChanged);

			if (newDate[6] == 1)
			{
				numDayStart.Maximum = newDate[3] + 1;

				numMinStart.Value = newDate[5];
				numHourStart.Value = newDate[4];
				numDayStart.Value = newDate[2];
				numMonthStart.Value = newDate[1];
				numYearStart.Value = newDate[0];
			}
		}

		private void timespanEndCommon_ValueChanged(object sender, EventArgs e)
		{
			bool mChanged = (sender as Control).Name == numMonthEnd.Name;
			bool yChanged = (sender as Control).Name == numYearEnd.Name;

			int y = (int)numYearEnd.Value;
			int m = (int)numMonthEnd.Value;
			int d = (int)numDayEnd.Value;
			int dmax = (int)numDayEnd.Maximum;
			int hh = (int)numHourEnd.Value;
			int mm = (int)numMinEnd.Value;

			int[] newDate = Utils.ControlDateTime(y, m, d, dmax, hh, mm, mChanged, yChanged);

			if (newDate[6] == 1)
			{
				numDayEnd.Maximum = newDate[3] + 1;

				numMinEnd.Value = newDate[5];
				numHourEnd.Value = newDate[4];
				numDayEnd.Value = newDate[2];
				numMonthEnd.Value = newDate[1];
				numYearEnd.Value = newDate[0];
			}
		}

		#endregion

		#region btnTimespanDefault

		private void btnTimespanStartDefault_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20);
			numYearStart.Value = dt.Year;
			numMonthStart.Value = dt.Month;
			numDayStart.Value = 1;
			numHourStart.Value = 22;
			numMinStart.Value = 0;
		}

		private void btnTimespanEndDefault_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20).AddMonths(1);
			numYearEnd.Value = dt.Year;
			numMonthEnd.Value = dt.Month;
			numDayEnd.Value = DateTime.DaysInMonth(dt.Year, dt.Month);
			numHourEnd.Value = 22;
			numMinEnd.Value = 0;
		}

		private void btnTimespanIntervalDefault_Click(object sender, EventArgs e)
		{
			numDayInterval.Value = 1;
			numHourInterval.Value = 0;
			numMinInterval.Value = 0;
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
