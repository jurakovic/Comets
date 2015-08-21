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

				txtMaxSunDist.Text = EphemerisSettings.MaxSunDistValue != null ? EphemerisSettings.MaxSunDistValue.Value.ToString() : String.Empty;
				cbxMaxSunDist.Checked = EphemerisSettings.MaxSunDistChecked;

				txtMaxEarthDist.Text = EphemerisSettings.MaxEarthDistValue != null ? EphemerisSettings.MaxEarthDistValue.Value.ToString() : String.Empty;
				cbxMaxEarthDist.Checked = EphemerisSettings.MaxEarthDistChecked;

				txtMinMag.Text = EphemerisSettings.MinMagnitudeValue != null ? EphemerisSettings.MinMagnitudeValue.Value.ToString() : String.Empty;
				cbxMinMag.Checked = EphemerisSettings.MinMagnitudeChecked;
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
				Comet c = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex);

				lblPerihDate.Text = String.Format("Perihelion date:                {0}", Utils.JDToDateTime(c.Tn).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss"));
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

				if (DateEnd < DateStart)
				{
					MessageBox.Show("End date is less than start date\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				if ((DateEnd - DateStart).TotalDays > 300 * 365.25)
				{
					MessageBox.Show("Timespan must be less than 300 years.\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

				EphemerisSettings.MaxSunDistChecked = cbxMaxSunDist.Checked;
				EphemerisSettings.MaxSunDistValue = txtMaxSunDist.TextLength > 0 ? (double?)txtMaxSunDist.Double() : null;

				EphemerisSettings.MaxEarthDistChecked = cbxMaxEarthDist.Checked;
				EphemerisSettings.MaxEarthDistValue = txtMaxEarthDist.TextLength > 0 ? (double?)txtMaxEarthDist.Double() : null;

				EphemerisSettings.MinMagnitudeChecked = cbxMinMag.Checked;
				EphemerisSettings.MinMagnitudeValue = txtMinMag.TextLength > 0 ? (double?)txtMinMag.Double() : null;

				if (!FormMain.Settings.IgnoreLongCalculationWarning && !SettingsBase.ValidateCalculationAmount(EphemerisSettings))
					return;

				if (EphemerisSettings.Results == null)
					EphemerisSettings.Results = new Dictionary<Comet, List<EphemerisResult>>();

				FormMain main = this.Owner as FormMain;

				if (EphemerisSettings.IsMultipleMode && EphemerisSettings.Comets.Count > 1)
					main.SetProgressMaximumValue(EphemerisSettings.Comets.Count);

				cts = new CancellationTokenSource();

				try
				{
					await EphemerisManager.CalculateEphemerisAsync(EphemerisSettings, FormMain.Progress, cts.Token);
				}
				catch (OperationCanceledException)
				{
					cts = null;
					EphemerisSettings.Results.Clear();
					main.HideProgress();
					return;
				}

				if (EphemerisSettings.IsMultipleMode && EphemerisSettings.Comets.Count > 1)
					main.SetProgressMaximumValue(EphemerisSettings.Results.Count);

				FormEphemeris fe = null;

				try
				{
					if (EphemerisSettings.AddNew)
					{
						fe = new FormEphemeris(EphemerisSettings) { Owner = this.Owner };
						fe.MdiParent = this.Owner;
						fe.WindowState = FormWindowState.Maximized;
						await fe.LoadResultsAsync(cts.Token);
						fe.Show();
					}
					else
					{
						fe = this.Owner.ActiveMdiChild as FormEphemeris;
						fe.EphemerisSettings = this.EphemerisSettings;
						await fe.LoadResultsAsync(cts.Token);
					}
				}
				catch (OperationCanceledException)
				{
					cts = null;
					EphemerisSettings.Results.Clear();
					main.HideProgress();

					if (EphemerisSettings.AddNew)
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
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					current = fdt.SelectedDateTime;
			}

			return current;
		}

		private double? GetT()
		{
			double? T = null;

			if (cbComet.SelectedIndex >= 0)
				T = EphemerisSettings.Comets.ElementAt(cbComet.SelectedIndex).Tn;

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

		#region Requirements

		private void txtMaxDistCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 1, 2, 0, 9.99);
		}

		private void txtMinMag_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 2, 2, -20, 40);
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
					Comet c = EphemerisSettings.Comets.OrderBy(x => Math.Abs(x.Tn - DateTime.Now.JD())).First();
					cbComet.SelectedIndex = EphemerisSettings.Comets.IndexOf(c);
				}
			}

			lblMultipleCount.Text = EphemerisSettings.Comets.Count + " comets";
		}

		#endregion
	}
}
