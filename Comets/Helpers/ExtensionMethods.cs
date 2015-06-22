using System.Windows.Forms;

namespace Comets.Helpers
{
	public static class ExtensionMethods
	{
		public static double Double(this string str)
		{
			double retval = 0;
			System.Double.TryParse(str.Trim(), out retval);
			return retval;
		}

		public static int Int(this string str)
		{
			return (int)str.Double();
		}

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

		public static int Int(this TextBoxBase txt)
		{
			return (int)txt.Double();
		}
	}
}
