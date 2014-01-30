using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cometary_Workshop
{
    class Observer
    {
        public class Location
        {
            public string name;
            public double latitude;
            public bool ns;
            public double longitude;
            public bool we;
            public int zone;
            public string dss;
            public string dse;


            public Location(string name, double latitude, bool ns, double longitude, bool we, int zone, string dss, string dse)
            {
                this.name = name;
                this.latitude = latitude;
                this.ns = ns;
                this.longitude = longitude;
                this.we = we;
                this.zone = zone;
                this.dss = dss;
                this.dse = dse;
            }
        }


        public string name;
        public int year;
        public int month;
        public int day;
        public int hours;
        public int minutes;
        public int seconds;
        public int tz;
        public bool dst;
        public double latitude;
        public double longitude;

        public Observer(Location place, int year, int month, int day, int hr, int min, int sec)
        {
            // The observatory object holds local date and time,
            // timezone correction in minutes with daylight saving if applicable,
            // latitude and longitude (west is positive)
            this.name = place.name;
            this.year = year;
            this.month = month;
            this.day = day;
            this.hours = hr;
            this.minutes = min;
            this.seconds = sec;
            this.tz = place.zone;
            this.dst = false;	// is it DST?
            this.latitude = place.latitude;
            this.longitude = place.longitude;
        }
    }
}
