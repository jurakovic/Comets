using Comets.Forms;
using System;
using System.Collections.Generic;

namespace Comets.Classes
{
    public class CommonSettings
    {
        #region Properties

        public Location Location { get { return FormMain.Settings.Location; } }
        public Comet Comet { get; set; }
        public string MinText { get; set; }
        public string MaxText { get; set; }
        public double MinLocalJD { get; set; }
        public double MaxLocalJD { get; set; }
        public double MinUtcJD { get; set; }
        public double MaxUtcJD { get; set; }
        public double Interval { get; set; }
        public List<EphemerisResult> Results { get; set; }

        #endregion
    }
}
