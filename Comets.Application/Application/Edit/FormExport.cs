using Comets.Core;
using Comets.Core.Managers;
using System;
using System.IO;
using System.Windows.Forms;
using ExportType = Comets.Core.Managers.ElementTypesManager.Type;

namespace Comets.Application.Edit
{
	public partial class FormExport : Form
	{
		#region Constructor

		public FormExport()
		{
			InitializeComponent();
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormExport_Load(object sender, EventArgs e)
		{
			this.cbxExportFormat.DataSource = ElementTypesManager.TypeName;
			this.lblTotalComets.Text = CommonManager.UserCollection.Count.ToString();
		}

		#endregion

		#region ComboBox

		private void cbxExportFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.lblExportFormat.Text = cbxExportFormat.Text;
		}

		#endregion

		#region Button

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

		#endregion
	}
}
