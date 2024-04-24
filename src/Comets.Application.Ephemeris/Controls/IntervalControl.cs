using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Ephemeris.Controls
{
	public partial class IntervalControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DayInterval
		{
			get { return txtDayInterval.Text.Int(); }
			set { txtDayInterval.Text = value.ToString(); }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int HourInterval
		{
			get { return txtHourInterval.Text.Int(); }
			set { txtHourInterval.Text = value.ToString(); }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MinuteInterval
		{
			get { return txtMinInterval.Text.Int(); }
			set { txtMinInterval.Text = value.ToString(); }
		}

		#endregion

		#region Constructor

		public IntervalControl()
		{
			InitializeComponent();

			txtDayInterval.Tag = new ValNum(0, 3652);
			txtHourInterval.Tag = ValNum.VHour;
			txtMinInterval.Tag = ValNum.VMinute;
		}

		#endregion

		#region +EventHandling

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

		#region Button

		private void btnDefaultInterval_Click(object sender, EventArgs e)
		{
			this.DayInterval = 1;
			this.HourInterval = this.MinuteInterval = 0;
		}

		#endregion

		#endregion
	}
}
