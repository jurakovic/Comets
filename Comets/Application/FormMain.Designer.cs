namespace Comets.Application
{
	partial class FormMain
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemFileEphemeris = new System.Windows.Forms.MenuItem();
			this.menuItemFileGraph = new System.Windows.Forms.MenuItem();
			this.menuItemFileOrbit = new System.Windows.Forms.MenuItem();
			this.menuItemSeparatorFile1 = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitalElements = new System.Windows.Forms.MenuItem();
			this.menuItemSeparatorFile3 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItemEphemeris = new System.Windows.Forms.MenuItem();
			this.menuItemEphemerisSettings = new System.Windows.Forms.MenuItem();
			this.menuItemEphemerisSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItemGraph = new System.Windows.Forms.MenuItem();
			this.menuItemGraphSettings = new System.Windows.Forms.MenuItem();
			this.menuItemGraphSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItemOrbit = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitMultiple = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitClearComets = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitSep1 = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitEclipticAxis = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitSep2 = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitAntialiasing = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitSep3 = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitLabels = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitComet = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitPlanet = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitMagnitude = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitDistance = new System.Windows.Forms.MenuItem();
			this.menuItemOrbitDate = new System.Windows.Forms.MenuItem();
			this.menuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuItemDatabase = new System.Windows.Forms.MenuItem();
			this.menuItemSeparatorEdit1 = new System.Windows.Forms.MenuItem();
			this.menuItemImport = new System.Windows.Forms.MenuItem();
			this.menuItemExport = new System.Windows.Forms.MenuItem();
			this.menuItemSeparatorEdit2 = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuItemView = new System.Windows.Forms.MenuItem();
			this.menuItemStatusBar = new System.Windows.Forms.MenuItem();
			this.menuItemWindow = new System.Windows.Forms.MenuItem();
			this.menuItemTileVert = new System.Windows.Forms.MenuItem();
			this.menuItemTileHoriz = new System.Windows.Forms.MenuItem();
			this.menuItemCascade = new System.Windows.Forms.MenuItem();
			this.menuItemMinimizeAll = new System.Windows.Forms.MenuItem();
			this.menuItemRestoreAll = new System.Windows.Forms.MenuItem();
			this.menuItemClose = new System.Windows.Forms.MenuItem();
			this.menuItemWindowSeparator = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusComets = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusSpace = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemEphemeris,
            this.menuItemGraph,
            this.menuItemOrbit,
            this.menuItemEdit,
            this.menuItemView,
            this.menuItemWindow,
            this.menuItemHelp});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileEphemeris,
            this.menuItemFileGraph,
            this.menuItemFileOrbit,
            this.menuItemSeparatorFile1,
            this.menuItemOrbitalElements,
            this.menuItemSeparatorFile3,
            this.menuItemExit});
			this.menuItemFile.Text = "File";
			// 
			// menuItemFileEphemeris
			// 
			this.menuItemFileEphemeris.Index = 0;
			this.menuItemFileEphemeris.Text = "Ephemeris";
			this.menuItemFileEphemeris.Click += new System.EventHandler(this.menuItemFileEphemerides_Click);
			// 
			// menuItemFileGraph
			// 
			this.menuItemFileGraph.Index = 1;
			this.menuItemFileGraph.Text = "Magnitude Graph";
			this.menuItemFileGraph.Click += new System.EventHandler(this.menuItemFileGraph_Click);
			// 
			// menuItemFileOrbit
			// 
			this.menuItemFileOrbit.Index = 2;
			this.menuItemFileOrbit.Text = "Orbit Viewer";
			this.menuItemFileOrbit.Click += new System.EventHandler(this.menuItemFileOrbit_Click);
			// 
			// menuItemSeparatorFile1
			// 
			this.menuItemSeparatorFile1.Index = 3;
			this.menuItemSeparatorFile1.Text = "-";
			// 
			// menuItemOrbitalElements
			// 
			this.menuItemOrbitalElements.Index = 4;
			this.menuItemOrbitalElements.Text = "Orbital elements";
			// 
			// menuItemSeparatorFile3
			// 
			this.menuItemSeparatorFile3.Index = 5;
			this.menuItemSeparatorFile3.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 6;
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemEphemeris
			// 
			this.menuItemEphemeris.Index = 1;
			this.menuItemEphemeris.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEphemerisSettings,
            this.menuItemEphemerisSaveAs});
			this.menuItemEphemeris.Text = "Ephemeris";
			this.menuItemEphemeris.Visible = false;
			// 
			// menuItemEphemerisSettings
			// 
			this.menuItemEphemerisSettings.Index = 0;
			this.menuItemEphemerisSettings.Text = "Settings";
			this.menuItemEphemerisSettings.Click += new System.EventHandler(this.menuItemEphemSettings_Click);
			// 
			// menuItemEphemerisSaveAs
			// 
			this.menuItemEphemerisSaveAs.Index = 1;
			this.menuItemEphemerisSaveAs.Text = "Save As";
			// 
			// menuItemGraph
			// 
			this.menuItemGraph.Index = 2;
			this.menuItemGraph.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGraphSettings,
            this.menuItemGraphSaveAs});
			this.menuItemGraph.Text = "Graph";
			this.menuItemGraph.Visible = false;
			// 
			// menuItemGraphSettings
			// 
			this.menuItemGraphSettings.Index = 0;
			this.menuItemGraphSettings.Text = "Settings";
			this.menuItemGraphSettings.Click += new System.EventHandler(this.menuItemGraphSettings_Click);
			// 
			// menuItemGraphSaveAs
			// 
			this.menuItemGraphSaveAs.Index = 1;
			this.menuItemGraphSaveAs.Text = "Save As";
			// 
			// menuItemOrbit
			// 
			this.menuItemOrbit.Index = 3;
			this.menuItemOrbit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOrbitMultiple,
            this.menuItemOrbitClearComets,
            this.menuItemOrbitSep1,
            this.menuItemOrbitEclipticAxis,
            this.menuItemOrbitSep2,
            this.menuItemOrbitAntialiasing,
            this.menuItemOrbitSep3,
            this.menuItemOrbitLabels,
            this.menuItemOrbitComet,
            this.menuItemOrbitPlanet,
            this.menuItemOrbitMagnitude,
            this.menuItemOrbitDistance,
            this.menuItemOrbitDate});
			this.menuItemOrbit.Text = "Orbit";
			this.menuItemOrbit.Visible = false;
			// 
			// menuItemOrbitMultiple
			// 
			this.menuItemOrbitMultiple.Index = 0;
			this.menuItemOrbitMultiple.Text = "Multiple mode";
			this.menuItemOrbitMultiple.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitClearComets
			// 
			this.menuItemOrbitClearComets.Index = 1;
			this.menuItemOrbitClearComets.Text = "Clear comets";
			this.menuItemOrbitClearComets.Click += new System.EventHandler(this.menuItemOrbitClearComets_Click);
			// 
			// menuItemOrbitSep1
			// 
			this.menuItemOrbitSep1.Index = 2;
			this.menuItemOrbitSep1.Text = "-";
			// 
			// menuItemOrbitEclipticAxis
			// 
			this.menuItemOrbitEclipticAxis.Index = 3;
			this.menuItemOrbitEclipticAxis.Text = "Ecliptic Axis";
			this.menuItemOrbitEclipticAxis.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitSep2
			// 
			this.menuItemOrbitSep2.Index = 4;
			this.menuItemOrbitSep2.Text = "-";
			// 
			// menuItemOrbitAntialiasing
			// 
			this.menuItemOrbitAntialiasing.Index = 5;
			this.menuItemOrbitAntialiasing.Text = "Antialiasing";
			this.menuItemOrbitAntialiasing.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitSep3
			// 
			this.menuItemOrbitSep3.Index = 6;
			this.menuItemOrbitSep3.Text = "-";
			// 
			// menuItemOrbitLabels
			// 
			this.menuItemOrbitLabels.Enabled = false;
			this.menuItemOrbitLabels.Index = 7;
			this.menuItemOrbitLabels.Text = "Labels";
			// 
			// menuItemOrbitComet
			// 
			this.menuItemOrbitComet.Index = 8;
			this.menuItemOrbitComet.Text = "   Comet";
			this.menuItemOrbitComet.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitPlanet
			// 
			this.menuItemOrbitPlanet.Index = 9;
			this.menuItemOrbitPlanet.Text = "   Planet";
			this.menuItemOrbitPlanet.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitMagnitude
			// 
			this.menuItemOrbitMagnitude.Index = 10;
			this.menuItemOrbitMagnitude.Text = "   Magnitude";
			this.menuItemOrbitMagnitude.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitDistance
			// 
			this.menuItemOrbitDistance.Index = 11;
			this.menuItemOrbitDistance.Text = "   Distance";
			this.menuItemOrbitDistance.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemOrbitDate
			// 
			this.menuItemOrbitDate.Index = 12;
			this.menuItemOrbitDate.Text = "   Date";
			this.menuItemOrbitDate.Click += new System.EventHandler(this.menuItemOrbitMultiple_Click);
			// 
			// menuItemEdit
			// 
			this.menuItemEdit.Index = 4;
			this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDatabase,
            this.menuItemSeparatorEdit1,
            this.menuItemImport,
            this.menuItemExport,
            this.menuItemSeparatorEdit2,
            this.menuItemSettings});
			this.menuItemEdit.Text = "Edit";
			// 
			// menuItemDatabase
			// 
			this.menuItemDatabase.Index = 0;
			this.menuItemDatabase.Text = "Database";
			this.menuItemDatabase.Click += new System.EventHandler(this.menuItemDatabase_Click);
			// 
			// menuItemSeparatorEdit1
			// 
			this.menuItemSeparatorEdit1.Index = 1;
			this.menuItemSeparatorEdit1.Text = "-";
			// 
			// menuItemImport
			// 
			this.menuItemImport.Index = 2;
			this.menuItemImport.Text = "Import";
			this.menuItemImport.Click += new System.EventHandler(this.menuItemImport_Click);
			// 
			// menuItemExport
			// 
			this.menuItemExport.Index = 3;
			this.menuItemExport.Text = "Export";
			this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
			// 
			// menuItemSeparatorEdit2
			// 
			this.menuItemSeparatorEdit2.Index = 4;
			this.menuItemSeparatorEdit2.Text = "-";
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 5;
			this.menuItemSettings.Text = "Settings";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// menuItemView
			// 
			this.menuItemView.Index = 5;
			this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStatusBar});
			this.menuItemView.Text = "View";
			// 
			// menuItemStatusBar
			// 
			this.menuItemStatusBar.Checked = true;
			this.menuItemStatusBar.Index = 0;
			this.menuItemStatusBar.Text = "Statur bar";
			this.menuItemStatusBar.Click += new System.EventHandler(this.menuItemStatusBar_Click);
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.Index = 6;
			this.menuItemWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemTileVert,
            this.menuItemTileHoriz,
            this.menuItemCascade,
            this.menuItemMinimizeAll,
            this.menuItemRestoreAll,
            this.menuItemClose,
            this.menuItemWindowSeparator});
			this.menuItemWindow.Text = "Window";
			this.menuItemWindow.Visible = false;
			// 
			// menuItemTileVert
			// 
			this.menuItemTileVert.Index = 0;
			this.menuItemTileVert.Text = "Tile Horizontally";
			this.menuItemTileVert.Click += new System.EventHandler(this.menuItemTileVert_Click);
			// 
			// menuItemTileHoriz
			// 
			this.menuItemTileHoriz.Index = 1;
			this.menuItemTileHoriz.Text = "Tile Vertically";
			this.menuItemTileHoriz.Click += new System.EventHandler(this.menuItemTileHoriz_Click);
			// 
			// menuItemCascade
			// 
			this.menuItemCascade.Index = 2;
			this.menuItemCascade.Text = "Cascade";
			this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
			// 
			// menuItemMinimizeAll
			// 
			this.menuItemMinimizeAll.Index = 3;
			this.menuItemMinimizeAll.Text = "Minimize All";
			this.menuItemMinimizeAll.Click += new System.EventHandler(this.menuItemMinimizeAll_Click);
			// 
			// menuItemRestoreAll
			// 
			this.menuItemRestoreAll.Index = 4;
			this.menuItemRestoreAll.Text = "Restore All";
			this.menuItemRestoreAll.Click += new System.EventHandler(this.menuItemRestoreAll_Click);
			// 
			// menuItemClose
			// 
			this.menuItemClose.Index = 5;
			this.menuItemClose.Text = "Close";
			this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
			// 
			// menuItemWindowSeparator
			// 
			this.menuItemWindowSeparator.Index = 6;
			this.menuItemWindowSeparator.Text = "-";
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 7;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
			this.menuItemHelp.Text = "Help";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 0;
			this.menuItemAbout.Text = "About";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusComets,
            this.statusSpace,
            this.statusProgressBar});
			this.statusStrip.Location = new System.Drawing.Point(0, 540);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(784, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusComets
			// 
			this.statusComets.AutoSize = false;
			this.statusComets.Name = "statusComets";
			this.statusComets.Size = new System.Drawing.Size(200, 17);
			this.statusComets.Text = "Comets: 0";
			this.statusComets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// statusSpace
			// 
			this.statusSpace.Name = "statusSpace";
			this.statusSpace.Size = new System.Drawing.Size(569, 17);
			this.statusSpace.Spring = true;
			// 
			// statusProgressBar
			// 
			this.statusProgressBar.AutoSize = false;
			this.statusProgressBar.Name = "statusProgressBar";
			this.statusProgressBar.Size = new System.Drawing.Size(200, 16);
			this.statusProgressBar.Visible = false;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.statusStrip);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Comets";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.MdiChildActivate += new System.EventHandler(this.FormMain_MdiChildActivate);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemEdit;
		private System.Windows.Forms.MenuItem menuItemView;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemDatabase;
		private System.Windows.Forms.MenuItem menuItemFileEphemeris;
		private System.Windows.Forms.MenuItem menuItemFileGraph;
		private System.Windows.Forms.MenuItem menuItemFileOrbit;
		private System.Windows.Forms.MenuItem menuItemSeparatorFile1;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemSeparatorEdit1;
		private System.Windows.Forms.MenuItem menuItemImport;
		private System.Windows.Forms.MenuItem menuItemExport;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItemSeparatorFile3;
		private System.Windows.Forms.MenuItem menuItemOrbitalElements;
		private System.Windows.Forms.MenuItem menuItemSeparatorEdit2;
		private System.Windows.Forms.MenuItem menuItemStatusBar;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusSpace;
		private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel statusComets;
		private System.Windows.Forms.MenuItem menuItemWindow;
		private System.Windows.Forms.MenuItem menuItemCascade;
		private System.Windows.Forms.MenuItem menuItemTileHoriz;
		private System.Windows.Forms.MenuItem menuItemTileVert;
		private System.Windows.Forms.MenuItem menuItemEphemeris;
		private System.Windows.Forms.MenuItem menuItemEphemerisSettings;
		private System.Windows.Forms.MenuItem menuItemWindowSeparator;
		private System.Windows.Forms.MenuItem menuItemEphemerisSaveAs;
		private System.Windows.Forms.MenuItem menuItemMinimizeAll;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItemRestoreAll;
		private System.Windows.Forms.MenuItem menuItemGraph;
		private System.Windows.Forms.MenuItem menuItemGraphSettings;
		private System.Windows.Forms.MenuItem menuItemGraphSaveAs;
		private System.Windows.Forms.MenuItem menuItemOrbit;
		private System.Windows.Forms.MenuItem menuItemOrbitMultiple;
		private System.Windows.Forms.MenuItem menuItemOrbitAntialiasing;
		private System.Windows.Forms.MenuItem menuItemOrbitSep1;
		private System.Windows.Forms.MenuItem menuItemOrbitSep3;
		private System.Windows.Forms.MenuItem menuItemOrbitLabels;
		private System.Windows.Forms.MenuItem menuItemOrbitComet;
		private System.Windows.Forms.MenuItem menuItemOrbitPlanet;
		private System.Windows.Forms.MenuItem menuItemOrbitMagnitude;
		private System.Windows.Forms.MenuItem menuItemOrbitDistance;
		private System.Windows.Forms.MenuItem menuItemOrbitDate;
		private System.Windows.Forms.MenuItem menuItemOrbitEclipticAxis;
		private System.Windows.Forms.MenuItem menuItemOrbitSep2;
		private System.Windows.Forms.MenuItem menuItemOrbitClearComets;
	}
}

