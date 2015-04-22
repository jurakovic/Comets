using Comets.Classes;
using Comets.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExportType = Comets.Classes.ElementTypes.Type;

namespace Comets.Helpers
{
    class ExportHelper
    {
        #region ExportMain

        public static void ExportMain(ExportType exportType, string filename, List<Comet> list)
        {
            StringBuilder sb = new StringBuilder();

            WriteHeaderText(exportType, ref sb);

            switch (exportType)
            {
                case ExportType.MPC:
                    ExportMpc00(ref sb, list); break;

                case ExportType.SkyMap:
                    ExportSkyMap01(ref sb, list); break;

                case ExportType.Guide:
                    ExportGuide02(ref sb, list); break;

                case ExportType.xephem:
                    ExportXephem03(ref sb, list); break;

                case ExportType.HomePlanet:
                    ExportHomePlanet04(ref sb, list); break;

                case ExportType.MyStars:
                    ExportMyStars05(ref sb, list); break;

                case ExportType.TheSky:
                    ExportTheSky06(ref sb, list); break;

                case ExportType.StarryNight:
                    ExportStarryNight07(ref sb, list); break;

                case ExportType.DeepSpace:
                    ExportDeepSpace08(ref sb, list); break;

                case ExportType.PCTCS:
                    ExportPcTcs09(ref sb, list); break;

                case ExportType.EarthCenteredUniverse:
                    ExportEarthCenUniv10(ref sb, list); break;

                case ExportType.DanceOfThePlanets:
                    ExportDanceOfThePlanets11(ref sb, list); break;

                case ExportType.MegaStarV4:
                    ExportMegaStar12(ref sb, list); break;

                case ExportType.SkyChartIII:
                    ExportSkyChart13(ref sb, list); break;

                case ExportType.VoyagerII:
                    ExportVoyager14(ref sb, list); break;

                case ExportType.SkyTools:
                    ExportSkyTools15(ref sb, list); break;

                case ExportType.Autostar:
                    ExportTheSky06(ref sb, list); break;

                case ExportType.Celestia:
                    ExportCelestia(ref sb, list); break;

                //case ExportType.CometForWindows:
                //    ExportCometForWindows(ref sb, list); break;

                //case ExportType.NASA:
                //    ExportNasaComet(ref sb, list); break;
            }

            File.WriteAllText(filename, sb.ToString());
        }

        #endregion

        #region WriteHeaderText

        public static void WriteHeaderText(ExportType exportType, ref StringBuilder sb)
        {
            switch (exportType)
            {
                case ExportType.HomePlanet:
                    sb.AppendLine("Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period");
                    break;

                case ExportType.MyStars:
                    sb.Append("RDPC\t").AppendLine(FormMain.userList.Count.ToString());
                    break;

                case ExportType.StarryNight:
                    sb.AppendLine("NOTE: If viewing this file and it appears confused, make the window very wide!");
                    sb.AppendLine();
                    sb.AppendLine("   The numbers are all in the proper format for easy use in Starry Night's");
                    sb.AppendLine("orbit editor. Just click on the word Sun in the planet floater and then");
                    sb.AppendLine("click on add. In the first window that appears select the comet as the type");
                    sb.AppendLine("of object you want to add. Please see the manual for more information.");
                    sb.AppendLine();
                    sb.AppendLine("   The orbital information should have the reference plane set at Ecliptic");
                    sb.AppendLine(" 2000 and the Style should be pericentric. Don't forget to use copy and");
                    sb.AppendLine(" paste to ease the input of the orbital data into Starry Night.");
                    sb.AppendLine();
                    sb.AppendLine("This file kindly prepared by the IAU Minor Planet Center & Central Bureau for Astronomical Telegrams.");
                    sb.AppendLine();
                    sb.AppendLine("Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         Reference");
                    sb.AppendLine();
                    break;

                case ExportType.DeepSpace:
                    sb.AppendLine("Type C: Equinox Year Month Day q e Peri Node i Mag k");
                    sb.AppendLine("Type A: Equinox Year Month Day a M e Peri Node i H G");
                    break;

                case ExportType.DanceOfThePlanets:
                    sb.AppendLine("Comet      peri(au)   e         iř       ęř       wř     peridate     name");
                    sb.AppendLine("(In order to be recognised by Dance of the Planets, this file)");
                    sb.AppendLine("(must have a .cmt extension.)");
                    sb.AppendLine("(File prepared by IAU Minor Planet Center/Central Bureau)");
                    sb.AppendLine("(for Astronomical Telegrams.)");
                    break;

                case ExportType.VoyagerII:
                    sb.AppendLine("NOTE TO VOYAGER II USERS:");
                    sb.AppendLine();
                    sb.AppendLine("   The following table will link the symbols below with the names used in");
                    sb.AppendLine("the Voyager II \"Define New Orbit...\" dialog for comets.");
                    sb.AppendLine();
                    sb.AppendLine("     q        perihelion distance (astronomical units)");
                    sb.AppendLine("     e        eccentricity (no units)");
                    sb.AppendLine("     i        inclination of orbit to ecliptic (degrees)");
                    sb.AppendLine("     Node     longitude of ascending node (degrees)");
                    sb.AppendLine("     w        argument of perihelion (degrees)");
                    sb.AppendLine("     L        mean anomaly (this is 0 at perihelion) (degrees)");
                    sb.AppendLine("     Date     epoch of orbit");
                    sb.AppendLine("     Equinox  reference equinox (usually 2000.0)");
                    sb.AppendLine();
                    sb.AppendLine("Save this page as plain text from your browser and use the table to input");
                    sb.AppendLine("the orbital elements for the comets that you would like to plot and");
                    sb.AppendLine("follow.  If you have any question, consult your software manual or the");
                    sb.AppendLine("Carina web site: <a href=\"http://www.carinasoft.com\">http://www.carinasoft.com</a>");
                    sb.AppendLine();
                    sb.AppendLine("Thanks to the IAU Minor Planet Center & Central Bureau for Astronomical");
                    sb.AppendLine("Telegrams for providing this information.");
                    sb.AppendLine();
                    sb.AppendLine("Name                            q          e         i        Node         w       L      T(Date)    Equinox");
                    break;
            }
        }

        #endregion

        #region EportFunctions

        protected static void ExportMpc00(ref StringBuilder sb, List<Comet> list)
        {
            string format = "              {0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}  {5:0.000000}  {6,8:0.0000}  {7,8:0.0000}  {8,8:0.0000}  00000000  {9,4:0.0} {10,4:0.0}  {11,-56} MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k, c.full));
            }
        }

        protected static void ExportSkyMap01(ref StringBuilder sb, List<Comet> list)
        {
            string tempFull = string.Empty;

            string format = "{0,-47}{1,4} {2:00} {3:00}.{4:0000} {5,9:0.000000}       {6:0.000000} {7,8:0.0000} {8,8:0.0000} {9,8:0.0000}  {10,4:0.0}  {11,4:0.0}";

            foreach (Comet c in list)
            {
                if (char.IsNumber(c.id[0]))
                    tempFull = c.full.Replace("/", " ");
                else
                    tempFull = c.full.Replace("(", "").Replace(")", "");

                sb.AppendLine(String.Format(format, tempFull, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k));
            }
        }

        protected static void ExportGuide02(ref StringBuilder sb, List<Comet> list)
        {
            string tempFull = string.Empty;

            string format = "{0,-43}{1,2}.{2:0000}  {3,2}  {4,4}  0.0        {5,9:0.000000}    {6:0.000000}  {7,8:0.0000}    {8,8:0.0000}    {9,8:0.0000}    2000.0   {10,4:0.0} {11,4:0.0}    MPC 00000";

            foreach (Comet c in list)
            {
                if (c.name.Length == 0)
                    tempFull = c.id;
                else
                {
                    if (char.IsNumber(c.id[0]))
                        tempFull = "P/" + c.name + " (" + c.id + ")";
                    else
                        tempFull = c.name + " (" + c.id + ")";
                }

                sb.AppendLine(String.Format(format, tempFull, c.Td, c.Th, c.Tm, c.Ty, c.q, c.e, c.i, c.w, c.N, c.g, c.k));
            }
        }

        protected static void ExportXephem03(ref StringBuilder sb, List<Comet> list)
        {
            //info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

            string tempFull = string.Empty;

            foreach (Comet c in list)
            {
                sb.AppendLine("# From MPC 00000");

                tempFull = c.id;

                if (char.IsNumber(c.id[0]))
                    tempFull += "/" + c.name;
                else if (c.name.Length > 0)
                    tempFull += " (" + c.name + ")";

                if (c.e < 1.0)
                {
                    double smAxis = Comet.GetSemimajorAxis(c.q, c.e);
                    double mdMotion = Comet.GetMeanMotion(c.e, c.P);

                    string format = "{0},e,{1:0.0000},{2:0.0000},{3:0.0000##},{4:0.0000##},{5:0.0000000},{6:0.00000000},0.0000,{7:00}/{8:00}.{9}/{10},2000,g {11,4:0.0},{12:0.0}";
                    sb.AppendLine(String.Format(format, tempFull, c.i, c.N, c.w, smAxis, mdMotion, c.e, c.Tm, c.Td, c.Th, c.Ty, c.g, c.k));
                }
                else if (c.e == 1.0)
                {
                    double Td = c.Td + c.Th/10000.0;
                    string format = "{0},p,{1:00}/{2:00.000#}/{3},{4:0.000#},{5:0.000#},{6:0.00000#},{7:0.000#},2000,{8:0.0},{9:0.0}";
                    sb.AppendLine(String.Format(format, tempFull, c.Tm, Td, c.Ty, c.i, c.w, c.q, c.N, c.g, c.k));
                }
                else if (c.e > 1.0)
                {
                    string format = "{0},h,{1:00}/{2:00}.{3}/{4},{5:0.0000},{6:0.0000},{7:0.0000},{8:0.000000},{9:0.000000},2000,{10:0.0},{11:0.0}";
                    sb.AppendLine(String.Format(format, tempFull, c.Tm, c.Td, c.Th, c.Ty, c.i, c.N, c.w, c.e, c.q, c.g, c.k));
                }
            }
        }

        protected static void ExportHomePlanet04(ref StringBuilder sb, List<Comet> list)
        {
            string tempFull = string.Empty;
            string format = string.Empty;

            foreach (Comet c in list)
            {
                tempFull = c.id;

                if (char.IsNumber(c.id[0]))
                    tempFull += "/" + c.name;
                else if (c.name.Length > 0)
                    tempFull += " (" + c.name + ")";

                if (c.P < 30000)
                {
                    format = "{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},{10:0.00000},{11:0.00} years, MPC 00000";
                    double smAxis = Comet.GetSemimajorAxis(c.q, c.e);
                    sb.AppendLine(String.Format(format, tempFull, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, smAxis, c.P));
                }
                else
                {
                    format = "{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},,, MPC 00000";
                    sb.AppendLine(String.Format(format, tempFull, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i));
                }
            }
        }

        protected static void ExportMyStars05(ref StringBuilder sb, List<Comet> list)
        {
            //
            // {2} zapravo nije w, nije ni M
            //

            int d = 4;
            int m = 3;
            int y = 2015;
            double JD = Comet.GregorianToJulian(y, m, d, 5000);

            string format = "{0};\t{1:0.0000}\t{2:0.0000}\t{3:0.000000}\t{4:0.000000}\t{5:0.0000}\t{6:0.0000}\t{7:0.0}\t{8:0.0}\tMPC00000\t{9:0.0}";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.full, c.T - 2400000.5, c.w, c.e, c.q, c.i, c.N, c.g, c.k, JD-2400000.5));
            }
        }

        protected static void ExportTheSky06(ref StringBuilder sb, List<Comet> list)
        {
            string format = "{0,-39}|2000|{1,4}{2:00}{3:00}.{4:0000} |{5,9:0.000000} |{6:0.000000} |{7,8:0.0000} |{8,8:0.0000} |{9,8:0.0000} |{10,4:0.0} |{11,4:0.0} | MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.full, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k*2.5));
            }
        }

        protected static void ExportStarryNight07(ref StringBuilder sb, List<Comet> list)
        {
            string format = "     {0,-29} {1,4:0.0}    0.0   {2:0.000000}   {3,9:0.000000}    {4,8:0.0000}  {5,8:0.0000}  {6,8:0.0000}  {7,12:0.0000}    0000000.0  {8,4:0.0}  {9,-13} MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.name, c.g, c.e, c.q, c.N, c.w, c.i, c.T, c.k * 2.5, c.id));
            }
        }

        protected static void ExportDeepSpace08(ref StringBuilder sb, List<Comet> list)
        {
            string format = "C J2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}";

            foreach (Comet c in list)
            {
                sb.AppendLine(c.name + " (" + c.id + ")");
                sb.AppendLine(String.Format(format, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5));
            }
        }

        protected static void ExportPcTcs09(ref StringBuilder sb, List<Comet> list)
        {
            string tempId = string.Empty;
            string line = string.Empty;

            string format = "{0} {1:0.000000} {2:0.000000} {3:0.0000} {4:0.0000} {5:0.0000} {6,4} {7:00} {8:00}.{9:0000} {10:0.0} {11:0.0} {12}";

            foreach (Comet c in list)
            {
                tempId = c.id.Replace(" ", "");
                line = String.Format(format, tempId, c.q, c.e, c.i, c.w, c.N, c.Ty, c.Tm, c.Td, c.Th, c.g, c.k * 2.5, c.name);

                sb.AppendLine(String.Format("{0,-126}", line));
            }
        }
        
        protected static void ExportEarthCenUniv10(ref StringBuilder sb, List<Comet> list)
        {
            string format = "E C 2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}";

            foreach (Comet c in list)
            {
                sb.AppendLine(c.full);
                sb.AppendLine(String.Format(format, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5));
            }
        }

        protected static void ExportDanceOfThePlanets11(ref StringBuilder sb, List<Comet> list)
        {
            string tempId = string.Empty;
            string q = string.Empty;

            string format = "{0,-11}{1} {2:0.000000} {3,8:0.0000} {4,8:0.0000} {5,8:0.0000} {6,4}.{7:00}{8:00}{9:0000} {10}";

            foreach (Comet c in list)
            {
                tempId = c.id.Replace(" ", "");

                if (c.q >= 10.0)
                    q = "********";
                else
                    q = String.Format("{0:0.000000}", c.q);

                sb.AppendLine(String.Format(format, tempId, q, c.e, c.i, c.N, c.w, c.Ty, c.Tm, c.Td, c.Th, c.name));
            }
        }

        protected static void ExportMegaStar12(ref StringBuilder sb, List<Comet> list)
        {
            string format = "{0,-30}{1,-12}{2,4} {3:00}  {4:00}.{5:0000}   {6,9:0.000000}   {7:0.000000}    {8,8:0.0000}    {9,8:0.0000}    {10,8:0.0000}   {11,4:0.0}   {12,4:0.0}    2000 MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.name, c.id, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5));
            }
        }
        
        protected static void ExportSkyChart13(ref StringBuilder sb, List<Comet> list)
        {
            string format = "P11\t2000.0\t-{0:0.000000}\t{1:0.000000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t0\t{5}/{6:00}/{7:00}.{8:0000}\t{9:0.0}\t{10:0.0}\t0\t0\t{11}; MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.q, c.e, c.i, c.w, c.N, c.Ty, c.Tm, c.Td, c.Th, c.g, c.k, c.full));
            }
        }

        protected static void ExportVoyager14(ref StringBuilder sb, List<Comet> list)
        {
            string mon = string.Empty;
            string line = string.Empty;

            string format = "{0,-26} {1,9:0.000000}   {2:0.000000}  {3,8:0.0000}   {4,8:0.0000}   {5,8:0.0000}   0.0  {6,4}{7}";

            foreach (Comet c in list)
            {
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

                line = String.Format(format, c.name, c.q, c.e, c.i, c.N, c.w, c.Ty, mon);

                if (c.Td < 10)
                    line += String.Format("{0}.{1:0000}  2000.0", c.Td, c.Th);
                else
                    line += String.Format("{0}.{1:0000} 2000.0", c.Td, c.Th);

                sb.AppendLine(line);
            }
        }

        protected static void ExportSkyTools15(ref StringBuilder sb, List<Comet> list)
        {
            string format = "C {0,-40} 0000 00 00 {1,4} {2:00} {3:00}.{4:000}  {5,9:0.000000}   {6:0.000000} {7,7:0.000} {8,7:0.000} {9,7:0.000}  {10,4:0.0}  {11,4:0.0} 0.002000 MPC 00000";

            foreach (Comet c in list)
            {
                sb.AppendLine(String.Format(format, c.full, c.Ty, c.Tm, c.Td, c.Th / 10, c.q, c.e, c.w, c.N, c.i, c.g, c.k));
            }
        }

        protected static void ExportCelestia(ref StringBuilder sb, List<Comet> list)
        {
            string mon = string.Empty;

            foreach (Comet c in list)
            {
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

                sb.AppendLine(String.Format("\"{0}\" \"Sol\"", c.full.Replace('/', ' ')));
                sb.AppendLine("{");
                sb.AppendLine("\tClass \"comet\"");
                sb.AppendLine("\tMesh \"asteroid.cms\"");
                sb.AppendLine("\tTexture \"asteroid.jpg\"");
                sb.AppendLine("\tRadius 5");
                sb.AppendLine("\tAlbedo 0.1");
                sb.AppendLine(String.Format("\t#Magnitude g {0:0.0}, k {1:0.0}", c.g, c.k));
                sb.AppendLine("\tEllipticalOrbit");
                sb.AppendLine("\t{");
                sb.AppendLine(String.Format("\t\tPeriod         {0,20:0.000000}", c.P));
                sb.AppendLine(String.Format("\t\tPericenterDistance        {0,9:0.000000}", c.q));

                if (c.e < 1.0)
                {
                    sb.AppendLine(String.Format("\t\tEccentricity               {0,8:0.000000}", c.e)); ;
                }
                else
                {
                    sb.AppendLine("\t\tEccentricity               0.999999");
                    sb.AppendLine(String.Format("\t\t#Eccentricity               {0,8:0.000000}  # Real", c.e));
                }

                sb.AppendLine(String.Format("\t\tInclination              {0,8:0.0000}", c.i));
                sb.AppendLine(String.Format("\t\tAscendingNode            {0,8:0.0000}", c.N));
                sb.AppendLine(String.Format("\t\tArgOfPericenter          {0,8:0.0000}", c.w));
                sb.AppendLine("\t\tMeanAnomaly                0.0");
                sb.AppendLine(String.Format("\t\tEpoch                {0,12:0.0000}     # {1,4} {2} {3:00}.{4:0000}", c.T, c.Ty, mon, c.Td, c.Th));
                sb.AppendLine("\t}");
                sb.AppendLine("}");
                sb.AppendLine();
            }
        }

        #endregion
    }
}
