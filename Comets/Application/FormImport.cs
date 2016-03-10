using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ImportType = Comets.BusinessLayer.Managers.ElementTypesManager.Type;

namespace Comets.Application
{
	public partial class FormImport : Form
	{
		#region Const

		private const string MpcUrl = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";

		#endregion

		#region Properties

		private string DownloadFilename { get; set; }
		private string LocalFilename { get; set; }
		private string ImportFilename { get; set; }
		private ImportType ImportType { get; set; }
		private bool IsUsedDownloadedFile { get; set; }
		private bool IsAutomaticUpdate { get; set; }

		#endregion

		#region Constructor

		public FormImport(bool isAutomaticUpdate = false)
		{
			InitializeComponent();
			ImportType = ImportType.NoFileSelected;
			IsAutomaticUpdate = isAutomaticUpdate;
			cbxClose.Visible = !isAutomaticUpdate;
		}

		#endregion

		#region FormImport_Load

		private void FormImport_Load(object sender, EventArgs e)
		{
			if (IsAutomaticUpdate)
			{
				gbxLocalFile.Enabled = false;
				btnDownload_Click(sender, e);
			}
		}

		#endregion

		#region btnDownload_Click

		private void btnDownload_Click(object sender, EventArgs e)
		{
			if (DownloadFilename == null)
			{
				string directory = SettingsManager.DownloadsDirectory;

				if (!Directory.Exists(SettingsManager.DownloadsDirectory))
				{
					try
					{
						Directory.CreateDirectory(SettingsManager.DownloadsDirectory);
					}
					catch
					{
						directory = Path.GetTempPath();
					}
				}

				DownloadFilename = directory + "\\Soft00Cmt_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
				IsUsedDownloadedFile = true;

				using (BackgroundWorker bwDownload = new BackgroundWorker())
				{
					bwDownload.DoWork += new DoWorkEventHandler(bwDownload_DoWork);
					bwDownload.RunWorkerAsync();
				}
			}
		}

		private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
		{
			if (progressDownload.InvokeRequired)
			{
				progressDownload.Invoke((MethodInvoker)delegate ()
				{
					bwDownload_DoWork(sender, e);
				});
			}
			else
			{
				progressDownload.Visible = true;
				progressDownload.Style = ProgressBarStyle.Marquee;

				using (WebClient wc = new WebClient())
				{
					Settings settings = CommonManager.Settings;

					if (settings.UseProxy)
					{
						WebProxy proxy = new WebProxy(settings.Proxy, settings.Port);
						proxy.Credentials = new NetworkCredential(settings.Username, settings.Password, settings.Domain);
						wc.Proxy = proxy;
					}

					wc.DownloadProgressChanged += Client_DownloadProgressChanged;
					wc.DownloadFileCompleted += Client_DownloadFileCompleted;
					Uri uri = new Uri(MpcUrl);

					try
					{
						wc.DownloadFileAsync(uri, DownloadFilename);
					}
					catch
					{
						//nothing...
					}
				}
			}
		}

		void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressDownload.Style = ProgressBarStyle.Blocks;
			progressDownload.Value = e.ProgressPercentage;
		}

		void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (File.Exists(DownloadFilename))
			{
				if (new FileInfo(DownloadFilename).Length == 0)
				{
					progressDownload.Visible = false;
					File.Delete(DownloadFilename);
					MessageBox.Show(e.Error.Message, "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
					DownloadFilename = null;
				}
				else
				{
					ImportFilename = DownloadFilename;
				}
			}
			else
			{
				DownloadFilename = null;
			}

			SetImportStatus();

			if (IsAutomaticUpdate)
				btnImport_Click(null, null);
		}

		#endregion

		#region btnBrowse_Click

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = CommonManager.Settings.LastUsedImportDirectory;
				ofd.Filter = "Orbital elements files (*.txt, *.dat, *.comet)|*.txt;*.dat;*.comet|" +
							"Text documents (*.txt)|*.txt|" +
							"DAT files (*.dat)|*.dat|" +
							"COMET files (*.comet)|*.comet|" +
							"All files (*.*)|*.*";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					CommonManager.Settings.LastUsedImportDirectory = Path.GetDirectoryName(ofd.FileName);
					txtLocalFile.Text = ofd.FileName; // txtImportFilename_TextChanged()
				}
			}
		}

		#endregion

		#region txtImportFilename_TextChanged

		private void txtImportFilename_TextChanged(object sender, EventArgs e)
		{
			LocalFilename = txtLocalFile.Text.Trim().Trim('"');

			if (LocalFilename.Length == 0)
			{
				LocalFilename = null;
				ImportFilename = null;
			}
			else
			{
				ImportFilename = LocalFilename;
			}

			SetImportStatus();
		}

		#endregion

		#region SetImportStatus

		private void SetImportStatus()
		{
			if (ImportFilename == null)
			{
				if (LocalFilename == null && DownloadFilename != null)
				{
					ImportFilename = DownloadFilename;
					IsUsedDownloadedFile = true;
				}
				else if (DownloadFilename == null && LocalFilename != null)
				{
					ImportFilename = LocalFilename;
					IsUsedDownloadedFile = false;
				}
			}

			ImportType = ImportManager.GetImportType(ImportFilename);

			switch (ImportType)
			{
				case ImportType.NoFileSelected:
					lblImportFormat.Text = "(no file selected)";
					labelDetectedComets.Text = "-";
					break;

				case ImportType.FileNotFound:
					lblImportFormat.Text = "(file not found)";
					labelDetectedComets.Text = "-";
					break;

				case ImportType.EmptyFile:
					lblImportFormat.Text = "(empty file)";
					labelDetectedComets.Text = "-";
					break;

				case ImportType.Unknown:
					lblImportFormat.Text = "(unknown)";
					labelDetectedComets.Text = "-";
					break;

				default:
					lblImportFormat.Text = ElementTypesManager.TypeName[(int)ImportType];
					labelDetectedComets.Text = ImportManager.GetNumberOfComets(ImportFilename, ImportType).ToString();
					break;
			}
		}

		#endregion

		#region btnImport_Click

		private void btnImport_Click(object sender, EventArgs e)
		{
			bool isImported = false;

			if (ImportType < ImportType.NoFileSelected)
			{
				int totalNew = 0;
				int totalOld = 0;

				string message;
				MessageBoxIcon icon;

				CometCollection collection = ImportManager.ImportMain(CommonManager.MainCollection, ImportType, ImportFilename, out totalNew, out totalOld);

				if (collection.Count > 0)
				{
					message = String.Format("Import complete\n\n{0} new, {1} updated\t\t\t\t", totalNew, totalOld);
					icon = MessageBoxIcon.Information;

					if (IsUsedDownloadedFile)
					{
						CommonManager.Settings.LastUpdateDate = DateTime.Now;
						CommonManager.Settings.IsSettingsChanged = true;
					}

					CommonManager.IsDataChanged = true;
					CommonManager.MainCollection = new CometCollection(collection);
					CommonManager.UserCollection = new CometCollection(collection);

					isImported = true;
				}
				else
				{
					message = "Something wrong happened. Zero comets imported.\t\t\t";
					icon = MessageBoxIcon.Error;
				}

				MessageBox.Show(message, "Comets", MessageBoxButtons.OK, icon);
			}

			if (IsAutomaticUpdate || (isImported && cbxClose.Checked))
				this.Close();
		}

		#endregion
	}
}
