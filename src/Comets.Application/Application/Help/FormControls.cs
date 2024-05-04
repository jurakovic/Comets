using System.Windows.Forms;

namespace Comets.Application.Help
{
	public partial class FormControls : Form
	{
		#region Constructor

		public FormControls()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void FormControls_Load(object sender, System.EventArgs e)
		{
			txtControls.Text = Properties.Resources.Controls;
			txtControls.Select(0, 0);
		}

		#endregion
	}
}
