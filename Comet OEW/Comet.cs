using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Comet_OEW
{
    public class Comet
    {
        public string full;
        public string name;
        public string id;
        public long T;
        public int y;
        public int m;
        public int d;
        public int h;
        public double P;
        public double q;
        public double e;
        public double i;
        public double an;
        public double pn;
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
            y = 0;
            m = 0;
            d = 0;
            h = 0;
            P = 0.0;
            q = 0.0;
            e = 0.0;
            i = 0.0;
            an = 0.0;
            pn = 0.0;
            g = 0.0;
            k = 0.0;
            sortkey = 0.0;
        }

        public Comet(Comet c)
        {
            full = c.full;
            name = c.name;
            id = c.id;
            T = c.T;
            y = c.y;
            m = c.m;
            d = c.d;
            h = c.h;
            P = c.P;
            q = c.q;
            e = c.e;
            i = c.i;
            an = c.an;
            pn = c.pn;
            g = c.g;
            k = c.k;
            sortkey = c.sortkey;
        }

        public void setPeriod()
        {
            if (e < 1.0) P = Math.Pow((q / (1.0 - e)), 1.5);
            if (e > 1.0) P = Math.Pow((q / (e - 1.0)), 1.5);
            if (e == 1.0) P = Math.Pow((q / (1 - 0.999999)), 1.5);
        }

        public void setT()
        {
            this.T = gregToJul(this.y, this.m, this.d);
        }

        public void setSortkey()
        {
            int oldt = total2;

            string s1, s2;
            double sort = 0.0;
            double v = 0.0;

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

            //if (oldt == total2)
            //{
            //    MessageBox.Show(full);
            //}

            sort += v;
            sort += 1000; //da npr C/240 V1 ne bude isto kao i 240P/NEAT i slicno...

            this.sortkey = sort;
        }

        public static string[] setIdNameFull(string full)
        {
            int oldt = total;

            //                   id   name  full
            string[] idname = { null, null, null };

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

            if (r1.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.IndexOf('/')); // 2P
                idname[1] = full.Substring(full.IndexOf('/') + 1); // Encke
                idname[2] = idname[0] + "/" + idname[1];
                
                //MessageBox.Show(full + " je regex r1" + r1.ToString() + " = ");
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

            else if (r3.Match(full).Success)
            {
                total++;
                idname[0] = full; // C/1750 C1
                idname[1] = "";
                idname[2] = idname[0];

                //MessageBox.Show(full + " je regex r3 " + r3.ToString() + " = ");
            }

            else if (r4.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.IndexOf('(') - 1); // C/2012 S1
                idname[1] = full.Substring(full.IndexOf('(')).Trim('(', ')'); // ISON
                idname[2] = idname[0] + " (" + idname[1] + ")";

                //MessageBox.Show(full + " je regex r4 " + r4.ToString() + " = ");
            }

            else if (r5.Match(full).Success)
            {
                total++;
                idname[0] = full; // D/1993 F2
                idname[1] = "";
                idname[2] = idname[0];

                //MessageBox.Show(full + " je regex r5 " + r5.ToString() + " = ");
            }

            else if (r6.Match(full).Success)
            {
                total++;
                idname[0] = full.Substring(0, full.LastIndexOf('-')); // D/1993 F2
                idname[1] = full.Substring(full.IndexOf('(')).Trim('(', ')'); //Shoemaker-Levy 9
                idname[1] += full.Substring(full.IndexOf('-') , full.IndexOf('(') - 1 - full.IndexOf('-') ); // 
                idname[2] = idname[0] + " (" + idname[1] + ")";

                //MessageBox.Show(full + " je regex r6 " + r6.ToString() + " = ");
            }

            if (oldt == total)
            {
                MessageBox.Show(full);
            }

            return idname;
        }

        public static long gregToJul(int y, int m, int d)
        {
            return 367 * y - (7 * (y + (m + 9) / 12)) / 4 - ((3 * (y + (m - 9) / 7)) / 100 + 1) / 4 + (275 * m) / 9 + d + 1721029;
        }
    }
}
