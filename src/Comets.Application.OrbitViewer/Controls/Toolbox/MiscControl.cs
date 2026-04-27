using System;
using System.Globalization;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class MiscControl : UserControl
	{
		#region Events

		public event Action<bool> OnShowAxesChanged;
		public event Action<bool> OnShowGridChanged;
		public event Action<double> OnGridExtentChanged;
		public event Action<bool> OnAntialiasingChanged;
		public event Action OnSaveImage;

		#endregion

		#region Constructor

		public MiscControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Public

		public void SetGridExtent(double extent)
		{
			txtGridExtent.Text = extent.ToString("G", CultureInfo.InvariantCulture);
		}

		#endregion

		#region EventHandling

		private void cbxShowAxes_CheckedChanged(object sender, EventArgs e)
		{
			OnShowAxesChanged(cbxShowAxes.Checked);
		}

		private void cbxShowGrid_CheckedChanged(object sender, EventArgs e)
		{
			txtGridExtent.Enabled = cbxShowGrid.Checked;
			OnShowGridChanged(cbxShowGrid.Checked);
		}

		private void txtGridExtent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				ApplyGridExtent();
				e.SuppressKeyPress = true;
			}
		}

		private void txtGridExtent_Leave(object sender, EventArgs e)
		{
			ApplyGridExtent();
		}

		private void ApplyGridExtent()
		{
			if (double.TryParse(txtGridExtent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) && v > 0)
				OnGridExtentChanged?.Invoke(v);
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
