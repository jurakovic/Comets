using BlueMystic;
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
		private DarkModeCS DM = null; //<- Line 1

		public ThemedForm()
		{
			this.Load += this.FormSettings_Load;
		}

		public void SetTheme()
		{
			DM = new DarkModeCS(this); //<- Line 2
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			//SetTheme();
		}
	}
}
