using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Application.ModulEphemeris
{
	public partial class FormEphemeris : Form, ISave
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

		#region Save

		public void Save()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				sfd.FileName = "Comets_Ephemeris_" + DateTime.Now.ToString(FormMain.DateTimeFormatSaveAs);
				sfd.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					File.WriteAllLines(sfd.FileName, richTextBox.Lines);
					CommonManager.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Ephemeris saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
