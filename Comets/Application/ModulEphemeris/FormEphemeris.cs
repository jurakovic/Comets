using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System.Windows.Forms;

namespace Comets.Application.ModulEphemeris
{
	public partial class FormEphemeris : Form
	{
		#region Properties

		public EphemerisSettings EphemerisSettings { get; set; }

		#endregion

		#region Constructor

		public FormEphemeris(EphemerisSettings settings)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.EphemerisSettings = settings;
		}

		#endregion

		#region Form_Load

		private void FormEphemerisResult_Load(object sender, System.EventArgs e)
		{
			LoadResults();
		}

		#endregion

		#region LoadResults

		public async void LoadResults()
		{
			this.Text = EphemerisSettings.ToString();
			richTextBox.Text = await EphemerisManager.GenerateEphemeris(EphemerisSettings);
			EphemerisSettings.Results.Clear();
		}

		#endregion
	}
}
