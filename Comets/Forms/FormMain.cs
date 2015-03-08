using Comets.Classes;
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
        public static string downloadsDir;
        public static string downloadedFile;

        public static string localDataDir;
        public static string localDatabase;
        public bool isDataChanged = false;


        //public static string filename;
        //public static bool fileIsDownloaded = false;

        public static List<Comet> mainList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        FormDatabase fdb;
        public OpenFileDialog ofd;

        public FormMain()
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();

            int offset = 250;
            this.Width = SystemInformation.VirtualScreen.Width - offset;
            this.Height = SystemInformation.VirtualScreen.Height - offset;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {      
            downloadsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            downloadsDir += "\\Comets";

            if (!Directory.Exists(downloadsDir))
                Directory.CreateDirectory(downloadsDir);

            localDataDir = downloadsDir;
            localDatabase = localDataDir + "\\Comets.db";

            fdb = new FormDatabase();

            ofd = new OpenFileDialog();
            ofd.InitialDirectory = FormMain.localDataDir;
            ofd.Filter = "Orbital elements files (*.txt, *.dat, *.comet)|*.txt;*.dat;*.comet|" +
                        "Text documents (*.txt)|*.txt|" +
                        "DAT files (*.dat)|*.dat|" +
                        "COMET files (*.comet)|*.comet|" +
                        "All files (*.*)|*.*";

            if (File.Exists(localDatabase))
            {
                mainList = ImportHelper.ImportMain((int)ElementTypes.Type.MPC, localDatabase);
                userList = mainList;
                SetStatusCometsLabel(mainList.Count);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDataChanged && mainList.Count > 0)
            {
                ExportHelper.ExportMain((int)ElementTypes.Type.MPC, localDatabase);
            }
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
    }
}
