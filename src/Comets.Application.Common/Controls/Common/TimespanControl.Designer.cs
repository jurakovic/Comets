namespace Comets.Application.Common.Controls.Common
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
			this.selectDateControlEnd = new DateAndTime.SelectDateControl();
			this.selectDateControlStart = new DateAndTime.SelectDateControl();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.gbxTimestamp.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxTimestamp
			// 
			this.gbxTimestamp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxTimestamp.Controls.Add(this.selectDateControlEnd);
			this.gbxTimestamp.Controls.Add(this.selectDateControlStart);
			this.gbxTimestamp.Controls.Add(this.label3);
			this.gbxTimestamp.Controls.Add(this.label4);
			this.gbxTimestamp.Location = new System.Drawing.Point(0, 0);
			this.gbxTimestamp.Name = "gbxTimestamp";
			this.gbxTimestamp.Size = new System.Drawing.Size(235, 85);
			this.gbxTimestamp.TabIndex = 0;
			this.gbxTimestamp.TabStop = false;
			this.gbxTimestamp.Text = "Timespan (Universal time)";
			// 
			// selectDateControlEnd
			// 
			this.selectDateControlEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.selectDateControlEnd.DefaultDateTime = null;
			this.selectDateControlEnd.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.selectDateControlEnd.Location = new System.Drawing.Point(53, 49);
			this.selectDateControlEnd.Name = "selectDateControlEnd";
			this.selectDateControlEnd.PerihelionDate = null;
			this.selectDateControlEnd.SelectedDateTime = new System.DateTime(0L);
			this.selectDateControlEnd.Size = new System.Drawing.Size(172, 24);
			this.selectDateControlEnd.TabIndex = 3;
			// 
			// selectDateControlStart
			// 
			this.selectDateControlStart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.selectDateControlStart.DefaultDateTime = null;
			this.selectDateControlStart.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.selectDateControlStart.Location = new System.Drawing.Point(53, 20);
			this.selectDateControlStart.Name = "selectDateControlStart";
			this.selectDateControlStart.PerihelionDate = null;
			this.selectDateControlStart.SelectedDateTime = new System.DateTime(0L);
			this.selectDateControlStart.Size = new System.Drawing.Size(172, 24);
			this.selectDateControlStart.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Start:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 54);
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
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.Name = "TimespanControl";
			this.Size = new System.Drawing.Size(235, 85);
			this.gbxTimestamp.ResumeLayout(false);
			this.gbxTimestamp.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxTimestamp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private Application.Common.Controls.DateAndTime.SelectDateControl selectDateControlStart;
		private Application.Common.Controls.DateAndTime.SelectDateControl selectDateControlEnd;
	}
}
