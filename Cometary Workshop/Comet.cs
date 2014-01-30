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
        //public double a;    //Semimajor Axis
        //public double n;    //Mean Motion
        //public double M;    //Mean Anomaly
        //public double E;    //Eccentric Anomaly
        //public double v;    //True Anomaly
        //public double L;    //Mean Longitude
        //public double Q;    //Aphelion Distance
        //public double bw;   //Longitude of Pericenter
        //public double l;    //True longitude l = v + bw;
        //public double F;    //eccentric longitude F = w + om + E;
        public double H;
        public double G;
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
            //a = 0.0;
            //n = 0.0;
            //M = 0.0;
            //E = 0.0;
            //v = 0.0;
            //Q = 0.0;
            H = 0.0;
            G = 0.0;
            sortkey = 0.0;
        }

        public void set_sortkey()
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
            else
                return 0.0;
        }

        public static double getMeanMotion_n(double e, double P)
        {
            if (e < 1.0)
                return 0.9856076686 / P; // Gaussian gravitational constant (degrees)
            else
                return 0.0;
        }

        public static double getMeanAnomaly_M(double T, double t, double e, double n, double q)
        {
            double M = 0.0;

            if (e < 1.0)
            {
                //http://en.wikipedia.org/wiki/Epoch_%28astronomy%29#Epoch_versus_equinox

                double delta = t - T;

                M = delta * n;
                M = NormalizeDegrees(M);
            }
            else
            {
                double delta = (t - T) * 365.256363004; //delta mora bit u sidereal year

                M = (13.3286488 * delta) / Math.Pow(q, 3 / 2);
                M = NormalizeDegrees(M);
            }
            return M;
        }

        public static double getEccentricAnomaly_E(double e, double M)
        {
            if (e < 1.0)
                return kepler(e, M);
            else
                return 0.0;
        }

        public static double getTrueAnomaly_v(double e, double E, double q, double T, double t)
        {
            double v = 0.0;

            if (e < 1.0)
            {
                E = DegToRad(E);
                v = Math.Sqrt((1.0 + e) / (1.0 - e)) * Math.Tan(E / 2.0);
                v = 2.0 * Math.Atan(v);
                v = RadToDeg(v);
            }
            else if (e == 1.0)
            {
                double s = barker(q, T, t);
                v = 2.0 * Math.Atan(s);
                v = RadToDeg(v);
            }
            else
            {
                double s, Q, gama;

                Q = (0.01720209895 / (2 * q)) * Math.Sqrt((1 + e) / q);
                gama = (1 - e) / (1 + e);

                s = hyp_barker(Q, gama, T, t);
                v = 2.0 * Math.Atan(s);
                v = RadToDeg(v);
            }

            return v;
        }

        public static double getMeanLongitude_L(double M, double om, double w)
        {
            return NormalizeDegrees(M + om + w);
        }

        public static double getAphelionDistance_Q(double e, double a)
        {
            if (e < 1.0)
                return a * (1 + e);
            else
                return 0.0;
        }

        public static double getLongitudeOfPericenter_bw(double om, double w)
        {
            return NormalizeDegrees(om + w);
        }

        public static double getTrueLongitude_l(double v, double bw)
        {
            return NormalizeDegrees(v + bw);
        }

        public static double getEccentricLongitude_F(double w, double om, double E)
        {
            return NormalizeDegrees(w + om + E);
        }

        public static double kepler(double e, double M)
        {
            //http://orbitsimulator.com/sheela/kepler.htm -> source
            //double diff = 1.0;
            //double E0 = M;
            //double E = 0;
            //while (diff > 0.0000001)
            //{
            //    E = E0 - (E0 - e * (Math.Sin(E0)) - DegToRad(M)) / (1.0 - e * Math.Cos(E0));
            //    diff = Math.Abs(E0 - E);
            //    E0 = E;
            //}
            //return NormalizeDegrees(RadToDeg(E));

            double M_PI_2 = 1.5707963267948966192313216916398;
            double M_PI_4 = 0.78539816339744830961566084581988;
            double M_PI = 3.1415926535897932384626433832795;
            int KEPLER_STEPS = 53;
            double Eo = M_PI_2;
            double F, M1;
            double D = M_PI_4;
            int i;

            /* covert to radians */
            M = DegToRad(M);

            F = Math.Sign(M);
            M = Math.Abs(M) / (2.0 * M_PI);
            M = (M - (int)M) * 2.0 * M_PI * F;

            if (M < 0)
                M = M + 2.0 * M_PI;
            F = 1.0;

            if (M > M_PI)
                F = -1.0;

            if (M > M_PI)
                M = 2.0 * M_PI - M;

            for (i = 0; i < KEPLER_STEPS; i++)
            {
                M1 = Eo - e * Math.Sin(Eo);
                Eo = Eo + D * Math.Sign(M - M1);
                D /= 2.0;
            }
            Eo *= F;

            /* back to degrees */
            Eo = RadToDeg(Eo);
            return Eo;
        }

        public static double barker(double q, double T, double t)
        {
            double delta = t - T;
            double G, Y, W, S;

            W = ((0.03649116245) / (q * Math.Sqrt(q))) * delta;
            G = W / 2.0;
            Y = Math.Pow((G + Math.Sqrt(G * G + 1)), 1.0 / 3.0);
            S = Y - 1 / Y;

            return S;
        }

        public static double hyp_barker(double Q1, double G, double T, double t)
        {
            double PREC = 0.0000001;
            double S, S0, S1, Y, G1, Q2, Q3, Z1, F;
            double t1 = T - t;
            int Z, L;

            Q2 = Q1 * t1;
            S = 2 / (3 * Math.Abs(Q2));
            S = 2 / Math.Tan(2 * Math.Atan(Math.Pow((Math.Tan(Math.Atan(S) / 2)), 1 / 3)));

            if (t1 < 0)
                S = -S;
            L = 0;

            do
            {
                S0 = S;
                Z = 1;
                Y = S * S;
                G1 = -Y * S;
                Q3 = Q2 + 2.0 * G * S * Y / 3.0;

            next_z:
                Z++;
                G1 = -G1 * G * Y;
                Z1 = (Z - (Z + 1) * G) / (2.0 * Z + 1.0);
                F = Z1 * G1;
                Q3 = Q3 + F;

                if (Z > 100 || Math.Abs(F) > 10000)
                    return 0.0;

                if (Math.Abs(F) > PREC)
                    goto next_z;

                L++;

                if (L > 100)
                    return 0.0;

                do
                {
                    S1 = S;
                    S = (2 * S * S * S / 3 + Q3) / (S * S + 1);
                } while (Math.Abs(S - S1) > PREC);

            } while (Math.Abs(S - S0) > PREC);

            return S;
        }

        public static double RadToDeg(double radAngle)
        {
            return radAngle * (180.0 / Math.PI);
        }

        public static double DegToRad(double degAngle)
        {
            return Math.PI * degAngle / 180.0;
        }

        public static double NormalizeDegrees(double deg)
        {
            deg %= 360;

            while (deg < 0)
            {
                deg += 360;
            }

            while (deg >= 360)
            {
                deg -= 360;
            }

            return deg;
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
