using Comets.BusinessLayer.Business;
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
			PerihelionDate,
			LastYear,
			ThisYear,
			NextYear,
			AfterNextYear,
			AddThreeMonths,
			AddSixMonth,
			AddOneYear,
		}

		#endregion

		#region Const

		private readonly DateTime LastYear = new DateTime(DateTime.Now.Year - 1, 1, 1);
		private readonly DateTime ThisYear = new DateTime(DateTime.Now.Year, 1, 1);
		private readonly DateTime NextYear = new DateTime(DateTime.Now.Year + 1, 1, 1);
		private readonly DateTime AfterNextYear = new DateTime(DateTime.Now.Year + 2, 1, 1);

		#endregion

		#region Properties

		public DateTime SelectedDateTime { get; set; }

		public DateTime? DefaultDateTime { get; set; }

		public DateTime? PerihelionDate { get; set; }

		public Control ReferenceControl { get; set; }

		#endregion

		#region Constructor

		public DateTimeMenuControl()
		{
			InitializeComponent();

			mnuDefault.Tag = DateTimePreset.Default;
			mnuNow.Tag = DateTimePreset.Now;
			mnuPerihelionDate.Tag = DateTimePreset.PerihelionDate;
			mnuLastYear.Tag = DateTimePreset.LastYear;
			mnuThisYear.Tag = DateTimePreset.ThisYear;
			mnuNextYear.Tag = DateTimePreset.NextYear;
			mnuAfterNextYear.Tag = DateTimePreset.AfterNextYear;
			mnuAddThreeMonths.Tag = DateTimePreset.AddThreeMonths;
			mnuAddSixMonth.Tag = DateTimePreset.AddSixMonth;
			mnuAddOneYear.Tag = DateTimePreset.AddOneYear;

			mnuLastYear.Text = LastYear.ToString(DateTimeFormat.PerihelionDate);
			mnuThisYear.Text = ThisYear.ToString(DateTimeFormat.PerihelionDate);
			mnuNextYear.Text = NextYear.ToString(DateTimeFormat.PerihelionDate);
			mnuAfterNextYear.Text = AfterNextYear.ToString(DateTimeFormat.PerihelionDate);
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
					case DateTimePreset.PerihelionDate:
						retval = PerihelionDate.Value;
						break;
					case DateTimePreset.LastYear:
						retval = LastYear;
						break;
					case DateTimePreset.ThisYear:
						retval = ThisYear;
						break;
					case DateTimePreset.NextYear:
						retval = NextYear;
						break;
					case DateTimePreset.AfterNextYear:
						retval = AfterNextYear;
						break;
					case DateTimePreset.AddThreeMonths:
						retval = SelectedDateTime.AddMonths(3);
						break;
					case DateTimePreset.AddSixMonth:
						retval = SelectedDateTime.AddMonths(6);
						break;
					case DateTimePreset.AddOneYear:
						retval = SelectedDateTime.AddYears(1);
						break;
					default:
						retval = DefaultDateTime.GetValueOrDefault(DateTime.Now);
						break;
				}

				OnSelectedDatetimeChanged(retval);
			}
		}

		#endregion

		#region Button

		private void btnShowMenu_Click(object sender, EventArgs e)
		{
			this.mnuDefault.Visible = this.sepDefault.Visible = DefaultDateTime != null;
			this.mnuPerihelionDate.Visible = this.sepPerihelionDate.Visible = PerihelionDate != null;

			Control src = ReferenceControl ?? (sender as Button);
			ctxMenu.Show(src.Parent, new Point(src.Left + 1, src.Top + src.Height - 1));
		}

		#endregion

		#endregion
	}
}
