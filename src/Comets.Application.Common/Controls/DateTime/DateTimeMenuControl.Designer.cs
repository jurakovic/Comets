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
			btnShowMenu = new Button();
			ctxMenu = new ContextMenuStrip(components);
			mnuDefault = new ToolStripMenuItem();
			sepDefault = new ToolStripMenuItem();
			mnuToday = new ToolStripMenuItem();
			sepNow = new ToolStripMenuItem();
			mnuPerihelionDate = new ToolStripMenuItem();
			sepPerihelionDate = new ToolStripMenuItem();
			mnuLastYear = new ToolStripMenuItem();
			mnuThisYear = new ToolStripMenuItem();
			mnuNextYear = new ToolStripMenuItem();
			mnuAfterNextYear = new ToolStripMenuItem();
			sepAdd = new ToolStripMenuItem();
			mnuAddThreeMonths = new ToolStripMenuItem();
			mnuAddSixMonths = new ToolStripMenuItem();
			mnuAddOneYear = new ToolStripMenuItem();
			sepSub = new ToolStripMenuItem();
			mnuSubThreeMonths = new ToolStripMenuItem();
			mnuSubSixMonths = new ToolStripMenuItem();
			mnuSubOneYear = new ToolStripMenuItem();
			ctxMenu.SuspendLayout();
			SuspendLayout();
			// 
			// btnShowMenu
			// 
			btnShowMenu.Dock = DockStyle.Fill;
			btnShowMenu.Font = new Font("Tahoma", 8.25F);
			btnShowMenu.Location = new Point(0, 0);
			btnShowMenu.Name = "btnShowMenu";
			btnShowMenu.Size = new Size(24, 23);
			btnShowMenu.TabIndex = 0;
			btnShowMenu.Text = "▼";
			btnShowMenu.UseVisualStyleBackColor = true;
			btnShowMenu.Click += btnShowMenu_Click;
			// 
			// ctxMenu
			// 
			ctxMenu.Items.AddRange(new ToolStripItem[] { mnuDefault, sepDefault, mnuToday, sepNow, mnuPerihelionDate, sepPerihelionDate, mnuLastYear, mnuThisYear, mnuNextYear, mnuAfterNextYear, sepAdd, mnuAddThreeMonths, mnuAddSixMonths, mnuAddOneYear, sepSub, mnuSubThreeMonths, mnuSubSixMonths, mnuSubOneYear });
			ctxMenu.Name = "ctxMenu";
			ctxMenu.Size = new Size(211, 400);
			// 
			// mnuDefault
			// 
			mnuDefault.MergeIndex = 0;
			mnuDefault.Name = "mnuDefault";
			mnuDefault.Size = new Size(210, 22);
			mnuDefault.Text = "Default";
			mnuDefault.Click += mnuCommon_Click;
			// 
			// sepDefault
			// 
			sepDefault.MergeIndex = 1;
			sepDefault.Name = "sepDefault";
			sepDefault.Size = new Size(210, 22);
			sepDefault.Text = "-";
			// 
			// mnuToday
			// 
			mnuToday.MergeIndex = 2;
			mnuToday.Name = "mnuToday";
			mnuToday.Size = new Size(210, 22);
			mnuToday.Text = "Today";
			mnuToday.Click += mnuCommon_Click;
			// 
			// sepNow
			// 
			sepNow.MergeIndex = 3;
			sepNow.Name = "sepNow";
			sepNow.Size = new Size(210, 22);
			sepNow.Text = "-";
			// 
			// mnuPerihelionDate
			// 
			mnuPerihelionDate.MergeIndex = 4;
			mnuPerihelionDate.Name = "mnuPerihelionDate";
			mnuPerihelionDate.Size = new Size(210, 22);
			mnuPerihelionDate.Text = "Perihelion Date";
			mnuPerihelionDate.Click += mnuCommon_Click;
			// 
			// sepPerihelionDate
			// 
			sepPerihelionDate.MergeIndex = 5;
			sepPerihelionDate.Name = "sepPerihelionDate";
			sepPerihelionDate.Size = new Size(210, 22);
			sepPerihelionDate.Text = "-";
			// 
			// mnuLastYear
			// 
			mnuLastYear.MergeIndex = 6;
			mnuLastYear.Name = "mnuLastYear";
			mnuLastYear.Size = new Size(210, 22);
			mnuLastYear.Text = "<last year first day>";
			mnuLastYear.Click += mnuCommon_Click;
			// 
			// mnuThisYear
			// 
			mnuThisYear.MergeIndex = 7;
			mnuThisYear.Name = "mnuThisYear";
			mnuThisYear.Size = new Size(210, 22);
			mnuThisYear.Text = "<this year first day>";
			mnuThisYear.Click += mnuCommon_Click;
			// 
			// mnuNextYear
			// 
			mnuNextYear.MergeIndex = 8;
			mnuNextYear.Name = "mnuNextYear";
			mnuNextYear.Size = new Size(210, 22);
			mnuNextYear.Text = "<next year first day>";
			mnuNextYear.Click += mnuCommon_Click;
			// 
			// mnuAfterNextYear
			// 
			mnuAfterNextYear.MergeIndex = 9;
			mnuAfterNextYear.Name = "mnuAfterNextYear";
			mnuAfterNextYear.Size = new Size(210, 22);
			mnuAfterNextYear.Text = "<after next year first day>";
			mnuAfterNextYear.Click += mnuCommon_Click;
			// 
			// sepAdd
			// 
			sepAdd.MergeIndex = 10;
			sepAdd.Name = "sepAdd";
			sepAdd.Size = new Size(210, 22);
			sepAdd.Text = "-";
			// 
			// mnuAddThreeMonths
			// 
			mnuAddThreeMonths.MergeIndex = 11;
			mnuAddThreeMonths.Name = "mnuAddThreeMonths";
			mnuAddThreeMonths.Size = new Size(210, 22);
			mnuAddThreeMonths.Text = "Add 3 months";
			mnuAddThreeMonths.Click += mnuCommon_Click;
			// 
			// mnuAddSixMonths
			// 
			mnuAddSixMonths.MergeIndex = 12;
			mnuAddSixMonths.Name = "mnuAddSixMonths";
			mnuAddSixMonths.Size = new Size(210, 22);
			mnuAddSixMonths.Text = "Add 6 months";
			mnuAddSixMonths.Click += mnuCommon_Click;
			// 
			// mnuAddOneYear
			// 
			mnuAddOneYear.MergeIndex = 13;
			mnuAddOneYear.Name = "mnuAddOneYear";
			mnuAddOneYear.Size = new Size(210, 22);
			mnuAddOneYear.Text = "Add 1 year";
			mnuAddOneYear.Click += mnuCommon_Click;
			// 
			// sepSub
			// 
			sepSub.MergeIndex = 14;
			sepSub.Name = "sepSub";
			sepSub.Size = new Size(210, 22);
			sepSub.Text = "-";
			// 
			// mnuSubThreeMonths
			// 
			mnuSubThreeMonths.MergeIndex = 15;
			mnuSubThreeMonths.Name = "mnuSubThreeMonths";
			mnuSubThreeMonths.Size = new Size(210, 22);
			mnuSubThreeMonths.Text = "Subtract 3 months";
			mnuSubThreeMonths.Click += mnuCommon_Click;
			// 
			// mnuSubSixMonths
			// 
			mnuSubSixMonths.MergeIndex = 16;
			mnuSubSixMonths.Name = "mnuSubSixMonths";
			mnuSubSixMonths.Size = new Size(210, 22);
			mnuSubSixMonths.Text = "Subtract 6 months";
			mnuSubSixMonths.Click += mnuCommon_Click;
			// 
			// mnuSubOneYear
			// 
			mnuSubOneYear.MergeIndex = 17;
			mnuSubOneYear.Name = "mnuSubOneYear";
			mnuSubOneYear.Size = new Size(210, 22);
			mnuSubOneYear.Text = "Subtract 1 year";
			mnuSubOneYear.Click += mnuCommon_Click;
			// 
			// DateTimeMenuControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnShowMenu);
			Font = new Font("Tahoma", 8.25F);
			Name = "DateTimeMenuControl";
			Size = new Size(24, 23);
			ctxMenu.ResumeLayout(false);
			ResumeLayout(false);
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
