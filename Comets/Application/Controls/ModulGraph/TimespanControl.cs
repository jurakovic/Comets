using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application.Controls.ModulGraph
{
	public partial class TimespanControl : UserControl
	{
		#region Fields

		private DateTime? _perihelionDateTime;

		#endregion

		#region Properties

		public DateTime SelectedDateStart
		{
			get { return selectDateControlStart.SelectedDateTime; }
			set { selectDateControlStart.SelectedDateTime = value; }
		}

		public DateTime SelectedDateEnd
		{
			get { return selectDateControlEnd.SelectedDateTime; }
			set { selectDateControlEnd.SelectedDateTime = value; }
		}

		public int DaysBeforeT
		{
			get { return txtDaysBeforeT.Int(); }
			set { txtDaysBeforeT.Text = value.ToString(); }
		}

		public int DaysAfterT
		{
			get { return txtDaysAfterT.Int(); }
			set { txtDaysAfterT.Text = value.ToString(); }
		}

		public bool DateRange
		{
			get
			{
				return rbtnRangeDate.Checked;
			}
			set
			{
				rbtnRangeDate.Checked = value;
				rbtnRangeDaysFromT.Checked = !value;
			}
		}

		public DateTime? PerihelionDate
		{
			get { return _perihelionDateTime; }
			set { _perihelionDateTime = selectDateControlStart.PerihelionDate = selectDateControlEnd.PerihelionDate = value; }
		}

		public DateTime DateStart
		{
			get
			{
				return this.DateRange
					? this.SelectedDateStart
					: this.PerihelionDate.GetValueOrDefault(DateTime.Now).AddDays(this.DaysBeforeT); //negativan broj
			}
		}

		public DateTime DateEnd
		{
			get
			{
				return this.DateRange
					? this.SelectedDateEnd
					: this.PerihelionDate.GetValueOrDefault(DateTime.Now).AddDays(this.DaysAfterT);
			}
		}

		private decimal TotalDays
		{
			get { return DateEnd.JD() - DateStart.JD(); }
		}

		public decimal Interval
		{
			get
			{
				decimal interval = 0.0m;
				decimal totalDays = this.TotalDays;

				if (totalDays <= 100)
					interval = totalDays / 100.0m;
				else if (totalDays < 365)
					interval = 1;
				else if (totalDays < 10 * 365.25m)
					interval = 2;
				else if (totalDays < 50 * 365.25m)
					interval = 5;
				else if (totalDays < 100 * 365.25m)
					interval = 15;
				else if (totalDays < 200 * 365.25m)
					interval = 30;
				else if (totalDays < 300 * 365.25m)
					interval = 40;

				return interval;
			}
		}

		#endregion

		#region Constructor

		public TimespanControl()
		{
			InitializeComponent();

			txtDaysBeforeT.Tag = new ValNum(-3653, -1);
			txtDaysAfterT.Tag = new ValNum(1, 3653);

			selectDateControlStart.DefaultDateTime = CommonManager.DefaultDateStart;
			selectDateControlEnd.DefaultDateTime = CommonManager.DefaultDateEnd;
		}

		#endregion

		#region +EventHandling

		#region Button

		private void btnDaysFromTDefault_Click(object sender, EventArgs e)
		{
			int offset = 180;
			this.DaysBeforeT = -offset;
			this.DaysAfterT = offset;
		}

		#endregion

		#region TextBox

		private void txtDaysFromTCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtDaysFromTCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtDaysFromTCommon_TextChanged(object sender, EventArgs e)
		{
			rbtnRangeDaysFromT.Checked = true;
		}

		#endregion

		#endregion

		#region ValidateData

		public void ValidateData()
		{
			if (this.DateEnd <= this.DateStart)
				throw new ValidationException("End date must be greather than start date");

			if (this.TotalDays >= 300 * 365.25m)
				throw new ValidationException("Timespan must be less than 300 years");
		}

		#endregion
	}
}
