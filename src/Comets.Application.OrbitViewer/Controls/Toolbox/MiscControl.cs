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

		#region Fields

		/// <summary>
		/// Extent last handed to the panel, so that leaving the textbox can tell a real
		/// edit from having merely passed through it. Seeded from the textbox itself,
		/// whose designer value matches the panel's own default.
		/// </summary>
		private double _appliedGridExtent;

		#endregion

		#region Constructor

		public MiscControl()
		{
			InitializeComponent();

			txtGridExtent.Tag = new ValNum(0.0, MaxGridExtent, 0);
			double.TryParse(txtGridExtent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _appliedGridExtent);
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
		/// Parses the grid extent textbox and, when the extent has actually changed,
		/// raises <see cref="OnGridExtentChanged"/> and switches the grid on.
		/// <para>
		/// Called on Enter and on leaving the textbox rather than on every keystroke, so
		/// typing "100" applies once instead of rendering at 1, then 10, then 100. The
		/// value is clamped to <see cref="MaxGridExtent"/>: ValNumManager rejects a typed
		/// character that would exceed the maximum, but a paste bypasses that filter, and
		/// an unbounded extent costs thousands of grid line uploads per frame.
		/// </para>
		/// <para>
		/// Nothing is raised when the value is unchanged. Leaving the textbox applies it,
		/// and tabbing through the toolbox leaves every control in turn, so re-applying
		/// regardless would tick Show grid on the way past without anything being typed.
		/// </para>
		/// </summary>
		private void ApplyGridExtent()
		{
			if (!double.TryParse(txtGridExtent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) || v <= 0)
				return;

			v = Math.Min(v, MaxGridExtent);
			txtGridExtent.Text = v.ToString("G", CultureInfo.InvariantCulture);

			if (v == _appliedGridExtent)
				return;

			_appliedGridExtent = v;
			OnGridExtentChanged?.Invoke(v);

			if (!cbxShowGrid.Checked)
				cbxShowGrid.Checked = true;
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			OnSaveImage();
		}

		#endregion
	}
}
