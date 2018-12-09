using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application.Controls.ModulGraph
{
	public partial class ValueRangeControl : UserControl
	{
		#region Properties

		public bool MinValueChecked
		{
			get { return cbxMinValue.Checked; }
			set { cbxMinValue.Checked = value; }
		}

		public double? MinValue
		{
			get { return txtMinValue.TextLength > 0 ? (double?)txtMinValue.Double() : null; }
			set { txtMinValue.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		public bool MaxValueChecked
		{
			get { return cbxMaxValue.Checked; }
			set { cbxMaxValue.Checked = value; }
		}

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
