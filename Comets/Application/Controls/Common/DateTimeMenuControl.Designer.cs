namespace Comets.Application.Controls.Common
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
			this.ctxMenu = new System.Windows.Forms.ContextMenu();
			this.mnuDefault = new System.Windows.Forms.MenuItem();
			this.sepDefault = new System.Windows.Forms.MenuItem();
			this.mnuPerihelionDate = new System.Windows.Forms.MenuItem();
			this.sepPerihelionDate = new System.Windows.Forms.MenuItem();
			this.mnuLastYear = new System.Windows.Forms.MenuItem();
			this.mnuThisYear = new System.Windows.Forms.MenuItem();
			this.mnuNextYear = new System.Windows.Forms.MenuItem();
			this.mnuAfterNextYear = new System.Windows.Forms.MenuItem();
			this.sepAdd = new System.Windows.Forms.MenuItem();
			this.mnuAddThreeMonths = new System.Windows.Forms.MenuItem();
			this.mnuAddSixMonth = new System.Windows.Forms.MenuItem();
			this.mnuAddOneYear = new System.Windows.Forms.MenuItem();
			this.mnuNow = new System.Windows.Forms.MenuItem();
			this.sepNow = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// btnShowMenu
			// 
			this.btnShowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnShowMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.ctxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDefault,
            this.sepDefault,
            this.mnuNow,
            this.sepNow,
            this.mnuPerihelionDate,
            this.sepPerihelionDate,
            this.mnuLastYear,
            this.mnuThisYear,
            this.mnuNextYear,
            this.mnuAfterNextYear,
            this.sepAdd,
            this.mnuAddThreeMonths,
            this.mnuAddSixMonth,
            this.mnuAddOneYear});
			// 
			// mnuDefault
			// 
			this.mnuDefault.Index = 0;
			this.mnuDefault.Text = "Default";
			this.mnuDefault.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepDefault
			// 
			this.sepDefault.Index = 1;
			this.sepDefault.Text = "-";
			// 
			// mnuPerihelionDate
			// 
			this.mnuPerihelionDate.Index = 4;
			this.mnuPerihelionDate.Text = "Perihelion Date";
			this.mnuPerihelionDate.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepPerihelionDate
			// 
			this.sepPerihelionDate.Index = 5;
			this.sepPerihelionDate.Text = "-";
			// 
			// mnuLastYear
			// 
			this.mnuLastYear.Index = 6;
			this.mnuLastYear.Text = "<last year first day>";
			this.mnuLastYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuThisYear
			// 
			this.mnuThisYear.Index = 7;
			this.mnuThisYear.Text = "<this year first day>";
			this.mnuThisYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuNextYear
			// 
			this.mnuNextYear.Index = 8;
			this.mnuNextYear.Text = "<next year first day>";
			this.mnuNextYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAfterNextYear
			// 
			this.mnuAfterNextYear.Index = 9;
			this.mnuAfterNextYear.Text = "<after next year first day>";
			this.mnuAfterNextYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepAdd
			// 
			this.sepAdd.Index = 10;
			this.sepAdd.Text = "-";
			// 
			// mnuAddThreeMonths
			// 
			this.mnuAddThreeMonths.Index = 11;
			this.mnuAddThreeMonths.Text = "Add 3 months";
			this.mnuAddThreeMonths.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAddSixMonth
			// 
			this.mnuAddSixMonth.Index = 12;
			this.mnuAddSixMonth.Text = "Add 6 months";
			this.mnuAddSixMonth.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuAddOneYear
			// 
			this.mnuAddOneYear.Index = 13;
			this.mnuAddOneYear.Text = "Add 1 year";
			this.mnuAddOneYear.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuNow
			// 
			this.mnuNow.Index = 2;
			this.mnuNow.Text = "Now";
			this.mnuNow.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// sepNow
			// 
			this.sepNow.Index = 3;
			this.sepNow.Text = "-";
			// 
			// DateTimeMenuControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnShowMenu);
			this.Name = "DateTimeMenuControl";
			this.Size = new System.Drawing.Size(24, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnShowMenu;
		private System.Windows.Forms.ContextMenu ctxMenu;
		private System.Windows.Forms.MenuItem mnuDefault;
		private System.Windows.Forms.MenuItem sepDefault;
		private System.Windows.Forms.MenuItem mnuAddThreeMonths;
		private System.Windows.Forms.MenuItem mnuAddSixMonth;
		private System.Windows.Forms.MenuItem mnuAddOneYear;
		private System.Windows.Forms.MenuItem sepAdd;
		private System.Windows.Forms.MenuItem mnuPerihelionDate;
		private System.Windows.Forms.MenuItem mnuLastYear;
		private System.Windows.Forms.MenuItem mnuThisYear;
		private System.Windows.Forms.MenuItem mnuNextYear;
		private System.Windows.Forms.MenuItem sepPerihelionDate;
		private System.Windows.Forms.MenuItem mnuAfterNextYear;
		private System.Windows.Forms.MenuItem mnuNow;
		private System.Windows.Forms.MenuItem sepNow;
	}
}
