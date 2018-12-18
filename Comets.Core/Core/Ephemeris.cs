
namespace Comets.Core
{
	public class Ephemeris
	{
		#region Properties

		/// <summary>
		/// Julian day
		/// </summary>
		public decimal JD { get; set; }

		/// <summary>
		/// Right Ascension
		/// </summary>
		public double RA { get; set; }

		/// <summary>
		/// Declination
		/// </summary>
		public double Dec { get; set; }

		/// <summary>
		/// Altitude
		/// </summary>
		public double Alt { get; set; }

		/// <summary>
		/// Azimuth
		/// </summary>
		public double Az { get; set; }

		/// <summary>
		/// Ecliptic Longitude
		/// </summary>
		public double EcLon { get; set; }

		/// <summary>
		/// Ecliptic Latitude
		/// </summary>
		public double EcLat { get; set; }

		/// <summary>
		/// Elongation
		/// </summary>
		public double Elongation { get; set; }

		/// <summary>
		/// Position Angle
		/// </summary>
		public double PositionAngle { get; set; }

		/// <summary>
		/// Sun distance
		/// </summary>
		public double SunDist { get; set; }

		/// <summary>
		/// Earth distance
		/// </summary>
		public double EarthDist { get; set; }

		/// <summary>
		/// Magnitude
		/// </summary>
		public double Magnitude { get; set; }

		#endregion

		#region Constructor

		public Ephemeris()
		{

		}

		#endregion
	}

	#region EphemerisChartDataPoint

	public class EphemerisChartDataPoint
	{
		/// <summary>
		/// X-axis
		/// </summary>
		public decimal JD { get; private set; }

		/// <summary>
		/// Y-axis
		/// </summary>
		public double Value { get; private set; }

		public EphemerisChartDataPoint(decimal jd, double value)
		{
			JD = jd;
			Value = value;
		}
	}

	#endregion
}
