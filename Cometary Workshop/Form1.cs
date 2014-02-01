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

        //public static bool masterFilterFlag;
        public static bool[] filterFlags;
        public static double[] filterValues;
        public static string filterName;

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();

            filterFlags = new bool[18];
            filterValues = new double[9];
            filterName = null;
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


        private void radioManual_CheckedChanged(object sender, EventArgs e)
        {
            comboImportType.Enabled = radioManual.Checked;
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
            downloadedFile = Form1.downloadsDir + @"Soft00" +
                    "Cmt_" + DateTime.Now.Year + "-" +
                    DateTime.Now.Month.ToString("00") + "-" +
                    DateTime.Now.Day.ToString("00") + "_" +
                    DateTime.Now.Hour.ToString("00") + "-" +
                    DateTime.Now.Minute.ToString("00") + "-" +
                    DateTime.Now.Second.ToString("00") + ".txt";

            DownloadForm df = new DownloadForm(url, downloadedFile);
            df.ShowDialog();

            if (File.Exists(downloadedFile)) importMain(downloadedFile, 0);
        }

        private void btnBrowseImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = downloadsDir;

            ofd.Filter = "Text files (*.txt)|*.TXT|" +
                        "DAT files (*.dat)|*.DAT|" +
                        "COMET files (*.comet)|*.COMET|" +
                        "All files (*.*)|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbImportFilename.Text = ofd.FileName;
                localFile = ofd.FileName;
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            contextOptions.Show(this.tabPage1, (sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            if (tbImportFilename.Text.Length == 0)
            {
                labelWarning.Text = "Please first select file.";
                labelWarning.Visible = true;
                return;
            }

            filename = localFile;

            importMain(filename, 0);
        }


        private void updateCometListbox(List<Comet> list)
        {
            cometListbox.Items.Clear();

            if (list.Count == 0)
            {
                tId.Text = "";
                tName.Text = "";
                tT.Text = "";
                tQ.Text = "";
                tE.Text = "";
                tI.Text = "";
                tAn.Text = "";
                tPn.Text = "";
                tP.Text = "";
                tAph.Text = "";
                tA.Text = "";
                tG.Text = "";
                tK.Text = "";
                tSort.Text = "";
                tEquinox.Text = "";
            }

            else
            {
                foreach (Comet c in list) cometListbox.Items.Add(c.full);
                cometListbox.SelectedIndex = 0;
            }

            if (list.Count == masterList.Count)
                labelComets.Text = "Comets: " + list.Count;
            else
                labelComets.Text = "Comets: " + list.Count + " (" + masterList.Count + ")";
        }

        void importMain(string filename, int importType)
        {
            if (!File.Exists(filename)) return;

            masterList.Clear();
            userList.Clear();

            importMpc(filename);

            //userList = masterList.ToList();
            ///sortList(userList);

            copyListUseFilters();
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
                c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                
                //c.n = Comet.getMeanMotion_n(c.e, c.P);
                //c.M = Comet.getMeanAnomaly_M(c.T, today, c.e, c.n, c.q);
                //c.E = Comet.getEccentricAnomaly_E(c.e, c.M);
                //c.v = Comet.getTrueAnomaly_v(c.e, c.E, c.q, c.T, today);
                //c.L = Comet.getMeanLongitude_L(c.M, c.om, c.w);
                c.Q = Comet.getAphelionDistance_Q(c.e, c.a);
                //c.bw = Comet.getLongitudeOfPericenter_bw(c.om, c.w);
                //c.l = Comet.getTrueLongitude_l(c.v, c.bw);
                //c.F = Comet.getEccentricLongitude_F(c.w, c.om, c.E);

                c.set_sortkey();

                masterList.Add(c);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this.tabPage1, (sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1);
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

            sortList(userList);
        }

        private void ascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            descendingToolStripMenuItem.Checked = false;
            sortList(userList);
        }

        private void descendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ascendingToolStripMenuItem.Checked = false;
            sortList(userList);
        }

        public void copyListUseFilters()
        {
            //if (masterFilterFlag == false) return;

            userList.Clear();

            foreach (Comet c in masterList)
            {
                if (filterFlags[0] && !c.full.ToUpper().Contains(filterName.ToUpper())) continue;
                if (filterFlags[1] && c.full.ToUpper().Contains(filterName.ToUpper())) continue;
                if (filterFlags[2] && c.T < filterValues[1]) continue;
                if (filterFlags[3] && c.T > filterValues[1]) continue;
                if (filterFlags[4] && c.q < filterValues[2]) continue;
                if (filterFlags[5] && c.q > filterValues[2]) continue;
                if (filterFlags[6] && !(c.e < 1.0) && c.Q < filterValues[3]) continue;
                if (filterFlags[7] && !(c.e < 1.0) && c.Q > filterValues[3]) continue;
                if (filterFlags[8] && c.e < filterValues[4]) continue;
                if (filterFlags[9] && c.e > filterValues[4]) continue;
                if (filterFlags[10] && c.N < filterValues[5]) continue;
                if (filterFlags[11] && c.N > filterValues[5]) continue;
                if (filterFlags[12] && c.w < filterValues[6]) continue;
                if (filterFlags[13] && c.w > filterValues[6]) continue;
                if (filterFlags[14] && c.i < filterValues[7]) continue;
                if (filterFlags[15] && c.i > filterValues[7]) continue;
                if (filterFlags[16] && c.P < filterValues[8]) continue;
                if (filterFlags[17] && c.P > filterValues[8]) continue;

                userList.Add(c);
            }

            sortList(userList);
            //masterFilterFlag = false;
        }

        public void sortList(List<Comet> list)
        {
            if (list.Count == 0)
            {
                updateCometListbox(userList);
                return;
            }

            List<Comet> tempList = list.ToList();
            userList.Clear();

            if (noSortToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.ToList();

            else if (noSortToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
            {
                userList = tempList.ToList();
                userList.Reverse();
            }

            else if (nameToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.sortkey).ToList();

            else if (nameToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.sortkey).ToList();

            else if (perihelionDateToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.T).ToList();

            else if (perihelionDateToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.T).ToList();

            else if (perihelionDistanceToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.q).ToList();

            else if (perihelionDistanceToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.q).ToList();

            else if (aphelionDistanceToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.Q).ToList();

            else if (aphelionDistanceToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.Q).ToList();

            else if (longOfTheAscNodeToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.N).ToList();

            else if (longOfTheAscNodeToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.N).ToList();

            else if (eccentricityToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.e).ToList();

            else if (eccentricityToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.e).ToList();

            else if (argOfPericenterToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.w).ToList();

            else if (argOfPericenterToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.w).ToList();

            else if (inclinationToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.i).ToList();

            else if (inclinationToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.i).ToList();

            else if (periodToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = tempList.OrderBy(Comet => Comet.P).ToList();

            else if (periodToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.P).ToList();

            updateCometListbox(userList);
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

            //if (c.P > 10000)
            //{
            //    tP.Text = "";
            //    tAph.Text = "";
            //    tA.Text = "";
            //}
            //else
            //{
                tP.Text = String.Format("{0:0.000000}", c.P);
                tAph.Text = String.Format("{0:0.000000}", c.Q);
                tA.Text = String.Format("{0:0.000000}", c.a);
            //}

            tG.Text = String.Format("{0:0.0}", c.H);
            tK.Text = String.Format("{0:0.0}", c.G);

            tSort.Text = String.Format("{0:0.0000000}", c.sortkey);

            tEquinox.Text = "2000.0";
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            //if (ff == null) ff = new FiltersForm();

            //ff.ShowDialog();
            //copyListUsingFilters();

            gbFilters.Visible = !gbFilters.Visible;
            gbDetails.Visible = !gbDetails.Visible;
        }

        private void chName_CheckedChanged(object sender, EventArgs e)
        {
            comboName.Enabled = (sender as CheckBox).Checked;
            tbName.Enabled = (sender as CheckBox).Checked;
        }

        private void chPerihDate_CheckedChanged(object sender, EventArgs e)
        {
            comboPerihDate.Enabled = (sender as CheckBox).Checked;
            tbPerihDateD.Enabled = (sender as CheckBox).Checked;
            tbPerihDateM.Enabled = (sender as CheckBox).Checked;
            tbPerihDateY.Enabled = (sender as CheckBox).Checked;
            btnPerihDateNow.Enabled = (sender as CheckBox).Checked;
        }

        private void chPerihDist_CheckedChanged(object sender, EventArgs e)
        {
            comboPerihDist.Enabled = (sender as CheckBox).Checked;
            tbPerihDist.Enabled = (sender as CheckBox).Checked;
        }

        private void chAphDist_CheckedChanged(object sender, EventArgs e)
        {
            comboAphDist.Enabled = (sender as CheckBox).Checked;
            tbAphDist.Enabled = (sender as CheckBox).Checked;
        }

        private void chEcc_CheckedChanged(object sender, EventArgs e)
        {
            comboEcc.Enabled = (sender as CheckBox).Checked;
            tbEcc.Enabled = (sender as CheckBox).Checked;
        }

        private void chAscNode_CheckedChanged(object sender, EventArgs e)
        {
            comboAscNode.Enabled = (sender as CheckBox).Checked;
            tbAscNode.Enabled = (sender as CheckBox).Checked;
        }

        private void chLongPeric_CheckedChanged(object sender, EventArgs e)
        {
            comboLongPeric.Enabled = (sender as CheckBox).Checked;
            tbLongPeric.Enabled = (sender as CheckBox).Checked;
        }

        private void chIncl_CheckedChanged(object sender, EventArgs e)
        {
            comboIncl.Enabled = (sender as CheckBox).Checked;
            tbIncl.Enabled = (sender as CheckBox).Checked;
        }

        private void chPeriod_CheckedChanged(object sender, EventArgs e)
        {
            comboPeriod.Enabled = (sender as CheckBox).Checked;
            tbPeriod.Enabled = (sender as CheckBox).Checked;
        }

        private void btnPerihDateNow_Click(object sender, EventArgs e)
        {
            tbPerihDateD.Text = DateTime.Now.Day.ToString("00");
            tbPerihDateM.Text = DateTime.Now.Month.ToString("00");
            tbPerihDateY.Text = DateTime.Now.Year.ToString("0000");
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            if (chName.Checked && comboName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Contains or Does not contain", "Error", MessageBoxButtons.OK);
                return;
            }

            if ((chPerihDate.Checked && comboPerihDate.SelectedIndex == -1) ||
                (chPerihDist.Checked && comboPerihDist.SelectedIndex == -1) ||
                (chAphDist.Checked && comboAphDist.SelectedIndex == -1) ||
                (chEcc.Checked && comboEcc.SelectedIndex == -1) ||
                (chAscNode.Checked && comboAscNode.SelectedIndex == -1) ||
                (chLongPeric.Checked && comboLongPeric.SelectedIndex == -1) ||
                (chPeriod.Checked && comboPeriod.SelectedIndex == -1))
            {
                MessageBox.Show("Please select Greather than (>) or Less than (<)", "Error", MessageBoxButtons.OK);
                return;
            }

            if (chName.Checked && tbName.Text.Length == 0)
            {
                MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK);
                return;
            }

            if ((chPerihDate.Checked && tbPerihDateD.Text.Length == 0) ||
                (chPerihDate.Checked && tbPerihDateM.Text.Length == 0) ||
                (chPerihDate.Checked && tbPerihDateY.Text.Length == 0) ||
                (chPerihDist.Checked && tbPerihDist.Text.Length == 0) ||
                (chAphDist.Checked && tbAphDist.Text.Length == 0) ||
                (chEcc.Checked && tbEcc.Text.Length == 0) ||
                (chAscNode.Checked && tbAscNode.Text.Length == 0) ||
                (chLongPeric.Checked && tbLongPeric.Text.Length == 0) ||
                (chPeriod.Checked && tbPeriod.Text.Length == 0))
            {
                MessageBox.Show("Please enter value", "Error", MessageBoxButtons.OK);
                return;
            }

            //check date
            if (chPerihDate.Checked)
            {
                try
                {
                    DateTime test = new DateTime(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text));
                }
                catch
                {
                    MessageBox.Show("Invalid date", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            //clear all
            for (int i = 0; i < 18; i++) filterFlags[i] = false;
            for (int i = 0; i < 9; i++) filterValues[i] = 0.0;
            filterName = null;

            //get values
            if (chName.Checked)
            {
                //filterValue[0]
                filterName = tbName.Text;

                if (comboName.SelectedIndex == 0) filterFlags[0] = true;
                if (comboName.SelectedIndex == 1) filterFlags[1] = true;
            }

            if (chPerihDate.Checked)
            {
                filterValues[1] = Comet.GregToJul(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text), 0);

                if (comboPerihDate.SelectedIndex == 0) filterFlags[2] = true;
                if (comboPerihDate.SelectedIndex == 1) filterFlags[3] = true;
            }
            if (chPerihDist.Checked)
            {
                filterValues[2] = Convert.ToDouble(tbPerihDist.Text);

                if (comboPerihDist.SelectedIndex == 0) filterFlags[4] = true;
                if (comboPerihDist.SelectedIndex == 1) filterFlags[5] = true;
            }
            if (chAphDist.Checked)
            {
                filterValues[3] = Convert.ToDouble(tbAphDist.Text);

                if (comboAphDist.SelectedIndex == 0) filterFlags[6] = true;
                if (comboAphDist.SelectedIndex == 1) filterFlags[7] = true;
            }
            if (chEcc.Checked)
            {
                filterValues[4] = Convert.ToDouble(tbEcc.Text);

                if (comboEcc.SelectedIndex == 0) filterFlags[8] = true;
                if (comboEcc.SelectedIndex == 1) filterFlags[9] = true;
            }
            if (chAscNode.Checked)
            {
                filterValues[5] = Convert.ToDouble(tbAscNode.Text);

                if (comboAscNode.SelectedIndex == 0) filterFlags[10] = true;
                if (comboAscNode.SelectedIndex == 1) filterFlags[11] = true;
            }
            if (chLongPeric.Checked)
            {
                filterValues[6] = Convert.ToDouble(tbLongPeric.Text);

                if (comboLongPeric.SelectedIndex == 0) filterFlags[12] = true;
                if (comboLongPeric.SelectedIndex == 1) filterFlags[13] = true;
            }
            if (chIncl.Checked)
            {
                filterValues[7] = Convert.ToDouble(tbIncl.Text);

                if (comboIncl.SelectedIndex == 0) filterFlags[14] = true;
                if (comboIncl.SelectedIndex == 1) filterFlags[15] = true;
            }
            if (chPeriod.Checked)
            {
                filterValues[8] = Convert.ToDouble(tbPeriod.Text);

                if (comboPeriod.SelectedIndex == 0) filterFlags[16] = true;
                if (comboPeriod.SelectedIndex == 1) filterFlags[17] = true;
            }

            //masterFilterFlag = true;

            copyListUseFilters();
        }

        private void btnCancelFilters_Click(object sender, EventArgs e)
        {
            gbFilters.Visible = !gbFilters.Visible;
            gbDetails.Visible = !gbDetails.Visible;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                gbDetails.Visible = true;
                gbFilters.Visible = false;
            }
        }
    }
}