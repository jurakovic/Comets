using BlueMystic;
using System;
using System.Windows.Forms;

namespace Comets.Application
{
	public abstract class ThemeForm : Form
	{
		private DarkModeCS DM = null; //<- Line 1

		public ThemeForm()
		{
			this.Load += this.FormSettings_Load;
		}

		public void SetTheme()
		{
			DM = new DarkModeCS(this); //<- Line 2
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			SetTheme();
		}
	}
}
