using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application.Controls.ModulGraph
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
			set { selectDateControlStart.PerihelionDate = selectDateControlEnd.PerihelionDate = value; }
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
	}
}
