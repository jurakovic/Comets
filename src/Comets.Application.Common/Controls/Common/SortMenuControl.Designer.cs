namespace Comets.Application.Common.Controls.Common
{
	partial class SortMenuControl
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
			this.btnSort = new System.Windows.Forms.Button();
			this.contextSort = new System.Windows.Forms.ContextMenu();
			this.mnuDesig = new System.Windows.Forms.MenuItem();
			this.mnuDiscoverer = new System.Windows.Forms.MenuItem();
			this.mnuPerihDate = new System.Windows.Forms.MenuItem();
			this.mnuPerihDist = new System.Windows.Forms.MenuItem();
			this.mnuPerihEarthDist = new System.Windows.Forms.MenuItem();
			this.mnuPerihMag = new System.Windows.Forms.MenuItem();
			this.mnuCurrSunDist = new System.Windows.Forms.MenuItem();
			this.mnuCurrEarthDist = new System.Windows.Forms.MenuItem();
			this.mnuCurrMag = new System.Windows.Forms.MenuItem();
			this.mnuPeriod = new System.Windows.Forms.MenuItem();
			this.mnuAphDistance = new System.Windows.Forms.MenuItem();
			this.mnuSemiMajorAxis = new System.Windows.Forms.MenuItem();
			this.mnuEcc = new System.Windows.Forms.MenuItem();
			this.mnuIncl = new System.Windows.Forms.MenuItem();
			this.mnuAscNode = new System.Windows.Forms.MenuItem();
			this.mnuArgPeri = new System.Windows.Forms.MenuItem();
			this.mnuSeparator = new System.Windows.Forms.MenuItem();
			this.mnuAsc = new System.Windows.Forms.MenuItem();
			this.mnuDesc = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// btnSort
			// 
			this.btnSort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSort.Location = new System.Drawing.Point(0, 0);
			this.btnSort.Name = "btnSort";
			this.btnSort.Size = new System.Drawing.Size(100, 23);
			this.btnSort.TabIndex = 4;
			this.btnSort.Text = "Sort by";
			this.btnSort.UseVisualStyleBackColor = true;
			this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
			// 
			// contextSort
			// 
			this.contextSort.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDesig,
            this.mnuDiscoverer,
            this.mnuPerihDate,
            this.mnuPerihDist,
            this.mnuPerihEarthDist,
            this.mnuPerihMag,
            this.mnuCurrSunDist,
            this.mnuCurrEarthDist,
            this.mnuCurrMag,
            this.mnuPeriod,
            this.mnuAphDistance,
            this.mnuSemiMajorAxis,
            this.mnuEcc,
            this.mnuIncl,
            this.mnuAscNode,
            this.mnuArgPeri,
            this.mnuSeparator,
            this.mnuAsc,
            this.mnuDesc});
			// 
			// mnuDesig
			// 
			this.mnuDesig.Index = 0;
			this.mnuDesig.RadioCheck = true;
			this.mnuDesig.Text = "Designation";
			this.mnuDesig.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuDiscoverer
			// 
			this.mnuDiscoverer.Index = 1;
			this.mnuDiscoverer.RadioCheck = true;
			this.mnuDiscoverer.Text = "Discoverer";
			this.mnuDiscoverer.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDate
			// 
			this.mnuPerihDate.Index = 2;
			this.mnuPerihDate.RadioCheck = true;
			this.mnuPerihDate.Text = "Perihelion date";
			this.mnuPerihDate.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDist
			// 
			this.mnuPerihDist.Index = 3;
			this.mnuPerihDist.Text = "Perihelion distance";
			this.mnuPerihDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihEarthDist
			// 
			this.mnuPerihEarthDist.Index = 4;
			this.mnuPerihEarthDist.Text = "Perihelion distance from Earth";
			this.mnuPerihEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihMag
			// 
			this.mnuPerihMag.Index = 5;
			this.mnuPerihMag.Text = "Perihelion magnitude";
			this.mnuPerihMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrSunDist
			// 
			this.mnuCurrSunDist.Index = 6;
			this.mnuCurrSunDist.Text = "Current distance from Sun";
			this.mnuCurrSunDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrEarthDist
			// 
			this.mnuCurrEarthDist.Index = 7;
			this.mnuCurrEarthDist.Text = "Current distance from Earth";
			this.mnuCurrEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrMag
			// 
			this.mnuCurrMag.Index = 8;
			this.mnuCurrMag.Text = "Current magnitude";
			this.mnuCurrMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPeriod
			// 
			this.mnuPeriod.Index = 9;
			this.mnuPeriod.RadioCheck = true;
			this.mnuPeriod.Text = "Period";
			this.mnuPeriod.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAphDistance
			// 
			this.mnuAphDistance.Index = 10;
			this.mnuAphDistance.Text = "Aphelion distance";
			this.mnuAphDistance.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSemiMajorAxis
			// 
			this.mnuSemiMajorAxis.Index = 11;
			this.mnuSemiMajorAxis.Text = "Semi-major axis";
			this.mnuSemiMajorAxis.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuEcc
			// 
			this.mnuEcc.Index = 12;
			this.mnuEcc.RadioCheck = true;
			this.mnuEcc.Text = "Eccentricity";
			this.mnuEcc.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuIncl
			// 
			this.mnuIncl.Index = 13;
			this.mnuIncl.RadioCheck = true;
			this.mnuIncl.Text = "Inclination";
			this.mnuIncl.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAscNode
			// 
			this.mnuAscNode.Index = 14;
			this.mnuAscNode.RadioCheck = true;
			this.mnuAscNode.Text = "Long. of the Asc. Node";
			this.mnuAscNode.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuArgPeri
			// 
			this.mnuArgPeri.Index = 15;
			this.mnuArgPeri.RadioCheck = true;
			this.mnuArgPeri.Text = "Arg. of Pericenter";
			this.mnuArgPeri.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSeparator
			// 
			this.mnuSeparator.Index = 16;
			this.mnuSeparator.Text = "-";
			// 
			// mnuAsc
			// 
			this.mnuAsc.Index = 17;
			this.mnuAsc.RadioCheck = true;
			this.mnuAsc.Text = "Ascending";
			this.mnuAsc.Click += new System.EventHandler(this.menuItemSortAscDesc_Click);
			// 
			// mnuDesc
			// 
			this.mnuDesc.Index = 18;
			this.mnuDesc.RadioCheck = true;
			this.mnuDesc.Text = "Descending";
			this.mnuDesc.Click += new System.EventHandler(this.menuItemSortAscDesc_Click);
			// 
			// SortMenuControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSort);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "SortMenuControl";
			this.Size = new System.Drawing.Size(100, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.ContextMenu contextSort;
		private System.Windows.Forms.MenuItem mnuDesig;
		private System.Windows.Forms.MenuItem mnuDiscoverer;
		private System.Windows.Forms.MenuItem mnuPerihDate;
		private System.Windows.Forms.MenuItem mnuPerihDist;
		private System.Windows.Forms.MenuItem mnuPerihEarthDist;
		private System.Windows.Forms.MenuItem mnuPerihMag;
		private System.Windows.Forms.MenuItem mnuCurrSunDist;
		private System.Windows.Forms.MenuItem mnuCurrEarthDist;
		private System.Windows.Forms.MenuItem mnuCurrMag;
		private System.Windows.Forms.MenuItem mnuPeriod;
		private System.Windows.Forms.MenuItem mnuAphDistance;
		private System.Windows.Forms.MenuItem mnuSemiMajorAxis;
		private System.Windows.Forms.MenuItem mnuEcc;
		private System.Windows.Forms.MenuItem mnuIncl;
		private System.Windows.Forms.MenuItem mnuAscNode;
		private System.Windows.Forms.MenuItem mnuArgPeri;
		private System.Windows.Forms.MenuItem mnuSeparator;
		private System.Windows.Forms.MenuItem mnuAsc;
		private System.Windows.Forms.MenuItem mnuDesc;
	}
}
