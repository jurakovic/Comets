using System;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class InfoLabelsControl : UserControl
	{
		#region Events

		public event Action<bool> OnShowDistanceLabelChanged;
		public event Action<bool> OnShowDateLabelChanged;

		#endregion

		#region Constructor

		public InfoLabelsControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void cbxMagDist_CheckedChanged(object sender, EventArgs e)
		{
			OnShowDistanceLabelChanged(cbxMagDist.Checked);
		}

		private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
		{
			OnShowDateLabelChanged(cbxDateTime.Checked);
		}

		#endregion
	}
}
