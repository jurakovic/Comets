
namespace Comets.BusinessLayer.Business
{
	/// <summary>
	/// TextBox Number validator
	/// </summary>
	public class ValNum
	{
		#region Enum

		public enum NameEnum { Unspecified = 0, Day, Month, Year, Hour, Minute, Second };

		#endregion

		#region Const

		//private static int DefaultValue = 1000;

		public static ValNum VDay = new ValNum(1, 31, NameEnum.Day);
		public static ValNum VMonth = new ValNum(1, 12, NameEnum.Month);
		public static ValNum VYear = new ValNum(1, 9999, NameEnum.Year);
		public static ValNum VHour = new ValNum(0, 23, NameEnum.Hour);
		public static ValNum VMinute = new ValNum(0, 59, NameEnum.Minute);
		public static ValNum VSecond = new ValNum(0, 59, NameEnum.Second);

		#endregion

		#region Fields

		double _min;
		double _max;
		int _dec;
		NameEnum _name;

		#endregion

		#region Properties

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
		public ValNum(int minimum, int maximum, NameEnum name)
		{
			_min = minimum;
			_max = maximum;
			_dec = 0;
			_name = name;
		}

		/// <summary>
		/// Minimum, Maximum constructor
		/// </summary>
		/// <param name="length"></param>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		public ValNum(int minimum, int maximum)
		{
			_min = minimum;
			_max = maximum;
			_dec = 0;
			_name = NameEnum.Unspecified;
		}

		/// <summary>
		/// Minimum, Maximum, Decimals constructor
		/// </summary>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="decimals"></param>
		public ValNum(double minimum, double maximum, int decimals)
		{
			_min = minimum;
			_max = maximum;
			_dec = decimals;
			_name = NameEnum.Unspecified;
		}

		#endregion
	}
}
