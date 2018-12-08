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
			this.separator1 = new System.Windows.Forms.MenuItem();
			this.mnuNow = new System.Windows.Forms.MenuItem();
			this.separator2 = new System.Windows.Forms.MenuItem();
			this.mnuMidnight = new System.Windows.Forms.MenuItem();
			this.mnuNoon = new System.Windows.Forms.MenuItem();
			this.separator3 = new System.Windows.Forms.MenuItem();
			this.mnuLastYearFirstDay = new System.Windows.Forms.MenuItem();
			this.mnuLastYearLastDay = new System.Windows.Forms.MenuItem();
			this.separator4 = new System.Windows.Forms.MenuItem();
			this.mnuThisYearFirstDay = new System.Windows.Forms.MenuItem();
			this.mnuThisYearLastDay = new System.Windows.Forms.MenuItem();
			this.separator5 = new System.Windows.Forms.MenuItem();
			this.mnuNextYearFirstDay = new System.Windows.Forms.MenuItem();
			this.mnuNextYearLastDay = new System.Windows.Forms.MenuItem();
			this.separator6 = new System.Windows.Forms.MenuItem();
			this.mnuPerihelionDate = new System.Windows.Forms.MenuItem();
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
            this.separator1,
            this.mnuNow,
            this.separator2,
            this.mnuMidnight,
            this.mnuNoon,
            this.separator3,
            this.mnuLastYearFirstDay,
            this.mnuLastYearLastDay,
            this.separator4,
            this.mnuThisYearFirstDay,
            this.mnuThisYearLastDay,
            this.separator5,
            this.mnuNextYearFirstDay,
            this.mnuNextYearLastDay,
            this.separator6,
            this.mnuPerihelionDate});
			// 
			// mnuDefault
			// 
			this.mnuDefault.Index = 0;
			this.mnuDefault.Text = "Default";
			this.mnuDefault.Click += new System.EventHandler(this.mnuCommon_Click);
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
			this.mnuNow.Click += new System.EventHandler(this.mnuCommon_Click);
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
			this.mnuMidnight.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuNoon
			// 
			this.mnuNoon.Index = 5;
			this.mnuNoon.Text = "Noon";
			this.mnuNoon.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// separator3
			// 
			this.separator3.Index = 6;
			this.separator3.Text = "-";
			// 
			// mnuLastYearFirstDay
			// 
			this.mnuLastYearFirstDay.Index = 7;
			this.mnuLastYearFirstDay.Text = "Last year: first day";
			this.mnuLastYearFirstDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuLastYearLastDay
			// 
			this.mnuLastYearLastDay.Index = 8;
			this.mnuLastYearLastDay.Text = "Last year: last day";
			this.mnuLastYearLastDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// separator4
			// 
			this.separator4.Index = 9;
			this.separator4.Text = "-";
			// 
			// mnuThisYearFirstDay
			// 
			this.mnuThisYearFirstDay.Index = 10;
			this.mnuThisYearFirstDay.Text = "This year: first day";
			this.mnuThisYearFirstDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuThisYearLastDay
			// 
			this.mnuThisYearLastDay.Index = 11;
			this.mnuThisYearLastDay.Text = "This year: last day";
			this.mnuThisYearLastDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// separator5
			// 
			this.separator5.Index = 12;
			this.separator5.Text = "-";
			// 
			// mnuNextYearFirstDay
			// 
			this.mnuNextYearFirstDay.Index = 13;
			this.mnuNextYearFirstDay.Text = "Next year: first day";
			this.mnuNextYearFirstDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuNextYearLastDay
			// 
			this.mnuNextYearLastDay.Index = 14;
			this.mnuNextYearLastDay.Text = "Next year: last day";
			this.mnuNextYearLastDay.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// separator6
			// 
			this.separator6.Index = 15;
			this.separator6.Text = "-";
			// 
			// mnuPerihelionDate
			// 
			this.mnuPerihelionDate.Index = 16;
			this.mnuPerihelionDate.Text = "Perihelion Date";
			this.mnuPerihelionDate.Click += new System.EventHandler(this.mnuCommon_Click);
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
		private System.Windows.Forms.MenuItem separator1;
		private System.Windows.Forms.MenuItem mnuNow;
		private System.Windows.Forms.MenuItem separator2;
		private System.Windows.Forms.MenuItem mnuMidnight;
		private System.Windows.Forms.MenuItem mnuNoon;
		private System.Windows.Forms.MenuItem separator3;
		private System.Windows.Forms.MenuItem mnuPerihelionDate;
		private System.Windows.Forms.MenuItem mnuLastYearFirstDay;
		private System.Windows.Forms.MenuItem mnuLastYearLastDay;
		private System.Windows.Forms.MenuItem separator4;
		private System.Windows.Forms.MenuItem mnuThisYearFirstDay;
		private System.Windows.Forms.MenuItem mnuThisYearLastDay;
		private System.Windows.Forms.MenuItem separator5;
		private System.Windows.Forms.MenuItem mnuNextYearFirstDay;
		private System.Windows.Forms.MenuItem mnuNextYearLastDay;
		private System.Windows.Forms.MenuItem separator6;
	}
}
