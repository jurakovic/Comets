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
			txtSecond = new System.Windows.Forms.TextBox();
			txtMinute = new System.Windows.Forms.TextBox();
			txtHour = new System.Windows.Forms.TextBox();
			txtDay = new System.Windows.Forms.TextBox();
			txtMonth = new System.Windows.Forms.TextBox();
			txtYear = new System.Windows.Forms.TextBox();
			btnCancel = new System.Windows.Forms.Button();
			btnOk = new System.Windows.Forms.Button();
			gbxDate = new System.Windows.Forms.GroupBox();
			txtLocalTime = new System.Windows.Forms.TextBox();
			lblLocalTime = new System.Windows.Forms.Label();
			lblS = new System.Windows.Forms.Label();
			lblM = new System.Windows.Forms.Label();
			lblH = new System.Windows.Forms.Label();
			lblD = new System.Windows.Forms.Label();
			lblMo = new System.Windows.Forms.Label();
			lblY = new System.Windows.Forms.Label();
			lblJD = new System.Windows.Forms.Label();
			lblTime = new System.Windows.Forms.Label();
			lblDate = new System.Windows.Forms.Label();
			txtJD = new System.Windows.Forms.TextBox();
			dateTimeMenuControl = new Common.Controls.DateAndTime.DateTimeMenuControl();
			gbxDate.SuspendLayout();
			SuspendLayout();
			// 
			// txtSecond
			// 
			txtSecond.Location = new System.Drawing.Point(171, 73);
			txtSecond.Name = "txtSecond";
			txtSecond.Size = new System.Drawing.Size(35, 21);
			txtSecond.TabIndex = 13;
			txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtSecond.TextChanged += txtCommon_TextChanged;
			txtSecond.KeyDown += txtCommon_KeyDown;
			txtSecond.KeyPress += txtCommon_KeyPress;
			// 
			// txtMinute
			// 
			txtMinute.Location = new System.Drawing.Point(125, 73);
			txtMinute.Name = "txtMinute";
			txtMinute.Size = new System.Drawing.Size(35, 21);
			txtMinute.TabIndex = 12;
			txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtMinute.TextChanged += txtCommon_TextChanged;
			txtMinute.KeyDown += txtCommon_KeyDown;
			txtMinute.KeyPress += txtCommon_KeyPress;
			// 
			// txtHour
			// 
			txtHour.Location = new System.Drawing.Point(79, 73);
			txtHour.Name = "txtHour";
			txtHour.Size = new System.Drawing.Size(35, 21);
			txtHour.TabIndex = 11;
			txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtHour.TextChanged += txtCommon_TextChanged;
			txtHour.KeyDown += txtCommon_KeyDown;
			txtHour.KeyPress += txtCommon_KeyPress;
			// 
			// txtDay
			// 
			txtDay.Location = new System.Drawing.Point(79, 35);
			txtDay.Name = "txtDay";
			txtDay.Size = new System.Drawing.Size(35, 21);
			txtDay.TabIndex = 4;
			txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtDay.TextChanged += txtCommon_TextChanged;
			txtDay.KeyDown += txtCommon_KeyDown;
			txtDay.KeyPress += txtCommon_KeyPress;
			// 
			// txtMonth
			// 
			txtMonth.Location = new System.Drawing.Point(125, 35);
			txtMonth.Name = "txtMonth";
			txtMonth.Size = new System.Drawing.Size(35, 21);
			txtMonth.TabIndex = 5;
			txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtMonth.TextChanged += txtMonthYear_TextChanged;
			txtMonth.KeyDown += txtCommon_KeyDown;
			txtMonth.KeyPress += txtCommon_KeyPress;
			// 
			// txtYear
			// 
			txtYear.Location = new System.Drawing.Point(171, 35);
			txtYear.Name = "txtYear";
			txtYear.Size = new System.Drawing.Size(45, 21);
			txtYear.TabIndex = 6;
			txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtYear.TextChanged += txtMonthYear_TextChanged;
			txtYear.KeyDown += txtCommon_KeyDown;
			txtYear.KeyPress += txtCommon_KeyPress;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(271, 149);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(90, 23);
			btnCancel.TabIndex = 3;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOk.Location = new System.Drawing.Point(271, 120);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(90, 23);
			btnOk.TabIndex = 2;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			// 
			// gbxDate
			// 
			gbxDate.Controls.Add(txtLocalTime);
			gbxDate.Controls.Add(lblLocalTime);
			gbxDate.Controls.Add(lblS);
			gbxDate.Controls.Add(lblM);
			gbxDate.Controls.Add(lblH);
			gbxDate.Controls.Add(lblD);
			gbxDate.Controls.Add(lblMo);
			gbxDate.Controls.Add(lblY);
			gbxDate.Controls.Add(lblJD);
			gbxDate.Controls.Add(lblTime);
			gbxDate.Controls.Add(lblDate);
			gbxDate.Controls.Add(txtJD);
			gbxDate.Controls.Add(txtSecond);
			gbxDate.Controls.Add(txtDay);
			gbxDate.Controls.Add(txtHour);
			gbxDate.Controls.Add(txtMinute);
			gbxDate.Controls.Add(txtMonth);
			gbxDate.Controls.Add(txtYear);
			gbxDate.Location = new System.Drawing.Point(11, 5);
			gbxDate.Name = "gbxDate";
			gbxDate.Size = new System.Drawing.Size(235, 191);
			gbxDate.TabIndex = 0;
			gbxDate.TabStop = false;
			// 
			// txtLocalTime
			// 
			txtLocalTime.BackColor = System.Drawing.SystemColors.Control;
			txtLocalTime.Location = new System.Drawing.Point(79, 111);
			txtLocalTime.Name = "txtLocalTime";
			txtLocalTime.ReadOnly = true;
			txtLocalTime.Size = new System.Drawing.Size(137, 21);
			txtLocalTime.TabIndex = 15;
			txtLocalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblLocalTime
			// 
			lblLocalTime.AutoSize = true;
			lblLocalTime.Location = new System.Drawing.Point(17, 114);
			lblLocalTime.Name = "lblLocalTime";
			lblLocalTime.Size = new System.Drawing.Size(58, 13);
			lblLocalTime.TabIndex = 14;
			lblLocalTime.Text = "Local time:";
			// 
			// lblS
			// 
			lblS.AutoSize = true;
			lblS.ForeColor = System.Drawing.SystemColors.GrayText;
			lblS.Location = new System.Drawing.Point(169, 57);
			lblS.Name = "lblS";
			lblS.Size = new System.Drawing.Size(13, 13);
			lblS.TabIndex = 9;
			lblS.Text = "S";
			// 
			// lblM
			// 
			lblM.AutoSize = true;
			lblM.ForeColor = System.Drawing.SystemColors.GrayText;
			lblM.Location = new System.Drawing.Point(123, 57);
			lblM.Name = "lblM";
			lblM.Size = new System.Drawing.Size(15, 13);
			lblM.TabIndex = 8;
			lblM.Text = "M";
			// 
			// lblH
			// 
			lblH.AutoSize = true;
			lblH.ForeColor = System.Drawing.SystemColors.GrayText;
			lblH.Location = new System.Drawing.Point(77, 57);
			lblH.Name = "lblH";
			lblH.Size = new System.Drawing.Size(14, 13);
			lblH.TabIndex = 7;
			lblH.Text = "H";
			// 
			// lblD
			// 
			lblD.AutoSize = true;
			lblD.ForeColor = System.Drawing.SystemColors.GrayText;
			lblD.Location = new System.Drawing.Point(77, 19);
			lblD.Name = "lblD";
			lblD.Size = new System.Drawing.Size(14, 13);
			lblD.TabIndex = 0;
			lblD.Text = "D";
			// 
			// lblMo
			// 
			lblMo.AutoSize = true;
			lblMo.ForeColor = System.Drawing.SystemColors.GrayText;
			lblMo.Location = new System.Drawing.Point(123, 19);
			lblMo.Name = "lblMo";
			lblMo.Size = new System.Drawing.Size(15, 13);
			lblMo.TabIndex = 1;
			lblMo.Text = "M";
			// 
			// lblY
			// 
			lblY.AutoSize = true;
			lblY.ForeColor = System.Drawing.SystemColors.GrayText;
			lblY.Location = new System.Drawing.Point(169, 19);
			lblY.Name = "lblY";
			lblY.Size = new System.Drawing.Size(13, 13);
			lblY.TabIndex = 2;
			lblY.Text = "Y";
			// 
			// lblJD
			// 
			lblJD.AutoSize = true;
			lblJD.Location = new System.Drawing.Point(17, 152);
			lblJD.Name = "lblJD";
			lblJD.Size = new System.Drawing.Size(23, 13);
			lblJD.TabIndex = 16;
			lblJD.Text = "JD:";
			// 
			// lblTime
			// 
			lblTime.AutoSize = true;
			lblTime.Location = new System.Drawing.Point(17, 76);
			lblTime.Name = "lblTime";
			lblTime.Size = new System.Drawing.Size(33, 13);
			lblTime.TabIndex = 10;
			lblTime.Text = "Time:";
			// 
			// lblDate
			// 
			lblDate.AutoSize = true;
			lblDate.Location = new System.Drawing.Point(17, 38);
			lblDate.Name = "lblDate";
			lblDate.Size = new System.Drawing.Size(34, 13);
			lblDate.TabIndex = 3;
			lblDate.Text = "Date:";
			// 
			// txtJD
			// 
			txtJD.Location = new System.Drawing.Point(79, 149);
			txtJD.Name = "txtJD";
			txtJD.Size = new System.Drawing.Size(137, 21);
			txtJD.TabIndex = 17;
			txtJD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtJD.TextChanged += txtJD_TextChanged;
			txtJD.KeyDown += txtJD_KeyDown;
			txtJD.KeyPress += txtCommon_KeyPress;
			// 
			// dateTimeMenuControl
			// 
			dateTimeMenuControl.DefaultDateTime = null;
			dateTimeMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			dateTimeMenuControl.Location = new System.Drawing.Point(271, 38);
			dateTimeMenuControl.Name = "dateTimeMenuControl";
			dateTimeMenuControl.PerihelionDate = null;
			dateTimeMenuControl.ReferenceControl = null;
			dateTimeMenuControl.SelectedDateTime = new System.DateTime(0L);
			dateTimeMenuControl.Size = new System.Drawing.Size(90, 23);
			dateTimeMenuControl.TabIndex = 1;
			dateTimeMenuControl.Title = "Select";
			// 
			// FormDateTime
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new System.Drawing.Size(384, 206);
			Controls.Add(gbxDate);
			Controls.Add(btnCancel);
			Controls.Add(btnOk);
			Controls.Add(dateTimeMenuControl);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormDateTime";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Universal Time (UTC)";
			Load += FormDateTime_Load;
			gbxDate.ResumeLayout(false);
			gbxDate.PerformLayout();
			ResumeLayout(false);
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