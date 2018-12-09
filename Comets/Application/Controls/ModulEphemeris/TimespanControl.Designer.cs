namespace Comets.Application.Controls.ModulEphemeris
{
	partial class TimespanControl
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
			this.selectDateControlEnd = new Comets.Application.Controls.Common.SelectDateControl();
			this.selectDateControlStart = new Comets.Application.Controls.Common.SelectDateControl();
			this.txtMinInterval = new System.Windows.Forms.TextBox();
			this.txtHourInterval = new System.Windows.Forms.TextBox();
			this.txtDayInterval = new System.Windows.Forms.TextBox();
			this.btnDefaultInterval = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.gbxTimestamp.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxTimestamp
			// 
			this.gbxTimestamp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxTimestamp.Controls.Add(this.selectDateControlEnd);
			this.gbxTimestamp.Controls.Add(this.selectDateControlStart);
			this.gbxTimestamp.Controls.Add(this.txtMinInterval);
			this.gbxTimestamp.Controls.Add(this.txtHourInterval);
			this.gbxTimestamp.Controls.Add(this.txtDayInterval);
			this.gbxTimestamp.Controls.Add(this.btnDefaultInterval);
			this.gbxTimestamp.Controls.Add(this.label3);
			this.gbxTimestamp.Controls.Add(this.label20);
			this.gbxTimestamp.Controls.Add(this.label4);
			this.gbxTimestamp.Location = new System.Drawing.Point(0, 0);
			this.gbxTimestamp.Name = "gbxTimestamp";
			this.gbxTimestamp.Size = new System.Drawing.Size(288, 135);
			this.gbxTimestamp.TabIndex = 0;
			this.gbxTimestamp.TabStop = false;
			this.gbxTimestamp.Text = "Timespan (Local time)";
			// 
			// selectDateControlEnd
			// 
			this.selectDateControlEnd.Location = new System.Drawing.Point(106, 48);
			this.selectDateControlEnd.Name = "selectDateControlEnd";
			this.selectDateControlEnd.Size = new System.Drawing.Size(172, 23);
			this.selectDateControlEnd.TabIndex = 3;
			// 
			// selectDateControlStart
			// 
			this.selectDateControlStart.Location = new System.Drawing.Point(106, 19);
			this.selectDateControlStart.Name = "selectDateControlStart";
			this.selectDateControlStart.Size = new System.Drawing.Size(172, 23);
			this.selectDateControlStart.TabIndex = 1;
			// 
			// txtMinInterval
			// 
			this.txtMinInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMinInterval.Location = new System.Drawing.Point(211, 77);
			this.txtMinInterval.Name = "txtMinInterval";
			this.txtMinInterval.Size = new System.Drawing.Size(42, 21);
			this.txtMinInterval.TabIndex = 7;
			this.txtMinInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtMinInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// txtHourInterval
			// 
			this.txtHourInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHourInterval.Location = new System.Drawing.Point(163, 77);
			this.txtHourInterval.Name = "txtHourInterval";
			this.txtHourInterval.Size = new System.Drawing.Size(42, 21);
			this.txtHourInterval.TabIndex = 6;
			this.txtHourInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHourInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtHourInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// txtDayInterval
			// 
			this.txtDayInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDayInterval.Location = new System.Drawing.Point(107, 77);
			this.txtDayInterval.Name = "txtDayInterval";
			this.txtDayInterval.Size = new System.Drawing.Size(42, 21);
			this.txtDayInterval.TabIndex = 5;
			this.txtDayInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDayInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtDayInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// btnDefaultInterval
			// 
			this.btnDefaultInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDefaultInterval.Location = new System.Drawing.Point(259, 80);
			this.btnDefaultInterval.Name = "btnDefaultInterval";
			this.btnDefaultInterval.Size = new System.Drawing.Size(16, 16);
			this.btnDefaultInterval.TabIndex = 8;
			this.btnDefaultInterval.UseVisualStyleBackColor = true;
			this.btnDefaultInterval.Click += new System.EventHandler(this.btnDefaultInterval_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Start:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(15, 81);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(49, 13);
			this.label20.TabIndex = 4;
			this.label20.Text = "Interval:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 54);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "End:";
			// 
			// TimespanControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxTimestamp);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TimespanControl";
			this.Size = new System.Drawing.Size(288, 135);
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
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label4;
		private Common.SelectDateControl selectDateControlStart;
		private Common.SelectDateControl selectDateControlEnd;
	}
}
