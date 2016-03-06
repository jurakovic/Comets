using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormDateTime : Form
	{
		#region Const

		public static DateTime Maximum = new DateTime(2300, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public static DateTime Minimum = new DateTime(1700, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#endregion

		#region Properties

		private DateTime _selected;
		public DateTime SelectedDateTime
		{
			get
			{
				return _selected;
			}
			private set
			{
				_selected = value;
				PopulateData();
			}
		}

		public bool ValueChangedByEvent { get; set; }

		private DateTime DefaultDateTime { get; set; }
		private double? T { get; set; }

		#endregion

		#region Constructor

		public FormDateTime(DateTime def, DateTime current, double? t)
		{
			InitializeComponent();

			txtDay.Tag = ValNum.VDay;
			txtMonth.Tag = ValNum.VMonth;
			txtYear.Tag = ValNum.VYear;

			txtHour.Tag = ValNum.VHour;
			txtMinute.Tag = ValNum.VMinute;
			txtSecond.Tag = ValNum.VSecond;

			DefaultDateTime = def;
			SelectedDateTime = current;
			T = t;

			this.mnuPerihelionDate.Visible = this.separator3.Visible = T != null;
		}

		#endregion

		#region TextBoxes

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

		#region ContextMenu

		private void btnSelect_Click(object sender, EventArgs e)
		{
			contextDateTime.Show(gbxDateTime, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
		}

		private void mnuDefault_Click(object sender, EventArgs e)
		{
			SelectedDateTime = DefaultDateTime;
		}

		private void mnuNow_Click(object sender, EventArgs e)
		{
			SelectedDateTime = DateTime.Now;
		}

		private void mnuMidnight_Click(object sender, EventArgs e)
		{
			DateTime d = DateTime.Now.AddDays(1);
			SelectedDateTime = new DateTime(d.Year, d.Month, d.Day, 00, 00, 00, DateTimeKind.Local);
		}

		private void mnuNoon_Click(object sender, EventArgs e)
		{
			DateTime d = DateTime.Now;
			SelectedDateTime = new DateTime(d.Year, d.Month, d.Day, 12, 00, 00, DateTimeKind.Local);
		}

		private void mnuPerihelionDate_Click(object sender, EventArgs e)
		{
			DateTime d = EphemerisManager.JDToDateTime(T.Value);
			SelectedDateTime = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, DateTimeKind.Utc).ToLocalTime();
		}

		#endregion

		#region btnOk_Click

		private void btnOk_Click(object sender, EventArgs e)
		{
			CollectData();
		}

		#endregion

		#region RangeDateTime

		public static DateTime RangeDateTime(DateTime dt)
		{
			if (dt < Minimum)
				dt = Minimum;

			if (dt > Maximum)
				dt = Maximum;

			if (dt.Year == 1582 && dt.Month == 10)
			{
				int day = dt.Day;

				if (5 <= dt.Day && dt.Day < 10)
				{
					day = 15;
				}
				else if (10 <= dt.Day && dt.Day < 15)
				{
					day = 4;
				}

				dt = new DateTime(dt.Year, dt.Month, day, dt.Hour, dt.Minute, dt.Second, dt.Kind);
			}

			return dt;
		}

		#endregion

		#region PopulateData

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

		#endregion

		#region CollectData

		private void CollectData()
		{
			int year = txtYear.Int();
			int mon = txtMonth.Int();
			int day = txtDay.Int();
			int hour = txtHour.Int();
			int min = txtMinute.Int();
			int sec = txtSecond.Int();

			DateTime dt = new DateTime(year, mon, day, hour, min, sec, DateTimeKind.Local);

			dt = RangeDateTime(dt);

			_selected = dt;
		}

		#endregion
	}
}
