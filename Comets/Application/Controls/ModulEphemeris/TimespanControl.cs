using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application.Controls.ModulEphemeris
{
	public partial class TimespanControl : UserControl
	{
		#region Properties

		public DateTime DateStart
		{
			get { return selectDateControlStart.SelectedDateTime; }
			set { selectDateControlStart.SelectedDateTime = value; }
		}

		public DateTime DateEnd
		{
			get { return selectDateControlEnd.SelectedDateTime; }
			set { selectDateControlEnd.SelectedDateTime = value; }
		}

		public int DayInterval
		{
			get { return txtDayInterval.Text.Int(); }
			set { txtDayInterval.Text = value.ToString(); }
		}

		public int HourInterval
		{
			get { return txtHourInterval.Text.Int(); }
			set { txtHourInterval.Text = value.ToString(); }
		}

		public int MinuteInterval
		{
			get { return txtMinInterval.Text.Int(); }
			set { txtMinInterval.Text = value.ToString(); }
		}

		public DateTime? PerihelionDate
		{
			set { selectDateControlStart.PerihelionDate = selectDateControlEnd.PerihelionDate = value; }
		}

		#endregion

		#region Constructor

		public TimespanControl()
		{
			InitializeComponent();

			txtDayInterval.Tag = new ValNum(0, 3652);
			txtHourInterval.Tag = new ValNum(0, 23);
			txtMinInterval.Tag = new ValNum(0, 59);

			selectDateControlStart.DefaultDateTime = CommonManager.DefaultDateStart;
			selectDateControlEnd.DefaultDateTime = CommonManager.DefaultDateEnd;
		}

		#endregion

		#region +EventHandling

		#region Button

		private void btnDefaultInterval_Click(object sender, EventArgs e)
		{
			this.DayInterval = 1;
			this.HourInterval = this.MinuteInterval = 0;
		}

		#endregion

		#region TextBox

		private void txtIntervalCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtIntervalCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		#endregion

		#endregion

		#region ValidateData

		public void ValidateData()
		{
			if (this.DateEnd <= this.DateStart)
				throw new ValidationException("End date must be greather than start date", selectDateControlStart);

			if ((this.DateEnd - this.DateStart).TotalDays >= 300 * 365.25)
				throw new ValidationException("Timespan must be less than 300 years", selectDateControlStart);
		}

		#endregion
	}
}
