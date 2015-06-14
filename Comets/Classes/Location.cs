
namespace Comets.Classes
{
	public class Location
	{
		#region Properties

		public string Name { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int Altitude { get; set; }
		public int Offset { get; set; }
		public bool DST { get; set; }
		public double Timezone
		{
			get
			{
				return DST ? (double)(Offset + 1) : (double)Offset;
			}
		}

		#endregion

		#region Constructor

		public Location()
		{
			Name = "Home";
			Latitude = 0.0;
			Longitude = 0.0;
			Altitude = 0;
			Offset = 0;
			DST = false;
		}

		#endregion
	}
}
