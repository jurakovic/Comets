using BlueMystic;
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
			DarkMode = new DarkModeCS(this, CommonManager.Settings.Theme);
		}

		public void SetTheme(Core.Settings.ThemeEnum theme)
		{
			DarkMode = new DarkModeCS(this, theme);
			this.Refresh();
		}
	}
}
