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
				PopulateTextBoxes();
			}
		}

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

			this.separator3.Visible = T != null;
			this.mnuPerihelionDate.Visible = T != null;
		}

		#endregion

		#region TextBoxes

		private void txtCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtMonthYear_TextChanged(object sender, EventArgs e)
		{
			if (txtMonth.Text.Length > 0 && txtYear.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(txtYear.Int(), txtMonth.Int());

				LeMiMa o = txtDay.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				if (txtDay.Text.Length > 0 && (txtDay.Int()) > n.Max)
					txtDay.Text = n.Max.ToString();

				txtDay.Tag = n;
			}
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
			int year = txtYear.Int();
			int mon = txtMonth.Int();
			int day = txtDay.Int();
			int hour = txtHour.Int();
			int min = txtMinute.Int();
			int sec = txtSecond.Int();

			DateTime dt = new DateTime(year, mon, day, hour, min, sec, DateTimeKind.Local);

			if (dt < Minimum)
				dt = Minimum;

			if (dt > Maximum)
				dt = Maximum;

			_selected = dt;
		}

		#endregion

		#region PopulateTextBoxes

		private void PopulateTextBoxes()
		{
			txtDay.Text = SelectedDateTime.Day.ToString();
			txtMonth.Text = SelectedDateTime.Month.ToString();
			txtYear.Text = SelectedDateTime.Year.ToString();

			txtHour.Text = SelectedDateTime.Hour.ToString();
			txtMinute.Text = SelectedDateTime.Minute.ToString();
			txtSecond.Text = SelectedDateTime.Second.ToString();
		}

		#endregion
	}
}
