using BlueMystic;
using Comets.Application.Common.Themes;
using Comets.Core.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application
{
#if RELEASE
    public abstract class ThemeForm : Form
#else
	public class ThemeForm : Form
#endif
	{
		private DarkModeCS DarkMode = null;

		public ThemeForm()
		{
			this.Load += this.FormSettings_Load;
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			//DarkMode = new DarkModeCS(this, CommonManager.Settings.Theme);
			ThemeManager.SetTheme(this, Theme.Light.IsDarkTheme);
		}

		public void SetTheme(Core.Settings.ThemeEnum theme)
		{
			//DarkMode = new DarkModeCS(this, theme);

			ThemeManager.SetTheme(this, theme != Core.Settings.ThemeEnum.Light);
			this.Refresh();
		}
	}
}
