using Comets.Classes;
using Comets.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Comets.Forms
{
    public partial class FormMain : Form
    {
        public static List<Comet> mainList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        public bool isDataChanged = false;

        private FormDatabase fdb;

        public OpenFileDialog ofd;

        public static Settings Settings;

        public FormMain()
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();

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
                else
                {
                    this.Left = Settings.Left;
                    this.Top = Settings.Top;
                    this.Width = Settings.Width;
                    this.Height = Settings.Height;
                    this.StartPosition = FormStartPosition.Manual;
                }
            }
            else
            {
                this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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

            if (!Directory.Exists(Settings.Downloads))
                Directory.CreateDirectory(Settings.Downloads);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDataChanged && mainList.Count > 0)
            {
                ExportHelper.ExportMain((int)ElementTypes.Type.MPC, Settings.Database, mainList);
            }

            if (Settings.RememberWindowPosition)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    Settings.Maximized = true;
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    Settings.Maximized = false;
                    Settings.Left = this.Left;
                    Settings.Top = this.Top;
                    Settings.Width = this.Width;
                    Settings.Height = this.Height;
                }

                Settings.SaveSettings(Settings);
            }

            // da ponovo ispiše postavke
            if(Settings.HasErrors)
                Settings.SaveSettings(Settings);
        }

        private void menuItemStatusBar_Click(object sender, EventArgs e)
        {
            menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
            this.statusStrip.Visible = menuItemStatusBar.Checked;
        }

        private void menuItemImport_Click(object sender, EventArgs e)
        {
            using (FormImport formImport = new FormImport(this))
            {
                formImport.ShowDialog();
            }
        }

        private void menuItemDatabase_Click(object sender, EventArgs e)
        {
            fdb.ShowDialog();
        }

        public void SetStatusCometsLabel(int count)
        {
            this.statusComets.Text = String.Format("Comets: {0}", count.ToString());
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
            }
        }
    }
}
