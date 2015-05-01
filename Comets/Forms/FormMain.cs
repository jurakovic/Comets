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
        #region Fields

        public static List<Comet> mainList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        public bool isDataChanged = false;

        private FormDatabase fdb;

        public OpenFileDialog ofd;

        public static Settings Settings;

        public static int ChildCount;

        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            Settings = Settings.LoadSettings();

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

        #region Form events

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

            fdb = new FormDatabase();

            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Settings.AppData;
            ofd.Filter = "Orbital elements files (*.txt, *.dat, *.comet)|*.txt;*.dat;*.comet|" +
                        "Text documents (*.txt)|*.txt|" +
                        "DAT files (*.dat)|*.dat|" +
                        "COMET files (*.comet)|*.comet|" +
                        "All files (*.*)|*.*";

            if (File.Exists(Settings.Database))
            {
                mainList = ImportHelper.ImportMain((int)ElementTypes.Type.MPC, Settings.Database);
                userList = mainList;
                SetStatusCometsLabel(mainList.Count);
            }

            if (Settings.DownloadOnStartup)
            {
                //TO DO
            }

            if (!Directory.Exists(Settings.Downloads))
                Directory.CreateDirectory(Settings.Downloads);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((isDataChanged || !File.Exists(Settings.Database) || Settings.IsSettingsChanged) && mainList.Count > 0)
            {
                ExportHelper.ExportMain(ElementTypes.Type.MPC, Settings.Database, mainList);
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

        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is FormEphemerisResult)
                this.menuItemEphemeris.Visible = true;
            else
                this.menuItemEphemeris.Visible = false;
        }

        #endregion

        #region File

        private void menuItemEphemerides_Click(object sender, EventArgs e)
        {
            FormEphemerisSettings fes = new FormEphemerisSettings() { Owner = this };
            fes.ShowDialog();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Ephemeris

        private void menuItemEphemSettings_Click(object sender, EventArgs e)
        {
            FormEphemerisResult fer = this.ActiveMdiChild as FormEphemerisResult;
            FormEphemerisSettings fes = new FormEphemerisSettings(fer.EphemerisSettings) { Owner = this };
            fes.ShowDialog();
        }

        #endregion

        #region Edit

        private void menuItemDatabase_Click(object sender, EventArgs e)
        {
            fdb.ShowDialog();
        }

        private void menuItemImport_Click(object sender, EventArgs e)
        {
            using (FormImport formImport = new FormImport(this))
            {
                formImport.ShowDialog();
            }
        }

        private void menuItemExport_Click(object sender, EventArgs e)
        {
            using (FormExport formExport = new FormExport())
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

        #region View

        private void menuItemStatusBar_Click(object sender, EventArgs e)
        {
            menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
            Settings.ShowStatusBar = menuItemStatusBar.Checked;
            this.statusStrip.Visible = menuItemStatusBar.Checked;
        }

        #endregion

        #region Window

        private void menuItemCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void menuItemTileHoriz_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuItemTileVert_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
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
