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
		public event Action<bool> OnShowAxesLabelsChanged;
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

			SyncShowAxesLabelsEnabled();
		}

		#endregion

		#region EventHandling

		private void cbxShowAxes_CheckedChanged(object sender, EventArgs e)
		{
			OnShowAxesChanged(cbxShowAxes.Checked);
			SyncShowAxesLabelsEnabled();
		}

		private void cbxShowAxesLabels_CheckedChanged(object sender, EventArgs e)
		{
			OnShowAxesLabelsChanged?.Invoke(cbxShowAxesLabels.Checked);
		}

		/// <summary>
		/// The labels are drawn at the ends of the axis lines, so the option is only
		/// meaningful while the axes themselves are shown.
		/// </summary>
		private void SyncShowAxesLabelsEnabled()
		{
			cbxShowAxesLabels.Enabled = cbxShowAxes.Checked;
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
		/// Called on Enter and on leaving the textbox, never on every keystroke.
		/// <para>
		/// See docs/05a-ecliptic-grid-implementation.md for why the value is clamped and
		/// why an unchanged value must raise nothing.
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
