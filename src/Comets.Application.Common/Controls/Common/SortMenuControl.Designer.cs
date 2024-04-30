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
			this.components = new System.ComponentModel.Container();
			this.btnSort = new System.Windows.Forms.Button();
			this.contextSort = new System.Windows.Forms.ContextMenuStrip(this.components);
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
			this.mnuSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.mnuAsc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDesc = new System.Windows.Forms.ToolStripMenuItem();
			this.contextSort.SuspendLayout();
			SuspendLayout();
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
			this.btnSort.Click += btnSort_Click;
			// 
			// contextSort
			// 
			this.contextSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuDesig, this.mnuDiscoverer, this.mnuPerihDate, this.mnuPerihDist, this.mnuPerihEarthDist, this.mnuPerihMag, this.mnuCurrSunDist, this.mnuCurrEarthDist, this.mnuCurrMag, this.mnuPeriod, this.mnuAphDistance, this.mnuSemiMajorAxis, this.mnuEcc, this.mnuIncl, this.mnuAscNode, this.mnuArgPeri, this.mnuSeparator, this.mnuAsc, this.mnuDesc });
			this.contextSort.Name = "contextSort";
			this.contextSort.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextSort.Size = new System.Drawing.Size(234, 406);
			// 
			// mnuDesig
			// 
			this.mnuDesig.MergeIndex = 0;
			this.mnuDesig.Name = "mnuDesig";
			this.mnuDesig.Size = new System.Drawing.Size(233, 22);
			this.mnuDesig.Text = "Designation";
			this.mnuDesig.Click += menuItemSortCommon_Click;
			// 
			// mnuDiscoverer
			// 
			this.mnuDiscoverer.MergeIndex = 1;
			this.mnuDiscoverer.Name = "mnuDiscoverer";
			this.mnuDiscoverer.Size = new System.Drawing.Size(233, 22);
			this.mnuDiscoverer.Text = "Discoverer";
			this.mnuDiscoverer.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihDate
			// 
			this.mnuPerihDate.MergeIndex = 2;
			this.mnuPerihDate.Name = "mnuPerihDate";
			this.mnuPerihDate.Size = new System.Drawing.Size(233, 22);
			this.mnuPerihDate.Text = "Perihelion date";
			this.mnuPerihDate.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihDist
			// 
			this.mnuPerihDist.MergeIndex = 3;
			this.mnuPerihDist.Name = "mnuPerihDist";
			this.mnuPerihDist.Size = new System.Drawing.Size(233, 22);
			this.mnuPerihDist.Text = "Perihelion distance";
			this.mnuPerihDist.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihEarthDist
			// 
			this.mnuPerihEarthDist.MergeIndex = 4;
			this.mnuPerihEarthDist.Name = "mnuPerihEarthDist";
			this.mnuPerihEarthDist.Size = new System.Drawing.Size(233, 22);
			this.mnuPerihEarthDist.Text = "Perihelion distance from Earth";
			this.mnuPerihEarthDist.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihMag
			// 
			this.mnuPerihMag.MergeIndex = 5;
			this.mnuPerihMag.Name = "mnuPerihMag";
			this.mnuPerihMag.Size = new System.Drawing.Size(233, 22);
			this.mnuPerihMag.Text = "Perihelion magnitude";
			this.mnuPerihMag.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrSunDist
			// 
			this.mnuCurrSunDist.MergeIndex = 6;
			this.mnuCurrSunDist.Name = "mnuCurrSunDist";
			this.mnuCurrSunDist.Size = new System.Drawing.Size(233, 22);
			this.mnuCurrSunDist.Text = "Current distance from Sun";
			this.mnuCurrSunDist.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrEarthDist
			// 
			this.mnuCurrEarthDist.MergeIndex = 7;
			this.mnuCurrEarthDist.Name = "mnuCurrEarthDist";
			this.mnuCurrEarthDist.Size = new System.Drawing.Size(233, 22);
			this.mnuCurrEarthDist.Text = "Current distance from Earth";
			this.mnuCurrEarthDist.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrMag
			// 
			this.mnuCurrMag.MergeIndex = 8;
			this.mnuCurrMag.Name = "mnuCurrMag";
			this.mnuCurrMag.Size = new System.Drawing.Size(233, 22);
			this.mnuCurrMag.Text = "Current magnitude";
			this.mnuCurrMag.Click += menuItemSortCommon_Click;
			// 
			// mnuPeriod
			// 
			this.mnuPeriod.MergeIndex = 9;
			this.mnuPeriod.Name = "mnuPeriod";
			this.mnuPeriod.Size = new System.Drawing.Size(233, 22);
			this.mnuPeriod.Text = "Period";
			this.mnuPeriod.Click += menuItemSortCommon_Click;
			// 
			// mnuAphDistance
			// 
			this.mnuAphDistance.MergeIndex = 10;
			this.mnuAphDistance.Name = "mnuAphDistance";
			this.mnuAphDistance.Size = new System.Drawing.Size(233, 22);
			this.mnuAphDistance.Text = "Aphelion distance";
			this.mnuAphDistance.Click += menuItemSortCommon_Click;
			// 
			// mnuSemiMajorAxis
			// 
			this.mnuSemiMajorAxis.MergeIndex = 11;
			this.mnuSemiMajorAxis.Name = "mnuSemiMajorAxis";
			this.mnuSemiMajorAxis.Size = new System.Drawing.Size(233, 22);
			this.mnuSemiMajorAxis.Text = "Semi-major axis";
			this.mnuSemiMajorAxis.Click += menuItemSortCommon_Click;
			// 
			// mnuEcc
			// 
			this.mnuEcc.MergeIndex = 12;
			this.mnuEcc.Name = "mnuEcc";
			this.mnuEcc.Size = new System.Drawing.Size(233, 22);
			this.mnuEcc.Text = "Eccentricity";
			this.mnuEcc.Click += menuItemSortCommon_Click;
			// 
			// mnuIncl
			// 
			this.mnuIncl.MergeIndex = 13;
			this.mnuIncl.Name = "mnuIncl";
			this.mnuIncl.Size = new System.Drawing.Size(233, 22);
			this.mnuIncl.Text = "Inclination";
			this.mnuIncl.Click += menuItemSortCommon_Click;
			// 
			// mnuAscNode
			// 
			this.mnuAscNode.MergeIndex = 14;
			this.mnuAscNode.Name = "mnuAscNode";
			this.mnuAscNode.Size = new System.Drawing.Size(233, 22);
			this.mnuAscNode.Text = "Long. of the Asc. Node";
			this.mnuAscNode.Click += menuItemSortCommon_Click;
			// 
			// mnuArgPeri
			// 
			this.mnuArgPeri.MergeIndex = 15;
			this.mnuArgPeri.Name = "mnuArgPeri";
			this.mnuArgPeri.Size = new System.Drawing.Size(233, 22);
			this.mnuArgPeri.Text = "Arg. of Pericenter";
			this.mnuArgPeri.Click += menuItemSortCommon_Click;
			// 
			// mnuSeparator
			// 
			this.mnuSeparator.MergeIndex = 16;
			this.mnuSeparator.Name = "mnuSeparator";
			this.mnuSeparator.Size = new System.Drawing.Size(230, 6);
			// 
			// mnuAsc
			// 
			this.mnuAsc.MergeIndex = 17;
			this.mnuAsc.Name = "mnuAsc";
			this.mnuAsc.Size = new System.Drawing.Size(233, 22);
			this.mnuAsc.Text = "Ascending";
			this.mnuAsc.Click += menuItemSortAscDesc_Click;
			// 
			// mnuDesc
			// 
			this.mnuDesc.MergeIndex = 18;
			this.mnuDesc.Name = "mnuDesc";
			this.mnuDesc.Size = new System.Drawing.Size(233, 22);
			this.mnuDesc.Text = "Descending";
			this.mnuDesc.Click += menuItemSortAscDesc_Click;
			// 
			// SortMenuControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSort);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "SortMenuControl";
			this.Size = new System.Drawing.Size(100, 23);
			this.contextSort.ResumeLayout(false);
			ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripSeparator mnuSeparator;
		private System.Windows.Forms.ToolStripMenuItem mnuAsc;
		private System.Windows.Forms.ToolStripMenuItem mnuDesc;
	}
}
