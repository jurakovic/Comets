namespace Comets.Application.Common.Controls.DateAndTime
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
			this.components = new System.ComponentModel.Container();
			this.btnShowMenu = new System.Windows.Forms.Button();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.sepDefault = new System.Windows.Forms.ToolStripSeparator();
			this.mnuToday = new System.Windows.Forms.ToolStripMenuItem();
			this.sepNow = new System.Windows.Forms.ToolStripSeparator();
			this.mnuPerihelionDate = new System.Windows.Forms.ToolStripMenuItem();
			this.sepPerihelionDate = new System.Windows.Forms.ToolStripSeparator();
			this.mnuLastYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuThisYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNextYear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAfterNextYear = new System.Windows.Forms.ToolStripMenuItem();
			this.sepAdd = new System.Windows.Forms.ToolStripSeparator();
			this.mnuAddThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddOneYear = new System.Windows.Forms.ToolStripMenuItem();
			this.sepSub = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSubThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSubSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSubOneYear = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnShowMenu
			// 
			this.btnShowMenu.ContextMenuStrip = this.ctxMenu;
			this.btnShowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnShowMenu.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnShowMenu.Location = new System.Drawing.Point(0, 0);
			this.btnShowMenu.Name = "btnShowMenu";
			this.btnShowMenu.Size = new System.Drawing.Size(24, 24);
			this.btnShowMenu.TabIndex = 0;
			this.btnShowMenu.Text = "▼";
			this.btnShowMenu.UseVisualStyleBackColor = true;
			this.btnShowMenu.Click += this.btnShowMenu_Click;
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuDefault, this.sepDefault, this.mnuToday, this.sepNow, this.mnuPerihelionDate, this.sepPerihelionDate, this.mnuLastYear, this.mnuThisYear, this.mnuNextYear, this.mnuAfterNextYear, this.sepAdd, this.mnuAddThreeMonths, this.mnuAddSixMonths, this.mnuAddOneYear, this.sepSub, this.mnuSubThreeMonths, this.mnuSubSixMonths, this.mnuSubOneYear });
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ctxMenu.Size = new System.Drawing.Size(211, 320);
			// 
			// mnuDefault
			// 
			this.mnuDefault.MergeIndex = 0;
			this.mnuDefault.Name = "mnuDefault";
			this.mnuDefault.Size = new System.Drawing.Size(210, 22);
			this.mnuDefault.Text = "Default";
			this.mnuDefault.Click += this.mnuCommon_Click;
			// 
			// sepDefault
			// 
			this.sepDefault.MergeIndex = 1;
			this.sepDefault.Name = "sepDefault";
			this.sepDefault.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuToday
			// 
			this.mnuToday.MergeIndex = 2;
			this.mnuToday.Name = "mnuToday";
			this.mnuToday.Size = new System.Drawing.Size(210, 22);
			this.mnuToday.Text = "Today";
			this.mnuToday.Click += this.mnuCommon_Click;
			// 
			// sepNow
			// 
			this.sepNow.MergeIndex = 3;
			this.sepNow.Name = "sepNow";
			this.sepNow.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuPerihelionDate
			// 
			this.mnuPerihelionDate.MergeIndex = 4;
			this.mnuPerihelionDate.Name = "mnuPerihelionDate";
			this.mnuPerihelionDate.Size = new System.Drawing.Size(210, 22);
			this.mnuPerihelionDate.Text = "Perihelion Date";
			this.mnuPerihelionDate.Click += this.mnuCommon_Click;
			// 
			// sepPerihelionDate
			// 
			this.sepPerihelionDate.MergeIndex = 5;
			this.sepPerihelionDate.Name = "sepPerihelionDate";
			this.sepPerihelionDate.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuLastYear
			// 
			this.mnuLastYear.MergeIndex = 6;
			this.mnuLastYear.Name = "mnuLastYear";
			this.mnuLastYear.Size = new System.Drawing.Size(210, 22);
			this.mnuLastYear.Text = "<last year first day>";
			this.mnuLastYear.Click += this.mnuCommon_Click;
			// 
			// mnuThisYear
			// 
			this.mnuThisYear.MergeIndex = 7;
			this.mnuThisYear.Name = "mnuThisYear";
			this.mnuThisYear.Size = new System.Drawing.Size(210, 22);
			this.mnuThisYear.Text = "<this year first day>";
			this.mnuThisYear.Click += this.mnuCommon_Click;
			// 
			// mnuNextYear
			// 
			this.mnuNextYear.MergeIndex = 8;
			this.mnuNextYear.Name = "mnuNextYear";
			this.mnuNextYear.Size = new System.Drawing.Size(210, 22);
			this.mnuNextYear.Text = "<next year first day>";
			this.mnuNextYear.Click += this.mnuCommon_Click;
			// 
			// mnuAfterNextYear
			// 
			this.mnuAfterNextYear.MergeIndex = 9;
			this.mnuAfterNextYear.Name = "mnuAfterNextYear";
			this.mnuAfterNextYear.Size = new System.Drawing.Size(210, 22);
			this.mnuAfterNextYear.Text = "<after next year first day>";
			this.mnuAfterNextYear.Click += this.mnuCommon_Click;
			// 
			// sepAdd
			// 
			this.sepAdd.MergeIndex = 10;
			this.sepAdd.Name = "sepAdd";
			this.sepAdd.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuAddThreeMonths
			// 
			this.mnuAddThreeMonths.MergeIndex = 11;
			this.mnuAddThreeMonths.Name = "mnuAddThreeMonths";
			this.mnuAddThreeMonths.Size = new System.Drawing.Size(210, 22);
			this.mnuAddThreeMonths.Text = "Add 3 months";
			this.mnuAddThreeMonths.Click += this.mnuCommon_Click;
			// 
			// mnuAddSixMonths
			// 
			this.mnuAddSixMonths.MergeIndex = 12;
			this.mnuAddSixMonths.Name = "mnuAddSixMonths";
			this.mnuAddSixMonths.Size = new System.Drawing.Size(210, 22);
			this.mnuAddSixMonths.Text = "Add 6 months";
			this.mnuAddSixMonths.Click += this.mnuCommon_Click;
			// 
			// mnuAddOneYear
			// 
			this.mnuAddOneYear.MergeIndex = 13;
			this.mnuAddOneYear.Name = "mnuAddOneYear";
			this.mnuAddOneYear.Size = new System.Drawing.Size(210, 22);
			this.mnuAddOneYear.Text = "Add 1 year";
			this.mnuAddOneYear.Click += this.mnuCommon_Click;
			// 
			// sepSub
			// 
			this.sepSub.MergeIndex = 14;
			this.sepSub.Name = "sepSub";
			this.sepSub.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuSubThreeMonths
			// 
			this.mnuSubThreeMonths.MergeIndex = 15;
			this.mnuSubThreeMonths.Name = "mnuSubThreeMonths";
			this.mnuSubThreeMonths.Size = new System.Drawing.Size(210, 22);
			this.mnuSubThreeMonths.Text = "Subtract 3 months";
			this.mnuSubThreeMonths.Click += this.mnuCommon_Click;
			// 
			// mnuSubSixMonths
			// 
			this.mnuSubSixMonths.MergeIndex = 16;
			this.mnuSubSixMonths.Name = "mnuSubSixMonths";
			this.mnuSubSixMonths.Size = new System.Drawing.Size(210, 22);
			this.mnuSubSixMonths.Text = "Subtract 6 months";
			this.mnuSubSixMonths.Click += this.mnuCommon_Click;
			// 
			// mnuSubOneYear
			// 
			this.mnuSubOneYear.MergeIndex = 17;
			this.mnuSubOneYear.Name = "mnuSubOneYear";
			this.mnuSubOneYear.Size = new System.Drawing.Size(210, 22);
			this.mnuSubOneYear.Text = "Subtract 1 year";
			this.mnuSubOneYear.Click += this.mnuCommon_Click;
			// 
			// DateTimeMenuControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnShowMenu);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "DateTimeMenuControl";
			this.Size = new System.Drawing.Size(24, 24);
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnShowMenu;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuDefault;
		private System.Windows.Forms.ToolStripSeparator sepDefault;
		private System.Windows.Forms.ToolStripMenuItem mnuAddThreeMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuAddSixMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuAddOneYear;
		private System.Windows.Forms.ToolStripSeparator sepAdd;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihelionDate;
		private System.Windows.Forms.ToolStripMenuItem mnuLastYear;
		private System.Windows.Forms.ToolStripMenuItem mnuThisYear;
		private System.Windows.Forms.ToolStripMenuItem mnuNextYear;
		private System.Windows.Forms.ToolStripSeparator sepPerihelionDate;
		private System.Windows.Forms.ToolStripMenuItem mnuAfterNextYear;
		private System.Windows.Forms.ToolStripMenuItem mnuToday;
		private System.Windows.Forms.ToolStripSeparator sepNow;
		private System.Windows.Forms.ToolStripSeparator sepSub;
		private System.Windows.Forms.ToolStripMenuItem mnuSubThreeMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuSubSixMonths;
		private System.Windows.Forms.ToolStripMenuItem mnuSubOneYear;
	}
}
