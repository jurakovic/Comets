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

namespace Cometary_Workshop
{
    public partial class Form1 : Form
    {
        const double DEG2RAD = Math.PI / 180.0;
        const double RAD2DEG = 180.0 / Math.PI;

        public static string downloadsDir;
        public static string localDataDir;
        public static string filename;
        public static bool fileIsDownloaded = false;

        public static List<Comet> masterList = new List<Comet>();
        public static List<Comet> userList = new List<Comet>();
        public static string lastSortItem = "noSortToolStripMenuItem";

        public static Location obs;// = new Location(45, 16);
        
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

            if (c.e == 1.0)
            {
                tAph.Text = "";
                tA.Text = "";
                tN.Text = "0";
                tM.Text = "0";
                tEan.Text = "0";
            }
            if (c.P > 10000)
            {
                tP.Text = "";
            }
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

            //tabControl1.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                String appdir = Path.GetDirectoryName(Application.ExecutablePath);
                String myfile = Path.Combine(appdir, "halley.html");
                webBrowser1.Url = new Uri("file:///" + myfile);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormEphemSettings fes = new FormEphemSettings();
            fes.ShowDialog();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            int ind = comboComet.SelectedIndex;
            if (ind == -1) return;
                       
            Comet cmt = userList[ind];

            double start = obs.startJD;
            double stop = obs.stopJD;

            while (start < stop)
            {
                double[] altaz = CometAlt(cmt, start, obs);
                int[] date = jdtocd(start);
                textBox1.Text +=
                    date[2] + "-" + date[1] + "-" + date[0] + "   " +
                    date[3] + ":" + date[4] + "   " +
                    " ra: " + hmsstring(altaz[3] / 15.0) +
                    " dec: " + anglestring(altaz[4], true, false) +
                    " alt: " + fixnum(altaz[0], 7, 1) +
                    " az:" + fixnum(altaz[1], 8, 1) + Environment.NewLine;

                start += obs.interval;
            }
        }

        public double[] CometAlt(Comet cmt, double jday, Location obs)
        {
            // Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
            double[] sun_xyz = sunxyz(jday);
            double[] planet_xyz = comet_xyz(cmt, jday);
            double dx = planet_xyz[0] + sun_xyz[0];
            double dy = planet_xyz[1] + sun_xyz[1];
            double dz = planet_xyz[2] + sun_xyz[2];
            double lon = rev(atan2d(dy, dx));
            double lat = atan2d(dz, Math.Sqrt(dx * dx + dy * dy));
            double[] radec = radecr(planet_xyz, sun_xyz, jday, obs);
            double ra = radec[0];
            double dec = radec[1];
            double[] altaz = radec2aa(ra, dec, jday, obs);
            double dist = radec[2];
            double r = planet_xyz[3];
            double mag = cmt.g + 5 * log10(dist) + 2.5 * cmt.k * log10(r);	// Schlyter's formula is wrong!
            return new double[11] { altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag };
        }

        public double[] comet_xyz(Comet cmt, double jday)
        {
            // heliocentric xyz for comet (cn is index to comets)
            // based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
            // returns heliocentric x, y, z, distance, longitude and latitude of object
            double d = jday - 2451543.5;
            double q = cmt.q;
            double e = cmt.e;
            //double Tj = jd0(cmt.y, cmt.m, cmt.d);	// get julian day of perihelion time
            double Tj = cmt.T;

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
                double _w = W * (1 + g * c * (a1 + a2 * g + a3 * g * g));
                v = 2 * atand(_w);
                r = q * (1 + _w * _w) / (1 + _w * _w * f);
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

            double N = cmt.om + 3.82394E-5 * d;	// precess from J2000.0 to now;
            double w = cmt.w;	// why not precess this value?
            double i = cmt.i;
            double xh = r * (cosd(N) * cosd(v + w) - sind(N) * sind(v + w) * cosd(i));
            double yh = r * (sind(N) * cosd(v + w) + cosd(N) * sind(v + w) * cosd(i));
            double zh = r * (sind(v + w) * sind(i));
            double lonecl = atan2d(yh, xh);
            double latecl = atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));

            return new double[6] { xh, yh, zh, r, lonecl, latecl };
        }

        public double[] sunxyz(double jday)
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

            return new double[6] { xs, ys, 0, r, lonsun, 0 };
        }

        double[] radecr(double[] obj, double[] sun, double jday, Location obs)
        {
            // radecr returns ra, dec and earth distance
            // obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
            // (note Sun coords are really Earth heliocentric coordinates with reverse signs)
            // Equatorial geocentric co-ordinates
            double xg = obj[0] + sun[0];
            double yg = obj[1] + sun[1];
            double zg = obj[2];
            // Obliquity of Ecliptic (exponent corrected, was E-9!)
            double obl = 23.4393 - 3.563E-7 * (jday - 2451543.5);
            // Convert to eq. co-ordinates
            double x1 = xg;
            double y1 = yg * cosd(obl) - zg * sind(obl);
            double z1 = yg * sind(obl) + zg * cosd(obl);
            // RA and dec (33.2)
            double ra = rev(atan2d(y1, x1));
            double dec = atan2d(z1, Math.Sqrt(x1 * x1 + y1 * y1));
            double dist = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            return new double[3] { ra, dec, dist };
        }

        double[] radec2aa(double ra, double dec, double jday, Location obs)
        {
            // Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
            // DOES NOT correct for parallax!
            // TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
            var TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
            var H = rev(TH0 - obs.longitude - ra);
            var alt = asind(sind(obs.latitude) * sind(dec) + cosd(obs.latitude) * cosd(dec) * cosd(H));
            var az = atan2d(sind(H), (cosd(H) * sind(obs.latitude) - tand(dec) * cosd(obs.latitude)));
            return new double[3] { alt, rev(az + 180.0), H };
        }

        string hmsstring(double t)
        {
            // the caller must add a leading + if required.
            var hours = Math.Abs(t);
            var minutes = 60.0 * (hours - Math.Floor(hours));
            hours = Math.Floor(hours);
            var seconds = Math.Round(60.0 * (minutes - Math.Floor(minutes)));
            minutes = Math.Floor(minutes);
            if (seconds >= 60) { minutes += 1; seconds -= 60; }
            if (minutes >= 60) { hours += 1; minutes -= 60; }
            if (hours >= 24) { hours -= 24; }
            var hmsstr = (t < 0) ? "-" : "";
            hmsstr = ((hours < 10) ? "0" : "") + hours;
            hmsstr += ((minutes < 10) ? ":0" : ":") + minutes;
            hmsstr += ((seconds < 10) ? ":0" : ":") + seconds;
            return hmsstr;
        }

        string anglestring(double a, bool circle, bool arcmin)
        {
            // Return angle as degrees:minutes. 'circle' is true for range between 0 and 360 
            // and false for -90 to +90, if 'arcmin' use deg and arcmin symbols
            var ar = Math.Round(a * 60) / 60;
            var deg = Math.Abs(ar);
            var min = Math.Round(60.0 * (deg - Math.Floor(deg)));
            if (min >= 60) { deg += 1; min = 0; }
            var anglestr = "";
            if (!circle) anglestr += (ar < 0 ? "-" : "+");
            if (circle) anglestr += ((Math.Floor(deg) < 100) ? "0" : "");
            anglestr += ((Math.Floor(deg) < 10) ? "0" : "") + Math.Floor(deg);
            if (arcmin) anglestr += ((min < 10) ? "&deg;0" : "&deg;") + (min) + "' ";
            else anglestr += ((min < 10) ? ":0" : ":") + (min);
            return anglestr;
        }

        string fixnum(double n, int l, int d)
        {
            // convert float n to right adjusted string of length l with d digits after decimal point.
            // the sign always requires one character, allow for that in l!
            int m = 1;
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

        int[] jdtocd(double jd)
        {
            // The calendar date from julian date, see Meeus p. 63
            // Returns year, month, day, day of week, hours, minutes, seconds


            double year, month, day, hours, minutes, seconds;

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
            day = Math.Floor(d);
            double h = (d - day) * 24;
            hours = Math.Floor(h);
            double m = (h - hours) * 60;
            minutes = Math.Floor(m);
            seconds = Math.Round((m - minutes) * 60);
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
            return new int[] { 
                Convert.ToInt32(year), Convert.ToInt32(month),
                Convert.ToInt32(day), Convert.ToInt32(hours), 
                Convert.ToInt32(minutes), Convert.ToInt32(seconds)
            };
        }

        public static double r2d(double radAngle) { return radAngle * (180.0 / Math.PI); }
        public static double d2r(double degAngle) { return Math.PI * degAngle / 180.0; }

        public double rev(double angle) { return angle - Math.Floor(angle / 360.0) * 360.0; }		// 0<=a<360
        public double rev2(double angle) { var a = rev(angle); return (a >= 180 ? a - 360.0 : a); }	// -180<=a<180
        public double sind(double angle) { return Math.Sin(angle * DEG2RAD); }
        public double cosd(double angle) { return Math.Cos(angle * DEG2RAD); }
        public double tand(double angle) { return Math.Tan(angle * DEG2RAD); }
        public double asind(double c) { return RAD2DEG * Math.Asin(c); }
        public double acosd(double c) { return RAD2DEG * Math.Acos(c); }
        public double atand(double c) { return RAD2DEG * Math.Atan(c); }
        public double atan2d(double y, double x) { return RAD2DEG * Math.Atan2(y, x); }
        public double log10(double x) { return Math.Log10(x); }
        public double sqr(double x) { return x * x; }
        public double cbrt(double x) { return Math.Pow(x, 1 / 3.0); }
        public double SGN(double x) { return (x < 0) ? -1 : +1; }


    }
}
