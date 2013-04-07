using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cometary_Workshop
{
    public class Location
    {
        public string name;
        public double timezone;
        public bool dst;
        public double latitude;
        public double longitude;
        public double startJD;
        public double stopJD;
        public double interval;

        public Location(string name, double timezone, bool dst, 
            double lat, double lon, double startj, double stopj, double interval)
        {
            this.name = name;
            this.timezone = timezone;
            this.dst = dst;
            this.latitude = lat;
            this.longitude = lon;
            this.startJD = startj;
            this.stopJD = stopj;
            this.interval = interval;
        }
    }
}
