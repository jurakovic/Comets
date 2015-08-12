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

		public EphemerisSettings EphemerisSettings { get; private set; }

		private DateTime _start;
		private DateTime DateStart
		{
			get { return _start; }
			set
			{
				_start = value;
				btnStartDate.Text = _start.ToString(FormMain.DateTimeFormat);
			}
		}

		private DateTime _end;
		private DateTime DateEnd
		{
			get { return _end; }
			set
			{
				_end = value;
				btnEndDate.Text = _end.ToString(FormMain.DateTimeFormat);
			}
		}

		#endregion

		#region Constructor

		public FormEphemerisSettings(FilterCollection filters, EphemerisSettings settings = null)
		{
			InitializeComponent();

			txtDayInterval.Tag = new LeMiMa(4, 0, 3652);
			txtHourInterval.Tag = new LeMiMa(2, 0, 23);
			txtMinInterval.Tag = new LeMiMa(2, 0, 59);

			EphemerisSettings = settings;

			if (EphemerisSettings == null)
			{
				DateStart = FormMain.DefaultDateStart;
				DateEnd = FormMain.DefaultDateEnd;

				btnTimespanIntervalDefault_Click(null, null);
			}
			else
			{
				if (EphemerisSettings.Filters == null)
					EphemerisSettings.Filters = filters;

				DateStart = EphemerisSettings.Start;
				DateEnd = EphemerisSettings.Stop;

				txtDayInterval.Text = EphemerisSettings.TimeSpan.Day.ToString();
				txtHourInterval.Text = EphemerisSettings.TimeSpan.Hour.ToString();
				txtMinInterval.Text = EphemerisSettings.TimeSpan.Minute.ToString();

				rbtnSingle.Checked = !EphemerisSettings.IsMultipleMode;
				rbtnMultiple.Checked = EphemerisSettings.IsMultipleMode;

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

		#region FormEphemerisSettings_Load

		private void FormEphemerisSettings_Load(object sender, EventArgs e)
		{
			if (EphemerisSettings == null)
			{
				EphemerisSettings = new EphemerisSettings();
				EphemerisSettings.Comets = FormMain.UserList.ToList();
				EphemerisSettings.Filters = FormMain.Filters;
				EphemerisSettings.SortProperty = FormMain.SortProperty;
				EphemerisSettings.SortAscending = FormMain.SortAscending;
				EphemerisSettings.AddNew = true;
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

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", Utils.JDToDateTime(c.T).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
				lblPerihDist.Text = String.Format("Perihelion distance:          {0:0.000000} AU", c.q);
				lblPeriod.Text = String.Format("Period:                              {0}", c.P < 10000 ? c.P.ToString("0.000000") + " years" : "-");
			}
		}

		#endregion

		#region btnFilter_Click

		private void btnFilter_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(
				EphemerisSettings.Comets,
				EphemerisSettings.Filters,
				EphemerisSettings.SortProperty,
				EphemerisSettings.SortAscending,
				true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					EphemerisSettings.Comets = fdb.Comets;
					EphemerisSettings.Filters = fdb.Filters;
					EphemerisSettings.SortProperty = fdb.SortProperty;
					EphemerisSettings.SortAscending = fdb.SortAscending;
				}

				BindList();
			}
		}

		#endregion

		#region btnOk_Click

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				if (DateEnd < DateStart)
				{
					MessageBox.Show(
						"End date is less than start date\t\t\t",
						"Comets",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					return;
				}

				double ind = txtDayInterval.Int();
				double inh = txtHourInterval.Int();
				double inm = txtMinInterval.Int();

				double interval = ind + (inh + (inm / 60.0)) / 24;

				if (interval == 0.0)
					interval = 1.0;

				EphemerisSettings.SelectedComet = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex);
				EphemerisSettings.Location = FormMain.Settings.Location;

				EphemerisSettings.IsMultipleMode = rbtnMultiple.Checked;

				EphemerisSettings.Start = DateStart;
				EphemerisSettings.Stop = DateEnd;
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

				if (EphemerisSettings.Results == null)
					EphemerisSettings.Results = new Dictionary<Comet, List<EphemerisResult>>();

				FormMain main = this.Owner as FormMain;

				if (EphemerisSettings.IsMultipleMode && EphemerisSettings.Comets.Count > 1)
					main.SetProgressMaximumValue(EphemerisSettings.Comets.Count);

				await EphemerisManager.CalculateEphemerisAsync(EphemerisSettings, FormMain.Progress);

				if (EphemerisSettings.AddNew && EphemerisSettings.SelectedComet != null)
				{
					FormEphemeris fe = new FormEphemeris(EphemerisSettings) { Owner = this.Owner };
					fe.MdiParent = this.Owner;
					fe.WindowState = FormWindowState.Maximized;
					await fe.LoadResultsAsync();
					fe.Show();
				}
				else if (EphemerisSettings.Results != null && EphemerisSettings.Results.Any())
				{
					FormEphemeris fe = this.Owner.ActiveMdiChild as FormEphemeris;
					fe.EphemerisSettings = this.EphemerisSettings;
					await fe.LoadResultsAsync();
				}

				main.HideProgress();

				this.Close();
			}
		}

		#endregion

		#region Date control

		private void btnStartDate_Click(object sender, EventArgs e)
		{
			DateStart = ShowFormDateTime(FormMain.DefaultDateStart, DateStart, GetT());
		}

		private void btnEndDate_Click(object sender, EventArgs e)
		{
			DateEnd = ShowFormDateTime(FormMain.DefaultDateEnd, DateEnd, GetT());
		}

		private DateTime ShowFormDateTime(DateTime def, DateTime current, double? jd)
		{
			using (FormDateTime fdt = new FormDateTime(def, current, jd))
			{
				if (fdt.ShowDialog() == DialogResult.OK)
					current = fdt.SelectedDateTime;
			}

			return current;
		}

		private double? GetT()
		{
			double? T = null;

			if (cbComet.SelectedIndex >= 0)
				T = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex).T;

			return T;
		}

		private void txtIntervalCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtIntervalCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void btnTimespanIntervalDefault_Click(object sender, EventArgs e)
		{
			txtDayInterval.Text = "1";
			txtHourInterval.Text = "0";
			txtMinInterval.Text = "0";
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
					Comet c = EphemerisSettings.Comets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First();
					cbComet.SelectedIndex = EphemerisSettings.Comets.IndexOf(c);
				}
			}

			lblMultipleCount.Text = EphemerisSettings.Comets.Count + " comets";
		}

		#endregion
	}
}
