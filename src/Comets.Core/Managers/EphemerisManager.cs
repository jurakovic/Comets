using Comets.Core;
using Comets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Comets.Core.Managers
{
	public static class EphemerisManager
	{
		#region Const

		const double EPSILON = 1e-10;

		const double DEG2RAD = Math.PI / 180.0;
		const double RAD2DEG = 180.0 / Math.PI;

		public static string SeriesValue = "VALUE:";
		public static string SeriesNow = "NOW:";
		public static string SeriesPerihelion = "PERIHELION:";

		public static DateTime MinimumDateTime = new DateTime(1700, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public static DateTime MaximumDateTime = new DateTime(2300, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#endregion

		#region GetEphemeris

		public static Ephemeris GetEphemeris(Comet comet, decimal jd, Location location)
		{
			return GetEphemeris(comet.T, comet.q, comet.e, comet.w, comet.N, comet.i, comet.g, comet.k, jd, location.Latitude, location.Longitude);
		}

		public static Ephemeris GetEphemeris(decimal T, double q, double e, double w, double N, double i, double g, double k, decimal jd, double latitude, double longitude)
		{
			Ephemeris ep = new Ephemeris();

			double[] dat = CometAlt(T, q, e, w, N, i, g, k, jd, latitude, longitude);
			ep.Alt = dat[0];
			ep.Az = dat[1];
			ep.RA = dat[3];
			ep.Dec = dat[4] - (dat[4] > 180.0 ? 360 : 0);
			ep.EcLon = Rev(dat[5]);
			ep.EcLat = dat[6];
			ep.SunDist = dat[8];
			ep.EarthDist = dat[9];
			ep.Magnitude = dat[10];

			double[] sundat = SunAlt(jd, latitude, longitude);
			double sunra = sundat[3];
			double sundec = sundat[4] - (sundat[4] > 180.0 ? 360 : 0);

			double[] sep = Separation(ep.RA, sunra, ep.Dec, sundec);
			ep.Elongation = sep[0];
			ep.PositionAngle = sep[1];

			ep.JD = jd;

			return ep;
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
			decimal jd = settings.Start.JD();
			decimal jdMax = settings.Stop.JD();
			decimal interval = settings.Interval;

			List<Ephemeris> epList = new List<Ephemeris>();

			while (jd <= jdMax)
			{
				ct.ThrowIfCancellationRequested();

				bool ch1 = true;
				bool ch2 = true;
				bool ch3 = true;

				Ephemeris ep = GetEphemeris(comet, jd, settings.Location);

				if (settings.MinMagnitudeChecked)
					ch1 = ep.Magnitude <= settings.MinMagnitudeValue;

				if (settings.MaxSunDistChecked)
					ch2 = ep.SunDist <= settings.MaxSunDistValue;

				if (settings.MaxEarthDistChecked)
					ch3 = ep.EarthDist <= settings.MaxEarthDistValue;

				if (ch1 && ch2 && ch3)
					epList.Add(ep);

				jd += interval;
			}

			if (epList.Count > 0)
				settings.Ephemerides.Add(comet, epList);
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
				foreach (var keyVal in settings.Ephemerides)
				{
					ct.ThrowIfCancellationRequested();
					progress.Report(i++);

					Comet comet = keyVal.Key;
					List<Ephemeris> list = keyVal.Value;

					sb.AppendLine(String.Format("Comet:               \t{0}", comet.full));

					sb.AppendLine(String.Format("Perihelion date:     \t{0}", JDToDateTime(comet.Tn).ToString(DateTimeFormat.PerihelionDate)));
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

					foreach (Ephemeris ep in list)
					{
						line.Clear();

						DateTime dt = settings.LocalTime ? JDToDateTime(ep.JD).ToLocalTime() : JDToDateTime(ep.JD);
						line.Append(dt.ToString(DateTimeFormat.Ephemeris));
						if (settings.RA) line.Append("  " + HMSString(ep.RA / 15.0));
						if (settings.Dec) line.Append("  " + AngleString(ep.Dec, false, true));
						if (settings.Alt) line.AppendFormat("{0,8:0.0°}", ep.Alt);
						if (settings.Az) line.AppendFormat("{0,8:0.0°}", ep.Az);
						if (settings.EcLon) line.Append("  " + AngleString(ep.EcLon, true, true));
						if (settings.EcLat) line.Append("  " + AngleString(ep.EcLat, false, true));
						if (settings.Elongation) line.AppendFormat("{0,8:0.0°} {1}", ep.Elongation, ep.PositionAngle >= 180 ? "W" : "E");
						if (settings.HelioDist) line.AppendFormat("{0,9:0.0000}", ep.SunDist);
						if (settings.GeoDist) line.AppendFormat("{0,9:0.0000}", ep.EarthDist);
						if (settings.Magnitude) line.AppendFormat("{0,6:0.0}", ep.Magnitude);

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
			string xAxisText = "Date";
			string yAxisText = String.Empty;
			string chartAreaName = "ChartAreaGraph";
			double multipleMaxMagnitude = 15.0;
			double multipleMaxDistance = 2.0;

			var dataPointDictionary = GetChartDataPointDictionary(settings.Ephemerides, settings.GraphChartType);

			var dataPointLists = dataPointDictionary.Select(x => x.Value);
			double min = dataPointLists.Select(x => x.Min(y => y.Value)).Min();
			double max = dataPointLists.Select(x => x.Max(y => y.Value)).Max();

			if (settings.GraphChartType == GraphSettings.ChartType.Magnitude)
			{
				//margin
				min -= 0.20;
				max += 0.20;
			}

			double minValue = Math.Floor(min);
			double maxValue = Math.Ceiling(max);

			switch (settings.GraphChartType)
			{
				case GraphSettings.ChartType.Magnitude:
					yAxisText = "Magnitude"; break;
				case GraphSettings.ChartType.SunDistance:
					yAxisText = "Sun distance (AU)"; break;
				case GraphSettings.ChartType.EarthDistance:
					yAxisText = "Earth distance (AU)"; break;
			}

			double yMinValue = settings.MinGraphValueChecked ? settings.MinGraphValue.Value : minValue;
			double yMaxValue;

			if (settings.MaxGraphValueChecked)
			{
				yMaxValue = settings.MaxGraphValue.Value;
			}
			else if (settings.IsMultipleMode && settings.Ephemerides.Count > 1)
			{
				if (settings.GraphChartType == GraphSettings.ChartType.Magnitude)
					yMaxValue = multipleMaxMagnitude;
				else
					yMaxValue = multipleMaxDistance;
			}
			else
			{
				yMaxValue = maxValue;
			}

			if (yMinValue >= yMaxValue)
			{
				yMinValue = minValue;
				yMaxValue = maxValue;
			}

			decimal xMinValue = settings.Start.JD();
			decimal xMaxValue = settings.Stop.JD();

			chart1.AntiAliasing = settings.AntialiasingChecked ? AntiAliasingStyles.All : AntiAliasingStyles.Text;
			chart1.BackColor = settings.BackgroundColor;

			ChartArea chartArea = new ChartArea();
			chartArea.Name = chartAreaName;
			chartArea.BackColor = settings.BackgroundColor;

			chartArea.AxisX2.Title = xAxisText;
			chartArea.AxisX2.TitleAlignment = StringAlignment.Far;
			chartArea.AxisX2.TitleFont = new Font("Tahoma", 8.25F);
			chartArea.AxisX2.TitleForeColor = settings.TextColor;
			chartArea.AxisX2.MajorGrid.Enabled = false;
			chartArea.AxisX2.IsLabelAutoFit = false;
			chartArea.AxisX2.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
			chartArea.AxisX2.IsMarginVisible = false;
			chartArea.AxisX2.MajorTickMark.LineColor = settings.TextColor;
			chartArea.AxisX2.LineColor = settings.TextColor;
			chartArea.AxisX2.LabelStyle.Font = new Font("Tahoma", 8.25F);
			chartArea.AxisX2.LabelStyle.ForeColor = settings.TextColor;
			chartArea.AxisX2.LabelStyle.Format = (xMaxValue - xMinValue) <= 3.0m ? DateTimeFormat.GraphLong : DateTimeFormat.GraphShort;
			chartArea.AxisY.Title = yAxisText;
			chartArea.AxisY.TitleAlignment = StringAlignment.Far;
			chartArea.AxisY.TitleFont = new Font("Tahoma", 8.25F);
			chartArea.AxisY.TitleForeColor = settings.TextColor;
			chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;
			chartArea.AxisY.IsReversed = true;
			chartArea.AxisY.MajorGrid.Enabled = false;
			chartArea.AxisY.IsLabelAutoFit = false;
			chartArea.AxisY.IsMarginVisible = false;
			chartArea.AxisY.MajorTickMark.Size = 0.5F;
			chartArea.AxisY.MajorTickMark.LineColor = settings.TextColor;
			chartArea.AxisY.LineColor = settings.TextColor;
			chartArea.AxisY.LabelStyle.Font = new Font("Tahoma", 8.25F);
			chartArea.AxisY.LabelStyle.ForeColor = settings.TextColor;
			chartArea.AxisY.LabelStyle.Format = "00.00";

			double yInterval = 0D;

			if (yMaxValue - yMinValue <= 1)
				yInterval = 0.1D;
			else if (yMaxValue - yMinValue <= 2)
				yInterval = 0.2D;
			else if (yMaxValue - yMinValue <= 5)
				yInterval = 0.5D;
			else if (yMaxValue - yMinValue <= 10)
				yInterval = 1D;
			else
				yInterval = 2D;

			chartArea.AxisX2.Minimum = JDToDateTime(xMinValue).ToOADate();
			chartArea.AxisX2.Maximum = JDToDateTime(xMaxValue).ToOADate();
			chartArea.AxisX2.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.IntervalOffset = 0;

			chartArea.AxisY.Minimum = yMinValue;
			chartArea.AxisY.Maximum = yMaxValue;
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
			foreach (var keyValuePair in dataPointDictionary)
			{
				progress.Report(i++);

				var dataPoints = keyValuePair.Value;

				if (dataPoints.Any(x => x.Value <= yMaxValue))
				{
					Series series = new Series();
					series.Tag = SeriesValue + keyValuePair.Key.full;
					series.ChartArea = chartAreaName;
					series.Color = settings.MagnitudeColor;
					series.ChartType = SeriesChartType.Spline;
					series.XAxisType = AxisType.Secondary;
					series.XValueType = ChartValueType.DateTime;

					foreach (EphemerisChartDataPoint dp in dataPoints)
						series.Points.Add(new DataPoint(JDToDateTime(dp.JD).ToOADate(), dp.Value));

					chart1.Series.Add(series);

					if (settings.PerihelionLineChecked && settings.IsMultipleMode)
						AddPerihelionLine(chart1, keyValuePair.Key, settings.PerihelionLineColor, xMinValue, xMaxValue, yMinValue, yMaxValue, chartAreaName, SeriesPerihelion);
				}
			}

			if (settings.PerihelionLineChecked && !settings.IsMultipleMode)
				AddPerihelionLine(chart1, settings.SelectedComet, settings.PerihelionLineColor, xMinValue, xMaxValue, yMinValue, yMaxValue, chartAreaName, SeriesPerihelion);

			decimal jdNow = DateTime.UtcNow.JD();

			if (settings.NowLineChecked && xMinValue < jdNow && xMaxValue > jdNow)
			{
				Series s = new Series();
				s.Tag = SeriesNow;
				s.ChartArea = chartAreaName;
				s.Color = settings.NowLineColor;
				s.ChartType = SeriesChartType.Line;
				s.XAxisType = AxisType.Secondary;
				s.XValueType = ChartValueType.DateTime;
				s.Points.Add(new DataPoint(JDToDateTime(jdNow).ToOADate(), yMinValue));
				s.Points.Add(new DataPoint(JDToDateTime(jdNow).ToOADate(), yMaxValue));
				chart1.Series.Add(s);
			}

			chart1.Titles.Clear();

			if (!settings.IsMultipleMode)
			{
				Title title = new Title(settings.SelectedComet.ToString());
				title.Font = new Font("Tahoma", 11.25F);
				title.ForeColor = settings.TextColor;
				chart1.Titles.Add(title);
			}
		}

		private static void AddPerihelionLine(Chart chart1, Comet comet, Color color, decimal xMin, decimal xMax, double yMin, double yMax, string chartAreaName, string seriesName)
		{
			decimal t = comet.Tn;
			decimal periodDays = Convert.ToDecimal(comet.P) * 365.25m;

			while (t - periodDays > xMin)
				t -= periodDays;

			while (t < xMax)
			{
				Series s = new Series();
				s.Tag = seriesName + comet.full;
				s.ChartArea = chartAreaName;
				s.Color = color;
				s.ChartType = SeriesChartType.Line;
				s.XAxisType = AxisType.Secondary;
				s.XValueType = ChartValueType.DateTime;
				s.Points.Add(new DataPoint(JDToDateTime(t).ToOADate(), yMin));
				s.Points.Add(new DataPoint(JDToDateTime(t).ToOADate(), yMax));

				chart1.Series.Add(s);
				t += periodDays;
			}
		}

		private static Dictionary<Comet, IEnumerable<EphemerisChartDataPoint>> GetChartDataPointDictionary(Dictionary<Comet, List<Ephemeris>> ephemerisDictinary, GraphSettings.ChartType type)
		{
			var retval = new Dictionary<Comet, IEnumerable<EphemerisChartDataPoint>>();

			foreach (var keyValuePair in ephemerisDictinary)
			{
				IEnumerable<EphemerisChartDataPoint> values = null;

				switch (type)
				{
					case GraphSettings.ChartType.Magnitude:
						values = from val in keyValuePair.Value select new EphemerisChartDataPoint(val.JD, val.Magnitude);
						break;
					case GraphSettings.ChartType.SunDistance:
						values = from val in keyValuePair.Value select new EphemerisChartDataPoint(val.JD, val.SunDist);
						break;
					case GraphSettings.ChartType.EarthDistance:
						values = from val in keyValuePair.Value select new EphemerisChartDataPoint(val.JD, val.EarthDist);
						break;
				}

				retval.Add(keyValuePair.Key, values);
			}

			return retval;
		}

		#endregion

		#region Calculate

		private static double[] CometAlt(decimal T, double q, double e, double w, double N, double i, double g, double k, decimal jd, double latitude, double longitude)
		{
			// Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
			double[] sun_xyz = SunXyz(jd);
			double[] cmt_xyz = CometXyz(T, q, e, w, N, i, jd);
			double[] cmt_xyz2 = CometXyz2(T, q, e, w, N, i, jd);

			var s1 = String.Join(Environment.NewLine, cmt_xyz) + Environment.NewLine + Environment.NewLine + String.Join(Environment.NewLine, cmt_xyz2);

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

		private static double[] SunAlt(decimal jd, double latitude, double longitude)
		{
			// return alt, az, time angle, ra, dec, ecl. long. and lat=0, illum=1, 0, dist, brightness 
			double[] sdat = SunXyz(jd);
			double ecl = 23.439291111 - 3.563E-7 * (double)(jd - 2451543.5m);
			double xe = sdat[0];
			double ye = sdat[1] * Cosd(ecl);
			double ze = sdat[1] * Sind(ecl);
			double ra = Rev(Atan2d(ye, xe));
			double dec = Atan2d(ze, Math.Sqrt(xe * xe + ye * ye));
			double[] topo = RaDecToAltAz(ra, dec, jd, latitude, longitude);
			return new double[] { topo[0], topo[1], topo[2], ra, dec, sdat[4], 0, 1, 0, sdat[3], -26.74 };
		}

		private static double[] SunXyz(decimal jd)
		{
			// return x,y,z ecliptic coordinates, distance, true longitude
			// days counted from 1999 Dec 31.0 UT
			double d = (double)(jd - 2451543.5m);
			double w = 282.9404 + 4.70935E-5 * d;
			double e = 0.016709 - 1.151E-9 * d;
			double M = Rev(356.0470 + 0.9856002585 * d);
			double E = M + e * RAD2DEG * Sind(M) * (1.0 + e * Cosd(M));
			double xv = Cosd(E) - e;
			double yv = Math.Sqrt(1.0 - e * e) * Sind(E);
			double v = Atan2d(yv, xv);      // true anomaly
			double r = Math.Sqrt(xv * xv + yv * yv);    // distance
			double lonsun = Rev(v + w); // true longitude
			double xs = r * Cosd(lonsun);       // rectangular coordinates, zs = 0 for sun 
			double ys = r * Sind(lonsun);
			return new double[] { xs, ys, 0, r, lonsun, 0 };
		}

		private static double[] CometXyz(decimal T, double q, double e, double w, double N, double i, decimal jd)
		{
			// heliocentric xyz for comet (cn is index to comets)
			// based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
			// returns heliocentric x, y, z, distance, longitude and latitude of object
			decimal d = jd - 2451543.5m;
			decimal Tj = T;  // get julian day of perihelion time
			double v, r;
			if (e > 0.99)
			{
				// treat as near parabolic (approx. method valid inside orbit of Pluto)
				double k = 0.01720209895;   // Gaussian gravitational constant
				double a = 0.75 * (double)(jd - Tj) * k * Math.Sqrt((1 + e) / (q * q * q));
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
			{
				// treat as elliptic
				double a = q / (1.0 - e);
				double P = 365.2568984 * Math.Sqrt(a * a * a);  // period in days
				double M = 360.0 * (double)(jd - Tj) / P;   // mean anomaly
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
				v = Rev(Atan2d(yv, xv));        // true anomaly
				r = Math.Sqrt(xv * xv + yv * yv);   // distance
			}

			// from here common for all orbits
			N = N + 3.82394E-5 * (double)d;
			//w  ->  why not precess this value?
			double xh = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double yh = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double zh = r * (Sind(v + w) * Sind(i));
			double lonecl = Atan2d(yh, xh);
			double latecl = Atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
			return new double[] { xh, yh, zh, r, lonecl, latecl };
		}

		private static double[] CometXyz2(decimal T, double q, double e, double w, double N, double i, decimal jd)
		{
			if (e < 1.0)
			{
				return CometXyzEllip(T, q, e, w, N, i, jd);
			}
			else if (e > 1.0)
			{
				return CometXyzHyper(T, q, e, w, N, i, jd);
			}
			else
			{
				return CometXyzPara(T, q, e, w, N, i, jd);
			}
		}

		private static double[] CometXyzEllip(decimal T, double q, double e, double w, double N, double i, decimal jd)
		{
			// heliocentric xyz for comet (cn is index to comets)
			// based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
			// returns heliocentric x, y, z, distance, longitude and latitude of object
			decimal d = jd - 2451543.5m;

			double a = q / (1.0 - e); // semimajor axis
			double M = 0.01720209895 * (double)(jd - T) / (Math.Sqrt(a) * a);

			M = M % (2 * Math.PI);  // Mean Anomaly [0...2pi]
			if (M < 0.0) M += 2.0 * Math.PI;

			double E = M + 0.85 * e * Math.Sign(Math.Sin(M));
			int escape = 0;
			for (; ; )
			{
				double Ep = E;
				double f2 = e * Math.Sin(E);
				double f = E - f2 - M;
				double f1 = 1.0 - e * Math.Cos(E);
				E += (-5.0 * f) / (f1 + Math.Sign(f1) * Math.Sqrt(Math.Abs(16.0 * f1 * f1 - 20.0 * f * f2)));
				if (Math.Abs(E - Ep) < EPSILON) break;
				if (++escape > 10) break;
			}

			//double xv = a * (Math.Cos(E) - e);
			//double yv = a * Math.Sqrt(1.0 - e * e) * Math.Sin(E);
			//double v = Rev(Math.Atan2(yv, xv));        // true anomaly
			//double r = Math.Sqrt(xv * xv + yv * yv);   // distance

			double xv = a * (Cosd(E * RAD2DEG) - e);
			double yv = a * Math.Sqrt(1.0 - e * e) * Sind(E * RAD2DEG);
			double v = Rev(Math.Atan2(yv * DEG2RAD, xv * DEG2RAD));        // true anomaly
			v *= RAD2DEG;
			double r = Math.Sqrt(xv * xv + yv * yv);   // distance

			// from here common for all orbits
			N = N + 3.82394E-5 * (double)d;
			//w  ->  why not precess this value?
			double xh = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double yh = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double zh = r * (Sind(v + w) * Sind(i));
			double lonecl = Atan2d(yh, xh);
			double latecl = Atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
			return new double[] { xh, yh, zh, r, lonecl, latecl };
		}

		private static double[] CometXyzPara(decimal T, double q, double e, double w, double N, double i, decimal jd)
		{
			double w1 = 3.649116245E-2 * (double)(jd - T) / (q * Math.Sqrt(q));
			double s1 = 0.0;

			for (; ; )
			{
				double s0 = s1;
				s1 = (2.0 * s0 * s0 * s0 + w1) / (3.0 * (s0 * s0 + 1.0));
				if (Math.Abs(s1 - s0) < EPSILON) break;
			}

			double s = s1;
			double v = 2.0 * Math.Atan(s);
			double r = q * (1.0 + s * s);

			decimal d = jd - 2451543.5m;

			// from here common for all orbits
			N = N + 3.82394E-5 * (double)d;
			v *= RAD2DEG;
			//w  ->  why not precess this value?
			double xh = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double yh = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double zh = r * (Sind(v + w) * Sind(i));
			double lonecl = Atan2d(yh, xh);
			double latecl = Atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
			return new double[] { xh, yh, zh, r, lonecl, latecl };
		}

		private static double[] CometXyzHyper(decimal T, double q, double e, double w, double N, double i, decimal jd)
		{
			decimal d = jd - 2451543.5m;



			double da = q / Math.Abs(1.0 - e);
			double n0 = 0.01720209895 / (da * Math.Sqrt(da));
			double m = n0 * (double)(jd - T);
			double U = 0.5;
			for (; ; )
			{
				double U0 = U;
				U = (2 * U0 * (e - U0 * (1 - m - Math.Log(Math.Abs(U0))))) / (e * (U0 * U0 + 1) - 2 * U0);
				if (Math.Abs(U0 - U) < EPSILON) break;
			}

			double num = Math.Sqrt(e * e - 1) * (U * U - 1) / (2 * U);
			double den = e - (U * U + 1) / (2 * U);
			double v = Math.Atan2(num, den);
			double r = da * ((e * (U * U + 1) / (2 * U)) - 1);

			v *= RAD2DEG;

			// from here common for all orbits
			N = N + 3.82394E-5 * (double)d;
			//w  ->  why not precess this value?
			double xh = r * (Cosd(N) * Cosd(v + w) - Sind(N) * Sind(v + w) * Cosd(i));
			double yh = r * (Sind(N) * Cosd(v + w) + Cosd(N) * Sind(v + w) * Cosd(i));
			double zh = r * (Sind(v + w) * Sind(i));
			double lonecl = Atan2d(yh, xh);
			double latecl = Atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));
			return new double[] { xh, yh, zh, r, lonecl, latecl };
		}

		private static double[] RaDecDist(double[] cmt, double[] sun, decimal jd)
		{
			// radecr returns ra, dec and earth distance
			// obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
			// (note Sun coords are really Earth heliocentric coordinates with reverse signs)
			// Equatorial geocentric co-ordinates
			double xg = cmt[0] + sun[0];
			double yg = cmt[1] + sun[1];
			double zg = cmt[2];
			// Obliquity of Ecliptic
			double obl = 23.439291111 - 3.563E-7 * (double)(jd - 2451543.5m);
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

		private static double[] RaDecToAltAz(double ra, double dec, decimal jday, double latitude, double longitude)
		{
			// Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
			// TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
			double TH0 = 280.46061837 + 360.98564736629 * (double)(jday - 2451545.0m);
			double H = Rev(TH0 + longitude - ra);
			double alt = Asind(Sind(latitude) * Sind(dec) + Cosd(latitude) * Cosd(dec) * Cosd(H));
			double az = Atan2d(Sind(H), (Cosd(H) * Sind(latitude) - Tand(dec) * Cosd(latitude)));
			return new double[] { alt, Rev(az + 180.0), H };
		}

		private static double[] Separation(double ra1, double ra2, double dec1, double dec2)
		{
			// ra, dec may also be long, lat, but PA is relative to the chosen coordinate system
			double d = Acosd(Sind(dec1) * Sind(dec2) + Cosd(dec1) * Cosd(dec2) * Cosd(ra1 - ra2));      // (Meeus 17.1)
			if (d < 0.1) d = Math.Sqrt(Sqr(Rev2(ra1 - ra2) * Cosd((dec1 + dec2) / 2)) + Sqr(dec1 - dec2));  // (17.2)
			double pa = Atan2d(Sind(ra1 - ra2), Cosd(dec2) * Tand(dec1) - Sind(dec2) * Cosd(ra1 - ra2));        // angle
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

			if (seconds >= 60)
			{
				minutes += 1;
				seconds -= 60;
			}

			if (minutes >= 60)
			{
				hours += 1;
				minutes -= 60;
			}

			if (hours >= 24)
			{
				hours -= 24;
			}

			string hmsstr = t < 0 ? "-" : "";
			hmsstr = (hours < 10 ? "0" : "") + hours;
			hmsstr += (minutes < 10 ? "h0" : "h") + minutes;
			hmsstr += (seconds < 10 ? "m0" : "m") + seconds;

			return hmsstr;
		}

		public static string AngleString(double a, bool circle, bool arcmin)
		{
			// Return angle as degrees:minutes. 'circle' is true for range between 0 and 360 
			// and false for -90 to +90, if 'arcmin' use deg and arcmin symbols
			double ar = Math.Round(a * 60) / 60;
			double deg = Math.Abs(ar);
			double min = Math.Round(60.0 * (deg - Math.Floor(deg)));

			if (min >= 60)
			{
				deg += 1;
				min = 0;
			}

			string anglestr = "";

			if (circle)
				anglestr += Math.Floor(deg) < 100 ? "0" : "";
			else
				anglestr += ar < 0 ? "-" : "+";

			anglestr += (Math.Floor(deg) < 10 ? "0" : "") + Math.Floor(deg);

			if (arcmin)
				anglestr += (min < 10 ? "°0" : "°") + min + "'";
			else
				anglestr += (min < 10 ? ":0" : ":") + min;

			return anglestr;
		}

		#endregion

		#region Date methods

		public static decimal JD(DateTime dt)
		{
			DateTime d = dt.ToUniversalTime();
			return JD(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
		}

		private static decimal JD(int year, int month, int day, int hour, int min, int sec)
		{
			decimal j = JD0(year, month, day, 0);
			decimal h = (hour + (min / 60.0m) + (sec / 3600.0m)) / 24;
			return j + h;
		}

		public static decimal JD0(int year, int month, int day, decimal hour)
		{
			// The Julian date at 0 hours(*) UT at Greenwich
			// (*) or actual UT time if day comprises time as fraction
			decimal y = year;
			decimal m = month;
			decimal d = day + hour / 10000.0m;
			if (m < 3) { m += 12; y -= 1; }
			decimal a = Math.Floor(y / 100);
			decimal b = 2 - a + Math.Floor(a / 4);
			decimal j = Math.Floor(365.25m * (y + 4716)) + Math.Floor(30.6001m * (m + 1)) + d + b - 1524.5m;
			return j;
		}

		public static DateTime JDToDateTime(decimal jd0)
		{
			decimal jd = jd0 + 0.5m;
			decimal a = Math.Floor(jd);

			if (a >= 2299160.5m)
			{
				decimal t = Math.Floor((a - 1867216.25m) / 36524.25m);
				a += t - Math.Floor(t / 4.0m) + 1.0m;
			}

			decimal b = Math.Floor(a) + 1524;
			decimal c = Math.Floor((b - 122.1m) / 365.25m);
			decimal k = Math.Floor(365.25m * c);
			decimal e = Math.Floor((b - k) / 30.6001m);

			decimal day0 = b - k - Math.Floor(30.6001m * e) + (jd - Math.Floor(jd));

			int month = (int)Math.Floor(e - (e >= 13.5m ? 13 : 1) + 0.5m);
			int year = (int)Math.Floor(c - (month > 2 ? 4716 : 4715) + 0.5m);
			int day = (int)Math.Floor(day0);

			decimal hour0 = ((day0 - day) * 24.0m) + 0.0000001m; // fix for 23:59:59
			int hour = (int)Math.Floor(hour0);

			decimal min = (hour0 - hour) * 60.0m;
			int minute = (int)Math.Floor(min);
			int second = (int)((min - minute) * 60.0m);

			//int dw = (int)(Math.Floor(jd + 1.5) - 7 * Math.Floor((jd + 1.5) / 7));
			//return new int[] { year, month, day, dw, hour, minute, second };

			return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
		}

		public static DateTime? JDToDateTimeSafe(decimal? jd0)
		{
			return jd0 != null ? JDToDateTime(jd0.Value) : (DateTime?)null;
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
			double a = Rev(angle);
			return a >= 180 ? a - 360.0 : a;
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
