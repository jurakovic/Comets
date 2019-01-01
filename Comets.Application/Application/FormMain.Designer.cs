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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
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
			this.mnuShowToolbox = new System.Windows.Forms.MenuItem();
			this.menuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuItemDatabase = new System.Windows.Forms.MenuItem();
			this.menuItemUpdate = new System.Windows.Forms.MenuItem();
			this.menuItemSeparatorEdit1 = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuItemView = new System.Windows.Forms.MenuItem();
			this.menuItemViewAlwaysOnTop = new System.Windows.Forms.MenuItem();
			this.menuItemViewStatusBar = new System.Windows.Forms.MenuItem();
			this.menuItemWindow = new System.Windows.Forms.MenuItem();
			this.menuItemTileVert = new System.Windows.Forms.MenuItem();
			this.menuItemTileHoriz = new System.Windows.Forms.MenuItem();
			this.menuItemCascade = new System.Windows.Forms.MenuItem();
			this.menuItemMinimizeAll = new System.Windows.Forms.MenuItem();
			this.menuItemRestoreAll = new System.Windows.Forms.MenuItem();
			this.menuItemClose = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemControls = new System.Windows.Forms.MenuItem();
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
			this.menuItemFile.Text = "&File";
			// 
			// menuItemFileEphemeris
			// 
			this.menuItemFileEphemeris.Index = 0;
			this.menuItemFileEphemeris.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.menuItemFileEphemeris.Text = "&Ephemeris";
			this.menuItemFileEphemeris.Click += new System.EventHandler(this.menuItemFileEphemerides_Click);
			// 
			// menuItemFileGraph
			// 
			this.menuItemFileGraph.Index = 1;
			this.menuItemFileGraph.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
			this.menuItemFileGraph.Text = "&Graph";
			this.menuItemFileGraph.Click += new System.EventHandler(this.menuItemFileGraph_Click);
			// 
			// menuItemFileOrbit
			// 
			this.menuItemFileOrbit.Index = 2;
			this.menuItemFileOrbit.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuItemFileOrbit.Text = "Orbit &Viewer";
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
			this.menuItemOrbitalElements.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuItemOrbitalElements.Text = "&Orbital elements";
			this.menuItemOrbitalElements.Click += new System.EventHandler(this.menuItemOrbitalElements_Click);
			// 
			// menuItemSeparatorFile3
			// 
			this.menuItemSeparatorFile3.Index = 5;
			this.menuItemSeparatorFile3.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 6;
			this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemEphemeris
			// 
			this.menuItemEphemeris.Index = 1;
			this.menuItemEphemeris.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEphemerisSettings,
            this.menuItemEphemerisSaveAs});
			this.menuItemEphemeris.Text = "E&phemeris";
			this.menuItemEphemeris.Visible = false;
			// 
			// menuItemEphemerisSettings
			// 
			this.menuItemEphemerisSettings.Index = 0;
			this.menuItemEphemerisSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftE;
			this.menuItemEphemerisSettings.Text = "&Settings";
			this.menuItemEphemerisSettings.Click += new System.EventHandler(this.menuItemEphemSettings_Click);
			// 
			// menuItemEphemerisSaveAs
			// 
			this.menuItemEphemerisSaveAs.Index = 1;
			this.menuItemEphemerisSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemEphemerisSaveAs.Text = "Save &As";
			this.menuItemEphemerisSaveAs.Click += new System.EventHandler(this.menuItemEphemerisSaveAs_Click);
			// 
			// menuItemGraph
			// 
			this.menuItemGraph.Index = 2;
			this.menuItemGraph.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGraphSettings,
            this.menuItemGraphSaveAs});
			this.menuItemGraph.Text = "&Graph";
			this.menuItemGraph.Visible = false;
			// 
			// menuItemGraphSettings
			// 
			this.menuItemGraphSettings.Index = 0;
			this.menuItemGraphSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftG;
			this.menuItemGraphSettings.Text = "&Settings";
			this.menuItemGraphSettings.Click += new System.EventHandler(this.menuItemGraphSettings_Click);
			// 
			// menuItemGraphSaveAs
			// 
			this.menuItemGraphSaveAs.Index = 1;
			this.menuItemGraphSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemGraphSaveAs.Text = "Save &As";
			this.menuItemGraphSaveAs.Click += new System.EventHandler(this.menuItemGraphSaveAs_Click);
			// 
			// menuItemOrbit
			// 
			this.menuItemOrbit.Index = 3;
			this.menuItemOrbit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuShowToolbox});
			this.menuItemOrbit.Text = "&Orbit";
			this.menuItemOrbit.Visible = false;
			// 
			// mnuShowToolbox
			// 
			this.mnuShowToolbox.Checked = true;
			this.mnuShowToolbox.Index = 0;
			this.mnuShowToolbox.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
			this.mnuShowToolbox.Text = "Show &Toolbox";
			this.mnuShowToolbox.Click += new System.EventHandler(this.mnuShowToolbox_Click);
			// 
			// menuItemEdit
			// 
			this.menuItemEdit.Index = 4;
			this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDatabase,
            this.menuItemUpdate,
            this.menuItemSeparatorEdit1,
            this.menuItemSettings});
			this.menuItemEdit.Text = "&Edit";
			// 
			// menuItemDatabase
			// 
			this.menuItemDatabase.Index = 0;
			this.menuItemDatabase.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuItemDatabase.Text = "&Database";
			this.menuItemDatabase.Click += new System.EventHandler(this.menuItemDatabase_Click);
			// 
			// menuItemUpdate
			// 
			this.menuItemUpdate.Index = 1;
			this.menuItemUpdate.Shortcut = System.Windows.Forms.Shortcut.F6;
			this.menuItemUpdate.Text = "&Update";
			this.menuItemUpdate.Click += new System.EventHandler(this.menuItemImport_Click);
			// 
			// menuItemSeparatorEdit1
			// 
			this.menuItemSeparatorEdit1.Index = 2;
			this.menuItemSeparatorEdit1.Text = "-";
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 3;
			this.menuItemSettings.Text = "&Settings";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// menuItemView
			// 
			this.menuItemView.Index = 5;
			this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemViewAlwaysOnTop,
            this.menuItemViewStatusBar});
			this.menuItemView.Text = "&View";
			// 
			// menuItemViewAlwaysOnTop
			// 
			this.menuItemViewAlwaysOnTop.Index = 0;
			this.menuItemViewAlwaysOnTop.Text = "Always on &Top";
			this.menuItemViewAlwaysOnTop.Click += new System.EventHandler(this.menuItemViewAlwaysOnTop_Click);
			// 
			// menuItemViewStatusBar
			// 
			this.menuItemViewStatusBar.Checked = true;
			this.menuItemViewStatusBar.Index = 1;
			this.menuItemViewStatusBar.Text = "Show &status bar";
			this.menuItemViewStatusBar.Click += new System.EventHandler(this.menuItemViewStatusBar_Click);
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.Index = 6;
			this.menuItemWindow.MdiList = true;
			this.menuItemWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemTileVert,
            this.menuItemTileHoriz,
            this.menuItemCascade,
            this.menuItemMinimizeAll,
            this.menuItemRestoreAll,
            this.menuItemClose});
			this.menuItemWindow.Text = "&Window";
			this.menuItemWindow.Visible = false;
			// 
			// menuItemTileVert
			// 
			this.menuItemTileVert.Index = 0;
			this.menuItemTileVert.Text = "Tile &Horizontally";
			this.menuItemTileVert.Click += new System.EventHandler(this.menuItemTileVert_Click);
			// 
			// menuItemTileHoriz
			// 
			this.menuItemTileHoriz.Index = 1;
			this.menuItemTileHoriz.Text = "Tile &Vertically";
			this.menuItemTileHoriz.Click += new System.EventHandler(this.menuItemTileHoriz_Click);
			// 
			// menuItemCascade
			// 
			this.menuItemCascade.Index = 2;
			this.menuItemCascade.Text = "&Cascade";
			this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
			// 
			// menuItemMinimizeAll
			// 
			this.menuItemMinimizeAll.Index = 3;
			this.menuItemMinimizeAll.Text = "&Minimize All";
			this.menuItemMinimizeAll.Click += new System.EventHandler(this.menuItemMinimizeAll_Click);
			// 
			// menuItemRestoreAll
			// 
			this.menuItemRestoreAll.Index = 4;
			this.menuItemRestoreAll.Text = "&Restore All";
			this.menuItemRestoreAll.Click += new System.EventHandler(this.menuItemRestoreAll_Click);
			// 
			// menuItemClose
			// 
			this.menuItemClose.Index = 5;
			this.menuItemClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
			this.menuItemClose.Text = "&Close";
			this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 7;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemControls,
            this.menuItemAbout});
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemControls
			// 
			this.menuItemControls.Index = 0;
			this.menuItemControls.Text = "&Controls";
			this.menuItemControls.Click += new System.EventHandler(this.menuItemControls_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 1;
			this.menuItemAbout.Text = "&About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
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
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.Move += new System.EventHandler(this.FormMain_Move);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
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
		private System.Windows.Forms.MenuItem menuItemUpdate;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItemSeparatorFile3;
		private System.Windows.Forms.MenuItem menuItemOrbitalElements;
		private System.Windows.Forms.MenuItem menuItemViewStatusBar;
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
		private System.Windows.Forms.MenuItem menuItemEphemerisSaveAs;
		private System.Windows.Forms.MenuItem menuItemMinimizeAll;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItemRestoreAll;
		private System.Windows.Forms.MenuItem menuItemGraph;
		private System.Windows.Forms.MenuItem menuItemGraphSettings;
		private System.Windows.Forms.MenuItem menuItemGraphSaveAs;
		private System.Windows.Forms.MenuItem menuItemOrbit;
		private System.Windows.Forms.MenuItem menuItemViewAlwaysOnTop;
		private System.Windows.Forms.MenuItem mnuShowToolbox;
		private System.Windows.Forms.MenuItem menuItemControls;
	}
}

