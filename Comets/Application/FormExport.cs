using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.IO;
using System.Windows.Forms;
using ExportType = Comets.BusinessLayer.Business.ElementTypes.Type;

namespace Comets.Application
{
	public partial class FormExport : Form
	{
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
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				sfd.Filter = ElementTypes.ExtensionFilters[cbxExportFormat.SelectedIndex] + "All files (*.*)|*.*";

				ExternalProgram ep = FormMain.Settings.ExternalPrograms.Find(x => x.Type == cbxExportFormat.SelectedIndex);

				if (ep != null)
					sfd.InitialDirectory = ep.Directory;
				else
					sfd.InitialDirectory = FormMain.Settings.LastUsedExportDirectory;

				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					FormMain.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					txtSaveAs.Text = sfd.FileName;
				}
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
				ExporManager.ExportMain(exportType, filename, FormMain.UserList);
				MessageBox.Show("Export complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		#endregion
	}
}
