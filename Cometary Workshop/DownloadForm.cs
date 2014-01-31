using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Cometary_Workshop
{
    public partial class DownloadForm : Form
    {
        public DownloadForm(string url, string file)
        {
            InitializeComponent();

            this.textUrl.Text = url;
            this.textFile.Text = file;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bwDownload.RunWorkerAsync();
        }

        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            if (progDownload.InvokeRequired)
            {
                progDownload.Invoke((MethodInvoker)delegate()
                {
                    bwDownload_DoWork(sender, e);
                });
            }
            else
            {
                string url = this.textUrl.Text;
                string downloadedFile = this.textFile.Text;

                try
                {


                    WebClient Client = new WebClient();
                    Client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    Uri urll = new Uri(url);
                    Client.DownloadFileAsync(urll, downloadedFile);
                }
                catch
                {
                    MessageBox.Show("Unable to download orbital elements", "Error", MessageBoxButtons.OK);
                    File.Delete(downloadedFile);
                    return;
                }
            }
        }


        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progDownload.Value = e.ProgressPercentage;
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //importMain(downloadedFile, 0);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
