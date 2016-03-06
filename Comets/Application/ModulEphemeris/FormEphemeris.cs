using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

		#region LoadResultsAsync

		public async Task LoadResultsAsync(CancellationToken ct)
		{
			this.Text = EphemerisSettings.ToString();
			richTextBox.Text = await EphemerisManager.GenerateEphemerisAsync(EphemerisSettings, FormMain.Progress, ct);
			richTextBox.SelectionStart = 0;
			EphemerisSettings.AddNew = false;
			EphemerisSettings.Ephemerides.Clear();
			GC.Collect();
		}

		#endregion

		#region SaveEphemeris

		public void SaveEphemeris()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				if (!String.IsNullOrEmpty(FormMain.Settings.LastUsedExportDirectory))
					sfd.InitialDirectory = FormMain.Settings.LastUsedExportDirectory;
				else
					sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

				sfd.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					File.WriteAllText(sfd.FileName, richTextBox.Text);
					FormMain.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Ephemeris saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
