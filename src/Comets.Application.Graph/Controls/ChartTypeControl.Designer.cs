namespace Comets.Application.Graph
{
	partial class ChartTypeControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			gbxChartType = new System.Windows.Forms.GroupBox();
			rbtnEarthDistance = new System.Windows.Forms.RadioButton();
			rbtnSunDistance = new System.Windows.Forms.RadioButton();
			rbtnMagnitude = new System.Windows.Forms.RadioButton();
			gbxChartType.SuspendLayout();
			SuspendLayout();
			// 
			// gbxChartType
			// 
			gbxChartType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gbxChartType.Controls.Add(rbtnEarthDistance);
			gbxChartType.Controls.Add(rbtnSunDistance);
			gbxChartType.Controls.Add(rbtnMagnitude);
			gbxChartType.Location = new System.Drawing.Point(0, 0);
			gbxChartType.Name = "gbxChartType";
			gbxChartType.Size = new System.Drawing.Size(149, 135);
			gbxChartType.TabIndex = 0;
			gbxChartType.TabStop = false;
			gbxChartType.Text = "Chart Type";
			// 
			// rbtnEarthDistance
			// 
			rbtnEarthDistance.AutoSize = true;
			rbtnEarthDistance.Location = new System.Drawing.Point(14, 75);
			rbtnEarthDistance.Name = "rbtnEarthDistance";
			rbtnEarthDistance.Size = new System.Drawing.Size(94, 17);
			rbtnEarthDistance.TabIndex = 2;
			rbtnEarthDistance.Text = "Earth distance";
			rbtnEarthDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnSunDistance
			// 
			rbtnSunDistance.AutoSize = true;
			rbtnSunDistance.Location = new System.Drawing.Point(14, 47);
			rbtnSunDistance.Name = "rbtnSunDistance";
			rbtnSunDistance.Size = new System.Drawing.Size(86, 17);
			rbtnSunDistance.TabIndex = 1;
			rbtnSunDistance.Text = "Sun distance";
			rbtnSunDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnMagnitude
			// 
			rbtnMagnitude.AutoSize = true;
			rbtnMagnitude.Checked = true;
			rbtnMagnitude.Location = new System.Drawing.Point(14, 21);
			rbtnMagnitude.Name = "rbtnMagnitude";
			rbtnMagnitude.Size = new System.Drawing.Size(75, 17);
			rbtnMagnitude.TabIndex = 0;
			rbtnMagnitude.TabStop = true;
			rbtnMagnitude.Text = "Magnitude";
			rbtnMagnitude.UseVisualStyleBackColor = true;
			// 
			// ChartTypeControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(gbxChartType);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "ChartTypeControl";
			Size = new System.Drawing.Size(149, 135);
			gbxChartType.ResumeLayout(false);
			gbxChartType.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxChartType;
		private System.Windows.Forms.RadioButton rbtnEarthDistance;
		private System.Windows.Forms.RadioButton rbtnSunDistance;
		private System.Windows.Forms.RadioButton rbtnMagnitude;
	}
}
