using System;
using System.Collections.Generic;

namespace Comets.BusinessLayer.Business
{
	public class SettingsBase
	{
		#region Properties

		public Location Location { get; set; }
		public List<Comet> Comets { get; set; }
		public Comet SelectedComet { get; set; }
		public bool IsMultipleMode { get; set; }
		public FilterCollection Filters { get; set; }
		public string SortProperty { get; set; }
		public bool SortAscending { get; set; }
		public DateTime Start { get; set; }
		public DateTime Stop { get; set; }
		public double Interval { get; set; }
		public Dictionary<Comet, List<EphemerisResult>> Results { get; set; }
		public bool AddNew { get; set; }

		#endregion
	}
}
