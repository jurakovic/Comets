
namespace Comets.Core
{
	public class Location
	{
		#region Properties

		public string Name { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		#endregion

		#region Constructor

		public Location()
		{
			Name = "Home";
			Latitude = 0.0;
			Longitude = 0.0;
		}

		#endregion
	}
}
