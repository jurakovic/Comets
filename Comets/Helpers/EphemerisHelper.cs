using Comets.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Comets.Helpers
{
    public static class EphemerisHelper
    {
        #region Const

        const double DEG2RAD = Math.PI / 180.0;
        const double RAD2DEG = 180.0 / Math.PI;

        #endregion

        #region CalculateEphemeris

        public async static Task<CommonSettings> CalculateEphemeris(CommonSettings settings)
        {
            double jd = settings.MinUtcJD;
            double jdMax = settings.MaxUtcJD;
            double jdLoc = settings.MinLocalJD;

            await Task.Run(() =>
            {
                while (jd <= jdMax)
                {
                    EphemerisResult er = new EphemerisResult();

                    double[] dat = CometAlt(settings.Comet, jd, settings.Location);
                    er.Alt = dat[0];
                    er.Az  = dat[1];
                    //double ha = dat[2];
                    er.RA = dat[3];
                    er.Dec = dat[4] - (dat[4] > 180.0 ? 360 : 0);
                    er.EcLon = rev(dat[5]);
                    er.EcLat = dat[6];
                    //double ill = dat[7];
                    er.HelioDist = dat[8];
                    er.GeoDist = dat[9];
                    er.Magnitude = dat[10];

                    double[] sundat = SunAlt(jd, settings.Location);
                    double sunra = sundat[3];
                    double sundec = sundat[4] - (sundat[4] > 180.0 ? 360 : 0);

                    double[] sep = separation(er.RA, sunra, er.Dec, sundec);
                    er.Elongation = sep[0];
                    er.PositionAngle = sep[1];

                    er.UtcJD = jd;
                    er.LocalJD = jdLoc;

                    settings.Results.Add(er);
                    
                    jd += settings.Interval;
                    jdLoc += settings.Interval;
                }
            });

            return settings;
        }

        #endregion

        #region GenerateEphemeris

        public async static Task<string> GenerateEphemeris(EphemerisSettings settings)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format("Comet:               \t{0}", settings.Comet.full));
            sb.AppendLine(String.Format("Perihelion date:     \t{0} {1} {2:00}.{3:0000}", settings.Comet.Ty, Comet.Month[settings.Comet.Tm - 1], settings.Comet.Td, settings.Comet.Th));
            sb.AppendLine(String.Format("Perihelion distance: \t{0:0.000000} AU", settings.Comet.q));
            sb.AppendLine(String.Format("Period:              \t{0}", (settings.Comet.P < 10000 && settings.Comet.e < 0.98 ? settings.Comet.P.ToString("0.000000") + " years" : "-")));
            sb.AppendLine();

            sb.Append(settings.LocalTime ? "     Local Time  " : " Universal Time  ");
            if (settings.RA) sb.Append("   R.A.   ");
            if (settings.Dec) sb.Append("   Dec   ");
            if (settings.Alt) sb.Append("   Alt  ");
            if (settings.Az) sb.Append("   Az   ");
            if (settings.EcLon) sb.Append(" Ecl.Lon ");
            if (settings.EcLat) sb.Append(" Ecl.Lat ");
            if (settings.Elongation) sb.Append("   Elong. ");
            if (settings.HelioDist) sb.Append("    r    ");
            if (settings.GeoDist) sb.Append("    d    ");
            if (settings.Magnitude) sb.AppendLine(" Mag.");

            StringBuilder line = new StringBuilder();

            await Task.Run(() =>
            {
                foreach (EphemerisResult er in settings.Results)
                {
                    line.Clear();

                    line.Append(settings.LocalTime ? dateString(er.LocalJD) : dateString(er.UtcJD));
                    if (settings.RA) line.Append("  " + hmsstring(er.RA / 15.0));
                    if (settings.Dec) line.Append("  " + anglestring(er.Dec, false, true));
                    if (settings.Alt) line.Append("  " + fixnum(er.Alt, 5, 1) + "°");
                    if (settings.Az) line.Append(" " + fixnum(er.Az, 6, 1) + "°");
                    if (settings.EcLon) line.Append("  " + anglestring(er.EcLon, true, true));
                    if (settings.EcLat) line.Append("  " + anglestring(er.EcLat, false, true));
                    if (settings.Elongation) line.Append(" " + fixnum(er.Elongation, 6, 1) + "°" + (er.PositionAngle >= 180 ? " W" : " E"));
                    if (settings.HelioDist) line.Append(" " + fixnum(er.HelioDist, 8, 4));
                    if (settings.GeoDist) line.Append(" " + fixnum(er.GeoDist, 8, 4));
                    if (settings.Magnitude) line.Append(" " + fixnum(er.Magnitude, 4, 1));

                    sb.AppendLine(line.ToString());
                }
            });

            return sb.ToString();
        }

        #endregion

        #region GenerateGraph

        public static void GenerateGraph(GraphSettings settings, Chart chart1)
        {
            string xDate = "Date";
            string yMagnitude = "Magnitude";
            string chartAreaName = "ChartAreaGraph";

            double minY = settings.Results.Min(x => x.Magnitude);
            double maxY = settings.Results.Max(x => x.Magnitude);

            minY = Math.Floor(minY - 0.25);
            maxY = Math.Ceiling(maxY + 0.25);

            //minY = 0; maxY = 20;

            double minX = settings.Results.Min(x => x.LocalJD);
            double maxX = settings.Results.Max(x => x.UtcJD);

            //double minX = settings.MinLocalJD > settings.MinUtcJD ? settings.MinLocalJD : settings.MinUtcJD;
            //double maxX = settings.MaxLocalJD < settings.MaxUtcJD ? settings.MaxLocalJD : settings.MaxUtcJD;

            //double minX = settings.MinLocalJD;
            //double maxX = settings.MaxUtcJD;

            double T = settings.Comet.T;

            chart1.AntiAliasing = AntiAliasingStyles.Text;

            ChartArea chartArea = new ChartArea();

            chartArea.AxisX2.Title = xDate;
            chartArea.AxisX2.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea.AxisX2.TitleFont = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.AxisX2.MajorGrid.Enabled = false;
            chartArea.AxisX2.IsLabelAutoFit = false;
            //chartArea.AxisX2.LabelStyle.IsEndLabelVisible = false;
            chartArea.AxisX2.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chartArea.AxisX2.IsMarginVisible = false;
            chartArea.AxisX2.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.AxisX2.LabelStyle.Format = "dd MMMM yyyy";                  /////date

            chartArea.AxisY.Title = yMagnitude;
            chartArea.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;
            chartArea.AxisY.IsReversed = true;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            //chartArea.AxisY.LabelStyle.IsEndLabelVisible = false;
            chartArea.AxisY.IsMarginVisible = false;
            chartArea.AxisY.MajorTickMark.Size = 0.5F;
            chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.Name = chartAreaName;

            Double interval = 0D;
          
            if (maxY - minY <= 1)
                interval = 0.1D;
            else if (maxY - minY <= 2)
                interval = 0.2D;
            else if (maxY - minY <= 5)
                interval = 0.5D;
            else if (maxY - minY <= 10)
                interval = 1D;
            else
                interval = 2D;

            chartArea.AxisX2.Interval = (maxX - minX) / 10;
            chartArea.AxisY.Interval = interval;

            chartArea.Position.Auto = false;
            chartArea.Position.Height = 90F;
            chartArea.Position.Width = 96F;
            chartArea.Position.X = 1F;
            chartArea.Position.Y = 8F;

            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartArea = chartAreaName;
            series.Color = System.Drawing.Color.Red;
            series.ChartType = SeriesChartType.Spline;
            series.XAxisType = AxisType.Secondary;
            series.XValueType = ChartValueType.DateTime;                                    ///// date

            foreach (EphemerisResult er in settings.Results)
                series.Points.Add(new DataPoint(Utils.JDToOta(er.LocalJD), er.Magnitude));    ///// date
                //series.Points.Add(new DataPoint(er.UtcJD, er.Magnitude));                 ///// jd

            chart1.Series.Clear();
            chart1.Series.Add(series);

            chart1.ChartAreas[chartAreaName].AxisY.Minimum = minY;
            chart1.ChartAreas[chartAreaName].AxisY.Maximum = maxY;

            //margine
            //chart1.ChartAreas[chartAreaName].AxisX2.Minimum = JDfrom - (JDto - JDfrom) * 0.02;
            //chart1.ChartAreas[chartAreaName].AxisX2.Maximum = JDto + (JDto - JDfrom) * 0.02;
            chart1.ChartAreas[chartAreaName].AxisX2.Minimum = Utils.JDToOta(minX);
            chart1.ChartAreas[chartAreaName].AxisX2.Maximum = Utils.JDToOta(maxX);

            
            //generate perihelion lines
            double periodDays = settings.Comet.P * 365.25;

            if ((maxX - minX > periodDays) || (minX < T && maxX > T))
                while (T > minX + periodDays)
                    T -= periodDays;
            else
                T += periodDays;

            while (T < maxX)
            {
                Series s = new Series();
                s.ChartArea = chartAreaName;
                s.Color = System.Drawing.Color.RoyalBlue;
                s.ChartType = SeriesChartType.Line;
                s.XAxisType = AxisType.Secondary;

                //s.Points.Add(new DataPoint(T, min));                    ///// JD
                //s.Points.Add(new DataPoint(T, max));                    ///// JD

                s.XValueType = ChartValueType.DateTime;               ///// date
                s.Points.Add(new DataPoint(Utils.JDToOta(T), minY));   ///// date
                s.Points.Add(new DataPoint(Utils.JDToOta(T), maxY));   ///// date

                chart1.Series.Add(s);
                T += periodDays;
            }
            //end

            Title title = new Title(settings.Comet.ToString());
            title.Font = new System.Drawing.Font("Tahoma", 11.25F);
            chart1.Titles.Clear();
            chart1.Titles.Add(title);
        }

        #endregion

        #region Methods

        public static double[] CometAlt(Comet c, double jday, Location location)
        {
            // Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
            double[] sun_xyz = sunxyz(jday);
            double[] cmt_xyz = comet_xyz(c, jday);
            double dx = cmt_xyz[0] + sun_xyz[0];
            double dy = cmt_xyz[1] + sun_xyz[1];
            double dz = cmt_xyz[2] + sun_xyz[2];
            double lon = rev(atan2d(dy, dx));
            double lat = atan2d(dz, Math.Sqrt(dx * dx + dy * dy));
            double[] radec = radecr(cmt_xyz, sun_xyz, jday);
            double ra = radec[0];
            double dec = radec[1];
            double[] altaz = radec2aa(ra, dec, jday, location);
            double dist = radec[2];
            double r = cmt_xyz[3];
            double mag = c.g + 5 * log10(dist) + 2.5 * c.k * log10(r);
            return new double[] { altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag };
        }

        public static double[] SunAlt(double jday, Location location)
        {
            // return alt, az, time angle, ra, dec, ecl. long. and lat=0, illum=1, 0, dist, brightness 
            double[] sdat = sunxyz(jday);
            double ecl = 23.439291111 - 3.563E-7 * (jday - 2451543.5);
            double xe = sdat[0];
            double ye = sdat[1] * cosd(ecl);
            double ze = sdat[1] * sind(ecl);
            double ra = rev(atan2d(ye, xe));
            double dec = atan2d(ze, Math.Sqrt(xe * xe + ye * ye));
            double[] topo = radec2aa(ra, dec, jday, location);
            return new double[] { topo[0], topo[1], topo[2], ra, dec, sdat[4], 0, 1, 0, sdat[3], -26.74 };
        }

        public static double[] sunxyz(double jday)
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

        public static double[] comet_xyz(Comet cmt, double jday)
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

        public static double[] radecr(double[] obj, double[] sun, double jday)
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

        public static double[] radec2aa(double ra, double dec, double jday, Location location)
        {
            // Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
            // TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
            double TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
            double H = rev(TH0 + location.Longitude - ra);
            double alt = asind(sind(location.Latitude) * sind(dec) + cosd(location.Latitude) * cosd(dec) * cosd(H));
            double az = atan2d(sind(H), (cosd(H) * sind(location.Latitude) - tand(dec) * cosd(location.Latitude)));
            return new double[] { alt, rev(az + 180.0), H };
        }

        public static double[] separation(double ra1, double ra2, double dec1, double dec2)
        {
            // ra, dec may also be long, lat, but PA is relative to the chosen coordinate system
            double d = acosd(sind(dec1) * sind(dec2) + cosd(dec1) * cosd(dec2) * cosd(ra1 - ra2));		// (Meeus 17.1)
            if (d < 0.1) d = Math.Sqrt(sqr(rev2(ra1 - ra2) * cosd((dec1 + dec2) / 2)) + sqr(dec1 - dec2));	// (17.2)
            double pa = atan2d(sind(ra1 - ra2), cosd(dec2) * tand(dec1) - sind(dec2) * cosd(ra1 - ra2));		// angle
            return new double[] { d, rev(pa) };
        }

        public static string fixnum(double n, int l, int d)
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

        public static string hmsstring(double t)
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

        public static string anglestring(double a, bool circle, bool arcmin)
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

        public static double jd(DateTime dt)
        {
            return jd(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public static double jd(double year, double month, double day, double hour, double min, double sec)
        {
            double j = jd0(year, month, day, 0);
            j += (hour + (min / 60.0) + (sec / 3600.0)) / 24;
            return j;
        }

        public static double jd0(double year, double month, double day, double hour)
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

        public static int[] jdtocd(double jd)
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

        public static string dateString(double jday)
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

        public static double rev(double angle) { return angle - Math.Floor(angle / 360.0) * 360.0; }		// 0<=a<360
        public static double rev2(double angle) { double a = rev(angle); return (a >= 180 ? a - 360.0 : a); }	// -180<=a<180
        public static double sind(double angle) { return Math.Sin(angle * DEG2RAD); }
        public static double cosd(double angle) { return Math.Cos(angle * DEG2RAD); }
        public static double tand(double angle) { return Math.Tan(angle * DEG2RAD); }
        public static double asind(double c) { return RAD2DEG * Math.Asin(c); }
        public static double acosd(double c) { return RAD2DEG * Math.Acos(c); }
        public static double atand(double c) { return RAD2DEG * Math.Atan(c); }
        public static double atan2d(double y, double x) { return RAD2DEG * Math.Atan2(y, x); }
        public static double log10(double x) { return 0.43429448190325182765112891891661 * Math.Log(x); }
        public static double sqr(double x) { return x * x; }
        public static double cbrt(double x) { return Math.Pow(x, 1 / 3.0); }

        #endregion
    }
}
