using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cometary_Workshop
{
    public class Ephemeris
    {
        public string name;
        public DateTime date;
        public string ra;
        public string dec;
        public string alt;
        public string az;
        public string elong;
        public string r; //sun distance
        public string d; //earth distance
        public string mag;


        //class comet_xyz
        //{
        //    public double xh, yh, zh, r, lonecl, latecl;
        //}

        public double[] comet_xyz(int cn, double jday)
        {
            // heliocentric xyz for comet (cn is index to comets)
            // based on Paul Schlyter's page http://www.stjarnhimlen.se/comp/ppcomp.html
            // returns heliocentric x, y, z, distance, longitude and latitude of object
            double d = jday - 2451543.5;
            Comet cm = Form1.userList.ElementAt(cn);
            double q = cm.q;
            double e = cm.e;
            double Tj = jd0(cm.Ty, cm.Tm, cm.Td, cm.Th);	// get julian day of perihelion time

            double v = 0.0, r = 0.0;
            if (e > 0.98)
            {
                // treat as near parabolic (approx. method valid inside orbit of Pluto)
                double k = 0.01720209895;	// Gaussian gravitational constant
                double a = 0.75 * (jday - Tj) * k * Math.Sqrt((1 + e) / (q * q * q));
                double b = Math.Sqrt(1 + a * a);
                double W = cbrt(b + a) - cbrt(b - a);
                double c = 1 + 1 / (W * W);
                double f = (1 - e) / (1 + e);
                double g = f / (c * c);
                double a1 = (2 / 3) + (2 / 5) * W * W;
                double a2 = (7 / 5) + (33 / 35) * W * W + (37 / 175) * W * W * W * W;
                double a3 = W * W * ((432 / 175) + (956 / 1125) * W * W + (84 / 1575) * W * W * W * W);
                double w = W * (1 + g * c * (a1 + a2 * g + a3 * g * g));
                v = 2 * atand(w);
                r = q * (1 + w * w) / (1 + w * w * f);
            }
            else
            {		// treat as elliptic
                double a = q / (1.0 - e);
                double P = 365.2568984 * Math.Sqrt(a * a * a);	// period in days
                double M = 360.0 * (jday - Tj) / P; 	// mean anomaly
                // eccentric anomaly E
                double E0 = M + RAD2DEG * e * sind(M) * (1.0 + e * cosd(M));
                double E1 = E0 - (E0 - RAD2DEG * e * sind(E0) - M) / (1.0 - e * cosd(E0));
                while (Math.Abs(E0 - E1) > 0.0005)
                {
                    E0 = E1;
                    E1 = E0 - (E0 - RAD2DEG * e * sind(E0) - M) / (1.0 - e * cosd(E0));
                }
                double xv = a * (cosd(E1) - e);
                double yv = a * Math.Sqrt(1.0 - e * e) * sind(E1);
                v = rev(atan2d(yv, xv));		// true anomaly
                r = Math.Sqrt(xv * xv + yv * yv);	// distance
            }	// from here common for all orbits
            double N = cm.N + 3.82394E-5 * d;	// precess from J2000.0 to now;
            double ww = cm.w;	// why not precess this value?
            double i = cm.i;
            double xh = r * (cosd(N) * cosd(v + ww) - sind(N) * sind(v + ww) * cosd(i));
            double yh = r * (sind(N) * cosd(v + ww) + cosd(N) * sind(v + ww) * cosd(i));
            double zh = r * (sind(v + ww) * sind(i));
            double lonecl = atan2d(yh, xh);
            double latecl = atan2d(zh, Math.Sqrt(xh * xh + yh * yh + zh * zh));

            return new double[] { xh, yh, zh, r, lonecl, latecl };
        }	// comet_xyz()

        double[] sunxyz(double jday)
        {
            // return x,y,z ecliptic coordinates, distance, true longitude
            // days counted from 1999 Dec 31.0 UT
            double d = jday - 2451543.5;
            double w = 282.9404 + 4.70935E-5 * d;		// argument of perihelion
            double e = 0.016709 - 1.151E-9 * d;
            double M = rev(356.0470 + 0.9856002585 * d); // mean anomaly
            double E = M + e * RAD2DEG * sind(M) * (1.0 + e * cosd(M));
            double xv = cosd(E) - e;
            double yv = Math.Sqrt(1.0 - e * e) * sind(E);
            double v = atan2d(yv, xv);		// true anomaly
            double r = Math.Sqrt(xv * xv + yv * yv);	// distance
            double lonsun = rev(v + w);	// true longitude
            double xs = r * cosd(lonsun);		// rectangular coordinates, zs = 0 for sun 
            double ys = r * sind(lonsun);
            return new double[] { xs, ys, 0, r, lonsun, 0 };
        }


        double[] CometAlt(double jday, Observer obs)
        {
            // Alt/Az, hour angle, ra/dec, ecliptic long. and lat, illuminated fraction (=1.0), dist(Sun), dist(Earth), brightness of planet p
            int cn = 0;
            //int cn = Form1.getCometListboxIndex();
            double[] sun_xyz = sunxyz(jday);
            double[] planet_xyz = comet_xyz(cn, jday);
            double dx = planet_xyz[0] + sun_xyz[0];
            double dy = planet_xyz[1] + sun_xyz[1];
            double dz = planet_xyz[2] + sun_xyz[2];
            double lon = rev(atan2d(dy, dx));
            double lat = atan2d(dz, Math.Sqrt(dx * dx + dy * dy));
            double[] radec = radecr(planet_xyz, sun_xyz, jday, obs);
            double ra = radec[0];
            double dec = radec[1];
            double[] altaz = radec2aa(ra, dec, jday, obs);
            double dist = radec[2];
            double r = planet_xyz[3];
            double mag = Form1.userList.ElementAt(cn).G + 5 * log10(dist) + 2.5 * Form1.userList.ElementAt(cn).H * log10(r);	// Schlyter's formula is wrong!
            return new double[]{altaz[0], altaz[1], altaz[2], ra, dec, lon, lat, 1.0, r, dist, mag};
        }	// CometAlt()

        double[] radecr(double[] obj, double[] sun, double jday, Observer obs)
        {
            // radecr returns ra, dec and earth distance
            // obj and sun comprise Heliocentric Ecliptic Rectangular Coordinates
            // (note Sun coords are really Earth heliocentric coordinates with reverse signs)
            // Equatorial geocentric co-ordinates
            double xg = obj[0] + sun[0];
            double yg = obj[1] + sun[1];
            double zg = obj[2];
            // Obliquity of Ecliptic (exponent corrected, was E-9!)
            double obl = 23.4393 - 3.563E-7 * (jday - 2451543.5);
            // Convert to eq. co-ordinates
            double x1 = xg;
            double y1 = yg * cosd(obl) - zg * sind(obl);
            double z1 = yg * sind(obl) + zg * cosd(obl);
            // RA and dec (33.2)
            double ra = rev(atan2d(y1, x1));
            double dec = atan2d(z1, Math.Sqrt(x1 * x1 + y1 * y1));
            double dist = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            return new double[] { ra, dec, dist };
        }	// radecr()


        double[] radec2aa(double ra, double dec, double jday, Observer obs)
        {
            // Convert ra/dec to alt/az, also return hour angle, azimut = 0 when north
            // DOES NOT correct for parallax!
            // TH0=Greenwich sid. time (eq. 12.4), H=hour angle (chapter 13)
            double TH0 = 280.46061837 + 360.98564736629 * (jday - 2451545.0);
            double H = rev(TH0 - obs.longitude - ra);
            double alt = asind(sind(obs.latitude) * sind(dec) + cosd(obs.latitude) * cosd(dec) * cosd(H));
            double az = atan2d(sind(H), (cosd(H) * sind(obs.latitude) - tand(dec) * cosd(obs.latitude)));
            return new double[] { alt, rev(az + 180.0), H };
        }	// radec2aa()

        double DEG2RAD = Math.PI / 180.0;
        double RAD2DEG = 180.0 / Math.PI;

        double rev(double angle) { return angle - Math.Floor(angle / 360.0) * 360.0; }		// 0<=a<360
        double rev2(double angle) { double a = rev(angle); return (a >= 180 ? a - 360.0 : a); }	// -180<=a<180
        double sind(double angle) { return Math.Sin(angle * DEG2RAD); }
        double cosd(double angle) { return Math.Cos(angle * DEG2RAD); }
        double tand(double angle) { return Math.Tan(angle * DEG2RAD); }
        double asind(double c) { return RAD2DEG * Math.Asin(c); }
        double acosd(double c) { return RAD2DEG * Math.Acos(c); }
        double atand(double c) { return RAD2DEG * Math.Atan(c); }
        double atan2d(double y, double x) { return RAD2DEG * Math.Atan2(y, x); }

        double log10(double x) { return 0.43429448190325182765112891891661 * Math.Log(x); } // 0.43429 is equivalent to javascript Math.LOG10E

        double sqr(double x) { return x * x; }
        double cbrt(double x) { return Math.Pow(x, 1 / 3.0); }

        double SGN(double x) { return (x < 0) ? -1 : +1; }

        double jd0(double year, double month, double day, double hour)
        {
            // The Julian date at 0 hours(*) UT at Greenwich
            // (*) or actual UT time if day comprises time as fraction
            double y = year;
            double m = month;
            if (m < 3) { m += 12; y -= 1; }
            double a = Math.Floor(y / 100);
            double b = 2 - a + Math.Floor(a / 4);
            double j = Math.Floor(365.25 * (y + 4716)) + Math.Floor(30.6001 * (m + 1)) + day + (hour / 10000) + b - 1524.5;
            return j;
        }	// jd0()
    }
}
