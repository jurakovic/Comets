using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Comet_OEW
{
    public partial class Form1 : Form
    {
        public static string downloadsDir;
        public static string localDataDir;
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
            downloadsDir += @"\Comet OEW\";
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

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
                filename = fd.FileName;
                int type = fd.FilterIndex;

                importMain(type, filename);
            }
        }

        private void downloadFromInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            DownloadForm df = new DownloadForm();
            df.ShowDialog();

            if (fileIsDownloaded)
            {
                importMain(0, filename);
            }

            this.Enabled = true;
        }

        public void importMain(int importType, string filename)
        {
            masterList.Clear();
            Comet.total = 0;
            Comet.total2 = 0;

            importMpc(filename);
            sortList();
        }

        public void importMpc(string filename)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                //string str = line.Replace('.', ',');
                string str = line;

                Comet c = new Comet();

                try
                {
                    c.y = Convert.ToInt32(str.Substring(14, 4).Trim());
                    c.m = Convert.ToInt32(str.Substring(19, 2).Trim());
                    c.d = Convert.ToInt32(str.Substring(22, 2).Trim());
                    c.h = Convert.ToInt32(str.Substring(25, 4).Trim());
                    c.q = Convert.ToDouble(str.Substring(31, 8).Trim());
                    c.e = Convert.ToDouble(str.Substring(41, 8).Trim());
                    c.w = Convert.ToDouble(str.Substring(51, 8).Trim());
                    c.om = Convert.ToDouble(str.Substring(61, 8).Trim());
                    c.i = Convert.ToDouble(str.Substring(71, 8).Trim());
                    c.g = Convert.ToDouble(str.Substring(91, 4).Trim());
                    c.k = Convert.ToDouble(str.Substring(96, 4).Trim());
                    c.full = str.Substring(102, 55).Trim();
                }
                catch
                {
                    continue;
                }

                c.k *= 2.5;

                string[] idn = Comet.setIdNameFull(c.full);
                c.id = idn[0];
                c.name = idn[1];
                c.full = idn[2];

                int y = DateTime.Now.Year;
                int m = DateTime.Now.Month;
                int d = DateTime.Now.Day;
                int h = (int)(((double)DateTime.Now.Hour / 24) * 10000);
                double today = Comet.GregToJul(y, m, d, h);

                c.T = Comet.GregToJul(c.y, c.m, c.d, c.h);
                c.P = Comet.getPeriod_P(c.q, c.e);
                c.a = Comet.getSemimajorAxis_a(c.q, c.e);
                c.n = Comet.getMeanMotion_n(c.e, c.P);
                c.M = Comet.getMeanAnomaly_M(c.T, today, c.e, c.n, c.q);
                c.E = Comet.getEccentricAnomaly_E(c.e, c.M);
                c.v = Comet.getTrueAnomaly_v(c.e, c.E, c.q, c.T, today);
                c.L = Comet.getMeanLongitude_L(c.M, c.om, c.w);
                c.Q = Comet.getAphelionDistance_Q(c.e, c.a);
                c.bw = Comet.getLongitudeOfPericenter_bw(c.om, c.w);
                c.l = Comet.getTrueLongitude_l(c.v, c.bw);
                c.F = Comet.getEccentricLongitude_F(c.w, c.om, c.E);

                c.set_sortkey();

                masterList.Add(c);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this.Left + btnSort.Left + 9, this.Top + btnSort.Top + 54);
        }

        private void comboComet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = comboComet.SelectedIndex;

            Comet c = userList.ElementAt(ind);

            //tFull.Text = c.full;
            tId.Text = c.id;
            tName.Text = c.name;
            tT.Text = c.y.ToString() + "-" + c.m.ToString("00") + "-" + c.d.ToString("00") + "." + c.h.ToString("0000");
            tQ.Text = String.Format("{0:0.000000}", c.q);
            tE.Text = String.Format("{0:0.000000}", c.e);
            tI.Text = String.Format("{0:0.0000}", c.i);
            tAn.Text = String.Format("{0:0.0000}", c.om);
            tPn.Text = String.Format("{0:0.0000}", c.w);
  
            tG.Text = String.Format("{0:0.0}", c.g);
            tK.Text = String.Format("{0:0.0}", c.k);

            tSort.Text = String.Format("{0:0.0000000}", c.sortkey);

            tEquinox.Text = "2000.0";

            //if (c.e < 1.0)
            //{
                tP.Text = String.Format("{0:0.000000}", c.P);
                tA.Text = String.Format("{0:0.000000}", c.a);
                tN.Text = String.Format("{0:0.000000}", c.n);
                tM.Text = String.Format("{0:0.000000}", c.M);
                tEan.Text = String.Format("{0:0.000000}", c.E);
                tPhi.Text = String.Format("{0:0.000000}", c.v);
                tL.Text = String.Format("{0:0.000000}", c.L);
                tAph.Text = String.Format("{0:0.000000}", c.Q);
                tBw.Text = String.Format("{0:0.000000}", c.bw);
                tF.Text = String.Format("{0:0.000000}", c.F);
                tTl.Text = String.Format("{0:0.000000}", c.l);
            //}
            //else
            //{
            //    tP.Text = "";
            //    tA.Text = "";
            //    tN.Text = "";
            //    tM.Text = "";
            //    tEan.Text = "";
            //    tPhi.Text = "";
            //    tL.Text = "";
            //    tAph.Text = "";
            //}
        }

        public void sortList()
        {
            if (masterList.Count == 0)
            {
                toolStripStatusLabel1.Text = "Ready";
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
                userList = masterList.OrderBy(Comet => Comet.om).ToList();

            else if (longOfTheAscNodeToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.om).ToList();

            else if (eccentricityToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.e).ToList();

            else if (eccentricityToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.e).ToList();

            else if (inclinationToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.i).ToList();

            else if (inclinationToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.i).ToList();

            else if (periodToolStripMenuItem.Checked && ascendingToolStripMenuItem.Checked)
                userList = masterList.OrderBy(Comet => Comet.P).ToList();

            else if (periodToolStripMenuItem.Checked && descendingToolStripMenuItem.Checked)
                userList = masterList.OrderByDescending(Comet => Comet.P).ToList();

            updateComboComet();
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

        private void updateComboComet()
        {
            comboComet.Items.Clear();
            foreach (Comet c in userList)
            {
                comboComet.Items.Add(c.full);
            }
            comboComet.SelectedIndex = 0;
            toolStripStatusLabel1.Text = "Comets: " + userList.Count;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //foreach (Control c in gbDetails.Controls)
            //{
            //    if (c is TextBox) (c as TextBox).ReadOnly = false;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //foreach (Control c in gbDetails.Controls)
            //{
            //    if (c is TextBox) (c as TextBox).ReadOnly = true;
            //}
        }
    }
}
