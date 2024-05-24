using Comets.Core;
using Comets.Core.Extensions;
using System;
using System.IO;
using System.Linq;
using ImportType = Comets.Core.Managers.ElementTypesManager.Type;

namespace Comets.Core.Managers
{
	public static class ImportManager
	{
		#region Const

		// todo: remove?
		private static char[] TrimCharacters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/', ';' };

		#endregion

		#region GetImportType

		public static ImportType GetImportType(string filename)
		{
			if (filename == null)
				return ImportType.NoFileSelected;

			if (!File.Exists(filename))
				return ImportType.FileNotFound;

			if (new FileInfo(filename).Length == 0)
				return ImportType.EmptyFile;

			string[] lines = File.ReadAllLines(filename);

			string line0 = lines.ElementAtOrDefault(lines.Length - 2);
			string line1 = lines[lines.Length - 1];

			try
			{
				ParseMpc00(line1);
				return ImportType.MPC;
			}
			catch
			{
				//try next
			}

			try
			{
				ParseSkyMap01(line1);
				return ImportType.SkyMap;
			}
			catch
			{

			}

			try
			{
				ParseXephem03(line1);
				return ImportType.xephem;
			}
			catch
			{

			}

			try
			{
				ParseHomePlanet04(line1);
				return ImportType.HomePlanet;
			}
			catch
			{

			}

			try
			{
				ParseMyStars05(line1);
				return ImportType.MyStars;
			}
			catch
			{

			}

			try
			{
				ParseTheSky06(line1);
				return ImportType.TheSky;
			}
			catch
			{

			}

			try
			{
				ParseStarryNight07(line1);
				return ImportType.StarryNight;
			}
			catch
			{

			}

			try
			{
				ParseDeepSpace08(line0, line1);
				return ImportType.DeepSpace;
			}
			catch
			{

			}

			try
			{
				ParseGuide02(line1);
				return ImportType.Guide;
			}
			catch
			{

			}

			try
			{
				ParsePcTcs09(line1);
				return ImportType.PCTCS;
			}
			catch
			{

			}

			try
			{
				ParseEarthCenUniv10(line0, line1);
				return ImportType.EarthCenteredUniverse;
			}
			catch
			{

			}

			try
			{
				ParseDanceOfThePlanets11(line1);
				return ImportType.DanceOfThePlanets;
			}
			catch
			{

			}

			try
			{
				ParseMegaStar12(line1);
				return ImportType.MegaStarV4;
			}
			catch
			{

			}

			try
			{
				ParseSkyChart13(line1);
				return ImportType.SkyChartIII;
			}
			catch
			{

			}

			try
			{
				ParseVoyager14(line1);
				return ImportType.VoyagerII;
			}
			catch
			{

			}

			try
			{
				ParseSkyTools15(line1);
				return ImportType.SkyTools;
			}
			catch
			{

			}

			try
			{
				if (lines[1] == "[File]")
					return ImportType.CometForWindows;
			}
			catch
			{

			}

			try
			{
				ParseNasaComet(line1);
				return ImportType.NASA;
			}
			catch
			{

			}

			return ImportType.Unknown;
		}

		#endregion

		#region GetNumberOfComets

		public static int GetNumberOfComets(string filename, ImportType importType)
		{
			int lines = File.ReadLines(filename).Count();

			if (importType.In(ImportType.xephem, ImportType.DeepSpace, ImportType.EarthCenteredUniverse))
				lines /= 2;

			if (importType.In(ImportType.DeepSpace, ImportType.HomePlanet, ImportType.MyStars))
				--lines;

			if (importType == ImportType.StarryNight)
				lines -= 15;

			if (importType == ImportType.DanceOfThePlanets)
				lines -= 5;

			if (importType == ImportType.VoyagerII)
				lines -= 23;

			if (importType == ImportType.CometForWindows)
				lines /= 13;

			if (importType == ImportType.NASA)
				lines -= 2;

			return lines;
		}

		#endregion

		#region ImportMain

		public static CometCollection ImportMain(CometCollection collection, ImportType importType, string filename)
		{
			CometCollection oldCollection = new CometCollection(collection);
			CometCollection newCollection = ImportByType(importType, filename);

			if (newCollection.Count > 0)
			{
				if (oldCollection.Count > 0)
				{
					oldCollection.SetImportResult(CometManager.ImportResult.NoChanges);
					newCollection.SetImportResult(CometManager.ImportResult.NoChanges);

					foreach (Comet n in newCollection)
					{
						Comet o = oldCollection.FirstOrDefault(x => x.full == n.full);

						if (o != null)
						{
							oldCollection.Remove(o);

							if (!o.Equals(n))
								n.ImportResult = CometManager.ImportResult.Update;
						}
						else
						{
							n.ImportResult = CometManager.ImportResult.New;
						}

						oldCollection.Add(n, ignore: importType == ImportType.VoyagerII);
					}
				}
				else
				{
					newCollection.SetImportResult(CometManager.ImportResult.New);
					oldCollection = newCollection;
				}
			}

			oldCollection = new CometCollection(oldCollection.OrderBy(x => x.sortkey));

			return oldCollection;
		}

		#endregion

		#region ImportByType

		private static CometCollection ImportByType(ImportType importType, string filename)
		{
			CometCollection collection = new CometCollection();

			switch (importType)
			{
				case ImportType.MPC:
					ImportMpc00(filename, ref collection); break;

				case ImportType.SkyMap:
					ImportSkyMap01(filename, ref collection); break;

				case ImportType.Guide:
					ImportGuide02(filename, ref collection); break;

				case ImportType.xephem:
					ImportXephem03(filename, ref collection); break;

				case ImportType.HomePlanet:
					ImportHomePlanet04(filename, ref collection); break;

				case ImportType.MyStars:
					ImportMyStars05(filename, ref collection); break;

				case ImportType.TheSky:
					ImportTheSky06(filename, ref collection); break;

				case ImportType.StarryNight:
					ImportStarryNight07(filename, ref collection); break;

				case ImportType.DeepSpace:
					ImportDeepSpace08(filename, ref collection); break;

				case ImportType.PCTCS:
					ImportPcTcs09(filename, ref collection); break;

				case ImportType.EarthCenteredUniverse:
					ImportEarthCenUniv10(filename, ref collection); break;

				case ImportType.DanceOfThePlanets:
					ImportDanceOfThePlanets11(filename, ref collection); break;

				case ImportType.MegaStarV4:
					ImportMegaStar12(filename, ref collection); break;

				case ImportType.SkyChartIII:
					ImportSkyChart13(filename, ref collection); break;

				case ImportType.VoyagerII:
					ImportVoyager14(filename, ref collection); break;

				case ImportType.SkyTools:
					ImportSkyTools15(filename, ref collection); break;

				case ImportType.CometForWindows:
					ImportCometForWindows(filename, ref collection); break;

				case ImportType.NASA:
					ImportNasaComet(filename, ref collection); break;
			}

			return collection;
		}

		#endregion

		#region Import methods

		private static void ImportMpc00(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseMpc00(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseMpc00(string line)
		{
			Comet c = new Comet();

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
			c.full = line.Substring(102, 55).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportSkyMap01(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseSkyMap01(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseSkyMap01(string line)
		{
			Comet c = new Comet();

			string tempFull = line.Substring(0, 44).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string id, name, fragment;
			CometManager.GetIdNameFromFull(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(id, name, fragment);

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


			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportGuide02(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseGuide02(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseGuide02(string line)
		{
			Comet c = new Comet();

			string tempFull = line.Substring(0, 42).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string id, name, fragment;
			CometManager.GetIdNameFromFull2(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(id, name, fragment);

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

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportXephem03(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 1; i < lines.Length; i += 2)
			{
				try
				{
					Comet c = ParseXephem03(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseXephem03(string line)
		{
			// http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

			Comet c = new Comet();

			string[] parts = line.Split(',');

			c.full = parts[0].Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			if (parts[1] == "e")
			{
				c.i = Convert.ToDouble(parts[2]);
				c.N = Convert.ToDouble(parts[3]);
				c.w = Convert.ToDouble(parts[4]);
				double a = Convert.ToDouble(parts[5]); // semi-major axis
				decimal n = Convert.ToDecimal(parts[6]); // mean daily motion
				c.e = Convert.ToDouble(parts[7]);
				decimal M = Convert.ToDecimal(parts[8]); // mean anomaly, i.e., degrees from perihelion

				string[] date = parts[9].Split('/');
				int m = Convert.ToInt32(date[0]);
				int y = Convert.ToInt32(date[2]);
				string[] dh = date[1].Split('.');
				int d = Convert.ToInt32(dh[0]);
				int h = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

				c.g = Convert.ToDouble(parts[11].Substring(2, parts[11].Length - 2));
				c.k = Convert.ToDouble(parts[12]);

				c.q = Math.Round(a * (1 - c.e), 6);

				if (M == 0)
				{
					c.Tm = m;
					c.Td = d;
					c.Th = h;
					c.Ty = y;

					c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
				}
				else
				{
					decimal E = EphemerisManager.JD0(y, m, d, h); // epoch date, i.e., time of M
					decimal t = E - M / n;

					if (M >= 180)
						t += Convert.ToDecimal(CometManager.GetPeriod(c.q, c.e)) * 365.25m;

					DateTime dt = EphemerisManager.JDToDateTime(t);
					c.Ty = dt.Year;
					c.Tm = dt.Month;
					c.Td = dt.Day;
					c.Th = (int)Math.Round(((dt.Hour + (dt.Minute / 60.0) + (dt.Second / 3600.0)) / 24) * 10000);

					c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th); //recalculate
				}
			}
			else if (parts[1] == "p")
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
				c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			}
			else if (parts[1] == "h")
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

				c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Not xephem format");
			}

			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportHomePlanet04(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 1; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseHomePlanet04(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseHomePlanet04(string line)
		{
			Comet c = new Comet();

			string[] parts = line.Split(',');

			c.full = parts[0].Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

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

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportMyStars05(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 1; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseMyStars05(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseMyStars05(string line)
		{
			//
			// w zapravo nije w
			//

			Comet c = new Comet();

			decimal T;
			int h;
			string[] Th;

			string[] parts = line.Split('\t');

			c.full = parts[0].Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			Th = parts[1].Split('.');
			T = Convert.ToDecimal(Th[0]);
			h = Convert.ToInt32(Th[1].Trim().PadRight(4, '0'));
			c.T = T + 2400000.5m;

			DateTime dd = EphemerisManager.JDToDateTime(c.T);
			c.Ty = dd.Year;
			c.Tm = dd.Month;
			c.Td = dd.Day;
			c.Th = h;

			c.w = Convert.ToDouble(parts[2]);
			c.e = Convert.ToDouble(parts[3]);
			c.q = Convert.ToDouble(parts[4]);
			c.i = Convert.ToDouble(parts[5]);
			c.N = Convert.ToDouble(parts[6]);
			c.g = Convert.ToDouble(parts[7]);
			c.k = Convert.ToDouble(parts[8]);

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportTheSky06(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseTheSky06(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseTheSky06(string line)
		{
			Comet c = new Comet();

			string[] parts = line.Split('|');

			c.full = parts[0].Trim()/*.TrimEnd(TrimCharacters)*/.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

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

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportStarryNight07(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 15; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseStarryNight07(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseStarryNight07(string line)
		{
			Comet c = new Comet();

			string tempName = line.Substring(5, 29).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			c.g = Convert.ToDouble(line.Substring(34, 6).Trim());
			c.e = Convert.ToDouble(line.Substring(48, 10).Trim());
			c.q = Convert.ToDouble(line.Substring(59, 11).Trim());
			c.N = Convert.ToDouble(line.Substring(72, 10).Trim());
			c.w = Convert.ToDouble(line.Substring(82, 10).Trim());
			c.i = Convert.ToDouble(line.Substring(92, 10).Trim());
			c.T = Convert.ToDecimal(line.Substring(102, 14).Trim());
			c.k = Convert.ToDouble(line.Substring(129, 6).Trim()) / 2.5;

			string tempId = line.Substring(136, 14).Trim();
			string tempFull = tempId + " " + tempName;

			string id, name, fragment;
			CometManager.GetIdNameFromFull(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(c.id, c.name, c.fragment);

			DateTime dd = EphemerisManager.JDToDateTime(c.T);
			c.Ty = dd.Year;
			c.Tm = dd.Month;
			c.Td = dd.Day;
			c.Th = (int)Math.Round(((dd.Hour + (dd.Minute / 60.0) + (dd.Second / 3600.0)) / 24) * 10000);

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportDeepSpace08(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 2; i < lines.Length; i += 2)
			{
				try
				{
					Comet c = ParseDeepSpace08(lines[i], lines[i + 1]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseDeepSpace08(string line1, string line2)
		{
			Comet c = new Comet();

			string tempfull = line1;
			string id, name, fragment;
			CometManager.GetIdNameFromFull2(tempfull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(id, name, fragment);

			string line = line2;
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

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportPcTcs09(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParsePcTcs09(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParsePcTcs09(string line)
		{
			Comet c = new Comet();

			string[] parts = line.TrimEnd().Split(' ');

			string tempId = parts[0];

			if (tempId.Contains('/'))
			{
				int p = 2;

				while (!Char.IsLetter(tempId[p])) p++;
				string id1 = tempId.Substring(0, p);
				string id2 = tempId.Substring(p, tempId.Length - p);

				tempId = id1 + " " + id2;
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

			string tempName = String.Empty;
			for (int i = 11; i < parts.Length; i++)
				tempName += parts[i] + " ";

			string tempFull = tempId + " " + tempName.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(c.id, c.name, c.fragment);

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportEarthCenUniv10(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 0; i < lines.Length; i += 2)
			{
				try
				{
					Comet c = ParseEarthCenUniv10(lines[i], lines[i + 1]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseEarthCenUniv10(string line1, string line2)
		{
			Comet c = new Comet();

			c.full = line1/*.TrimEnd(TrimCharacters)*/.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			string line = line2;
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

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportDanceOfThePlanets11(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 5; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseDanceOfThePlanets11(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseDanceOfThePlanets11(string line)
		{
			Comet c = new Comet();

			string tempId = line.Substring(0, 11).Trim();

			if (tempId.Contains('/'))
			{
				int p = 2;

				while (!Char.IsLetter(tempId[p])) p++;
				string id1 = tempId.Substring(0, p);
				string id2 = tempId.Substring(p, tempId.Length - p);

				tempId = id1 + " " + id2;
			}

			// some comets have q > 10 AU, but this format shows it as "********" so they are discarded
			c.q = Convert.ToDouble(line.Substring(11, 9).Trim());
			c.e = Convert.ToDouble(line.Substring(20, 9).Trim());
			c.i = Convert.ToDouble(line.Substring(29, 9).Trim());
			c.N = Convert.ToDouble(line.Substring(38, 9).Trim());
			c.w = Convert.ToDouble(line.Substring(47, 9).Trim());

			c.Ty = Convert.ToInt32(line.Substring(56, 4).Trim());
			c.Tm = Convert.ToInt32(line.Substring(61, 2).Trim());
			c.Td = Convert.ToInt32(line.Substring(61, 2).Trim());
			c.Th = Convert.ToInt32(line.Substring(65, 4).Trim().PadRight(4, '0'));

			string tempName;
			if (line.Length == 69)
				tempName = String.Empty;
			else
				tempName = line.Substring(70, line.Length - 70).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();

			string tempFull = tempId + " " + tempName;

			string id, name, fragment;
			CometManager.GetIdNameFromFull(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(c.id, c.name, c.fragment);

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportMegaStar12(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseMegaStar12(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseMegaStar12(string line)
		{
			Comet c = new Comet();

			string tempName = line.Substring(0, 30).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string tempId = line.Substring(30, 12).Trim();
			string tempFull = tempId + " " + tempName;

			string id, name, fragment;
			CometManager.GetIdNameFromFull(tempFull, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;
			c.full = CometManager.GetFullFromIdName(c.id, c.name, c.fragment);

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
			c.k = Convert.ToDouble(line.Substring(126, 6).Trim()) / 2.5;

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportSkyChart13(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseSkyChart13(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseSkyChart13(string line)
		{
			Comet c = new Comet();

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

			c.full = parts[12].Split(';')[0]/*.TrimEnd(TrimCharacters)*/.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportVoyager14(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 23; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseVoyager14(lines[i]);
					collection.Add(c, true);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseVoyager14(string line)
		{
			Comet c = new Comet();

			string temp = line.Substring(0, 27).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();

			if ((temp[0].In(CometManager.CometTypes) && temp[1] == '/') || Char.IsDigit(temp[0]))
			{
				c.full = temp;
				c.id = temp;
				c.name = String.Empty;
			}
			else
			{
				c.full = temp;
				c.id = String.Empty;
				c.name = temp;
			}

			c.q = Convert.ToDouble(line.Substring(27, 9).Trim());
			c.e = Convert.ToDouble(line.Substring(39, 8).Trim());
			c.i = Convert.ToDouble(line.Substring(49, 8).Trim());
			c.N = Convert.ToDouble(line.Substring(60, 8).Trim());
			c.w = Convert.ToDouble(line.Substring(71, 8).Trim());

			c.Ty = Convert.ToInt32(line.Substring(87, 4).Trim());

			string mon = line.Substring(91, 3);
			int ind = Array.IndexOf(CometManager.Month, mon) + 1;
			if (ind > 0)
				c.Tm = ind;
			else
				throw new Exception("Invalid month.");

			string[] dh = line.Substring(94, 7).Trim().Split('.');
			c.Td = Convert.ToInt32(dh[0]);
			c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			//voyager nema id
			//c.sortkey = CometManager.GetSortkey(c.id);
			//c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportSkyTools15(string filename, ref CometCollection collection)
		{
			foreach (string line in File.ReadAllLines(filename))
			{
				try
				{
					Comet c = ParseSkyTools15(line);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseSkyTools15(string line)
		{
			Comet c = new Comet();

			string tempFull = line.Substring(2, 41).Trim()/*.TrimEnd(TrimCharacters)*/.Trim();
			string tempName = String.Empty;
			string tempId = String.Empty;

			if (tempFull[0].In(CometManager.CometTypes) && tempFull[1] == '/')
			{
				int spaces = tempFull.Count(f => f == ' ');

				if (spaces == 1)
				{
					tempId = tempFull;
				}
				else //if (spaces >= 2)
				{
					int secondspace = GetNthIndex(tempFull, ' ', 2);
					tempId = tempFull.Substring(0, secondspace);
					tempName = tempFull.Substring(secondspace + 1, tempFull.Length - secondspace - 1);
				}

				c.full = CometManager.GetFullFromIdName(tempId, tempName);
				c.id = tempId;
				c.name = tempName;
				//c.fragment todo
			}
			else
			{
				c.full = tempFull;

				string id, name, fragment;
				CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
				c.id = id;
				c.name = name;
				c.fragment = fragment;
			}

			c.Ty = Convert.ToInt32(line.Substring(54, 4).Trim());
			c.Tm = Convert.ToInt32(line.Substring(59, 2).Trim());
			c.Td = Convert.ToInt32(line.Substring(62, 2).Trim());
			c.Th = Convert.ToInt32(line.Substring(65, 4).Trim().PadRight(4, '0'));

			c.q = Convert.ToDouble(line.Substring(70, 9).Trim());
			c.e = Convert.ToDouble(line.Substring(82, 8).Trim());
			c.w = Convert.ToDouble(line.Substring(91, 8).Trim());
			c.N = Convert.ToDouble(line.Substring(99, 8).Trim());
			c.i = Convert.ToDouble(line.Substring(107, 7).Trim());

			c.g = Convert.ToDouble(line.Substring(115, 5).Trim());
			c.k = Convert.ToDouble(line.Substring(122, 4).Trim());

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportCometForWindows(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 6; i < lines.Length; i += 13)
			{
				try
				{
					string[] parseLines = new string[]
					{
						lines[i],
						lines[i + 3],
						lines[i + 4],
						lines[i + 5],
						lines[i + 6],
						lines[i + 7],
						lines[i + 8],
						lines[i + 11]
					};

					Comet c = ParseCometForWindows(parseLines);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseCometForWindows(string[] lines)
		{
			Comet c = new Comet();

			c.full = lines[0].Split('=')[1].Trim()/*.TrimEnd(TrimCharacters)*/.Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			string[] date = lines[1].Split('=')[1].Split(' ');
			c.Ty = Convert.ToInt32(date[0]);
			c.Tm = Convert.ToInt32(date[1]);
			string[] dh = date[2].Split('.');
			c.Td = Convert.ToInt32(dh[0]);
			c.Th = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

			c.q = Convert.ToDouble(lines[2].Split('=')[1]);
			c.e = Convert.ToDouble(lines[3].Split('=')[1]);
			c.w = Convert.ToDouble(lines[4].Split('=')[1]);
			c.N = Convert.ToDouble(lines[5].Split('=')[1]);
			c.i = Convert.ToDouble(lines[6].Split('=')[1]);

			string[] gk = lines[7].Split('=')[1].Split(' ');
			c.g = Convert.ToDouble(gk[0]);
			c.k = Convert.ToDouble(gk[1]) / 2.5;

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		private static void ImportNasaComet(string filename, ref CometCollection collection)
		{
			string[] lines = File.ReadAllLines(filename);

			for (int i = 2; i < lines.Length; i++)
			{
				try
				{
					Comet c = ParseNasaComet(lines[i]);
					collection.Add(c);
				}
				catch
				{
					continue;
				}
			}
		}

		private static Comet ParseNasaComet(string line)
		{
			Comet c = new Comet();

			c.full = line.Substring(0, 43).Trim();

			string id, name, fragment;
			CometManager.GetIdNameFromFull(c.full, out id, out name, out fragment);
			c.id = id;
			c.name = name;
			c.fragment = fragment;

			/////////////////////////
			//pogledat epoch
			//mozda treba njega ucitat pa precesirat vrijednosti W N i na epoch 2000

			//int epoch = Convert.ToInt32(line.Substring(44, 7);
			/////////////////////////

			c.q = Convert.ToDouble(line.Substring(52, 11).Trim());
			c.e = Convert.ToDouble(line.Substring(64, 10).Trim());
			c.i = Convert.ToDouble(line.Substring(75, 9).Trim());
			c.w = Convert.ToDouble(line.Substring(85, 9).Trim());
			c.N = Convert.ToDouble(line.Substring(95, 9).Trim());

			c.Ty = Convert.ToInt32(line.Substring(105, 4).Trim());
			c.Tm = Convert.ToInt32(line.Substring(109, 2).Trim());
			c.Td = Convert.ToInt32(line.Substring(111, 2).Trim());
			c.Th = Convert.ToInt32(Convert.ToDouble(line.Substring(114, 5).Trim().PadRight(5, '0')) / 10.0);

			c.T = EphemerisManager.JD0(c.Ty, c.Tm, c.Td, c.Th);
			c.P = CometManager.GetPeriod(c.q, c.e);
			c.a = CometManager.GetSemimajorAxis(c.q, c.e);
			c.n = CometManager.GetMeanMotion(c.e, c.P);
			c.Q = CometManager.GetAphelionDistance(c.e, c.a);

			c.sortkey = CometManager.GetSortkey(c.id, c.fragment);
			c.idKey = CometManager.GetIdKey(c.id);

			return c;
		}

		#endregion

		#region Methods

		private static int GetNthIndex(string s, char c, int n)
		{
			//http://stackoverflow.com/questions/2571716/find-nth-occurrence-of-a-character-in-a-string

			int count = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == c)
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

		#endregion
	}
}
