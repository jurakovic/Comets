using BlueMystic;
using Comets.Core.Managers;
using System;
using System.Windows.Forms;

namespace Comets.Application
{
#if RELEASE
    public abstract class ThemedForm : Form
#else
	public class ThemedForm : Form
#endif
	{
		private DarkModeCS DarkMode = null;

		public ThemedForm()
		{
			this.Load += this.FormSettings_Load;
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			DarkMode = new DarkModeCS(this, CommonManager.Settings.Theme);
		}
	}
}
