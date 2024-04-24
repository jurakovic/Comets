
namespace Comets.OrbitViewer
{
	public class ATimeSpan
	{
		#region Properties

		public int Year { get; private set; }
		public int Month { get; private set; }
		public int Day { get; private set; }
		public int Hour { get; private set; }
		public int Minute { get; private set; }
		public int Second { get; private set; }

		#endregion

		#region Constructor

		public ATimeSpan(int year, int month, int day, int hour, int min, int sec)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
			this.Hour = hour;
			this.Minute = min;
			this.Second = sec;
		}

		#endregion
	}
}