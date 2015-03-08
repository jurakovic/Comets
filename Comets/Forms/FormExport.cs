using Comets.Classes;
using Comets.Helpers;
using System;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormExport : Form
    {
        SaveFileDialog sfd;

        public FormExport()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            this.cbxExportFormat.DataSource = ElementTypes.TypeName;
            this.lblTotalComets.Text = FormMain.userList.Count.ToString();

            sfd = new SaveFileDialog();
            //sfd.InitialDirectory = FormMain.localDataDir;
            sfd.Filter = "Text documents (*.txt)|*.txt|" +
                        "DAT files (*.dat)|*.dat|" +
                        "COMET files (*.comet)|*.comet|" +
                        "All files (*.*)|*.*";
        }

        private void cbxExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblExportFormat.Text = cbxExportFormat.Text;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSaveAs.Text = sfd.FileName;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string filename = txtSaveAs.Text.Trim();

            if (filename.Length > 0)
            {
                int exportType = cbxExportFormat.SelectedIndex;
                ExportHelper.ExportMain(exportType, filename);
                MessageBox.Show("Export complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
