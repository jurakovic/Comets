﻿namespace Comets.Application.Common.Controls.DateAndTime
{
	partial class DateTimeMenuControl
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
			this.btnShowMenu = new System.Windows.Forms.Button();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip();
			this.mnuDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.sepDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToday = new System.Windows.Forms.ToolStripMenuItem();
			this.sepNow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPerihelionDate = new System.Windows.Forms.ToolStripMenuItem();
			this.sepPerihelionDate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuLastYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuThisYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNextYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAfterNextYear = new System.Windows.Forms.ToolStripMenuItem();
			this.sepAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddOneYear = new System.Windows.Forms.ToolStripMenuItem();
			this.sepSub = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSubThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSubSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSubOneYear = new System.Windows.Forms.ToolStripMenuItem();
			this.SuspendLayout();
			// 
			// btnShowMenu
			// 
			this.btnShowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnShowMenu.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnShowMenu.Location = new System.Drawing.Point(0, 0);
			this.btnShowMenu.Name = "btnShowMenu";
			this.btnShowMenu.Size = new System.Drawing.Size(24, 23);
			this.btnShowMenu.TabIndex = 0;
			this.btnShowMenu.Text = "▼";
			this.btnShowMenu.UseVisualStyleBackColor = true;
			this.btnShowMenu.Click += new System.EventHandler(this.btnShowMenu_Click);
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuDefault,
            this.sepDefault,
            this.mnuToday,
            this.sepNow,
            this.mnuPerihelionDate,
            this.sepPerihelionDate,
            this.mnuLastYear,
            this.mnuThisYear,
            this.mnuNextYear,
            this.mnuAfterNextYear,
            this.sepAdd,
            this.mnuAddThreeMonths,
            this.mnuAddSixMonths,
            this.mnuAddOneYear,
            this.sepSub,
            this.mnuSubThreeMonths,
            this.mnuSubSixMonths,
            this.mnuSubOneYear});
			// 
			// mnuDefault
			// 
			this.mnuDefault.Text = "Default";
			this.mnuDefault.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepDefault
			// 
			this.sepDefault.Text = "-";
			// 
			// mnuToday
			// 
			this.mnuToday.Text = "Today";
			this.mnuToday.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepNow
			// 
			this.sepNow.Text = "-";
			// 
			// mnuPerihelionDate
			// 
			this.mnuPerihelionDate.Text = "Perihelion Date";
			this.mnuPerihelionDate.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepPerihelionDate
			// 
			this.sepPerihelionDate.Text = "-";
			// 
			// mnuLastYear
			// 
			this.mnuLastYear.Text = "<last year first day>";
			this.mnuLastYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuThisYear
			// 
			this.mnuThisYear.Text = "<this year first day>";
			this.mnuThisYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuNextYear
			// 
			this.mnuNextYear.Text = "<next year first day>";
			this.mnuNextYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAfterNextYear
			// 
			this.mnuAfterNextYear.Text = "<after next year first day>";
			this.mnuAfterNextYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepAdd
			// 
			this.sepAdd.Text = "-";
			// 
			// mnuAddThreeMonths
			// 
			this.mnuAddThreeMonths.Text = "Add 3 months";
			this.mnuAddThreeMonths.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAddSixMonths
			// 
			this.mnuAddSixMonths.Text = "Add 6 months";
			this.mnuAddSixMonths.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAddOneYear
			// 
			this.mnuAddOneYear.Text = "Add 1 year";
			this.mnuAddOneYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepSub
			// 
			this.sepSub.Text = "-";
			// 
			// mnuSubThreeMonths
			// 
			this.mnuSubThreeMonths.Text = "Subtract 3 months";
			this.mnuSubThreeMonths.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuSubSixMonths
			// 
			this.mnuSubSixMonths.Text = "Subtract 6 months";
			this.mnuSubSixMonths.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuSubOneYear
			// 
			this.mnuSubOneYear.Text = "Subtract 1 year";
			this.mnuSubOneYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// DateTimeMenuControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnShowMenu);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "DateTimeMenuControl";
			this.Size = new System.Drawing.Size(24, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnShowMenu;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuDefault;
		private System.Windows.Forms.ToolStripMenuItem sepDefault;
		private System.Windows.Forms.ToolStripMenuItem mnuAddThreeMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuAddSixMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuAddOneYear;
		private System.Windows.Forms.ToolStripMenuItem sepAdd;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihelionDate;
		private System.Windows.Forms.ToolStripMenuItem mnuLastYear;
		private System.Windows.Forms.ToolStripMenuItem mnuThisYear;
		private System.Windows.Forms.ToolStripMenuItem mnuNextYear;
		private System.Windows.Forms.ToolStripMenuItem sepPerihelionDate;
		private System.Windows.Forms.ToolStripMenuItem mnuAfterNextYear;
		private System.Windows.Forms.ToolStripMenuItem mnuToday;
		private System.Windows.Forms.ToolStripMenuItem sepNow;
		private System.Windows.Forms.ToolStripMenuItem sepSub;
		private System.Windows.Forms.ToolStripMenuItem mnuSubThreeMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuSubSixMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuSubOneYear;
	}
}
