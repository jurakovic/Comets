﻿using Comets.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.DateAndTime
{
	public partial class DateTimeMenuControl : UserControl
	{
		#region Events

		public event Action<object, DateTime> OnSelectedDatetimeChanged;

		#endregion

		#region Enum

		public enum DateTimePreset
		{
			Default = 0,
			Today,
			PerihelionDate,
			LastYear,
			ThisYear,
			NextYear,
			AfterNextYear,
			AddThreeMonths,
			AddSixMonth,
			AddOneYear,
			SubThreeMonths,
			SubSixMonths,
			SubOneYear,
		}

		#endregion

		#region Const

		private readonly DateTime LastYear = new DateTime(DateTime.UtcNow.Year - 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private readonly DateTime ThisYear = new DateTime(DateTime.UtcNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private readonly DateTime NextYear = new DateTime(DateTime.UtcNow.Year + 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private readonly DateTime AfterNextYear = new DateTime(DateTime.UtcNow.Year + 2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#endregion

		#region Properties

		public string Title
		{
			get { return btnShowMenu.Text; }
			set { btnShowMenu.Text = value; }
		}

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
			mnuToday.Tag = DateTimePreset.Today;
			mnuPerihelionDate.Tag = DateTimePreset.PerihelionDate;
			mnuLastYear.Tag = DateTimePreset.LastYear;
			mnuThisYear.Tag = DateTimePreset.ThisYear;
			mnuNextYear.Tag = DateTimePreset.NextYear;
			mnuAfterNextYear.Tag = DateTimePreset.AfterNextYear;
			mnuAddThreeMonths.Tag = DateTimePreset.AddThreeMonths;
			mnuAddSixMonths.Tag = DateTimePreset.AddSixMonth;
			mnuAddOneYear.Tag = DateTimePreset.AddOneYear;
			mnuSubThreeMonths.Tag = DateTimePreset.SubThreeMonths;
			mnuSubSixMonths.Tag = DateTimePreset.SubSixMonths;
			mnuSubOneYear.Tag = DateTimePreset.SubOneYear;

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
				ToolStripMenuItem item = sender as ToolStripMenuItem;
				DateTimePreset preset = (DateTimePreset)item.Tag;

				DateTime retval;

				switch (preset)
				{
					case DateTimePreset.Today:
						retval = DateTime.UtcNow.Date;
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
					case DateTimePreset.SubThreeMonths:
						retval = SelectedDateTime.AddMonths(-3);
						break;
					case DateTimePreset.SubSixMonths:
						retval = SelectedDateTime.AddMonths(-6);
						break;
					case DateTimePreset.SubOneYear:
						retval = SelectedDateTime.AddYears(-1);
						break;
					default:
						retval = DefaultDateTime.GetValueOrDefault(DateTime.UtcNow);
						break;
				}

				OnSelectedDatetimeChanged(ReferenceControl, retval);
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
