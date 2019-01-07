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
			this.gbxChartType = new System.Windows.Forms.GroupBox();
			this.rbtnEarthDistance = new System.Windows.Forms.RadioButton();
			this.rbtnSunDistance = new System.Windows.Forms.RadioButton();
			this.rbtnMagnitude = new System.Windows.Forms.RadioButton();
			this.gbxChartType.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxChartType
			// 
			this.gbxChartType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxChartType.Controls.Add(this.rbtnEarthDistance);
			this.gbxChartType.Controls.Add(this.rbtnSunDistance);
			this.gbxChartType.Controls.Add(this.rbtnMagnitude);
			this.gbxChartType.Location = new System.Drawing.Point(0, 0);
			this.gbxChartType.Name = "gbxChartType";
			this.gbxChartType.Size = new System.Drawing.Size(149, 135);
			this.gbxChartType.TabIndex = 0;
			this.gbxChartType.TabStop = false;
			this.gbxChartType.Text = "Chart Type";
			// 
			// rbtnEarthDistance
			// 
			this.rbtnEarthDistance.AutoSize = true;
			this.rbtnEarthDistance.Location = new System.Drawing.Point(14, 75);
			this.rbtnEarthDistance.Name = "rbtnEarthDistance";
			this.rbtnEarthDistance.Size = new System.Drawing.Size(94, 17);
			this.rbtnEarthDistance.TabIndex = 2;
			this.rbtnEarthDistance.Text = "Earth distance";
			this.rbtnEarthDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnSunDistance
			// 
			this.rbtnSunDistance.AutoSize = true;
			this.rbtnSunDistance.Location = new System.Drawing.Point(14, 47);
			this.rbtnSunDistance.Name = "rbtnSunDistance";
			this.rbtnSunDistance.Size = new System.Drawing.Size(86, 17);
			this.rbtnSunDistance.TabIndex = 1;
			this.rbtnSunDistance.Text = "Sun distance";
			this.rbtnSunDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnMagnitude
			// 
			this.rbtnMagnitude.AutoSize = true;
			this.rbtnMagnitude.Checked = true;
			this.rbtnMagnitude.Location = new System.Drawing.Point(14, 21);
			this.rbtnMagnitude.Name = "rbtnMagnitude";
			this.rbtnMagnitude.Size = new System.Drawing.Size(75, 17);
			this.rbtnMagnitude.TabIndex = 0;
			this.rbtnMagnitude.TabStop = true;
			this.rbtnMagnitude.Text = "Magnitude";
			this.rbtnMagnitude.UseVisualStyleBackColor = true;
			// 
			// ChartTypeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxChartType);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "ChartTypeControl";
			this.Size = new System.Drawing.Size(149, 135);
			this.gbxChartType.ResumeLayout(false);
			this.gbxChartType.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxChartType;
		private System.Windows.Forms.RadioButton rbtnEarthDistance;
		private System.Windows.Forms.RadioButton rbtnSunDistance;
		private System.Windows.Forms.RadioButton rbtnMagnitude;
	}
}
