using Comets.Core;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ImportType = Comets.Core.Managers.ElementTypesManager.Type;

namespace Comets.Application.Edit
{
	public partial class FormUpdate : ThemeForm
	{
		#region Events

		public event Action OnImportCompleted;

		#endregion

		#region Properties

		private string DownloadUrl { get { return CommonManager.Settings.DownloadUrl; } }
		private string DownloadFilename { get; set; }
		private string LocalFilename { get; set; }
		private string ImportFilename { get; set; }
		private ImportType ImportType { get; set; }
		private bool IsUsedDownloadedFile { get; set; }
		private bool IsAutomaticUpdate { get; set; }

		private CometCollection ImportedComets { get; set; }
		private ToolTip Tooltip { get; set; }

		#endregion

		#region Constructor

		public FormUpdate(bool isAutomaticUpdate = false)
		{
			InitializeComponent();
			Tooltip = new ToolTip();
			ImportType = ImportType.NoFileSelected;
			IsAutomaticUpdate = isAutomaticUpdate;
			cbxClose.Visible = !isAutomaticUpdate;
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormImport_Load(object sender, EventArgs e)
		{
			if (IsAutomaticUpdate)
			{
				gbxLocalFile.Enabled = false;
				StartDownload();
			}
		}

		#endregion

		#region LinkLabel

		private void linkOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo(DownloadUrl) { UseShellExecute = true });
		}

		private void labelDetectedComets_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (ImportedComets != null)
			{
				using (FormDatabase fdb = new FormDatabase(ImportedComets, false, null, CommonManager.DefaultSortProperty, CommonManager.DefaultSortAscending, false, true) { Owner = this })
				{
					fdb.TopMost = this.TopMost;
					fdb.ShowDialog();
				}
			}
		}

		#endregion

		#region Button

		private void btnDownload_Click(object sender, EventArgs e)
		{
			StartDownload();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = CommonManager.Settings.LastUsedImportDirectory;
				ofd.Filter = "Orbital elements files (*.txt, *.dat, *.csv, *.comet)|*.txt;*.dat;*.csv;*.comet|" +
							"Text documents (*.txt)|*.txt|" +
							"DAT files (*.dat)|*.dat|" +
							"CSV files (*.csv)|*.csv|" +
							"COMET files (*.comet)|*.comet|" +
							"All files (*.*)|*.*";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					CommonManager.Settings.LastUsedImportDirectory = Path.GetDirectoryName(ofd.FileName);
					txtLocalFile.Text = ofd.FileName; // txtImportFilename_TextChanged()
				}
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			ImportComets();
		}

		#endregion

		#region TextBox

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

		#region BackgroundWorker

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

				//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

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
					Uri uri = new Uri(DownloadUrl);

					try
					{
						wc.DownloadFileAsync(uri, DownloadFilename);
					}
					catch
					{
						DownloadFilename = null;
						throw;
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

			//if (IsAutomaticUpdate)
			//	ImportComets();
		}

		#endregion

		#endregion

		#region +Methods

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

			ImportedComets = null;
			Tooltip.RemoveAll();

			ImportType = ImportManager.GetImportType(ImportFilename);

			switch (ImportType)
			{
				case ImportType.NoFileSelected:
					lblImportFormat.Text = "(no file selected)";
					lblCometCount.Text = "-";
					break;

				case ImportType.FileNotFound:
					lblImportFormat.Text = "(file not found)";
					lblCometCount.Text = "-";
					break;

				case ImportType.EmptyFile:
					lblImportFormat.Text = "(empty file)";
					lblCometCount.Text = "-";
					break;

				case ImportType.Unknown:
					lblImportFormat.Text = "(unknown)";
					lblCometCount.Text = "-";
					break;

				default:
					lblImportFormat.Text = ElementTypesManager.TypeName[(int)ImportType];
					ImportedComets = ImportManager.ImportMain(CommonManager.MainCollection, ImportType, ImportFilename);
					string tooltopText;
					lblCometCount.Text = GetCountsText(out tooltopText);
					Tooltip.SetToolTip(lblCometCount, tooltopText);
					break;
			}
		}

		#endregion

		#region GetCountsText

		private string GetCountsText(out string tooltopText)
		{
			tooltopText = "New, updates, no changes; total";

			int cntNew = ImportedComets.Count(x => x.ImportResult == CometManager.ImportResult.New);
			int cntUpd = ImportedComets.Count(x => x.ImportResult == CometManager.ImportResult.Update);
			int cntNoc = ImportedComets.Count(x => x.ImportResult == CometManager.ImportResult.NoChanges);

			return String.Format("{0}, {1}, {2}; {3}", cntNew, cntUpd, cntNoc, cntNew + cntUpd + cntNoc);
		}

		#endregion

		#region StartDownload

		private void StartDownload()
		{
			if (DownloadFilename == null)
			{
				string directory = SettingsManager.DownloadsDirectory;

				if (!Directory.Exists(directory))
				{
					try
					{
						Directory.CreateDirectory(directory);
					}
					catch
					{
						directory = Path.GetTempPath();
					}
				}

				string filename = Path.GetFileNameWithoutExtension(DownloadUrl);
				string extension = Path.GetExtension(DownloadUrl);
				string datetime = DateTime.Now.ToString(DateTimeFormat.Filename);

				DownloadFilename = String.Format("{0}\\{1}_{2}{3}", directory, filename, datetime, extension);
				IsUsedDownloadedFile = true;

				using (BackgroundWorker bwDownload = new BackgroundWorker())
				{
					bwDownload.DoWork += new DoWorkEventHandler(bwDownload_DoWork);
					bwDownload.RunWorkerAsync();
				}
			}
		}

		#endregion

		#region ImportComets

		private void ImportComets()
		{
			bool isImported = false;

			if (ImportType < ImportType.NoFileSelected && ImportedComets != null)
			{
				string message;
				MessageBoxIcon icon;

				CometCollection collection = ImportedComets;

				if (collection.Count > 0)
				{
					message = "Import complete\t\t\t\t\t";
					icon = MessageBoxIcon.Information;

					if (IsUsedDownloadedFile)
					{
						CommonManager.Settings.LastUpdateDate = DateTime.UtcNow;
						CommonManager.Settings.IsSettingsChanged = true;
					}

					CommonManager.Filters = null;
					CommonManager.IsDataChanged = true;
					CommonManager.MainCollection = new CometCollection(collection);
					CommonManager.UserCollection = new CometCollection(collection);

					isImported = true;
					OnImportCompleted();
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

		#endregion
	}
}
