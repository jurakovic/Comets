using Comets.Application.Common.General;
using Comets.Core;
using Comets.Core.Managers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Application.Ephemeris
{
	public partial class FormEphemeris : Form, ISave
	{
		#region Properties

		public EphemerisSettings EphemerisSettings { get; set; }

		private IProgress<int> Progress { get; set; }

		#endregion

		#region Constructor

		public FormEphemeris(EphemerisSettings settings, IProgress<int> progress)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.EphemerisSettings = settings;
			this.Progress = progress;
		}

		#endregion

		#region LoadResultsAsync

		public async Task LoadResultsAsync(CancellationToken ct)
		{
			this.Text = EphemerisSettings.ToString();
			richTextBox.Text = await EphemerisManager.GenerateEphemerisAsync(EphemerisSettings, this.Progress, ct);
			richTextBox.SelectionStart = 0;
			EphemerisSettings.AddNew = false;
			EphemerisSettings.Ephemerides.Clear();
			GC.Collect();
		}

		#endregion

		#region ISave

		public void Save()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				sfd.FileName = "Comets_Ephemeris_" + DateTime.Now.ToString(DateTimeFormat.Filename);
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
