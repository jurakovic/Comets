using System.Windows.Forms;
using Comets.Classes;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisResult : Form
    {
        public EphemerisSettings EphemerisSettings { get; set; }

        public FormEphemerisResult(EphemerisSettings settings, int tag)
        {
            InitializeComponent();

            this.EphemerisSettings = settings;
            this.Tag = tag;
            LoadResults();
        }

        private void FormEphemerisResult_Load(object sender, System.EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.SetWindowMenuItemVisible(true);
        }

        private void FormEphemerisResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.RemoveWindowMenuItem((int)this.Tag);
            main.SetWindowMenuItemVisible(main.MdiChildren.Length > 1 ? true : false);
        }

        public void LoadResults()
        {
            this.Text = this.Tag + " " + EphemerisSettings.ToString();
            richTextBox.Text = EphemerisSettings.EphemerisResult;
        }
    }
}
