using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comets.BusinessLayer.Business
{
	public	class OrbitViewerSettings
	{
		#region Properties

		public bool MultipleMode { get; set; }
		public bool EclipticAxis { get; set; }
		public bool Antialiasing { get; set; }
		public bool ShowPlanetName { get; set; }
		public bool ShowCometName { get; set; }
		public bool ShowMagnitute { get; set; }
		public bool ShowDistance { get; set; }
		public bool ShowDate { get; set; }
		
		#endregion

		#region Constructor

		public OrbitViewerSettings()
		{
			MultipleMode = false;
			EclipticAxis = true;
			Antialiasing = false;
			ShowPlanetName = true;
			ShowCometName = true;
			ShowMagnitute = true;
			ShowDistance = true;
			ShowDate = true;
		}

		#endregion
	}
}
