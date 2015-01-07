using Comets.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormMain : Form
    {
        public static string downloadsDir;
        public static string localDataDir;
        public static string downloadedFile;
        public static string filename;
        public static bool fileIsDownloaded = false;

        public static List<Comet> masterList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        public FormMain()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {      
            downloadsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            downloadsDir += @"\Comets\";

            if (!Directory.Exists(downloadsDir))
                Directory.CreateDirectory(downloadsDir);
        }

        private void menuItemImport_Click(object sender, EventArgs e)
        {
            FormImport formImport = new FormImport(this);
            formImport.ShowDialog();
        }

        public void SetStatusCometsLabel(int count)
        {
            this.statusComets.Text = "Comets: " + count.ToString();
        }
    }
}
