using System;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class MiscControl : UserControl
	{
		#region Events

		public event Action<bool> OnShowAxesChanged;
		public event Action<bool> OnAntialiasingChanged;
		public event Action OnSaveImage;

		#endregion

		#region Constructor

		public MiscControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void cbxShowAxes_CheckedChanged(object sender, EventArgs e)
		{
			OnShowAxesChanged(cbxShowAxes.Checked);
		}

		private void cbxAntialiasing_CheckedChanged(object sender, EventArgs e)
		{
			OnAntialiasingChanged(cbxAntialiasing.Checked);
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			OnSaveImage();
		}

		#endregion
	}
}
