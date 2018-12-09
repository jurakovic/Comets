namespace Comets.Application.Controls.ModulGraph
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
			this.gbxTimespan = new System.Windows.Forms.GroupBox();
			this.pnlRangeDaysFromT = new System.Windows.Forms.Panel();
			this.txtDaysAfterT = new System.Windows.Forms.TextBox();
			this.txtDaysBeforeT = new System.Windows.Forms.TextBox();
			this.btnTimespanDaysFromTDefault = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlRangeDate = new System.Windows.Forms.Panel();
			this.selectDateControlEnd = new Comets.Application.Controls.Common.SelectDateControl();
			this.selectDateControlStart = new Comets.Application.Controls.Common.SelectDateControl();
			this.label1 = new System.Windows.Forms.Label();
			this.rbtnRangeDaysFromT = new System.Windows.Forms.RadioButton();
			this.rbtnRangeDate = new System.Windows.Forms.RadioButton();
			this.gbxTimespan.SuspendLayout();
			this.pnlRangeDaysFromT.SuspendLayout();
			this.pnlRangeDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxTimespan
			// 
			this.gbxTimespan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxTimespan.Controls.Add(this.pnlRangeDaysFromT);
			this.gbxTimespan.Controls.Add(this.pnlRangeDate);
			this.gbxTimespan.Controls.Add(this.rbtnRangeDaysFromT);
			this.gbxTimespan.Controls.Add(this.rbtnRangeDate);
			this.gbxTimespan.Location = new System.Drawing.Point(0, 0);
			this.gbxTimespan.Name = "gbxTimespan";
			this.gbxTimespan.Size = new System.Drawing.Size(558, 83);
			this.gbxTimespan.TabIndex = 0;
			this.gbxTimespan.TabStop = false;
			this.gbxTimespan.Text = "Timespan (Local time)";
			// 
			// pnlRangeDaysFromT
			// 
			this.pnlRangeDaysFromT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysAfterT);
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysBeforeT);
			this.pnlRangeDaysFromT.Controls.Add(this.btnTimespanDaysFromTDefault);
			this.pnlRangeDaysFromT.Controls.Add(this.label4);
			this.pnlRangeDaysFromT.Controls.Add(this.label3);
			this.pnlRangeDaysFromT.Controls.Add(this.label2);
			this.pnlRangeDaysFromT.Location = new System.Drawing.Point(167, 45);
			this.pnlRangeDaysFromT.Name = "pnlRangeDaysFromT";
			this.pnlRangeDaysFromT.Size = new System.Drawing.Size(362, 27);
			this.pnlRangeDaysFromT.TabIndex = 3;
			// 
			// txtDaysAfterT
			// 
			this.txtDaysAfterT.Location = new System.Drawing.Point(190, 3);
			this.txtDaysAfterT.Name = "txtDaysAfterT";
			this.txtDaysAfterT.Size = new System.Drawing.Size(42, 21);
			this.txtDaysAfterT.TabIndex = 3;
			this.txtDaysAfterT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDaysAfterT.TextChanged += new System.EventHandler(this.txtDaysFromTCommon_TextChanged);
			this.txtDaysAfterT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDaysFromTCommon_KeyDown);
			this.txtDaysAfterT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDaysFromTCommon_KeyPress);
			// 
			// txtDaysBeforeT
			// 
			this.txtDaysBeforeT.Location = new System.Drawing.Point(65, 3);
			this.txtDaysBeforeT.Name = "txtDaysBeforeT";
			this.txtDaysBeforeT.Size = new System.Drawing.Size(42, 21);
			this.txtDaysBeforeT.TabIndex = 0;
			this.txtDaysBeforeT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDaysBeforeT.TextChanged += new System.EventHandler(this.txtDaysFromTCommon_TextChanged);
			this.txtDaysBeforeT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDaysFromTCommon_KeyDown);
			this.txtDaysBeforeT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDaysFromTCommon_KeyPress);
			// 
			// btnTimespanDaysFromTDefault
			// 
			this.btnTimespanDaysFromTDefault.Location = new System.Drawing.Point(339, 6);
			this.btnTimespanDaysFromTDefault.Name = "btnTimespanDaysFromTDefault";
			this.btnTimespanDaysFromTDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanDaysFromTDefault.TabIndex = 5;
			this.btnTimespanDaysFromTDefault.UseVisualStyleBackColor = true;
			this.btnTimespanDaysFromTDefault.Click += new System.EventHandler(this.btnDaysFromTDefault_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(244, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "days";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(113, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "days";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(175, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(11, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "-";
			// 
			// pnlRangeDate
			// 
			this.pnlRangeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRangeDate.Controls.Add(this.selectDateControlEnd);
			this.pnlRangeDate.Controls.Add(this.selectDateControlStart);
			this.pnlRangeDate.Controls.Add(this.label1);
			this.pnlRangeDate.Location = new System.Drawing.Point(167, 15);
			this.pnlRangeDate.Name = "pnlRangeDate";
			this.pnlRangeDate.Size = new System.Drawing.Size(362, 27);
			this.pnlRangeDate.TabIndex = 2;
			// 
			// selectDateControlEnd
			// 
			this.selectDateControlEnd.Location = new System.Drawing.Point(188, 2);
			this.selectDateControlEnd.Name = "selectDateControlEnd";
			this.selectDateControlEnd.Size = new System.Drawing.Size(172, 23);
			this.selectDateControlEnd.TabIndex = 4;
			// 
			// selectDateControlStart
			// 
			this.selectDateControlStart.Location = new System.Drawing.Point(2, 2);
			this.selectDateControlStart.Name = "selectDateControlStart";
			this.selectDateControlStart.Size = new System.Drawing.Size(172, 23);
			this.selectDateControlStart.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(175, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "-";
			// 
			// rbtnRangeDaysFromT
			// 
			this.rbtnRangeDaysFromT.AutoSize = true;
			this.rbtnRangeDaysFromT.Location = new System.Drawing.Point(12, 48);
			this.rbtnRangeDaysFromT.Name = "rbtnRangeDaysFromT";
			this.rbtnRangeDaysFromT.Size = new System.Drawing.Size(83, 17);
			this.rbtnRangeDaysFromT.TabIndex = 1;
			this.rbtnRangeDaysFromT.Text = "Days from T";
			this.rbtnRangeDaysFromT.UseVisualStyleBackColor = true;
			// 
			// rbtnRangeDate
			// 
			this.rbtnRangeDate.AutoSize = true;
			this.rbtnRangeDate.Checked = true;
			this.rbtnRangeDate.Location = new System.Drawing.Point(12, 21);
			this.rbtnRangeDate.Name = "rbtnRangeDate";
			this.rbtnRangeDate.Size = new System.Drawing.Size(48, 17);
			this.rbtnRangeDate.TabIndex = 0;
			this.rbtnRangeDate.TabStop = true;
			this.rbtnRangeDate.Text = "Date";
			this.rbtnRangeDate.UseVisualStyleBackColor = true;
			// 
			// TimespanControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxTimespan);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "TimespanControl";
			this.Size = new System.Drawing.Size(558, 83);
			this.gbxTimespan.ResumeLayout(false);
			this.gbxTimespan.PerformLayout();
			this.pnlRangeDaysFromT.ResumeLayout(false);
			this.pnlRangeDaysFromT.PerformLayout();
			this.pnlRangeDate.ResumeLayout(false);
			this.pnlRangeDate.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxTimespan;
		private System.Windows.Forms.Panel pnlRangeDaysFromT;
		private System.Windows.Forms.Button btnTimespanDaysFromTDefault;
		private System.Windows.Forms.TextBox txtDaysAfterT;
		private System.Windows.Forms.TextBox txtDaysBeforeT;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlRangeDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rbtnRangeDaysFromT;
		private System.Windows.Forms.RadioButton rbtnRangeDate;
		private Common.SelectDateControl selectDateControlEnd;
		private Common.SelectDateControl selectDateControlStart;
	}
}
