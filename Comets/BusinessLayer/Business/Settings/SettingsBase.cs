using Comets.OrbitViewer;
using System.Collections.Generic;

namespace Comets.BusinessLayer.Business
{
	public class SettingsBase
	{
		#region Properties

		public Location Location { get; set; }
		public List<Comet> Comets { get; set; }
		public Comet SelectedComet { get; set; }
		public ATime Start { get; set; }
		public ATime Stop { get; set; }
		public double Interval { get; set; }
		public List<EphemerisResult> Results { get; set; }

		#endregion
	}
}
