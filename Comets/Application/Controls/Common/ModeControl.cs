using System.Windows.Forms;

namespace Comets.Application.Controls.Common
{
	public partial class ModeControl : UserControl
	{
		#region Properties

		public bool IsMultipleMode
		{
			get
			{
				return this.rbtnMultiple.Checked;
			}
			set
			{
				this.rbtnSingle.Checked = !value;
				this.rbtnMultiple.Checked = value;
			}
		}

		public int CometCount
		{
			set { this.lblCometCount.Text = value + " comets"; }
		}

		#endregion

		#region Constructor

		public ModeControl()
		{
			InitializeComponent();
		}

		#endregion
	}
}
