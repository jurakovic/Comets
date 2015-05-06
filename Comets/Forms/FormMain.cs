using Comets.Classes;
using Comets.Forms.Ephemeris;
using Comets.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormMain : Form
    {
        #region Properties

        public static List<Comet> MainList { get; set; }
        public static List<Comet> UserList { get; set; }

        public bool IsDataChanged { get; set; }

        private FormDatabase fdb { get; set; }

        public OpenFileDialog ofd { get; set; }

        public static Settings Settings { get; set; }

        public static int ChildCount { get; set; }

        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            MainList = new List<Comet>();
            UserList = new List<Comet>();

            Settings = Settings.LoadSettings();

            fdb = new FormDatabase();

            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Settings.AppData;
            ofd.Filter = "Orbital elements files (*.txt, *.dat, *.comet)|*.txt;*.dat;*.comet|" +
                        "Text documents (*.txt)|*.txt|" +
                        "DAT files (*.dat)|*.dat|" +
                        "COMET files (*.comet)|*.comet|" +
                        "All files (*.*)|*.*";

            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            int margin = 250;
            if (Settings.RememberWindowPosition)
            {
                if (Settings.Maximized)
                {
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
                    this.WindowState = FormWindowState.Maximized;
                }
                else //if (!Settings.Maximized && Settings.Left == 0 && Settings.Top == 0 && Settings.Width == 0 && Settings.Height == 0)
                {
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            else
            {
                this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            menuItemStatusBar.Checked = Settings.ShowStatusBar;
            this.statusStrip.Visible = Settings.ShowStatusBar;
        }

        #endregion

        #region Form_Load

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Settings.RememberWindowPosition && (Settings.Left > 0 || Settings.Top > 0 || Settings.Width > 0 || Settings.Height > 0))
            {
                this.Left = Settings.Left;
                this.Top = Settings.Top;
                this.Width = Settings.Width;
                this.Height = Settings.Height;
                this.StartPosition = FormStartPosition.Manual;
            }

            if (File.Exists(Settings.Database))
            {
                MainList = ImportHelper.ImportMain((int)ElementTypes.Type.MPC, Settings.Database);
                UserList = MainList;
                SetStatusCometsLabel(MainList.Count);
            }

            if (Settings.DownloadOnStartup)
            {
                //TO DO
            }

            if (!Directory.Exists(Settings.Downloads))
                Directory.CreateDirectory(Settings.Downloads);
        }

        #endregion

        #region Form_Closing

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((IsDataChanged || !File.Exists(Settings.Database) || Settings.IsSettingsChanged) && MainList.Count > 0)
            {
                ExportHelper.ExportMain(ElementTypes.Type.MPC, Settings.Database, MainList);
            }

            if (Settings.RememberWindowPosition)
            {
                Settings.Maximized = this.WindowState == FormWindowState.Maximized;
                Settings.Left = this.Left;
                Settings.Top = this.Top;
                Settings.Width = this.Width;
                Settings.Height = this.Height;

                Settings.SaveSettings(Settings);
            }

            // da ponovo ispiše postavke
            if(Settings.HasErrors)
                Settings.SaveSettings(Settings);
        }

        #endregion

        #region Form_MdiChildActivate

        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            this.menuItemEphemeris.Visible = this.ActiveMdiChild is FormEphemerisResult ? true : false;
        }

        #endregion

        #region Menu: File

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Menu: Ephemeris

        private void menuItemEphemerides_Click(object sender, EventArgs e)
        {
            using (FormEphemerisSettings fes = new FormEphemerisSettings() { Owner = this })
            {
                fes.ShowDialog();
            }
        }

        private void menuItemEphemSettings_Click(object sender, EventArgs e)
        {
            FormEphemerisResult fer = this.ActiveMdiChild as FormEphemerisResult;
            using (FormEphemerisSettings fes = new FormEphemerisSettings(fer.EphemerisSettings) { Owner = this })
            {
                fes.ShowDialog();
            }
        }

        #endregion

        #region Menu: Edit

        private void menuItemDatabase_Click(object sender, EventArgs e)
        {
            fdb.ShowDialog();
        }

        private void menuItemImport_Click(object sender, EventArgs e)
        {
            using (FormImport formImport = new FormImport() { Owner = this })
            {
                formImport.ShowDialog();
            }
        }

        private void menuItemExport_Click(object sender, EventArgs e)
        {
            using (FormExport formExport = new FormExport() { Owner = this })
            {
                formExport.ShowDialog();
            }
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            using (FormSettings frs = new FormSettings())
            {
                frs.ShowDialog();

                menuItemStatusBar.Checked = Settings.ShowStatusBar;
                this.statusStrip.Visible = Settings.ShowStatusBar;
            }
        }

        #endregion

        #region Menu: View

        private void menuItemStatusBar_Click(object sender, EventArgs e)
        {
            menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
            Settings.ShowStatusBar = menuItemStatusBar.Checked;
            this.statusStrip.Visible = menuItemStatusBar.Checked;
        }

        #endregion

        #region Menu: Window

        private void menuItemTileHoriz_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuItemTileVert_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void menuItemCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void menuItemMinimizeAll_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
                child.WindowState = FormWindowState.Minimized;
        }

        private void menuItemRestoreAll_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
                child.WindowState = FormWindowState.Normal;
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        public void AddWindowItem(string text)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Tag = ChildCount;
            menuItem.Text = ChildCount + " " + text;
            menuItem.Click += new EventHandler(this.menuItemWindow_Click);
            this.menuItemWindow.MenuItems.Add(menuItem);
        }

        public void menuItemWindow_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
                if (child.Text == (sender as MenuItem).Text)
                    child.Activate();
        }

        public void RenameWindowItem(int tag, string text)
        {
            foreach (MenuItem mi in menuItemWindow.MenuItems)
            {
                if (mi.Tag != null && (int)mi.Tag == tag)
                {
                    mi.Text = text;
                    break;
                }
            }        
        }

        public void RemoveWindowMenuItem(int tag)
        {
            foreach (MenuItem mi in menuItemWindow.MenuItems)
            {
                if (mi.Tag != null && (int)mi.Tag == tag)
                {
                    menuItemWindow.MenuItems.Remove(mi);
                    break;
                }
            }
        }

        public void SetWindowMenuItemVisible(bool visible)
        {
            this.menuItemWindow.Visible = visible;
        }

        #endregion

        #region Methods

        public void SetStatusCometsLabel(int count)
        {
            this.statusComets.Text = String.Format("Comets: {0}", count.ToString());
        }

        #endregion
    }
}
