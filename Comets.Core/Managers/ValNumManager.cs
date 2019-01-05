using Comets.Core.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Comets.Core.Managers
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

			if (minimum > maximum)
				throw new Exception("Minimum can not be greather than maximum");

			bool negative = minimum < 0;
			int decimals = v.Decimals;

			if (e.KeyChar == ',')
				e.KeyChar = '.';

			string input = Char.IsControl(e.KeyChar) ? String.Empty : e.KeyChar.ToString();

			string text;

			if (textbox.SelectionLength > 0)
				text = textbox.Text.Replace(textbox.SelectedText, input);
			else if (e.KeyChar == '\b') //backspace
				text = String.Concat(textbox.Text.Select((c, i) => i == textbox.SelectionStart - 1 ? input : c.ToString()));
			else
				text = textbox.Text.Insert(textbox.SelectionStart, input);

			string pattern = "^";

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

			bool handle = !Regex.IsMatch(text, pattern) || text.Double() < minimum || text.Double() > maximum;

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

			double value = 0;
			bool hasValue = Double.TryParse(txt.Text, out value);
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