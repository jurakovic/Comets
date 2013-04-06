using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cometary_Workshop
{
    public class Location
    {
        //public string name;
        public double latitude;
        public double longitude;


        public Location(double lat, double lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}
