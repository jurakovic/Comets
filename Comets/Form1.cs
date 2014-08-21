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

namespace Comets
{
    public partial class Form1 : Form
    {
        const double DEG2RAD = Math.PI / 180.0;
        const double RAD2DEG = 180.0 / Math.PI;

        public static string downloadsDir;
        public static string localDataDir;
        public static string downloadedFile;
        public static string filename;
        public static bool fileIsDownloaded = false;

        int importType;
        bool finishedImportFlag;
        bool filtersAppliedFlag;

        public static List<Comet> masterList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();

        public static Filter[] filters = new Filter[8];

        Observer obs;

        #region FormMain

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
            downloadsDir += @"\Comet Ephemerides\";
            if (!Directory.Exists(downloadsDir)) Directory.CreateDirectory(downloadsDir);

            for (int i = 0; i < filters.Count(); i++)
            {
                filters[i] = new Filter();
            }

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

        #endregion

        #region DownloadOrSelectFile

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

            if (File.Exists(downloadedFile)) importMain(downloadedFile);
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
                filename = tbImportFilename.Text.Trim();

                importType = getImportType(filename);

                if (importType == -1)
                {
                    labelImpFormat.Text = "unknown";
                    labelDetectedComets.Text = "-";
                    return;
                }

                if (importType == 0) labelImpFormat.Text = "MPC (Soft00Cmt)";
                if (importType == 1) labelImpFormat.Text = "SkyMap (Soft01Cmt)";
                if (importType == 2) labelImpFormat.Text = "Guide (Soft02Cmt)";
                if (importType == 3) labelImpFormat.Text = "xephem (Soft03Cmt)";
                if (importType == 4) labelImpFormat.Text = "Home Planet (Soft04Cmt)";
                if (importType == 5) labelImpFormat.Text = "MyStars! (Soft05Cmt)";
                if (importType == 6) labelImpFormat.Text = "TheSky (Soft06Cmt) / Autostar (Soft16Cmt)";
                if (importType == 7) labelImpFormat.Text = "Starry Night (Soft07Cmt)";
                if (importType == 8) labelImpFormat.Text = "Deep Space (Soft08Cmt)";
                if (importType == 9) labelImpFormat.Text = "PC-TCS (Soft09Cmt)";
                if (importType == 10) labelImpFormat.Text = "Earth Centered Universe (Soft10Cmt)";
                if (importType == 11) labelImpFormat.Text = "Dance of the Planets (Soft11Cmt)";
                if (importType == 12) labelImpFormat.Text = "MegaStar V4.x (Soft12Cmt)";
                if (importType == 13) labelImpFormat.Text = "SkyChart III (Soft13Cmt)";
                if (importType == 14) labelImpFormat.Text = "Voyager II (Soft14Cmt)";
                if (importType == 15) labelImpFormat.Text = "SkyTools (Soft15Cmt)";
                //if (importType == 16) labelImportFormat.Text = "Autostar (Soft16Cmt)";
                if (importType == 17) labelImpFormat.Text = "Comet for Windows";
                if (importType == 18) labelImpFormat.Text = "NASA (ELEMENTS.COMET)";

                labelDetectedComets.Text = GetNumberOfComets(filename, importType).ToString();
            }
            //else
            //{
            //    tbImportFilename.Text = "";
            //    labelImpFormat.Text = "(no file selected)";
            //    labelDetectedComets.Text = "-";
            //}
        }

        private void tbImportFilename_TextChanged(object sender, EventArgs e)
        {
            filename = tbImportFilename.Text.Trim().Trim('"');

            if (filename.Length == 0)
            {
                labelImpFormat.Text = "(no file selected)";
                labelDetectedComets.Text = "-";
                return;
            }

            if (!File.Exists(filename))
            {
                labelImpFormat.Text = "(file not found)";
                labelDetectedComets.Text = "-";
                return;
            }

            importType = getImportType(filename);

            if (importType == -1)
            {
                labelImpFormat.Text = "unknown";
                labelDetectedComets.Text = "-";
                return;
            }

            if (importType == 0) labelImpFormat.Text = "MPC (Soft00Cmt)";
            if (importType == 1) labelImpFormat.Text = "SkyMap (Soft01Cmt)";
            if (importType == 2) labelImpFormat.Text = "Guide (Soft02Cmt)";
            if (importType == 3) labelImpFormat.Text = "xephem (Soft03Cmt)";
            if (importType == 4) labelImpFormat.Text = "Home Planet (Soft04Cmt)";
            if (importType == 5) labelImpFormat.Text = "MyStars! (Soft05Cmt)";
            if (importType == 6) labelImpFormat.Text = "TheSky (Soft06Cmt) / Autostar (Soft16Cmt)";
            if (importType == 7) labelImpFormat.Text = "Starry Night (Soft07Cmt)";
            if (importType == 8) labelImpFormat.Text = "Deep Space (Soft08Cmt)";
            if (importType == 9) labelImpFormat.Text = "PC-TCS (Soft09Cmt)";
            if (importType == 10) labelImpFormat.Text = "Earth Centered Universe (Soft10Cmt)";
            if (importType == 11) labelImpFormat.Text = "Dance of the Planets (Soft11Cmt)";
            if (importType == 12) labelImpFormat.Text = "MegaStar V4.x (Soft12Cmt)";
            if (importType == 13) labelImpFormat.Text = "SkyChart III (Soft13Cmt)";
            if (importType == 14) labelImpFormat.Text = "Voyager II (Soft14Cmt)";
            if (importType == 15) labelImpFormat.Text = "SkyTools (Soft15Cmt)";
            //if (importType == 16) labelImportFormat.Text = "Autostar (Soft16Cmt)";
            if (importType == 17) labelImpFormat.Text = "Comet for Windows";
            if (importType == 18) labelImpFormat.Text = "NASA (ELEMENTS.COMET)";

            labelDetectedComets.Text = GetNumberOfComets(filename, importType).ToString();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (tbImportFilename.Text.Length == 0) btnBrowseImportFile_Click(sender, e);

            if (tbImportFilename.Text.Length == 0) return;

            if (!File.Exists(filename))
            {
                MessageBox.Show("File not found." + Environment.NewLine + "Check the file name and try again.               ", "Import",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            importMain(filename);
        }

        int getImportType(string filename)
        {
            Comet c = new Comet();

            string[] lines = File.ReadAllLines(filename);
            string lastLine = lines[lines.Count() - 1];

            try //mpc 0
            {
                c.Ty = Convert.ToInt32(lastLine.Substring(14, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(19, 2).Trim());
                c.Td = Convert.ToInt32(lastLine.Substring(22, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(25, 4).Trim());
                c.q = Convert.ToDouble(lastLine.Substring(30, 9).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(41, 8).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(51, 8).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(61, 8).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(71, 8).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(91, 4).Trim());
                c.g = Convert.ToDouble(lastLine.Substring(96, 4).Trim());
                string full = lastLine.Substring(102, 55).Trim();

                return 0;
            }
            catch
            {
                //go next
            }

            try //skymap 1
            {
                string full = lastLine.Substring(0, 44).Trim();
                c.Ty = Convert.ToInt32(lastLine.Substring(47, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(52, 2).Trim());
                c.Td = Convert.ToInt32(lastLine.Substring(55, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(58, 4).Trim());
                c.q = Convert.ToDouble(lastLine.Substring(63, 10).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(78, 10).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(88, 9).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(97, 9).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(106, 9).Trim());
                c.g = Convert.ToDouble(lastLine.Substring(115, 5).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(121, 5).Trim());

                return 1;
            }
            catch
            {

            }

            try //guide 2
            {
                string full = lastLine.Substring(0, 42).Trim();
                c.Td = Convert.ToInt32(lastLine.Substring(43, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(46, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(52, 2).Trim());
                c.Ty = Convert.ToInt32(lastLine.Substring(55, 5).Trim());
                c.q = Convert.ToDouble(lastLine.Substring(73, 10).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(85, 10).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(95, 10).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(107, 10).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(119, 10).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(140, 5).Trim());
                c.g = Convert.ToDouble(lastLine.Substring(145, 5).Trim());

                return 2;
            }
            catch
            {

            }

            try //xephem 3
            {
                string[] parts = lastLine.Split(',');
                if (parts.Count() == 13 && (parts[1] == "e" || parts[1] == "p" || parts[1] == "h")) return 3;
            }
            catch
            {

            }

            try //home planet 4
            {
                string[] parts = lastLine.Split(',');
                if (parts.Count() == 10) return 4;
            }
            catch
            {

            }

            try //mystars 5
            {
                string[] parts = lastLine.Split('\t');
                if (parts.Count() == 11) return 5;
            }
            catch
            {

            }

            try //thesky 6
            {
                string[] parts = lastLine.Split('|');
                if (parts.Count() == 11 && parts[0].Length == 39 && parts[1].Length == 4) return 6;
            }
            catch
            {

            }

            try //starry night 7
            {
                string name = lastLine.Substring(5, 29).Trim();
                c.g = Convert.ToDouble(lastLine.Substring(34, 6).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(48, 10).Trim());
                c.q = Convert.ToDouble(lastLine.Substring(59, 11).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(72, 10).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(82, 10).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(92, 10).Trim());
                c.T = Convert.ToDouble(lastLine.Substring(102, 14).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(129, 6).Trim()) / 2.5;
                string id = lastLine.Substring(136, 14).Trim();

                return 7;
            }
            catch
            {

            }

            try //deepspace 8
            {
                string[] parts = lastLine.Split(' ');
                if (parts.Count() == 12 && parts[0].Length == 1) return 8;
            }
            catch
            {

            }

            try //pc-tcs 9
            {
                string[] parts = lastLine.Split(' ');
                if (parts.Count() >= 12 && lastLine.Length == 126) return 9;
            }
            catch
            {

            }

            try //ecu 10
            {
                string[] parts = lastLine.Split(' ');
                if (parts.Count() == 13 && parts[0].Length == 1 && parts[1].Length == 1) return 10;
            }
            catch
            {

            }

            try //dance 11
            {
                string id = lastLine.Substring(0, 11).Trim();
                c.q = Convert.ToDouble(lastLine.Substring(11, 9).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(20, 9).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(29, 9).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(38, 9).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(47, 9).Trim());
                c.Ty = Convert.ToInt32(lastLine.Substring(56, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(61, 2).Trim());
                c.Td = Convert.ToInt32(lastLine.Substring(61, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(65, 4).Trim());

                return 11;
            }
            catch
            {

            }

            try //megastar 12
            {
                string name = lastLine.Substring(0, 30).Trim();
                string id = lastLine.Substring(30, 12).Trim();
                c.Ty = Convert.ToInt32(lastLine.Substring(42, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(47, 2).Trim());
                c.Td = Convert.ToInt32(lastLine.Substring(51, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(54, 4).Trim());
                c.q = Convert.ToDouble(lastLine.Substring(59, 12).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(73, 8).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(85, 8).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(97, 8).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(109, 8).Trim());
                c.g = Convert.ToDouble(lastLine.Substring(119, 6).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(126, 6).Trim());

                return 12;
            }
            catch
            {

            }

            try //skychart 13
            {
                string[] parts = lastLine.Split('\t');
                if (parts.Count() == 14 && parts[0].Length == 3) return 13;
            }
            catch
            {

            }

            //voyager 14
            if (lastLine.Contains("Jan") ||
                lastLine.Contains("Feb") ||
                lastLine.Contains("Mar") ||
                lastLine.Contains("Apr") ||
                lastLine.Contains("May") ||
                lastLine.Contains("Jun") ||
                lastLine.Contains("Jul") ||
                lastLine.Contains("Aug") ||
                lastLine.Contains("Sep") ||
                lastLine.Contains("Oct") ||
                lastLine.Contains("Nov") ||
                lastLine.Contains("Dec")) return 14;

            try //skytools 15
            {
                int y = Convert.ToInt32(lastLine.Substring(43, 4).Trim());
                int m = Convert.ToInt32(lastLine.Substring(48, 2).Trim());
                int d = Convert.ToInt32(lastLine.Substring(51, 2).Trim());

                c.Ty = Convert.ToInt32(lastLine.Substring(54, 4).Trim());
                c.Tm = Convert.ToInt32(lastLine.Substring(59, 2).Trim());
                c.Td = Convert.ToInt32(lastLine.Substring(61, 2).Trim());
                c.Th = Convert.ToInt32(lastLine.Substring(65, 4).Trim());

                c.q = Convert.ToDouble(lastLine.Substring(70, 9).Trim());
                c.e = Convert.ToDouble(lastLine.Substring(82, 8).Trim());
                c.w = Convert.ToDouble(lastLine.Substring(91, 8).Trim());
                c.N = Convert.ToDouble(lastLine.Substring(99, 8).Trim());
                c.i = Convert.ToDouble(lastLine.Substring(107, 7).Trim());

                c.g = Convert.ToDouble(lastLine.Substring(115, 5).Trim());
                c.k = Convert.ToDouble(lastLine.Substring(122, 4).Trim());

                return 15;
            }
            catch
            {

            }

            //comet for windows 17
            if (lines[1] == "[File]") return 17;

            //nasa elements.comet 18
            if (lines[1].Contains("-----")) return 18;

            return -1;
        }

        int GetNumberOfComets(string filename, int importType)
        {
            int lines = File.ReadLines(filename).Count();

            if (importType == 3 || importType == 8 || importType == 10) lines /= 2;
            if (importType == 8 || importType == 4 || importType == 5) --lines;
            if (importType == 7) lines -= 15;
            if (importType == 11) lines -= 5;
            if (importType == 14) lines -= 23;
            if (importType == 17) lines /= 13;
            if (importType == 18) lines -= 2;

            return lines;
        }

        #endregion

        #region ImportMain

        void importMain(string filename)
        {
            masterList.Clear();
            userList.Clear();

            //importType = 18; //za testiranje

            if (importType == -1)
            {
                MessageBox.Show("Unknown import type.               ", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (importType == 0)
                importMpc0(filename);
            if (importType == 1)
                importSkyMap1(filename);
            if (importType == 2)
                importGuide2(filename);
            if (importType == 3)
                importXephem3(filename);
            if (importType == 4)
                importHomePlanet4(filename);
            if (importType == 5)
                importMyStars5(filename);
            if (importType == 6)
                importTheSky6(filename);
            if (importType == 7)
                importStarryNight7(filename);
            if (importType == 8)
                importDeepSpace8(filename);
            if (importType == 9)
                importPcTcs9(filename);
            if (importType == 10)
                importEarthCenUniv10(filename);
            if (importType == 11)
                importDanceOfThePlanets11(filename);
            if (importType == 12)
                importMegaStar12(filename);
            if (importType == 13)
                importSkyChart13(filename);
            if (importType == 14)
                importVoyager14(filename);
            if (importType == 15)
                importSkyTools15(filename);
            if (importType == 17)
                importCometForWindows(filename);
            if (importType == 18)
                importNasaComet(filename);

            finishedImportFlag = true;
            filtersAppliedFlag = false;
            copyListUseFilters();
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
                t_N1.Text = "";
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
            t_N1.Text = c.N.ToString("0.0000");
            t_w.Text = c.w.ToString("0.0000");

            if (c.P > 10000 || c.e > 0.98)
            {
                t_P.Text = "";
                t_Q2.Text = "";
                t_a.Text = "";
                t_n2.Text = "";
            }
            else
            {
                t_P.Text = c.P.ToString("0.000000");
                t_Q2.Text = c.Q.ToString("0.000000");
                t_a.Text = c.a.ToString("0.000000");
                t_n2.Text = c.n.ToString("0.000000");
            }

            t_g.Text = c.g.ToString("0.0");
            t_k.Text = c.k.ToString("0.0");

            t_sortKey.Text = c.sortkey.ToString("0.00000000000");

            tEquinox.Text = "2000.0";
        }

        #endregion

        #region ImportFunctions
        
        void importMpc0(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    c.Ty = Convert.ToInt32(line.Substring(14, 4).Trim());
                    c.Tm = Convert.ToInt32(line.Substring(19, 2).Trim());
                    c.Td = Convert.ToInt32(line.Substring(22, 2).Trim());
                    c.Th = Convert.ToInt32(line.Substring(25, 4).Trim().PadRight(4, '0'));
                    c.q = Convert.ToDouble(line.Substring(30, 9).Trim());
                    c.e = Convert.ToDouble(line.Substring(41, 8).Trim());
                    c.w = Convert.ToDouble(line.Substring(51, 8).Trim());
                    c.N = Convert.ToDouble(line.Substring(61, 8).Trim());
                    c.i = Convert.ToDouble(line.Substring(71, 8).Trim());
                    c.g = Convert.ToDouble(line.Substring(91, 4).Trim());
                    c.k = Convert.ToDouble(line.Substring(96, 4).Trim());
                    c.full = line.Substring(102, 55).Trim();

                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);

                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importSkyMap1(string filename)
        {
            //pazit kod exporta zbog 167P

            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string tempfull = "", id = "", name = "";

                    tempfull = line.Substring(0, 44).Trim();
                    c.Ty = Convert.ToInt32(line.Substring(47, 4).Trim());
                    c.Tm = Convert.ToInt32(line.Substring(52, 2).Trim());
                    c.Td = Convert.ToInt32(line.Substring(55, 2).Trim());
                    c.Th = Convert.ToInt32(line.Substring(58, 4).Trim().PadRight(4, '0'));
                    c.q = Convert.ToDouble(line.Substring(63, 10).Trim());
                    c.e = Convert.ToDouble(line.Substring(78, 10).Trim());
                    c.w = Convert.ToDouble(line.Substring(88, 9).Trim());
                    c.N = Convert.ToDouble(line.Substring(97, 9).Trim());
                    c.i = Convert.ToDouble(line.Substring(106, 9).Trim());
                    c.g = Convert.ToDouble(line.Substring(115, 5).Trim());
                    c.k = Convert.ToDouble(line.Substring(121, 5).Trim());


                    if ((tempfull[0] == 'C' || tempfull[0] == 'P' || tempfull[0] == 'D' || tempfull[0] == 'X') && tempfull[1] == '/')
                    {
                        int spaces = tempfull.Count(f => f == ' ');

                        if (spaces == 1)
                        {
                            id = tempfull;
                        }
                        else //if (spaces >= 2)
                        {
                            int secondspace = GetNthIndex(tempfull, ' ', 2);
                            id = tempfull.Substring(0, secondspace);
                            name = tempfull.Substring(secondspace + 1, tempfull.Length - secondspace - 1);
                            //tempfull = id + " (" + name + ")";
                        }
                    }
                    else
                    {
                        int spaceind = tempfull.IndexOf(' ');
                        if (spaceind == -1)
                        {
                            //ako nema razmaka, "282P"
                            id = tempfull;
                        }
                        else
                        {
                            id = tempfull.Substring(0, spaceind);
                            name = tempfull.Substring(spaceind + 1, tempfull.Length - spaceind - 1);
                        }
                    }

                    c.full = Comet.setFullFromIdName(id, name);
                    c.id = id;
                    c.name = name;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importGuide2(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string tempfull = "", id = "", name = "";

                    tempfull = line.Substring(0, 42).Trim();
                    c.Td = Convert.ToInt32(line.Substring(43, 2).Trim());
                    c.Th = Convert.ToInt32(line.Substring(46, 4).Trim().PadRight(4, '0'));
                    c.Tm = Convert.ToInt32(line.Substring(52, 2).Trim());
                    c.Ty = Convert.ToInt32(line.Substring(55, 5).Trim());
                    c.q = Convert.ToDouble(line.Substring(73, 10).Trim());
                    c.e = Convert.ToDouble(line.Substring(85, 10).Trim());
                    c.i = Convert.ToDouble(line.Substring(95, 10).Trim());
                    c.w = Convert.ToDouble(line.Substring(107, 10).Trim());
                    c.N = Convert.ToDouble(line.Substring(119, 10).Trim());
                    c.g = Convert.ToDouble(line.Substring(140, 5).Trim());
                    c.k = Convert.ToDouble(line.Substring(145, 5).Trim());

                    if (tempfull.Contains('('))
                    {
                        int ind = tempfull.IndexOf('(');

                        name = tempfull.Substring(0, ind - 1);
                        if (name.Contains("/")) name = name.Substring(2, name.Length - 2);

                        id = tempfull.Substring(ind + 1, tempfull.Length - ind - 2);
                    }
                    else
                    {
                        id = tempfull;
                    }

                    c.full = Comet.setFullFromIdName(id, name);
                    c.id = id;
                    c.name = name;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }



                masterList.Add(c);
            }
        }

        void importXephem3(string filename)
        {
            //
            // ispraviti vrijeme perihela
            //

            string[] lines = File.ReadAllLines(filename);

            for (int i = 1; i < lines.Count(); i += 2)
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = lines[i].Split(',');

                    c.full = parts[0];
                    if (c.full[c.full.Length - 1] == '/') c.full = c.full.TrimEnd('/');
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    if (parts[1] == "e")
                    {
                        c.i = Convert.ToDouble(parts[2]);
                        c.N = Convert.ToDouble(parts[3]);
                        c.w = Convert.ToDouble(parts[4]);
                        double smAxis = Convert.ToDouble(parts[5]);
                        double mdMotion = Convert.ToDouble(parts[6]);
                        c.e = Convert.ToDouble(parts[7]);
                        double mAnomaly = Convert.ToDouble(parts[8]);

                        string[] date = parts[9].Split('/');
                        int m = Convert.ToInt32(date[0]);
                        int y = Convert.ToInt32(date[2]);
                        string[] dh = date[1].Split('.');
                        int d = Convert.ToInt32(dh[0]);
                        int h = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                        c.g = Convert.ToDouble(parts[11].Substring(2, parts[11].Length - 2));
                        c.k = Convert.ToDouble(parts[12]);

                        c.q = smAxis * (1 - c.e);
                        double T = jd0(y, m, d, h);

                        if (mAnomaly == 0) mAnomaly = 0.00000001;
                        if (mdMotion == 0) mdMotion = 0.00000001;

                        c.T = T - mAnomaly / mdMotion;
                        ////////////////////////////////////// to malo pogledati i ispraviti


                        int[] newdate = jdtocd(c.T);
                        c.Ty = newdate[0];
                        c.Tm = newdate[1];
                        c.Td = newdate[2];
                        c.Th = (int)(((newdate[4] + (newdate[5] / 60.0) + (newdate[6] / 3600.0)) / 24) * 10000);
                    }
                    if (parts[1] == "p")
                    {
                        string[] date = parts[2].Split('/');
                        c.Tm = Convert.ToInt32(date[0]);
                        c.Ty = Convert.ToInt32(date[2]);
                        string[] dh = date[1].Split('.');
                        c.Td = Convert.ToInt32(dh[0]);
                        c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                        c.i = Convert.ToDouble(parts[3]);
                        c.w = Convert.ToDouble(parts[4]);
                        c.q = Convert.ToDouble(parts[5]);
                        c.N = Convert.ToDouble(parts[6]);
                        c.g = Convert.ToDouble(parts[8]);
                        c.k = Convert.ToDouble(parts[9]);

                        c.e = 1.0;
                        c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    }
                    if (parts[1] == "h")
                    {
                        string[] date = parts[2].Split('/');
                        c.Tm = Convert.ToInt32(date[0]);
                        c.Ty = Convert.ToInt32(date[2]);
                        string[] dh = date[1].Split('.');
                        c.Td = Convert.ToInt32(dh[0]);
                        c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                        c.i = Convert.ToDouble(parts[3]);
                        c.N = Convert.ToDouble(parts[4]);
                        c.w = Convert.ToDouble(parts[5]);
                        c.e = Convert.ToDouble(parts[6]);
                        c.q = Convert.ToDouble(parts[7]);
                        c.g = Convert.ToDouble(parts[9]);
                        c.k = Convert.ToDouble(parts[10]);

                        c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    }

                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importHomePlanet4(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 1; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = lines[i].Split(',');

                    c.full = parts[0];
                    if (c.full[c.full.Length - 1] == '/') c.full = c.full.TrimEnd('/');
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    string[] date = parts[1].Split('-');
                    c.Ty = Convert.ToInt32(date[0]);
                    c.Tm = Convert.ToInt32(date[1]);
                    string[] dh = date[2].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(parts[2]);
                    c.e = Convert.ToDouble(parts[3]);
                    c.w = Convert.ToDouble(parts[4]);
                    c.N = Convert.ToDouble(parts[5]);
                    c.i = Convert.ToDouble(parts[6]);

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importMyStars5(string filename)
        {
            //
            // w zapravo nije w
            //

            string[] lines = File.ReadAllLines(filename);

            for (int i = 1; i < lines.Count(); i++)
            {
                Comet c = new Comet();
                double T;
                int h;
                string[] Th;

                try
                {
                    string[] parts = lines[i].Split('\t');

                    c.full = parts[0];
                    c.full = c.full.TrimEnd(';');
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    Th = parts[1].Split('.');
                    T = Convert.ToDouble(Th[0]);
                    h = Convert.ToInt32(Th[1].Trim().PadRight(4, '0'));
                    c.T = T + 2400000.5;

                    int[] dd = jdtocd(c.T);
                    c.Ty = dd[0];
                    c.Tm = dd[1];
                    c.Td = dd[2];
                    c.Th = h;

                    c.w = Convert.ToDouble(parts[2]);
                    c.e = Convert.ToDouble(parts[3]);
                    c.q = Convert.ToDouble(parts[4]);
                    c.i = Convert.ToDouble(parts[5]);
                    c.N = Convert.ToDouble(parts[6]);
                    c.g = Convert.ToDouble(parts[7]);
                    c.k = Convert.ToDouble(parts[8]);

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importTheSky6(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = line.Split('|');

                    c.full = parts[0].Trim();
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    string date = parts[2].Trim().PadRight(13, '0');
                    c.Ty = Convert.ToInt32(date.Substring(0, 4));
                    c.Tm = Convert.ToInt32(date.Substring(4, 2));
                    c.Td = Convert.ToInt32(date.Substring(6, 2));
                    c.Th = Convert.ToInt32(date.Substring(9, 4).Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(parts[3]);
                    c.e = Convert.ToDouble(parts[4]);
                    c.w = Convert.ToDouble(parts[5]);
                    c.N = Convert.ToDouble(parts[6]);
                    c.i = Convert.ToDouble(parts[7]);
                    c.g = Convert.ToDouble(parts[8]);
                    c.k = Convert.ToDouble(parts[9]) / 2.5;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importStarryNight7(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 15; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    c.name = lines[i].Substring(5, 29).Trim();
                    c.g = Convert.ToDouble(lines[i].Substring(34, 6).Trim());
                    c.e = Convert.ToDouble(lines[i].Substring(48, 10).Trim());
                    c.q = Convert.ToDouble(lines[i].Substring(59, 11).Trim());
                    c.N = Convert.ToDouble(lines[i].Substring(72, 10).Trim());
                    c.w = Convert.ToDouble(lines[i].Substring(82, 10).Trim());
                    c.i = Convert.ToDouble(lines[i].Substring(92, 10).Trim());
                    c.T = Convert.ToDouble(lines[i].Substring(102, 14).Trim());
                    c.k = Convert.ToDouble(lines[i].Substring(129, 6).Trim()) / 2.5;
                    c.id = lines[i].Substring(136, 14).Trim();

                    c.full = Comet.setFullFromIdName(c.id, c.name);

                    int[] dd = jdtocd(c.T);
                    c.Ty = dd[0];
                    c.Tm = dd[1];
                    c.Td = dd[2];
                    c.Th = (int)(((dd[4] + (dd[5] / 60.0) + (dd[6] / 3600.0)) / 24) * 10000);

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importDeepSpace8(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 2; i < lines.Count(); i += 2)
            {
                Comet c = new Comet();

                try
                {
                    string tempfull = lines[i];
                    string[] idname = tempfull.Split('(');
                    c.name = idname[0].Trim();
                    c.id = idname[1].TrimEnd(')');
                    c.full = Comet.setFullFromIdName(c.id, c.name);

                    string line = lines[i + 1];
                    string[] parts = line.Split(' ');

                    c.Ty = Convert.ToInt32(parts[2]);
                    c.Tm = Convert.ToInt32(parts[3]);
                    string[] dh = parts[4].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(parts[5]);
                    c.e = Convert.ToDouble(parts[6]);
                    c.w = Convert.ToDouble(parts[7]);
                    c.N = Convert.ToDouble(parts[8]);
                    c.i = Convert.ToDouble(parts[9]);
                    c.g = Convert.ToDouble(parts[10]);
                    c.k = Convert.ToDouble(parts[11]) / 2.5;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importPcTcs9(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = line.Split(' ');

                    string id = parts[0];

                    if (id.Contains('/'))
                    {
                        int p = 2;

                        while (!char.IsLetter(id[p])) p++;
                        string id1 = id.Substring(0, p);
                        string id2 = id.Substring(p, id.Length - p);

                        id = id1 + " " + id2;
                    }


                    c.q = Convert.ToDouble(parts[1]);
                    c.e = Convert.ToDouble(parts[2]);
                    c.i = Convert.ToDouble(parts[3]);
                    c.w = Convert.ToDouble(parts[4]);
                    c.N = Convert.ToDouble(parts[5]);

                    c.Ty = Convert.ToInt32(parts[6]);
                    c.Tm = Convert.ToInt32(parts[7]);
                    string[] dh = parts[8].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.g = Convert.ToDouble(parts[9]);
                    c.k = Convert.ToDouble(parts[10]) / 2.5;

                    c.name = parts[11].Trim();

                    c.full = Comet.setFullFromIdName(id, c.name);
                    c.id = id;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importEarthCenUniv10(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < lines.Count(); i += 2)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i];
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    string line = lines[i + 1];
                    string[] parts = line.Split(' ');

                    c.Ty = Convert.ToInt32(parts[3]);
                    c.Tm = Convert.ToInt32(parts[4]);
                    string[] dh = parts[5].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(parts[6]);
                    c.e = Convert.ToDouble(parts[7]);
                    c.w = Convert.ToDouble(parts[8]);
                    c.N = Convert.ToDouble(parts[9]);
                    c.i = Convert.ToDouble(parts[10]);
                    c.g = Convert.ToDouble(parts[11]);
                    c.k = Convert.ToDouble(parts[12]) / 2.5;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importDanceOfThePlanets11(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 5; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    string id = lines[i].Substring(0, 11).Trim();

                    if (id.Contains('/'))
                    {
                        int p = 2;

                        while (!char.IsLetter(id[p])) p++;
                        string id1 = id.Substring(0, p);
                        string id2 = id.Substring(p, id.Length - p);

                        id = id1 + " " + id2;
                    }

                    c.q = Convert.ToDouble(lines[i].Substring(11, 9).Trim());
                    c.e = Convert.ToDouble(lines[i].Substring(20, 9).Trim());
                    c.i = Convert.ToDouble(lines[i].Substring(29, 9).Trim());
                    c.N = Convert.ToDouble(lines[i].Substring(38, 9).Trim());
                    c.w = Convert.ToDouble(lines[i].Substring(47, 9).Trim());

                    c.Ty = Convert.ToInt32(lines[i].Substring(56, 4).Trim());
                    c.Tm = Convert.ToInt32(lines[i].Substring(61, 2).Trim());
                    c.Td = Convert.ToInt32(lines[i].Substring(61, 2).Trim());
                    c.Th = Convert.ToInt32(lines[i].Substring(65, 4).Trim().PadRight(4, '0'));

                    if (lines[i].Length == 69)
                        c.name = "";
                    else
                        c.name = lines[i].Substring(70, lines[i].Length - 70).Trim();

                    c.full = Comet.setFullFromIdName(id, c.name);
                    c.id = id;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importMegaStar12(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    c.name = line.Substring(0, 30).Trim();
                    c.id = line.Substring(30, 12).Trim();

                    c.full = Comet.setFullFromIdName(c.id, c.name);

                    c.Ty = Convert.ToInt32(line.Substring(42, 4).Trim());
                    c.Tm = Convert.ToInt32(line.Substring(47, 2).Trim());
                    c.Td = Convert.ToInt32(line.Substring(51, 2).Trim());
                    c.Th = Convert.ToInt32(line.Substring(54, 4).Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(line.Substring(59, 12).Trim());
                    c.e = Convert.ToDouble(line.Substring(73, 8).Trim());
                    c.w = Convert.ToDouble(line.Substring(85, 8).Trim());
                    c.N = Convert.ToDouble(line.Substring(97, 8).Trim());
                    c.i = Convert.ToDouble(line.Substring(109, 8).Trim());
                    c.g = Convert.ToDouble(line.Substring(119, 6).Trim());
                    c.k = Convert.ToDouble(line.Substring(126, 6).Trim());

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importSkyChart13(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = line.Split('\t');

                    c.q = Math.Abs(Convert.ToDouble(parts[2]));
                    c.e = Convert.ToDouble(parts[3]);
                    c.i = Convert.ToDouble(parts[4]);
                    c.w = Convert.ToDouble(parts[5]);
                    c.N = Convert.ToDouble(parts[6]);

                    string[] date = parts[8].Split('/');
                    c.Ty = Convert.ToInt32(date[0]);
                    c.Tm = Convert.ToInt32(date[1]);
                    string[] dh = date[2].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    string[] gk = parts[9].Split(' ');
                    c.g = Convert.ToDouble(gk[0]);
                    c.k = Convert.ToDouble(gk[1]);

                    c.full = parts[12].Split(';')[0];
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importVoyager14(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 23; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    c.name = lines[i].Substring(0, 27).Trim();
                    c.id = "";
                    c.full = c.name;

                    c.q = Convert.ToDouble(lines[i].Substring(27, 9).Trim());
                    c.e = Convert.ToDouble(lines[i].Substring(39, 8).Trim());
                    c.i = Convert.ToDouble(lines[i].Substring(49, 8).Trim());
                    c.N = Convert.ToDouble(lines[i].Substring(60, 8).Trim());
                    c.w = Convert.ToDouble(lines[i].Substring(71, 8).Trim());

                    c.Ty = Convert.ToInt32(lines[i].Substring(87, 4).Trim());
                    string mon = lines[i].Substring(91, 3);
                    if (mon == "Jan") c.Tm = 1;
                    if (mon == "Feb") c.Tm = 2;
                    if (mon == "Mar") c.Tm = 3;
                    if (mon == "Apr") c.Tm = 4;
                    if (mon == "May") c.Tm = 5;
                    if (mon == "Jun") c.Tm = 6;
                    if (mon == "Jul") c.Tm = 7;
                    if (mon == "Aug") c.Tm = 8;
                    if (mon == "Sep") c.Tm = 9;
                    if (mon == "Oct") c.Tm = 10;
                    if (mon == "Nov") c.Tm = 11;
                    if (mon == "Dec") c.Tm = 12;

                    string[] dh = lines[i].Substring(94, 7).Trim().Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importSkyTools15(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string tempfull = "", id = "", name = "";

                    tempfull = line.Substring(2, 41).Trim();

                    if ((tempfull[0] == 'C' || tempfull[0] == 'P' || tempfull[0] == 'D' || tempfull[0] == 'X') && tempfull[1] == '/')
                    {
                        int spaces = tempfull.Count(f => f == ' ');

                        if (spaces == 1)
                        {
                            id = tempfull;
                        }
                        else //if (spaces >= 2)
                        {
                            int secondspace = GetNthIndex(tempfull, ' ', 2);
                            id = tempfull.Substring(0, secondspace);
                            name = tempfull.Substring(secondspace + 1, tempfull.Length - secondspace - 1);
                        }

                        c.full = Comet.setFullFromIdName(id, name);
                        c.id = id;
                        c.name = name;
                    }
                    else
                    {
                        c.full = tempfull;
                        string[] idn = Comet.setIdNameFromFull(c.full);
                        c.id = idn[0];
                        c.name = idn[1];
                    }

                    c.Ty = Convert.ToInt32(line.Substring(54, 4).Trim());
                    c.Tm = Convert.ToInt32(line.Substring(59, 2).Trim());
                    c.Td = Convert.ToInt32(line.Substring(61, 2).Trim());
                    c.Th = Convert.ToInt32(line.Substring(65, 4).Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(line.Substring(70, 9).Trim());
                    c.e = Convert.ToDouble(line.Substring(82, 8).Trim());
                    c.w = Convert.ToDouble(line.Substring(91, 8).Trim());
                    c.N = Convert.ToDouble(line.Substring(99, 8).Trim());
                    c.i = Convert.ToDouble(line.Substring(107, 7).Trim());

                    c.g = Convert.ToDouble(line.Substring(115, 5).Trim());
                    c.k = Convert.ToDouble(line.Substring(122, 4).Trim());

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importCometForWindows(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 6; i < lines.Count(); i += 13)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i].Split('=')[1];
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    string[] date = lines[i + 3].Split('=')[1].Split(' ');
                    c.Ty = Convert.ToInt32(date[0]);
                    c.Tm = Convert.ToInt32(date[1]);
                    string[] dh = date[2].Split('.');
                    c.Td = Convert.ToInt32(dh[0]);
                    c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                    c.q = Convert.ToDouble(lines[i + 4].Split('=')[1]);
                    c.e = Convert.ToDouble(lines[i + 5].Split('=')[1]);
                    c.w = Convert.ToDouble(lines[i + 6].Split('=')[1]);
                    c.N = Convert.ToDouble(lines[i + 7].Split('=')[1]);
                    c.i = Convert.ToDouble(lines[i + 8].Split('=')[1]);

                    string[] gk = lines[i + 11].Split('=')[1].Split(' ');
                    c.g = Convert.ToDouble(gk[0]);
                    c.k = Convert.ToDouble(gk[1]) / 2.5;

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        void importNasaComet(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 2; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i].Substring(0, 43).Trim();
                    string[] idn = Comet.setIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    /////////////////////////
                    //pogledat epoch
                    //mozda treba njega ucitat pa precesirat vrijednosti W N i na epoch 2000

                    //int epoch = Convert.ToInt32(lines[i].Substring(44, 7);
                    /////////////////////////

                    c.q = Convert.ToDouble(lines[i].Substring(52, 11).Trim());
                    c.e = Convert.ToDouble(lines[i].Substring(64, 10).Trim());
                    c.i = Convert.ToDouble(lines[i].Substring(75, 9).Trim());
                    c.w = Convert.ToDouble(lines[i].Substring(85, 9).Trim());
                    c.N = Convert.ToDouble(lines[i].Substring(95, 9).Trim());

                    c.Ty = Convert.ToInt32(lines[i].Substring(105, 4).Trim());
                    c.Tm = Convert.ToInt32(lines[i].Substring(109, 2).Trim());
                    c.Td = Convert.ToInt32(lines[i].Substring(111, 2).Trim());
                    c.Th = Convert.ToInt32(Convert.ToDouble(lines[i].Substring(114, 5).Trim().PadRight(5, '0')) / 10.0);

                    c.T = jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.getPeriod_P(c.q, c.e);
                    c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                    c.n = Comet.getMeanMotion_n(c.e, c.P);
                    c.Q = Comet.getAphelionDistance_Q(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                }
                catch
                {
                    continue;
                }

                masterList.Add(c);
            }
        }

        #endregion

        #region Sort

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this.tabPage1, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
        }

        private void menuItemSort_Click(object sender, EventArgs e)
        {
            //ako kliknem na item koji je vec odabran
            if ((sender as MenuItem).Checked) return;

            //da zapamti kako su zadnja 2 odabrana
            bool order = menuItemAsc.Checked;

            //prvo sve odznačiti
            foreach (MenuItem item in contextSort.MenuItems) (item as MenuItem).Checked = false;

            //pa označiti onog koji je kliknut
            (sender as MenuItem).Checked = true;

            menuItemAsc.Checked = order;
            menuItemDesc.Checked = !order;

            sortList(userList);
        }

        private void menuItemAsc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = true;
            menuItemDesc.Checked = false;
            sortList(userList);
        }

        private void menuItemDesc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = false;
            menuItemDesc.Checked = true;
            sortList(userList);
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

            if (menuItemDesig.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.sortkey).ToList();

            else if (menuItemDesig.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.sortkey).ToList();

            else if (menuItemName.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.name).ToList();

            else if (menuItemName.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.name).ToList();

            else if (menuItemPerihDate.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.T).ToList();

            else if (menuItemPerihDate.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.T).ToList();

            else if (menuItemPerihDist.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.q).ToList();

            else if (menuItemPerihDist.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.q).ToList();

            else if (menuItemIncl.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.i).ToList();

            else if (menuItemIncl.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.i).ToList();

            else if (menuItemEcc.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.e).ToList();

            else if (menuItemEcc.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.e).ToList();

            else if (menuItemAscNode.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.N).ToList();

            else if (menuItemAscNode.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.N).ToList();

            else if (menuItemArgPeri.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.w).ToList();

            else if (menuItemArgPeri.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.w).ToList();

            else if (menuItemPeriod.Checked && menuItemAsc.Checked)
                userList = tempList.OrderBy(Comet => Comet.P).ToList();

            else if (menuItemPeriod.Checked && menuItemDesc.Checked)
                userList = tempList.OrderByDescending(Comet => Comet.P).ToList();

            updateCometListbox(userList);
        }

        #endregion

        #region Filters

        public class Filter
        {
            public bool flag;
            public int index;
            public string text;
            public double value;

            public Filter()
            {
                flag = false;
                index = -1;
                text = "";
                value = 0.0;
            }
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            gbFilters.Visible = !gbFilters.Visible;
            gbDetails.Visible = !gbDetails.Visible;
        }

        private void checkboxFilters_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in (sender as CheckBox).Parent.Controls)
            {
                c.Enabled = (sender as CheckBox).Checked;
            }

            (sender as CheckBox).Enabled = true;
        }

        private void btnPerihDateNow_Click(object sender, EventArgs e)
        {
            tbPerihDateD.Text = DateTime.Now.Day.ToString("00");
            tbPerihDateM.Text = DateTime.Now.Month.ToString("00");
            tbPerihDateY.Text = DateTime.Now.Year.ToString();
        }

        private void textBoxFilters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        
        private void btnCancelFilters_Click(object sender, EventArgs e)
        {
            setFiltersToForm(filters);

            gbFilters.Visible = !gbFilters.Visible;
            gbDetails.Visible = !gbDetails.Visible;
        }
        
        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            if (getFiltersFromForm() == false) return;

            copyListUseFilters();

            gbFilters.Visible = !gbFilters.Visible;
            gbDetails.Visible = !gbDetails.Visible;
        }

        public void copyListUseFilters()
        {
            if (filtersAppliedFlag || masterList.Count == 0) return;

            userList.Clear();

            foreach (Comet c in masterList)
            {
                if (filters[0].flag && filters[0].index == 0 && !c.full.ToUpper().Contains(filters[0].text.ToUpper())) continue;
                if (filters[0].flag && filters[0].index == 1 && c.full.ToUpper().Contains(filters[0].text.ToUpper())) continue;
                if (filters[1].flag && filters[1].index == 0 && c.T < filters[1].value) continue;
                if (filters[1].flag && filters[1].index == 1 && c.T > filters[1].value) continue;
                if (filters[2].flag && filters[2].index == 0 && c.q < filters[2].value) continue;
                if (filters[2].flag && filters[2].index == 1 && c.q > filters[2].value) continue;
                if (filters[3].flag && filters[3].index == 0 && c.e < filters[3].value) continue;
                if (filters[3].flag && filters[3].index == 1 && c.e > filters[3].value) continue;
                if (filters[4].flag && filters[4].index == 0 && c.N < filters[4].value) continue;
                if (filters[4].flag && filters[4].index == 1 && c.N > filters[4].value) continue;
                if (filters[5].flag && filters[5].index == 0 && c.w < filters[5].value) continue;
                if (filters[5].flag && filters[5].index == 1 && c.w > filters[5].value) continue;
                if (filters[6].flag && filters[6].index == 0 && c.i < filters[6].value) continue;
                if (filters[6].flag && filters[6].index == 1 && c.i > filters[6].value) continue;
                if (filters[7].flag && filters[7].index == 0 && c.P < filters[7].value) continue;
                if (filters[7].flag && filters[7].index == 1 && c.P > filters[7].value) continue;

                userList.Add(c);
            }

            sortList(userList);
            filtersAppliedFlag = true;
        }

        #endregion

        #region FilterPresets

        private void btnPresets_Click(object sender, EventArgs e)
        {
            contextPresets.Show(this.gbFilters, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
        }

        private void contextSavePresetMenuItem_Click(object sender, EventArgs e)
        {
            if (getFiltersFromForm() == false) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Comet preset file (*.cpf)|*.cpf";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);

                foreach (Filter f in filters)
                {
                    sw.WriteLine(f.flag + ";" + f.index + ";" + f.text + ";" + f.value);
                    //sw.Write(f.flag + ";" + f.index + ";" + f.text + ";" + f.value+ '|');
                }

                sw.Close();
                setFiltersToForm(filters);
                //filtersApplied = false;
                copyListUseFilters();

                gbFilters.Visible = !gbFilters.Visible;
                gbDetails.Visible = !gbDetails.Visible;
            }
        }

        private void contextLoadPresetMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Comet preset file (*.cpf)|*.cpf";

            Filter[] tempFilters = new Filter[8];
            for (int i = 0; i < tempFilters.Count(); i++)
            {
                tempFilters[i] = new Filter();
            }

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(ofd.FileName);

                    for (int i = 0; i < lines.Count(); i++)
                    {
                        string[] parts = lines[i].Split(';');

                        tempFilters[i].flag = Convert.ToBoolean(parts[0]);
                        tempFilters[i].index = Convert.ToInt32(parts[1]);
                        tempFilters[i].text = parts[2];
                        tempFilters[i].value = Convert.ToDouble(parts[3]);
                    }

                    setFiltersToForm(tempFilters);
                    //filters = tempFilters.ToArray();
                    //filtersApplied = false;
                    //copyListUseFilters();
                }
                catch
                {
                    setFiltersToForm(filters);
                    MessageBox.Show("Invalid preset file.                                    ", "Load preset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //gbFilters.Visible = !gbFilters.Visible;
                //gbDetails.Visible = !gbDetails.Visible;
            }
        }

        public bool getFiltersFromForm()
        {
            if (chName.Checked && comboName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Contains or Does not contain.               ", "Filters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((chPerihDate.Checked && comboPerihDate.SelectedIndex == -1) ||
                (chPerihDist.Checked && comboPerihDist.SelectedIndex == -1) ||
                (chEcc.Checked && comboEcc.SelectedIndex == -1) ||
                (chAscNode.Checked && comboAscNode.SelectedIndex == -1) ||
                (chArgPeric.Checked && comboArgPeric.SelectedIndex == -1) ||
                (chPeriod.Checked && comboPeriod.SelectedIndex == -1))
            {
                MessageBox.Show("Please select Greather than (>) or Less than (<).               ", "Filters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (chName.Checked && tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter name.               ", "Filters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((chPerihDate.Checked && tbPerihDateD.Text.Trim().Length == 0) ||
                (chPerihDate.Checked && tbPerihDateM.Text.Trim().Length == 0) ||
                (chPerihDate.Checked && tbPerihDateY.Text.Trim().Length == 0) ||
                (chPerihDist.Checked && tbPerihDist.Text.Trim().Length == 0) ||
                (chEcc.Checked && tbEcc.Text.Trim().Length == 0) ||
                (chAscNode.Checked && tbAscNode.Text.Trim().Length == 0) ||
                (chArgPeric.Checked && tbArgPeric.Text.Trim().Length == 0) ||
                (chPeriod.Checked && tbPeriod.Text.Trim().Length == 0))
            {
                MessageBox.Show("Please enter value.               ", "Filters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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
                    MessageBox.Show("Invalid date.                             ", "Filters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            Filter[] fs = new Filter[8];
            for (int i = 0; i < fs.Count(); i++)
            {
                fs[i] = new Filter();
            }

            fs[0].flag = chName.Checked;
            fs[0].index = comboName.SelectedIndex;
            fs[0].text = tbName.Text.Trim();

            fs[1].flag = chPerihDate.Checked;
            fs[1].index = comboPerihDate.SelectedIndex;
            fs[1].text = tbPerihDateD.Text.Trim() + "." + tbPerihDateM.Text.Trim() + "." + tbPerihDateY.Text.Trim();
            if (fs[1].flag) fs[1].value = jd0(Convert.ToInt32(tbPerihDateY.Text.Trim()), Convert.ToInt32(tbPerihDateM.Text.Trim()), Convert.ToInt32(tbPerihDateD.Text.Trim()), 0);

            fs[2].flag = chPerihDist.Checked;
            fs[2].index = comboPerihDist.SelectedIndex;
            fs[2].text = tbPerihDist.Text.Trim();
            if (fs[2].flag) fs[2].value = Convert.ToDouble(fs[2].text);

            fs[3].flag = chEcc.Checked;
            fs[3].index = comboEcc.SelectedIndex;
            fs[3].text = tbEcc.Text.Trim();
            if (fs[3].flag) fs[3].value = Convert.ToDouble(fs[3].text);

            fs[4].flag = chAscNode.Checked;
            fs[4].index = comboAscNode.SelectedIndex;
            fs[4].text = tbAscNode.Text.Trim();
            if (fs[4].flag) fs[4].value = Convert.ToDouble(fs[4].text);

            fs[5].flag = chArgPeric.Checked;
            fs[5].index = comboArgPeric.SelectedIndex;
            fs[5].text = tbArgPeric.Text.Trim();
            if (fs[5].flag) fs[5].value = Convert.ToDouble(fs[5].text);

            fs[6].flag = chIncl.Checked;
            fs[6].index = comboIncl.SelectedIndex;
            fs[6].text = tbIncl.Text.Trim();
            if (fs[6].flag) fs[6].value = Convert.ToDouble(fs[6].text);

            fs[7].flag = chPeriod.Checked;
            fs[7].index = comboPeriod.SelectedIndex;
            fs[7].text = tbPeriod.Text.Trim();
            if (fs[7].flag) fs[7].value = Convert.ToDouble(fs[7].text);

            filtersAppliedFlag = false;
            filters = fs.ToArray();

            return true;
        }

        public void setFiltersToForm(Filter[] fs)
        {
            chName.Checked = fs[0].flag;
            comboName.SelectedIndex = fs[0].index;
            tbName.Text = fs[0].text;

            chPerihDate.Checked = fs[1].flag;
            comboPerihDate.SelectedIndex = fs[1].index;
            if (fs[1].text != "")
            {
                string[] date = fs[1].text.Split('.');
                tbPerihDateD.Text = date[0];
                tbPerihDateM.Text = date[1];
                tbPerihDateY.Text = date[2];
            }
            else
            {
                tbPerihDateD.Text = "";
                tbPerihDateM.Text = "";
                tbPerihDateY.Text = "";
            }

            chPerihDist.Checked = fs[2].flag;
            comboPerihDist.SelectedIndex = fs[2].index;
            tbPerihDist.Text = fs[2].text;

            chEcc.Checked = fs[3].flag;
            comboEcc.SelectedIndex = fs[3].index;
            tbEcc.Text = fs[3].text;

            chAscNode.Checked = fs[4].flag;
            comboAscNode.SelectedIndex = fs[4].index;
            tbAscNode.Text = fs[4].text;

            chArgPeric.Checked = fs[5].flag;
            comboArgPeric.SelectedIndex = fs[5].index;
            tbArgPeric.Text = fs[5].text;

            chIncl.Checked = fs[6].flag;
            comboIncl.SelectedIndex = fs[6].index;
            tbIncl.Text = fs[6].text;

            chPeriod.Checked = fs[7].flag;
            comboPeriod.SelectedIndex = fs[7].index;
            tbPeriod.Text = fs[7].text;
        }

        #endregion

        #region Export

        private void btnExport_Click(object sender, EventArgs e)
        {
            contextExport.Show(this.tabPage1, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
        }

        private void contextExportMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file (*.txt)|*.txt|" +
                "SSC file (*.ssc)|*.ssc|" +
                "DAT file (*.dat)|*.dat|" +
                "COMET file (*.COMET)|*.COMET|" +
                "All files (*.*)|*.*";

            int exportType = -1;
            if ((sender as MenuItem).Text == "Celestia") exportType = 17;
            //if ((sender as MenuItem).Text == "Stellarium") exportType = 18;
            //...
            //...

            if (exportType == 17) sfd.FilterIndex = 2;


            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exportMain(sfd.FileName, exportType);
            }
        }

        void exportMain(string filename, int exportType)
        {
            writePretext(filename, exportType);

            if (exportType == 17) exportCelestia(filename);

            System.Diagnostics.Process.Start(filename);
        }

        void writePretext(string filename, int exportType)
        {
            StreamWriter sw = new StreamWriter(filename);

            if (exportType == 4)
            {
                sw.Write("Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period\n");
            }

            if (exportType == 5)
            {
                sw.Write("RDPC\t" + userList.Count + Environment.NewLine);
            }

            if (exportType == 7)
            {
                sw.Write("NOTE: If viewing this file and it appears confused, make the window very wide!\n\n");
                sw.Write("   The numbers are all in the proper format for easy use in Starry Night's\n");
                sw.Write("orbit editor. Just click on the word Sun in the planet floater and then\n");
                sw.Write("click on add. In the first window that appears select the comet as the type\n");
                sw.Write("of object you want to add. Please see the manual for more information.\n\n");
                sw.Write("   The orbital information should have the reference plane set at Ecliptic\n");
                sw.Write(" 2000 and the Style should be pericentric. Don't forget to use copy and\n");
                sw.Write(" paste to ease the input of the orbital data into Starry Night.\n\n");
                sw.Write("This file kindly prepared by the IAU Minor Planet Center & Central Bureau for Astronomical Telegrams.\n\n");
                sw.Write("Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         Reference\n\n");
            }

            if (exportType == 8)
            {
                sw.Write("Type C: Equinox Year Month Day q e Peri Node i Mag k\n");
                sw.Write("Type A: Equinox Year Month Day a M e Peri Node i H G\n");
            }

            if (exportType == 11)
            {
                sw.Write("Comet      peri(au)   e         iř       ęř       wř     peridate     name\n");
                sw.Write("(In order to be recognised by Dance of the Planets, this file)\n");
                sw.Write("(must have a .cmt extension.)\n");
                sw.Write("(File prepared by IAU Minor Planet Center/Central Bureau)\n");
                sw.Write("(for Astronomical Telegrams.)\n");

            }

            if (exportType == 14)
            {
                sw.Write("NOTE TO VOYAGER II USERS:\n\n");
                sw.Write("   The following table will link the symbols below with the names used in\n");
                sw.Write("the Voyager II \"Define New Orbit...\" dialog for comets.\n\n");
                sw.Write("     q        perihelion distance (astronomical units)\n");
                sw.Write("     e        eccentricity (no units)\n");
                sw.Write("     i        inclination of orbit to ecliptic (degrees)\n");
                sw.Write("     Node     longitude of ascending node (degrees)\n");
                sw.Write("     w        argument of perihelion (degrees)\n");
                sw.Write("     L        mean anomaly (this is 0 at perihelion) (degrees)\n");
                sw.Write("     Date     epoch of orbit\n");
                sw.Write("     Equinox  reference equinox (usually 2000.0)\n\n");
                sw.Write("Save this page as plain text from your browser and use the table to input\n");
                sw.Write("the orbital elements for the comets that you would like to plot and\n");
                sw.Write("follow.  If you have any question, consult your software manual or the\n");
                sw.Write("Carina web site: <a href=\"http://www.carinasoft.com\">http://www.carinasoft.com</a>\n\n");
                sw.Write("Thanks to the IAU Minor Planet Center & Central Bureau for Astronomical\n");
                sw.Write("Telegrams for providing this information.\n\n");
                sw.Write("Name                            q          e         i        Node         w       L      T(Date)    Equinox\n");
            }

            sw.Close();
        }

        void exportCelestia(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);

            foreach (Comet c in userList)
            {
                string mon = "";
                if (c.Tm == 1) mon = "Jan";
                if (c.Tm == 2) mon = "Feb";
                if (c.Tm == 3) mon = "Mar";
                if (c.Tm == 4) mon = "Apr";
                if (c.Tm == 5) mon = "May";
                if (c.Tm == 6) mon = "Jun";
                if (c.Tm == 7) mon = "Jul";
                if (c.Tm == 8) mon = "Aug";
                if (c.Tm == 9) mon = "Sep";
                if (c.Tm == 10) mon = "Oct";
                if (c.Tm == 11) mon = "Nov";
                if (c.Tm == 12) mon = "Dec";

                sw.WriteLine("\"" + c.full.Replace('/', ' ') + "\" \"Sol\"");
                sw.WriteLine("{");
                sw.WriteLine("\tClass \"comet\"");
                sw.WriteLine("\tMesh \"asteroid.cms\"");
                sw.WriteLine("\tTexture \"asteroid.jpg\"");
                sw.WriteLine("\tRadius 5");
                sw.WriteLine("\tAlbedo 0.1");
                //sw.WriteLine("\t# Magnitude " + c.g.ToString("0.0") + " " + c.k.ToString("0.0"));
                sw.WriteLine("\tEllipticalOrbit");
                sw.WriteLine("\t{");
                sw.WriteLine("\t\tPeriod         " + c.P.ToString("0.000000").PadLeft(20, ' '));
                sw.WriteLine("\t\tPericenterDistance        " + c.q.ToString("0.000000").PadLeft(9, ' '));

                if (c.e < 1.0)
                {
                    sw.WriteLine("\t\tEccentricity               " + c.e.ToString("0.000000").PadLeft(8, ' '));
                }
                else
                {
                    sw.WriteLine("\t\tEccentricity               0.999999");
                    sw.WriteLine("\t\t#Eccentricity               " + c.e.ToString("0.000000").PadLeft(8, ' ') + "   # Real");
                }

                sw.WriteLine("\t\tInclination              " + c.i.ToString("0.0000").PadLeft(8, ' '));
                sw.WriteLine("\t\tAscendingNode            " + c.N.ToString("0.0000").PadLeft(8, ' '));
                sw.WriteLine("\t\tArgOfPericenter          " + c.w.ToString("0.0000").PadLeft(8, ' '));
                sw.WriteLine("\t\tMeanAnomaly                0.0");
                sw.WriteLine("\t\tEpoch                " + c.T.ToString("0.0000").PadLeft(12, ' ')
                    + "     # " + c.Ty.ToString() + " " + mon + " " + c.Td.ToString("00") + "." + c.Th.ToString("0000"));
                sw.WriteLine("\t}");
                sw.WriteLine("}");
                sw.WriteLine("");
            }

            sw.Close();
        }

        #endregion

        #region Other

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                btnFilters.Text = "Filters ▼";
                gbDetails.Visible = true;
                gbFilters.Visible = false;
            }
            if (tabControl1.SelectedIndex == 1 && finishedImportFlag == true)
            {
                tbImportFilename.Text = "";
                labelImpFormat.Text = "(no file selected)";
                labelDetectedComets.Text = "-";

                finishedImportFlag = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox1.Checked;
        }

        private void btnSaveLocation_Click(object sender, EventArgs e)
        {
            double latdeg = Convert.ToDouble(tbLatDeg.Text);
            double latmin = Convert.ToDouble(tbLatMin.Text);
            double latsec = Convert.ToDouble(tbLatSec.Text);

            double londeg = Convert.ToDouble(tbLonDeg.Text);
            double lonmin = Convert.ToDouble(tbLonMin.Text);
            double lonsec = Convert.ToDouble(tbLonSec.Text);

            double lat = latdeg + latmin / 60 + latsec / 3600;
            if (comboLat.SelectedIndex == 1) lat = -lat;

            double lon = londeg + lonmin / 60 + lonsec / 3600;
            if (comboLon.SelectedIndex == 1) lon = -lon;

            bool dst = checkBoxDST.Checked;

            double timezone;
            timezone = Convert.ToDouble(tbTimezone.Text.Substring(1, 2));
            timezone += Convert.ToDouble(tbTimezone.Text.Substring(4, 2)) / 60;
            if (tbTimezone.Text[0] == '-') timezone = -timezone;

            obs = new Observer(lat, lon, timezone * 60, dst);
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
            if (obs == null) btnSaveLocation_Click(sender, e);

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
            DateTime UTStartTime = localStartTime.AddMinutes(-obs.tz);
            DateTime UTStopTIme = localStopTime.AddMinutes(-obs.tz);

            if (obs.dst)
            {
                UTStartTime = UTStartTime.AddHours(-1);
                UTStopTIme = UTStopTIme.AddHours(-1);
            }

            double jday = jd(UTStartTime.Year, UTStartTime.Month, UTStartTime.Day, UTStartTime.Hour, UTStartTime.Minute, UTStartTime.Second);
            double jdmax = jd(UTStopTIme.Year, UTStopTIme.Month, UTStopTIme.Day, UTStopTIme.Hour, UTStopTIme.Minute, UTStopTIme.Second);
            double locjday = jd(localStartTime.Year, localStartTime.Month, localStartTime.Day, localStartTime.Hour, localStartTime.Minute, localStartTime.Second);

            double interval = Convert.ToDouble(tbIntervalDay.Text) +
                            (Convert.ToDouble(tbIntervalHour.Text) + (Convert.ToDouble(tbIntervalMin.Text) / 60.0)) / 24;

            btnSettingsEphem.Text = "Settings ▼";
            panelEphemSettings.Height = 29;
            richEphem.Clear();

            if (radioLocalTime.Checked) richEphem.Text += "     Local Time  ";
            else richEphem.Text += " Universal Time  ";
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

                line += radioLocalTime.Checked ? dateString(locjday) : dateString(jday);
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
            //MessageBox.Show((end - begin).TotalMilliseconds.ToString());
        }

        double[] CometAlt(Comet c, double jday, Observer obs)
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
            double mag = c.g + 5 * log10(dist) + 2.5 * c.k * log10(r);
            return new double[] { altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag };
        }

        double[] SunAlt(double jday, Observer obs)
        {
            // return alt, az, time angle, ra, dec, ecl. long. and lat=0, illum=1, 0, dist, brightness 
            double[] sdat = sunxyz(jday);
            double ecl = 23.439291111 - 3.563E-7 * (jday - 2451543.5);
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
            double w = 282.9404 + 4.70935E-5 * d;
            double e = 0.016709 - 1.151E-9 * d;
            double M = rev(356.0470 + 0.9856002585 * d);
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
            double N = cmt.N + 3.82394E-5 * d;
            double ww = cmt.w;	// why not precess this value?
            double i = cmt.i;
            double xh = r * (cosd(N) * cosd(v + ww) - sind(N) * sind(v + ww) * cosd(i));
            double yh = r * (sind(N) * cosd(v + ww) + cosd(N) * sind(v + ww) * cosd(i));
            double zh = r * (sind(v + ww) * sind(i));
            double lonecl = atan2d(yh, xh);
            double latecl = atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
            return new double[] { xh, yh, zh, r, lonecl, latecl };
        }

        double[] radecr(double[] obj, double[] sun, double jday, Observer obs)
        {
            // radecr returns ra, dec and earth distance
            // obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
            // (note Sun coords are really Earth heliocentric coordinates with reverse signs)
            // Equatorial geocentric co-ordinates
            double xg = obj[0] + sun[0];
            double yg = obj[1] + sun[1];
            double zg = obj[2];
            // Obliquity of Ecliptic
            double obl = 23.439291111 - 3.563E-7 * (jday - 2451543.5);
            // Convert to eq. co-ordinates
            double x1 = xg;
            double y1 = yg * cosd(obl) - zg * sind(obl);
            double z1 = yg * sind(obl) + zg * cosd(obl);
            // RA and dec (33.2)
            double ra = rev(atan2d(y1, x1));
            double dec = atan2d(z1, Math.Sqrt(x1 * x1 + y1 * y1));
            double dist = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            return new double[] { ra, dec, dist };
        }

        double[] radec2aa(double ra, double dec, double jday, Observer obs)
        {
            // Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
            // TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
            double TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
            double H = rev(TH0 + obs.longitude - ra);
            double alt = asind(sind(obs.latitude) * sind(dec) + cosd(obs.latitude) * cosd(dec) * cosd(H));
            double az = atan2d(sind(H), (cosd(H) * sind(obs.latitude) - tand(dec) * cosd(obs.latitude)));
            return new double[] { alt, rev(az + 180.0), H };
        }

        double[] separation(double ra1, double ra2, double dec1, double dec2)
        {
            // ra, dec may also be long, lat, but PA is relative to the chosen coordinate system
            double d = acosd(sind(dec1) * sind(dec2) + cosd(dec1) * cosd(dec2) * cosd(ra1 - ra2));		// (Meeus 17.1)
            if (d < 0.1) d = Math.Sqrt(sqr(rev2(ra1 - ra2) * cosd((dec1 + dec2) / 2)) + sqr(dec1 - dec2));	// (17.2)
            double pa = atan2d(sind(ra1 - ra2), cosd(dec2) * tand(dec1) - sind(dec2) * cosd(ra1 - ra2));		// angle
            return new double[] { d, rev(pa) };
        }

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
        }

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
        }

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
            return anglestr;
        }

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
        }

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

        public int GetNthIndex(string s, char t, int n)
        {
            //http://stackoverflow.com/questions/2571716/find-nth-occurrence-of-a-character-in-a-string

            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
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

        #endregion
    }
}