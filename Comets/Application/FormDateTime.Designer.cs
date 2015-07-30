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
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.contextDateTime = new System.Windows.Forms.ContextMenu();
			this.mnuDefault = new System.Windows.Forms.MenuItem();
			this.separator1 = new System.Windows.Forms.MenuItem();
			this.mnuNow = new System.Windows.Forms.MenuItem();
			this.separator2 = new System.Windows.Forms.MenuItem();
			this.mnuMidnight = new System.Windows.Forms.MenuItem();
			this.mnuNoon = new System.Windows.Forms.MenuItem();
			this.separator3 = new System.Windows.Forms.MenuItem();
			this.mnuPerihelionDate = new System.Windows.Forms.MenuItem();
			this.gbxDateTime = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gbxDateTime.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTime
			// 
			this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(20, 63);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(29, 13);
			this.lblTime.TabIndex = 28;
			this.lblTime.Text = "Time";
			// 
			// txtSecond
			// 
			this.txtSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSecond.Location = new System.Drawing.Point(173, 60);
			this.txtSecond.Name = "txtSecond";
			this.txtSecond.Size = new System.Drawing.Size(42, 21);
			this.txtSecond.TabIndex = 5;
			this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSecond.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// lblDate
			// 
			this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(20, 31);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 26;
			this.lblDate.Text = "Date";
			// 
			// txtMinute
			// 
			this.txtMinute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMinute.Location = new System.Drawing.Point(120, 60);
			this.txtMinute.Name = "txtMinute";
			this.txtMinute.Size = new System.Drawing.Size(42, 21);
			this.txtMinute.TabIndex = 4;
			this.txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMinute.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtMinute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtHour
			// 
			this.txtHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHour.Location = new System.Drawing.Point(67, 60);
			this.txtHour.Name = "txtHour";
			this.txtHour.Size = new System.Drawing.Size(42, 21);
			this.txtHour.TabIndex = 3;
			this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtHour.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtDay
			// 
			this.txtDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDay.Location = new System.Drawing.Point(67, 28);
			this.txtDay.Name = "txtDay";
			this.txtDay.Size = new System.Drawing.Size(42, 21);
			this.txtDay.TabIndex = 0;
			this.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtDay.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
			this.txtDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtMonth
			// 
			this.txtMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMonth.Location = new System.Drawing.Point(120, 28);
			this.txtMonth.Name = "txtMonth";
			this.txtMonth.Size = new System.Drawing.Size(42, 21);
			this.txtMonth.TabIndex = 1;
			this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMonth.TextChanged += new System.EventHandler(this.txtMonthYear_TextChanged);
			this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// txtYear
			// 
			this.txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtYear.Location = new System.Drawing.Point(173, 28);
			this.txtYear.Name = "txtYear";
			this.txtYear.Size = new System.Drawing.Size(61, 21);
			this.txtYear.TabIndex = 2;
			this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtYear.TextChanged += new System.EventHandler(this.txtMonthYear_TextChanged);
			this.txtYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommon_KeyDown);
			this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommon_KeyPress);
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.Location = new System.Drawing.Point(279, 26);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(83, 23);
			this.btnSelect.TabIndex = 6;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(291, 127);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(185, 127);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// contextDateTime
			// 
			this.contextDateTime.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDefault,
            this.separator1,
            this.mnuNow,
            this.separator2,
            this.mnuMidnight,
            this.mnuNoon,
            this.separator3,
            this.mnuPerihelionDate});
			// 
			// mnuDefault
			// 
			this.mnuDefault.Index = 0;
			this.mnuDefault.Text = "Default";
			this.mnuDefault.Click += new System.EventHandler(this.mnuDefault_Click);
			// 
			// separator1
			// 
			this.separator1.Index = 1;
			this.separator1.Text = "-";
			// 
			// mnuNow
			// 
			this.mnuNow.Index = 2;
			this.mnuNow.Text = "Now";
			this.mnuNow.Click += new System.EventHandler(this.mnuNow_Click);
			// 
			// separator2
			// 
			this.separator2.Index = 3;
			this.separator2.Text = "-";
			// 
			// mnuMidnight
			// 
			this.mnuMidnight.Index = 4;
			this.mnuMidnight.Text = "Midnight";
			this.mnuMidnight.Click += new System.EventHandler(this.mnuMidnight_Click);
			// 
			// mnuNoon
			// 
			this.mnuNoon.Index = 5;
			this.mnuNoon.Text = "Noon";
			this.mnuNoon.Click += new System.EventHandler(this.mnuNoon_Click);
			// 
			// separator3
			// 
			this.separator3.Index = 6;
			this.separator3.Text = "-";
			// 
			// mnuPerihelionDate
			// 
			this.mnuPerihelionDate.Index = 7;
			this.mnuPerihelionDate.Text = "Perihelion Date";
			this.mnuPerihelionDate.Click += new System.EventHandler(this.mnuPerihelionDate_Click);
			// 
			// gbxDateTime
			// 
			this.gbxDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxDateTime.Controls.Add(this.label4);
			this.gbxDateTime.Controls.Add(this.label3);
			this.gbxDateTime.Controls.Add(this.label2);
			this.gbxDateTime.Controls.Add(this.txtSecond);
			this.gbxDateTime.Controls.Add(this.txtYear);
			this.gbxDateTime.Controls.Add(this.txtMonth);
			this.gbxDateTime.Controls.Add(this.btnSelect);
			this.gbxDateTime.Controls.Add(this.txtDay);
			this.gbxDateTime.Controls.Add(this.lblTime);
			this.gbxDateTime.Controls.Add(this.txtHour);
			this.gbxDateTime.Controls.Add(this.txtMinute);
			this.gbxDateTime.Controls.Add(this.lblDate);
			this.gbxDateTime.Controls.Add(this.label1);
			this.gbxDateTime.Location = new System.Drawing.Point(12, 6);
			this.gbxDateTime.Name = "gbxDateTime";
			this.gbxDateTime.Size = new System.Drawing.Size(379, 106);
			this.gbxDateTime.TabIndex = 0;
			this.gbxDateTime.TabStop = false;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(162, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(11, 13);
			this.label4.TabIndex = 33;
			this.label4.Text = ":";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(162, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(11, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = ".";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(109, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(11, 13);
			this.label2.TabIndex = 31;
			this.label2.Text = ":";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(109, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 13);
			this.label1.TabIndex = 30;
			this.label1.Text = ".";
			// 
			// FormDateTime
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(405, 165);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gbxDateTime);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ContextMenu contextDateTime;
		private System.Windows.Forms.MenuItem mnuDefault;
		private System.Windows.Forms.MenuItem separator1;
		private System.Windows.Forms.MenuItem mnuNow;
		private System.Windows.Forms.MenuItem separator2;
		private System.Windows.Forms.MenuItem mnuMidnight;
		private System.Windows.Forms.MenuItem mnuNoon;
		private System.Windows.Forms.MenuItem separator3;
		private System.Windows.Forms.MenuItem mnuPerihelionDate;
		private System.Windows.Forms.GroupBox gbxDateTime;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

	}
}