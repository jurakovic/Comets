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
			components = new System.ComponentModel.Container();
			btnSort = new Button();
			contextSort = new ContextMenuStrip(components);
			mnuDesig = new ToolStripMenuItem();
			mnuDiscoverer = new ToolStripMenuItem();
			mnuPerihDate = new ToolStripMenuItem();
			mnuPerihDist = new ToolStripMenuItem();
			mnuPerihEarthDist = new ToolStripMenuItem();
			mnuPerihMag = new ToolStripMenuItem();
			mnuCurrSunDist = new ToolStripMenuItem();
			mnuCurrEarthDist = new ToolStripMenuItem();
			mnuCurrMag = new ToolStripMenuItem();
			mnuPeriod = new ToolStripMenuItem();
			mnuAphDistance = new ToolStripMenuItem();
			mnuSemiMajorAxis = new ToolStripMenuItem();
			mnuEcc = new ToolStripMenuItem();
			mnuIncl = new ToolStripMenuItem();
			mnuAscNode = new ToolStripMenuItem();
			mnuArgPeri = new ToolStripMenuItem();
			mnuSeparator = new ToolStripSeparator();
			mnuAsc = new ToolStripMenuItem();
			mnuDesc = new ToolStripMenuItem();
			contextSort.SuspendLayout();
			SuspendLayout();
			// 
			// btnSort
			// 
			btnSort.Dock = DockStyle.Fill;
			btnSort.Location = new Point(0, 0);
			btnSort.Name = "btnSort";
			btnSort.Size = new Size(100, 23);
			btnSort.TabIndex = 4;
			btnSort.Text = "Sort by";
			btnSort.UseVisualStyleBackColor = true;
			btnSort.Click += btnSort_Click;
			// 
			// contextSort
			// 
			contextSort.Items.AddRange(new ToolStripItem[] { mnuDesig, mnuDiscoverer, mnuPerihDate, mnuPerihDist, mnuPerihEarthDist, mnuPerihMag, mnuCurrSunDist, mnuCurrEarthDist, mnuCurrMag, mnuPeriod, mnuAphDistance, mnuSemiMajorAxis, mnuEcc, mnuIncl, mnuAscNode, mnuArgPeri, mnuSeparator, mnuAsc, mnuDesc });
			contextSort.Name = "contextSort";
			contextSort.RenderMode = ToolStripRenderMode.System;
			contextSort.Size = new Size(234, 422);
			// 
			// mnuDesig
			// 
			mnuDesig.MergeIndex = 0;
			mnuDesig.Name = "mnuDesig";
			mnuDesig.Size = new Size(233, 22);
			mnuDesig.Text = "Designation";
			mnuDesig.Click += menuItemSortCommon_Click;
			// 
			// mnuDiscoverer
			// 
			mnuDiscoverer.MergeIndex = 1;
			mnuDiscoverer.Name = "mnuDiscoverer";
			mnuDiscoverer.Size = new Size(233, 22);
			mnuDiscoverer.Text = "Discoverer";
			mnuDiscoverer.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihDate
			// 
			mnuPerihDate.MergeIndex = 2;
			mnuPerihDate.Name = "mnuPerihDate";
			mnuPerihDate.Size = new Size(233, 22);
			mnuPerihDate.Text = "Perihelion date";
			mnuPerihDate.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihDist
			// 
			mnuPerihDist.MergeIndex = 3;
			mnuPerihDist.Name = "mnuPerihDist";
			mnuPerihDist.Size = new Size(233, 22);
			mnuPerihDist.Text = "Perihelion distance";
			mnuPerihDist.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihEarthDist
			// 
			mnuPerihEarthDist.MergeIndex = 4;
			mnuPerihEarthDist.Name = "mnuPerihEarthDist";
			mnuPerihEarthDist.Size = new Size(233, 22);
			mnuPerihEarthDist.Text = "Perihelion distance from Earth";
			mnuPerihEarthDist.Click += menuItemSortCommon_Click;
			// 
			// mnuPerihMag
			// 
			mnuPerihMag.MergeIndex = 5;
			mnuPerihMag.Name = "mnuPerihMag";
			mnuPerihMag.Size = new Size(233, 22);
			mnuPerihMag.Text = "Perihelion magnitude";
			mnuPerihMag.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrSunDist
			// 
			mnuCurrSunDist.MergeIndex = 6;
			mnuCurrSunDist.Name = "mnuCurrSunDist";
			mnuCurrSunDist.Size = new Size(233, 22);
			mnuCurrSunDist.Text = "Current distance from Sun";
			mnuCurrSunDist.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrEarthDist
			// 
			mnuCurrEarthDist.MergeIndex = 7;
			mnuCurrEarthDist.Name = "mnuCurrEarthDist";
			mnuCurrEarthDist.Size = new Size(233, 22);
			mnuCurrEarthDist.Text = "Current distance from Earth";
			mnuCurrEarthDist.Click += menuItemSortCommon_Click;
			// 
			// mnuCurrMag
			// 
			mnuCurrMag.MergeIndex = 8;
			mnuCurrMag.Name = "mnuCurrMag";
			mnuCurrMag.Size = new Size(233, 22);
			mnuCurrMag.Text = "Current magnitude";
			mnuCurrMag.Click += menuItemSortCommon_Click;
			// 
			// mnuPeriod
			// 
			mnuPeriod.MergeIndex = 9;
			mnuPeriod.Name = "mnuPeriod";
			mnuPeriod.Size = new Size(233, 22);
			mnuPeriod.Text = "Period";
			mnuPeriod.Click += menuItemSortCommon_Click;
			// 
			// mnuAphDistance
			// 
			mnuAphDistance.MergeIndex = 10;
			mnuAphDistance.Name = "mnuAphDistance";
			mnuAphDistance.Size = new Size(233, 22);
			mnuAphDistance.Text = "Aphelion distance";
			mnuAphDistance.Click += menuItemSortCommon_Click;
			// 
			// mnuSemiMajorAxis
			// 
			mnuSemiMajorAxis.MergeIndex = 11;
			mnuSemiMajorAxis.Name = "mnuSemiMajorAxis";
			mnuSemiMajorAxis.Size = new Size(233, 22);
			mnuSemiMajorAxis.Text = "Semi-major axis";
			mnuSemiMajorAxis.Click += menuItemSortCommon_Click;
			// 
			// mnuEcc
			// 
			mnuEcc.MergeIndex = 12;
			mnuEcc.Name = "mnuEcc";
			mnuEcc.Size = new Size(233, 22);
			mnuEcc.Text = "Eccentricity";
			mnuEcc.Click += menuItemSortCommon_Click;
			// 
			// mnuIncl
			// 
			mnuIncl.MergeIndex = 13;
			mnuIncl.Name = "mnuIncl";
			mnuIncl.Size = new Size(233, 22);
			mnuIncl.Text = "Inclination";
			mnuIncl.Click += menuItemSortCommon_Click;
			// 
			// mnuAscNode
			// 
			mnuAscNode.MergeIndex = 14;
			mnuAscNode.Name = "mnuAscNode";
			mnuAscNode.Size = new Size(233, 22);
			mnuAscNode.Text = "Long. of the Asc. Node";
			mnuAscNode.Click += menuItemSortCommon_Click;
			// 
			// mnuArgPeri
			// 
			mnuArgPeri.MergeIndex = 15;
			mnuArgPeri.Name = "mnuArgPeri";
			mnuArgPeri.Size = new Size(233, 22);
			mnuArgPeri.Text = "Arg. of Pericenter";
			mnuArgPeri.Click += menuItemSortCommon_Click;
			// 
			// mnuSeparator
			// 
			mnuSeparator.MergeIndex = 16;
			mnuSeparator.Name = "mnuSeparator";
			mnuSeparator.Size = new Size(233, 22);
			// 
			// mnuAsc
			// 
			mnuAsc.MergeIndex = 17;
			mnuAsc.Name = "mnuAsc";
			mnuAsc.Size = new Size(233, 22);
			mnuAsc.Text = "Ascending";
			mnuAsc.Click += menuItemSortAscDesc_Click;
			// 
			// mnuDesc
			// 
			mnuDesc.MergeIndex = 18;
			mnuDesc.Name = "mnuDesc";
			mnuDesc.Size = new Size(233, 22);
			mnuDesc.Text = "Descending";
			mnuDesc.Click += menuItemSortAscDesc_Click;
			// 
			// SortMenuControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnSort);
			Font = new Font("Tahoma", 8.25F);
			Name = "SortMenuControl";
			Size = new Size(100, 23);
			contextSort.ResumeLayout(false);
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
