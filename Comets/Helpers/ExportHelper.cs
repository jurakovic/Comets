using Comets.Classes;
using Comets.Forms;
using System;
using System.IO;
using System.Text;
using ExportType = Comets.Classes.ElementTypes.Type;


namespace Comets.Helpers
{
    class ExportHelper
    {
        #region ExportMain

        public static void ExportMain(int exportType, string filename)
        {
            StringBuilder sb = new StringBuilder();

            WriteHeaderText(exportType, ref sb);

            ExportMpc0(ref sb);

            File.WriteAllText(filename, sb.ToString());
        }

        #endregion

        #region WriteHeaderText

        public static void WriteHeaderText(int exportType, ref StringBuilder sb)
        {
            if (exportType == (int)ExportType.HomePlanet)
            {
                sb.AppendLine("Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period");
            }

            if (exportType == (int)ExportType.MyStars)
            {
                sb.Append("RDPC\t").AppendLine(FormMain.userList.Count.ToString());
            }

            if (exportType == (int)ExportType.StarryNight)
            {
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
                sb.AppendLine("Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         ReferenceDD");
            }

            if (exportType == (int)ExportType.DeepSpace)
            {
                sb.AppendLine("Type C: Equinox Year Month Day q e Peri Node i Mag k");
                sb.AppendLine("Type A: Equinox Year Month Day a M e Peri Node i H G");
            }

            if (exportType == (int)ExportType.DanceOfThePlanets)
            {
                sb.AppendLine("Comet      peri(au)   e         iř       ęř       wř     peridate     name");
                sb.AppendLine("(In order to be recognised by Dance of the Planets, this file)");
                sb.AppendLine("(must have a .cmt extension.)");
                sb.AppendLine("(File prepared by IAU Minor Planet Center/Central Bureau)");
                sb.AppendLine("(for Astronomical Telegrams.)");

            }

            if (exportType == (int)ExportType.VoyagerII)
            {
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
            }
        }

        #endregion

        #region EportFunctions

        protected static void ExportMpc0(ref StringBuilder sb)
        {
            string format = "              {0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}  {5:0.000000}  {6,8:0.0000}  {7,8:0.0000}  {8,8:0.0000}  00000000  {9,4:0.0} {10,4:0.0}  {11,-56} MPC 00000";

            foreach (Comet c in FormMain.userList)
            {
                sb.AppendLine(String.Format(format, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k, c.full));
            }
        }

        #endregion
    }
}
