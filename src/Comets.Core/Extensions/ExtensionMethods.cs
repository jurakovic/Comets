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

		#region IComparable

		/// <summary>
		/// Returns the value in the specified range.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static T Range<T>(this T value, T min, T max) where T : IComparable
		{
			return value.CompareTo(max) > 0 ? max : value.CompareTo(min) < 0 ? min : value;
		}

		#endregion

		#region Controls

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

		/// <summary>
		/// Inverts Control.Visible property (Visible = !Visible)
		/// </summary>
		/// <param name="cbx"></param>
		public static void InvertVisible(this Control control)
		{
			control.Visible = !control.Visible;
		}

		/// <summary>
		/// Inverts CheckBox.Checked property (Checked = !Checked)
		/// </summary>
		/// <param name="cbx"></param>
		public static void InvertChecked(this CheckBox cbx)
		{
			cbx.Checked = !cbx.Checked;
		}

		/// <summary>
		/// Inverts MenuItem.Checked property (Checked = !Checked)
		/// </summary>
		/// <param name="cbx"></param>
		public static void InvertChecked(this MenuItem item)
		{
			item.Checked = !item.Checked;
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
