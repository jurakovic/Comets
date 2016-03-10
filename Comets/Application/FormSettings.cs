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
			Settings settings = CommonManager.Settings;

			chAutomaticUpdate.Checked = settings.AutomaticUpdate;
			txtUpdateInterval.Text = settings.UpdateInterval.ToString();
			chNewVersionOnStartup.Checked = settings.NewVersionOnStartup;
			chRememberWindowPosition.Checked = settings.RememberWindowPosition;
			chExitWithoutConfirm.Checked = settings.ExitWithoutConfirm;
			cbxIgnoreLongCalculationWarning.Checked = settings.IgnoreLongCalculationWarning;
			chShowStatusBar.Checked = settings.ShowStatusBar;

			rbNoProxy.Checked = !settings.UseProxy;
			rbManualProxy.Checked = settings.UseProxy;
			txtDomain.Text = settings.Domain;
			txtUsername.Text = settings.Username;
			txtPassword.Text = settings.Password;
			txtProxy.Text = settings.Proxy;
			txtPort.Text = settings.Port > 0 ? settings.Port.ToString() : String.Empty;

			txtName.Text = settings.Location.Name;
			txtLatitude.Text = (Math.Abs(settings.Location.Latitude)).ToString("0.000000");
			cbxNorthSouth.SelectedIndex = settings.Location.Latitude >= 0.0 ? 0 : 1;
			txtLongitude.Text = (Math.Abs(settings.Location.Longitude)).ToString("0.000000");
			cbxEastWest.SelectedIndex = settings.Location.Longitude >= 0.0 ? 0 : 1;

			Programs = new BindingList<ExternalProgram>(settings.ExternalPrograms.OrderBy(x => x.Type).ToList());

			dgvPrograms.DataSource = Programs;
			cbxExternalProgram.DataSource = ElementTypesManager.TypeName;
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

			Settings settings = CommonManager.Settings;

			settings.AutomaticUpdate = chAutomaticUpdate.Checked;
			settings.UpdateInterval = txtUpdateInterval.Int();
			settings.NewVersionOnStartup = chNewVersionOnStartup.Checked;
			settings.IgnoreLongCalculationWarning = cbxIgnoreLongCalculationWarning.Checked;
			settings.ExitWithoutConfirm = chExitWithoutConfirm.Checked;
			settings.RememberWindowPosition = chRememberWindowPosition.Checked;
			settings.ShowStatusBar = chShowStatusBar.Checked;

			settings.UseProxy = rbManualProxy.Checked;
			settings.Domain = txtDomain.Text.Trim();
			settings.Username = txtUsername.Text.Trim();
			settings.Password = txtPassword.Text.Trim();
			settings.Proxy = txtProxy.Text.Trim();
			settings.Port = txtPort.Int();

			settings.Location.Name = txtName.Text.Trim();
			settings.Location.Latitude = txtLatitude.Double();
			if (cbxNorthSouth.SelectedIndex == 1) settings.Location.Latitude *= -1; //south
			settings.Location.Longitude = txtLongitude.Double();
			if (cbxEastWest.SelectedIndex == 1) settings.Location.Longitude *= -1; //west

			settings.ExternalPrograms = Programs.ToList();

			settings.IsSettingsChanged = true;

			SettingsManager.SaveSettings(settings);
			this.Close();
		}

		#endregion

		#region Tab: General

		private void txtUpdateInterval_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = ValNumManager.TextBoxValueUpDown(sender, e);
		}

		private void txtUpdateInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
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
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
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
			if (Programs.Count > 0 && dgvPrograms.SelectedRows.Count > 0)
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
			if (Programs.Count > 0 && dgvPrograms.SelectedRows.Count > 0)
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
				fbd.Description = "Select " + ElementTypesManager.Software[cbxExternalProgram.SelectedIndex] + " directory";
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
