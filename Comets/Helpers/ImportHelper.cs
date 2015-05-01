using Comets.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImportType = Comets.Classes.ElementTypes.Type;

namespace Comets.Helpers
{
    public static class ImportHelper
    {
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

            Comet c = new Comet();

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

                return ImportType.MPC;
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

                return ImportType.SkyMap;
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

                return ImportType.Guide;
            }
            catch
            {

            }

            try //xephem 3
            {
                string[] parts = lastLine.Split(',');
                if (parts.Count() == 13 && (parts[1] == "e" || parts[1] == "p" || parts[1] == "h")) return ImportType.xephem;
            }
            catch
            {

            }

            try //home planet 4
            {
                string[] parts = lastLine.Split(',');
                if (parts.Count() == 10) return ImportType.HomePlanet;
            }
            catch
            {

            }

            try //mystars 5
            {
                string[] parts = lastLine.Split('\t');
                if (parts.Count() == 11) return ImportType.MyStars;
            }
            catch
            {

            }

            try //thesky 6
            {
                string[] parts = lastLine.Split('|');
                if (parts.Count() == 11 && parts[0].Length == 39 && parts[1].Length == 4) return ImportType.TheSky;
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

                return ImportType.StarryNight;
            }
            catch
            {

            }

            try //deepspace 8
            {
                string[] parts = lastLine.Split(' ');
                if (parts.Count() == 12 && parts[0].Length == 1) return ImportType.DeepSpace;
            }
            catch
            {

            }

            try //pc-tcs 9
            {
                string[] parts = lastLine.TrimEnd().Split(' ');
                if (parts.Count() >= 12 && lastLine.Length == 126) return ImportType.PCTCS;
            }
            catch
            {

            }

            try //ecu 10
            {
                string[] parts = lastLine.Split(' ');
                if (parts.Count() == 13 && parts[0].Length == 1 && parts[1].Length == 1) return ImportType.EarthCenteredUniverse;
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

                return ImportType.DanceOfThePlanets;
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

                return ImportType.MegaStarV4;
            }
            catch
            {

            }

            try //skychart 13
            {
                string[] parts = lastLine.Split('\t');
                if (parts.Count() == 14 && parts[0].Length == 3) return ImportType.SkyChartIII;
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
                lastLine.Contains("Dec")) return ImportType.VoyagerII;

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

                return ImportType.SkyTools;
            }
            catch
            {

            }

            try
            {
                //comet for windows 18
                if (lines[1] == "[File]")
                    return ImportType.CometForWindows;

                //nasa elements.comet 19
                if (lines[1].Contains("-----"))
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

            if (importType == ImportType.xephem || importType == ImportType.DeepSpace || importType == ImportType.EarthCenteredUniverse)
                lines /= 2;
            
            if (importType == ImportType.DeepSpace || importType == ImportType.HomePlanet || importType == ImportType.MyStars)
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

        public static List<Comet> ImportMain(ImportType importType, string filename)
        {
            List<Comet> list = new List<Comet>();

            switch (importType)
            {
                case ImportType.MPC:
                    ImportMpc00(filename, ref list); break;

                case ImportType.SkyMap:
                    ImportSkyMap01(filename, ref list); break;

                case ImportType.Guide:
                    ImportGuide02(filename, ref list); break;

                case ImportType.xephem:
                    ImportXephem03(filename, ref list); break;

                case ImportType.HomePlanet:
                    ImportHomePlanet04(filename, ref list); break;

                case ImportType.MyStars:
                    ImportMyStars05(filename, ref list); break;

                case ImportType.TheSky:
                    ImportTheSky06(filename, ref list); break;

                case ImportType.StarryNight:
                    ImportStarryNight07(filename, ref list); break;

                case ImportType.DeepSpace:
                    ImportDeepSpace08(filename, ref list); break;

                case ImportType.PCTCS:
                    ImportPcTcs09(filename, ref list); break;

                case ImportType.EarthCenteredUniverse:
                    ImportEarthCenUniv10(filename, ref list); break;

                case ImportType.DanceOfThePlanets:
                    ImportDanceOfThePlanets11(filename, ref list); break;

                case ImportType.MegaStarV4:
                    ImportMegaStar12(filename, ref list); break;

                case ImportType.SkyChartIII:
                    ImportSkyChart13(filename, ref list); break;

                case ImportType.VoyagerII:
                    ImportVoyager14(filename, ref list); break;

                case ImportType.SkyTools:
                    ImportSkyTools15(filename, ref list); break;

                case ImportType.CometForWindows:
                    ImportCometForWindows(filename, ref list); break;

                case ImportType.NASA:
                    ImportNasaComet(filename, ref list); break;
            }

            list = list.OrderBy(x => x.sortkey).ToList();

            return list;
        }

        #endregion

        #region ImportFunctions

        public static void ImportMpc00(string filename, ref List<Comet> list)
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

                    string[] idn = Comet.GetIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportSkyMap01(string filename, ref List<Comet> list)
        {
            string tempFull = string.Empty;
            string tempId = string.Empty;
            string tempName = string.Empty;

            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    tempFull = line.Substring(0, 44).Trim();
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


                    if ((tempFull[0] == 'C' || tempFull[0] == 'P' || tempFull[0] == 'D' || tempFull[0] == 'X') && tempFull[1] == '/')
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
                    }
                    else
                    {
                        int spaceind = tempFull.IndexOf(' ');
                        if (spaceind == -1)
                        {
                            //ako nema razmaka, "282P"
                            tempId = tempFull;
                        }
                        else
                        {
                            tempId = tempFull.Substring(0, spaceind);
                            tempName = tempFull.Substring(spaceind + 1, tempFull.Length - spaceind - 1);
                        }
                    }

                    c.full = Comet.GetFullFromIdName(tempId, tempName);
                    c.id = tempId;
                    c.name = tempName;

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportGuide02(string filename, ref List<Comet> list)
        {
            string tempFull = string.Empty;
            string tempId = string.Empty;
            string tempName = string.Empty;

            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    tempFull = line.Substring(0, 42).Trim();
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

                    if (tempFull.Contains('('))
                    {
                        int ind = tempFull.IndexOf('(');

                        tempName = tempFull.Substring(0, ind - 1);

                        if (tempName.Contains("/"))
                            tempName = tempName.Substring(2, tempName.Length - 2);

                        tempId = tempFull.Substring(ind + 1, tempFull.Length - ind - 2);
                    }
                    else
                    {
                        tempId = tempFull;
                    }

                    c.full = Comet.GetFullFromIdName(tempId, tempName);
                    c.id = tempId;
                    c.name = tempName;

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }



                list.Add(c);
            }
        }

        public static void ImportXephem03(string filename, ref List<Comet> list)
        {
            // http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

            string[] lines = File.ReadAllLines(filename);

            for (int i = 1; i < lines.Count(); i += 2)
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = lines[i].Split(',');

                    c.full = parts[0];
                    if (c.full[c.full.Length - 1] == '/') c.full = c.full.TrimEnd('/');
                    string[] idn = Comet.GetIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    if (parts[1] == "e")
                    {
                        c.i = Convert.ToDouble(parts[2]);
                        c.N = Convert.ToDouble(parts[3]);
                        c.w = Convert.ToDouble(parts[4]);
                        double a = Convert.ToDouble(parts[5]); // semi-major axis
                        double n = Convert.ToDouble(parts[6]); // mean daily motion
                        c.e = Convert.ToDouble(parts[7]);
                        double M = Convert.ToDouble(parts[8]); // mean anomaly, i.e., degrees from perihelion

                        string[] date = parts[9].Split('/');
                        int m = Convert.ToInt32(date[0]);
                        int y = Convert.ToInt32(date[2]);
                        string[] dh = date[1].Split('.');
                        int d = Convert.ToInt32(dh[0]);
                        int h = Convert.ToInt32(dh[1].Trim().PadRight(4, '0'));

                        c.g = Convert.ToDouble(parts[11].Substring(2, parts[11].Length - 2));
                        c.k = Convert.ToDouble(parts[12]);

                        c.q = a * (1 - c.e);
                        
                        if (M == 0)
                        {
                            c.Tm = m;
                            c.Td = d;
                            c.Th = h;
                            c.Ty = y;
                        }
                        else
                        {
                            double E = EphemerisHelper.jd0(y, m, d, h); // epoch date, i.e., time of M

                            c.T = E - M / n;

                            if (M >= 180)
                                c.T +=  Comet.GetPeriod(c.q, c.e) * 365.25;
                            
                            int[] newdate = EphemerisHelper.jdtocd(c.T);
                            c.Ty = newdate[0];
                            c.Tm = newdate[1];
                            c.Td = newdate[2];
                            c.Th = (int)(((newdate[4] + (newdate[5] / 60.0) + (newdate[6] / 3600.0)) / 24) * 10000);

                            var bzvz = c.Th + 3;
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
                        c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
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

                        c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    }

                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportHomePlanet04(string filename, ref List<Comet> list)
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
                    string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportMyStars05(string filename, ref List<Comet> list)
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
                    string[] idn = Comet.GetIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    Th = parts[1].Split('.');
                    T = Convert.ToDouble(Th[0]);
                    h = Convert.ToInt32(Th[1].Trim().PadRight(4, '0'));
                    c.T = T + 2400000.5;

                    int[] dd = EphemerisHelper.jdtocd(c.T);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportTheSky06(string filename, ref List<Comet> list)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = line.Split('|');

                    c.full = parts[0].Trim();
                    string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportStarryNight07(string filename, ref List<Comet> list)
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

                    c.full = Comet.GetFullFromIdName(c.id, c.name);

                    int[] dd = EphemerisHelper.jdtocd(c.T);
                    c.Ty = dd[0];
                    c.Tm = dd[1];
                    c.Td = dd[2];
                    c.Th = (int)(((dd[4] + (dd[5] / 60.0) + (dd[6] / 3600.0)) / 24) * 10000);

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportDeepSpace08(string filename, ref List<Comet> list)
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
                    c.full = Comet.GetFullFromIdName(c.id, c.name);

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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportPcTcs09(string filename, ref List<Comet> list)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    string[] parts = line.TrimEnd().Split(' ');

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

                    string tempName = string.Empty;
                    for (int i = 11; i < parts.Count(); i++)
                        tempName += parts[i] + " ";

                    c.name = tempName.Trim();

                    c.full = Comet.GetFullFromIdName(id, c.name);
                    c.id = id;

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportEarthCenUniv10(string filename, ref List<Comet> list)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < lines.Count(); i += 2)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i];
                    string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportDanceOfThePlanets11(string filename, ref List<Comet> list)
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
                        c.name = string.Empty;
                    else
                        c.name = lines[i].Substring(70, lines[i].Length - 70).Trim();

                    c.full = Comet.GetFullFromIdName(id, c.name);
                    c.id = id;

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportMegaStar12(string filename, ref List<Comet> list)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    c.name = line.Substring(0, 30).Trim();
                    c.id = line.Substring(30, 12).Trim();

                    c.full = Comet.GetFullFromIdName(c.id, c.name);

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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportSkyChart13(string filename, ref List<Comet> list)
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
                    string[] idn = Comet.GetIdNameFromFull(c.full);
                    c.id = idn[0];
                    c.name = idn[1];

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportVoyager14(string filename, ref List<Comet> list)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 23; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    c.name = lines[i].Substring(0, 27).Trim();
                    c.id = string.Empty;
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportSkyTools15(string filename, ref List<Comet> list)
        {
            string tempFull = string.Empty;
            string tempId = string.Empty;
            string tempName = string.Empty;

            foreach (string line in File.ReadAllLines(filename))
            {
                Comet c = new Comet();

                try
                {
                    tempFull = line.Substring(2, 41).Trim();

                    if ((tempFull[0] == 'C' || tempFull[0] == 'P' || tempFull[0] == 'D' || tempFull[0] == 'X') && tempFull[1] == '/')
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

                        c.full = Comet.GetFullFromIdName(tempId, tempName);
                        c.id = tempId;
                        c.name = tempName;
                    }
                    else
                    {
                        c.full = tempFull;
                        string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportCometForWindows(string filename, ref List<Comet> list)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 6; i < lines.Count(); i += 13)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i].Split('=')[1];
                    string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        public static void ImportNasaComet(string filename, ref List<Comet> list)
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 2; i < lines.Count(); i++)
            {
                Comet c = new Comet();

                try
                {
                    c.full = lines[i].Substring(0, 43).Trim();
                    string[] idn = Comet.GetIdNameFromFull(c.full);
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

                    c.T = EphemerisHelper.jd0(c.Ty, c.Tm, c.Td, c.Th);
                    c.P = Comet.GetPeriod(c.q, c.e);
                    c.a = Comet.GetSemimajorAxis(c.q, c.e);
                    c.n = Comet.GetMeanMotion(c.e, c.P);
                    c.Q = Comet.GetAphelionDistance(c.e, c.a);

                    c.sortkey = Comet.GetSortkey(c.id);
                    c.idKey = Comet.GetIdKey(c.id);
                }
                catch
                {
                    continue;
                }

                list.Add(c);
            }
        }

        #endregion

        #region GetNthIndex

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
