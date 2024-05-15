using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BlueMystic.DarkModeCS;

namespace Comets.Application.Common.Themes
{
	public class Theme
	{
		public bool IsDarkTheme { get; init; }
		public Color Background { get; init; }
		public Color Text { get; init; }


		public static Theme Light = new Theme()
		{
			IsDarkTheme = false,
			Background = SystemColors.ControlLight,
			Text = SystemColors.ControlText
		};

		public static Theme Dark = new Theme()
		{
			IsDarkTheme = true,
			Background = Color.Black,
			Text = SystemColors.GrayText,
		};
	}

	public static class ThemeManager
	{
		public static void SetTheme(this Form ownerForm, bool isDark) // todo...
		{
			if (isDark)
			{
				ThemeControl(ownerForm, Theme.Dark);
			}
			else
			{
				ThemeControl(ownerForm, Theme.Light);
			}
		}

		private static void ThemeControl(Control control, Theme theme)
		{
			if (control != null && control.Controls != null)
			{
				foreach (Control _control in control.Controls)
				{
					ThemeControl(_control, theme);
				}
				control.ControlAdded += (object sender, ControlEventArgs e) =>
				{
					ThemeControl(e.Control, theme);
				};
			}


			control.GetType().GetProperty("BackColor")?.SetValue(control, theme.Background);

			control.GetType().GetProperty("ForeColor")?.SetValue(control, theme.Text);


			control.HandleCreated += (object sender, EventArgs e) =>
			{
				ApplySystemDarkTheme(control, theme.IsDarkTheme);
			};
			control.ControlAdded += (object sender, ControlEventArgs e) =>
			{
				ThemeControl(e.Control, theme);
			};
		}

		private static void ApplySystemDarkTheme(Control control = null, bool isDarkMode = false)
		{
			/* 			    
				DWMWA_USE_IMMERSIVE_DARK_MODE:   https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute

				Use with DwmSetWindowAttribute. Allows the window frame for this window to be drawn in dark mode colors when the dark mode system setting is enabled. 
				For compatibility reasons, all windows default to light mode regardless of the system setting. 
				The pvAttribute parameter points to a value of type BOOL. TRUE to honor dark mode for the window, FALSE to always use light mode.

				This value is supported starting with Windows 11 Build 22000.

				SetWindowTheme:     https://learn.microsoft.com/en-us/windows/win32/api/uxtheme/nf-uxtheme-setwindowtheme
				Causes a window to use a different set of visual style information than its class normally uses.
			 */
			int[] DarkModeOn = new[] { isDarkMode ? 0x01 : 0x00 }; //<- 1=True, 0=False

			SetWindowTheme(control.Handle, "DarkMode_Explorer", null);

			if (DwmSetWindowAttribute(control.Handle, (int)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, DarkModeOn, 4) != 0)
				DwmSetWindowAttribute(control.Handle, (int)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, DarkModeOn, 4);

			foreach (Control child in control.Controls)
			{
				if (child.Controls.Count != 0)
					ApplySystemDarkTheme(child, isDarkMode);
			}
		}

		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

	}
}
