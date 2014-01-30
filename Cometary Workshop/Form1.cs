using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Cometary_Workshop
{
    public partial class Form1 : Form
    {
        const double DEG2RAD = Math.PI / 180.0;
        const double RAD2DEG = 180.0 / Math.PI;

        public static string downloadsDir;
        public static string localDataDir;
        public static string downloadedFile;
        public static string localFile;
        public static string filename;
        public static bool fileIsDownloaded = false;

        public static List<Comet> masterList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        public static string lastSortItem = "noSortToolStripMenuItem";

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            downloadsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            downloadsDir += @"\Cometary Workshop\";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void radioInternet_CheckedChanged(object sender, EventArgs e)
        {
            btnDownload.Enabled = radioInternet.Checked;
            radioAutomatic.Checked = true;
            radioManual.Enabled = false;

            progDownload.Visible = radioInternet.Checked && fileIsDownloaded;
            labelDownload.Visible = radioInternet.Checked && fileIsDownloaded;

            labelWarning.Visible = false;
        }
        private void radioFile_CheckedChanged(object sender, EventArgs e)
        {
            tbImportFilename.Enabled = radioFile.Checked;
            btnBrowseImportFile.Enabled = radioFile.Checked;
            radioManual.Enabled = true;
        }

        private void radioManual_CheckedChanged(object sender, EventArgs e)
        {
            comboImportType.Enabled = radioManual.Checked;
        }

        private void chFilterName_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterName.Enabled = (sender as CheckBox).Checked;
            tbFilterName.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPerihDate_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterPerihDate.Enabled = (sender as CheckBox).Checked;
            tbFilterPerihDateD.Enabled = (sender as CheckBox).Checked;
            tbFilterPerihDateM.Enabled = (sender as CheckBox).Checked;
            tbFilterPerihDateY.Enabled = (sender as CheckBox).Checked;
            btnFilterPerihDateNow.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPericDist_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterPericDist.Enabled = (sender as CheckBox).Checked;
            tbFilterPericDist.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterEcc_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterEcc.Enabled = (sender as CheckBox).Checked;
            tbFilterEcc.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterAscNode_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterAscNode.Enabled = (sender as CheckBox).Checked;
            tbFilterAscNode.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterLongPeric_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterLongPeric.Enabled = (sender as CheckBox).Checked;
            tbFilterLongPeric.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterIncl_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterIncl.Enabled = (sender as CheckBox).Checked;
            tbFilterIncl.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPeriod_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterPeriod.Enabled = (sender as CheckBox).Checked;
            tbFilterPeriod.Enabled = (sender as CheckBox).Checked;
        }


        private void btnBrowseImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = downloadsDir;

            fd.Filter = "MPC (Soft00Cmt) (*.txt)|*.TXT|" +
                        "SkyMap (Soft01Cmt) (*.txt)|*.TXT|" +
                        "Guide (Soft02Cmt) (*.txt)|*.TXT|" +
                        "xephem (Soft03Cmt) (*.txt)|*.TXT|" +
                        "Home Planet (Soft04Cmt) (*.txt)|*.TXT|" +
                        "MyStars! (Soft05Cmt) (*.txt)|*.TXT|" +
                        "TheSky (Soft06Cmt) (*.txt)|*.TXT|" +
                        "Starry Night (Soft07Cmt) (*.txt)|*.TXT|" +
                        "Deep Space (Soft08Cmt) (*.txt)|*.TXT|" +
                        "PC-TCS (Soft09Cmt) (*.txt)|*.TXT|" +
                        "Earth Centered Universe (Soft10Cmt) (*.txt)|*.TXT|" +
                        "Dance of the Planets (Soft11Cmt) (*.txt)|*.TXT|" +
                        "MegaStar V4.x (Soft12Cmt) (*.txt)|*.TXT|" +
                        "SkyChart III (Soft13Cmt) (*.txt)|*.TXT|" +
                        "Voyager II (Soft14Cmt) (*.txt)|*.TXT|" +
                        "SkyTools (Soft15Cmt) (*.txt)|*.TXT|" +
                        "Autostar (Soft16Cmt) (*.txt)|*.TXT|" +
                        "Comet for Windows (Comet.dat) (*.dat)|*.DAT|" +
                        "NASA (ELEMENTS.COMET) (*.comet)|*.COMET|" +
                        "All files (*.*)|*.*";

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbImportFilename.Text = fd.FileName;
                localFile = fd.FileName;
            }
        }


        private void btnOptions_Click(object sender, EventArgs e)
        {
            contextOptions.Show(this.tabPage2, (sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (radioInternet.Checked)
            {
                if (fileIsDownloaded != true)
                {
                    labelWarning.Text = "Please first download data.";
                    labelWarning.Visible = true;
                    return;
                }

                filename = downloadedFile;

            }
            else //if radiofile.checked
            {
                if (tbImportFilename.Text.Length == 0)
                {
                    labelWarning.Text = "Please first select file.";
                    labelWarning.Visible = true;
                    return;
                }

                filename = localFile;
            }



            importMain(filename, 0);
        }


        private void updateCometListbox()
        {
            cometListbox.Items.Clear();
            foreach (Comet c in userList)
            {
                cometListbox.Items.Add(c.full);
            }
            cometListbox.SelectedIndex = 0;

            labelComets.Text = "Comets: " + userList.Count;
        }

        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            if (progDownload.InvokeRequired)
            {
                progDownload.Invoke((MethodInvoker)delegate()
                {
                    bwDownload_DoWork(sender, e);
                });
            }
            else
            {
                string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
                downloadedFile = Form1.downloadsDir + @"Soft00" +
                        "Cmt_" + DateTime.Now.Year + "-" +
                        DateTime.Now.Month.ToString("00") + "-" +
                        DateTime.Now.Day.ToString("00") + "_" +
                        DateTime.Now.Hour.ToString("00") + "-" +
                        DateTime.Now.Minute.ToString("00") + "-" +
                        DateTime.Now.Second.ToString("00") + ".txt";
                try
                {
                    WebClient Client = new WebClient();
                    Client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    Uri urll = new Uri(url);
                    Client.DownloadFileAsync(urll, downloadedFile);
                }
                catch
                {
                    MessageBox.Show("Unable to download orbital elements", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progDownload.Value = e.ProgressPercentage;
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            labelDownload.Visible = true;
            fileIsDownloaded = true;
        }

        void importMain(string filename, int importType)
        {
            masterList.Clear();
            userList.Clear();

            importMpc(filename);

            //userList = masterList.ToList();
            sortList();
            updateCometListbox();
        }

        void importMpc(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                //string str = line.Replace('.', ',');
                string str = line;

                Comet c = new Comet();

                try
                {
                    c.Ty = Convert.ToInt32(str.Substring(14, 4).Trim());
                    c.Tm = Convert.ToInt32(str.Substring(19, 2).Trim());
                    c.Td = Convert.ToInt32(str.Substring(22, 2).Trim());
                    c.Th = Convert.ToInt32(str.Substring(25, 4).Trim());
                    c.q = Convert.ToDouble(str.Substring(31, 8).Trim());
                    c.e = Convert.ToDouble(str.Substring(41, 8).Trim());
                    c.w = Convert.ToDouble(str.Substring(51, 8).Trim());
                    c.N = Convert.ToDouble(str.Substring(61, 8).Trim());
                    c.i = Convert.ToDouble(str.Substring(71, 8).Trim());
                    c.H = Convert.ToDouble(str.Substring(91, 4).Trim());
                    c.G = Convert.ToDouble(str.Substring(96, 4).Trim());
                    c.full = str.Substring(102, 55).Trim();
                }
                catch
                {
                    continue;
                }

                c.G *= 2.5;

                string[] idn = Comet.setIdNameFull(c.full);
                c.id = idn[0];
                c.name = idn[1];
                c.full = idn[2];

                int y = DateTime.Now.Year;
                int m = DateTime.Now.Month;
                int d = DateTime.Now.Day;
                int h = (int)(((double)DateTime.Now.Hour / 24) * 10000);
                double today = Comet.GregToJul(y, m, d, h);

                c.T = Comet.GregToJul(c.Ty, c.Tm, c.Td, c.Th);
                c.P = Comet.getPeriod_P(c.q, c.e);
                //c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                //c.n = Comet.getMeanMotion_n(c.e, c.P);
                //c.M = Comet.getMeanAnomaly_M(c.T, today, c.e, c.n, c.q);
                //c.E = Comet.getEccentricAnomaly_E(c.e, c.M);
                //c.v = Comet.getTrueAnomaly_v(c.e, c.E, c.q, c.T, today);
                //c.L = Comet.getMeanLongitude_L(c.M, c.om, c.w);
                //c.Q = Comet.getAphelionDistance_Q(c.e, c.a);
                //c.bw = Comet.getLongitudeOfPericenter_bw(c.om, c.w);
                //c.l = Comet.getTrueLongitude_l(c.v, c.bw);
                //c.F = Comet.getEccentricLongitude_F(c.w, c.om, c.E);

                c.set_sortkey();

                masterList.Add(c);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this.tabPage2, (sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1);
        }

        private void ContextClick1(object sender, EventArgs e)
        {
            //ako kliknem na item koji je vec odabran
            if ((sender as ToolStripMenuItem).Name == lastSortItem)
            {
                (sender as ToolStripMenuItem).Checked = true;
                return;
            }

            //da zapamti kako su zadnja 2 odabrana
            bool order = ascendingToolStripMenuItem.Checked;

            //da onaj koji je prethodno bio odabran sad bude false
            foreach (ToolStripItem item in contextSort.Items)
            {
                if (item.Name == lastSortItem) (item as ToolStripMenuItem).Checked = false;
            }

            ascendingToolStripMenuItem.Checked = order;
            descendingToolStripMenuItem.Checked = !order;

            lastSortItem = (sender as ToolStripMenuItem).Name;

            sortList();
        }

        private void ascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            descendingToolStripMenuItem.Checked = false;
            sortList();
        }

        private void descendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ascendingToolStripMenuItem.Checked = false;
            sortList();
        }

        public void sortList()
        {
            if (masterList.Count == 0)
            {
                //toolStripStatusLabel1.Text = "Ready";
                return;
            }

            else if (noSortToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.ToList();

            else if (noSortToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
            {
                userList = masterList.ToList();
                userList.Reverse();
            }

            else if (nameToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.sortkey).ToList();

            else if (nameToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.sortkey).ToList();

            else if (perihelionDateToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.T).ToList();

            else if (perihelionDateToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.T).ToList();

            else if (pericenterDistanceToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.q).ToList();

            else if (pericenterDistanceToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.q).ToList();

            else if (longOfTheAscNodeToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.N).ToList();

            else if (longOfTheAscNodeToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.N).ToList();

            else if (eccentricityToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.e).ToList();

            else if (eccentricityToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.e).ToList();

            else if (argOfPericenterToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.w).ToList();

            else if (argOfPericenterToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.w).ToList();

            else if (inclinationToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.i).ToList();

            else if (inclinationToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.i).ToList();

            else if (periodToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.P).ToList();

            else if (periodToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.P).ToList();

            updateCometListbox();
        }

        private void cometListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = cometListbox.SelectedIndex;

            Comet c = userList.ElementAt(ind);

            //tFull.Text = c.full;
            tId.Text = c.id;
            tName.Text = c.name;
            tT.Text = c.Ty.ToString() + "-" + c.Tm.ToString("00") + "-" + c.Td.ToString("00") + "." + c.Th.ToString("0000");
            tQ.Text = String.Format("{0:0.000000}", c.q);
            tE.Text = String.Format("{0:0.000000}", c.e);
            tI.Text = String.Format("{0:0.0000}", c.i);
            tAn.Text = String.Format("{0:0.0000}", c.N);
            tPn.Text = String.Format("{0:0.0000}", c.w);

            tG.Text = String.Format("{0:0.0}", c.H);
            tK.Text = String.Format("{0:0.0}", c.G);

            tSort.Text = String.Format("{0:0.0000000}", c.sortkey);

            tEquinox.Text = "2000.0";

            tP.Text = String.Format("{0:0.000000}", c.P);

            if (c.P > 10000)
            {
                tP.Text = "";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            progDownload.Value = 0;
            progDownload.Visible = true;
            labelDownload.Visible = false;
            labelWarning.Visible = false;
            
            bwDownload.RunWorkerAsync();
        }

        public int getCometListboxIndex()
        {
            return cometListbox.SelectedIndex;
        }

        public int listboxIndex
        {

            get { return this.cometListbox.SelectedIndex; }

            //set { this.txtLog.Text = value; }

        }
    }


}