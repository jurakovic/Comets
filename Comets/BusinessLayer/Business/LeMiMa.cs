
namespace Comets.BusinessLayer.Business
{
	/// <summary>
	/// Length, Minimum, Maximum
	/// </summary>
	public class LeMiMa
	{
		#region Enum

		public enum NameEnum { Unspecified = 0, Day, Month, Year, Hour, Minute, Second };

		#endregion

		#region Const

		public static LeMiMa LDay = new LeMiMa(2, 1, 31, NameEnum.Day);
		public static LeMiMa LMonth = new LeMiMa(2, 1, 12, NameEnum.Month);
		public static LeMiMa LYear = new LeMiMa(4, 1, 9999, NameEnum.Year);
		public static LeMiMa LHour = new LeMiMa(2, 0, 23, NameEnum.Hour);
		public static LeMiMa LMinute = new LeMiMa(2, 0, 59, NameEnum.Minute);
		public static LeMiMa LSecond = new LeMiMa(2, 0, 59, NameEnum.Second);

		#endregion

		#region Properties

		public int Len { get; private set; }
		public int Min { get; private set; }
		public int Max { get; private set; }
		public NameEnum Name { get; private set; }

		#endregion

		#region Constructor

		public LeMiMa(int len, int min, int max, NameEnum name = NameEnum.Unspecified)
		{
			Len = len;
			Min = min;
			Max = max;
			Name = name;
		}

		#endregion
	}
}
