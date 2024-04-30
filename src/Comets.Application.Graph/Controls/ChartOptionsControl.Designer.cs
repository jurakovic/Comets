namespace Comets.Application.Graph
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
			gbxChartOptions = new System.Windows.Forms.GroupBox();
			lblValue = new System.Windows.Forms.Label();
			cbxValue = new System.Windows.Forms.CheckBox();
			pnlPerihLineColor = new System.Windows.Forms.Panel();
			pnlNowLineColor = new System.Windows.Forms.Panel();
			pnlMagnitudeColor = new System.Windows.Forms.Panel();
			cbxAntialiasing = new System.Windows.Forms.CheckBox();
			cbxNowLine = new System.Windows.Forms.CheckBox();
			cbxPerihelionLine = new System.Windows.Forms.CheckBox();
			gbxChartOptions.SuspendLayout();
			SuspendLayout();
			// 
			// gbxChartOptions
			// 
			gbxChartOptions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gbxChartOptions.Controls.Add(lblValue);
			gbxChartOptions.Controls.Add(cbxValue);
			gbxChartOptions.Controls.Add(pnlPerihLineColor);
			gbxChartOptions.Controls.Add(pnlNowLineColor);
			gbxChartOptions.Controls.Add(pnlMagnitudeColor);
			gbxChartOptions.Controls.Add(cbxAntialiasing);
			gbxChartOptions.Controls.Add(cbxNowLine);
			gbxChartOptions.Controls.Add(cbxPerihelionLine);
			gbxChartOptions.Location = new System.Drawing.Point(0, 0);
			gbxChartOptions.Name = "gbxChartOptions";
			gbxChartOptions.Size = new System.Drawing.Size(189, 135);
			gbxChartOptions.TabIndex = 0;
			gbxChartOptions.TabStop = false;
			gbxChartOptions.Text = "Chart options";
			// 
			// lblValue
			// 
			lblValue.AutoSize = true;
			lblValue.Location = new System.Drawing.Point(30, 22);
			lblValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblValue.Name = "lblValue";
			lblValue.Size = new System.Drawing.Size(33, 13);
			lblValue.TabIndex = 1;
			lblValue.Text = "Value";
			// 
			// cbxValue
			// 
			cbxValue.AutoSize = true;
			cbxValue.Checked = true;
			cbxValue.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxValue.Enabled = false;
			cbxValue.Location = new System.Drawing.Point(14, 21);
			cbxValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cbxValue.Name = "cbxValue";
			cbxValue.Size = new System.Drawing.Size(29, 17);
			cbxValue.TabIndex = 0;
			cbxValue.Text = " ";
			cbxValue.UseVisualStyleBackColor = true;
			// 
			// pnlPerihLineColor
			// 
			pnlPerihLineColor.BackColor = System.Drawing.Color.RoyalBlue;
			pnlPerihLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pnlPerihLineColor.Location = new System.Drawing.Point(136, 72);
			pnlPerihLineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pnlPerihLineColor.Name = "pnlPerihLineColor";
			pnlPerihLineColor.Size = new System.Drawing.Size(25, 20);
			pnlPerihLineColor.TabIndex = 6;
			pnlPerihLineColor.TabStop = true;
			pnlPerihLineColor.Click += pnlColorCommon_Click;
			// 
			// pnlNowLineColor
			// 
			pnlNowLineColor.BackColor = System.Drawing.Color.LimeGreen;
			pnlNowLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pnlNowLineColor.Location = new System.Drawing.Point(136, 45);
			pnlNowLineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pnlNowLineColor.Name = "pnlNowLineColor";
			pnlNowLineColor.Size = new System.Drawing.Size(25, 20);
			pnlNowLineColor.TabIndex = 4;
			pnlNowLineColor.TabStop = true;
			pnlNowLineColor.Click += pnlColorCommon_Click;
			// 
			// pnlMagnitudeColor
			// 
			pnlMagnitudeColor.BackColor = System.Drawing.Color.Red;
			pnlMagnitudeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pnlMagnitudeColor.Location = new System.Drawing.Point(136, 19);
			pnlMagnitudeColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pnlMagnitudeColor.Name = "pnlMagnitudeColor";
			pnlMagnitudeColor.Size = new System.Drawing.Size(25, 20);
			pnlMagnitudeColor.TabIndex = 2;
			pnlMagnitudeColor.TabStop = true;
			pnlMagnitudeColor.Click += pnlColorCommon_Click;
			// 
			// cbxAntialiasing
			// 
			cbxAntialiasing.AutoSize = true;
			cbxAntialiasing.Location = new System.Drawing.Point(14, 102);
			cbxAntialiasing.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cbxAntialiasing.Name = "cbxAntialiasing";
			cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			cbxAntialiasing.TabIndex = 7;
			cbxAntialiasing.Text = "Antialiasing";
			cbxAntialiasing.UseVisualStyleBackColor = true;
			// 
			// cbxNowLine
			// 
			cbxNowLine.AutoSize = true;
			cbxNowLine.Checked = true;
			cbxNowLine.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxNowLine.Location = new System.Drawing.Point(14, 48);
			cbxNowLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cbxNowLine.Name = "cbxNowLine";
			cbxNowLine.Size = new System.Drawing.Size(66, 17);
			cbxNowLine.TabIndex = 3;
			cbxNowLine.Text = "Now line";
			cbxNowLine.UseVisualStyleBackColor = true;
			// 
			// cbxPerihelionLine
			// 
			cbxPerihelionLine.AutoSize = true;
			cbxPerihelionLine.Checked = true;
			cbxPerihelionLine.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxPerihelionLine.Location = new System.Drawing.Point(14, 75);
			cbxPerihelionLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cbxPerihelionLine.Name = "cbxPerihelionLine";
			cbxPerihelionLine.Size = new System.Drawing.Size(91, 17);
			cbxPerihelionLine.TabIndex = 5;
			cbxPerihelionLine.Text = "Perihelion line";
			cbxPerihelionLine.UseVisualStyleBackColor = true;
			// 
			// ChartOptionsControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(gbxChartOptions);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "ChartOptionsControl";
			Size = new System.Drawing.Size(189, 135);
			gbxChartOptions.ResumeLayout(false);
			gbxChartOptions.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxChartOptions;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.CheckBox cbxValue;
		private System.Windows.Forms.Panel pnlPerihLineColor;
		private System.Windows.Forms.Panel pnlNowLineColor;
		private System.Windows.Forms.Panel pnlMagnitudeColor;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
		private System.Windows.Forms.CheckBox cbxNowLine;
		private System.Windows.Forms.CheckBox cbxPerihelionLine;
	}
}
