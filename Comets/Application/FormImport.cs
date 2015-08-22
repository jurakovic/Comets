using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ImportType = Comets.BusinessLayer.Business.ElementTypes.Type;

namespace Comets.Application
{
	public partial class FormImport : Form
	{
		#region Const

		const string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";

		#endregion

		#region Properties

		string DownloadFilename { get; set; }
		string LocalFilename { get; set; }
		string ImportFilename { get; set; }
		ImportType ImportType { get; set; }
		bool IsUsedDownloadedFile { get; set; }
		bool IsAutomaticUpdate { get; set; }

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
				if (!Directory.Exists(SettingsManager.Downloads))
					Directory.CreateDirectory(SettingsManager.Downloads);

				DownloadFilename = SettingsManager.Downloads + "\\Soft00Cmt_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
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
				progressDownload.Invoke((MethodInvoker)delegate()
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
					if (FormMain.Settings.UseProxy)
					{
						WebProxy proxy = new WebProxy(FormMain.Settings.Proxy, FormMain.Settings.Port);
						proxy.Credentials = new NetworkCredential(FormMain.Settings.Username, FormMain.Settings.Password, FormMain.Settings.Domain);
						wc.Proxy = proxy;
					}

					wc.DownloadProgressChanged += Client_DownloadProgressChanged;
					wc.DownloadFileCompleted += Client_DownloadFileCompleted;
					Uri uri = new Uri(url);

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
			if (File.Exists(DownloadFilename) && new FileInfo(DownloadFilename).Length == 0)
			{
				progressDownload.Visible = false;
				File.Delete(DownloadFilename);
				MessageBox.Show(e.Error.Message, "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
				DownloadFilename = null;
			}
			else
			{
				ImportFilename = DownloadFilename;
				SetImportStatus();
			}

			if (IsAutomaticUpdate)
				btnImport_Click(null, null);
		}

		#endregion

		#region btnBrowse_Click

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = FormMain.Settings.LastUsedImportDirectory;
				ofd.Filter = "Orbital elements files (*.txt, *.dat, *.comet)|*.txt;*.dat;*.comet|" +
							"Text documents (*.txt)|*.txt|" +
							"DAT files (*.dat)|*.dat|" +
							"COMET files (*.comet)|*.comet|" +
							"All files (*.*)|*.*";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					FormMain.Settings.LastUsedImportDirectory = Path.GetDirectoryName(ofd.FileName);
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
					lblImportFormat.Text = ElementTypes.TypeName[(int)ImportType];
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
				CometCollection collection = ImportManager.ImportMain(FormMain.MainCollection, ImportType, ImportFilename, false);

				if (collection.Count > 0)
				{
					if (IsUsedDownloadedFile)
					{
						FormMain.Settings.LastUpdateDate = DateTime.Now;
						FormMain.Settings.IsSettingsChanged = true;
					}

					FormMain.IsDataChanged = true;
					FormMain.MainCollection = new CometCollection(collection);
					FormMain.UserCollection = new CometCollection(collection);

					isImported = true;
				}
			}

			if (IsAutomaticUpdate || (isImported && cbxClose.Checked))
				this.Close();
		}

		#endregion
	}
}
