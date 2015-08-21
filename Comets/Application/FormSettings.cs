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
			txtUpdateInterval.Tag = new ValNum(1, 99);
			txtLatitude.Tag = new ValNum(0, 90.0, 6);
			txtLongitude.Tag = new ValNum(0, 180.0, 6);
		}

		#endregion

		#region Form_Load

		private void FormSettings_Load(object sender, EventArgs e)
		{
			chAutomaticUpdate.Checked = FormMain.Settings.AutomaticUpdate;
			txtUpdateInterval.Text = FormMain.Settings.UpdateInterval.ToString();
			chNewVersionOnStartup.Checked = FormMain.Settings.NewVersionOnStartup;
			chRememberWindowPosition.Checked = FormMain.Settings.RememberWindowPosition;
			chExitWithoutConfirm.Checked = FormMain.Settings.ExitWithoutConfirm;
			cbxIgnoreLongCalculationWarning.Checked = FormMain.Settings.IgnoreLongCalculationWarning;
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
				tabControl1.SelectedIndex = 3;
				MessageBox.Show("Please enter Proxy and Port\t\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (chAutomaticUpdate.Checked && String.IsNullOrEmpty(txtUpdateInterval.Text.Trim()))
			{
				tabControl1.SelectedIndex = 0;
				MessageBox.Show("Please enter days interval for automatic update\t\t", "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			FormMain.Settings.AutomaticUpdate = chAutomaticUpdate.Checked;
			FormMain.Settings.UpdateInterval = txtUpdateInterval.Int();
			FormMain.Settings.NewVersionOnStartup = chNewVersionOnStartup.Checked;
			FormMain.Settings.IgnoreLongCalculationWarning = cbxIgnoreLongCalculationWarning.Checked;
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

		#region Tab: General

		private void txtUpdateInterval_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtUpdateInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		#endregion

		#region Tab: Network

		private void rbCommon_CheckedChanged(object sender, EventArgs e)
		{
			pnlProxy.Enabled = rbManualProxy.Checked;
		}

		#endregion

		#region Tab: Location

		private void txtLatitudeLongitude_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
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
			if (Programs.Any() && dgvPrograms.SelectedRows.Count > 0)
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
			if (Programs.Any() && dgvPrograms.SelectedRows.Count > 0)
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
			string text = txtDirectory.Text.Trim();

			if (text.Length > 0 && System.IO.Directory.Exists(text))
			{
				Programs.Add(new ExternalProgram(cbxExternalProgram.SelectedIndex, text));

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
