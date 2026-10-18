using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using ImportType = Comets.Core.Managers.ElementTypesManager.Type;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;
using DataTypeEnum = Comets.Core.Managers.FilterManager.DataTypeEnum;
using CometEphemeris = Comets.Core.Ephemeris;

namespace SpecVectors
{
	/// <summary>
	/// Writes docs/spec/vectors. Run through run.sh, which pins the clock and the time zone;
	/// see README.md.
	/// </summary>
	public static class Program
	{
		/// <summary>The "now" every date-dependent vector is computed for. run.sh fakes the clock to this.</summary>
		public static readonly DateTime FakeNow = new DateTime(2026, 10, 18, 12, 0, 0, DateTimeKind.Utc);

		static string InputDir;   // full or trimmed source files (Soft00Cmt.txt ... ELEMENTS.COMET)
		static string OutDir;     // docs/spec/vectors
		static string ImportDir => Path.Combine(OutDir, "import");

		static readonly JsonSerializerOptions Json = new JsonSerializerOptions
		{
			WriteIndented = true,
			NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			DefaultIgnoreCondition = JsonIgnoreCondition.Never,
		};

		public static int Main(string[] args)
		{
			for (int a = 0; a < args.Length - 1; a++)
			{
				if (args[a] == "--input") InputDir = args[a + 1];
				if (args[a] == "--out") OutDir = args[a + 1];
			}

			if (InputDir == null || OutDir == null)
			{
				Console.Error.WriteLine("usage: SpecVectors --input <dir> --out <docs/spec/vectors>");
				return 2;
			}

			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

			if (Math.Abs((DateTime.UtcNow - FakeNow).TotalSeconds) > 1)
			{
				Console.Error.WriteLine($"Clock is {DateTime.UtcNow:o}, expected {FakeNow:o}. Run through run.sh (libfaketime).");
				return 3;
			}

			if (TimeZoneInfo.Local.BaseUtcOffset != TimeSpan.Zero || TimeZoneInfo.Local.SupportsDaylightSavingTime)
			{
				Console.Error.WriteLine("Local time zone must be UTC (TZ=UTC). Run through run.sh.");
				return 3;
			}

			// The harness never reads settings.ini: default settings, observer at lat 0, lon 0.
			CommonManager.Settings = new Settings();

			Directory.CreateDirectory(OutDir);
			Directory.CreateDirectory(ImportDir);

			PrepareImportInputs();

			CometCollection sample = ImportManager.ImportMain(new CometCollection(), ImportType.MPC, Path.Combine(ImportDir, "sample.txt"));
			CometCollection all = ImportManager.ImportMain(new CometCollection(), ImportType.MPC, Path.Combine(ImportDir, "full", "Soft00Cmt.txt"));

			WriteMeta();
			WriteTime();
			WriteNames(all);
			WriteElements();
			WriteFormatting();
			WritePrecession();
			WriteComets(sample);
			WriteEphemeris(sample);
			WriteImports();
			WriteExports(sample);
			WriteImportMerge(sample, all);
			WriteFilters(all);
			WriteSorting(sample);
			WriteSelection(sample);
			WriteEphemerisText(sample);
			WriteGraphs(sample);
			WriteSimulationSteps();
			WritePlanets();
			WriteOrbitViewerPositions(sample);
			WriteOrbitSampling(sample);
			WriteEngineComparison(sample);

			Console.WriteLine("Vectors written to " + Path.GetFullPath(OutDir));
			return 0;
		}

		#region Helpers

		static void Save(string name, object value)
		{
			string path = Path.Combine(OutDir, name);
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			File.WriteAllText(path, JsonSerializer.Serialize(value, Json) + "\n", new UTF8Encoding(false));
			Console.WriteLine("  " + name);
		}

		static void SaveText(string name, string text)
		{
			string path = Path.Combine(OutDir, name);
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			// Stored with LF line endings; the application writes CRLF (see 03-element-formats.md).
			File.WriteAllText(path, text.Replace("\r\n", "\n"), new UTF8Encoding(false));
			Console.WriteLine("  " + name);
		}

		static string Iso(DateTime dt) => dt.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);

		static decimal JD(DateTime utc) => EphemerisManager.JD(utc);

		static DateTime Utc(int y, int m, int d, int h = 0, int min = 0, int s = 0) => new DateTime(y, m, d, h, min, s, DateTimeKind.Utc);

		/// <summary>Static (import-time) fields of a comet. Independent of the clock and observer.</summary>
		static Dictionary<string, object> StaticFields(Comet c)
		{
			return new Dictionary<string, object>
			{
				["full"] = c.full,
				["id"] = c.id,
				["name"] = c.name,
				["fragment"] = c.fragment,
				["Ty"] = c.Ty,
				["Tm"] = c.Tm,
				["Td"] = c.Td,
				["Th"] = c.Th,
				["T"] = c.T,
				["q"] = c.q,
				["e"] = c.e,
				["w"] = c.w,
				["N"] = c.N,
				["i"] = c.i,
				["g"] = c.g,
				["k"] = c.k,
				["P"] = c.P,
				["a"] = c.a,
				["Q"] = c.Q,
				["n"] = c.n,
				["sortkey"] = c.sortkey,
			};
		}

		/// <summary>Static fields plus the clock- and observer-dependent ones (at FakeNow, lat 0, lon 0).</summary>
		static Dictionary<string, object> AllFields(Comet c)
		{
			var d = StaticFields(c);
			d["isPeriodic"] = c.IsPeriodic;
			d["Tn"] = c.Tn;
			try { d["TnDate"] = Iso(EphemerisManager.JDToDateTime(c.Tn)); }
			catch (ArgumentOutOfRangeException) { d["TnDate"] = null; } // year < 1: not representable on the desktop
			d["PerihEarthDist"] = c.PerihEarthDist;
			d["PerihMag"] = c.PerihMag;
			d["CurrentSunDist"] = c.CurrentSunDist;
			d["CurrentEarthDist"] = c.CurrentEarthDist;
			d["CurrentMag"] = c.CurrentMag;
			return d;
		}

		static Dictionary<string, object> EphemerisFields(CometEphemeris ep)
		{
			return new Dictionary<string, object>
			{
				["JD"] = ep.JD,
				["RA"] = ep.RA,
				["Dec"] = ep.Dec,
				["Alt"] = ep.Alt,
				["Az"] = ep.Az,
				["EcLon"] = ep.EcLon,
				["EcLat"] = ep.EcLat,
				["Elongation"] = ep.Elongation,
				["PositionAngle"] = ep.PositionAngle,
				["SunDist"] = ep.SunDist,
				["EarthDist"] = ep.EarthDist,
				["Magnitude"] = ep.Magnitude,
			};
		}

		static Comet Find(CometCollection c, string full) => c.Single(x => x.full == full);

		#endregion

		#region Import inputs

		/// <summary>Comets of the real Soft00Cmt snapshot that make up the sample collection.</summary>
		static readonly string[] SampleNames =
		{
			"1P/Halley", "2P/Encke", "12P/Pons-Brooks", "29P/Schwassmann-Wachmann", "67P/Churyumov-Gerasimenko",
			"73P-BT/Schwassmann-Wachmann", "96P-B/Machholz", "141P-A/Machholz", "3D-A/Biela", "18D/Perrine-Mrkos",
			"1I/`Oumuamua", "2I/Borisov", "3I/ATLAS",
			"C/1942 EA (Vaisala)", "C/1947 X1-B (Southern comet)", "C/1995 O1 (Hale-Bopp)", "C/1999 F1 (Catalina)",
			"C/2013 A1 (SidingSpring)", "C/2014 UN271 (Bernardinelli-Bernstein)", "C/2019 E3 (ATLAS)",
			"C/2022 E3 (ZTF)", "C/2023 A3 (Tsuchinshan-ATLAS)", "C/2023 R1-B", "C/2024 G3 (ATLAS)", "C/2025 A6 (Lemmon)",
			"P/1998 VS24 (LINEAR)", "P/2004 V5-A (LINEAR-Hill)", "A/2018 W3", "A/2025 S2",
		};

		/// <summary>
		/// Synthetic comets appended to the sample to reach cases the current MPC file lacks:
		/// e exactly 1, negative year, X/ type, D/ fragment with digits, a 19th-century fragment.
		/// The elements are made up; only the shapes matter.
		/// </summary>
		static CometCollection SyntheticComets()
		{
			Comet Make(string full, int y, int m, int d, int h, double q, double e, double w, double N, double i, double g, double k)
			{
				Comet c = new Comet { full = full, Ty = y, Tm = m, Td = d, Th = h, q = q, e = e, w = w, N = N, i = i, g = g, k = k };
				CometManager.GetIdNameFromFull(full, out string id, out string name, out string fragment);
				c.id = id; c.name = name; c.fragment = fragment;
				c.T = EphemerisManager.JD0(y, m, d, h);
				c.P = CometManager.GetPeriod(q, e);
				c.a = CometManager.GetSemimajorAxis(q, e);
				c.n = CometManager.GetMeanMotion(e, c.P);
				c.Q = CometManager.GetAphelionDistance(e, c.a);
				c.sortkey = CometManager.GetSortkey(id, fragment);
				return c;
			}

			return new CometCollection
			{
				Make("C/2026 T99 (Parabola Test)", 2026, 11, 3, 2500, 0.812345, 1.0, 45.1234, 123.4567, 67.8901, 9.5, 4.0),
				Make("C/-146 P1", -146, 8, 10, 5000, 0.43, 1.0, 120.0, 210.0, 55.0, 4.0, 4.0),
				Make("X/1106 C1", 1106, 2, 1, 0, 0.18, 1.0, 300.0, 80.0, 140.0, 3.0, 4.0),
				Make("D/1993 F2-A (Shoemaker-Levy 9)", 1994, 3, 18, 5000, 5.381, 0.2161, 354.89, 220.54, 6.003, 14.0, 4.0),
				Make("C/1860 D1-A (Liais)", 1860, 2, 17, 2500, 1.1968, 1.0, 145.03, 324.82, 79.42, 7.0, 4.0),
			};
		}

		/// <summary>
		/// Copies the source files into vectors/import: a full copy of Soft00Cmt (for name
		/// parsing and filters) and a trimmed copy of every format (for import parsing), plus
		/// sample.txt and the synthetic Comet for Windows file. Trimming keeps the header,
		/// the first 30 records and the last 3, so detection, which reads the end of the file,
		/// sees real data. Re-running on already trimmed files changes nothing.
		/// </summary>
		static void PrepareImportInputs()
		{
			Directory.CreateDirectory(Path.Combine(ImportDir, "full"));

			// A fresh download directory has the full file at the top; the committed inputs keep it
			// in full/ next to the trimmed copy.
			string soft00 = Path.Combine(InputDir, "full", "Soft00Cmt.txt");
			if (!File.Exists(soft00))
				soft00 = Path.Combine(InputDir, "Soft00Cmt.txt");
			string full00 = Path.Combine(ImportDir, "full", "Soft00Cmt.txt");
			if (Path.GetFullPath(soft00) != Path.GetFullPath(full00))
				File.WriteAllText(full00, NormalizeNewlines(File.ReadAllText(soft00)));

			// (file, header lines, lines per record)
			var formats = new (string file, int header, int perRecord)[]
			{
				("Soft00Cmt.txt", 0, 1), ("Soft01Cmt.txt", 0, 1), ("Soft02Cmt.txt", 0, 1), ("Soft03Cmt.txt", 0, 2),
				("Soft04Cmt.txt", 1, 1), ("Soft05Cmt.txt", 1, 1), ("Soft06Cmt.txt", 0, 1), ("Soft07Cmt.txt", 15, 1),
				("Soft08Cmt.txt", 2, 2), ("Soft09Cmt.txt", 0, 1), ("Soft10Cmt.txt", 0, 2), ("Soft11Cmt.txt", 5, 1),
				("Soft12Cmt.txt", 0, 1), ("Soft13Cmt.txt", 0, 1), ("Soft14Cmt.txt", 23, 1), ("Soft15Cmt.txt", 0, 1),
				("Soft16Cmt.txt", 0, 1), ("ELEMENTS.COMET", 2, 1),
			};

			foreach (var f in formats)
			{
				string src = Path.Combine(InputDir, f.file);
				string dst = Path.Combine(ImportDir, f.file);
				string[] lines = SplitLines(File.ReadAllText(File.Exists(src) ? src : dst));

				string[] header = lines.Take(f.header).ToArray();
				string[] body = lines.Skip(f.header).ToArray();
				int records = body.Length / f.perRecord;
				const int first = 30, last = 3;

				if (records > first + last)
				{
					body = body.Take(first * f.perRecord)
						.Concat(body.Skip((records - last) * f.perRecord))
						.ToArray();
				}

				File.WriteAllText(dst, String.Join("\n", header.Concat(body)) + "\n", new UTF8Encoding(false));
			}

			// sample.txt: the sample comets' original Soft00Cmt lines, then the synthetic comets
			// written by the desktop MPC exporter.
			string[] all00 = SplitLines(File.ReadAllText(full00));
			var picked = new List<string>();
			foreach (string name in SampleNames)
			{
				string line = all00.FirstOrDefault(l => l.Length > 102 && l.Substring(102, Math.Min(55, l.Length - 102)).Trim() == name);
				if (line == null)
					throw new InvalidOperationException("Sample comet not in Soft00Cmt: " + name);
				picked.Add(line);
			}

			string synthetic = ExportManager.ExportMain(ImportType.MPC, SyntheticComets());
			picked.AddRange(SplitLines(synthetic));
			File.WriteAllText(Path.Combine(ImportDir, "sample.txt"), String.Join("\n", picked) + "\n", new UTF8Encoding(false));

			// Comet for Windows: no exporter exists, and no real file is available, so this one is
			// synthetic, laid out exactly as ImportCometForWindows reads it (13-line records from
			// line 6, see 03-element-formats.md).
			var cfw = new StringBuilder();
			cfw.Append("; Comet for Windows elements file - SYNTHETIC test data\n");
			cfw.Append("[File]\n");
			cfw.Append("Version=1\n");
			cfw.Append("Count=3\n");
			cfw.Append("Equinox=2000.0\n");
			cfw.Append("\n");
			foreach (var (full, date, q, e, w, N, i, mag) in new[]
			{
				("C/1995 O1 (Hale-Bopp)", "1997 03 29.0284", "0.925995", "0.994895", "130.7313", "281.8097", "89.7335", "-2.0 10.0"),
				("2P/Encke", "2027 02 10.2276", "0.338603", "0.847319", "187.2873", "334.0197", "11.3480", "14.4 10.0"),
				("C/2023 A3 (Tsuchinshan-ATLAS)", "2024 09 27.7372", "0.391423", "1.000102", "308.4925", "21.5596", "139.1112", "4.5 10.0"),
			})
			{
				cfw.Append($"Name={full}\n");
				cfw.Append("Type=Comet\n");
				cfw.Append("Reference=MPC\n");
				cfw.Append($"Perihelion={date}\n");
				cfw.Append($"q={q}\n");
				cfw.Append($"e={e}\n");
				cfw.Append($"Peri={w}\n");
				cfw.Append($"Node={N}\n");
				cfw.Append($"Incl={i}\n");
				cfw.Append("Equinox=2000.0\n");
				cfw.Append("Epoch=\n");
				cfw.Append($"Magnitude={mag}\n");
				cfw.Append("\n");
			}
			File.WriteAllText(Path.Combine(ImportDir, "CometForWindows.dat"), cfw.ToString(), new UTF8Encoding(false));
		}

		static string NormalizeNewlines(string text) => text.Replace("\r\n", "\n").Replace("\r", "\n");

		/// <summary>Same splitting as File.ReadAllLines: no trailing empty line for a final newline.</summary>
		static string[] SplitLines(string text)
		{
			text = NormalizeNewlines(text);
			if (text.EndsWith("\n"))
				text = text.Substring(0, text.Length - 1);
			return text.Length == 0 ? new string[0] : text.Split('\n');
		}

		#endregion

		#region Meta

		static void WriteMeta()
		{
			Save("meta.json", new
			{
				description = "Test vectors for the Comets web specification, generated from the desktop sources by tools/spec-vectors.",
				fakeNowUtc = Iso(FakeNow),
				fakeNowJD = JD(FakeNow),
				timeZone = "UTC",
				observer = new { name = "Home", latitude = 0.0, longitude = 0.0 },
				mpcSnapshot = "Soft*Cmt.txt and ELEMENTS.COMET downloaded 2026-10-18",
				lineEndings = "Text files are stored with LF; the application writes CRLF.",
			});
		}

		#endregion

		#region Time

		static void WriteTime()
		{
			var jd0 = new List<object>();
			foreach (var (y, m, d, h) in new[]
			{
				(2000, 1, 1, 5000), (2000, 1, 1, 0), (1999, 12, 31, 0), (1997, 3, 29, 284), (2026, 10, 18, 5000),
				(1700, 1, 1, 0), (2300, 1, 1, 0), (1582, 10, 15, 0), (1582, 10, 4, 0), (1600, 2, 29, 9999),
				(2024, 2, 29, 1234), (-146, 8, 10, 5000), (1106, 2, 1, 0), (2061, 7, 28, 7500), (1, 1, 1, 0),
			})
			{
				jd0.Add(new { year = y, month = m, day = d, hour = h, jd = EphemerisManager.JD0(y, m, d, h) });
			}

			var jd = new List<object>();
			foreach (DateTime dt in new[]
			{
				Utc(2000, 1, 1, 12), Utc(2026, 10, 18, 12), Utc(1986, 2, 9, 15, 30, 45), Utc(1700, 1, 1),
				Utc(2299, 12, 31, 23, 59, 59), Utc(2024, 2, 29, 6, 7, 8), new DateTime(2026, 10, 18, 12, 0, 0, 999, DateTimeKind.Utc),
			})
			{
				jd.Add(new { utc = Iso(dt) + (dt.Millisecond != 0 ? $" (+{dt.Millisecond} ms)" : ""), jd = EphemerisManager.JD(dt) });
			}

			var toDate = new List<object>();
			foreach (decimal v in new[]
			{
				2451545.0m, 2451544.5m, 2461332.0m, 2461331.999999m, 2461331.4999999m, 2460000.123456m,
				2299160.5m, 2299161.0m, 2342972.5m, 2561118.5m, 2446470.8977m, 2461297.4762m, 2433282.5m,
			})
			{
				toDate.Add(new { jd = v, utc = Iso(EphemerisManager.JDToDateTime(v)) });
			}

			Save("time.json", new
			{
				note = "jd0: EphemerisManager.JD0(year, month, day, hour) where hour is the 4-digit fraction of day (0..9999, units of 1e-4 day). jd: JD of a UTC instant, milliseconds dropped. jdToDate: JD back to a UTC instant (whole seconds, truncated).",
				jd0,
				jd,
				jdToDate = toDate,
				minimumDate = Iso(EphemerisManager.MinimumDateTime),
				maximumDate = Iso(EphemerisManager.MaximumDateTime),
			});
		}

		#endregion

		#region Names

		static void WriteNames(CometCollection all)
		{
			string[] examples00 =
			{
				"3D", "3D-B/Biela", "3D-A Biela", "C/-146 P1", "C/1014 C1", "C/1860 D1-A (Liais)", "C/1860 D1-A Liais",
				"D/1993 F2-P2 (Shoemaker-Levy 9)", "D/1993 F2-P1 Shoemaker-Levy 9", "1P/Halley", "73P-BT/Schwassmann-Wachmann",
				"1I/`Oumuamua", "C/2023 R1-B", "A/2018 W3", "P/2004 V5-A (LINEAR-Hill)", "C/1942 EA (Vaisala)",
				"C/1947 X1-B (Southern comet)", "C/2014 UN271 (Bernardinelli-Bernstein)", "282P", "P/2019 LD2 (ATLAS)",
				"C/2020 F3 (NEOWISE)", "141P-A/Machholz", "C/2025 K1-C (ATLAS)", "X/1106 C1", "C/1956 R1 (Arend-Roland)",
			};

			string[] examples02 =
			{
				"P/PANSTARRS (470P)", "P/PANSTARRS 2 (470P-A)", "471P-A", "P/Ikeya-Murakami (332P)", "P/Ikeya-Murakami 2 (332P-A)",
				"Hale-Bopp (C/-1995 O1)", "Hale-Bopp 2 (C/-1995 O1-A)", "Hale-Bopp (C/1995 O1)", "Hale-Bopp 2 (C/1995 O1-A)",
				"A/-2019 G3", "A/-2019 G3-A", "A/2019 G3", "A/2019 G3-A", "PANSTARRS (470P)", "PANSTARRS (470P-A1)",
				" (471P)", " (471P-A1)", "Vaisala (C/1942 EA)", "P/Halley (1P)", "C/2023 R1-B", "P/Borisov (2I)",
			};

			object Parse00(string s)
			{
				try
				{
					CometManager.GetIdNameFromFull(s, out string id, out string name, out string fragment);
					return new { input = s, id, name, fragment, full = CometManager.GetFullFromIdName(id, name, fragment) };
				}
				catch (Exception ex) { return new { input = s, error = ex.Message }; }
			}

			object Parse02(string s)
			{
				try
				{
					CometManager.GetIdNameFromFull2(s, out string id, out string name, out string fragment);
					return new { input = s, id, name, fragment, full = CometManager.GetFullFromIdName(id, name, fragment) };
				}
				catch (Exception ex) { return new { input = s, error = ex.Message }; }
			}

			object Key(string id, string fragment)
			{
				try { return new { id, fragment, sortkey = CometManager.GetSortkey(id, fragment) }; }
				catch (Exception ex) { return new { id, fragment, error = ex.GetType().Name }; }
			}

			// The spec's parsers must agree with the desktop regexes; this throws if they do not.
			var names02 = new List<string>(examples02);
			foreach (string f in new[] { "Soft02Cmt.txt", "Soft08Cmt.txt" })
			{
				string path = File.Exists(Path.Combine(InputDir, f)) ? Path.Combine(InputDir, f) : Path.Combine(ImportDir, f);
				string[] lines = SplitLines(File.ReadAllText(path));
				if (f == "Soft02Cmt.txt")
					names02.AddRange(lines.Where(l => l.Length >= 42).Select(l => l.Substring(0, 42).Trim()));
				else
					names02.AddRange(lines.Skip(2).Where((l, ix) => ix % 2 == 0));
			}
			var (verified, timeouts) = NameParsing.Verify(examples00.Concat(all.Select(c => c.full)), names02);
			Console.WriteLine($"  name parsers verified on {verified} inputs ({timeouts} desktop regex timeouts skipped)");

			Save("names.json", new
			{
				note = "pattern00 = GetIdNameFromFull (MPC-style 'id-fragment/name' or 'id (name)'); pattern02 = GetIdNameFromFull2 ('name (id-fragment)'). full = GetFullFromIdName(id, name, fragment). The 'mpcSoft00' list holds every comet of the Soft00Cmt snapshot.",
				pattern00 = examples00.Select(Parse00).ToList(),
				pattern02 = examples02.Select(Parse02).ToList(),
				sortkeys = new[]
				{
					("1P", ""), ("3D", "A"), ("5D", "B1"), ("240P", ""), ("C/-146 P1", ""), ("C/-43 K1", ""), ("C/240 V1", ""),
					("C/2000 A1", "B2"), ("1I", ""), ("C/1942 EA", ""), ("C/2014 UN271", ""), ("73P", "BT"), ("A/2018 W3", ""),
					("P/2004 V5", "A"), ("X/1106 C1", ""),
				}.Select(t => Key(t.Item1, t.Item2)).ToList(),
				isPeriodic = new[] { "1P", "73P", "3D", "1I", "C/1995 O1", "P/2004 V5", "A/2018 W3", "3P-A", "" }
					.Select(id => new { id, isPeriodic = new Comet { id = id }.IsPeriodic }).ToList(),
				mpcSoft00 = all.Select(c => new { c.full, c.id, c.name, c.fragment, c.sortkey, c.IsPeriodic }).ToList(),
			});
		}

		#endregion

		#region Elements

		static void WriteElements()
		{
			var cases = new List<object>();
			foreach (var (q, e) in new[] { (0.570945, 0.96803), (0.338603, 0.847319), (1.0, 0.0), (0.391423, 1.000102), (1.356507, 6.139884), (0.8, 1.0), (10.959316, 1.004054), (5.82477, 0.999655), (5.381, 0.2161) })
			{
				double P = CometManager.GetPeriod(q, e);
				double a = CometManager.GetSemimajorAxis(q, e);
				// A dictionary, not an anonymous type: System.Text.Json rejects members 'q' and 'Q' side by side.
				cases.Add(new Dictionary<string, object> { ["q"] = q, ["e"] = e, ["P"] = P, ["a"] = a, ["n"] = CometManager.GetMeanMotion(e, P), ["Q"] = CometManager.GetAphelionDistance(e, a) });
			}

			Save("elements.json", new { note = "Derived elements: P = GetPeriod(q,e), a = GetSemimajorAxis(q,e), n = GetMeanMotion(e,P), Q = GetAphelionDistance(e,a).", cases });
		}

		#endregion

		#region Formatting

		static void WriteFormatting()
		{
			double[] hours = { 0, 5.123456, 12.999999, 23.99999, 23.999999999, 1.0 / 60.0, 6.5, 18.2583333333, 0.0041666, -3.25 };
			double[] angles = { 0, 45.5, -0.5, -12.991667, 89.9999, -89.9999, 359.99, 100.25, 9.999, 0.0083, 123.456, -0.0083 };

			Save("formatting.json", new
			{
				note = "hms = HMSString(hours) (RA is passed as degrees/15). angle = AngleString(value, circle, arcmin). Math.Round in these functions is round-half-to-even.",
				hms = hours.Select(h => new { hours = h, text = EphemerisManager.HMSString(h) }).ToList(),
				angle = angles.SelectMany(a => new[]
				{
					new { value = a, circle = false, arcmin = true, text = EphemerisManager.AngleString(a, false, true) },
					new { value = a, circle = true, arcmin = true, text = EphemerisManager.AngleString(a, true, true) },
					new { value = a, circle = false, arcmin = false, text = EphemerisManager.AngleString(a, false, false) },
				}).ToList(),
			});
		}

		#endregion

		#region Precession

		static void WritePrecession()
		{
			var cases = new List<object>();
			foreach (var (mjd, i, N, w) in new[]
			{
				(39875.0, 162.19053, 59.09895, 112.24143), (60363.0, 11.33676, 334.01714, 187.28820),
				(33281.0, 30.0, 100.0, 200.0), (51544.5, 10.0, 20.0, 30.0), (15020.0, 89.0, 0.5, 359.5), (70000.0, 0.01, 45.0, 90.0),
			})
			{
				double i2 = i, N2 = N, w2 = w;
				EphemerisManager.PrecessElementsToJ2000(mjd, ref i2, ref N2, ref w2);
				cases.Add(new { epochMjd = mjd, i, N, w, iJ2000 = i2, NJ2000 = N2, wJ2000 = w2 });
			}

			Save("precession.json", new { note = "PrecessElementsToJ2000(epochMjd, i, N, w): angular elements from the equinox of epoch to J2000 (Lieske 1977). Used by the NASA importer.", cases });
		}

		#endregion

		#region Comets

		static void WriteComets(CometCollection sample)
		{
			Save("comets.json", new
			{
				note = "The sample collection (import/sample.txt), in database order (sortkey). Tn and the Perih*/Current* values are for fakeNow and an observer at latitude 0, longitude 0.",
				comets = sample.Select(AllFields).ToList(),
			});
		}

		#endregion

		#region Ephemeris

		static void WriteEphemeris(CometCollection sample)
		{
			var dates = new[] { Utc(2026, 10, 18, 12), Utc(2024, 10, 12, 18, 30), Utc(1986, 2, 9, 0), Utc(1997, 4, 1, 2), Utc(1700, 1, 1), Utc(2299, 12, 31, 23, 59, 59), Utc(2061, 7, 28, 6) };
			var observers = new[] { (name: "Null Island", lat: 0.0, lon: 0.0), (name: "Zagreb", lat: 45.815, lon: 15.9819), (name: "Sydney", lat: -33.8688, lon: 151.2093), (name: "Reykjavik", lat: 64.1466, lon: -21.9426) };

			var cases = new List<object>();
			foreach (Comet c in sample)
			{
				foreach (DateTime dt in dates)
				{
					foreach (var o in observers)
					{
						// Every comet at every date for the first observer; the others only at FakeNow,
						// which is enough to pin down the topocentric part.
						if (o.name != "Null Island" && dt != FakeNow)
							continue;

						CometEphemeris ep = EphemerisManager.GetEphemeris(c.T, c.q, c.e, c.w, c.N, c.i, c.g, c.k, JD(dt), o.lat, o.lon);
						cases.Add(new
						{
							comet = c.full,
							utc = Iso(dt),
							observer = new { o.name, latitude = o.lat, longitude = o.lon },
							ephemeris = EphemerisFields(ep),
						});
					}
				}
			}

			Save("ephemeris.json", new
			{
				note = "EphemerisManager.GetEphemeris(T, q, e, w, N, i, g, k, JD, latitude, longitude). Elements are in comets.json. Dec, EcLat, Alt in [-90, 90]; RA, EcLon, Az, PositionAngle in [0, 360).",
				cases,
			});
		}

		#endregion

		#region Imports

		static void WriteImports()
		{
			string[] files =
			{
				"Soft00Cmt.txt", "Soft01Cmt.txt", "Soft02Cmt.txt", "Soft03Cmt.txt", "Soft04Cmt.txt", "Soft05Cmt.txt", "Soft06Cmt.txt",
				"Soft07Cmt.txt", "Soft08Cmt.txt", "Soft09Cmt.txt", "Soft10Cmt.txt", "Soft11Cmt.txt", "Soft12Cmt.txt", "Soft13Cmt.txt",
				"Soft14Cmt.txt", "Soft15Cmt.txt", "Soft16Cmt.txt", "ELEMENTS.COMET", "CometForWindows.dat", "sample.txt",
			};

			foreach (string f in files)
				SaveImportExpectation(Path.Combine(ImportDir, f), "import/" + f);

			// Detection edge cases.
			string empty = Path.Combine(ImportDir, "edge-empty.txt");
			File.WriteAllText(empty, "");
			string garbage = Path.Combine(ImportDir, "edge-unknown.txt");
			// Kept short on purpose: a long line of letters with no designation in it sends the desktop
			// pattern 02 into exponential backtracking (see NameParsing.cs), and the desktop hangs.
			File.WriteAllText(garbage, "garbage\n0 0 0\n");

			Save("import-detection.json", new
			{
				note = "GetImportType for edge cases. For every real input, the detected type is in import-expected/<file>.json.",
				cases = new object[]
				{
					new { file = "(none)", type = ImportManager.GetImportType(null).ToString() },
					new { file = "import/edge-empty.txt", type = ImportManager.GetImportType(empty).ToString() },
					new { file = "import/edge-unknown.txt", type = ImportManager.GetImportType(garbage).ToString() },
				},
			});
		}

		static void SaveImportExpectation(string path, string label)
		{
			ImportType type = ImportManager.GetImportType(path);
			int count = type < ImportType.NoFileSelected ? ImportManager.GetNumberOfComets(path, type) : 0;
			CometCollection comets = type < ImportType.NoFileSelected
				? ImportManager.ImportMain(new CometCollection(), type, path)
				: new CometCollection();

			Save("import-expected/" + Path.GetFileName(path) + ".json", new
			{
				file = label,
				detectedType = type.ToString(),
				typeName = type < ImportType.NoFileSelected ? ElementTypesManager.TypeName[(int)type] : null,
				estimatedCount = count,
				note = "Comets in database order (stable sort by sortkey) after ImportMain into an empty database.",
				comets = comets.Select(StaticFields).ToList(),
			});
		}

		#endregion

		#region Exports

		static readonly ImportType[] ExportTypes =
		{
			ImportType.MPC, ImportType.SkyMap, ImportType.Guide, ImportType.xephem, ImportType.HomePlanet, ImportType.MyStars,
			ImportType.TheSky, ImportType.StarryNight, ImportType.DeepSpace, ImportType.PCTCS, ImportType.EarthCenteredUniverse,
			ImportType.DanceOfThePlanets, ImportType.MegaStarV4, ImportType.SkyChartIII, ImportType.VoyagerII, ImportType.SkyTools,
			ImportType.Autostar, ImportType.SpaceEngine, ImportType.Celestia,
		};

		/// <summary>The one deliberate correction to desktop export output (see 03-element-formats.md).</summary>
		static string CanonicalizeExport(ImportType type, string text)
		{
			if (type == ImportType.DanceOfThePlanets)
				text = text.Replace("Comet      peri(au)   e         iř       ęř       wř     peridate     name",
									"Comet      peri(au)   e         ix       jx       wx     peridate     name");
			return text;
		}

		static void WriteExports(CometCollection sample)
		{
			var index = new List<object>();
			foreach (ImportType t in ExportTypes)
			{
				string text = CanonicalizeExport(t, ExportManager.ExportMain(t, sample));
				string file = $"export/{(int)t:00}-{t}.txt";
				SaveText(file, text);
				index.Add(new { type = t.ToString(), typeName = ElementTypesManager.TypeName[(int)t], file });

				// Round trip: what the importer makes of the exported file.
				string tmp = Path.Combine(Path.GetTempPath(), $"specvectors-{t}.txt");
				File.WriteAllText(tmp, text);
				SaveImportExpectation(tmp, "export/" + Path.GetFileName(file));
				File.Move(Path.Combine(OutDir, "import-expected", Path.GetFileName(tmp) + ".json"),
						  Path.Combine(OutDir, "import-expected", "roundtrip-" + Path.GetFileName(file) + ".json"), true);
				File.Delete(tmp);
			}

			Save("export/index.json", new
			{
				note = "ExportMain(type, sample collection) at fakeNow. Input: import/sample.txt in database order. import-expected/roundtrip-<file>.json is the result of importing each file back.",
				exports = index,
			});
		}

		#endregion

		#region Import merge

		static void WriteImportMerge(CometCollection sample, CometCollection all)
		{
			// "Old" database: the sample. "New" file: the sample without two comets, with one comet's
			// q changed, and with one comet added.
			string[] sampleLines = SplitLines(File.ReadAllText(Path.Combine(ImportDir, "sample.txt")));
			string[] all00 = SplitLines(File.ReadAllText(Path.Combine(ImportDir, "full", "Soft00Cmt.txt")));

			var newLines = sampleLines
				.Where(l => !l.Contains("29P/Schwassmann-Wachmann") && !l.Contains("A/2018 W3"))
				.Select(l => l.Contains("2P/Encke") ? l.Substring(0, 30) + " 0.338700" + l.Substring(39) : l)
				.ToList();
			newLines.Add(all00.First(l => l.Contains("21P/Giacobini-Zinner")));

			string newFile = Path.Combine(ImportDir, "merge-new.txt");
			File.WriteAllText(newFile, String.Join("\n", newLines) + "\n", new UTF8Encoding(false));

			CometCollection merged = ImportManager.ImportMain(sample, ImportType.MPC, newFile);

			Save("import-merge.json", new
			{
				note = "ImportMain(old = sample collection, MPC, import/merge-new.txt). merge-new.txt drops 29P and A/2018 W3, changes 2P's q, and adds 21P. Comets only in the old database stay, marked NoChanges.",
				oldFile = "import/sample.txt",
				newFile = "import/merge-new.txt",
				result = merged.Select(c => new { c.full, importResult = c.ImportResult.ToString(), c.q }).ToList(),
			});
		}

		#endregion

		#region Filters

		static Filter F(PropertyEnum p, int compare, string text, bool isChecked = true)
		{
			DataTypeEnum type = p == PropertyEnum.full || p == PropertyEnum.name || p == PropertyEnum.id ? DataTypeEnum.String : DataTypeEnum.Double;
			return new Filter(p, type, isChecked, text, compare);
		}

		static void WriteFilters(CometCollection all)
		{
			string jd(DateTime dt) => JD(dt).ToString(CultureInfo.InvariantCulture);

			var cases = new (string title, Filter[] filters)[]
			{
				("full contains 'atlas'", new[] { F(PropertyEnum.full, 0, "atlas") }),
				("full contains 'halley, encke' (any of)", new[] { F(PropertyEnum.full, 0, "halley, encke") }),
				("full does not contain 'panstarrs,atlas,soho,linear'", new[] { F(PropertyEnum.full, 1, "panstarrs,atlas,soho,linear") }),
				("full starts with 'c/2025'", new[] { F(PropertyEnum.full, 2, "c/2025") }),
				("full ends with 'b'", new[] { F(PropertyEnum.full, 3, "b") }),
				("name contains 'machholz'", new[] { F(PropertyEnum.name, 0, "machholz") }),
				("id starts with 'p/'", new[] { F(PropertyEnum.id, 2, "p/") }),
				("unchecked filter is ignored", new[] { F(PropertyEnum.full, 0, "halley", false) }),
				("Tn equals 2026-10-18 (+/- 1 day)", new[] { F(PropertyEnum.Tn, 2, jd(Utc(2026, 10, 18))) }),
				("Tn in 2027", new[] { F(PropertyEnum.Tn, 0, jd(Utc(2027, 1, 1))), F(PropertyEnum.Tn, 1, jd(Utc(2028, 1, 1))) }),
				("q less than 0.5", new[] { F(PropertyEnum.q, 1, "0.5") }),
				("q equals 1 (+/- 0.1)", new[] { F(PropertyEnum.q, 2, "1") }),
				("PerihEarthDist less than 0.5", new[] { F(PropertyEnum.PerihEarthDist, 1, "0.5") }),
				("PerihMag less than 10", new[] { F(PropertyEnum.PerihMag, 1, "10") }),
				("CurrentSunDist less than 1.5", new[] { F(PropertyEnum.CurrentSunDist, 1, "1.5") }),
				("CurrentEarthDist less than 2", new[] { F(PropertyEnum.CurrentEarthDist, 1, "2") }),
				("CurrentMag less than 14", new[] { F(PropertyEnum.CurrentMag, 1, "14") }),
				("CurrentMag equals 15 (+/- 0.5)", new[] { F(PropertyEnum.CurrentMag, 2, "15") }),
				("P less than 6", new[] { F(PropertyEnum.P, 1, "6") }),
				("Q greater than 1000", new[] { F(PropertyEnum.Q, 0, "1000") }),
				("a greater than 100", new[] { F(PropertyEnum.a, 0, "100") }),
				("e greater than 1", new[] { F(PropertyEnum.e, 0, "1") }),
				("e equals 0.5 (+/- 0.01)", new[] { F(PropertyEnum.e, 2, "0.5") }),
				("i equals 90 (+/- 1)", new[] { F(PropertyEnum.i, 2, "90") }),
				("N greater than 350", new[] { F(PropertyEnum.N, 0, "350") }),
				("w less than 5", new[] { F(PropertyEnum.w, 1, "5") }),
				("combined: periodic-looking, bright, near", new[] { F(PropertyEnum.full, 1, "c/,a/"), F(PropertyEnum.CurrentMag, 1, "16"), F(PropertyEnum.CurrentEarthDist, 1, "3") }),
			};

			Save("filters.json", new
			{
				note = "FilterManager.ApplyFilters(Soft00Cmt snapshot in database order, filters) at fakeNow, observer lat 0 lon 0. compare: string 0=Contains 1=DoesNotContain 2=StartsWith 3=EndsWith; number 0=GreaterThan 1=LessThan 2=Equals (within EqualValueOffset). Tn filter values are JDs.",
				input = "import/full/Soft00Cmt.txt",
				cases = cases.Select(c => new
				{
					c.title,
					filters = c.filters.Select(f => new { property = f.Property.ToString(), dataType = f.DataType.ToString(), @checked = f.Checked, text = f.Text, compare = f.CompareIndex }).ToList(),
					result = FilterManager.ApplyFilters(all, new FilterCollection().Also(fc => fc.AddRange(c.filters))).Select(x => x.full).ToList(),
				}).ToList(),
			});
		}

		static T Also<T>(this T value, Action<T> action) { action(value); return value; }

		#endregion

		#region Sorting

		static void WriteSorting(CometCollection sample)
		{
			string[] props = { "sortkey", "name", "Tn", "q", "PerihEarthDist", "PerihMag", "CurrentSunDist", "CurrentEarthDist", "CurrentMag", "P", "Q", "a", "e", "i", "N", "w" };

			var cases = new List<object>();
			foreach (string p in props)
			{
				PropertyInfo prop = typeof(Comet).GetProperty(p);
				foreach (bool asc in new[] { true, false })
				{
					// Same as SortMenuControl.SortCollection: stable OrderBy / OrderByDescending
					// with the default comparer (invariant culture for strings).
					var sorted = asc ? sample.OrderBy(x => prop.GetValue(x)) : sample.OrderByDescending(x => prop.GetValue(x));
					cases.Add(new { property = p, ascending = asc, result = sorted.Select(x => x.full).ToList() });
				}
			}

			Save("sorting.json", new
			{
				note = "Stable sort of the sample collection (database order) by each sort property, at fakeNow. Strings compare with invariant-culture (ICU root) collation.",
				input = sample.Select(x => x.full).ToList(),
				cases,
			});
		}

		#endregion

		#region Selection

		static void WriteSelection(CometCollection sample)
		{
			Save("selection.json", new
			{
				note = "CometManager.GetCometBySelectionType(sample, type) at fakeNow; the first of equal candidates in collection order wins.",
				cases = Enum.GetValues(typeof(CometManager.SelectionType)).Cast<CometManager.SelectionType>()
					.Select(t => new { type = t.ToString(), comet = CometManager.GetCometBySelectionType(sample, t).full }).ToList(),
			});
		}

		#endregion

		#region Ephemeris text

		static EphemerisSettings NewEphemerisSettings(CometCollection comets, Comet selected, bool multiple, DateTime start, DateTime stop, decimal interval, Location location)
		{
			var s = new EphemerisSettings
			{
				Comets = comets,
				SelectedComet = selected,
				IsMultipleMode = multiple,
				Start = start,
				Stop = stop,
				Interval = interval,
				Location = location,
				Ephemerides = new Dictionary<Comet, List<CometEphemeris>>(),
				MinMagnitudeChecked = false,
				MinMagnitudeValue = 12.0,
			};
			return s;
		}

		static string RunEphemeris(EphemerisSettings s)
		{
			var progress = new Progress<int>(_ => { });
			EphemerisManager.CalculateEphemerisAsync(s, progress, CancellationToken.None).GetAwaiter().GetResult();
			return EphemerisManager.GenerateEphemerisAsync(s, progress, CancellationToken.None).GetAwaiter().GetResult();
		}

		static void WriteEphemerisText(CometCollection sample)
		{
			var zagreb = new Location { Name = "Zagreb", Latitude = 45.815, Longitude = 15.9819 };
			var cases = new List<object>();

			void Add(string id, string description, EphemerisSettings s, string tz)
			{
				object settingsDto = new
				{
					comets = s.IsMultipleMode ? "all sample comets (database order)" : s.SelectedComet.full,
					multiple = s.IsMultipleMode,
					startUtc = Iso(s.Start),
					stopUtc = Iso(s.Stop),
					intervalDays = s.Interval,
					location = new { s.Location.Name, s.Location.Latitude, s.Location.Longitude },
					localTime = s.LocalTime,
					timeZone = tz,
					columns = new { s.RA, s.Dec, s.Alt, s.Az, s.EcLon, s.EcLat, s.Elongation, s.HelioDist, s.GeoDist, s.Magnitude },
					requirements = new
					{
						maxSunDist = s.MaxSunDistChecked ? s.MaxSunDistValue : null,
						maxEarthDist = s.MaxEarthDistChecked ? s.MaxEarthDistValue : null,
						minMagnitude = s.MinMagnitudeChecked ? s.MinMagnitudeValue : null,
					},
					title = s.ToString(),
				};

				string text = RunEphemeris(s);
				SaveText($"ephemeris-text/{id}.txt", text);
				cases.Add(new { id, description, file = $"ephemeris-text/{id}.txt", settings = settingsDto });
			}

			// 1. Single comet, Universal Time, every column on.
			var s1 = NewEphemerisSettings(sample, Find(sample, "C/2023 A3 (Tsuchinshan-ATLAS)"), false, Utc(2024, 10, 1), Utc(2024, 10, 20), 1.0m, CommonManager.Settings.Location);
			s1.LocalTime = false; s1.EcLon = true; s1.EcLat = true;
			Add("01-single-ut-all-columns", "Single comet, UT, all columns, 1 day step.", s1, "UTC");

			// 2. All comets, default columns, magnitude requirement.
			var s2 = NewEphemerisSettings(sample, null, true, Utc(2026, 10, 1), Utc(2026, 11, 1), 5.0m, CommonManager.Settings.Location);
			s2.LocalTime = false; s2.MinMagnitudeChecked = true; s2.MinMagnitudeValue = 15.0;
			Add("02-multiple-default-columns-mag15", "All sample comets, default columns, only rows with magnitude <= 15; comets without rows are left out.", s2, "UTC");

			// 3. Hour/minute step, a subset of columns, distance requirements.
			decimal step3 = 0 + (2 + (30 / 60.0m)) / 24.0m;
			var s3 = NewEphemerisSettings(sample, Find(sample, "12P/Pons-Brooks"), false, Utc(2024, 4, 21), Utc(2024, 4, 22), step3, zagreb);
			s3.LocalTime = false; s3.RA = false; s3.Dec = false; s3.Alt = true; s3.Az = true; s3.EcLon = true; s3.EcLat = true; s3.Elongation = false; s3.Magnitude = false;
			s3.MaxSunDistChecked = true; s3.MaxSunDistValue = 1.0; s3.MaxEarthDistChecked = true; s3.MaxEarthDistValue = 2.0;
			Add("03-single-ut-2h30m-subset-zagreb", "12P from Zagreb, 2 h 30 min step, Alt/Az/ecliptic + distances, r <= 1 and d <= 2.", s3, "UTC");

			// 4. Local time, Europe/Zagreb, across the end of daylight saving time.
			Environment.SetEnvironmentVariable("TZ", "Europe/Zagreb");
			TimeZoneInfo.ClearCachedData();
			try
			{
				var s4 = NewEphemerisSettings(sample, Find(sample, "C/2025 A6 (Lemmon)"), false, Utc(2026, 10, 24, 18), Utc(2026, 10, 25, 12), 0.25m, zagreb);
				s4.LocalTime = true;
				Add("04-single-localtime-zagreb-dst-end", "Local time in Europe/Zagreb across the 2026-10-25 change from CEST to CET, 6 h step.", s4, "Europe/Zagreb");
			}
			finally
			{
				Environment.SetEnvironmentVariable("TZ", "UTC");
				TimeZoneInfo.ClearCachedData();
			}

			// 5. Hyperbolic and parabolic objects.
			var s5 = NewEphemerisSettings(sample, Find(sample, "C/2026 T99 (Parabola Test)"), false, Utc(2026, 10, 1), Utc(2026, 12, 1), 10.0m, CommonManager.Settings.Location);
			s5.LocalTime = false;
			Add("05-single-parabolic", "Synthetic parabolic comet (e = 1), UT, default columns.", s5, "UTC");

			Save("ephemeris-text/index.json", new
			{
				note = "EphemerisManager.CalculateEphemerisAsync + GenerateEphemerisAsync. Files use LF here; the app saves CRLF. 'title' is the result tab title.",
				cases,
			});
		}

		#endregion

		#region Graphs

		/// <summary>FormGraphSettings: the sampling interval picked from the timespan.</summary>
		static decimal GraphInterval(DateTime start, DateTime stop)
		{
			decimal totalDays = JD(stop) - JD(start);
			if (totalDays <= 100) return totalDays / 100.0m;
			if (totalDays < 365) return 1;
			if (totalDays < 10 * 365.25m) return 2;
			if (totalDays < 50 * 365.25m) return 5;
			if (totalDays < 100 * 365.25m) return 15;
			if (totalDays < 200 * 365.25m) return 30;
			if (totalDays < 300 * 365.25m) return 40;
			return 0;
		}

		static void WriteGraphs(CometCollection sample)
		{
			var cases = new List<object>();

			void Add(string id, string description, GraphSettings s)
			{
				s.Interval = GraphInterval(s.Start, s.Stop);
				s.Ephemerides = new Dictionary<Comet, List<CometEphemeris>>();
				s.Location = CommonManager.Settings.Location;
				var progress = new Progress<int>(_ => { });
				EphemerisManager.CalculateEphemerisAsync(s, progress, CancellationToken.None).GetAwaiter().GetResult();

				var chart = new Chart();
				EphemerisManager.GenerateGraph(s, chart, progress);
				ChartArea area = chart.ChartAreas.Single();

				decimal FromOA(double oa) => JD(DateTime.SpecifyKind(DateTime.FromOADate(oa), DateTimeKind.Utc));

				cases.Add(new
				{
					id,
					description,
					settings = new
					{
						chartType = s.GraphChartType.ToString(),
						comets = s.IsMultipleMode ? "all sample comets (database order)" : s.SelectedComet.full,
						multiple = s.IsMultipleMode,
						startUtc = Iso(s.Start),
						stopUtc = Iso(s.Stop),
						intervalDays = s.Interval,
						minValue = s.MinGraphValueChecked ? s.MinGraphValue : null,
						maxValue = s.MaxGraphValueChecked ? s.MaxGraphValue : null,
						perihelionLine = s.PerihelionLineChecked,
						nowLine = s.NowLineChecked,
						title = s.ToString(),
					},
					chart = new
					{
						title = chart.Titles.Select(t => t.Text).SingleOrDefault(),
						xAxis = new { title = area.AxisX2.Title, minJD = FromOA(area.AxisX2.Minimum), maxJD = FromOA(area.AxisX2.Maximum), labelFormat = area.AxisX2.LabelStyle.Format },
						yAxis = new { title = area.AxisY.Title, minimum = area.AxisY.Minimum, maximum = area.AxisY.Maximum, interval = area.AxisY.Interval, reversed = area.AxisY.IsReversed, labelFormat = area.AxisY.LabelStyle.Format },
						series = chart.Series.Select(x => new
						{
							tag = x.Tag as string,
							kind = x.ChartType == SeriesChartType.Spline ? "value" : (((string)x.Tag).StartsWith(EphemerisManager.SeriesNow) ? "now" : "perihelion"),
							points = x.Points.Select(p => new[] { (double)FromOA(p.XValue), p.YValues[0] }).ToList(),
						}).ToList(),
					},
				});
			}

			Add("01-magnitude-single", "Magnitude of C/2023 A3 over 2024-06-01..2025-01-01.", new GraphSettings
			{
				Comets = sample, SelectedComet = Find(sample, "C/2023 A3 (Tsuchinshan-ATLAS)"), IsMultipleMode = false,
				Start = Utc(2024, 6, 1), Stop = Utc(2025, 1, 1), GraphChartType = GraphSettings.ChartType.Magnitude,
				MaxGraphValueChecked = false, MaxGraphValue = null,
			});

			Add("02-magnitude-multiple-max12", "Magnitude of all sample comets over 2026-01-01..2027-01-01, maximum 12 (series entirely fainter are left out), now line inside the range.", new GraphSettings
			{
				Comets = sample, SelectedComet = null, IsMultipleMode = true,
				Start = Utc(2026, 1, 1), Stop = Utc(2027, 1, 1), GraphChartType = GraphSettings.ChartType.Magnitude,
			});

			Add("03-sundistance-single-12P", "Sun distance of 12P over 2010..2030 with explicit minimum 0 and maximum 20 (periodic perihelion lines).", new GraphSettings
			{
				Comets = sample, SelectedComet = Find(sample, "12P/Pons-Brooks"), IsMultipleMode = false,
				Start = Utc(2010, 1, 1), Stop = Utc(2030, 1, 1), GraphChartType = GraphSettings.ChartType.SunDistance,
				MinGraphValueChecked = true, MinGraphValue = 0, MaxGraphValueChecked = true, MaxGraphValue = 20,
			});

			Add("04-earthdistance-short-span", "Earth distance of 2P over 2 days (long date labels, interval = span/100).", new GraphSettings
			{
				Comets = sample, SelectedComet = Find(sample, "2P/Encke"), IsMultipleMode = false,
				Start = Utc(2026, 10, 17), Stop = Utc(2026, 10, 19), GraphChartType = GraphSettings.ChartType.EarthDistance,
				MaxGraphValueChecked = false, MaxGraphValue = null,
			});

			Add("05-earthdistance-multiple-default-max", "Earth distance of all sample comets over 2026-06-01..2026-12-01 with no maximum: multiple mode defaults the maximum to 2 AU.", new GraphSettings
			{
				Comets = sample, SelectedComet = null, IsMultipleMode = true,
				Start = Utc(2026, 6, 1), Stop = Utc(2026, 12, 1), GraphChartType = GraphSettings.ChartType.EarthDistance,
				MaxGraphValueChecked = false, MaxGraphValue = null,
			});

			var intervals = new[] { (Utc(2026, 1, 1), Utc(2026, 1, 2)), (Utc(2026, 1, 1), Utc(2026, 4, 11)), (Utc(2026, 1, 1), Utc(2026, 4, 12)), (Utc(2026, 1, 1), Utc(2027, 1, 1)), (Utc(2026, 1, 1), Utc(2035, 1, 1)), (Utc(2000, 1, 1), Utc(2049, 1, 1)), (Utc(1950, 1, 1), Utc(2049, 1, 1)), (Utc(1850, 1, 1), Utc(2049, 1, 1)), (Utc(1750, 1, 1), Utc(2049, 1, 1)) }
				.Select(t => new { startUtc = Iso(t.Item1), stopUtc = Iso(t.Item2), intervalDays = GraphInterval(t.Item1, t.Item2) }).ToList();

			Save("graph.json", new
			{
				note = "EphemerisManager.GenerateGraph recorded through a stub chart. x values are JD. Series kinds: value (spline), perihelion (vertical line), now (vertical line at fakeNow).",
				samplingIntervals = intervals,
				cases,
			});
		}

		#endregion

		#region Simulation steps

		static void WriteSimulationSteps()
		{
			var spans = new (string label, ATimeSpan span)[]
			{
				("1 Hour", new ATimeSpan(0, 0, 0, 1, 0, 0)), ("1 Day", new ATimeSpan(0, 0, 1, 0, 0, 0)), ("10 Days", new ATimeSpan(0, 0, 10, 0, 0, 0)),
				("1 Month", new ATimeSpan(0, 1, 0, 0, 0, 0)), ("3 Months", new ATimeSpan(0, 3, 0, 0, 0, 0)), ("1 Year", new ATimeSpan(1, 0, 0, 0, 0, 0)),
			};
			var starts = new[] { Utc(2026, 1, 31, 12), Utc(2024, 2, 29), Utc(2026, 10, 18, 23, 30), Utc(1582, 10, 3) };

			var cases = new List<object>();
			foreach (DateTime start in starts)
			{
				foreach (var (label, span) in spans)
				{
					foreach (bool forward in new[] { true, false })
					{
						var atime = new ATime(start, 0.0);
						atime.ChangeDate(span, forward);
						cases.Add(new
						{
							startUtc = Iso(start),
							step = label,
							forward,
							resultUtc = $"{atime.Year:0000}-{atime.Month:00}-{atime.Day:00}T{atime.Hour:00}:{atime.Minute:00}:{atime.Second:00}Z",
						});
					}
				}
			}

			Save("simulation-steps.json", new
			{
				note = "Single-step (|< and >|) date changes: calendar arithmetic, years then months then days then hours. A month step from the 31st clamps to the last day of the month.",
				cases,
			});
		}

		#endregion

		#region Planets

		static readonly Canonical.Planet[] Planets = (Canonical.Planet[])Enum.GetValues(typeof(Canonical.Planet));

		static void WritePlanets()
		{
			var dates = new[] { Utc(1700, 1, 1), Utc(1800, 6, 15), Utc(1950, 1, 1), Utc(2000, 1, 1, 12), Utc(2026, 10, 18, 12), Utc(2061, 7, 28), Utc(2100, 1, 1), Utc(2299, 12, 31, 23, 59, 59) };

			var cases = new List<object>();
			foreach (DateTime dt in dates)
			{
				decimal jd = JD(dt);
				double d = (double)(jd - (decimal)Canonical.DayZeroJD);

				cases.Add(new
				{
					utc = Iso(dt),
					jd,
					d,
					planets = Planets.Select(p =>
					{
						var el = Canonical.GetElements(p, d);
						var (x, y, z) = Canonical.PlanetPosition(p, jd);
						var (dLon, dLat) = Canonical.Perturbations(p, d);
						return new
						{
							planet = p.ToString(),
							elements = new { el.N, el.i, el.w, el.a, el.e, el.M },
							perturbation = new { dLon, dLat },
							x, y, z,
							r = Math.Sqrt(x * x + y * y + z * z),
						};
					}).ToList(),
				});
			}

			Save("planets.json", new
			{
				note = "Canonical planet positions (04-astronomy.md): heliocentric ecliptic of date, AU. Earth = negated desktop Sun position; its 'elements' are the Sun's with w + 180 and are used only for drawing Earth's orbit.",
				cases,
			});
		}

		#endregion

		#region Orbit viewer positions

		static void WriteOrbitViewerPositions(CometCollection sample)
		{
			var dates = new[] { Utc(2026, 10, 18, 12), Utc(1986, 2, 9), Utc(2061, 7, 28, 6), Utc(1997, 4, 1, 2) };

			var cases = new List<object>();
			foreach (Comet c in sample)
			{
				foreach (DateTime dt in dates)
				{
					decimal jd = JD(dt);
					double[] p = Canonical.CometXyz(c.T, c.q, c.e, c.w, c.N, c.i, jd);
					cases.Add(new { comet = c.full, utc = Iso(dt), jd, x = p[0], y = p[1], z = p[2], r = p[3] });
				}
			}

			Save("orbit-viewer-positions.json", new
			{
				note = "Canonical heliocentric ecliptic-of-date comet positions used by the 3D view (AU). Identical to the positions behind ephemeris.json.",
				cases,
			});
		}

		#endregion

		#region Orbit sampling

		static void WriteOrbitSampling(CometCollection sample)
		{
			string[] names = { "2P/Encke", "1P/Halley", "C/1995 O1 (Hale-Bopp)", "C/1999 F1 (Catalina)", "3I/ATLAS", "C/2013 A1 (SidingSpring)", "C/2026 T99 (Parabola Test)" };

			var cases = new List<object>();
			foreach (string name in names)
			{
				Comet c = Find(sample, name);
				var ov = new OVComet(c);
				var orbit = new CometOrbit(ov);

				// CometOrbit rotates its plane samples by VectorConstant (orthonormal; the
				// precession part is the identity for J2000 elements). Undo it with the transpose
				// to recover the orbital-plane points the canonical sampler must produce.
				// Only a subset of indices is stored (first 5, every 25th, last 5): enough to pin the
				// sampler down without a multi-megabyte file.
				Matrix m = ov.VectorConstant;
				var pts = new List<double[]>();
				for (int k = 0; k < orbit.PointCount; k++)
				{
					if (!(k < 5 || k % 25 == 0 || k >= orbit.PointCount - 5))
						continue;
					Xyz v = orbit.GetAt(k);
					double x = m.A11 * v.X + m.A21 * v.Y + m.A31 * v.Z;
					double y = m.A12 * v.X + m.A22 * v.Y + m.A32 * v.Z;
					pts.Add(new[] { k, x, y });
				}

				cases.Add(new { comet = name, c.q, c.e, pointCount = orbit.PointCount, points = pts });
			}

			Save("orbit-sampling.json", new
			{
				note = "Orbit line samples in the orbital plane (x towards perihelion, y along the motion at perihelion), AU, as produced by the desktop adaptive sampler (OrbitDivisionCount 500, MaxOrbitAU 500). Each point is [index, x, y]; only indices < 5, multiples of 25 and the last 5 are listed. Within float rounding of the inverse rotation (~1e-12 AU).",
				cases,
			});
		}

		#endregion

		#region Engine comparison

		/// <summary>
		/// How far the canonical engine moves things relative to the desktop orbit viewer, which
		/// had its own engine. Feeds the decision record in 04-astronomy.md; not a test vector.
		/// </summary>
		static void WriteEngineComparison(CometCollection sample)
		{
			var planetStats = new List<object>();
			foreach (var p in Planets)
			{
				double maxDist = 0, maxLon = 0;
				DateTime worst = default;
				for (DateTime dt = Utc(1700, 1, 1); dt < Utc(2300, 1, 1); dt = dt.AddDays(10))
				{
					decimal jd = JD(dt);
					var (x, y, z) = Canonical.PlanetPosition(p, jd);
					Xyz o = Planet.GetPos((Comets.OrbitViewer.Object)(int)(p + 1), new ATime(dt, 0.0));
					double dist = Math.Sqrt(Sq(x - o.X) + Sq(y - o.Y) + Sq(z - o.Z));
					double dLon = Math.Abs(Wrap180(Math.Atan2(y, x) - Math.Atan2(o.Y, o.X)) * 180 / Math.PI);
					if (dist > maxDist) { maxDist = dist; worst = dt; }
					maxLon = Math.Max(maxLon, dLon);
				}
				planetStats.Add(new { planet = p.ToString(), maxDistanceAU = maxDist, maxHeliocentricLongitudeDeg = maxLon, worstUtc = Iso(worst) });
			}

			var cometStats = new List<object>();
			foreach (Comet c in sample.Where(c => c.Ty > 1700 && c.Ty < 2300))
			{
				var ov = new OVComet(c);
				double maxDist = 0, maxAngle = 0;
				for (DateTime dt = Utc(1990, 1, 1); dt < Utc(2060, 1, 1); dt = dt.AddDays(10))
				{
					decimal jd = JD(dt);
					double[] a = Canonical.CometXyz(c.T, c.q, c.e, c.w, c.N, c.i, jd);
					var atime = new ATime(dt, 0.0);
					Matrix toEcl = Matrix.RotateX(ATime.GetEp(atime.JD)).Mul(Matrix.PrecMatrix(Astro.JD2000, atime.JD));
					Xyz b = ov.GetPos(atime.JD).Rotate(toEcl);
					double dist = Math.Sqrt(Sq(a[0] - b.X) + Sq(a[1] - b.Y) + Sq(a[2] - b.Z));
					double r = Math.Sqrt(Sq(a[0]) + Sq(a[1]) + Sq(a[2]));
					maxDist = Math.Max(maxDist, dist);
					maxAngle = Math.Max(maxAngle, dist / r * 180 / Math.PI);
				}
				cometStats.Add(new { comet = c.full, maxDistanceAU = maxDist, maxAngleSeenFromSunDeg = maxAngle });
			}

			Save("engine-comparison.json", new
			{
				note = "Canonical engine vs the desktop orbit viewer's own engine. Planets 1700..2300, comets 1990..2060, 10-day steps. Not a test vector.",
				planets = planetStats,
				comets = cometStats,
			});
		}

		static double Sq(double x) => x * x;
		static double Wrap180(double rad) { while (rad > Math.PI) rad -= 2 * Math.PI; while (rad < -Math.PI) rad += 2 * Math.PI; return rad; }

		#endregion
	}
}
