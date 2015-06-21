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

		#region HandleKeyPress

		public static bool HandleKeyPress(object sender, KeyPressEventArgs e, int length, int decimals, double? minimum = null, double? maximum = null)
		{
			if (length < 1)
				throw new Exception("Length must be greather than 0");

			if (minimum.GetValueOrDefault() > maximum.GetValueOrDefault())
				throw new Exception("Minimum can not be greather than maximum");

			TextBox textbox = sender as TextBox;

			bool negative = minimum.GetValueOrDefault() < 0;
			string text;
			bool handle;

			if (textbox.SelectionLength > 0)
				text = textbox.Text.Replace(textbox.SelectedText, Char.IsControl(e.KeyChar) ? String.Empty : e.KeyChar.ToString());
			else
				text = textbox.Text + (Char.IsControl(e.KeyChar) ? String.Empty : e.KeyChar.ToString());

			string pattern = "^";

			if (negative)
				pattern += "-?";

			pattern += "[0-9]{0," + length + "}[.]?$";

			if (decimals > 0)
			{
				pattern = "(" + pattern + ")|(^";

				if (negative)
					pattern += "-?";

				pattern += "[0-9]{0," + length + "}([.][0-9]{0," + decimals + "})?$)";
			}

			handle = !Regex.IsMatch(text, pattern);

			if (minimum != null && Char.IsDigit(e.KeyChar) && !handle)
				handle = Convert.ToDouble(text) < minimum;

			if (maximum != null && Char.IsDigit(e.KeyChar) && !handle)
				handle = Convert.ToDouble(text) > maximum;

			return handle;
		}

		#endregion

		#region TextBoxValueUpDown

		public static bool TextBoxValueUpDown(object sender, KeyEventArgs e, double minimum, double maximum)
		{
			TextBox txt = sender as TextBox;

			double value = 0;
			bool hasValue = Double.TryParse(txt.Text, out value);
			bool suppress = false;

			bool up = e.KeyData == Keys.Up;
			bool down = e.KeyData == Keys.Down;

			if (hasValue && (up || down))
			{
				if (up && value < maximum)
					++value;
				else if (down && value > minimum)
					--value;

				txt.Text = value.ToString();
				suppress = true;
			}

			return suppress;
		}

		#endregion

		#region ConvertToDouble

		public static double ConvertToDouble(string str)
		{
			double retval = 0.0;

			Double.TryParse(str, out retval);

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
