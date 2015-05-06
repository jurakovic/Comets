using Comets.Classes;
using Comets.Helpers;
using System;
using System.Windows.Forms;
using ExportType = Comets.Classes.ElementTypes.Type;

namespace Comets.Forms
{
    public partial class FormExport : Form
    {
        #region Properties

        SaveFileDialog sfd { get; set; }

        #endregion

        #region Constructor

        public FormExport()
        {
            InitializeComponent();
        }

        #endregion

        #region Form_Load

        private void FormExport_Load(object sender, EventArgs e)
        {
            this.cbxExportFormat.DataSource = ElementTypes.TypeName;
            this.lblTotalComets.Text = FormMain.UserList.Count.ToString();

            sfd = new SaveFileDialog();
            //sfd.InitialDirectory = FormMain.localDataDir;
            sfd.Filter = "Text documents (*.txt)|*.txt|" +
                        "DAT files (*.dat)|*.dat|" +
                        "COMET files (*.comet)|*.comet|" +
                        "All files (*.*)|*.*";
        }

        #endregion

        #region cbxExportFormat_SelectedIndexChanged

        private void cbxExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblExportFormat.Text = cbxExportFormat.Text;
        }

        #endregion

        #region btnBrowse_Click

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSaveAs.Text = sfd.FileName;
            }
        }

        #endregion

        #region btnExport_Click

        private void btnExport_Click(object sender, EventArgs e)
        {
            string filename = txtSaveAs.Text.Trim();

            if (filename.Length > 0 && FormMain.UserList.Count > 0)
            {
                ExportType exportType = (ExportType)cbxExportFormat.SelectedIndex;
                ExportHelper.ExportMain(exportType, filename, FormMain.UserList);
                MessageBox.Show("Export complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
