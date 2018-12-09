using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.IO;
using System.Windows.Forms;
using ExportType = Comets.BusinessLayer.Managers.ElementTypesManager.Type;

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
			this.cbxExportFormat.DataSource = ElementTypesManager.TypeName;
			this.lblTotalComets.Text = CommonManager.UserCollection.Count.ToString();
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
				ExportType type = (ExportType)cbxExportFormat.SelectedIndex;
				ExternalProgram ep = CommonManager.Settings.ExternalPrograms.Find(x => x.Type == (int)type);

				sfd.Filter = ElementTypesManager.ExtensionFilters[(int)type] + "All files (*.*)|*.*";
				sfd.FileName = type.ToString() + "_" + DateTime.Now.ToString(DateTimeFormat.Filename);
				sfd.InitialDirectory = ep?.Directory ?? CommonManager.Settings.LastUsedExportDirectory;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					CommonManager.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					txtSaveAs.Text = sfd.FileName;
				}
			}
		}

		#endregion

		#region btnExport_Click

		private void btnExport_Click(object sender, EventArgs e)
		{
			string filename = txtSaveAs.Text.Trim();

			if (filename.Length > 0 && CommonManager.UserCollection.Count > 0)
			{
				ExportType exportType = (ExportType)cbxExportFormat.SelectedIndex;

				bool isExported = ExporManager.ExportMain(exportType, filename, CommonManager.UserCollection);

				if (isExported)
				{
					MessageBox.Show("Export complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);

					if (cbxClose.Checked)
						this.Close();
				}
			}
		}

		#endregion
	}
}
