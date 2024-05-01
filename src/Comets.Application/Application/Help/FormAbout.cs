using System.Reflection;
using System.Windows.Forms;

namespace Comets.Application.Help
{
	public partial class FormAbout : Form
	{
		#region Constructor

		public FormAbout()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void FormAbout_Load(object sender, System.EventArgs e)
		{
			string version = Assembly.GetEntryAssembly()
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
				.InformationalVersion;

			lblVersion.Text = $"Comets {version}";
		}

		#endregion
	}
}
