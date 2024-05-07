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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileEphemeris = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileOrbit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorFile1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemOrbitalElements = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorFile3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparatorEdit1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemeris = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemerisSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEphemerisSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraph = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraphSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGraphSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemOrbit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuShowToolbox = new System.Windows.Forms.ToolStripMenuItem();
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
			this.menuItemFileOrbit3d = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemFile, this.menuItemEdit, this.menuItemEphemeris, this.menuItemGraph, this.menuItemOrbit, this.menuItemView, this.menuItemWindow, this.menuItemHelp });
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.MdiWindowListItem = this.menuItemWindow;
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.mainMenu.Size = new System.Drawing.Size(784, 24);
			this.mainMenu.TabIndex = 0;
			// 
			// menuItemFile
			// 
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemFileEphemeris, this.menuItemFileGraph, this.menuItemFileOrbit, this.menuItemFileOrbit3d, this.menuItemSeparatorFile1, this.menuItemOrbitalElements, this.menuItemSeparatorFile3, this.menuItemExit });
			this.menuItemFile.MergeIndex = 0;
			this.menuItemFile.Name = "menuItemFile";
			this.menuItemFile.Size = new System.Drawing.Size(37, 20);
			this.menuItemFile.Text = "&File";
			// 
			// menuItemFileEphemeris
			// 
			this.menuItemFileEphemeris.MergeIndex = 0;
			this.menuItemFileEphemeris.Name = "menuItemFileEphemeris";
			this.menuItemFileEphemeris.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			this.menuItemFileEphemeris.Size = new System.Drawing.Size(204, 22);
			this.menuItemFileEphemeris.Text = "&Ephemeris";
			this.menuItemFileEphemeris.Click += this.menuItemFileEphemerides_Click;
			// 
			// menuItemFileGraph
			// 
			this.menuItemFileGraph.MergeIndex = 1;
			this.menuItemFileGraph.Name = "menuItemFileGraph";
			this.menuItemFileGraph.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G;
			this.menuItemFileGraph.Size = new System.Drawing.Size(204, 22);
			this.menuItemFileGraph.Text = "&Graph";
			this.menuItemFileGraph.Click += this.menuItemFileGraph_Click;
			// 
			// menuItemFileOrbit
			// 
			this.menuItemFileOrbit.MergeIndex = 2;
			this.menuItemFileOrbit.Name = "menuItemFileOrbit";
			this.menuItemFileOrbit.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V;
			this.menuItemFileOrbit.Size = new System.Drawing.Size(204, 22);
			this.menuItemFileOrbit.Text = "Orbit &Viewer";
			this.menuItemFileOrbit.Click += this.menuItemFileOrbit_Click;
			// 
			// menuItemSeparatorFile1
			// 
			this.menuItemSeparatorFile1.MergeIndex = 3;
			this.menuItemSeparatorFile1.Name = "menuItemSeparatorFile1";
			this.menuItemSeparatorFile1.Size = new System.Drawing.Size(201, 6);
			// 
			// menuItemOrbitalElements
			// 
			this.menuItemOrbitalElements.MergeIndex = 4;
			this.menuItemOrbitalElements.Name = "menuItemOrbitalElements";
			this.menuItemOrbitalElements.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			this.menuItemOrbitalElements.Size = new System.Drawing.Size(204, 22);
			this.menuItemOrbitalElements.Text = "&Orbital Elements";
			this.menuItemOrbitalElements.Click += this.menuItemOrbitalElements_Click;
			// 
			// menuItemSeparatorFile3
			// 
			this.menuItemSeparatorFile3.MergeIndex = 5;
			this.menuItemSeparatorFile3.Name = "menuItemSeparatorFile3";
			this.menuItemSeparatorFile3.Size = new System.Drawing.Size(201, 6);
			// 
			// menuItemExit
			// 
			this.menuItemExit.MergeIndex = 6;
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
			this.menuItemExit.Size = new System.Drawing.Size(204, 22);
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += this.menuItemExit_Click;
			// 
			// menuItemEdit
			// 
			this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemDatabase, this.menuItemUpdate, this.menuItemSeparatorEdit1, this.menuItemSettings });
			this.menuItemEdit.MergeIndex = 4;
			this.menuItemEdit.Name = "menuItemEdit";
			this.menuItemEdit.Size = new System.Drawing.Size(39, 20);
			this.menuItemEdit.Text = "&Edit";
			// 
			// menuItemDatabase
			// 
			this.menuItemDatabase.MergeIndex = 0;
			this.menuItemDatabase.Name = "menuItemDatabase";
			this.menuItemDatabase.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.menuItemDatabase.Size = new System.Drawing.Size(141, 22);
			this.menuItemDatabase.Text = "&Database";
			this.menuItemDatabase.Click += this.menuItemDatabase_Click;
			// 
			// menuItemUpdate
			// 
			this.menuItemUpdate.MergeIndex = 1;
			this.menuItemUpdate.Name = "menuItemUpdate";
			this.menuItemUpdate.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.menuItemUpdate.Size = new System.Drawing.Size(141, 22);
			this.menuItemUpdate.Text = "&Update";
			this.menuItemUpdate.Click += this.menuItemImport_Click;
			// 
			// menuItemSeparatorEdit1
			// 
			this.menuItemSeparatorEdit1.MergeIndex = 2;
			this.menuItemSeparatorEdit1.Name = "menuItemSeparatorEdit1";
			this.menuItemSeparatorEdit1.Size = new System.Drawing.Size(138, 6);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.MergeIndex = 3;
			this.menuItemSettings.Name = "menuItemSettings";
			this.menuItemSettings.Size = new System.Drawing.Size(141, 22);
			this.menuItemSettings.Text = "&Settings";
			this.menuItemSettings.Click += this.menuItemSettings_Click;
			// 
			// menuItemEphemeris
			// 
			this.menuItemEphemeris.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemEphemerisSettings, this.menuItemEphemerisSaveAs });
			this.menuItemEphemeris.MergeIndex = 1;
			this.menuItemEphemeris.Name = "menuItemEphemeris";
			this.menuItemEphemeris.Size = new System.Drawing.Size(74, 20);
			this.menuItemEphemeris.Text = "E&phemeris";
			this.menuItemEphemeris.Visible = false;
			// 
			// menuItemEphemerisSettings
			// 
			this.menuItemEphemerisSettings.MergeIndex = 0;
			this.menuItemEphemerisSettings.Name = "menuItemEphemerisSettings";
			this.menuItemEphemerisSettings.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.E;
			this.menuItemEphemerisSettings.Size = new System.Drawing.Size(188, 22);
			this.menuItemEphemerisSettings.Text = "&Settings";
			this.menuItemEphemerisSettings.Click += this.menuItemEphemSettings_Click;
			// 
			// menuItemEphemerisSaveAs
			// 
			this.menuItemEphemerisSaveAs.MergeIndex = 1;
			this.menuItemEphemerisSaveAs.Name = "menuItemEphemerisSaveAs";
			this.menuItemEphemerisSaveAs.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
			this.menuItemEphemerisSaveAs.Size = new System.Drawing.Size(188, 22);
			this.menuItemEphemerisSaveAs.Text = "Save &As";
			this.menuItemEphemerisSaveAs.Click += this.menuItemEphemerisSaveAs_Click;
			// 
			// menuItemGraph
			// 
			this.menuItemGraph.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemGraphSettings, this.menuItemGraphSaveAs });
			this.menuItemGraph.MergeIndex = 2;
			this.menuItemGraph.Name = "menuItemGraph";
			this.menuItemGraph.Size = new System.Drawing.Size(51, 20);
			this.menuItemGraph.Text = "&Graph";
			this.menuItemGraph.Visible = false;
			// 
			// menuItemGraphSettings
			// 
			this.menuItemGraphSettings.MergeIndex = 0;
			this.menuItemGraphSettings.Name = "menuItemGraphSettings";
			this.menuItemGraphSettings.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.G;
			this.menuItemGraphSettings.Size = new System.Drawing.Size(190, 22);
			this.menuItemGraphSettings.Text = "&Settings";
			this.menuItemGraphSettings.Click += this.menuItemGraphSettings_Click;
			// 
			// menuItemGraphSaveAs
			// 
			this.menuItemGraphSaveAs.MergeIndex = 1;
			this.menuItemGraphSaveAs.Name = "menuItemGraphSaveAs";
			this.menuItemGraphSaveAs.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
			this.menuItemGraphSaveAs.Size = new System.Drawing.Size(190, 22);
			this.menuItemGraphSaveAs.Text = "Save &As";
			this.menuItemGraphSaveAs.Click += this.menuItemGraphSaveAs_Click;
			// 
			// menuItemOrbit
			// 
			this.menuItemOrbit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuShowToolbox });
			this.menuItemOrbit.MergeIndex = 3;
			this.menuItemOrbit.Name = "menuItemOrbit";
			this.menuItemOrbit.Size = new System.Drawing.Size(46, 20);
			this.menuItemOrbit.Text = "&Orbit";
			this.menuItemOrbit.Visible = false;
			// 
			// mnuShowToolbox
			// 
			this.mnuShowToolbox.Checked = true;
			this.mnuShowToolbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuShowToolbox.MergeIndex = 0;
			this.mnuShowToolbox.Name = "mnuShowToolbox";
			this.mnuShowToolbox.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T;
			this.mnuShowToolbox.Size = new System.Drawing.Size(188, 22);
			this.mnuShowToolbox.Text = "Show &Toolbox";
			this.mnuShowToolbox.Click += this.mnuShowToolbox_Click;
			// 
			// menuItemView
			// 
			this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemViewAlwaysOnTop, this.menuItemViewStatusBar });
			this.menuItemView.MergeIndex = 5;
			this.menuItemView.Name = "menuItemView";
			this.menuItemView.Size = new System.Drawing.Size(44, 20);
			this.menuItemView.Text = "&View";
			// 
			// menuItemViewAlwaysOnTop
			// 
			this.menuItemViewAlwaysOnTop.MergeIndex = 0;
			this.menuItemViewAlwaysOnTop.Name = "menuItemViewAlwaysOnTop";
			this.menuItemViewAlwaysOnTop.Size = new System.Drawing.Size(157, 22);
			this.menuItemViewAlwaysOnTop.Text = "Always on &Top";
			this.menuItemViewAlwaysOnTop.Click += this.menuItemViewAlwaysOnTop_Click;
			// 
			// menuItemViewStatusBar
			// 
			this.menuItemViewStatusBar.Checked = true;
			this.menuItemViewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.menuItemViewStatusBar.MergeIndex = 1;
			this.menuItemViewStatusBar.Name = "menuItemViewStatusBar";
			this.menuItemViewStatusBar.Size = new System.Drawing.Size(157, 22);
			this.menuItemViewStatusBar.Text = "Show &status bar";
			this.menuItemViewStatusBar.Click += this.menuItemViewStatusBar_Click;
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemTileVert, this.menuItemTileHoriz, this.menuItemCascade, this.menuItemMinimizeAll, this.menuItemRestoreAll, this.menuItemClose });
			this.menuItemWindow.MergeIndex = 6;
			this.menuItemWindow.Name = "menuItemWindow";
			this.menuItemWindow.Size = new System.Drawing.Size(63, 20);
			this.menuItemWindow.Text = "&Window";
			this.menuItemWindow.Visible = false;
			// 
			// menuItemTileVert
			// 
			this.menuItemTileVert.MergeIndex = 0;
			this.menuItemTileVert.Name = "menuItemTileVert";
			this.menuItemTileVert.Size = new System.Drawing.Size(159, 22);
			this.menuItemTileVert.Text = "Tile &Horizontally";
			this.menuItemTileVert.Click += this.menuItemTileVert_Click;
			// 
			// menuItemTileHoriz
			// 
			this.menuItemTileHoriz.MergeIndex = 1;
			this.menuItemTileHoriz.Name = "menuItemTileHoriz";
			this.menuItemTileHoriz.Size = new System.Drawing.Size(159, 22);
			this.menuItemTileHoriz.Text = "Tile &Vertically";
			this.menuItemTileHoriz.Click += this.menuItemTileHoriz_Click;
			// 
			// menuItemCascade
			// 
			this.menuItemCascade.MergeIndex = 2;
			this.menuItemCascade.Name = "menuItemCascade";
			this.menuItemCascade.Size = new System.Drawing.Size(159, 22);
			this.menuItemCascade.Text = "&Cascade";
			this.menuItemCascade.Click += this.menuItemCascade_Click;
			// 
			// menuItemMinimizeAll
			// 
			this.menuItemMinimizeAll.MergeIndex = 3;
			this.menuItemMinimizeAll.Name = "menuItemMinimizeAll";
			this.menuItemMinimizeAll.Size = new System.Drawing.Size(159, 22);
			this.menuItemMinimizeAll.Text = "&Minimize All";
			this.menuItemMinimizeAll.Click += this.menuItemMinimizeAll_Click;
			// 
			// menuItemRestoreAll
			// 
			this.menuItemRestoreAll.MergeIndex = 4;
			this.menuItemRestoreAll.Name = "menuItemRestoreAll";
			this.menuItemRestoreAll.Size = new System.Drawing.Size(159, 22);
			this.menuItemRestoreAll.Text = "&Restore All";
			this.menuItemRestoreAll.Click += this.menuItemRestoreAll_Click;
			// 
			// menuItemClose
			// 
			this.menuItemClose.MergeIndex = 5;
			this.menuItemClose.Name = "menuItemClose";
			this.menuItemClose.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4;
			this.menuItemClose.Size = new System.Drawing.Size(159, 22);
			this.menuItemClose.Text = "&Close";
			this.menuItemClose.Click += this.menuItemClose_Click;
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemControls, this.menuItemAbout });
			this.menuItemHelp.MergeIndex = 7;
			this.menuItemHelp.Name = "menuItemHelp";
			this.menuItemHelp.Size = new System.Drawing.Size(44, 20);
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemControls
			// 
			this.menuItemControls.MergeIndex = 0;
			this.menuItemControls.Name = "menuItemControls";
			this.menuItemControls.Size = new System.Drawing.Size(119, 22);
			this.menuItemControls.Text = "&Controls";
			this.menuItemControls.Click += this.menuItemControls_Click;
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.MergeIndex = 1;
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(119, 22);
			this.menuItemAbout.Text = "&About";
			this.menuItemAbout.Click += this.menuItemAbout_Click;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusComets, this.statusSpace, this.statusProgressBar });
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
			// menuItemFileOrbit3d
			// 
			this.menuItemFileOrbit3d.MergeIndex = 2;
			this.menuItemFileOrbit3d.Name = "menuItemFileOrbit3d";
			this.menuItemFileOrbit3d.Size = new System.Drawing.Size(204, 22);
			this.menuItemFileOrbit3d.Text = "Orbit Viewer &3D";
			this.menuItemFileOrbit3d.Click += this.menuItemFileOrbit3d_Click;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.statusStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Comets";
			this.FormClosing += this.FormMain_FormClosing;
			this.FormClosed += this.FormMain_FormClosed;
			this.Load += this.FormMain_Load;
			this.MdiChildActivate += this.FormMain_MdiChildActivate;
			this.Shown += this.FormMain_Shown;
			this.Move += this.FormMain_Move;
			this.Resize += this.FormMain_Resize;
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemDatabase;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileEphemeris;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileGraph;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileOrbit;
		private System.Windows.Forms.ToolStripSeparator menuItemSeparatorFile1;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.ToolStripSeparator menuItemSeparatorEdit1;
		private System.Windows.Forms.ToolStripMenuItem menuItemUpdate;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
		private System.Windows.Forms.ToolStripSeparator menuItemSeparatorFile3;
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
		private System.Windows.Forms.ToolStripMenuItem menuItemFileOrbit3d;
	}
}

