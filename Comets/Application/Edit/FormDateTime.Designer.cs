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
			this.lblTime = new System.Windows.Forms.Label();
			this.txtSecond = new System.Windows.Forms.TextBox();
			this.lblDate = new System.Windows.Forms.Label();
			this.txtMinute = new System.Windows.Forms.TextBox();
			this.txtHour = new System.Windows.Forms.TextBox();
			this.txtDay = new System.Windows.Forms.TextBox();
			this.txtMonth = new System.Windows.Forms.TextBox();
			this.txtYear = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.gbxDateTime = new System.Windows.Forms.GroupBox();
			this.dateTimeMenuControl = new Comets.Application.Controls.Common.DateTimeMenuControl();
			this.gbxDateTime.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(17, 56);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(29, 13);
			this.lblTime.TabIndex = 28;
			this.lblTime.Text = "Time";
			// 
			// txtSecond
			// 
			this.txtSecond.Location = new System.Drawing.Point(140, 53);
			this.txtSecond.Name = "txtSecond";
			this.txtSecond.Size = new System.Drawing.Size(35, 21);
			this.txtSecond.TabIndex = 5;
			this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSecond.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(17, 24);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 26;
			this.lblDate.Text = "Date";
			// 
			// txtMinute
			// 
			this.txtMinute.Location = new System.Drawing.Point(99, 53);
			this.txtMinute.Name = "txtMinute";
			this.txtMinute.Size = new System.Drawing.Size(35, 21);
			this.txtMinute.TabIndex = 4;
			this.txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMinute.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtMinute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtHour
			// 
			this.txtHour.Location = new System.Drawing.Point(58, 53);
			this.txtHour.Name = "txtHour";
			this.txtHour.Size = new System.Drawing.Size(35, 21);
			this.txtHour.TabIndex = 3;
			this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtHour.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtDay
			// 
			this.txtDay.Location = new System.Drawing.Point(58, 21);
			this.txtDay.Name = "txtDay";
			this.txtDay.Size = new System.Drawing.Size(35, 21);
			this.txtDay.TabIndex = 0;
			this.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDay.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtMonth
			// 
			this.txtMonth.Location = new System.Drawing.Point(99, 21);
			this.txtMonth.Name = "txtMonth";
			this.txtMonth.Size = new System.Drawing.Size(35, 21);
			this.txtMonth.TabIndex = 1;
			this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMonth.TextChanged += new System.EventHandler(this.txtMonthYear_TextChanged);
			this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtYear
			// 
			this.txtYear.Location = new System.Drawing.Point(140, 21);
			this.txtYear.Name = "txtYear";
			this.txtYear.Size = new System.Drawing.Size(45, 21);
			this.txtYear.TabIndex = 2;
			this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtYear.TextChanged += new System.EventHandler(this.txtMonthYear_TextChanged);
			this.txtYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(249, 52);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(249, 20);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// gbxDateTime
			// 
			this.gbxDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxDateTime.Controls.Add(this.dateTimeMenuControl);
			this.gbxDateTime.Controls.Add(this.txtSecond);
			this.gbxDateTime.Controls.Add(this.txtYear);
			this.gbxDateTime.Controls.Add(this.txtMonth);
			this.gbxDateTime.Controls.Add(this.txtDay);
			this.gbxDateTime.Controls.Add(this.lblTime);
			this.gbxDateTime.Controls.Add(this.txtHour);
			this.gbxDateTime.Controls.Add(this.txtMinute);
			this.gbxDateTime.Controls.Add(this.lblDate);
			this.gbxDateTime.Location = new System.Drawing.Point(7, 1);
			this.gbxDateTime.Name = "gbxDateTime";
			this.gbxDateTime.Size = new System.Drawing.Size(233, 89);
			this.gbxDateTime.TabIndex = 0;
			this.gbxDateTime.TabStop = false;
			// 
			// dateTimeMenuControl1
			// 
			this.dateTimeMenuControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimeMenuControl.Location = new System.Drawing.Point(197, 20);
			this.dateTimeMenuControl.Name = "dateTimeMenuControl1";
			this.dateTimeMenuControl.Size = new System.Drawing.Size(24, 23);
			this.dateTimeMenuControl.TabIndex = 35;
			// 
			// FormDateTime
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(333, 97);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gbxDateTime);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDateTime";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Local Time";
			this.gbxDateTime.ResumeLayout(false);
			this.gbxDateTime.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.TextBox txtSecond;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.TextBox txtMinute;
		private System.Windows.Forms.TextBox txtHour;
		private System.Windows.Forms.TextBox txtDay;
		private System.Windows.Forms.TextBox txtMonth;
		private System.Windows.Forms.TextBox txtYear;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gbxDateTime;
		private Controls.Common.DateTimeMenuControl dateTimeMenuControl;
	}
}