
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

		private static int DefaultValue = 1000;

		public static LeMiMa LDay = new LeMiMa(NameEnum.Day, 2, 1, 31);
		public static LeMiMa LMonth = new LeMiMa(NameEnum.Month, 2, 1, 12);
		public static LeMiMa LYear = new LeMiMa(NameEnum.Year, 4, 1, 9999);
		public static LeMiMa LHour = new LeMiMa(NameEnum.Hour, 2, 0, 23);
		public static LeMiMa LMinute = new LeMiMa(NameEnum.Minute, 2, 0, 59);
		public static LeMiMa LSecond = new LeMiMa(NameEnum.Second, 2, 0, 59);

		#endregion

		#region Fields

		int _len;
		double _min;
		double _max;
		int _dec;
		NameEnum _name;

		#endregion

		#region Properties

		/// <summary>
		/// Length
		/// </summary>
		public int Len
		{
			get { return _len; }
		}

		/// <summary>
		/// Integer Minimum
		/// </summary>
		public int IMin
		{
			get { return (int)_min; }
		}

		/// <summary>
		/// Integer Maximum
		/// </summary>
		public int IMax
		{
			get { return (int)_max; }
		}

		/// <summary>
		/// Double Minimum
		/// </summary>
		public double DMin
		{
			get { return _min; }
		}

		/// <summary>
		/// Double Maximum
		/// </summary>
		public double DMax
		{
			get { return _max; }
		}

		/// <summary>
		/// Decimals
		/// </summary>
		public int Decimals
		{
			get { return _dec; }
		}

		/// <summary>
		/// Name
		/// </summary>
		public NameEnum Name
		{
			get { return _name; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for Date Control
		/// </summary>
		/// <param name="length"></param>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="name"></param>
		public LeMiMa(NameEnum name, int length, int minimum, int maximum)
		{
			_name = name;
			_len = length;
			_min = minimum;
			_max = maximum;
			_dec = DefaultValue;
		}

		/// <summary>
		/// Length, Minimum, Maximum constructor
		/// </summary>
		/// <param name="length"></param>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		public LeMiMa(int length, int minimum, int maximum)
		{
			_len = length;
			_min = minimum;
			_max = maximum;
			_dec = DefaultValue;
			_name = NameEnum.Unspecified;
		}

		/// <summary>
		/// Minimum, Maximum, Decimals constructor
		/// </summary>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="decimals"></param>
		public LeMiMa(double minimum, double maximum, int decimals)
		{
			_len = DefaultValue;
			_min = minimum;
			_max = maximum;
			_dec = decimals;
			_name = NameEnum.Unspecified;
		}

		#endregion
	}
}
