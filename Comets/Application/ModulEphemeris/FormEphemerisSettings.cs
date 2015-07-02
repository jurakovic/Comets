using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.ModulEphemeris
{
	public partial class FormEphemerisSettings : Form
	{
		#region Properties

		public EphemerisSettings EphemerisSettings { get; set; }
		private bool AddNewEphemeris { get; set; }

		#endregion

		#region Constructor

		public FormEphemerisSettings(EphemerisSettings settings = null)
		{
			InitializeComponent();

			txtYearStart.Tag = LeMiMa.LYear;
			txtMonthStart.Tag = LeMiMa.LMonth;
			txtDayStart.Tag = LeMiMa.LDay;
			txtHourStart.Tag = LeMiMa.LHour;
			txtMinStart.Tag = LeMiMa.LMinute;

			txtYearEnd.Tag = LeMiMa.LYear;
			txtMonthEnd.Tag = LeMiMa.LMonth;
			txtDayEnd.Tag = LeMiMa.LDay;
			txtHourEnd.Tag = LeMiMa.LHour;
			txtMinEnd.Tag = LeMiMa.LMinute;

			txtDayInterval.Tag = new LeMiMa(4, 0, 3652);
			txtHourInterval.Tag = new LeMiMa(2, 0, 23);
			txtMinInterval.Tag = new LeMiMa(2, 0, 59);

			EphemerisSettings = settings;

			if (EphemerisSettings == null)
			{
				AddNewEphemeris = true;

				btnTimespanStartDefault_Click(null, null);
				btnTimespanEndDefault_Click(null, null);
				btnTimespanIntervalDefault_Click(null, null);
			}
			else
			{
				ATime dt = EphemerisSettings.Start;
				txtYearStart.Text = dt.Year.ToString();
				txtMonthStart.Text = dt.Month.ToString();
				txtDayStart.Text = dt.Day.ToString();
				txtHourStart.Text = dt.Hour.ToString();
				txtMinStart.Text = dt.Minute.ToString();

				dt = EphemerisSettings.Stop;
				txtYearEnd.Text = dt.Year.ToString();
				txtMonthEnd.Text = dt.Month.ToString();
				txtDayEnd.Text = dt.Day.ToString();
				txtHourEnd.Text = dt.Hour.ToString();
				txtMinEnd.Text = dt.Minute.ToString();

				txtDayInterval.Text = EphemerisSettings.TimeSpan.Day.ToString();
				txtHourInterval.Text = EphemerisSettings.TimeSpan.Hour.ToString();
				txtMinInterval.Text = EphemerisSettings.TimeSpan.Minute.ToString();

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
			if (EphemerisSettings == null)
			{
				EphemerisSettings = new EphemerisSettings();
				EphemerisSettings.Comets = FormMain.UserList.ToList();
			}

			BindList();
		}

		#endregion

		#region cbComet_SelectedIndexChanged

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				Comet c = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = "Perihelion date:                " + c.Ty.ToString() + " " + Comet.Month[c.Tm - 1] + " " + c.Td.ToString("00") + "." + c.Th.ToString("0000");
				lblPerihDist.Text = "Perihelion distance:          " + c.q.ToString("0.000000") + " AU";
				lblPeriod.Text = c.P < 10000 ? "Period:                              " + c.P.ToString("0.000000") + " years" : "Period:                              -";
			}
		}

		#endregion

		#region btnSelectComets_Click

		private void btnSelectComets_Click(object sender, EventArgs e)
		{
			using (FormSelectComets fsc = new FormSelectComets(EphemerisSettings.Comets) { Owner = this })
			{
				fsc.TopMost = this.TopMost;

				if (fsc.ShowDialog() == DialogResult.OK)
					BindList();
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
					int syr = txtYearStart.Int();
					int smo = txtMonthStart.Int();
					int sdy = txtDayStart.Int();
					int shr = txtHourStart.Int();
					int smi = txtMinStart.Int();

					int eyr = txtYearEnd.Int();
					int emo = txtMonthEnd.Int();
					int edy = txtDayEnd.Int();
					int ehr = txtHourEnd.Int();
					int emi = txtMinEnd.Int();

					ind = txtDayInterval.Int();
					inh = txtHourInterval.Int();
					inm = txtMinInterval.Int();

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

				EphemerisSettings.SelectedComet = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex);
				EphemerisSettings.Location = FormMain.Settings.Location;

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

				await EphemerisManager.CalculateEphemeris(EphemerisSettings);

				this.Close();
			}
		}

		#endregion

		#region Date control

		private void txtDateCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtDateCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtYearMonthStart_TextChanged(object sender, EventArgs e)
		{
			if (txtMonthStart.Text.Length > 0 && txtYearStart.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(txtYearStart.Int(), txtMonthStart.Int());

				LeMiMa o = txtDayStart.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				txtDayStart.Tag = n;

				if (txtDayStart.Text.Length > 0 && (txtDayStart.Int()) > n.Max)
					txtDayStart.Text = n.Max.ToString();
			}
		}

		private void txtYearMonthEnd_TextChanged(object sender, EventArgs e)
		{
			if (txtMonthEnd.Text.Length > 0 && txtYearEnd.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(txtYearEnd.Int(), txtMonthEnd.Int());

				LeMiMa o = txtDayEnd.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				txtDayEnd.Tag = n;

				if (txtDayEnd.Text.Length > 0 && txtDayEnd.Int() > n.Max)
					txtDayEnd.Text = n.Max.ToString();
			}
		}

		#endregion

		#region btnTimespanDefault

		private void btnTimespanStartDefault_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20);
			txtYearStart.Text = dt.Year.ToString();
			txtMonthStart.Text = dt.Month.ToString();
			txtDayStart.Text = "1";
			txtHourStart.Text = "22";
			txtMinStart.Text = "0";
		}

		private void btnTimespanEndDefault_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20).AddMonths(1);
			txtYearEnd.Text = dt.Year.ToString();
			txtMonthEnd.Text = dt.Month.ToString();
			txtDayEnd.Text = DateTime.DaysInMonth(dt.Year, dt.Month).ToString();
			txtHourEnd.Text = "22";
			txtMinEnd.Text = "0";
		}

		private void btnTimespanIntervalDefault_Click(object sender, EventArgs e)
		{
			txtDayInterval.Text = "1";
			txtHourInterval.Text = "0";
			txtMinInterval.Text = "0";
		}

		#endregion

		#region Form_Closing

		private void FormEphemerisSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (AddNewEphemeris && EphemerisSettings.SelectedComet != null)
			{
				FormEphemeris fe = new FormEphemeris(EphemerisSettings);
				fe.MdiParent = this.Owner;
				fe.WindowState = FormWindowState.Maximized;
				fe.Show();
			}
			else if (EphemerisSettings.Results != null && EphemerisSettings.Results.Any())
			{
				FormEphemeris fe = this.Owner.ActiveMdiChild as FormEphemeris;
				fe.EphemerisSettings = this.EphemerisSettings;
				fe.LoadResults();
			}
		}

		#endregion

		#region BindList

		private void BindList()
		{
			cbComet.DisplayMember = "full";
			cbComet.DataSource = EphemerisSettings.Comets;

			if (EphemerisSettings.Comets.Any())
			{
				if (EphemerisSettings.SelectedComet != null && EphemerisSettings.Comets.Contains(EphemerisSettings.SelectedComet))
				{
					cbComet.SelectedIndex = EphemerisSettings.Comets.IndexOf(EphemerisSettings.SelectedComet);
				}
				else
				{
					//comet with nearest perihelion date
					Comet c = EphemerisSettings.Comets.Where(x => x.T - DateTime.Now.JD() > 0).OrderBy(y => y.T).FirstOrDefault();
					cbComet.SelectedIndex = c != null ? EphemerisSettings.Comets.IndexOf(c) : 0;
				}
			}
		}

		#endregion
	}
}
