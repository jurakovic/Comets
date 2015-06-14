using Comets.Forms;
using Comets.OrbitViewer;
using System.Collections.Generic;

namespace Comets.Classes
{
	public class CommonSettings
	{
		#region Properties

		public Location Location { get { return FormMain.Settings.Location; } }
		public Comet Comet { get; set; }
		public ATime Start { get; set; }
		public ATime Stop { get; set; }
		public double Interval { get; set; }
		public List<EphemerisResult> Results { get; set; }

		#endregion
	}
}
