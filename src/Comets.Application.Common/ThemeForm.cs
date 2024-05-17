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
		private DarkModeCS _darkMode = null;

		public bool IsDarkMode => (_darkMode?.IsDarkMode).GetValueOrDefault();

		public ThemeForm()
		{
			this.Load += this.FormSettings_Load;
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			_darkMode = new DarkModeCS(this, CommonManager.Settings.Theme);
		}
	}
}
