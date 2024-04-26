namespace Comets.Application.Ephemeris.Controls
{
	partial class IntervalControl
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
			gbxTimestamp = new GroupBox();
			lblM = new Label();
			lblH = new Label();
			lblD = new Label();
			txtMinInterval = new TextBox();
			txtHourInterval = new TextBox();
			txtDayInterval = new TextBox();
			btnDefaultInterval = new Button();
			gbxTimestamp.SuspendLayout();
			SuspendLayout();
			// 
			// gbxTimestamp
			// 
			gbxTimestamp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxTimestamp.Controls.Add(lblM);
			gbxTimestamp.Controls.Add(lblH);
			gbxTimestamp.Controls.Add(lblD);
			gbxTimestamp.Controls.Add(txtMinInterval);
			gbxTimestamp.Controls.Add(txtHourInterval);
			gbxTimestamp.Controls.Add(txtDayInterval);
			gbxTimestamp.Controls.Add(btnDefaultInterval);
			gbxTimestamp.Location = new Point(0, 0);
			gbxTimestamp.Name = "gbxTimestamp";
			gbxTimestamp.Size = new Size(204, 85);
			gbxTimestamp.TabIndex = 0;
			gbxTimestamp.TabStop = false;
			gbxTimestamp.Text = "Interval";
			// 
			// lblM
			// 
			lblM.AutoSize = true;
			lblM.ForeColor = SystemColors.GrayText;
			lblM.Location = new Point(106, 32);
			lblM.Name = "lblM";
			lblM.Size = new Size(15, 13);
			lblM.TabIndex = 2;
			lblM.Text = "M";
			// 
			// lblH
			// 
			lblH.AutoSize = true;
			lblH.ForeColor = SystemColors.GrayText;
			lblH.Location = new Point(58, 32);
			lblH.Name = "lblH";
			lblH.Size = new Size(14, 13);
			lblH.TabIndex = 1;
			lblH.Text = "H";
			// 
			// lblD
			// 
			lblD.AutoSize = true;
			lblD.ForeColor = SystemColors.GrayText;
			lblD.Location = new Point(10, 32);
			lblD.Name = "lblD";
			lblD.Size = new Size(14, 13);
			lblD.TabIndex = 0;
			lblD.Text = "D";
			// 
			// txtMinInterval
			// 
			txtMinInterval.Location = new Point(108, 48);
			txtMinInterval.Name = "txtMinInterval";
			txtMinInterval.Size = new Size(42, 21);
			txtMinInterval.TabIndex = 5;
			txtMinInterval.TextAlign = HorizontalAlignment.Right;
			txtMinInterval.KeyDown += txtIntervalCommon_KeyDown;
			txtMinInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// txtHourInterval
			// 
			txtHourInterval.Location = new Point(60, 48);
			txtHourInterval.Name = "txtHourInterval";
			txtHourInterval.Size = new Size(42, 21);
			txtHourInterval.TabIndex = 4;
			txtHourInterval.TextAlign = HorizontalAlignment.Right;
			txtHourInterval.KeyDown += txtIntervalCommon_KeyDown;
			txtHourInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// txtDayInterval
			// 
			txtDayInterval.Location = new Point(12, 48);
			txtDayInterval.Name = "txtDayInterval";
			txtDayInterval.Size = new Size(42, 21);
			txtDayInterval.TabIndex = 3;
			txtDayInterval.TextAlign = HorizontalAlignment.Right;
			txtDayInterval.KeyDown += txtIntervalCommon_KeyDown;
			txtDayInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// btnDefaultInterval
			// 
			btnDefaultInterval.Location = new Point(156, 51);
			btnDefaultInterval.Name = "btnDefaultInterval";
			btnDefaultInterval.Size = new Size(16, 16);
			btnDefaultInterval.TabIndex = 6;
			btnDefaultInterval.UseVisualStyleBackColor = true;
			btnDefaultInterval.Click += btnDefaultInterval_Click;
			// 
			// IntervalControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxTimestamp);
			Font = new Font("Tahoma", 8.25F);
			Name = "IntervalControl";
			Size = new Size(204, 85);
			gbxTimestamp.ResumeLayout(false);
			gbxTimestamp.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxTimestamp;
		private System.Windows.Forms.TextBox txtMinInterval;
		private System.Windows.Forms.TextBox txtHourInterval;
		private System.Windows.Forms.TextBox txtDayInterval;
		private System.Windows.Forms.Button btnDefaultInterval;
		private System.Windows.Forms.Label lblM;
		private System.Windows.Forms.Label lblH;
		private System.Windows.Forms.Label lblD;
	}
}
