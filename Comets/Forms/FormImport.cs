using Comets.Classes;
using Comets.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormImport : Form
    {
        const string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";

        string downloadFilename;
        string importFilename;

        int importType = (int)ImportHelper.ImportType.Unknown;

        FormMain formMain = null;

        public FormImport(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            downloadFilename = FormMain.downloadsDir + "Soft00Cmt_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

            bwDownload.RunWorkerAsync();
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

                WebClient Client = new WebClient();
                Client.DownloadProgressChanged += Client_DownloadProgressChanged;
                Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                Uri uri = new Uri(url);

                try
                {
                    Client.DownloadFileAsync(uri, downloadFilename);
                }
                catch
                {
                    //nothing...
                }
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressDownload.Value = e.ProgressPercentage;
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (new FileInfo(downloadFilename).Length == 0)
            {
                progressDownload.Visible = false;
                File.Delete(downloadFilename);
                MessageBox.Show(e.Error.Message, "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                importFilename = downloadFilename;
                setImportStatus();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = FormMain.downloadsDir;

            ofd.Filter = "Text files (*.txt)|*.TXT|" +
                        "DAT files (*.dat)|*.DAT|" +
                        "COMET files (*.comet)|*.COMET|" +
                        "All files (*.*)|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                importFilename = ofd.FileName;
                txtImportFilename.Text = importFilename; // to ce triggerati event
            }
        }

        private void setImportStatus()
        {
            importType = (int)ImportHelper.GetImportType(importFilename);

            if (!File.Exists(importFilename))
            {
                lblImportFormat1.Text = "(file not found)";
                labelDetectedComets.Text = "-";
            }
            else if (importFilename == null)
            {
                lblImportFormat1.Text = "(no file selected)";
                labelDetectedComets.Text = "-";
            }
            else if (importType == (int)ImportHelper.ImportType.Unknown)
            {
                lblImportFormat1.Text = "unknown";
                labelDetectedComets.Text = "-";
            }
            else
            {
                lblImportFormat1.Text = ImportHelper.ImportTypeName[importType];
                labelDetectedComets.Text = ImportHelper.GetNumberOfComets(importFilename, importType).ToString();
            }
        }

        private void txtImportFilename_TextChanged(object sender, EventArgs e)
        {
            string localFilename = txtImportFilename.Text.Trim().Trim('"');

            if (localFilename.Length == 0)
            {
                if (downloadFilename != null)
                    importFilename = downloadFilename;
                else
                    importFilename = null;
            }
            else
            {
                importFilename = localFilename;
            }

            setImportStatus();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            List<Comet> list = ImportHelper.ImportMain(importType, importFilename);

            if (list == null)
            {
                MessageBox.Show("Unknown import format.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormMain.masterList = list;
                this.formMain.SetStatusCometsLabel(list.Count);
                MessageBox.Show("Import complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClose.Focus();
            }
        }
    }
}
