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
			this.gbxTimestamp = new System.Windows.Forms.GroupBox();
			this.lblM = new System.Windows.Forms.Label();
			this.lblH = new System.Windows.Forms.Label();
			this.lblD = new System.Windows.Forms.Label();
			this.txtMinInterval = new System.Windows.Forms.TextBox();
			this.txtHourInterval = new System.Windows.Forms.TextBox();
			this.txtDayInterval = new System.Windows.Forms.TextBox();
			this.btnDefaultInterval = new System.Windows.Forms.Button();
			this.gbxTimestamp.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxTimestamp
			// 
			this.gbxTimestamp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxTimestamp.Controls.Add(this.lblM);
			this.gbxTimestamp.Controls.Add(this.lblH);
			this.gbxTimestamp.Controls.Add(this.lblD);
			this.gbxTimestamp.Controls.Add(this.txtMinInterval);
			this.gbxTimestamp.Controls.Add(this.txtHourInterval);
			this.gbxTimestamp.Controls.Add(this.txtDayInterval);
			this.gbxTimestamp.Controls.Add(this.btnDefaultInterval);
			this.gbxTimestamp.Location = new System.Drawing.Point(0, 0);
			this.gbxTimestamp.Name = "gbxTimestamp";
			this.gbxTimestamp.Size = new System.Drawing.Size(204, 85);
			this.gbxTimestamp.TabIndex = 0;
			this.gbxTimestamp.TabStop = false;
			this.gbxTimestamp.Text = "Interval";
			// 
			// lblM
			// 
			this.lblM.AutoSize = true;
			this.lblM.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblM.Location = new System.Drawing.Point(106, 32);
			this.lblM.Name = "lblM";
			this.lblM.Size = new System.Drawing.Size(15, 13);
			this.lblM.TabIndex = 2;
			this.lblM.Text = "M";
			// 
			// lblH
			// 
			this.lblH.AutoSize = true;
			this.lblH.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblH.Location = new System.Drawing.Point(58, 32);
			this.lblH.Name = "lblH";
			this.lblH.Size = new System.Drawing.Size(14, 13);
			this.lblH.TabIndex = 1;
			this.lblH.Text = "H";
			// 
			// lblD
			// 
			this.lblD.AutoSize = true;
			this.lblD.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblD.Location = new System.Drawing.Point(10, 32);
			this.lblD.Name = "lblD";
			this.lblD.Size = new System.Drawing.Size(14, 13);
			this.lblD.TabIndex = 0;
			this.lblD.Text = "D";
			// 
			// txtMinInterval
			// 
			this.txtMinInterval.Location = new System.Drawing.Point(108, 48);
			this.txtMinInterval.Name = "txtMinInterval";
			this.txtMinInterval.Size = new System.Drawing.Size(42, 21);
			this.txtMinInterval.TabIndex = 5;
			this.txtMinInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinInterval.KeyDown += txtIntervalCommon_KeyDown;
			this.txtMinInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// txtHourInterval
			// 
			this.txtHourInterval.Location = new System.Drawing.Point(60, 48);
			this.txtHourInterval.Name = "txtHourInterval";
			this.txtHourInterval.Size = new System.Drawing.Size(42, 21);
			this.txtHourInterval.TabIndex = 4;
			this.txtHourInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHourInterval.KeyDown += txtIntervalCommon_KeyDown;
			this.txtHourInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// txtDayInterval
			// 
			this.txtDayInterval.Location = new System.Drawing.Point(12, 48);
			this.txtDayInterval.Name = "txtDayInterval";
			this.txtDayInterval.Size = new System.Drawing.Size(42, 21);
			this.txtDayInterval.TabIndex = 3;
			this.txtDayInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDayInterval.KeyDown += txtIntervalCommon_KeyDown;
			this.txtDayInterval.KeyPress += txtIntervalCommon_KeyPress;
			// 
			// btnDefaultInterval
			// 
			this.btnDefaultInterval.Location = new System.Drawing.Point(156, 51);
			this.btnDefaultInterval.Name = "btnDefaultInterval";
			this.btnDefaultInterval.Size = new System.Drawing.Size(16, 16);
			this.btnDefaultInterval.TabIndex = 6;
			this.btnDefaultInterval.UseVisualStyleBackColor = true;
			this.btnDefaultInterval.Click += btnDefaultInterval_Click;
			// 
			// IntervalControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxTimestamp);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "IntervalControl";
			this.Size = new System.Drawing.Size(204, 85);
			this.gbxTimestamp.ResumeLayout(false);
			this.gbxTimestamp.PerformLayout();
			this.ResumeLayout(false);
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
