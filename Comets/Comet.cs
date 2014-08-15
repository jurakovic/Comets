using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Comets
{
    public class Comet
    {
        public string full;
        public string name;
        public string id;
        public double T;
        public int Ty;
        public int Tm;
        public int Td;
        public int Th;
        public double P;    //Period
        public double q;    //Perihelion Distance
        public double e;    //Eccentricity
        public double i;    //Inclination
        public double N;    //Longitude of the Ascending Node
        public double w;    //Argument of Pericenter
        public double a;    //Semimajor Axis
        public double Q;    //Aphelion Distance
        public double g;
        public double k;
        public double sortkey;

        public Comet()
        {
            full = "";
            name = "";
            id = "";
            T = 0;
            Ty = 0;
            Tm = 0;
            Td = 0;
            Th = 0;
            P = 0.0;
            q = 0.0;
            e = 0.0;
            i = 0.0;
            N = 0.0;
            w = 0.0;
            a = 0.0;
            Q = 0.0;
            g = 0.0;
            k = 0.0;
            sortkey = 0.0;
        }


        public static double GetSortkey(string id)
        {
            double sort = 0.0;
            double v = 0.0;
            double offset = 2000.0;
            string fragm = "";

            //http://stackoverflow.com/questions/3720012/regular-expression-to-split-string-and-number
            Regex numAlpha = new Regex("(?<letters>[a-zA-Z]*)(?<digits>[0-9]*)");

            if (id.Contains('-') && id[2] != '-') // 128P-B, C/-146 P1
            {
                string[] fi = id.Split('-');
                id = fi[0];
                fragm = fi[1];

                var match = numAlpha.Match(fragm);

                string fragmLetters = match.Groups["letters"].Value;
                string fragmDigits = match.Groups["digits"].Value;

                for (int i = 0, divider = 1000000000; i < fragmLetters.Length; i++, divider *= 100)
                {
                    v += (fragmLetters[i] - 64) / (double)divider;
                }

                if (fragmDigits != "")
                    v += Convert.ToDouble(fragmDigits) / 10000000000000.0;
            }

            if (char.IsDigit(id[0]))
            {
                sort = Convert.ToDouble(id.Substring(0, id.Length - 1));
            }
            else
            {
                string[] yc = id.Split(' ');
                sort = Convert.ToDouble(yc[0].Split('/')[1]) + offset; //da npr C/240 V1 ne bude isto kao i 240P/NEAT i slicno...

                string code = yc[1];
                
                var match = numAlpha.Match(code);

                string codeLetters = match.Groups["letters"].Value;
                string codeDigits = match.Groups["digits"].Value;

                // pretpostavka da mogu doci najvise 2 slova u id-u
                // 1. slovo dijelim sa 100
                // 2. slovo dijelim sa 10000
                // pretpostavka da se moze pojaviti najvise troznamenkasti broj
                // njega dijelim sa 10000000

                for (int i = 0, divider = 100; i < codeLetters.Length; i++, divider *= 100)
                {
                    v += (codeLetters[i] - 64) / (double)divider;
                }

                if (codeDigits != "")
                    v += Convert.ToDouble(codeDigits) / 10000000.0;
            }

            sort += v;

            return sort;
        }

        public static double getPeriod_P(double q, double e)
        {
            if (e < 1.0)
                return Math.Pow((q / (1.0 - e)), 1.5);
            else if (e > 1.0)
                return Math.Pow((q / (e - 1.0)), 1.5);
            else //if (e == 1.0)
                return Math.Pow((q / (1 - 0.999999)), 1.5); //okvirno samo za sortiranje
        }

        public static double getSemimajorAxis_a(double q, double e)
        {
            if (e < 1.0)
                return q / (1 - e);
            else if (e > 1.0)
                return -(q / (1 - e));
            else //if (e == 1.0)
                return q / (1 - 0.999999);
        }

        public static double getAphelionDistance_Q(double e, double a)
        {

            if (e < 1.0)
                return a * (1 + e);
            else if (e > 1.0)
                return a * (1 + (2-e));
            else //if (e == 1.0) //koristi se zamo za sortiranje
                return a * (1 + 0.999999);
        }

        public static string[] setIdNameFromFull(string full)
        {
            string id = "", name = "";

            if (char.IsDigit(full[0]))
            {
                if (full.Contains('/'))
                {
                    string[] idname = full.Split('/');
                    id = idname[0];
                    name = idname[1];
                }
                else
                {
                    id = full;
                }
            }
            else
            {
                if (full.Contains('('))
                {
                    string[] idname = full.Split('(');
                    id = idname[0].Trim();
                    name = idname[1].TrimEnd(')');
                }
                else
                {
                    id = full;
                }
            }

            return new string[] {id, name};
        }

        public static string setFullFromIdName(string id, string name)
        {
            string full = id;

            if (id.Contains('/'))
            {
                if (name != "")
                {
                    full += " (" + name + ")";
                }
            }
            else
            {
                if (name != "")
                {
                    full += "/" + name;
                }
            }

            return full;
        }

        public static double GregToJul(int y, int m, int d, int h)
        {
            double hh = (double)h / 10000;
            return 367 * y - (7 * (y + (m + 9) / 12)) / 4 - ((3 * (y + (m - 9) / 7)) / 100 + 1) / 4 + (275 * m) / 9 + d + 1721029 + hh;
        }
    }
}
