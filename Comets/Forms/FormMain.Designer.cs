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
            this.menuItemEphemerides = new System.Windows.Forms.MenuItem();
            this.menuItemLightCurve = new System.Windows.Forms.MenuItem();
            this.menuItemOrbitVIewer = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorFile1 = new System.Windows.Forms.MenuItem();
            this.menuItemOrbitalElements = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorFile3 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemEdit = new System.Windows.Forms.MenuItem();
            this.menuItemDatabase = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorEdit1 = new System.Windows.Forms.MenuItem();
            this.menuItemImport = new System.Windows.Forms.MenuItem();
            this.menuItemExport = new System.Windows.Forms.MenuItem();
            this.menuItemSeparatorEdit2 = new System.Windows.Forms.MenuItem();
            this.menuItemLocation = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemView = new System.Windows.Forms.MenuItem();
            this.menuItemStatusBar = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.menuItemEdit,
            this.menuItemView,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEphemerides,
            this.menuItemLightCurve,
            this.menuItemOrbitVIewer,
            this.menuItemSeparatorFile1,
            this.menuItemOrbitalElements,
            this.menuItemSeparatorFile3,
            this.menuItemExit});
            this.menuItemFile.Text = "File";
            // 
            // menuItemEphemerides
            // 
            this.menuItemEphemerides.Index = 0;
            this.menuItemEphemerides.Text = "Ephemerides";
            // 
            // menuItemLightCurve
            // 
            this.menuItemLightCurve.Index = 1;
            this.menuItemLightCurve.Text = "Light curve";
            // 
            // menuItemOrbitVIewer
            // 
            this.menuItemOrbitVIewer.Index = 2;
            this.menuItemOrbitVIewer.Text = "Orbit viewer";
            this.menuItemOrbitVIewer.Visible = false;
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
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 1;
            this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDatabase,
            this.menuItemSeparatorEdit1,
            this.menuItemImport,
            this.menuItemExport,
            this.menuItemSeparatorEdit2,
            this.menuItemLocation,
            this.menuItemSettings});
            this.menuItemEdit.Text = "Edit";
            // 
            // menuItemDatabase
            // 
            this.menuItemDatabase.Index = 0;
            this.menuItemDatabase.Text = "Database";
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
            // 
            // menuItemSeparatorEdit2
            // 
            this.menuItemSeparatorEdit2.Index = 4;
            this.menuItemSeparatorEdit2.Text = "-";
            // 
            // menuItemLocation
            // 
            this.menuItemLocation.Index = 5;
            this.menuItemLocation.Text = "Location";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 6;
            this.menuItemSettings.Text = "Settings";
            // 
            // menuItemView
            // 
            this.menuItemView.Index = 2;
            this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStatusBar});
            this.menuItemView.Text = "View";
            // 
            // menuItemStatusBar
            // 
            this.menuItemStatusBar.Checked = true;
            this.menuItemStatusBar.Index = 0;
            this.menuItemStatusBar.Text = "Statur bar";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 3;
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
            this.statusLabel,
            this.statusComets,
            this.statusSpace,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 518);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = false;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(200, 17);
            this.statusLabel.Text = "Ready";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.statusSpace.Size = new System.Drawing.Size(369, 17);
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
            this.ClientSize = new System.Drawing.Size(784, 540);
            this.Controls.Add(this.statusStrip);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comets";
            this.Load += new System.EventHandler(this.FormMain_Load);
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
        private System.Windows.Forms.MenuItem menuItemEphemerides;
        private System.Windows.Forms.MenuItem menuItemLightCurve;
        private System.Windows.Forms.MenuItem menuItemOrbitVIewer;
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
        private System.Windows.Forms.MenuItem menuItemLocation;
        private System.Windows.Forms.MenuItem menuItemStatusBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusSpace;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusComets;
    }
}

