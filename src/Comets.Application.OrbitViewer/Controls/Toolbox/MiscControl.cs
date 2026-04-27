using Comets.Core;
using Comets.Core.Managers;
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

			txtGridExtent.Tag = new ValNum(0.0, 150, 0);
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
			OnShowGridChanged(cbxShowGrid.Checked);
		}

		private void txtGridExtent_TextChanged(object sender, EventArgs e)
		{
			if (ApplyGridExtent() && !cbxShowGrid.Checked)
				cbxShowGrid.Checked = true;
		}

		private void txtGridExtent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				ApplyGridExtent();
				e.SuppressKeyPress = true;
			}
		}

		private void txtGridExtent_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtGridExtent_Leave(object sender, EventArgs e)
		{
			ApplyGridExtent();
		}

		private bool ApplyGridExtent()
		{
			if (double.TryParse(txtGridExtent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) && v > 0)
			{
				OnGridExtentChanged?.Invoke(v);
				return true;
			}
			return false;
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
