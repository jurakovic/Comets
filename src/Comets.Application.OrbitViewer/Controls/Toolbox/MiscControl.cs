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
		public event Action OnSaveImage;

		#endregion

		#region Const

		private const double MaxGridExtent = 150.0;

		#endregion

		#region Constructor

		public MiscControl()
		{
			InitializeComponent();

			txtGridExtent.Tag = new ValNum(0.0, MaxGridExtent, 0);
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

		/// <summary>
		/// Parses the grid extent textbox and raises <see cref="OnGridExtentChanged"/>.
		/// <para>
		/// Called on Enter and on leaving the textbox rather than on every keystroke, so
		/// typing "100" applies once instead of rendering at 1, then 10, then 100. The
		/// value is clamped to <see cref="MaxGridExtent"/>: ValNumManager rejects a typed
		/// character that would exceed the maximum, but a paste bypasses that filter, and
		/// an unbounded extent costs thousands of grid line uploads per frame.
		/// </para>
		/// </summary>
		/// <returns>True when a usable extent was parsed and applied.</returns>
		private bool ApplyGridExtent()
		{
			if (double.TryParse(txtGridExtent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) && v > 0)
			{
				v = Math.Min(v, MaxGridExtent);
				txtGridExtent.Text = v.ToString("G", CultureInfo.InvariantCulture);

				OnGridExtentChanged?.Invoke(v);

				if (!cbxShowGrid.Checked)
					cbxShowGrid.Checked = true;

				return true;
			}
			return false;
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			OnSaveImage();
		}

		#endregion
	}
}
