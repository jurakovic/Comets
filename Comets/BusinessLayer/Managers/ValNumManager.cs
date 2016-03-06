using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Comets.BusinessLayer.Managers
{
	public static class ValNumManager
	{
		#region HandleKeyPress

		public static bool HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textbox = sender as TextBox;

			ValNum v = textbox.Tag as ValNum;

			if (v == null)
				throw new NullReferenceException("Textbox has no defined ValNum Tag");

			double minimum = v.DMin;
			double maximum = v.DMax;
			int decimals = v.Decimals;

			if (minimum > maximum)
				throw new Exception("Minimum can not be greather than maximum");

			if (e.KeyChar == ',')
				e.KeyChar = '.';

			string str = Char.IsControl(e.KeyChar) ? String.Empty : e.KeyChar.ToString();

			string text = textbox.SelectionLength > 0 ? textbox.Text.Replace(textbox.SelectedText, str) : textbox.Text + str;

			string pattern = "^";

			bool negative = minimum < 0;

			if (negative)
				pattern += "-?";

			pattern += "[0-9]*$";

			if (decimals > 0)
			{
				pattern = "(" + pattern + ")|(^";

				if (negative)
					pattern += "-?";

				pattern += "[0-9]*([.][0-9]{0," + decimals + "})?$)";
			}

			bool handle = !Regex.IsMatch(text, pattern);

			if (Char.IsDigit(e.KeyChar) && !handle)
				handle = text.Double() < minimum;

			if (Char.IsDigit(e.KeyChar) && !handle)
				handle = text.Double() > maximum;

			return handle;
		}

		#endregion

		#region TextBoxValueUpDown

		public static bool TextBoxValueUpDown(object sender, KeyEventArgs e)
		{
			TextBox txt = sender as TextBox;

			ValNum v = txt.Tag as ValNum;

			if (v == null)
				throw new NullReferenceException("Textbox has no defined ValNum Tag");

			int value = 0;
			bool hasValue = Int32.TryParse(txt.Text, out value);
			bool suppress = false;

			bool up = e.KeyData == Keys.Up;
			bool down = e.KeyData == Keys.Down;

			if (hasValue && (up || down))
			{
				if (up && value < v.IMax)
					++value;
				else if (down && value > v.IMin)
					--value;

				txt.Text = value.ToString();
				suppress = true;
			}

			return suppress;
		}

		#endregion
	}
}