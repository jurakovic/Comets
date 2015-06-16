using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Comets.Helpers
{
	public static class Utils
	{
		#region GetNthIndex

		public static int GetNthIndex(string s, char c, int n)
		{
			//http://stackoverflow.com/questions/2571716/find-nth-occurrence-of-a-character-in-a-string

			int count = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == c)
				{
					count++;
					if (count == n)
					{
						return i;
					}
				}
			}
			return -1;
		}

		#endregion

		#region ValidateKeyPress

		public static bool ValidateKeyPress(object sender, KeyPressEventArgs e, int length, int decimals)
		{
			if (length < 1)
				throw new Exception("Length must be > 0");

			string text = (sender as TextBox).Text;

			bool handle;

			if (char.IsControl(e.KeyChar) || (sender as TextBox).SelectionLength == text.Length && ((char.IsDigit(e.KeyChar))))
			{
				handle = false;
			}
			else
			{
				string pattern;

				if (decimals > 0)
					pattern = @"(^[0-9]{1," + length + @"}[.]?$)|(^[0-9]{1," + length + @"}([.][0-9]{1," + decimals + @"})?$)";
				else
					pattern = @"^[0-9]{1," + length + @"}?$";

				handle = !Regex.IsMatch(text + e.KeyChar, pattern);
			}

			return handle;
		}

		#endregion

		#region ConvertToDouble

		public static double ConvertToDouble(string str)
		{
			double retval = 0.0;

			double.TryParse(str, out retval);

			return retval;
		}

		#endregion

		#region JDToDateTime

		public static DateTime JDToDateTime(double jd)
		{
			int[] dt = EphemerisHelper.jdtocd(jd);
			return new DateTime(dt[0], dt[1], dt[2], dt[4], dt[5], dt[6]);
		}

		#endregion

		#region JDToOta

		public static double JDToOta(double jd)
		{
			return JDToDateTime(jd).ToOADate();
		}

		#endregion

		#region ControlDateTime

		public static int[] ControlDateTime(int y, int m, int d, int dmax, int hh, int mm, bool mChanged, bool yChanged)
		{
			int changed = 0;

			bool hhChanged = false;
			bool dChanged = false;

			bool dayIsMin = false;
			bool dayIsMax = false;

			if (mm >= 60)
			{
				++hh;
				mm = 0;
				hhChanged = true;
			}
			else if (mm <= -1)
			{
				--hh;
				mm = 59;
				hhChanged = true;
			}

			if (hh >= 24)
			{
				++d;
				hh = 0;
				dChanged = true;
			}
			else if (hh <= -1)
			{
				--d;
				hh = 23;
				dChanged = true;
			}

			if (d >= dmax)
			{
				++m;
				mChanged = true;
				dayIsMin = true;
			}
			else if (d <= 0)
			{
				--m;
				mChanged = true;
				dayIsMax = true;
			}

			if (mChanged || yChanged)
			{
				if (m >= 13)
				{
					++y;
					m = 1;
				}
				else if (m <= 0)
				{
					--y;
					m = 12;
				}

				int daysInMonth = DateTime.DaysInMonth(y, m);
				dmax = daysInMonth;

				if (!dayIsMin && d >= (int)dmax)
					dayIsMax = true;

				if (dayIsMin)
					d = 1;

				if (dayIsMax)
					d = daysInMonth;
			}

			if (y == 1582 && m == 10)
			{
				if (5 <= d && d < 10)
				{
					d = 15;
					dChanged = true;
				}
				else if (10 <= d && d < 15)
				{
					d = 4;
					dChanged = true;
				}
			}

			if (hhChanged || dChanged || dayIsMin || dayIsMax || mChanged || yChanged)
			{
				changed = 1;
			}

			return new int[] { y, m, d, dmax, hh, mm, changed };
		}

		#endregion
	}
}
