using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Cometary_Workshop
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (progDownload.InvokeRequired)
            {
                progDownload.Invoke((MethodInvoker)delegate()
                {
                    bw_DoWork(sender, e);
                });
            }
            else
            {
                string url = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
                Form1.localFile = Form1.downloadsDir + @"Soft00" +
                        "Cmt_" + DateTime.Now.Year + "-" +
                        DateTime.Now.Month.ToString("00") + "-" +
                        DateTime.Now.Day.ToString("00") + "_" +
                        DateTime.Now.Hour.ToString("00") + "-" +
                        DateTime.Now.Minute.ToString("00") + "-" +
                        DateTime.Now.Second.ToString("00") + ".txt";
                try
                {
                    WebClient Client = new WebClient();
                    Client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    Uri urll = new Uri(url);
                    Client.DownloadFileAsync(urll, Form1.localFile);
                }
                catch
                {
                    //MessageBox.Show("Unable to download orbital elements", "Error", MessageBoxButtons.OK);
                    //return;
                }
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progDownload.Value = e.ProgressPercentage;
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //this.Close();

            FileInfo fi = new FileInfo(Form1.localFile);
            if (fi.Length == 0)
            {
                File.Delete(Form1.localFile);
                this.Visible = false;
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                Form1.fileIsDownloaded = true;
                this.Text = "Done";
                this.ControlBox = true;
            }
        }
    }
}
