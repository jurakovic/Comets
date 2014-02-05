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

        public static string lastSortItem = "nameToolStripMenuItem";

        //public static bool masterFilterFlag;
        public static bool[] filterFlags;
        public static double[] filterValues;
        public static string filterName;

        class Obs
        {
            public double latitude;
            public double longitude;
            public double tz;
            public bool dst;

            public Obs(double lat, double lon, double tz, bool dst)
            {
                this.latitude = lat;
                this.longitude = lon;
                this.tz = tz;
                this.dst = dst;
            }
        };

        Obs obs;

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

            filterFlags = new bool[18];
            filterValues = new double[9];
            filterName = null;


            comboLat.SelectedIndex = 0;
            comboLon.SelectedIndex = 0;

            DateTime dt = DateTime.Now.AddHours(1);
            tbStartYear.Text = dt.Year.ToString();
            tbStartMonth.Text = dt.Month.ToString("00");
            tbStartDay.Text = dt.Day.ToString("00");
            tbStartHour.Text = dt.Hour.ToString("00");
            tbStartMin.Text = "00";

            dt = dt.AddDays(15);
            tbEndYear.Text = dt.Year.ToString();
            tbEndMonth.Text = dt.Month.ToString("00");
            tbEndDay.Text = dt.Day.ToString("00");
            tbEndHour.Text = dt.Hour.ToString("00");
            tbEndMin.Text = "00";

            tbIntervalDay.Text = "1";
            tbIntervalHour.Text = "00";
            tbIntervalMin.Text = "00";
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
            comboCometEphem.Items.Clear();

            if (list.Count == 0)
            {
                t_id.Text = "";
                t_name.Text = "";
                t_T.Text = "";
                t_q1.Text = "";
                t_e.Text = "";
                t_i.Text = "";
                t_N.Text = "";
                t_w.Text = "";
                t_P.Text = "";
                t_Q2.Text = "";
                t_a.Text = "";
                t_g.Text = "";
                t_k.Text = "";
                t_sortKey.Text = "";
                tEquinox.Text = "";
            }

            else
            {
                foreach (Comet c in list)
                {
                    cometListbox.Items.Add(c.full);
                    comboCometEphem.Items.Add(c.full);
                }
                cometListbox.SelectedIndex = 0;
                comboCometEphem.SelectedIndex = 0;

                //comboCometEphem.MaxDropDownItems = 23;
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
                    c.k = Convert.ToDouble(str.Substring(91, 4).Trim());
                    c.g = Convert.ToDouble(str.Substring(96, 4).Trim());
                    c.full = str.Substring(102, 55).Trim();
                }
                catch
                {
                    continue;
                }

                //c.k *= 2.5;

                string[] idn = Comet.setIdNameFull(c.full);
                c.id = idn[0];
                c.name = idn[1];
                c.full = idn[2];

                //c.T = Comet.GregToJul(c.Ty, c.Tm, c.Td, c.Th);
                c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                c.P = Comet.getPeriod_P(c.q, c.e);
                c.a = Comet.getSemimajorAxis_a(c.q, c.e);

                c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                c.get_sortkey();

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
                if (filterFlags[6] && c.Q < filterValues[3]) continue;
                if (filterFlags[7] && c.Q > filterValues[3]) continue;
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

            if (nameToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
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

            t_id.Text = c.id;
            t_name.Text = c.name;
            t_T.Text = c.Ty.ToString() + "-" + c.Tm.ToString("00") + "-" + c.Td.ToString("00") + "." + c.Th.ToString("0000");
            t_q1.Text = c.q.ToString("0.000000");
            t_e.Text = c.e.ToString("0.000000");
            t_i.Text = c.i.ToString("0.0000");
            t_N.Text = c.N.ToString("0.0000");
            t_w.Text = c.w.ToString("0.0000");

            if (c.P > 10000 || c.e > 0.98)
            {
                t_P.Text = "";
                t_Q2.Text = "";
                t_a.Text = "";
            }
            else
            {
                t_P.Text = c.P.ToString("0.000000");
                t_Q2.Text = c.Q.ToString("0.000000");
                t_a.Text = c.a.ToString("0.000000");
            }

            t_g.Text = c.g.ToString("0.0");
            t_k.Text = c.k.ToString("0.0");

            t_sortKey.Text = c.sortkey.ToString("0.0000000");

            tEquinox.Text = "2000.0";
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            btnFilters.Text = gbFilters.Visible ? "Filters ▼" : "Filters ▲";

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
                //filterValues[1] = Comet.GregToJul(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text), 0);
                filterValues[1] = jd0(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text), 0);

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
            btnFilters_Click(sender, e);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                gbDetails.Visible = true;
                gbFilters.Visible = false;
            }
        }

        private void btnSettingsEphem_Click(object sender, EventArgs e)
        {
            if (panelEphemSettings.Height == 29)
            {
                btnSettingsEphem.Text = "Settings ▲";
                panelEphemSettings.Height = 330;
            }
            else
            {
                btnSettingsEphem.Text = "Settings ▼";
                panelEphemSettings.Height = 29;
            }
        }

        private void btnCalcEphem_Click(object sender, EventArgs e)
        {
            if (comboCometEphem.SelectedIndex < 0) return;

            //////////////////////////////
            //prebacit na save kod lokacije
            double locationLatitude = 45.783333;
            double locationLongitude = 15.933333;
            bool dst = checkBoxDST.Checked;
            double timezone;

            timezone = Convert.ToDouble(tbTimezone.Text.Substring(1, 2));
            timezone += Convert.ToDouble(tbTimezone.Text.Substring(4, 2)) / 60;
            if (tbTimezone.Text[0] == '-') timezone = -timezone;

            obs = new Obs(locationLatitude, locationLongitude, timezone * 60, dst);
            ///////////////////////////////


            DateTime localStartTime;
            DateTime localStopTime;

            try
            {
                localStartTime = new DateTime(Convert.ToInt32(tbStartYear.Text),
                                        Convert.ToInt32(tbStartMonth.Text),
                                        Convert.ToInt32(tbStartDay.Text),
                                        Convert.ToInt32(tbStartHour.Text),
                                        Convert.ToInt32(tbStartMin.Text), 0);

                localStopTime = new DateTime(Convert.ToInt32(tbEndYear.Text),
                                        Convert.ToInt32(tbEndMonth.Text),
                                        Convert.ToInt32(tbEndDay.Text),
                                        Convert.ToInt32(tbEndHour.Text),
                                        Convert.ToInt32(tbEndMin.Text), 0);
            }
            catch
            {
                MessageBox.Show("Invalid date");
                return;
            }
            if (obs.dst)
            {
                localStartTime = localStartTime.AddHours(-1);
                localStopTime = localStopTime.AddHours(-1);
            }

            DateTime UTStartTime = localStartTime.AddMinutes(-obs.tz);
            DateTime UTStopTIme = localStopTime.AddMinutes(-obs.tz);

            double jday = jd(UTStartTime.Year, UTStartTime.Month, UTStartTime.Day, UTStartTime.Hour, UTStartTime.Minute, UTStartTime.Second);
            double jdmax = jd(UTStopTIme.Year, UTStopTIme.Month, UTStopTIme.Day, UTStopTIme.Hour, UTStopTIme.Minute, UTStopTIme.Second);
            double locjday = jd(localStartTime.Year, localStartTime.Month, localStartTime.Day, localStartTime.Hour, localStartTime.Minute, localStartTime.Second);

            double interval = Convert.ToDouble(tbIntervalDay.Text) +
                            (Convert.ToDouble(tbIntervalHour.Text) + (Convert.ToDouble(tbIntervalMin.Text) / 60.0)) / 24;

            btnSettingsEphem.Text = "Settings ▼";
            panelEphemSettings.Height = 29;
            richEphem.Clear();

            if (chTime.Checked)
            {
                if (radioLocal.Checked) richEphem.Text += "Local date/time  ";
                else richEphem.Text += "   UT date/time  ";
            }
            if (chRA.Checked) richEphem.Text += "   R.A.   ";
            if (chDec.Checked) richEphem.Text += "   Dec   ";
            if (chAlt.Checked) richEphem.Text += "   Alt  ";
            if (chAz.Checked) richEphem.Text += "   Az   ";
            if (chEcLon.Checked) richEphem.Text += " Ecl.Lon ";
            if (chEcLat.Checked) richEphem.Text += " Ecl.Lat ";
            if (chElong.Checked) richEphem.Text += "   Elong. ";
            if (chHelioDist.Checked) richEphem.Text += "    r    ";
            if (chGeoDist.Checked) richEphem.Text += "    d    ";
            if (chMag.Checked) richEphem.Text += " Mag.";

            richEphem.Text += Environment.NewLine;

            Comet c = userList.ElementAt(comboCometEphem.SelectedIndex);

            DateTime begin = DateTime.Now;

            while (jday <= jdmax)
            {
                double[] dat = CometAlt(c, jday, obs);
                double alt = dat[0];
                double az = dat[1];
                //double ha = dat[2];
                double ra = dat[3];
                double dec = dat[4] - (dat[4] > 180.0 ? 360 : 0);
                double eclon = rev(dat[5]);
                double eclat = dat[6];
                double ill = dat[7];
                double r = dat[8];
                double dist = dat[9];
                double mag = dat[10];

                double[] sundat = SunAlt(jday, obs);
                double sunra = sundat[3];
                double sundec = sundat[4] - (sundat[4] > 180.0 ? 360 : 0);

                double[] sep = separation(ra, sunra, dec, sundec);
                double elong = sep[0];
                double pa = sep[1];

                string line = "";

                if (chTime.Checked) line += radioLocal.Checked ? dateString(locjday) : dateString(jday);
                if (chRA.Checked) line += "  " + hmsstring(ra / 15.0);
                if (chDec.Checked) line += "  " + anglestring(dec, false, true);
                if (chAlt.Checked) line += "  " + fixnum(alt, 5, 1) + "°";
                if (chAz.Checked) line += " " + fixnum(az, 6, 1) + "°";
                if (chEcLon.Checked) line += "  " + anglestring(eclon, true, true);
                if (chEcLat.Checked) line += "  " + anglestring(eclat, false, true);
                if (chElong.Checked) line += " " + fixnum(elong, 6, 1) + "°" + (pa >= 180 ? " W" : " E");
                if (chHelioDist.Checked) line += " " + fixnum(r, 8, 4);
                if (chGeoDist.Checked) line += " " + fixnum(dist, 8, 4);
                if (chMag.Checked) line += " " + fixnum(mag, 4, 1);

                richEphem.Text += line + Environment.NewLine;

                jday += interval;
                locjday += interval;
            }

            DateTime end = DateTime.Now;
            MessageBox.Show((end - begin).TotalMilliseconds.ToString());
        }

        double[] CometAlt(Comet c, double jday, Obs obs)
        {
            // Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
            double[] sun_xyz = sunxyz(jday);
            double[] cmt_xyz = comet_xyz(c, jday);
            double dx = cmt_xyz[0] + sun_xyz[0];
            double dy = cmt_xyz[1] + sun_xyz[1];
            double dz = cmt_xyz[2] + sun_xyz[2];
            double lon = rev(atan2d(dy, dx));
            double lat = atan2d(dz, Math.Sqrt(dx * dx + dy * dy));
            double[] radec = radecr(cmt_xyz, sun_xyz, jday, obs);
            double ra = radec[0];
            double dec = radec[1];
            double[] altaz = radec2aa(ra, dec, jday, obs);
            double dist = radec[2];
            double r = cmt_xyz[3];
            double mag = c.k + 5 * log10(dist) + 2.5 * c.g * log10(r);	// Schlyter's formula is wrong!
            return new double[] { altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag };
        }	// CometAlt()

        double[] SunAlt(double jday, Obs obs)
        {
            // return alt, az, time angle, ra, dec, ecl. long. and lat=0, illum=1, 0, dist, brightness 
            double[] sdat = sunxyz(jday);
            double ecl = 23.4393 - 3.563E-7 * (jday - 2451543.5);
            double xe = sdat[0];
            double ye = sdat[1] * cosd(ecl);
            double ze = sdat[1] * sind(ecl);
            double ra = rev(atan2d(ye, xe));
            double dec = atan2d(ze, Math.Sqrt(xe * xe + ye * ye));
            double[] topo = radec2aa(ra, dec, jday, obs);
            return new double[] { topo[0], topo[1], topo[2], ra, dec, sdat[4], 0, 1, 0, sdat[3], -26.74 };
        }

        double[] sunxyz(double jday)
        {
            // return x,y,z ecliptic coordinates, distance, true longitude
            // days counted from 1999 Dec 31.0 UT
            double d = jday - 2451543.5;
            double w = 282.9404 + 4.70935E-5 * d;		// argument of perihelion
            double e = 0.016709 - 1.151E-9 * d;
            double M = rev(356.0470 + 0.9856002585 * d); // mean anomaly
            double E = M + e * RAD2DEG * sind(M) * (1.0 + e * cosd(M));
            double xv = cosd(E) - e;
            double yv = Math.Sqrt(1.0 - e * e) * sind(E);
            double v = atan2d(yv, xv);		// true anomaly
            double r = Math.Sqrt(xv * xv + yv * yv);	// distance
            double lonsun = rev(v + w);	// true longitude
            double xs = r * cosd(lonsun);		// rectangular coordinates, zs = 0 for sun 
            double ys = r * sind(lonsun);
            return new double[] { xs, ys, 0, r, lonsun, 0 };
        }

        double[] comet_xyz(Comet cmt, double jday)
        {
            // heliocentric xyz for comet (cn is index to comets)
            // based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
            // returns heliocentric x, y, z, distance, longitude and latitude of object
            double d = jday - 2451543.5;
            double Tj = cmt.T;	// get julian day of perihelion time
            double q = cmt.q;
            double e = cmt.e;
            double v, r;
            if (e > 0.98)
            {
                // treat as near parabolic (approx. method valid inside orbit of Pluto)
                double k = 0.01720209895;	// Gaussian gravitational constant
                double a = 0.75 * (jday - Tj) * k * Math.Sqrt((1 + e) / (q * q * q));
                double b = Math.Sqrt(1 + a * a);
                double W = cbrt(b + a) - cbrt(b - a);
                double c = 1 + 1 / (W * W);
                double f = (1 - e) / (1 + e);
                double g = f / (c * c);
                double a1 = (2 / 3) + (2 / 5) * W * W;
                double a2 = (7 / 5) + (33 / 35) * W * W + (37 / 175) * W * W * W * W;
                double a3 = W * W * ((432 / 175) + (956 / 1125) * W * W + (84 / 1575) * W * W * W * W);
                double w = W * (1 + g * c * (a1 + a2 * g + a3 * g * g));
                v = 2 * atand(w);
                r = q * (1 + w * w) / (1 + w * w * f);
            }
            else
            {		// treat as elliptic
                double a = q / (1.0 - e);
                double P = 365.2568984 * Math.Sqrt(a * a * a);	// period in days
                double M = 360.0 * (jday - Tj) / P; 	// mean anomaly
                // eccentric anomaly E
                double E0 = M + RAD2DEG * e * sind(M) * (1.0 + e * cosd(M));
                double E1 = E0 - (E0 - RAD2DEG * e * sind(E0) - M) / (1.0 - e * cosd(E0));
                while (Math.Abs(E0 - E1) > 0.0005)
                {
                    E0 = E1;
                    E1 = E0 - (E0 - RAD2DEG * e * sind(E0) - M) / (1.0 - e * cosd(E0));
                }
                double xv = a * (cosd(E1) - e);
                double yv = a * Math.Sqrt(1.0 - e * e) * sind(E1);
                v = rev(atan2d(yv, xv));		// true anomaly
                r = Math.Sqrt(xv * xv + yv * yv);	// distance
            }	// from here common for all orbits
            double N; 
            if (radJ2000.Checked)
                N = cmt.N;
            else // precess from J2000.0 to now;
                N = cmt.N + 3.82394E-5 * d;
            double ww = cmt.w;	// why not precess this value?
            double i = cmt.i;
            double xh = r * (cosd(N) * cosd(v + ww) - sind(N) * sind(v + ww) * cosd(i));
            double yh = r * (sind(N) * cosd(v + ww) + cosd(N) * sind(v + ww) * cosd(i));
            double zh = r * (sind(v + ww) * sind(i));
            double lonecl = atan2d(yh, xh);
            double latecl = atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
            return new double[] { xh, yh, zh, r, lonecl, latecl };
        }	// comet_xyz()

        double[] radecr(double[] obj, double[] sun, double jday, Obs obs)
        {
            // radecr returns ra, dec and earth distance
            // obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
            // (note Sun coords are really Earth heliocentric coordinates with reverse signs)
            // Equatorial geocentric co-ordinates
            double xg = obj[0] + sun[0];
            double yg = obj[1] + sun[1];
            double zg = obj[2];
            // Obliquity of Ecliptic (exponent corrected, was E-9!)
            double obl;
            if (radJ2000.Checked)
                obl = 23.439291111;
            else
                obl = 23.439291111 - 3.563E-7 * (jday - 2451543.5);

            // Convert to eq. co-ordinates
            double x1 = xg;
            double y1 = yg * cosd(obl) - zg * sind(obl);
            double z1 = yg * sind(obl) + zg * cosd(obl);
            // RA and dec (33.2)
            double ra = rev(atan2d(y1, x1));
            double dec = atan2d(z1, Math.Sqrt(x1 * x1 + y1 * y1));
            double dist = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            return new double[] { ra, dec, dist };
        } 	// radecr()

        double[] radec2aa(double ra, double dec, double jday, Obs obs)
        {
            // Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
            // DOES NOT correct for parallax!
            // TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
            double TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
            double H = rev(TH0 + obs.longitude - ra);
            double alt = asind(sind(obs.latitude) * sind(dec) + cosd(obs.latitude) * cosd(dec) * cosd(H));
            double az = atan2d(sind(H), (cosd(H) * sind(obs.latitude) - tand(dec) * cosd(obs.latitude)));
            return new double[] { alt, rev(az + 180.0), H };
        }	// radec2aa()

        double[] separation(double ra1, double ra2, double dec1, double dec2)
        {
            // ra, dec may also be long, lat, but PA is relative to the chosen coordinate system
            double d = acosd(sind(dec1) * sind(dec2) + cosd(dec1) * cosd(dec2) * cosd(ra1 - ra2));		// (Meeus 17.1)
            if (d < 0.1) d = Math.Sqrt(sqr(rev2(ra1 - ra2) * cosd((dec1 + dec2) / 2)) + sqr(dec1 - dec2));	// (17.2)
            double pa = atan2d(sind(ra1 - ra2), cosd(dec2) * tand(dec1) - sind(dec2) * cosd(ra1 - ra2));		// angle
            return new double[] { d, rev(pa) };
        }	// end separation()

        string fixnum(double n, int l, int d)
        {
            // convert float n to right adjusted string of length l with d digits after decimal point.
            // the sign always requires one character, allow for that in l!
            double m = 1;
            for (int i = 0; i < d; i++) m *= 10;
            double n1 = Math.Round(Math.Abs(n) * m);
            double nint = Math.Floor(n1 / m);
            string nfract = (n1 - m * nint) + ""; // force conversion to string
            while (nfract.Length < d) nfract = "0" + nfract;
            string str = (n < 0 ? "-" : " ") + nint;
            if (d > 0) str = str + "." + nfract;
            while (str.Length < l) str = " " + str;
            return str;
        } // end fixnum()

        string hmsstring(double t)
        {
            // the caller must add a leading + if required.
            double hours = Math.Abs(t);
            double minutes = 60.0 * (hours - Math.Floor(hours));
            hours = Math.Floor(hours);
            double seconds = Math.Round(60.0 * (minutes - Math.Floor(minutes)));
            minutes = Math.Floor(minutes);
            if (seconds >= 60) { minutes += 1; seconds -= 60; }
            if (minutes >= 60) { hours += 1; minutes -= 60; }
            if (hours >= 24) { hours -= 24; }
            string hmsstr = (t < 0) ? "-" : "";
            hmsstr = ((hours < 10) ? "0" : "") + hours;
            hmsstr += ((minutes < 10) ? "h0" : "h") + minutes;
            hmsstr += ((seconds < 10) ? "m0" : "m") + seconds;
            return hmsstr;
        }	// end hmsstring()

        string anglestring(double a, bool circle, bool arcmin)
        {
            // Return angle as degrees:minutes. 'circle' is true for range between 0 and 360 
            // and false for -90 to +90, if 'arcmin' use deg and arcmin symbols
            double ar = Math.Round(a * 60) / 60;
            double deg = Math.Abs(ar);
            double min = Math.Round(60.0 * (deg - Math.Floor(deg)));
            if (min >= 60) { deg += 1; min = 0; }
            string anglestr = "";
            if (!circle) anglestr += (ar < 0 ? "-" : "+");
            if (circle) anglestr += ((Math.Floor(deg) < 100) ? "0" : "");
            anglestr += ((Math.Floor(deg) < 10) ? "0" : "") + Math.Floor(deg);
            if (arcmin) anglestr += ((min < 10) ? "°0" : "°") + (min) + "'";
            else anglestr += ((min < 10) ? ":0" : ":") + (min);
            return anglestr ;
        } // end anglestring()


        double jd(double year, double month, double day, double hour, double min, double sec)
        {
            double j = jd0(year, month, day, 0);
            j += (hour + (min / 60.0) + (sec / 3600.0)) / 24;
            return j;
        }

        double jd0(double year, double month, double day, double hour)
        {
            // The Julian date at 0 hours(*) UT at Greenwich
            // (*) or actual UT time if day comprises time as fraction
            double y = year;
            double m = month;
            double d = day + hour / 10000.0;
            if (m < 3) { m += 12; y -= 1; }
            double a = Math.Floor(y / 100);
            double b = 2 - a + Math.Floor(a / 4);
            double j = Math.Floor(365.25 * (y + 4716)) + Math.Floor(30.6001 * (m + 1)) + d + b - 1524.5;
            return j;
        }	// jd0()


        int[] jdtocd(double jd)
        {
            // The calendar date from julian date, see Meeus p. 63
            // Returns year, month, day, day of week, hours, minutes, seconds
            double Z = Math.Floor(jd + 0.5);
            double F = jd + 0.5 - Z;
            double A;
            if (Z < 2299161)
            {
                A = Z;
            }
            else
            {
                double alpha = Math.Floor((Z - 1867216.25) / 36524.25);
                A = Z + 1 + alpha - Math.Floor(alpha / 4);
            }
            double B = A + 1524;
            double C = Math.Floor((B - 122.1) / 365.25);
            double D = Math.Floor(365.25 * C);
            double E = Math.Floor((B - D) / 30.6001);
            double d = B - D - Math.Floor(30.6001 * E) + F;
            double year, month;
            if (E < 14)
            {
                month = E - 1;
            }
            else
            {
                month = E - 13;
            }
            if (month > 2)
            {
                year = C - 4716;
            }
            else
            {
                year = C - 4715;
            }
            double day = Math.Floor(d);
            double h = (d - day) * 24;
            double hours = Math.Floor(h);
            double m = (h - hours) * 60;
            double minutes = Math.Floor(m);
            double seconds = Math.Round((m - minutes) * 60);
            if (seconds >= 60)
            {
                minutes = minutes + 1;
                seconds = seconds - 60;
            }
            if (minutes >= 60)
            {
                hours = hours + 1;
                minutes = 0;
            }
            double dw = Math.Floor(jd + 1.5) - 7 * Math.Floor((jd + 1.5) / 7);
            return new int[] { (int)year, (int)month, (int)day, (int)dw, (int)hours, (int)minutes, (int)seconds };
        }

        string dateString(double jday)
        {
            int[] date = jdtocd(jday);

            int year = date[0];
            int month = date[1];
            int day = date[2];
            int hour = date[4];
            int minute = date[5];

            string datestr = "";

            datestr += ((day < 10) ? "0" : "") + day;
            datestr += ((month < 10) ? ".0" : ".") + month;
            datestr += "." + year;
            datestr += ((hour < 10) ? " 0" : " ") + hour;
            datestr += ((minute < 10) ? ":0" : ":") + minute;
            return datestr;
        }



        double rev(double angle) { return angle - Math.Floor(angle / 360.0) * 360.0; }		// 0<=a<360
        double rev2(double angle) { double a = rev(angle); return (a >= 180 ? a - 360.0 : a); }	// -180<=a<180
        double sind(double angle) { return Math.Sin(angle * DEG2RAD); }
        double cosd(double angle) { return Math.Cos(angle * DEG2RAD); }
        double tand(double angle) { return Math.Tan(angle * DEG2RAD); }
        double asind(double c) { return RAD2DEG * Math.Asin(c); }
        double acosd(double c) { return RAD2DEG * Math.Acos(c); }
        double atand(double c) { return RAD2DEG * Math.Atan(c); }
        double atan2d(double y, double x) { return RAD2DEG * Math.Atan2(y, x); }
        double log10(double x) { return 0.43429448190325182765112891891661 * Math.Log(x); }
        double sqr(double x) { return x * x; }
        double cbrt(double x) { return Math.Pow(x, 1 / 3.0); }
        double SGN(double x) { return (x < 0) ? -1 : +1; }
    }
}