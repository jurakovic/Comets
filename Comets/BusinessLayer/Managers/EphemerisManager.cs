using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Comets.BusinessLayer.Managers
{
	public static class EphemerisManager
	{
		#region Const

		const double DEG2RAD = Math.PI / 180.0;
		const double RAD2DEG = 180.0 / Math.PI;

		#endregion

		#region GetEphemeris

		public static EphemerisResult GetEphemeris(Comet comet, double jd, Location location)
		{
			return GetEphemeris(comet.T, comet.q, comet.e, comet.w, comet.N, comet.i, comet.g, comet.k, jd, location.Latitude, location.Longitude);
		}

		public static EphemerisResult GetEphemeris(double T, double q, double e, double w, double N, double i, double g, double k, double jd, double latitude, double longitude)
		{
			EphemerisResult er = new EphemerisResult();

			double[] dat = CometAlt(T, q, e, w, N, i, g, k, jd, latitude, longitude);
			er.Alt = dat[0];
			er.Az = dat[1];
			er.RA = dat[3];
			er.Dec = dat[4] - (dat[4] > 180.0 ? 360 : 0);
			er.EcLon = Rev(dat[5]);
			er.EcLat = dat[6];
			er.SunDist = dat[8];
			er.EarthDist = dat[9];
			er.Magnitude = dat[10];

			double[] sundat = SunAlt(jd, latitude, longitude);
			double sunra = sundat[3];
			double sundec = sundat[4] - (sundat[4] > 180.0 ? 360 : 0);

			double[] sep = Separation(er.RA, sunra, er.Dec, sundec);
			er.Elongation = sep[0];
			er.PositionAngle = sep[1];

			er.JD = jd;

			return er;
		}

		#endregion

		#region CalculateEphemerisAsync

		public async static Task<SettingsBase> CalculateEphemerisAsync(SettingsBase settings, IProgress<int> progress, CancellationToken ct)
		{
			await Task.Run(() =>
			{
				if (!settings.IsMultipleMode || settings.Comets.Count == 1)
				{
					CalculateEphemeris(settings, settings.SelectedComet, ct);
				}
				else
				{
					int i = 1;
					foreach (Comet comet in settings.Comets)
					{
						progress.Report(i++);
						CalculateEphemeris(settings, comet, ct);
					}
				}
			});

			return settings;
		}

		private static void CalculateEphemeris(SettingsBase settings, Comet comet, CancellationToken ct)
		{
			decimal jd = (decimal)settings.Start.JD();
			decimal jdMax = (decimal)settings.Stop.JD();
			decimal interval = (decimal)settings.Interval;

			List<EphemerisResult> erList = new List<EphemerisResult>();
			while (jd <= jdMax)
			{
				ct.ThrowIfCancellationRequested();

				bool ch1 = true;
				bool ch2 = true;
				bool ch3 = true;

				EphemerisResult er = GetEphemeris(comet, (double)jd, settings.Location);

				if (settings.MinMagnitudeChecked)
					ch1 = er.Magnitude <= settings.MinMagnitudeValue;

				if (settings.MaxSunDistChecked)
					ch2 = er.SunDist <= settings.MaxSunDistValue;

				if (settings.MaxEarthDistChecked)
					ch3 = er.EarthDist <= settings.MaxEarthDistValue;

				if (ch1 && ch2 && ch3)
					erList.Add(er);

				jd += interval;
			}

			if (erList.Count > 0)
				settings.Results.Add(comet, erList);
		}

		#endregion

		#region GenerateEphemerisAsync

		public async static Task<string> GenerateEphemerisAsync(EphemerisSettings settings, IProgress<int> progress, CancellationToken ct)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilder line = new StringBuilder();

			await Task.Run(() =>
			{
				Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

				int i = 1;
				foreach (var keyVal in settings.Results)
				{
					ct.ThrowIfCancellationRequested();
					progress.Report(i++);

					Comet comet = keyVal.Key;
					List<EphemerisResult> list = keyVal.Value;

					sb.AppendLine(String.Format("Comet:               \t{0}", comet.full));

					sb.AppendLine(String.Format("Perihelion date:     \t{0}", Utils.JDToDateTime(comet.Tn).ToLocalTime().ToString("dd MMM yyyy HH:mm:ss")));
					sb.AppendLine(String.Format("Perihelion distance: \t{0:0.000000} AU", comet.q));
					sb.AppendLine(String.Format("Period:              \t{0}", (comet.P < 10000 ? comet.P.ToString("0.000000") + " years" : "-")));
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
					if (settings.Magnitude) sb.Append(" Mag.");

					sb.AppendLine();

					foreach (EphemerisResult er in list)
					{
						line.Clear();

						DateTime dt = settings.LocalTime ? Utils.JDToDateTime(er.JD).ToLocalTime() : Utils.JDToDateTime(er.JD);
						line.Append(dt.ToString("dd.MM.yyyy HH:mm"));
						if (settings.RA) line.Append("  " + HMSString(er.RA / 15.0));
						if (settings.Dec) line.Append("  " + AngleString(er.Dec, false, true));
						if (settings.Alt) line.AppendFormat("{0,8:0.0°}", er.Alt);
						if (settings.Az) line.AppendFormat("{0,8:0.0°}", er.Az);
						if (settings.EcLon) line.Append("  " + AngleString(er.EcLon, true, true));
						if (settings.EcLat) line.Append("  " + AngleString(er.EcLat, false, true));
						if (settings.Elongation) line.AppendFormat("{0,8:0.0°} {1}", er.Elongation, er.PositionAngle >= 180 ? "W" : "E");
						if (settings.HelioDist) line.AppendFormat("{0,9:0.0000}", er.SunDist);
						if (settings.GeoDist) line.AppendFormat("{0,9:0.0000}", er.EarthDist);
						if (settings.Magnitude) line.AppendFormat("{0,6:0.0}", er.Magnitude);

						sb.AppendLine(line.ToString());
					}

					sb.AppendLine().AppendLine().AppendLine();
				}
			});

			return sb.ToString();
		}

		#endregion

		#region GenerateGraph

		public static void GenerateGraph(GraphSettings settings, Chart chart1, IProgress<int> progress)
		{
			string xDate = "Date";
			string yMagnitude = "Magnitude";
			string chartAreaName = "ChartAreaGraph";
			double multipleMaxMagnitude = 15.0;

			double minMagnitude = Math.Floor(settings.Results.Select(x => x.Value).Select(y => y.Min(z => z.Magnitude)).Min() - 0.20);
			double maxMagnitude = Math.Ceiling(settings.Results.Select(x => x.Value).Select(y => y.Max(z => z.Magnitude)).Max() + 0.20);

			double yMin;
			double yMax;

			if (settings.MinGraphMagnitudeChecked)
				yMin = settings.MinGraphMagnitudeValue.Value;
			else
				yMin = minMagnitude;

			if (settings.MaxGraphMagnitudeChecked)
				yMax = settings.MaxGraphMagnitudeValue.Value;
			else if (settings.IsMultipleMode && settings.Results.Count > 1)
				yMax = multipleMaxMagnitude;
			else
				yMax = maxMagnitude;

			if (yMin >= yMax)
			{
				yMin = minMagnitude;
				yMax = maxMagnitude;
			}

			double xMin = settings.Start.JD();
			double xMax = settings.Stop.JD();

			chart1.AntiAliasing = settings.AntialiasingChecked ? AntiAliasingStyles.All : AntiAliasingStyles.Text;

			ChartArea chartArea = new ChartArea();
			chartArea.Name = chartAreaName;

			chartArea.AxisX2.Title = xDate;
			chartArea.AxisX2.TitleAlignment = StringAlignment.Far;
			chartArea.AxisX2.TitleFont = new Font("Tahoma", 8.25F);
			chartArea.AxisX2.MajorGrid.Enabled = false;
			chartArea.AxisX2.IsLabelAutoFit = false;
			chartArea.AxisX2.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
			chartArea.AxisX2.IsMarginVisible = false;
			chartArea.AxisX2.LabelStyle.Font = new Font("Tahoma", 8.25F);

			string format;

			if ((xMax - xMin) <= 3.0)
				format = "dd MMM yyyy HH:mm";
			else
				format = "dd MMM yyyy";

			chartArea.AxisX2.LabelStyle.Format = format;

			chartArea.AxisY.Title = yMagnitude;
			chartArea.AxisY.TitleAlignment = StringAlignment.Far;
			chartArea.AxisY.TitleFont = new Font("Tahoma", 8.25F);
			chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;
			chartArea.AxisY.IsReversed = true;
			chartArea.AxisY.MajorGrid.Enabled = false;
			chartArea.AxisY.IsLabelAutoFit = false;
			chartArea.AxisY.IsMarginVisible = false;
			chartArea.AxisY.MajorTickMark.Size = 0.5F;
			chartArea.AxisY.LabelStyle.Font = new Font("Tahoma", 8.25F);

			double yInterval = 0D;

			if (yMax - yMin <= 1)
				yInterval = 0.1D;
			else if (yMax - yMin <= 2)
				yInterval = 0.2D;
			else if (yMax - yMin <= 5)
				yInterval = 0.5D;
			else if (yMax - yMin <= 10)
				yInterval = 1D;
			else
				yInterval = 2D;

			chartArea.AxisX2.Minimum = Utils.JDToDateTime(xMin).ToLocalTime().ToOADate();
			chartArea.AxisX2.Maximum = Utils.JDToDateTime(xMax).ToLocalTime().ToOADate();
			chartArea.AxisX2.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.IntervalOffset = 0;

			chartArea.AxisY.Minimum = yMin;
			chartArea.AxisY.Maximum = yMax;
			chartArea.AxisY.Interval = yInterval;

			chartArea.Position.Auto = false;
			chartArea.Position.Height = 90F;
			chartArea.Position.Width = 96F;
			chartArea.Position.X = 1F;
			chartArea.Position.Y = 8F;

			chart1.ChartAreas.Clear();
			chart1.ChartAreas.Add(chartArea);

			chart1.Series.Clear();

			int i = 1;
			foreach (List<EphemerisResult> erList in settings.Results.Select(x => x.Value))
			{
				progress.Report(i++);

				if (erList.Any(x => x.Magnitude <= yMax))
				{
					Series series = new Series();
					series.ChartArea = chartAreaName;
					series.Color = settings.MagnitudeColor;
					series.ChartType = SeriesChartType.Spline;
					series.XAxisType = AxisType.Secondary;
					series.XValueType = ChartValueType.DateTime;

					foreach (EphemerisResult er in erList)
						series.Points.Add(new DataPoint(Utils.JDToDateTime(er.JD).ToLocalTime().ToOADate(), er.Magnitude)); //local time

					chart1.Series.Add(series);
				}
			}

			if (settings.PerihelionLineChecked)
			{
				if (!settings.IsMultipleMode)
					AddPerihelionLines(chart1, settings.SelectedComet, settings.PerihelionLineColor, xMin, xMax, yMin, yMax, chartAreaName);
				else
					foreach (Comet c in settings.Comets)
						AddPerihelionLines(chart1, c, settings.PerihelionLineColor, xMin, xMax, yMin, yMax, chartAreaName);
			}

			if (settings.NowLineChecked)
			{
				double JDnow = DateTime.Now.JD();
				if (xMin < JDnow && xMax > JDnow)
				{
					Series s = new Series();
					s.ChartArea = chartAreaName;
					s.Color = settings.NowLineColor;
					s.ChartType = SeriesChartType.Line;
					s.XAxisType = AxisType.Secondary;
					s.XValueType = ChartValueType.DateTime;
					s.Points.Add(new DataPoint(Utils.JDToDateTime(JDnow).ToLocalTime().ToOADate(), yMin));
					s.Points.Add(new DataPoint(Utils.JDToDateTime(JDnow).ToLocalTime().ToOADate(), yMax));
					chart1.Series.Add(s);
				}
			}

			Title title = new Title(settings.SelectedComet.ToString());
			title.Font = new Font("Tahoma", 11.25F);
			chart1.Titles.Clear();
			chart1.Titles.Add(title);
		}

		private static void AddPerihelionLines(Chart chart1, Comet comet, Color color, double xMin, double xMax, double yMin, double yMax, string chartAreaName)
		{
			double t = comet.Tn;
			double periodDays = comet.P * 365.25;

			while (t - periodDays > xMin)
				t -= periodDays;

			while (t < xMax)
			{
				Series s = new Series();
				s.ChartArea = chartAreaName;
				s.Color = color;
				s.ChartType = SeriesChartType.Line;
				s.XAxisType = AxisType.Secondary;
				s.XValueType = ChartValueType.DateTime;
				s.Points.Add(new DataPoint(Utils.JDToDateTime(t).ToLocalTime().ToOADate(), yMin));
				s.Points.Add(new DataPoint(Utils.JDToDateTime(t).ToLocalTime().ToOADate(), yMax));

				chart1.Series.Add(s);
				t += periodDays;
			}
		}

		#endregion

		#region Calculate

		private static double[] CometAlt(double T, double q, double e, double w, double N, double i, double g, double k, double jd, double latitude, double longitude)
		{
			// Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
			double[] sun_xyz = SunXyz(jd);
			double[] cmt_xyz = CometXyz(T, q, e, w, N, i, jd);
			double dx = cmt_xyz[0] + sun_xyz[0];
			double dy = cmt_xyz[1] + sun_xyz[1];
			double dz = cmt_xyz[2] + sun_xyz[2];
			double lon = Rev(Atan2d(dy, dx));
			double lat = Atan2d(dz, Math.Sqrt(dx * dx + dy * dy));
			double[] radec = RaDecDist(cmt_xyz, sun_xyz, jd);
			double ra = radec[0];
			double dec = radec[1];
			double[] altaz = RaDecToAltAz(ra, dec, jd, latitude, longitude);
			double dist = radec[2];
			double r = cmt_xyz[3];
			double mag = g + 5 * Log10(dist) + 2.5 * k * Log10(r);
			return new double[] { altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag };
		}

		private static double[] SunAlt(double jd, double latitude, double longitude)
		{
			// return alt, az, time angle, ra, dec, ecl. long. and lat=0, illum=1, 0, dist, brightness 
			double[] sdat = SunXyz(jd);
			double ecl = 23.439291111 - 3.563E-7 * (jd - 2451543.5);
			double xe = sdat[0];
			double ye = sdat[1] * Cosd(ecl);
			double ze = sdat[1] * Sind(ecl);
			double ra = Rev(Atan2d(ye, xe));
			double dec = Atan2d(ze, Math.Sqrt(xe * xe + ye * ye));
			double[] topo = RaDecToAltAz(ra, dec, jd, latitude, longitude);
			return new double[] { topo[0], topo[1], topo[2], ra, dec, sdat[4], 0, 1, 0, sdat[3], -26.74 };
		}

		private static double[] SunXyz(double jd)
		{
			// return x,y,z ecliptic coordinates, distance, true longitude
			// days counted from 1999 Dec 31.0 UT
			double d = jd - 2451543.5;
			double w = 282.9404 + 4.70935E-5 * d;
			double e = 0.016709 - 1.151E-9 * d;
			double M = Rev(356.0470 + 0.9856002585 * d);
			double E = M + e * RAD2DEG * Sind(M) * (1.0 + e * Cosd(M));
			double xv = Cosd(E) - e;
			double yv = Math.Sqrt(1.0 - e * e) * Sind(E);
			double v = Atan2d(yv, xv);		// true anomaly
			double r = Math.Sqrt(xv * xv + yv * yv);	// distance
			double lonsun = Rev(v + w);	// true longitude
			double xs = r * Cosd(lonsun);		// rectangular coordinates, zs = 0 for sun 
			double ys = r * Sind(lonsun);
			return new double[] { xs, ys, 0, r, lonsun, 0 };
		}

		private static double[] CometXyz(double T, double q, double e, double w, double N, double i, double jd)
		{
			// heliocentric xyz for comet (cn is index to comets)
			// based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
			// returns heliocentric x, y, z, distance, longitude and latitude of object
			double d = jd - 2451543.5;
			double Tj = T;	// get julian day of perihelion time
			double v, r;
			if (e > 0.99)
			{
				// treat as near parabolic (approx. method valid inside orbit of Pluto)
				double k = 0.01720209895;	// Gaussian gravitational constant
				double a = 0.75 * (jd - Tj) * k * Math.Sqrt((1 + e) / (q * q * q));
				double b = Math.Sqrt(1 + a * a);
				double W = Cbrt(b + a) - Cbrt(b - a);
				double c = (W * W) / (1 + W * W);
				double f = (1 - e) / (1 + e);
				double g = f * c * c;
				double a1 = (2 / 3) + (2 / 5) * W * W;
				double a2 = (7 / 5) + (33 / 35) * W * W + (37 / 175) * W * W * W * W;
				double a3 = W * W * ((432 / 175) + (956 / 1125) * W * W + (84 / 1575) * W * W * W * W);
				double ww = W * (1 + g * c * (a1 + a2 * g + a3 * g * g));
				v = 2 * Atand(ww);
				r = q * (1 + ww * ww) / (1 + ww * ww * f);
			}
			else
			{		// treat as elliptic
				double a = q / (1.0 - e);
				double P = 365.2568984 * Math.Sqrt(a * a * a);	// period in days
				double M = 360.0 * (jd - Tj) / P; 	// mean anomaly
				// eccentric anomaly E
				double E0 = M + RAD2DEG * e * Sind(M) * (1.0 + e * Cosd(M));
				double E1 = E0 - (E0 - RAD2DEG * e * Sind(E0) - M) / (1.0 - e * Cosd(E0));
				while (Math.Abs(E0 - E1) > 0.0005)
				{
					E0 = E1;
					E1 = E0 - (E0 - RAD2DEG * e * Sind(E0) - M) / (1.0 - e * Cosd(E0));
				}
				double xv = a * (Cosd(E1) - e);
				double yv = a * Math.Sqrt(1.0 - e * e) * Sind(E1);
				v = Rev(Atan2d(yv, xv));		// true anomaly
				r = Math.Sqrt(xv * xv + yv * yv);	// distance
			}	// from here common for all orbits
			N = N + 3.82394E-5 * d;
			//w  ->  why not precess this value?
			double xh = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double yh = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double zh = r * (Sind(v + w) * Sind(i));
			double lonecl = Atan2d(yh, xh);
			double latecl = Atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
			return new double[] { xh, yh, zh, r, lonecl, latecl };
		}

		private static double[] RaDecDist(double[] cmt, double[] sun, double jd)
		{
			// radecr returns ra, dec and earth distance
			// obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
			// (note Sun coords are really Earth heliocentric coordinates with reverse signs)
			// Equatorial geocentric co-ordinates
			double xg = cmt[0] + sun[0];
			double yg = cmt[1] + sun[1];
			double zg = cmt[2];
			// Obliquity of Ecliptic
			double obl = 23.439291111 - 3.563E-7 * (jd - 2451543.5);
			// Convert to eq. co-ordinates
			double x1 = xg;
			double y1 = yg * Cosd(obl) - zg * Sind(obl);
			double z1 = yg * Sind(obl) + zg * Cosd(obl);
			// RA and dec (33.2)
			double ra = Rev(Atan2d(y1, x1));
			double dec = Atan2d(z1, Math.Sqrt(x1 * x1 + y1 * y1));
			double dist = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
			return new double[] { ra, dec, dist };
		}

		private static double[] RaDecToAltAz(double ra, double dec, double jday, double latitude, double longitude)
		{
			// Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
			// TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
			double TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
			double H = Rev(TH0 + longitude - ra);
			double alt = Asind(Sind(latitude) * Sind(dec) + Cosd(latitude) * Cosd(dec) * Cosd(H));
			double az = Atan2d(Sind(H), (Cosd(H) * Sind(latitude) - Tand(dec) * Cosd(latitude)));
			return new double[] { alt, Rev(az + 180.0), H };
		}

		private static double[] Separation(double ra1, double ra2, double dec1, double dec2)
		{
			// ra, dec may also be long, lat, but PA is relative to the chosen coordinate system
			double d = Acosd(Sind(dec1) * Sind(dec2) + Cosd(dec1) * Cosd(dec2) * Cosd(ra1 - ra2));		// (Meeus 17.1)
			if (d < 0.1) d = Math.Sqrt(Sqr(Rev2(ra1 - ra2) * Cosd((dec1 + dec2) / 2)) + Sqr(dec1 - dec2));	// (17.2)
			double pa = Atan2d(Sind(ra1 - ra2), Cosd(dec2) * Tand(dec1) - Sind(dec2) * Cosd(ra1 - ra2));		// angle
			return new double[] { d, Rev(pa) };
		}

		public static string HMSString(double t)
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

		public static string AngleString(double a, bool circle, bool arcmin)
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

		#endregion

		#region Date methods

		public static double JD(DateTime dt)
		{
			DateTime d = dt.ToUniversalTime();
			return JD(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
		}

		private static double JD(double year, double month, double day, double hour, double min, double sec)
		{
			double j = JD0(year, month, day, 0);
			j += (hour + (min / 60.0) + (sec / 3600.0)) / 24;
			return j;
		}

		public static double JD0(double year, double month, double day, double hour)
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

		public static int[] JDToDateTime(double jd0)
		{
			double jd = jd0 + 0.5;
			double a = Math.Floor(jd);

			if (a >= 2299160.5)
			{
				double t = Math.Floor((a - 1867216.25) / 36524.25);
				a += t - Math.Floor(t / 4.0) + 1.0;
			}

			double b = Math.Floor(a) + 1524;
			double c = Math.Floor((b - 122.1) / 365.25);
			double k = Math.Floor((365.25) * c);
			double e = Math.Floor((b - k) / 30.6001);

			double day0 = b - k - Math.Floor(30.6001 * e) + (jd - Math.Floor(jd));

			int month = (int)Math.Floor(e - ((e >= 13.5) ? 13 : 1) + 0.5);
			int year = (int)Math.Floor(c - ((month > 2) ? 4716 : 4715) + 0.5);
			int day = (int)Math.Floor(day0);

			double hour0 = ((day0 - day) * 24.0) + 0.0000001; // fix for 23:59:59
			int hour = (int)Math.Floor(hour0);

			double min = (hour0 - hour) * 60.0;
			int minute = (int)Math.Floor(min);
			int second = (int)((min - minute) * 60.0);

			int dw = (int)(Math.Floor(jd + 1.5) - 7 * Math.Floor((jd + 1.5) / 7));

			return new int[] { year, month, day, dw, hour, minute, second };
		}

		#endregion

		#region Math functions

		/// <summary>
		/// 0 <= a < 360
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		private static double Rev(double angle)
		{
			return angle - Math.Floor(angle / 360.0) * 360.0;
		}

		/// <summary>
		/// -180 <= a < 180
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		private static double Rev2(double angle)
		{
			double a = Rev(angle); return (a >= 180 ? a - 360.0 : a);
		}

		private static double Sind(double angle)
		{
			return Math.Sin(angle * DEG2RAD);
		}

		private static double Cosd(double angle)
		{
			return Math.Cos(angle * DEG2RAD);
		}

		private static double Tand(double angle)
		{
			return Math.Tan(angle * DEG2RAD);
		}

		private static double Asind(double c)
		{
			return RAD2DEG * Math.Asin(c);
		}

		private static double Acosd(double c)
		{
			return RAD2DEG * Math.Acos(c);
		}

		private static double Atand(double c)
		{
			return RAD2DEG * Math.Atan(c);
		}

		private static double Atan2d(double y, double x)
		{
			return RAD2DEG * Math.Atan2(y, x);
		}

		private static double Log10(double x)
		{
			return 0.43429448190325182765112891891661 * Math.Log(x);
		}

		private static double Sqr(double x)
		{
			return x * x;
		}

		private static double Cbrt(double x)
		{
			return Math.Pow(x, 1 / 3.0);
		}

		#endregion
	}
}
