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
			gbxChartOptions = new GroupBox();
			lblValue = new Label();
			cbxValue = new CheckBox();
			pnlPerihLineColor = new Panel();
			pnlNowLineColor = new Panel();
			pnlMagnitudeColor = new Panel();
			cbxAntialiasing = new CheckBox();
			cbxNowLine = new CheckBox();
			cbxPerihelionLine = new CheckBox();
			gbxChartOptions.SuspendLayout();
			SuspendLayout();
			// 
			// gbxChartOptions
			// 
			gbxChartOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxChartOptions.Controls.Add(lblValue);
			gbxChartOptions.Controls.Add(cbxValue);
			gbxChartOptions.Controls.Add(pnlPerihLineColor);
			gbxChartOptions.Controls.Add(pnlNowLineColor);
			gbxChartOptions.Controls.Add(pnlMagnitudeColor);
			gbxChartOptions.Controls.Add(cbxAntialiasing);
			gbxChartOptions.Controls.Add(cbxNowLine);
			gbxChartOptions.Controls.Add(cbxPerihelionLine);
			gbxChartOptions.Location = new Point(0, 0);
			gbxChartOptions.Name = "gbxChartOptions";
			gbxChartOptions.Size = new Size(189, 135);
			gbxChartOptions.TabIndex = 0;
			gbxChartOptions.TabStop = false;
			gbxChartOptions.Text = "Chart options";
			// 
			// lblValue
			// 
			lblValue.AutoSize = true;
			lblValue.Location = new Point(30, 22);
			lblValue.Margin = new Padding(4, 0, 4, 0);
			lblValue.Name = "lblValue";
			lblValue.Size = new Size(33, 13);
			lblValue.TabIndex = 1;
			lblValue.Text = "Value";
			// 
			// cbxValue
			// 
			cbxValue.AutoSize = true;
			cbxValue.Checked = true;
			cbxValue.CheckState = CheckState.Checked;
			cbxValue.Enabled = false;
			cbxValue.Location = new Point(14, 21);
			cbxValue.Margin = new Padding(4, 3, 4, 3);
			cbxValue.Name = "cbxValue";
			cbxValue.Size = new Size(29, 17);
			cbxValue.TabIndex = 0;
			cbxValue.Text = " ";
			cbxValue.UseVisualStyleBackColor = true;
			// 
			// pnlPerihLineColor
			// 
			pnlPerihLineColor.BackColor = Color.RoyalBlue;
			pnlPerihLineColor.BorderStyle = BorderStyle.Fixed3D;
			pnlPerihLineColor.Location = new Point(136, 72);
			pnlPerihLineColor.Margin = new Padding(4, 3, 4, 3);
			pnlPerihLineColor.Name = "pnlPerihLineColor";
			pnlPerihLineColor.Size = new Size(25, 20);
			pnlPerihLineColor.TabIndex = 6;
			pnlPerihLineColor.TabStop = true;
			pnlPerihLineColor.Click += pnlColorCommon_Click;
			// 
			// pnlNowLineColor
			// 
			pnlNowLineColor.BackColor = Color.LimeGreen;
			pnlNowLineColor.BorderStyle = BorderStyle.Fixed3D;
			pnlNowLineColor.Location = new Point(136, 45);
			pnlNowLineColor.Margin = new Padding(4, 3, 4, 3);
			pnlNowLineColor.Name = "pnlNowLineColor";
			pnlNowLineColor.Size = new Size(25, 20);
			pnlNowLineColor.TabIndex = 4;
			pnlNowLineColor.TabStop = true;
			pnlNowLineColor.Click += pnlColorCommon_Click;
			// 
			// pnlMagnitudeColor
			// 
			pnlMagnitudeColor.BackColor = Color.Red;
			pnlMagnitudeColor.BorderStyle = BorderStyle.Fixed3D;
			pnlMagnitudeColor.Location = new Point(136, 19);
			pnlMagnitudeColor.Margin = new Padding(4, 3, 4, 3);
			pnlMagnitudeColor.Name = "pnlMagnitudeColor";
			pnlMagnitudeColor.Size = new Size(25, 20);
			pnlMagnitudeColor.TabIndex = 2;
			pnlMagnitudeColor.TabStop = true;
			pnlMagnitudeColor.Click += pnlColorCommon_Click;
			// 
			// cbxAntialiasing
			// 
			cbxAntialiasing.AutoSize = true;
			cbxAntialiasing.Location = new Point(14, 102);
			cbxAntialiasing.Margin = new Padding(4, 3, 4, 3);
			cbxAntialiasing.Name = "cbxAntialiasing";
			cbxAntialiasing.Size = new Size(80, 17);
			cbxAntialiasing.TabIndex = 7;
			cbxAntialiasing.Text = "Antialiasing";
			cbxAntialiasing.UseVisualStyleBackColor = true;
			// 
			// cbxNowLine
			// 
			cbxNowLine.AutoSize = true;
			cbxNowLine.Checked = true;
			cbxNowLine.CheckState = CheckState.Checked;
			cbxNowLine.Location = new Point(14, 48);
			cbxNowLine.Margin = new Padding(4, 3, 4, 3);
			cbxNowLine.Name = "cbxNowLine";
			cbxNowLine.Size = new Size(66, 17);
			cbxNowLine.TabIndex = 3;
			cbxNowLine.Text = "Now line";
			cbxNowLine.UseVisualStyleBackColor = true;
			// 
			// cbxPerihelionLine
			// 
			cbxPerihelionLine.AutoSize = true;
			cbxPerihelionLine.Checked = true;
			cbxPerihelionLine.CheckState = CheckState.Checked;
			cbxPerihelionLine.Location = new Point(14, 75);
			cbxPerihelionLine.Margin = new Padding(4, 3, 4, 3);
			cbxPerihelionLine.Name = "cbxPerihelionLine";
			cbxPerihelionLine.Size = new Size(91, 17);
			cbxPerihelionLine.TabIndex = 5;
			cbxPerihelionLine.Text = "Perihelion line";
			cbxPerihelionLine.UseVisualStyleBackColor = true;
			// 
			// ChartOptionsControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxChartOptions);
			Font = new Font("Tahoma", 8.25F);
			Margin = new Padding(4, 3, 4, 3);
			Name = "ChartOptionsControl";
			Size = new Size(189, 135);
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
