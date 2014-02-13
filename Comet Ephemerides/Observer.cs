using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cometary_Workshop
{
    class Observer
    {
        public double latitude;
        public double longitude;
        public double tz;
        public bool dst;

        public Observer(double lat, double lon, double tz, bool dst)
        {
            this.latitude = lat;
            this.longitude = lon;
            this.tz = tz;
            this.dst = dst;
        }
    }
}
