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
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public double Interval { get; set; }
        public List<EphemerisResult> Results { get; set; }

        #endregion
    }
}
