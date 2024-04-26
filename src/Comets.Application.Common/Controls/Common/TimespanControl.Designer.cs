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
			gbxTimestamp = new GroupBox();
			selectDateControlEnd = new DateAndTime.SelectDateControl();
			selectDateControlStart = new DateAndTime.SelectDateControl();
			label3 = new Label();
			label4 = new Label();
			gbxTimestamp.SuspendLayout();
			SuspendLayout();
			// 
			// gbxTimestamp
			// 
			gbxTimestamp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxTimestamp.Controls.Add(selectDateControlEnd);
			gbxTimestamp.Controls.Add(selectDateControlStart);
			gbxTimestamp.Controls.Add(label3);
			gbxTimestamp.Controls.Add(label4);
			gbxTimestamp.Location = new Point(0, 0);
			gbxTimestamp.Name = "gbxTimestamp";
			gbxTimestamp.Size = new Size(235, 85);
			gbxTimestamp.TabIndex = 0;
			gbxTimestamp.TabStop = false;
			gbxTimestamp.Text = "Timespan (Universal time)";
			// 
			// selectDateControlEnd
			// 
			selectDateControlEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			selectDateControlEnd.DefaultDateTime = null;
			selectDateControlEnd.Font = new Font("Tahoma", 8.25F);
			selectDateControlEnd.Location = new Point(53, 49);
			selectDateControlEnd.Name = "selectDateControlEnd";
			selectDateControlEnd.PerihelionDate = null;
			selectDateControlEnd.SelectedDateTime = new DateTime(0L);
			selectDateControlEnd.Size = new Size(172, 23);
			selectDateControlEnd.TabIndex = 3;
			// 
			// selectDateControlStart
			// 
			selectDateControlStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			selectDateControlStart.DefaultDateTime = null;
			selectDateControlStart.Font = new Font("Tahoma", 8.25F);
			selectDateControlStart.Location = new Point(53, 20);
			selectDateControlStart.Name = "selectDateControlStart";
			selectDateControlStart.PerihelionDate = null;
			selectDateControlStart.SelectedDateTime = new DateTime(0L);
			selectDateControlStart.Size = new Size(172, 23);
			selectDateControlStart.TabIndex = 1;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(8, 25);
			label3.Name = "label3";
			label3.Size = new Size(35, 13);
			label3.TabIndex = 0;
			label3.Text = "Start:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(8, 54);
			label4.Name = "label4";
			label4.Size = new Size(29, 13);
			label4.TabIndex = 2;
			label4.Text = "End:";
			// 
			// TimespanControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxTimestamp);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Name = "TimespanControl";
			Size = new Size(235, 85);
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
