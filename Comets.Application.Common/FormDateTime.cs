using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormDateTime : Form
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

		public bool ValueChangedByEvent { get; set; }

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

			dateTimeMenuControl.OnSelectedDatetimeChanged += OnSelectedDateTimeChanged;
			dateTimeMenuControl.DefaultDateTime = defaultDateTime;
			dateTimeMenuControl.PerihelionDate = perihelionDate;

			SelectedDateTime = selectedDateTime;
		}

		#endregion

		#region +EventHandling

		#region Event

		public void OnSelectedDateTimeChanged(DateTime dateTime)
		{
			SelectedDateTime = dateTime;
		}

		#endregion

		#region TextBox

		private void txtCommon_KeyDown(object sender, KeyEventArgs e)
		{
			bool up = e.KeyData == Keys.Up;
			bool down = e.KeyData == Keys.Down;

			if (up || down)
			{
				ValueChangedByEvent = true;

				ValNum val = (sender as TextBox).Tag as ValNum;
				Type t = typeof(DateTime);
				MethodInfo minfo = t.GetMethod("Add" + val.DateTimeValue + "s"); //e.g. AddMonths
				SelectedDateTime = (DateTime)minfo.Invoke(SelectedDateTime, new object[] { up ? 1 : -1 });

				e.SuppressKeyPress = true;
				ValueChangedByEvent = false;
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
				bool tempValueChanged = ValueChangedByEvent;
				ValueChangedByEvent = true;

				int max = DateTime.DaysInMonth(txtYear.Int(), txtMonth.Int());

				ValNum o = txtDay.Tag as ValNum;
				ValNum n = new ValNum(o.IMin, max, ValNum.DateTimeValueEnum.Day);

				if (txtDay.Text.Length > 0 && txtDay.Int() > n.IMax)
					txtDay.Text = n.IMax.ToString();

				txtDay.Tag = n;

				ValueChangedByEvent = tempValueChanged;
			}

			if (!ValueChangedByEvent)
				CollectData();
		}

		private void txtCommon_TextChanged(object sender, EventArgs e)
		{
			if (!ValueChangedByEvent)
				CollectData();
		}

		#endregion

		#region Button

		private void btnOk_Click(object sender, EventArgs e)
		{
			CollectData();
		}

		#endregion

		#endregion

		#region Methods

		public static bool RangeDateTime(DateTime dt, out DateTime value)
		{
			bool retval = false;

			if (dt < EphemerisManager.MinimumDateTime || dt > EphemerisManager.MaximumDateTime)
			{
				dt = dt < EphemerisManager.MinimumDateTime ? EphemerisManager.MinimumDateTime : EphemerisManager.MaximumDateTime;
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
			ValueChangedByEvent = true;

			txtDay.Text = SelectedDateTime.Day.ToString("00");
			txtMonth.Text = SelectedDateTime.Month.ToString("00");
			txtYear.Text = SelectedDateTime.Year.ToString();

			txtHour.Text = SelectedDateTime.Hour.ToString("00");
			txtMinute.Text = SelectedDateTime.Minute.ToString("00");
			txtSecond.Text = SelectedDateTime.Second.ToString("00");

			ValueChangedByEvent = false;
		}

		private void CollectData()
		{
			int year = txtYear.Int();
			int mon = txtMonth.Int();
			int day = txtDay.Int();
			int hour = txtHour.Int();
			int min = txtMinute.Int();
			int sec = txtSecond.Int();

			DateTime dt = new DateTime(year, mon, day, hour, min, sec, DateTimeKind.Local);

			RangeDateTime(dt, out _selectedDateTime);
		}

		#endregion
	}
}
