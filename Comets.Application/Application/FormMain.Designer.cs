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
			this.mainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileEphemeris = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileOrbit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorFile1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemOrbitalElements = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorFile3 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemeris = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemerisSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemerisSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraphSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraphSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemOrbit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuShowToolbox = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorEdit1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTileVert = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTileHoriz = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCascade = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMinimizeAll = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRestoreAll = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemControls = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusComets = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusSpace = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
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
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
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
			this.menuItemFileEphemeris.Text = "&Ephemeris";
			this.menuItemFileEphemeris.Click += new System.EventHandler(this.menuItemFileEphemerides_Click);
			// 
			// menuItemFileGraph
			// 
			this.menuItemFileGraph.Text = "&Graph";
			this.menuItemFileGraph.Click += new System.EventHandler(this.menuItemFileGraph_Click);
			// 
			// menuItemFileOrbit
			// 
			this.menuItemFileOrbit.Text = "Orbit &Viewer";
			this.menuItemFileOrbit.Click += new System.EventHandler(this.menuItemFileOrbit_Click);
			// 
			// menuItemSeparatorFile1
			// 
			this.menuItemSeparatorFile1.Text = "-";
			// 
			// menuItemOrbitalElements
			// 
			this.menuItemOrbitalElements.Text = "&Orbital elements";
			this.menuItemOrbitalElements.Click += new System.EventHandler(this.menuItemOrbitalElements_Click);
			// 
			// menuItemSeparatorFile3
			// 
			this.menuItemSeparatorFile3.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemEphemeris
			// 
			this.menuItemEphemeris.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemEphemerisSettings,
            this.menuItemEphemerisSaveAs});
			this.menuItemEphemeris.Text = "E&phemeris";
			this.menuItemEphemeris.Visible = false;
			// 
			// menuItemEphemerisSettings
			// 
			this.menuItemEphemerisSettings.Text = "&Settings";
			this.menuItemEphemerisSettings.Click += new System.EventHandler(this.menuItemEphemSettings_Click);
			// 
			// menuItemEphemerisSaveAs
			// 
			this.menuItemEphemerisSaveAs.Text = "Save &As";
			this.menuItemEphemerisSaveAs.Click += new System.EventHandler(this.menuItemEphemerisSaveAs_Click);
			// 
			// menuItemGraph
			// 
			this.menuItemGraph.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemGraphSettings,
            this.menuItemGraphSaveAs});
			this.menuItemGraph.Text = "&Graph";
			this.menuItemGraph.Visible = false;
			// 
			// menuItemGraphSettings
			// 

			this.menuItemGraphSettings.Text = "&Settings";
			this.menuItemGraphSettings.Click += new System.EventHandler(this.menuItemGraphSettings_Click);
			// 
			// menuItemGraphSaveAs
			// 

			this.menuItemGraphSaveAs.Text = "Save &As";
			this.menuItemGraphSaveAs.Click += new System.EventHandler(this.menuItemGraphSaveAs_Click);
			// 
			// menuItemOrbit
			// 

			this.menuItemOrbit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuShowToolbox});
			this.menuItemOrbit.Text = "&Orbit";
			this.menuItemOrbit.Visible = false;
			// 
			// mnuShowToolbox
			// 
			this.mnuShowToolbox.Checked = true;

			this.mnuShowToolbox.Text = "Show &Toolbox";
			this.mnuShowToolbox.Click += new System.EventHandler(this.mnuShowToolbox_Click);
			// 
			// menuItemEdit
			// 

			this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemDatabase,
            this.menuItemUpdate,
            this.menuItemSeparatorEdit1,
            this.menuItemSettings});
			this.menuItemEdit.Text = "&Edit";
			// 
			// menuItemDatabase
			// 

			this.menuItemDatabase.Text = "&Database";
			this.menuItemDatabase.Click += new System.EventHandler(this.menuItemDatabase_Click);
			// 
			// menuItemUpdate
			// 

			this.menuItemUpdate.Text = "&Update";
			this.menuItemUpdate.Click += new System.EventHandler(this.menuItemImport_Click);
			// 
			// menuItemSeparatorEdit1
			// 
			this.menuItemSeparatorEdit1.Text = "-";
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Text = "&Settings";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// menuItemView
			// 
			this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemViewAlwaysOnTop,
            this.menuItemViewStatusBar});
			this.menuItemView.Text = "&View";
			// 
			// menuItemViewAlwaysOnTop
			// 
			this.menuItemViewAlwaysOnTop.Text = "Always on &Top";
			this.menuItemViewAlwaysOnTop.Click += new System.EventHandler(this.menuItemViewAlwaysOnTop_Click);
			// 
			// menuItemViewStatusBar
			// 
			this.menuItemViewStatusBar.Checked = true;
			this.menuItemViewStatusBar.Text = "Show &status bar";
			this.menuItemViewStatusBar.Click += new System.EventHandler(this.menuItemViewStatusBar_Click);
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
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
			this.menuItemTileVert.Text = "Tile &Horizontally";
			this.menuItemTileVert.Click += new System.EventHandler(this.menuItemTileVert_Click);
			// 
			// menuItemTileHoriz
			// 
			this.menuItemTileHoriz.Text = "Tile &Vertically";
			this.menuItemTileHoriz.Click += new System.EventHandler(this.menuItemTileHoriz_Click);
			// 
			// menuItemCascade
			// 
			this.menuItemCascade.Text = "&Cascade";
			this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
			// 
			// menuItemMinimizeAll
			// 
			this.menuItemMinimizeAll.Text = "&Minimize All";
			this.menuItemMinimizeAll.Click += new System.EventHandler(this.menuItemMinimizeAll_Click);
			// 
			// menuItemRestoreAll
			// 
			this.menuItemRestoreAll.Text = "&Restore All";
			this.menuItemRestoreAll.Click += new System.EventHandler(this.menuItemRestoreAll_Click);
			// 
			// menuItemClose
			// 
			this.menuItemClose.ShortcutKeys = Keys.Control | Keys.F4;
			this.menuItemClose.Text = "&Close";
			this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemControls,
            this.menuItemAbout});
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemControls
			// 
			this.menuItemControls.Text = "&Controls";
			this.menuItemControls.Click += new System.EventHandler(this.menuItemControls_Click);
			// 
			// menuItemAbout
			// 
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
			this.ContextMenuStrip = this.mainMenu;
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

		private System.Windows.Forms.ContextMenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemDatabase;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileEphemeris;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileGraph;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileOrbit;
		private System.Windows.Forms.ToolStripMenuItem menuItemSeparatorFile1;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.ToolStripMenuItem menuItemSeparatorEdit1;
		private System.Windows.Forms.ToolStripMenuItem menuItemUpdate;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
		private System.Windows.Forms.ToolStripMenuItem menuItemSeparatorFile3;
		private System.Windows.Forms.ToolStripMenuItem menuItemOrbitalElements;
		private System.Windows.Forms.ToolStripMenuItem menuItemViewStatusBar;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusSpace;
		private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel statusComets;
		private System.Windows.Forms.ToolStripMenuItem menuItemWindow;
		private System.Windows.Forms.ToolStripMenuItem menuItemCascade;
		private System.Windows.Forms.ToolStripMenuItem menuItemTileHoriz;
		private System.Windows.Forms.ToolStripMenuItem menuItemTileVert;
		private System.Windows.Forms.ToolStripMenuItem menuItemEphemeris;
		private System.Windows.Forms.ToolStripMenuItem menuItemEphemerisSettings;
		private System.Windows.Forms.ToolStripMenuItem menuItemEphemerisSaveAs;
		private System.Windows.Forms.ToolStripMenuItem menuItemMinimizeAll;
		private System.Windows.Forms.ToolStripMenuItem menuItemClose;
		private System.Windows.Forms.ToolStripMenuItem menuItemRestoreAll;
		private System.Windows.Forms.ToolStripMenuItem menuItemGraph;
		private System.Windows.Forms.ToolStripMenuItem menuItemGraphSettings;
		private System.Windows.Forms.ToolStripMenuItem menuItemGraphSaveAs;
		private System.Windows.Forms.ToolStripMenuItem menuItemOrbit;
		private System.Windows.Forms.ToolStripMenuItem menuItemViewAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem mnuShowToolbox;
		private System.Windows.Forms.ToolStripMenuItem menuItemControls;
	}
}

