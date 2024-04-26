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
			mainMenu = new MenuStrip();
			menuItemFile = new ToolStripMenuItem();
			menuItemFileEphemeris = new ToolStripMenuItem();
			menuItemFileGraph = new ToolStripMenuItem();
			menuItemFileOrbit = new ToolStripMenuItem();
			menuItemSeparatorFile1 = new ToolStripSeparator();
			menuItemOrbitalElements = new ToolStripMenuItem();
			menuItemSeparatorFile3 = new ToolStripSeparator();
			menuItemExit = new ToolStripMenuItem();
			menuItemEphemeris = new ToolStripMenuItem();
			menuItemEphemerisSettings = new ToolStripMenuItem();
			menuItemEphemerisSaveAs = new ToolStripMenuItem();
			menuItemGraph = new ToolStripMenuItem();
			menuItemGraphSettings = new ToolStripMenuItem();
			menuItemGraphSaveAs = new ToolStripMenuItem();
			menuItemOrbit = new ToolStripMenuItem();
			mnuShowToolbox = new ToolStripMenuItem();
			menuItemEdit = new ToolStripMenuItem();
			menuItemDatabase = new ToolStripMenuItem();
			menuItemUpdate = new ToolStripMenuItem();
			menuItemSeparatorEdit1 = new ToolStripSeparator();
			menuItemSettings = new ToolStripMenuItem();
			menuItemView = new ToolStripMenuItem();
			menuItemViewAlwaysOnTop = new ToolStripMenuItem();
			menuItemViewStatusBar = new ToolStripMenuItem();
			menuItemWindow = new ToolStripMenuItem();
			menuItemTileVert = new ToolStripMenuItem();
			menuItemTileHoriz = new ToolStripMenuItem();
			menuItemCascade = new ToolStripMenuItem();
			menuItemMinimizeAll = new ToolStripMenuItem();
			menuItemRestoreAll = new ToolStripMenuItem();
			menuItemClose = new ToolStripMenuItem();
			menuItemHelp = new ToolStripMenuItem();
			menuItemControls = new ToolStripMenuItem();
			menuItemAbout = new ToolStripMenuItem();
			statusStrip = new StatusStrip();
			statusComets = new ToolStripStatusLabel();
			statusSpace = new ToolStripStatusLabel();
			statusProgressBar = new ToolStripProgressBar();
			mainMenu.SuspendLayout();
			statusStrip.SuspendLayout();
			SuspendLayout();
			// 
			// mainMenu
			// 
			mainMenu.Items.AddRange(new ToolStripItem[] { menuItemFile, menuItemEphemeris, menuItemGraph, menuItemOrbit, menuItemEdit, menuItemView, menuItemWindow, menuItemHelp });
			mainMenu.Location = new Point(0, 0);
			mainMenu.MdiWindowListItem = menuItemWindow;
			mainMenu.Name = "mainMenu";
			mainMenu.Padding = new Padding(0, 2, 0, 2);
			mainMenu.RenderMode = ToolStripRenderMode.System;
			mainMenu.Size = new Size(784, 24);
			mainMenu.TabIndex = 0;
			// 
			// menuItemFile
			// 
			menuItemFile.DropDownItems.AddRange(new ToolStripItem[] { menuItemFileEphemeris, menuItemFileGraph, menuItemFileOrbit, menuItemSeparatorFile1, menuItemOrbitalElements, menuItemSeparatorFile3, menuItemExit });
			menuItemFile.MergeIndex = 0;
			menuItemFile.Name = "menuItemFile";
			menuItemFile.Size = new Size(37, 20);
			menuItemFile.Text = "&File";
			// 
			// menuItemFileEphemeris
			// 
			menuItemFileEphemeris.MergeIndex = 0;
			menuItemFileEphemeris.Name = "menuItemFileEphemeris";
			menuItemFileEphemeris.ShortcutKeys = Keys.Control | Keys.E;
			menuItemFileEphemeris.Size = new Size(204, 22);
			menuItemFileEphemeris.Text = "&Ephemeris";
			menuItemFileEphemeris.Click += menuItemFileEphemerides_Click;
			// 
			// menuItemFileGraph
			// 
			menuItemFileGraph.MergeIndex = 1;
			menuItemFileGraph.Name = "menuItemFileGraph";
			menuItemFileGraph.ShortcutKeys = Keys.Control | Keys.G;
			menuItemFileGraph.Size = new Size(204, 22);
			menuItemFileGraph.Text = "&Graph";
			menuItemFileGraph.Click += menuItemFileGraph_Click;
			// 
			// menuItemFileOrbit
			// 
			menuItemFileOrbit.MergeIndex = 2;
			menuItemFileOrbit.Name = "menuItemFileOrbit";
			menuItemFileOrbit.ShortcutKeys = Keys.Control | Keys.V;
			menuItemFileOrbit.Size = new Size(204, 22);
			menuItemFileOrbit.Text = "Orbit &Viewer";
			menuItemFileOrbit.Click += menuItemFileOrbit_Click;
			// 
			// menuItemSeparatorFile1
			// 
			menuItemSeparatorFile1.MergeIndex = 3;
			menuItemSeparatorFile1.Name = "menuItemSeparatorFile1";
			menuItemSeparatorFile1.Size = new Size(201, 6);
			// 
			// menuItemOrbitalElements
			// 
			menuItemOrbitalElements.MergeIndex = 4;
			menuItemOrbitalElements.Name = "menuItemOrbitalElements";
			menuItemOrbitalElements.ShortcutKeys = Keys.Control | Keys.O;
			menuItemOrbitalElements.Size = new Size(204, 22);
			menuItemOrbitalElements.Text = "&Orbital elements";
			menuItemOrbitalElements.Click += menuItemOrbitalElements_Click;
			// 
			// menuItemSeparatorFile3
			// 
			menuItemSeparatorFile3.MergeIndex = 5;
			menuItemSeparatorFile3.Name = "menuItemSeparatorFile3";
			menuItemSeparatorFile3.Size = new Size(201, 6);
			// 
			// menuItemExit
			// 
			menuItemExit.MergeIndex = 6;
			menuItemExit.Name = "menuItemExit";
			menuItemExit.ShortcutKeys = Keys.Alt | Keys.F4;
			menuItemExit.Size = new Size(204, 22);
			menuItemExit.Text = "E&xit";
			menuItemExit.Click += menuItemExit_Click;
			// 
			// menuItemEphemeris
			// 
			menuItemEphemeris.DropDownItems.AddRange(new ToolStripItem[] { menuItemEphemerisSettings, menuItemEphemerisSaveAs });
			menuItemEphemeris.MergeIndex = 1;
			menuItemEphemeris.Name = "menuItemEphemeris";
			menuItemEphemeris.Size = new Size(74, 20);
			menuItemEphemeris.Text = "E&phemeris";
			menuItemEphemeris.Visible = false;
			// 
			// menuItemEphemerisSettings
			// 
			menuItemEphemerisSettings.MergeIndex = 0;
			menuItemEphemerisSettings.Name = "menuItemEphemerisSettings";
			menuItemEphemerisSettings.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
			menuItemEphemerisSettings.Size = new Size(188, 22);
			menuItemEphemerisSettings.Text = "&Settings";
			menuItemEphemerisSettings.Click += menuItemEphemSettings_Click;
			// 
			// menuItemEphemerisSaveAs
			// 
			menuItemEphemerisSaveAs.MergeIndex = 1;
			menuItemEphemerisSaveAs.Name = "menuItemEphemerisSaveAs";
			menuItemEphemerisSaveAs.ShortcutKeys = Keys.Control | Keys.S;
			menuItemEphemerisSaveAs.Size = new Size(188, 22);
			menuItemEphemerisSaveAs.Text = "Save &As";
			menuItemEphemerisSaveAs.Click += menuItemEphemerisSaveAs_Click;
			// 
			// menuItemGraph
			// 
			menuItemGraph.DropDownItems.AddRange(new ToolStripItem[] { menuItemGraphSettings, menuItemGraphSaveAs });
			menuItemGraph.MergeIndex = 2;
			menuItemGraph.Name = "menuItemGraph";
			menuItemGraph.Size = new Size(51, 20);
			menuItemGraph.Text = "&Graph";
			menuItemGraph.Visible = false;
			// 
			// menuItemGraphSettings
			// 
			menuItemGraphSettings.MergeIndex = 0;
			menuItemGraphSettings.Name = "menuItemGraphSettings";
			menuItemGraphSettings.ShortcutKeys = Keys.Control | Keys.Shift | Keys.G;
			menuItemGraphSettings.Size = new Size(190, 22);
			menuItemGraphSettings.Text = "&Settings";
			menuItemGraphSettings.Click += menuItemGraphSettings_Click;
			// 
			// menuItemGraphSaveAs
			// 
			menuItemGraphSaveAs.MergeIndex = 1;
			menuItemGraphSaveAs.Name = "menuItemGraphSaveAs";
			menuItemGraphSaveAs.ShortcutKeys = Keys.Control | Keys.S;
			menuItemGraphSaveAs.Size = new Size(190, 22);
			menuItemGraphSaveAs.Text = "Save &As";
			menuItemGraphSaveAs.Click += menuItemGraphSaveAs_Click;
			// 
			// menuItemOrbit
			// 
			menuItemOrbit.DropDownItems.AddRange(new ToolStripItem[] { mnuShowToolbox });
			menuItemOrbit.MergeIndex = 3;
			menuItemOrbit.Name = "menuItemOrbit";
			menuItemOrbit.Size = new Size(46, 20);
			menuItemOrbit.Text = "&Orbit";
			menuItemOrbit.Visible = false;
			// 
			// mnuShowToolbox
			// 
			mnuShowToolbox.Checked = true;
			mnuShowToolbox.CheckState = CheckState.Checked;
			mnuShowToolbox.MergeIndex = 0;
			mnuShowToolbox.Name = "mnuShowToolbox";
			mnuShowToolbox.ShortcutKeys = Keys.Control | Keys.T;
			mnuShowToolbox.Size = new Size(188, 22);
			mnuShowToolbox.Text = "Show &Toolbox";
			mnuShowToolbox.Click += mnuShowToolbox_Click;
			// 
			// menuItemEdit
			// 
			menuItemEdit.DropDownItems.AddRange(new ToolStripItem[] { menuItemDatabase, menuItemUpdate, menuItemSeparatorEdit1, menuItemSettings });
			menuItemEdit.MergeIndex = 4;
			menuItemEdit.Name = "menuItemEdit";
			menuItemEdit.Size = new Size(39, 20);
			menuItemEdit.Text = "&Edit";
			// 
			// menuItemDatabase
			// 
			menuItemDatabase.MergeIndex = 0;
			menuItemDatabase.Name = "menuItemDatabase";
			menuItemDatabase.ShortcutKeys = Keys.F5;
			menuItemDatabase.Size = new Size(141, 22);
			menuItemDatabase.Text = "&Database";
			menuItemDatabase.Click += menuItemDatabase_Click;
			// 
			// menuItemUpdate
			// 
			menuItemUpdate.MergeIndex = 1;
			menuItemUpdate.Name = "menuItemUpdate";
			menuItemUpdate.ShortcutKeys = Keys.F6;
			menuItemUpdate.Size = new Size(141, 22);
			menuItemUpdate.Text = "&Update";
			menuItemUpdate.Click += menuItemImport_Click;
			// 
			// menuItemSeparatorEdit1
			// 
			menuItemSeparatorEdit1.MergeIndex = 2;
			menuItemSeparatorEdit1.Name = "menuItemSeparatorEdit1";
			menuItemSeparatorEdit1.Size = new Size(138, 6);
			// 
			// menuItemSettings
			// 
			menuItemSettings.MergeIndex = 3;
			menuItemSettings.Name = "menuItemSettings";
			menuItemSettings.Size = new Size(141, 22);
			menuItemSettings.Text = "&Settings";
			menuItemSettings.Click += menuItemSettings_Click;
			// 
			// menuItemView
			// 
			menuItemView.DropDownItems.AddRange(new ToolStripItem[] { menuItemViewAlwaysOnTop, menuItemViewStatusBar });
			menuItemView.MergeIndex = 5;
			menuItemView.Name = "menuItemView";
			menuItemView.Size = new Size(44, 20);
			menuItemView.Text = "&View";
			// 
			// menuItemViewAlwaysOnTop
			// 
			menuItemViewAlwaysOnTop.MergeIndex = 0;
			menuItemViewAlwaysOnTop.Name = "menuItemViewAlwaysOnTop";
			menuItemViewAlwaysOnTop.Size = new Size(157, 22);
			menuItemViewAlwaysOnTop.Text = "Always on &Top";
			menuItemViewAlwaysOnTop.Click += menuItemViewAlwaysOnTop_Click;
			// 
			// menuItemViewStatusBar
			// 
			menuItemViewStatusBar.Checked = true;
			menuItemViewStatusBar.CheckState = CheckState.Checked;
			menuItemViewStatusBar.MergeIndex = 1;
			menuItemViewStatusBar.Name = "menuItemViewStatusBar";
			menuItemViewStatusBar.Size = new Size(157, 22);
			menuItemViewStatusBar.Text = "Show &status bar";
			menuItemViewStatusBar.Click += menuItemViewStatusBar_Click;
			// 
			// menuItemWindow
			// 
			menuItemWindow.DropDownItems.AddRange(new ToolStripItem[] { menuItemTileVert, menuItemTileHoriz, menuItemCascade, menuItemMinimizeAll, menuItemRestoreAll, menuItemClose });
			menuItemWindow.MergeIndex = 6;
			menuItemWindow.Name = "menuItemWindow";
			menuItemWindow.Size = new Size(63, 20);
			menuItemWindow.Text = "&Window";
			menuItemWindow.Visible = false;
			// 
			// menuItemTileVert
			// 
			menuItemTileVert.MergeIndex = 0;
			menuItemTileVert.Name = "menuItemTileVert";
			menuItemTileVert.Size = new Size(159, 22);
			menuItemTileVert.Text = "Tile &Horizontally";
			menuItemTileVert.Click += menuItemTileVert_Click;
			// 
			// menuItemTileHoriz
			// 
			menuItemTileHoriz.MergeIndex = 1;
			menuItemTileHoriz.Name = "menuItemTileHoriz";
			menuItemTileHoriz.Size = new Size(159, 22);
			menuItemTileHoriz.Text = "Tile &Vertically";
			menuItemTileHoriz.Click += menuItemTileHoriz_Click;
			// 
			// menuItemCascade
			// 
			menuItemCascade.MergeIndex = 2;
			menuItemCascade.Name = "menuItemCascade";
			menuItemCascade.Size = new Size(159, 22);
			menuItemCascade.Text = "&Cascade";
			menuItemCascade.Click += menuItemCascade_Click;
			// 
			// menuItemMinimizeAll
			// 
			menuItemMinimizeAll.MergeIndex = 3;
			menuItemMinimizeAll.Name = "menuItemMinimizeAll";
			menuItemMinimizeAll.Size = new Size(159, 22);
			menuItemMinimizeAll.Text = "&Minimize All";
			menuItemMinimizeAll.Click += menuItemMinimizeAll_Click;
			// 
			// menuItemRestoreAll
			// 
			menuItemRestoreAll.MergeIndex = 4;
			menuItemRestoreAll.Name = "menuItemRestoreAll";
			menuItemRestoreAll.Size = new Size(159, 22);
			menuItemRestoreAll.Text = "&Restore All";
			menuItemRestoreAll.Click += menuItemRestoreAll_Click;
			// 
			// menuItemClose
			// 
			menuItemClose.MergeIndex = 5;
			menuItemClose.Name = "menuItemClose";
			menuItemClose.ShortcutKeys = Keys.Control | Keys.F4;
			menuItemClose.Size = new Size(159, 22);
			menuItemClose.Text = "&Close";
			menuItemClose.Click += menuItemClose_Click;
			// 
			// menuItemHelp
			// 
			menuItemHelp.DropDownItems.AddRange(new ToolStripItem[] { menuItemControls, menuItemAbout });
			menuItemHelp.MergeIndex = 7;
			menuItemHelp.Name = "menuItemHelp";
			menuItemHelp.Size = new Size(44, 20);
			menuItemHelp.Text = "&Help";
			// 
			// menuItemControls
			// 
			menuItemControls.MergeIndex = 0;
			menuItemControls.Name = "menuItemControls";
			menuItemControls.Size = new Size(119, 22);
			menuItemControls.Text = "&Controls";
			menuItemControls.Click += menuItemControls_Click;
			// 
			// menuItemAbout
			// 
			menuItemAbout.MergeIndex = 1;
			menuItemAbout.Name = "menuItemAbout";
			menuItemAbout.Size = new Size(119, 22);
			menuItemAbout.Text = "&About";
			menuItemAbout.Click += menuItemAbout_Click;
			// 
			// statusStrip
			// 
			statusStrip.Items.AddRange(new ToolStripItem[] { statusComets, statusSpace, statusProgressBar });
			statusStrip.Location = new Point(0, 540);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new Size(784, 22);
			statusStrip.TabIndex = 1;
			statusStrip.Text = "statusStrip1";
			// 
			// statusComets
			// 
			statusComets.AutoSize = false;
			statusComets.Name = "statusComets";
			statusComets.Size = new Size(200, 17);
			statusComets.Text = "Comets: 0";
			statusComets.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// statusSpace
			// 
			statusSpace.Name = "statusSpace";
			statusSpace.Size = new Size(569, 17);
			statusSpace.Spring = true;
			// 
			// statusProgressBar
			// 
			statusProgressBar.AutoSize = false;
			statusProgressBar.Name = "statusProgressBar";
			statusProgressBar.Size = new Size(200, 16);
			statusProgressBar.Visible = false;
			// 
			// FormMain
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 562);
			Controls.Add(mainMenu);
			Controls.Add(statusStrip);
			Font = new Font("Tahoma", 8.25F);
			Icon = (Icon)resources.GetObject("$this.Icon");
			IsMdiContainer = true;
			MainMenuStrip = mainMenu;
			MinimumSize = new Size(800, 600);
			Name = "FormMain";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Comets";
			FormClosing += FormMain_FormClosing;
			FormClosed += FormMain_FormClosed;
			Load += FormMain_Load;
			MdiChildActivate += FormMain_MdiChildActivate;
			Shown += FormMain_Shown;
			Move += FormMain_Move;
			Resize += FormMain_Resize;
			mainMenu.ResumeLayout(false);
			mainMenu.PerformLayout();
			statusStrip.ResumeLayout(false);
			statusStrip.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
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
	}
}

