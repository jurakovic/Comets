using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comets.Application.Controls.Common
{
	public partial class DateTimeMenuControl : UserControl
	{
		#region Events

		public Action<DateTime> OnSelectedDatetimeChanged;

		#endregion

		#region Enum

		public enum DateTimePreset
		{
			Default = 0,
			Now,
			Midnight,
			Noon,
			LastYearFirstDay,
			LastYearLastDay,
			ThisYearFirstDay,
			ThisYearLastDay,
			NextYearFirstDay,
			NextYearLastDay,
			PerihelionDate
		}

		#endregion

		#region Properties

		public DateTime DefaultDateTime { get; set; }

		public DateTime? PerihelionDate { get; set; }

		#endregion

		#region Constructor

		public DateTimeMenuControl()
		{
			InitializeComponent();

			mnuDefault.Tag = DateTimePreset.Default;
			mnuNow.Tag = DateTimePreset.Now;
			mnuMidnight.Tag = DateTimePreset.Midnight;
			mnuNoon.Tag = DateTimePreset.Noon;
			mnuLastYearFirstDay.Tag = DateTimePreset.LastYearFirstDay;
			mnuLastYearLastDay.Tag = DateTimePreset.LastYearLastDay;
			mnuThisYearFirstDay.Tag = DateTimePreset.ThisYearFirstDay;
			mnuThisYearLastDay.Tag = DateTimePreset.ThisYearLastDay;
			mnuNextYearFirstDay.Tag = DateTimePreset.NextYearFirstDay;
			mnuNextYearLastDay.Tag = DateTimePreset.NextYearLastDay;
			mnuPerihelionDate.Tag = DateTimePreset.PerihelionDate;
		}

		#endregion

		#region +EventHandling

		#region MenuItem

		private void mnuCommon_Click(object sender, EventArgs e)
		{
			if (OnSelectedDatetimeChanged != null)
			{
				MenuItem item = sender as MenuItem;
				DateTimePreset preset = (DateTimePreset)item.Tag;

				DateTime retval;

				switch (preset)
				{
					case DateTimePreset.Now:
						retval = DateTime.Now;
						break;
					case DateTimePreset.Midnight:
						retval = DateTime.Now.Date;
						break;
					case DateTimePreset.Noon:
						retval = DateTime.Now.Date.AddHours(12);
						break;
					case DateTimePreset.LastYearFirstDay:
						retval = new DateTime(DateTime.Now.Year - 1, 1, 1);
						break;
					case DateTimePreset.LastYearLastDay:
						retval = new DateTime(DateTime.Now.Year - 1, 12, 31);
						break;
					case DateTimePreset.ThisYearFirstDay:
						retval = new DateTime(DateTime.Now.Year, 1, 1);
						break;
					case DateTimePreset.ThisYearLastDay:
						retval = new DateTime(DateTime.Now.Year, 12, 31);
						break;
					case DateTimePreset.NextYearFirstDay:
						retval = new DateTime(DateTime.Now.Year + 1, 1, 1);
						break;
					case DateTimePreset.NextYearLastDay:
						retval = new DateTime(DateTime.Now.Year + 1, 12, 31);
						break;
					case DateTimePreset.PerihelionDate:
						retval = PerihelionDate.Value;
						break;
					default:
						retval = DefaultDateTime;
						break;
				}

				OnSelectedDatetimeChanged(retval);
			}
		}

		#endregion

		#region Button

		private void btnShowMenu_Click(object sender, EventArgs e)
		{
			this.mnuPerihelionDate.Visible = this.separator6.Visible = PerihelionDate != null;

			Button src = sender as Button;
			ctxMenu.Show(this, new Point(src.Left + 1, src.Top + src.Height - 1));
		}

		#endregion

		#endregion
	}
}
