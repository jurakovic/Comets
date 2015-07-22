using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormDateTime : Form
	{
		#region Const

		public static DateTime Maximum = new DateTime(2500, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public static DateTime Minimum = new DateTime(1500, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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

		public bool IsTextChangedByProgram { get; set; }

		private DateTime DefaultDateTime { get; set; }
		private double? T { get; set; }

		#endregion

		#region Constructor

		public FormDateTime(DateTime def, DateTime current, double? t)
		{
			InitializeComponent();

			txtDay.Tag = LeMiMa.LDay;
			txtMonth.Tag = LeMiMa.LMonth;
			txtYear.Tag = LeMiMa.LYear;

			txtHour.Tag = LeMiMa.LHour;
			txtMinute.Tag = LeMiMa.LMinute;
			txtSecond.Tag = LeMiMa.LSecond;

			DefaultDateTime = def;
			SelectedDateTime = current;
			T = t;

			this.mnuPerihelionDate.Visible = this.separator3.Visible = T != null;
		}

		#endregion

		#region TextBoxes

		private void txtCommon_KeyDown(object sender, KeyEventArgs e)
		{
			IsTextChangedByProgram = true;

			bool suppress = Utils.TextBoxValueUpDown(sender, e);

			bool up = e.KeyData == Keys.Up;
			bool down = e.KeyData == Keys.Down;

			if (up || down)
			{
				DateTime dt = SelectedDateTime;

				int value = 0;

				if (up)
					value = 1;

				if (down)
					value = -1;

				LeMiMa lemima = (sender as TextBox).Tag as LeMiMa;

				switch (lemima.Name)
				{
					case LeMiMa.NameEnum.Day:
						dt = dt.AddDays(value);
						break;
					case LeMiMa.NameEnum.Month:
						dt = dt.AddMonths(value);
						break;
					case LeMiMa.NameEnum.Year:
						dt = dt.AddYears(value);
						break;
					case LeMiMa.NameEnum.Hour:
						dt = dt.AddHours(value);
						break;
					case LeMiMa.NameEnum.Minute:
						dt = dt.AddMinutes(value);
						break;
					case LeMiMa.NameEnum.Second:
						dt = dt.AddSeconds(value);
						break;
				}

				dt = RangeDateTime(dt);
				SelectedDateTime = dt;
			}

			e.SuppressKeyPress = suppress;
			IsTextChangedByProgram = false;
		}

		private void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtCommon_TextChanged(object sender, EventArgs e)
		{
			if (!IsTextChangedByProgram)
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
			DateTime d = Utils.JDToDateTime(T.Value);
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

		private DateTime RangeDateTime(DateTime dt)
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
			IsTextChangedByProgram = true;

			txtDay.Text = SelectedDateTime.Day.ToString("00");
			txtMonth.Text = SelectedDateTime.Month.ToString("00");
			txtYear.Text = SelectedDateTime.Year.ToString();

			txtHour.Text = SelectedDateTime.Hour.ToString("00");
			txtMinute.Text = SelectedDateTime.Minute.ToString("00");
			txtSecond.Text = SelectedDateTime.Second.ToString("00");

			IsTextChangedByProgram = false;
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
