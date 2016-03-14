using System.IO;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormControls : Form
	{
		#region Constructor

		public FormControls()
		{
			InitializeComponent();
		}

		#endregion

		#region FormControls_Load

		private void FormControls_Load(object sender, System.EventArgs e)
		{
			string controls = "controls.txt";
			txtControls.Text = File.Exists(controls) ? File.ReadAllText(controls) : "controls.txt not found...";
			txtControls.Select(0, 0);
		}

		#endregion
	}
}
