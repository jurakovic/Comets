using Comets.Classes;
using Comets.Helpers;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Forms
{
	public partial class FormSettings : Form
	{
		#region Properties

		BindingList<ExternalProgram> Programs { get; set; }

		#endregion

		#region Constructor

		public FormSettings()
		{
			InitializeComponent();
		}

		#endregion

		#region Form_Load

		private void FormSettings_Load(object sender, EventArgs e)
		{
			txtAppData.Text = FormMain.Settings.AppData;
			txtDownloads.Text = FormMain.Settings.Downloads;
			chDownloadOnStartup.Checked = FormMain.Settings.DownloadOnStartup;
			chNewVersionOnStartup.Checked = FormMain.Settings.NewVersionOnStartup;
			chRememberWindowPosition.Checked = FormMain.Settings.RememberWindowPosition;
			chExitWithoutConfirm.Checked = FormMain.Settings.ExitWithoutConfirm;
			chShowStatusBar.Checked = FormMain.Settings.ShowStatusBar;

			rbNoProxy.Checked = !FormMain.Settings.UseProxy;
			rbManualProxy.Checked = FormMain.Settings.UseProxy;
			txtDomain.Text = FormMain.Settings.Domain;
			txtUsername.Text = FormMain.Settings.Username;
			txtPassword.Text = FormMain.Settings.Password;
			txtProxy.Text = FormMain.Settings.Proxy;
			txtPort.Text = FormMain.Settings.Port > 0 ? FormMain.Settings.Port.ToString() : String.Empty;

			txtName.Text = FormMain.Settings.Location.Name;
			txtLatitude.Text = (Math.Abs(FormMain.Settings.Location.Latitude)).ToString("0.000000");
			cbxNorthSouth.SelectedIndex = FormMain.Settings.Location.Latitude >= 0.0 ? 0 : 1;
			txtLongitude.Text = (Math.Abs(FormMain.Settings.Location.Longitude)).ToString("0.000000");
			cbxEastWest.SelectedIndex = FormMain.Settings.Location.Longitude >= 0.0 ? 0 : 1;
			txtAltitude.Text = FormMain.Settings.Location.Altitude.ToString();
			numTimezone.Value = FormMain.Settings.Location.Offset;
			chDST.Checked = FormMain.Settings.Location.DST;

			Programs = new BindingList<ExternalProgram>(FormMain.Settings.ExternalPrograms.OrderBy(x => x.Type).ToList());

			dgvPrograms.DataSource = Programs;
			cbxExternalProgram.DataSource = ElementTypes.TypeName;
		}

		#endregion

		#region btnOK_Click

		private void btnOK_Click(object sender, EventArgs e)
		{
			FormMain.Settings.AppData = txtAppData.Text.Trim();
			FormMain.Settings.Database = FormMain.Settings.AppData + "\\Comets.db";
			FormMain.Settings.Downloads = txtDownloads.Text.Trim();
			FormMain.Settings.DownloadOnStartup = chDownloadOnStartup.Checked;
			FormMain.Settings.NewVersionOnStartup = chNewVersionOnStartup.Checked;
			FormMain.Settings.ExitWithoutConfirm = chExitWithoutConfirm.Checked;
			FormMain.Settings.RememberWindowPosition = chRememberWindowPosition.Checked;
			FormMain.Settings.ShowStatusBar = chShowStatusBar.Checked;

			FormMain.Settings.UseProxy = rbManualProxy.Checked;
			FormMain.Settings.Domain = txtDomain.Text.Trim();
			FormMain.Settings.Username = txtUsername.Text.Trim();
			FormMain.Settings.Password = txtPassword.Text.Trim();
			FormMain.Settings.Proxy = txtProxy.Text.Trim();
			FormMain.Settings.Port = txtPort.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtPort.Text.Trim());

			FormMain.Settings.Location.Name = txtName.Text.Trim();
			FormMain.Settings.Location.Latitude = Convert.ToDouble(txtLatitude.Text.Trim());
			if (cbxNorthSouth.SelectedIndex == 1) FormMain.Settings.Location.Latitude *= -1;
			FormMain.Settings.Location.Longitude = Convert.ToDouble(txtLongitude.Text.Trim());
			if (cbxEastWest.SelectedIndex == 1) FormMain.Settings.Location.Longitude *= -1;
			FormMain.Settings.Location.Altitude = Convert.ToInt32(txtAltitude.Text.Trim());
			FormMain.Settings.Location.Offset = Convert.ToInt32(numTimezone.Text.Trim());
			FormMain.Settings.Location.DST = chDST.Checked;

			FormMain.Settings.ExternalPrograms = Programs.ToList();

			FormMain.Settings.IsSettingsChanged = true;

			Settings.SaveSettings(FormMain.Settings);
			this.Close();
		}

		#endregion

		#region btnClose_Click

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Tab: General

		private void btnAppData_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = FormMain.Settings.AppData;
				fbd.Description = "Select Application data directory";
				fbd.ShowNewFolderButton = true;

				if (fbd.ShowDialog() == DialogResult.OK)
					txtAppData.Text = fbd.SelectedPath;
			}
		}

		private void btnDownloads_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = FormMain.Settings.Downloads;
				fbd.Description = "Select Downloads directory";
				fbd.ShowNewFolderButton = true;

				if (fbd.ShowDialog() == DialogResult.OK)
					txtDownloads.Text = fbd.SelectedPath;
			}
		}

		private void btnDefaultAppData_Click(object sender, EventArgs e)
		{
			txtAppData.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Comets";
		}

		private void btnDefaultDownloads_Click(object sender, EventArgs e)
		{
			txtDownloads.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Comets\\Downloads";
		}

		#endregion

		#region Tab: Network

		private void rbCommon_CheckedChanged(object sender, EventArgs e)
		{
			pnlProxy.Enabled = rbManualProxy.Checked;
		}

		#endregion

		#region Tab: Location

		private void txtLatitude_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 6, 0, 90);
		}

		private void txtLongitude_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 6, 0, 180);
		}

		#endregion

		#region Tab: Programs

		private void btnAdd_Click(object sender, EventArgs e)
		{
			gbxPrograms.Visible = false;
			gbxAddProgram.Visible = true;

			cbxProgram_SelectedIndexChanged(null, null);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (Programs.Any())
			{
				ExternalProgram selectedProgram = (dgvPrograms.SelectedRows[0].DataBoundItem as ExternalProgram);

				gbxPrograms.Visible = false;
				gbxAddProgram.Visible = true;

				cbxExternalProgram.SelectedIndex = selectedProgram.Type;
				txtDirectory.Text = selectedProgram.Directory;
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Programs.Any())
			{
				ExternalProgram selectedProgram = (dgvPrograms.SelectedRows[0].DataBoundItem as ExternalProgram);
				Programs.Remove(selectedProgram);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Programs.Clear();
		}

		private void cbxProgram_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Programs.Any(x => x.Type == cbxExternalProgram.SelectedIndex))
				txtDirectory.Text = Programs.Where(x => x.Type == cbxExternalProgram.SelectedIndex).First().Directory;
			else
				txtDirectory.Text = String.Empty;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.Description = "Select " + ElementTypes.Software[cbxExternalProgram.SelectedIndex] + " directory";
				fbd.ShowNewFolderButton = true;

				if (fbd.ShowDialog() == DialogResult.OK)
					txtDirectory.Text = fbd.SelectedPath;
			}
		}

		private void btnProgramsCancel_Click(object sender, EventArgs e)
		{
			gbxPrograms.Visible = true;
			gbxAddProgram.Visible = false;
		}

		private void btnProgramsOk_Click(object sender, EventArgs e)
		{
			if (txtDirectory.Text.Length > 0 && System.IO.Directory.Exists(txtDirectory.Text))
			{
				Programs.Add(new ExternalProgram(cbxExternalProgram.SelectedIndex, txtDirectory.Text));

				cbxExternalProgram.SelectedIndex = 0;
				txtDirectory.Text = String.Empty;

				gbxPrograms.Visible = true;
				gbxAddProgram.Visible = false;

				dgvPrograms.ClearSelection();
			}
		}

		#endregion
	}
}
