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
			if (CommonManager.Settings.UseDarkTheme)
				DarkMode = new DarkModeCS(this, true);
		}
	}
}
