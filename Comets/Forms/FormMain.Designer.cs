namespace Comets.Forms
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
            this.menuItemEphemerisFile = new System.Windows.Forms.MenuItem();
            this.menuItemMagnitudeGraph = new System.Windows.Forms.MenuItem();
            this.menuItemOrbitViewer = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorFile1 = new System.Windows.Forms.MenuItem();
            this.menuItemOrbitalElements = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorFile3 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemEphemeris = new System.Windows.Forms.MenuItem();
            this.menuItemEphemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemEphemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItemMagnitude = new System.Windows.Forms.MenuItem();
            this.menuItemMagSettings = new System.Windows.Forms.MenuItem();
            this.menuItemMagSaveAs = new System.Windows.Forms.MenuItem();
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
            this.menuItemMagnitude,
            this.menuItemEdit,
            this.menuItemView,
            this.menuItemWindow,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEphemerisFile,
            this.menuItemMagnitudeGraph,
            this.menuItemOrbitViewer,
            this.menuItemSeparatorFile1,
            this.menuItemOrbitalElements,
            this.menuItemSeparatorFile3,
            this.menuItemExit});
            this.menuItemFile.Text = "File";
            // 
            // menuItemEphemerisFile
            // 
            this.menuItemEphemerisFile.Index = 0;
            this.menuItemEphemerisFile.Text = "Ephemeris";
            this.menuItemEphemerisFile.Click += new System.EventHandler(this.menuItemEphemerides_Click);
            // 
            // menuItemMagnitudeGraph
            // 
            this.menuItemMagnitudeGraph.Index = 1;
            this.menuItemMagnitudeGraph.Text = "Magnitude Graph";
            this.menuItemMagnitudeGraph.Click += new System.EventHandler(this.menuItemMagnitudeGraph_Click);
            // 
            // menuItemOrbitViewer
            // 
            this.menuItemOrbitViewer.Index = 2;
            this.menuItemOrbitViewer.Text = "Orbit viewer";
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
            this.menuItemEphemSettings,
            this.menuItemEphemSaveAs});
            this.menuItemEphemeris.Text = "Ephemeris";
            this.menuItemEphemeris.Visible = false;
            // 
            // menuItemEphemSettings
            // 
            this.menuItemEphemSettings.Index = 0;
            this.menuItemEphemSettings.Text = "Settings";
            this.menuItemEphemSettings.Click += new System.EventHandler(this.menuItemEphemSettings_Click);
            // 
            // menuItemEphemSaveAs
            // 
            this.menuItemEphemSaveAs.Index = 1;
            this.menuItemEphemSaveAs.Text = "Save As";
            // 
            // menuItemMagnitude
            // 
            this.menuItemMagnitude.Index = 2;
            this.menuItemMagnitude.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemMagSettings,
            this.menuItemMagSaveAs});
            this.menuItemMagnitude.Text = "Magnitude";
            this.menuItemMagnitude.Visible = false;
            // 
            // menuItemMagSettings
            // 
            this.menuItemMagSettings.Index = 0;
            this.menuItemMagSettings.Text = "Settings";
            this.menuItemMagSettings.Click += new System.EventHandler(this.menuItemMagSettings_Click);
            // 
            // menuItemMagSaveAs
            // 
            this.menuItemMagSaveAs.Index = 1;
            this.menuItemMagSaveAs.Text = "Save As";
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 3;
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
            this.menuItemView.Index = 4;
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
            this.menuItemWindow.Index = 5;
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
            this.menuItemHelp.Index = 6;
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
        private System.Windows.Forms.MenuItem menuItemEphemerisFile;
        private System.Windows.Forms.MenuItem menuItemMagnitudeGraph;
        private System.Windows.Forms.MenuItem menuItemOrbitViewer;
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
        private System.Windows.Forms.MenuItem menuItemEphemSettings;
        private System.Windows.Forms.MenuItem menuItemWindowSeparator;
        private System.Windows.Forms.MenuItem menuItemEphemSaveAs;
        private System.Windows.Forms.MenuItem menuItemMinimizeAll;
        private System.Windows.Forms.MenuItem menuItemClose;
        private System.Windows.Forms.MenuItem menuItemRestoreAll;
        private System.Windows.Forms.MenuItem menuItemMagnitude;
        private System.Windows.Forms.MenuItem menuItemMagSettings;
        private System.Windows.Forms.MenuItem menuItemMagSaveAs;
    }
}

