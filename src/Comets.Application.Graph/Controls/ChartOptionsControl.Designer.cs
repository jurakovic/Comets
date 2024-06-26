﻿namespace Comets.Application.Graph
{
	partial class ChartOptionsControl
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
			this.gbxChartOptions = new System.Windows.Forms.GroupBox();
			this.cbxValue = new System.Windows.Forms.CheckBox();
			this.pnlPerihLineColor = new System.Windows.Forms.Panel();
			this.pnlNowLineColor = new System.Windows.Forms.Panel();
			this.pnlMagnitudeColor = new System.Windows.Forms.Panel();
			this.cbxAntialiasing = new System.Windows.Forms.CheckBox();
			this.cbxNowLine = new System.Windows.Forms.CheckBox();
			this.cbxPerihelionLine = new System.Windows.Forms.CheckBox();
			this.gbxChartOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxChartOptions
			// 
			this.gbxChartOptions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxChartOptions.Controls.Add(this.cbxValue);
			this.gbxChartOptions.Controls.Add(this.pnlPerihLineColor);
			this.gbxChartOptions.Controls.Add(this.pnlNowLineColor);
			this.gbxChartOptions.Controls.Add(this.pnlMagnitudeColor);
			this.gbxChartOptions.Controls.Add(this.cbxAntialiasing);
			this.gbxChartOptions.Controls.Add(this.cbxNowLine);
			this.gbxChartOptions.Controls.Add(this.cbxPerihelionLine);
			this.gbxChartOptions.Location = new System.Drawing.Point(0, 0);
			this.gbxChartOptions.Name = "gbxChartOptions";
			this.gbxChartOptions.Size = new System.Drawing.Size(189, 135);
			this.gbxChartOptions.TabIndex = 0;
			this.gbxChartOptions.TabStop = false;
			this.gbxChartOptions.Text = "Chart options";
			// 
			// cbxValue
			// 
			this.cbxValue.AutoSize = true;
			this.cbxValue.Checked = true;
			this.cbxValue.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxValue.Enabled = false;
			this.cbxValue.Location = new System.Drawing.Point(14, 21);
			this.cbxValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbxValue.Name = "cbxValue";
			this.cbxValue.Size = new System.Drawing.Size(52, 17);
			this.cbxValue.TabIndex = 0;
			this.cbxValue.Text = "Value";
			this.cbxValue.UseVisualStyleBackColor = true;
			// 
			// pnlPerihLineColor
			// 
			this.pnlPerihLineColor.BackColor = System.Drawing.Color.RoyalBlue;
			this.pnlPerihLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlPerihLineColor.Location = new System.Drawing.Point(136, 72);
			this.pnlPerihLineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlPerihLineColor.Name = "pnlPerihLineColor";
			this.pnlPerihLineColor.Size = new System.Drawing.Size(25, 20);
			this.pnlPerihLineColor.TabIndex = 6;
			this.pnlPerihLineColor.TabStop = true;
			this.pnlPerihLineColor.Click += this.pnlColorCommon_Click;
			// 
			// pnlNowLineColor
			// 
			this.pnlNowLineColor.BackColor = System.Drawing.Color.LimeGreen;
			this.pnlNowLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlNowLineColor.Location = new System.Drawing.Point(136, 45);
			this.pnlNowLineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlNowLineColor.Name = "pnlNowLineColor";
			this.pnlNowLineColor.Size = new System.Drawing.Size(25, 20);
			this.pnlNowLineColor.TabIndex = 4;
			this.pnlNowLineColor.TabStop = true;
			this.pnlNowLineColor.Click += this.pnlColorCommon_Click;
			// 
			// pnlMagnitudeColor
			// 
			this.pnlMagnitudeColor.BackColor = System.Drawing.Color.Red;
			this.pnlMagnitudeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlMagnitudeColor.Location = new System.Drawing.Point(136, 19);
			this.pnlMagnitudeColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlMagnitudeColor.Name = "pnlMagnitudeColor";
			this.pnlMagnitudeColor.Size = new System.Drawing.Size(25, 20);
			this.pnlMagnitudeColor.TabIndex = 2;
			this.pnlMagnitudeColor.TabStop = true;
			this.pnlMagnitudeColor.Click += this.pnlColorCommon_Click;
			// 
			// cbxAntialiasing
			// 
			this.cbxAntialiasing.AutoSize = true;
			this.cbxAntialiasing.Location = new System.Drawing.Point(14, 102);
			this.cbxAntialiasing.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbxAntialiasing.Name = "cbxAntialiasing";
			this.cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			this.cbxAntialiasing.TabIndex = 7;
			this.cbxAntialiasing.Text = "Antialiasing";
			this.cbxAntialiasing.UseVisualStyleBackColor = true;
			// 
			// cbxNowLine
			// 
			this.cbxNowLine.AutoSize = true;
			this.cbxNowLine.Checked = true;
			this.cbxNowLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxNowLine.Location = new System.Drawing.Point(14, 48);
			this.cbxNowLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbxNowLine.Name = "cbxNowLine";
			this.cbxNowLine.Size = new System.Drawing.Size(66, 17);
			this.cbxNowLine.TabIndex = 3;
			this.cbxNowLine.Text = "Now line";
			this.cbxNowLine.UseVisualStyleBackColor = true;
			// 
			// cbxPerihelionLine
			// 
			this.cbxPerihelionLine.AutoSize = true;
			this.cbxPerihelionLine.Checked = true;
			this.cbxPerihelionLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxPerihelionLine.Location = new System.Drawing.Point(14, 75);
			this.cbxPerihelionLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbxPerihelionLine.Name = "cbxPerihelionLine";
			this.cbxPerihelionLine.Size = new System.Drawing.Size(91, 17);
			this.cbxPerihelionLine.TabIndex = 5;
			this.cbxPerihelionLine.Text = "Perihelion line";
			this.cbxPerihelionLine.UseVisualStyleBackColor = true;
			// 
			// ChartOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxChartOptions);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "ChartOptionsControl";
			this.Size = new System.Drawing.Size(189, 135);
			this.gbxChartOptions.ResumeLayout(false);
			this.gbxChartOptions.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxChartOptions;
		private System.Windows.Forms.CheckBox cbxValue;
		private System.Windows.Forms.Panel pnlPerihLineColor;
		private System.Windows.Forms.Panel pnlNowLineColor;
		private System.Windows.Forms.Panel pnlMagnitudeColor;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
		private System.Windows.Forms.CheckBox cbxNowLine;
		private System.Windows.Forms.CheckBox cbxPerihelionLine;
	}
}
