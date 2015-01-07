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

namespace Comets
{
    public partial class FormImport : Form
    {
        const string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
        string filename;
        int importType = (int)ImportHelper.ImportType.Unknown;

        FormMain formMain = null;

        public FormImport(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            filename = FormMain.downloadsDir + "Soft00Cmt_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

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
                    Client.DownloadFileAsync(uri, filename);
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
            if (new FileInfo(filename).Length == 0)
            {
                progressDownload.Visible = false;
                File.Delete(filename);
                MessageBox.Show(e.Error.Message, "Comets", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
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
                txtImportFilename.Text = ofd.FileName;
                filename = txtImportFilename.Text.Trim();

                setImportStatus();
            }
        }

        private void setImportStatus()
        {
            importType = (int)ImportHelper.GetImportType(filename);

            if (importType == (int)ImportHelper.ImportType.Unknown)
            {
                lblImportFormat1.Text = "unknown";
                labelDetectedComets.Text = "-";
                return;
            }

            lblImportFormat1.Text = ImportHelper.ImportTypeName[importType];
            labelDetectedComets.Text = ImportHelper.GetNumberOfComets(filename, importType).ToString();
        }

        private void txtImportFilename_TextChanged(object sender, EventArgs e)
        {
            filename = txtImportFilename.Text.Trim().Trim('"');

            if (filename.Length == 0)
            {
                lblImportFormat1.Text = "(no file selected)";
                labelDetectedComets.Text = "-";
                return;
            }

            if (!File.Exists(filename))
            {
                lblImportFormat1.Text = "(file not found)";
                labelDetectedComets.Text = "-";
                return;
            }

            setImportStatus();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (importType == (int)ImportHelper.ImportType.Unknown)
                return;

            List<Comet> list = ImportHelper.ImportMain(importType, filename);

            if (list != null)
            {
                FormMain.masterList = list;
                this.formMain.SetStatusCometsLabel(list.Count);
                MessageBox.Show("Import complete.                                    ", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
