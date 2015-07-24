namespace Comets.BusinessLayer.Business
{
	public class OrbitViewerSettings
	{
		#region Properties

		public bool MultipleMode { get; set; }
		public bool ShowAxes { get; set; }
		public bool Antialiasing { get; set; }
		public bool ShowPlanetName { get; set; }
		public bool ShowCometName { get; set; }
		public bool ShowMagnitute { get; set; }
		public bool ShowDistance { get; set; }
		public bool ShowDate { get; set; }
		public bool PreserveSelected { get; set; }

		#endregion

		#region Constructor

		public OrbitViewerSettings()
		{
			MultipleMode = false;
			ShowAxes = false;
			Antialiasing = false;
			ShowPlanetName = true;
			ShowCometName = true;
			ShowMagnitute = true;
			ShowDistance = true;
			ShowDate = true;
			PreserveSelected = false;
		}

		#endregion
	}
}
