﻿using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
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

		#endregion

		#region Constructor

		public FormImport()
		{
			InitializeComponent();
			ImportType = ImportType.NoFileSelected;
		}

		#endregion

		#region btnDownload_Click

		private void btnDownload_Click(object sender, EventArgs e)
		{
			if (DownloadFilename != null)
			{
				File.Delete(DownloadFilename);
				progressDownload.Value = 0;
			}

			DownloadFilename = FormMain.Settings.Downloads + "\\Soft00Cmt_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

			using (BackgroundWorker bwDownload = new BackgroundWorker())
			{
				bwDownload.DoWork += new DoWorkEventHandler(bwDownload_DoWork);
				bwDownload.RunWorkerAsync();
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
			progressDownload.Value = e.ProgressPercentage;
		}

		void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (File.Exists(DownloadFilename) && new FileInfo(DownloadFilename).Length == 0)
			{
				progressDownload.Visible = false;
				File.Delete(DownloadFilename);
				MessageBox.Show(e.Error.Message, "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				ImportFilename = DownloadFilename;
				SetImportStatus();
			}
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

				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
					ImportFilename = DownloadFilename;

				else if (DownloadFilename == null && LocalFilename != null)
					ImportFilename = LocalFilename;
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
			if (ImportType < ImportType.NoFileSelected)
			{
				List<Comet> newList = ImportManager.ImportMain(ImportType, ImportFilename);

				if (newList.Count == 0)
				{
					MessageBox.Show("Something wrong happened. Zero comets imported.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					List<Comet> mergedList = FormMain.MainList.ToList();

					int _new = 0, old = 0;

					foreach (Comet n in newList)
					{
						Comet o = mergedList.Find(x => x.full == n.full);

						if (o != null)
						{
							mergedList.Remove(o);
							old++;
						}
						else
						{
							_new++;
						}

						mergedList.Add(n);
					}

					FormMain.IsDataChanged = true;
					FormMain.MainList = mergedList.OrderBy(x => x.sortkey).ToList();
					FormMain.UserList = mergedList.OrderBy(x => x.sortkey).ToList();
					(this.Owner as FormMain).SetStatusCometsLabel(mergedList.Count, mergedList.Count);

					MessageBox.Show(
						String.Format("Import complete\n\n{0} new, {1} updated\t\t\t\t", _new, old)
						, "Comets"
						, MessageBoxButtons.OK
						, MessageBoxIcon.Information);

					this.Close();
				}
			}
		}

		#endregion
	}
}
