using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        #region JDToOta

        public static Double JDToOta(double jd)
        {
            int[] dt = EphemerisHelper.jdtocd(jd);
            return new DateTime(dt[0], dt[1], dt[2], dt[4], dt[5], dt[6]).ToOADate();
        }

        #endregion
    }
}
