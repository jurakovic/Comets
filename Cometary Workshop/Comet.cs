using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Cometary_Workshop
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
        public double N;   //Longitude of the Ascending Node
        public double w;    //Argument of Pericenter
        public double a;    //Semimajor Axis
        public double Q;    //Aphelion Distance
        public double g;
        public double k;
        public double sortkey;

        public static int total = 0;
        public static int total2 = 0;

        public Comet()
        {
            full = null;
            name = null;
            id = null;
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

        public void get_sortkey()
        {
            int oldt = total2;

            string s1, s2;
            double sort = 0.0;
            double v = 0.0;

            //282P
            Regex r0 = new Regex(@"^[0-9]+[PD]");

            // 2P/Encke
            Regex r1 = new Regex(@"^[0-9]+[PD]/");

            // C/1995 O1 (Hale-Bopp)
            Regex r2 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][0-9]");

            // C/2001 U10 (SOHO)
            Regex r3 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][0-9][0-9]");

            // P/2005 JN (Spacewatch)
            Regex r4 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][A-Z]");

            // C/1997 BA6
            Regex r5 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][A-Z][0-9]");

            // P/1998 VS24 (LINEAR)
            Regex r6 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][A-Z][0-9][0-9]");

            // P/1999 XN120 (Catalina)
            Regex r7 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z][A-Z][0-9][0-9][0-9]");

            // 1. slovo dijelim sa 100
            // 2. slovo dijelim sa 10000
            // brojeve dijelim sa 10000000
            double prvoSlovo = 100.0;
            double drugoSlovo = 10000.0;
            double broj = 10000000.0;

            if (r7.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;
                v += (id[++spaceIndex] - 64) / drugoSlovo;

                s2 = id.Substring(++spaceIndex, 3);
                v += Convert.ToDouble(s2) / broj;
            }

            else if (r6.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;
                v += (id[++spaceIndex] - 64) / drugoSlovo;

                s2 = id.Substring(++spaceIndex, 2);
                v += Convert.ToDouble(s2) / broj;
            }

            else if (r5.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;
                v += (id[++spaceIndex] - 64) / drugoSlovo;
                v += (id[++spaceIndex] - 48) / broj;
            }

            else if (r4.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;
                v += (id[++spaceIndex] - 64) / drugoSlovo;
            }

            else if (r3.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;

                s2 = id.Substring(++spaceIndex, 2);
                v += Convert.ToDouble(s2) / broj;
            }

            else if (r2.Match(full).Success)
            {
                total2++;

                int spaceIndex = id.IndexOf(' ');
                s1 = id.Substring(2, spaceIndex - 2);
                sort = Convert.ToDouble(s1);

                v = (id[++spaceIndex] - 64) / prvoSlovo;
                v += (id[++spaceIndex] - 48) / broj;
            }

            else if (r1.Match(full).Success)
            {
                total2++;

                s1 = id.Substring(0, id.Length - 1);
                sort = Convert.ToDouble(s1);
                sort -= 1000;
            }

            else if (r0.Match(full).Success)
            {
                total2++;

                s1 = id.Substring(0, id.Length - 1);
                sort = Convert.ToDouble(s1);
                sort -= 1000;
            }

            //if (oldt == total2)
            //{
            //    MessageBox.Show(full);
            //}

            sort += v;
            sort += 1000; //da npr C/240 V1 ne bude isto kao i 240P/NEAT i slicno...

            this.sortkey = sort;
        }

        public static double getPeriod_P(double q, double e)
        {
            if (e < 1.0)
                return Math.Pow((q / (1.0 - e)), 1.5);
            else if (e > 1.0)
                return Math.Pow((q / (e - 1.0)), 1.5);
            else //if (e == 1.0)
                return Math.Pow((q / (1 - 0.999999)), 1.5);
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
            else //if (e == 1.0)
                return a * (1 + 0.999999);
        }

        public static string[] setIdNameFull(string full)
        {
            int oldt = total;

            //                   id   name  full
            //string[] idname = { null, null, null };
            string[] idname = { "", "", "" };

            //282P
            Regex r0 = new Regex(@"^[0-9]+[PD]"); // |282P|

            // 2P/Encke
            Regex r1 = new Regex(@"^[0-9]+[PD]/"); // |2P/|

            // 128P-B/Shoemaker-Holt
            Regex r2 = new Regex(@"^[0-9]+[PD]-[A-Z]+"); // |128P-B|

            // C/1750 C1, C/-146 P1
            Regex r3 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z]+[0-9]*$"); // |C/1750 C1|

            // C/2012 S1 (ISON)
            Regex r4 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z]+[0-9]* [(]"); // |C/2012 S1 (|

            //D/-146 P1-G
            Regex r5 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z]+[0-9]*-[A-Z]*[0-9]*$");

            // D/1993 F2-N (Shoemaker-Levy 9), D/1993 F2-P1 (Shoemaker-Levy 9)
            Regex r6 = new Regex(@"^[CPD]/-?[0-9]+ [A-Z]+[0-9]*-."); // |D/1993 F2-|


            if (r6.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.LastIndexOf('-')); // D/1993 F2
                idname[1] = full.Substring(full.IndexOf('(')).Trim('(', ')'); //Shoemaker-Levy 9
                idname[1] += full.Substring(full.IndexOf('-'), full.IndexOf('(') - 1 - full.IndexOf('-')); // 
                idname[2] = idname[0] + " (" + idname[1] + ")";

                //MessageBox.Show(full + " je regex r6 " + r6.ToString() + " = ");
            }

            else if (r5.Match(full).Success)
            {
                total++;
                idname[0] = full; // D/1993 F2
                idname[1] = "";
                idname[2] = idname[0];

                //MessageBox.Show(full + " je regex r5 " + r5.ToString() + " = ");
            }

            else if (r4.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.IndexOf('(') - 1); // C/2012 S1
                idname[1] = full.Substring(full.IndexOf('(')).Trim('(', ')'); // ISON
                idname[2] = idname[0] + " (" + idname[1] + ")";

                //MessageBox.Show(full + " je regex r4 " + r4.ToString() + " = ");
            }

            else if (r3.Match(full).Success)
            {
                total++;
                idname[0] = full; // C/1750 C1
                idname[1] = "";
                idname[2] = idname[0];

                //MessageBox.Show(full + " je regex r3 " + r3.ToString() + " = ");
            }

            else if (r2.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.IndexOf('-')); // 128P
                idname[1] = full.Substring(full.IndexOf('/') + 1); // Shoemaker-Holt
                idname[1] += full.Substring(full.IndexOf('-'), full.IndexOf('/') - full.IndexOf('-')); // Shoemaker-Holt-B
                idname[2] = idname[0] + "/" + idname[1];

                //MessageBox.Show(full + " je regex r2 " + r2.ToString() + " = ");
            }

            else if (r1.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.IndexOf('/')); // 2P
                idname[1] = full.Substring(full.IndexOf('/') + 1); // Encke
                idname[2] = idname[0] + "/" + idname[1];

                //MessageBox.Show(full + " je regex r1" + r1.ToString() + " = ");
            }

            else if (r0.Match(full).Success)
            {
                total++;
                idname[0] = full;
                idname[1] = "";
                idname[2] = full;

                //MessageBox.Show(full + " je regex r1" + r1.ToString() + " = ");
            } 

            //if (oldt == total)
            //{
            //    MessageBox.Show(full);
            //}

            return idname;
        }

        public static double GregToJul(int y, int m, int d, int h)
        {
            double hh = (double)h / 10000;
            return 367 * y - (7 * (y + (m + 9) / 12)) / 4 - ((3 * (y + (m - 9) / 7)) / 100 + 1) / 4 + (275 * m) / 9 + d + 1721029 + hh;
        }
    }
}
