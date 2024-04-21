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
			this.contextSort = new System.Windows.Forms.ContextMenuStrip();
			this.mnuDesig = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDiscoverer = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPerihDate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPerihDist = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPerihEarthDist = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPerihMag = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCurrSunDist = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCurrEarthDist = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCurrMag = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPeriod = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAphDistance = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSemiMajorAxis = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEcc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuIncl = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAscNode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuArgPeri = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSeparator = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAsc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDesc = new System.Windows.Forms.ToolStripMenuItem();
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
			this.contextSort.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
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
			this.mnuDesig.Text = "Designation";
			this.mnuDesig.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuDiscoverer
			// 
			this.mnuDiscoverer.Text = "Discoverer";
			this.mnuDiscoverer.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDate
			// 
			this.mnuPerihDate.Text = "Perihelion date";
			this.mnuPerihDate.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDist
			// 
			this.mnuPerihDist.Text = "Perihelion distance";
			this.mnuPerihDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihEarthDist
			// 
			this.mnuPerihEarthDist.Text = "Perihelion distance from Earth";
			this.mnuPerihEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihMag
			// 
			this.mnuPerihMag.Text = "Perihelion magnitude";
			this.mnuPerihMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrSunDist
			// 
			this.mnuCurrSunDist.Text = "Current distance from Sun";
			this.mnuCurrSunDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrEarthDist
			// 
			this.mnuCurrEarthDist.Text = "Current distance from Earth";
			this.mnuCurrEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrMag
			// 
			this.mnuCurrMag.Text = "Current magnitude";
			this.mnuCurrMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPeriod
			// 
			this.mnuPeriod.Text = "Period";
			this.mnuPeriod.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAphDistance
			// 
			this.mnuAphDistance.Text = "Aphelion distance";
			this.mnuAphDistance.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSemiMajorAxis
			// 
			this.mnuSemiMajorAxis.Text = "Semi-major axis";
			this.mnuSemiMajorAxis.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuEcc
			// 
			this.mnuEcc.Text = "Eccentricity";
			this.mnuEcc.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuIncl
			// 
			this.mnuIncl.Text = "Inclination";
			this.mnuIncl.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAscNode
			// 
			this.mnuAscNode.Text = "Long. of the Asc. Node";
			this.mnuAscNode.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuArgPeri
			// 

			this.mnuArgPeri.Text = "Arg. of Pericenter";
			this.mnuArgPeri.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSeparator
			// 
			this.mnuSeparator.Text = "-";
			// 
			// mnuAsc
			// 
			this.mnuAsc.Text = "Ascending";
			this.mnuAsc.Click += new System.EventHandler(this.menuItemSortAscDesc_Click);
			// 
			// mnuDesc
			// 
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
		private System.Windows.Forms.ContextMenuStrip contextSort;
		private System.Windows.Forms.ToolStripMenuItem mnuDesig;
		private System.Windows.Forms.ToolStripMenuItem mnuDiscoverer;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihDate;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihDist;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihEarthDist;
		private System.Windows.Forms.ToolStripMenuItem mnuPerihMag;
		private System.Windows.Forms.ToolStripMenuItem mnuCurrSunDist;
		private System.Windows.Forms.ToolStripMenuItem mnuCurrEarthDist;
		private System.Windows.Forms.ToolStripMenuItem mnuCurrMag;
		private System.Windows.Forms.ToolStripMenuItem mnuPeriod;
		private System.Windows.Forms.ToolStripMenuItem mnuAphDistance;
		private System.Windows.Forms.ToolStripMenuItem mnuSemiMajorAxis;
		private System.Windows.Forms.ToolStripMenuItem mnuEcc;
		private System.Windows.Forms.ToolStripMenuItem mnuIncl;
		private System.Windows.Forms.ToolStripMenuItem mnuAscNode;
		private System.Windows.Forms.ToolStripMenuItem mnuArgPeri;
		private System.Windows.Forms.ToolStripMenuItem mnuSeparator;
		private System.Windows.Forms.ToolStripMenuItem mnuAsc;
		private System.Windows.Forms.ToolStripMenuItem mnuDesc;
	}
}
