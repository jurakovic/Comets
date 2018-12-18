using Comets.Core.Managers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Core.Extensions
{
	public static class ExtensionMethods
	{
		#region Object

		public static bool In<T>(this T source, params T[] values)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			return values.Contains(source);
		}

		#endregion

		#region String

		/// <summary>
		/// Converts string value to double. Returns 0.0 if conversion failed.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static double Double(this string str)
		{
			double retval = 0;
			System.Double.TryParse(str.Trim(), out retval);
			return retval;
		}

		/// <summary>
		/// Converts string value to int. Returns 0 if conversion failed.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int Int(this string str)
		{
			return (int)str.Double();
		}

		#endregion

		#region TextBox

		/// <summary>
		/// Converts TextBox.Text value to double. Returns 0.0 if conversion failed.
		/// </summary>
		/// <param name="txt"></param>
		/// <returns></returns>
		public static double Double(this TextBoxBase txt)
		{
			return txt.Text.Double();
		}

		/// <summary>
		/// Converts TextBox.Text value to int. Returns 0 if conversion failed.
		/// </summary>
		/// <param name="txt"></param>
		/// <returns></returns>
		public static int Int(this TextBoxBase txt)
		{
			return (int)txt.Text.Double();
		}

		#endregion

		#region DateTime

		/// <summary>
		/// Converts DateTime to Julian day
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static decimal JD(this DateTime dt)
		{
			return EphemerisManager.JD(dt);
		}

		/// <summary>
		/// Returns UTC offset
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static double Timezone(this DateTime dt)
		{
			return TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours;
		}

		#endregion
	}
}
