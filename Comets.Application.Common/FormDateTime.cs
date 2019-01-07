using Comets.Application.Common.Controls.Common;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormDateTime : ValueChangeForm
	{
		#region Fields

		private DateTime _selectedDateTime;

		#endregion

		#region Properties

		public DateTime SelectedDateTime
		{
			get
			{
				return _selectedDateTime;
			}
			private set
			{
				_selectedDateTime = dateTimeMenuControl.SelectedDateTime = value;
				PopulateData();
			}
		}

		#endregion

		#region Constructor

		public FormDateTime(DateTime selectedDateTime, DateTime? defaultDateTime, DateTime? perihelionDate)
		{
			InitializeComponent();

			txtDay.Tag = ValNum.VDay;
			txtMonth.Tag = ValNum.VMonth;
			txtYear.Tag = ValNum.VYear;

			txtHour.Tag = ValNum.VHour;
			txtMinute.Tag = ValNum.VMinute;
			txtSecond.Tag = ValNum.VSecond;

			decimal min = EphemerisManager.MinimumDateTime.JD();
			decimal max = EphemerisManager.MaximumDateTime.JD();
			txtJD.Tag = new ValNum((double)min, (double)max, 5);

			dateTimeMenuControl.OnSelectedDatetimeChanged += SetDateTime;
			dateTimeMenuControl.DefaultDateTime = defaultDateTime;
			dateTimeMenuControl.PerihelionDate = perihelionDate;

			SelectedDateTime = selectedDateTime;
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormDateTime_Load(object sender, EventArgs e)
		{
			this.ActiveControl = btnOk;
		}

		#endregion

		#region TextBox

		private void txtCommon_KeyDown(object sender, KeyEventArgs e)
		{
			bool up = e.KeyData == Keys.Up;
			bool down = e.KeyData == Keys.Down;

			if (up || down)
			{
				ValueChangedInternal = true;

				ValNum val = (sender as TextBox).Tag as ValNum;
				Type t = typeof(DateTime);
				MethodInfo minfo = t.GetMethod("Add" + val.DateTimeValue + "s"); //e.g. AddMonths
				SelectedDateTime = (DateTime)minfo.Invoke(SelectedDateTime, new object[] { up ? 1 : -1 });

				e.SuppressKeyPress = true;
				ValueChangedInternal = false;
			}
			else
			{
				e.SuppressKeyPress = e.KeyCode == Keys.Delete;
			}
		}

		private void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtMonthYear_TextChanged(object sender, EventArgs e)
		{
			if (txtMonth.Text.Length > 0 && txtYear.Text.Length > 0)
			{
				bool tempValueChanged = ValueChangedInternal;
				ValueChangedInternal = true;

				int max = DateTime.DaysInMonth(txtYear.Int(), txtMonth.Int());

				ValNum o = txtDay.Tag as ValNum;
				ValNum n = new ValNum(o.IMin, max, ValNum.DateTimeValueEnum.Day);

				if (txtDay.Text.Length > 0 && txtDay.Int() > n.IMax)
					txtDay.Text = n.IMax.ToString();

				txtDay.Tag = n;

				ValueChangedInternal = tempValueChanged;
			}

			if (!ValueChangedInternal)
				CollectData();
		}

		private void txtCommon_TextChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
				CollectData();
		}

		private void txtJD_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = e.KeyCode == Keys.Delete || ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtJD_TextChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
				SelectedDateTime = EphemerisManager.JDToDateTime((decimal)txtJD.Double());
		}

		#endregion

		#region Button

		private void btnOk_Click(object sender, EventArgs e)
		{
			CollectData();

			RangeDateTime(SelectedDateTime, out DateTime dt);
			SelectedDateTime = dt;
		}

		#endregion

		#endregion

		#region Methods

		private void SetDateTime(object sender, DateTime dateTime)
		{
			SelectedDateTime = dateTime;
		}

		public static bool RangeDateTime(DateTime dt, out DateTime value)
		{
			bool retval = false;

			if (dt < EphemerisManager.MinimumDateTime || dt > EphemerisManager.MaximumDateTime)
			{
				dt = dt.Range(EphemerisManager.MinimumDateTime, EphemerisManager.MaximumDateTime);
				retval = true;
			}
			else if (dt.Year == 1582 && dt.Month == 10)
			{
				int day = dt.Day;

				if (day >= 5 && day < 10)
					day = 15;
				else if (day >= 10 && day < 15)
					day = 4;

				dt = new DateTime(dt.Year, dt.Month, day, dt.Hour, dt.Minute, dt.Second, dt.Kind);
			}

			value = dt;

			return retval;
		}

		private void PopulateData()
		{
			ValueChangedInternal = true;

			txtDay.Text = SelectedDateTime.Day.ToString();
			txtMonth.Text = SelectedDateTime.Month.ToString();
			txtYear.Text = SelectedDateTime.Year.ToString();

			txtHour.Text = SelectedDateTime.Hour.ToString();
			txtMinute.Text = SelectedDateTime.Minute.ToString();
			txtSecond.Text = SelectedDateTime.Second.ToString();

			txtLocalTime.Text = SelectedDateTime.ToLocalTime().ToString(DateTimeFormat.Full);

			txtJD.Text = SelectedDateTime.JD().ToString("0.0####");

			ValueChangedInternal = false;
		}

		private void CollectData()
		{
			int year = txtYear.Int();
			int mon = txtMonth.Int();
			int day = txtDay.Int();
			int hour = txtHour.Int();
			int min = txtMinute.Int();
			int sec = txtSecond.Int();

			SelectedDateTime = new DateTime(year, mon, day, hour, min, sec, DateTimeKind.Utc);
		}

		#endregion
	}
}
