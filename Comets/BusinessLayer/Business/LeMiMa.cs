
namespace Comets.BusinessLayer.Business
{
	/// <summary>
	/// Length, Minimum, Maximum
	/// </summary>
	public class LeMiMa
	{
		public static LeMiMa LDay = new LeMiMa(2, 1, 31);
		public static LeMiMa LMonth = new LeMiMa(2, 1, 12);
		public static LeMiMa LYear = new LeMiMa(4, 1, 9999);
		public static LeMiMa LHour = new LeMiMa(2, 0, 23);
		public static LeMiMa LMinute = new LeMiMa(2, 0, 59);
		public static LeMiMa LSecond = new LeMiMa(2, 0, 59);

		public int Len { get; private set; }
		public int Min { get; private set; }
		public int Max { get; private set; }

		public LeMiMa(int len, int min, int max)
		{
			Len = len;
			Min = min;
			Max = max;
		}
	}
}
