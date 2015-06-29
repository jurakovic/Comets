using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application
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
			chUpdateOnStartup.Checked = FormMain.Settings.UpdateOnStartup;
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
			if (rbManualProxy.Checked && (String.IsNullOrEmpty(txtProxy.Text.Trim()) || txtPort.Int() == 0))
			{
				MessageBox.Show("Please enter Proxy and Port\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			FormMain.Settings.UpdateOnStartup = chUpdateOnStartup.Checked;
			FormMain.Settings.NewVersionOnStartup = chNewVersionOnStartup.Checked;
			FormMain.Settings.ExitWithoutConfirm = chExitWithoutConfirm.Checked;
			FormMain.Settings.RememberWindowPosition = chRememberWindowPosition.Checked;
			FormMain.Settings.ShowStatusBar = chShowStatusBar.Checked;

			FormMain.Settings.UseProxy = rbManualProxy.Checked;
			FormMain.Settings.Domain = txtDomain.Text.Trim();
			FormMain.Settings.Username = txtUsername.Text.Trim();
			FormMain.Settings.Password = txtPassword.Text.Trim();
			FormMain.Settings.Proxy = txtProxy.Text.Trim();
			FormMain.Settings.Port = txtPort.Int();

			FormMain.Settings.Location.Name = txtName.Text.Trim();
			FormMain.Settings.Location.Latitude = txtLatitude.Double();
			if (cbxNorthSouth.SelectedIndex == 1) FormMain.Settings.Location.Latitude *= -1;
			FormMain.Settings.Location.Longitude = txtLongitude.Double();
			if (cbxEastWest.SelectedIndex == 1) FormMain.Settings.Location.Longitude *= -1;
			FormMain.Settings.Location.Altitude = txtAltitude.Int();
			FormMain.Settings.Location.Offset = numTimezone.Text.Int();
			FormMain.Settings.Location.DST = chDST.Checked;

			FormMain.Settings.ExternalPrograms = Programs.ToList();

			FormMain.Settings.IsSettingsChanged = true;

			SettingsManager.SaveSettings(FormMain.Settings);
			this.Close();
		}

		#endregion

		#region btnClose_Click

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
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
