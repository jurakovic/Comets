using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

		private CancellationTokenSource cts;

		#endregion

		#region Constructor

		public FormEphemerisSettings(FilterCollection filters, EphemerisSettings settings = null)
		{
			InitializeComponent();

			txtDayInterval.Tag = new ValNum(0, 3652);
			txtHourInterval.Tag = new ValNum(0, 23);
			txtMinInterval.Tag = new ValNum(0, 59);

			txtMaxEarthDist.Tag = new ValNum(0, 9.99, 2);
			txtMaxSunDist.Tag = new ValNum(0, 9.99, 2);
			txtMinMag.Tag = ValNum.VMagnitude;

			if (settings == null)
			{
				DateStart = CommonManager.DefaultDateStart;
				DateEnd = CommonManager.DefaultDateEnd;

				btnTimespanIntervalDefault_Click(null, null);
			}
			else
			{
				if (settings.Filters == null)
					settings.Filters = filters;

				DateStart = settings.Start;
				DateEnd = settings.Stop;

				txtDayInterval.Text = settings.TimeSpan.Day.ToString();
				txtHourInterval.Text = settings.TimeSpan.Hour.ToString();
				txtMinInterval.Text = settings.TimeSpan.Minute.ToString();

				rbtnSingle.Checked = !settings.IsMultipleMode;
				rbtnMultiple.Checked = settings.IsMultipleMode;

				radioLocalTime.Checked = settings.LocalTime;
				radioUnivTime.Checked = !settings.LocalTime;
				chRA.Checked = settings.RA;
				chDec.Checked = settings.Dec;
				chEcLon.Checked = settings.EcLon;
				chEcLat.Checked = settings.EcLat;
				chHelioDist.Checked = settings.HelioDist;
				chGeoDist.Checked = settings.GeoDist;
				chAlt.Checked = settings.Alt;
				chAz.Checked = settings.Az;
				chElong.Checked = settings.Elongation;
				chMag.Checked = settings.Magnitude;

				txtMaxSunDist.Text = settings.MaxSunDistValue != null ? settings.MaxSunDistValue.Value.ToString() : String.Empty;
				cbxMaxSunDist.Checked = settings.MaxSunDistChecked;

				txtMaxEarthDist.Text = settings.MaxEarthDistValue != null ? settings.MaxEarthDistValue.Value.ToString() : String.Empty;
				cbxMaxEarthDist.Checked = settings.MaxEarthDistChecked;

				txtMinMag.Text = settings.MinMagnitudeValue != null ? settings.MinMagnitudeValue.Value.ToString() : String.Empty;
				cbxMinMag.Checked = settings.MinMagnitudeChecked;

				this.EphemerisSettings = settings;
			}
		}

		#endregion

		#region FormEphemerisSettings_Load

		private void FormEphemerisSettings_Load(object sender, EventArgs e)
		{
			if (this.EphemerisSettings == null)
			{
				EphemerisSettings settings = new EphemerisSettings();
				settings.Comets = new CometCollection(CommonManager.UserCollection);
				settings.Filters = CommonManager.Filters;
				settings.SortProperty = CommonManager.SortProperty;
				settings.SortAscending = CommonManager.SortAscending;
				settings.AddNew = true;

				this.EphemerisSettings = settings;
			}

			BindCollection();
		}

		#endregion

		#region FormEphemerisSettings_FormClosing

		private void FormEphemerisSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (cts != null && cts.IsCancellationRequested)
				e.Cancel = true;
		}

		#endregion

		#region cbComet_SelectedIndexChanged

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				Comet c = this.EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", EphemerisManager.JDToDateTime(c.Tn).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
				lblPerihDist.Text = String.Format("Perihelion distance:          {0:0.000000} AU", c.q);
				lblPeriod.Text = String.Format("Period:                              {0}", c.P < 10000 ? c.P.ToString("0.000000") + " years" : "-");
			}
		}

		#endregion

		#region btnFilter_Click

		private void btnFilter_Click(object sender, EventArgs e)
		{
			EphemerisSettings sett = this.EphemerisSettings;

			using (FormDatabase fdb = new FormDatabase(CommonManager.MainCollection, sett.Filters, sett.SortProperty, sett.SortAscending, true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					sett.Comets = fdb.CometsFiltered;
					sett.Filters = fdb.Filters;
					sett.SortProperty = fdb.SortProperty;
					sett.SortAscending = fdb.SortAscending;

					BindCollection();
				}
			}
		}

		#endregion

		#region btnOk_Click

		private async void btnOk_Click(object sender, EventArgs e)
		{
			if (cbComet.SelectedIndex >= 0)
			{
				if (cbxMaxSunDist.Checked && txtMaxSunDist.TextLength == 0)
				{
					MessageBox.Show("Please enter Maximum Sun distance value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMaxEarthDist.Checked && txtMaxEarthDist.TextLength == 0)
				{
					MessageBox.Show("Please enter Maximum Earth distance value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (cbxMinMag.Checked && txtMinMag.TextLength == 0)
				{
					MessageBox.Show("Please enter Minimum magnitude value\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if (DateEnd <= DateStart)
				{
					MessageBox.Show("End date must be greather than start date\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if ((DateEnd - DateStart).TotalDays > 300 * 365.25)
				{
					MessageBox.Show("Timespan must be less than 300 years.\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				decimal ind = Convert.ToDecimal(txtDayInterval.Int());
				decimal inh = Convert.ToDecimal(txtHourInterval.Int());
				decimal inm = Convert.ToDecimal(txtMinInterval.Int());

				decimal interval = ind + (inh + (inm / 60.0m)) / 24.0m;

				if (interval == 0.0m)
					interval = 1.0m;

				EphemerisSettings settings = this.EphemerisSettings;

				settings.SelectedComet = settings.Comets.ElementAt(cbComet.SelectedIndex);
				settings.Location = CommonManager.Settings.Location;

				settings.IsMultipleMode = rbtnMultiple.Checked;

				settings.Start = DateStart;
				settings.Stop = DateEnd;
				settings.Interval = interval;

				settings.TimeSpan = new ATimeSpan(0, 0, (int)ind, (int)inh, (int)inm, 0);

				settings.LocalTime = radioLocalTime.Checked;
				settings.RA = chRA.Checked;
				settings.Dec = chDec.Checked;
				settings.EcLon = chEcLon.Checked;
				settings.EcLat = chEcLat.Checked;
				settings.HelioDist = chHelioDist.Checked;
				settings.GeoDist = chGeoDist.Checked;
				settings.Alt = chAlt.Checked;
				settings.Az = chAz.Checked;
				settings.Elongation = chElong.Checked;
				settings.Magnitude = chMag.Checked;

				settings.MaxSunDistChecked = cbxMaxSunDist.Checked;
				settings.MaxSunDistValue = txtMaxSunDist.TextLength > 0 ? (double?)txtMaxSunDist.Double() : null;

				settings.MaxEarthDistChecked = cbxMaxEarthDist.Checked;
				settings.MaxEarthDistValue = txtMaxEarthDist.TextLength > 0 ? (double?)txtMaxEarthDist.Double() : null;

				settings.MinMagnitudeChecked = cbxMinMag.Checked;
				settings.MinMagnitudeValue = txtMinMag.TextLength > 0 ? (double?)txtMinMag.Double() : null;

				if (!CommonManager.Settings.IgnoreLongCalculationWarning && !SettingsBase.ValidateCalculationAmount(settings))
					return;

				if (settings.Ephemerides == null)
					settings.Ephemerides = new Dictionary<Comet, List<Ephemeris>>();

				FormMain main = this.Owner as FormMain;

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					main.SetProgressMaximumValue(settings.Comets.Count);

				cts = new CancellationTokenSource();

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(settings, FormMain.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					cts = null;
					settings.Ephemerides.Clear();
					main.HideProgress();
					return;
				}

				if (settings.IsMultipleMode && settings.Comets.Count > 1)
					main.SetProgressMaximumValue(settings.Ephemerides.Count);

				FormEphemeris fe = null;

				try
				{
					if (settings.AddNew)
					{
						fe = new FormEphemeris(settings) { Owner = this.Owner };
						fe.MdiParent = this.Owner;
						fe.WindowState = FormWindowState.Maximized;
						await fe.LoadResultsAsync(cts.Token);
						fe.Show();
					}
					else
					{
						fe = this.Owner.ActiveMdiChild as FormEphemeris;
						fe.EphemerisSettings = settings;
						await fe.LoadResultsAsync(cts.Token);
					}
				}
				catch (OperationCanceledException)
				{
					cts = null;
					settings.Ephemerides.Clear();
					main.HideProgress();

					if (settings.AddNew)
						fe.Dispose();

					return;
				}

				main.HideProgress();

				this.Close();
			}
		}

		#endregion

		#region btnCancel_Click

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (cts != null)
				cts.Cancel();
			else
				this.Close();
		}

		#endregion

		#region Date control

		private void btnStartDate_Click(object sender, EventArgs e)
		{
			DateStart = ShowFormDateTime(CommonManager.DefaultDateStart, DateStart, GetT());
		}

		private void btnEndDate_Click(object sender, EventArgs e)
		{
			DateEnd = ShowFormDateTime(CommonManager.DefaultDateEnd, DateEnd, GetT());
		}

		private DateTime ShowFormDateTime(DateTime def, DateTime current, decimal? jd)
		{
			using (FormDateTime fdt = new FormDateTime(def, current, jd))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					current = fdt.SelectedDateTime;
			}

			return current;
		}

		private decimal? GetT()
		{
			return this.EphemerisSettings.Comets.ElementAtOrDefault(cbComet.SelectedIndex)?.Tn;
		}

		private void txtIntervalCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtIntervalCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void btnTimespanIntervalDefault_Click(object sender, EventArgs e)
		{
			txtDayInterval.Text = "1";
			txtHourInterval.Text = "0";
			txtMinInterval.Text = "0";
		}

		#endregion

		#region Requirements

		private void txtMagDistCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtMaxSunDist_TextChanged(object sender, EventArgs e)
		{
			cbxMaxSunDist.Checked = txtMaxSunDist.TextLength > 0;
		}

		private void txtMaxEarthDist_TextChanged(object sender, EventArgs e)
		{
			cbxMaxEarthDist.Checked = txtMaxEarthDist.TextLength > 0;
		}

		private void txtMinMag_TextChanged(object sender, EventArgs e)
		{
			cbxMinMag.Checked = txtMinMag.TextLength > 0;
		}

		#endregion

		#region BindCollection

		private void BindCollection()
		{
			EphemerisSettings settings = this.EphemerisSettings;

			cbComet.DisplayMember = CometManager.PropertyEnum.full.ToString();
			cbComet.DataSource = settings.Comets;

			if (settings.Comets.Count > 0)
			{
				if (settings.SelectedComet != null && settings.Comets.Contains(settings.SelectedComet))
				{
					cbComet.SelectedIndex = settings.Comets.IndexOf(settings.SelectedComet);
				}
				else
				{
					//comet with nearest perihelion date
					decimal jdNow = DateTime.Now.JD();
					Comet c = settings.Comets.OrderBy(x => Math.Abs(x.Tn - jdNow)).First();
					cbComet.SelectedIndex = settings.Comets.IndexOf(c);
				}
			}

			lblMultipleCount.Text = settings.Comets.Count + " comets";
		}

		#endregion
	}
}
