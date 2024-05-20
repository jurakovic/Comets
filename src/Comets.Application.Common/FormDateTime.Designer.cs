namespace Comets.Application
{
	partial class FormDateTime
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtSecond = new System.Windows.Forms.TextBox();
			this.txtMinute = new System.Windows.Forms.TextBox();
			this.txtHour = new System.Windows.Forms.TextBox();
			this.txtDay = new System.Windows.Forms.TextBox();
			this.txtMonth = new System.Windows.Forms.TextBox();
			this.txtYear = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.gbxDate = new System.Windows.Forms.GroupBox();
			this.txtLocalTime = new System.Windows.Forms.TextBox();
			this.lblLocalTime = new System.Windows.Forms.Label();
			this.lblS = new System.Windows.Forms.Label();
			this.lblM = new System.Windows.Forms.Label();
			this.lblH = new System.Windows.Forms.Label();
			this.lblD = new System.Windows.Forms.Label();
			this.lblMo = new System.Windows.Forms.Label();
			this.lblY = new System.Windows.Forms.Label();
			this.lblJD = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.txtJD = new System.Windows.Forms.TextBox();
			this.dateTimeMenuControl = new Common.Controls.DateAndTime.DateTimeMenuControl();
			this.gbxDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSecond
			// 
			this.txtSecond.Location = new System.Drawing.Point(171, 73);
			this.txtSecond.Name = "txtSecond";
			this.txtSecond.Size = new System.Drawing.Size(35, 21);
			this.txtSecond.TabIndex = 13;
			this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSecond.TextChanged += this.txtCommon_TextChanged;
			this.txtSecond.KeyDown += this.txtCommon_KeyDown;
			this.txtSecond.KeyPress += this.txtCommon_KeyPress;
			// 
			// txtMinute
			// 
			this.txtMinute.Location = new System.Drawing.Point(125, 73);
			this.txtMinute.Name = "txtMinute";
			this.txtMinute.Size = new System.Drawing.Size(35, 21);
			this.txtMinute.TabIndex = 12;
			this.txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMinute.TextChanged += this.txtCommon_TextChanged;
			this.txtMinute.KeyDown += this.txtCommon_KeyDown;
			this.txtMinute.KeyPress += this.txtCommon_KeyPress;
			// 
			// txtHour
			// 
			this.txtHour.Location = new System.Drawing.Point(79, 73);
			this.txtHour.Name = "txtHour";
			this.txtHour.Size = new System.Drawing.Size(35, 21);
			this.txtHour.TabIndex = 11;
			this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtHour.TextChanged += this.txtCommon_TextChanged;
			this.txtHour.KeyDown += this.txtCommon_KeyDown;
			this.txtHour.KeyPress += this.txtCommon_KeyPress;
			// 
			// txtDay
			// 
			this.txtDay.Location = new System.Drawing.Point(79, 35);
			this.txtDay.Name = "txtDay";
			this.txtDay.Size = new System.Drawing.Size(35, 21);
			this.txtDay.TabIndex = 4;
			this.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDay.TextChanged += this.txtCommon_TextChanged;
			this.txtDay.KeyDown += this.txtCommon_KeyDown;
			this.txtDay.KeyPress += this.txtCommon_KeyPress;
			// 
			// txtMonth
			// 
			this.txtMonth.Location = new System.Drawing.Point(125, 35);
			this.txtMonth.Name = "txtMonth";
			this.txtMonth.Size = new System.Drawing.Size(35, 21);
			this.txtMonth.TabIndex = 5;
			this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMonth.TextChanged += this.txtMonthYear_TextChanged;
			this.txtMonth.KeyDown += this.txtCommon_KeyDown;
			this.txtMonth.KeyPress += this.txtCommon_KeyPress;
			// 
			// txtYear
			// 
			this.txtYear.Location = new System.Drawing.Point(171, 35);
			this.txtYear.Name = "txtYear";
			this.txtYear.Size = new System.Drawing.Size(45, 21);
			this.txtYear.TabIndex = 6;
			this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtYear.TextChanged += this.txtMonthYear_TextChanged;
			this.txtYear.KeyDown += this.txtCommon_KeyDown;
			this.txtYear.KeyPress += this.txtCommon_KeyPress;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(271, 149);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(271, 120);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(90, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += this.btnOk_Click;
			// 
			// gbxDate
			// 
			this.gbxDate.Controls.Add(this.txtLocalTime);
			this.gbxDate.Controls.Add(this.lblLocalTime);
			this.gbxDate.Controls.Add(this.lblS);
			this.gbxDate.Controls.Add(this.lblM);
			this.gbxDate.Controls.Add(this.lblH);
			this.gbxDate.Controls.Add(this.lblD);
			this.gbxDate.Controls.Add(this.lblMo);
			this.gbxDate.Controls.Add(this.lblY);
			this.gbxDate.Controls.Add(this.lblJD);
			this.gbxDate.Controls.Add(this.lblTime);
			this.gbxDate.Controls.Add(this.lblDate);
			this.gbxDate.Controls.Add(this.txtJD);
			this.gbxDate.Controls.Add(this.txtSecond);
			this.gbxDate.Controls.Add(this.txtDay);
			this.gbxDate.Controls.Add(this.txtHour);
			this.gbxDate.Controls.Add(this.txtMinute);
			this.gbxDate.Controls.Add(this.txtMonth);
			this.gbxDate.Controls.Add(this.txtYear);
			this.gbxDate.Location = new System.Drawing.Point(11, 5);
			this.gbxDate.Name = "gbxDate";
			this.gbxDate.Size = new System.Drawing.Size(235, 191);
			this.gbxDate.TabIndex = 0;
			this.gbxDate.TabStop = false;
			// 
			// txtLocalTime
			// 
			this.txtLocalTime.BackColor = System.Drawing.SystemColors.Control;
			this.txtLocalTime.Location = new System.Drawing.Point(79, 111);
			this.txtLocalTime.Name = "txtLocalTime";
			this.txtLocalTime.ReadOnly = true;
			this.txtLocalTime.Size = new System.Drawing.Size(137, 21);
			this.txtLocalTime.TabIndex = 15;
			this.txtLocalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblLocalTime
			// 
			this.lblLocalTime.AutoSize = true;
			this.lblLocalTime.Location = new System.Drawing.Point(17, 114);
			this.lblLocalTime.Name = "lblLocalTime";
			this.lblLocalTime.Size = new System.Drawing.Size(58, 13);
			this.lblLocalTime.TabIndex = 14;
			this.lblLocalTime.Text = "Local Time:";
			// 
			// lblS
			// 
			this.lblS.AutoSize = true;
			this.lblS.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblS.Location = new System.Drawing.Point(169, 57);
			this.lblS.Name = "lblS";
			this.lblS.Size = new System.Drawing.Size(13, 13);
			this.lblS.TabIndex = 9;
			this.lblS.Text = "S";
			// 
			// lblM
			// 
			this.lblM.AutoSize = true;
			this.lblM.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblM.Location = new System.Drawing.Point(123, 57);
			this.lblM.Name = "lblM";
			this.lblM.Size = new System.Drawing.Size(15, 13);
			this.lblM.TabIndex = 8;
			this.lblM.Text = "M";
			// 
			// lblH
			// 
			this.lblH.AutoSize = true;
			this.lblH.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblH.Location = new System.Drawing.Point(77, 57);
			this.lblH.Name = "lblH";
			this.lblH.Size = new System.Drawing.Size(14, 13);
			this.lblH.TabIndex = 7;
			this.lblH.Text = "H";
			// 
			// lblD
			// 
			this.lblD.AutoSize = true;
			this.lblD.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblD.Location = new System.Drawing.Point(77, 19);
			this.lblD.Name = "lblD";
			this.lblD.Size = new System.Drawing.Size(14, 13);
			this.lblD.TabIndex = 0;
			this.lblD.Text = "D";
			// 
			// lblMo
			// 
			this.lblMo.AutoSize = true;
			this.lblMo.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblMo.Location = new System.Drawing.Point(123, 19);
			this.lblMo.Name = "lblMo";
			this.lblMo.Size = new System.Drawing.Size(15, 13);
			this.lblMo.TabIndex = 1;
			this.lblMo.Text = "M";
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblY.Location = new System.Drawing.Point(169, 19);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(13, 13);
			this.lblY.TabIndex = 2;
			this.lblY.Text = "Y";
			// 
			// lblJD
			// 
			this.lblJD.AutoSize = true;
			this.lblJD.Location = new System.Drawing.Point(17, 152);
			this.lblJD.Name = "lblJD";
			this.lblJD.Size = new System.Drawing.Size(23, 13);
			this.lblJD.TabIndex = 16;
			this.lblJD.Text = "JD:";
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(17, 76);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(33, 13);
			this.lblTime.TabIndex = 10;
			this.lblTime.Text = "Time:";
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(17, 38);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(34, 13);
			this.lblDate.TabIndex = 3;
			this.lblDate.Text = "Date:";
			// 
			// txtJD
			// 
			this.txtJD.Location = new System.Drawing.Point(79, 149);
			this.txtJD.Name = "txtJD";
			this.txtJD.Size = new System.Drawing.Size(137, 21);
			this.txtJD.TabIndex = 17;
			this.txtJD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtJD.TextChanged += this.txtJD_TextChanged;
			this.txtJD.KeyDown += this.txtJD_KeyDown;
			this.txtJD.KeyPress += this.txtCommon_KeyPress;
			// 
			// dateTimeMenuControl
			// 
			this.dateTimeMenuControl.DefaultDateTime = null;
			this.dateTimeMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.dateTimeMenuControl.Location = new System.Drawing.Point(271, 38);
			this.dateTimeMenuControl.Name = "dateTimeMenuControl";
			this.dateTimeMenuControl.PerihelionDate = null;
			this.dateTimeMenuControl.ReferenceControl = null;
			this.dateTimeMenuControl.SelectedDateTime = new System.DateTime(0L);
			this.dateTimeMenuControl.Size = new System.Drawing.Size(90, 23);
			this.dateTimeMenuControl.TabIndex = 1;
			this.dateTimeMenuControl.Title = "Select";
			// 
			// FormDateTime
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(384, 206);
			this.Controls.Add(this.gbxDate);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.dateTimeMenuControl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDateTime";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Universal Time (UTC)";
			this.Load += this.FormDateTime_Load;
			this.gbxDate.ResumeLayout(false);
			this.gbxDate.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.TextBox txtSecond;
		private System.Windows.Forms.TextBox txtMinute;
		private System.Windows.Forms.TextBox txtHour;
		private System.Windows.Forms.TextBox txtDay;
		private System.Windows.Forms.TextBox txtMonth;
		private System.Windows.Forms.TextBox txtYear;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gbxDate;
		private System.Windows.Forms.TextBox txtJD;
		private System.Windows.Forms.Label lblDate;
		private Common.Controls.DateAndTime.DateTimeMenuControl dateTimeMenuControl;
		private System.Windows.Forms.Label lblJD;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label lblS;
		private System.Windows.Forms.Label lblM;
		private System.Windows.Forms.Label lblH;
		private System.Windows.Forms.Label lblD;
		private System.Windows.Forms.Label lblMo;
		private System.Windows.Forms.Label lblY;
		private System.Windows.Forms.Label lblLocalTime;
		private System.Windows.Forms.TextBox txtLocalTime;
	}
}
