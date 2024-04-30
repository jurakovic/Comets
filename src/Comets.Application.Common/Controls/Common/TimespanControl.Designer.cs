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
			gbxTimestamp = new System.Windows.Forms.GroupBox();
			selectDateControlEnd = new DateAndTime.SelectDateControl();
			selectDateControlStart = new DateAndTime.SelectDateControl();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			gbxTimestamp.SuspendLayout();
			SuspendLayout();
			// 
			// gbxTimestamp
			// 
			gbxTimestamp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gbxTimestamp.Controls.Add(selectDateControlEnd);
			gbxTimestamp.Controls.Add(selectDateControlStart);
			gbxTimestamp.Controls.Add(label3);
			gbxTimestamp.Controls.Add(label4);
			gbxTimestamp.Location = new System.Drawing.Point(0, 0);
			gbxTimestamp.Name = "gbxTimestamp";
			gbxTimestamp.Size = new System.Drawing.Size(235, 85);
			gbxTimestamp.TabIndex = 0;
			gbxTimestamp.TabStop = false;
			gbxTimestamp.Text = "Timespan (Universal time)";
			// 
			// selectDateControlEnd
			// 
			selectDateControlEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			selectDateControlEnd.DefaultDateTime = null;
			selectDateControlEnd.Font = new System.Drawing.Font("Tahoma", 8.25F);
			selectDateControlEnd.Location = new System.Drawing.Point(53, 49);
			selectDateControlEnd.Name = "selectDateControlEnd";
			selectDateControlEnd.PerihelionDate = null;
			selectDateControlEnd.SelectedDateTime = new System.DateTime(0L);
			selectDateControlEnd.Size = new System.Drawing.Size(172, 23);
			selectDateControlEnd.TabIndex = 3;
			// 
			// selectDateControlStart
			// 
			selectDateControlStart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			selectDateControlStart.DefaultDateTime = null;
			selectDateControlStart.Font = new System.Drawing.Font("Tahoma", 8.25F);
			selectDateControlStart.Location = new System.Drawing.Point(53, 20);
			selectDateControlStart.Name = "selectDateControlStart";
			selectDateControlStart.PerihelionDate = null;
			selectDateControlStart.SelectedDateTime = new System.DateTime(0L);
			selectDateControlStart.Size = new System.Drawing.Size(172, 23);
			selectDateControlStart.TabIndex = 1;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(8, 25);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(35, 13);
			label3.TabIndex = 0;
			label3.Text = "Start:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(8, 54);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(29, 13);
			label4.TabIndex = 2;
			label4.Text = "End:";
			// 
			// TimespanControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(gbxTimestamp);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Name = "TimespanControl";
			Size = new System.Drawing.Size(235, 85);
			gbxTimestamp.ResumeLayout(false);
			gbxTimestamp.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxTimestamp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private Application.Common.Controls.DateAndTime.SelectDateControl selectDateControlStart;
		private Application.Common.Controls.DateAndTime.SelectDateControl selectDateControlEnd;
	}
}
