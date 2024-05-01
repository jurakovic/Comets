using Comets.Application.Common.General;
using Comets.Core;
using Comets.Core.Managers;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormElements : Form, ISave
	{
		#region Events

		public event Action OnElementsTextWriteEnd;

		#endregion

		#region Properties

		private ElementTypesManager.Type ExportType
		{
			get { return (ElementTypesManager.Type)cboFormat.SelectedIndex; }
		}

		#endregion

		#region Constructor

		public FormElements()
		{
			InitializeComponent();
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormExport_Load(object sender, EventArgs e)
		{
			this.cboFormat.DataSource = ElementTypesManager.TypeName.ToList();
		}

		#endregion

		#region ComboBox

		private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Text = String.Format("Orbital Elements ({0})", ElementTypesManager.Software[(int)ExportType]);
			rtxtElements.Text = ExportManager.ExportMain(ExportType, CommonManager.UserCollection);
			OnElementsTextWriteEnd();
		}

		#endregion

		#region Button

		private void btnSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#endregion

		#region ISAve

		public void Save()
		{
			if (CommonManager.UserCollection.Count > 0)
			{
				using (SaveFileDialog sfd = new SaveFileDialog())
				{
					ExternalProgram ep = CommonManager.Settings.ExternalPrograms.Find(x => x.Type == (int)ExportType);

					sfd.Filter = ElementTypesManager.ExtensionFilters[(int)ExportType] + "All files (*.*)|*.*";
					sfd.FileName = ExportType.ToString() + "_" + DateTime.Now.ToString(DateTimeFormat.Filename);
					sfd.InitialDirectory = ep?.Directory ?? CommonManager.Settings.LastUsedExportDirectory;

					if (sfd.ShowDialog() == DialogResult.OK)
					{
						string text = String.Join(Environment.NewLine, rtxtElements.Lines); //because .Text has LF instead of CRLF
						bool isExported = ExportManager.WriteToFile(sfd.FileName, text);

						if (isExported)
						{
							CommonManager.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
							MessageBox.Show("Export complete.\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
			}
		}

		#endregion
	}
}
