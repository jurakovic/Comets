﻿using Comets.Application.Common.General;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Ephemeris
{
	public partial class RequirementsControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MaxSunDistChecked
		{
			get { return cbxMaxSunDist.Checked; }
			set { cbxMaxSunDist.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? MaxSunDistValue
		{
			get { return txtMaxSunDist.TextLength > 0 ? (double?)txtMaxSunDist.Double() : null; }
			set { txtMaxSunDist.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MaxEarthDistChecked
		{
			get { return cbxMaxEarthDist.Checked; }
			set { cbxMaxEarthDist.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? MaxEarthDistValue
		{
			get { return txtMaxEarthDist.TextLength > 0 ? (double?)txtMaxEarthDist.Double() : null; }
			set { txtMaxEarthDist.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MinMagnitudeChecked
		{
			get { return cbxMinMag.Checked; }
			set { cbxMinMag.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? MinMagnitudeValue
		{
			get { return txtMinMag.TextLength > 0 ? (double?)txtMinMag.Double() : null; }
			set { txtMinMag.Text = value != null ? value.Value.ToString() : String.Empty; }
		}

		#endregion

		#region Constructor

		public RequirementsControl()
		{
			InitializeComponent();

			txtMaxEarthDist.Tag = new ValNum(0, 9.99, 2);
			txtMaxSunDist.Tag = new ValNum(0, 9.99, 2);
			txtMinMag.Tag = ValNum.VMagnitude;
		}

		#endregion

		#region EventHandling

		private void txtMagDistCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void txtMaxSunDist_TextChanged(object sender, EventArgs e)
		{
			cbxMaxSunDist.Checked = txtMaxSunDist.TextLength > 0;
		}

		private void txtMaxEarthDist_TextChanged(object sender, EventArgs e)
		{
			cbxMaxEarthDist.Checked = txtMaxEarthDist.TextLength > 0;
		}

		private void txtMinMag_TextChanged(object sender, EventArgs e)
		{
			cbxMinMag.Checked = txtMinMag.TextLength > 0;
		}

		#endregion

		#region Validate

		public void ValidateData()
		{
			if (this.MaxSunDistChecked && this.MaxSunDistValue == null)
				throw new ValidationException("Please enter Maximum Sun distance value", txtMaxSunDist);

			if (this.MaxEarthDistChecked && this.MaxEarthDistValue == null)
				throw new ValidationException("Please enter Maximum Earth distance value", txtMaxEarthDist);

			if (this.MinMagnitudeChecked && this.MinMagnitudeValue == null)
				throw new ValidationException("Please enter Minimum magnitude value", txtMinMag);
		}

		#endregion
	}
}
