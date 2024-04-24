using Comets.Application.Common.General;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Graph
{
	public partial class ValueRangeControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MinValueChecked
		{
			get { return cbxMinValue.Checked; }
			set { cbxMinValue.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? MinValue
		{
			get { return txtMinValue.TextLength > 0 ? (double?)txtMinValue.Double() : null; }
			set { txtMinValue.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MaxValueChecked
		{
			get { return cbxMaxValue.Checked; }
			set { cbxMaxValue.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? MaxValue
		{
			get { return txtMaxValue.TextLength > 0 ? (double?)txtMaxValue.Double() : null; }
			set { txtMaxValue.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		#endregion

		#region Constructor

		public ValueRangeControl()
		{
			InitializeComponent();

			txtMinValue.Tag = ValNum.VMagnitude;
			txtMaxValue.Tag = ValNum.VMagnitude;
		}

		#endregion

		#region EventHandling

		private void txtMagCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtMinMag_TextChanged(object sender, EventArgs e)
		{
			cbxMinValue.Checked = txtMinValue.TextLength > 0;
		}

		private void txtMaxMag_TextChanged(object sender, EventArgs e)
		{
			cbxMaxValue.Checked = txtMaxValue.TextLength > 0;
		}

		#endregion

		#region ValidateData

		public void ValidateData()
		{
			if (this.MinValueChecked && this.MinValue == null)
				throw new ValidationException("Enter Minimum value", txtMinValue);

			if (this.MaxValueChecked && this.MaxValue == null)
				throw new ValidationException("Enter Maximum value", txtMaxValue);

			if (this.MinValueChecked && this.MaxValueChecked && this.MinValue.GetValueOrDefault() >= this.MaxValue.GetValueOrDefault())
				throw new ValidationException("Minimum value must be lower than Maximum value", txtMaxValue);
		}

		#endregion
	}
}
