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
			components = new System.ComponentModel.Container();
			btnShowMenu = new System.Windows.Forms.Button();
			ctxMenu = new System.Windows.Forms.ContextMenuStrip(components);
			mnuDefault = new System.Windows.Forms.ToolStripMenuItem();
			sepDefault = new System.Windows.Forms.ToolStripSeparator();
			mnuToday = new System.Windows.Forms.ToolStripMenuItem();
			sepNow = new System.Windows.Forms.ToolStripSeparator();
			mnuPerihelionDate = new System.Windows.Forms.ToolStripMenuItem();
			sepPerihelionDate = new System.Windows.Forms.ToolStripSeparator();
			mnuLastYear = new System.Windows.Forms.ToolStripMenuItem();
			mnuThisYear = new System.Windows.Forms.ToolStripMenuItem();
			mnuNextYear = new System.Windows.Forms.ToolStripMenuItem();
			mnuAfterNextYear = new System.Windows.Forms.ToolStripMenuItem();
			sepAdd = new System.Windows.Forms.ToolStripSeparator();
			mnuAddThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			mnuAddSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			mnuAddOneYear = new System.Windows.Forms.ToolStripMenuItem();
			sepSub = new System.Windows.Forms.ToolStripSeparator();
			mnuSubThreeMonths = new System.Windows.Forms.ToolStripMenuItem();
			mnuSubSixMonths = new System.Windows.Forms.ToolStripMenuItem();
			mnuSubOneYear = new System.Windows.Forms.ToolStripMenuItem();
			ctxMenu.SuspendLayout();
			SuspendLayout();
			// 
			// btnShowMenu
			// 
			btnShowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			btnShowMenu.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnShowMenu.Location = new System.Drawing.Point(0, 0);
			btnShowMenu.Name = "btnShowMenu";
			btnShowMenu.Size = new System.Drawing.Size(24, 23);
			btnShowMenu.TabIndex = 0;
			btnShowMenu.Text = "▼";
			btnShowMenu.UseVisualStyleBackColor = true;
			btnShowMenu.Click += btnShowMenu_Click;
			// 
			// ctxMenu
			// 
			ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuDefault, sepDefault, mnuToday, sepNow, mnuPerihelionDate, sepPerihelionDate, mnuLastYear, mnuThisYear, mnuNextYear, mnuAfterNextYear, sepAdd, mnuAddThreeMonths, mnuAddSixMonths, mnuAddOneYear, sepSub, mnuSubThreeMonths, mnuSubSixMonths, mnuSubOneYear });
			ctxMenu.Name = "ctxMenu";
			ctxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			ctxMenu.Size = new System.Drawing.Size(211, 400);
			// 
			// mnuDefault
			// 
			mnuDefault.MergeIndex = 0;
			mnuDefault.Name = "mnuDefault";
			mnuDefault.Size = new System.Drawing.Size(210, 22);
			mnuDefault.Text = "Default";
			mnuDefault.Click += mnuCommon_Click;
			// 
			// sepDefault
			// 
			sepDefault.MergeIndex = 1;
			sepDefault.Name = "sepDefault";
			sepDefault.Size = new System.Drawing.Size(210, 22);
			// 
			// mnuToday
			// 
			mnuToday.MergeIndex = 2;
			mnuToday.Name = "mnuToday";
			mnuToday.Size = new System.Drawing.Size(210, 22);
			mnuToday.Text = "Today";
			mnuToday.Click += mnuCommon_Click;
			// 
			// sepNow
			// 
			sepNow.MergeIndex = 3;
			sepNow.Name = "sepNow";
			sepNow.Size = new System.Drawing.Size(210, 22);
			// 
			// mnuPerihelionDate
			// 
			mnuPerihelionDate.MergeIndex = 4;
			mnuPerihelionDate.Name = "mnuPerihelionDate";
			mnuPerihelionDate.Size = new System.Drawing.Size(210, 22);
			mnuPerihelionDate.Text = "Perihelion Date";
			mnuPerihelionDate.Click += mnuCommon_Click;
			// 
			// sepPerihelionDate
			// 
			sepPerihelionDate.MergeIndex = 5;
			sepPerihelionDate.Name = "sepPerihelionDate";
			sepPerihelionDate.Size = new System.Drawing.Size(210, 22);
			// 
			// mnuLastYear
			// 
			mnuLastYear.MergeIndex = 6;
			mnuLastYear.Name = "mnuLastYear";
			mnuLastYear.Size = new System.Drawing.Size(210, 22);
			mnuLastYear.Text = "<last year first day>";
			mnuLastYear.Click += mnuCommon_Click;
			// 
			// mnuThisYear
			// 
			mnuThisYear.MergeIndex = 7;
			mnuThisYear.Name = "mnuThisYear";
			mnuThisYear.Size = new System.Drawing.Size(210, 22);
			mnuThisYear.Text = "<this year first day>";
			mnuThisYear.Click += mnuCommon_Click;
			// 
			// mnuNextYear
			// 
			mnuNextYear.MergeIndex = 8;
			mnuNextYear.Name = "mnuNextYear";
			mnuNextYear.Size = new System.Drawing.Size(210, 22);
			mnuNextYear.Text = "<next year first day>";
			mnuNextYear.Click += mnuCommon_Click;
			// 
			// mnuAfterNextYear
			// 
			mnuAfterNextYear.MergeIndex = 9;
			mnuAfterNextYear.Name = "mnuAfterNextYear";
			mnuAfterNextYear.Size = new System.Drawing.Size(210, 22);
			mnuAfterNextYear.Text = "<after next year first day>";
			mnuAfterNextYear.Click += mnuCommon_Click;
			// 
			// sepAdd
			// 
			sepAdd.MergeIndex = 10;
			sepAdd.Name = "sepAdd";
			sepAdd.Size = new System.Drawing.Size(210, 22);
			// 
			// mnuAddThreeMonths
			// 
			mnuAddThreeMonths.MergeIndex = 11;
			mnuAddThreeMonths.Name = "mnuAddThreeMonths";
			mnuAddThreeMonths.Size = new System.Drawing.Size(210, 22);
			mnuAddThreeMonths.Text = "Add 3 months";
			mnuAddThreeMonths.Click += mnuCommon_Click;
			// 
			// mnuAddSixMonths
			// 
			mnuAddSixMonths.MergeIndex = 12;
			mnuAddSixMonths.Name = "mnuAddSixMonths";
			mnuAddSixMonths.Size = new System.Drawing.Size(210, 22);
			mnuAddSixMonths.Text = "Add 6 months";
			mnuAddSixMonths.Click += mnuCommon_Click;
			// 
			// mnuAddOneYear
			// 
			mnuAddOneYear.MergeIndex = 13;
			mnuAddOneYear.Name = "mnuAddOneYear";
			mnuAddOneYear.Size = new System.Drawing.Size(210, 22);
			mnuAddOneYear.Text = "Add 1 year";
			mnuAddOneYear.Click += mnuCommon_Click;
			// 
			// sepSub
			// 
			sepSub.MergeIndex = 14;
			sepSub.Name = "sepSub";
			sepSub.Size = new System.Drawing.Size(210, 22);
			// 
			// mnuSubThreeMonths
			// 
			mnuSubThreeMonths.MergeIndex = 15;
			mnuSubThreeMonths.Name = "mnuSubThreeMonths";
			mnuSubThreeMonths.Size = new System.Drawing.Size(210, 22);
			mnuSubThreeMonths.Text = "Subtract 3 months";
			mnuSubThreeMonths.Click += mnuCommon_Click;
			// 
			// mnuSubSixMonths
			// 
			mnuSubSixMonths.MergeIndex = 16;
			mnuSubSixMonths.Name = "mnuSubSixMonths";
			mnuSubSixMonths.Size = new System.Drawing.Size(210, 22);
			mnuSubSixMonths.Text = "Subtract 6 months";
			mnuSubSixMonths.Click += mnuCommon_Click;
			// 
			// mnuSubOneYear
			// 
			mnuSubOneYear.MergeIndex = 17;
			mnuSubOneYear.Name = "mnuSubOneYear";
			mnuSubOneYear.Size = new System.Drawing.Size(210, 22);
			mnuSubOneYear.Text = "Subtract 1 year";
			mnuSubOneYear.Click += mnuCommon_Click;
			// 
			// DateTimeMenuControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(btnShowMenu);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "DateTimeMenuControl";
			Size = new System.Drawing.Size(24, 23);
			ctxMenu.ResumeLayout(false);
			ResumeLayout(false);
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
