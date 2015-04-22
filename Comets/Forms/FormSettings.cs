using Comets.Classes;
using System;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            txtAppData.Text = FormMain.Settings.AppData;
            txtDownloads.Text = FormMain.Settings.Downloads;
            chDownloadOnStartup.Checked = FormMain.Settings.DownloadOnStartup;
            chNewVersionOnStartup.Checked = FormMain.Settings.NewVersionOnStartup;
            chRememberWindowPosition.Checked = FormMain.Settings.RememberWindowPosition;
            chExitWithoutConfirm.Checked = FormMain.Settings.ExitWithoutConfirm;

            rbNoProxy.Checked = !FormMain.Settings.UseProxy;
            rbManualProxy.Checked = FormMain.Settings.UseProxy;
            txtDomain.Text = FormMain.Settings.Domain;
            txtUsername.Text = FormMain.Settings.Username;
            txtPassword.Text = FormMain.Settings.Password;
            txtProxy.Text = FormMain.Settings.Proxy;
            txtPort.Text = FormMain.Settings.Port > 0 ? FormMain.Settings.Port.ToString() : string.Empty;

            txtName.Text = FormMain.Settings.Name;
            txtLatitude.Text = (Math.Abs(FormMain.Settings.Latitude)).ToString("0.000000");
            cbxNorthSouth.SelectedIndex = FormMain.Settings.Latitude >= 0.0 ? 0 : 1;
            txtLongitude.Text = (Math.Abs(FormMain.Settings.Longitude)).ToString("0.000000");
            cbxEastWest.SelectedIndex = FormMain.Settings.Longitude >= 0.0 ? 0 : 1;
            txtAltitude.Text = FormMain.Settings.Altitude.ToString();
            numTimezone.Value = FormMain.Settings.Timezone;
            chDST.Checked = FormMain.Settings.DST;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FormMain.Settings.AppData = txtAppData.Text.Trim();
            FormMain.Settings.Downloads = txtDownloads.Text.Trim();
            FormMain.Settings.DownloadOnStartup = chDownloadOnStartup.Checked;
            FormMain.Settings.NewVersionOnStartup = chNewVersionOnStartup.Checked;
            FormMain.Settings.ExitWithoutConfirm = chExitWithoutConfirm.Checked;
            FormMain.Settings.RememberWindowPosition = chRememberWindowPosition.Checked;

            FormMain.Settings.UseProxy = rbManualProxy.Checked;
            FormMain.Settings.Domain = txtDomain.Text.Trim();
            FormMain.Settings.Username = txtUsername.Text.Trim();
            FormMain.Settings.Password = txtPassword.Text.Trim();
            FormMain.Settings.Proxy = txtProxy.Text.Trim();
            FormMain.Settings.Port = txtPort.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtPort.Text.Trim());

            FormMain.Settings.Name = txtName.Text.Trim();
            FormMain.Settings.Latitude = Convert.ToDouble(txtLatitude.Text.Trim());
            if (cbxNorthSouth.SelectedIndex == 1) FormMain.Settings.Latitude *= -1;
            FormMain.Settings.Longitude = Convert.ToDouble(txtLongitude.Text.Trim());
            if (cbxEastWest.SelectedIndex == 1) FormMain.Settings.Longitude *= -1;
            FormMain.Settings.Altitude = Convert.ToInt32(txtAltitude.Text.Trim());
            FormMain.Settings.Timezone = Convert.ToInt32(numTimezone.Text.Trim());
            FormMain.Settings.DST = chDST.Checked;

            Settings.SaveSettings(FormMain.Settings);
            this.Close();
        }

        private void rbCommon_CheckedChanged(object sender, EventArgs e)
        {
            pnlProxy.Enabled = rbManualProxy.Checked;
        }

        private void txtLatitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ValidateKeyPress(sender, e, 10, true, 6);
        }

        private void txtLongitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ValidateKeyPress(sender, e, 10, true, 6);
        }

        bool ValidateKeyPress(object sender, KeyPressEventArgs e, int length, bool separator, int decimals)
        {
            string text = (sender as TextBox).Text;

            if (length > 0 && !char.IsControl(e.KeyChar) && text.Length >= length) return true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) return true;

            if (!separator && (e.KeyChar == '.')) return true;

            if (decimals > 0 && !char.IsControl(e.KeyChar) && (text.IndexOf('.') > -1) && text.Substring(text.IndexOf('.'), text.Length - text.IndexOf('.')).Length > decimals) return true;

            if (separator && (e.KeyChar == '.') && (text.IndexOf('.') > -1)) return true;

            return false;
        }
    }
}
