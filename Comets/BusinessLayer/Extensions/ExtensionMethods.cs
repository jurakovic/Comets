using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Windows.Forms;

namespace Comets.BusinessLayer.Extensions
{
	public static class ExtensionMethods
	{
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
			double retval = 0;
			retval = txt.Text.Double();

			LeMiMa l = txt.Tag as LeMiMa;

			if (l != null)
			{
				if (retval < l.Min)
					retval = l.Min;

				if (retval > l.Max)
					retval = l.Max;
			}

			return retval;
		}

		/// <summary>
		/// Converts TextBox.Text value to int. Returns 0 if conversion failed.
		/// </summary>
		/// <param name="txt"></param>
		/// <returns></returns>
		public static int Int(this TextBoxBase txt)
		{
			return (int)txt.Double();
		}

		#endregion

		#region DateTime

		/// <summary>
		/// Converts DateTime to Julian day
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static double JD(this DateTime dt)
		{
			return EphemerisManager.JD(dt);
		}

		#endregion
	}
}
