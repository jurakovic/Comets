using System.Windows.Forms;
using Comets.Classes;
using Comets.Helpers;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisResult : Form
    {
        #region Properties

        public EphemerisSettings EphemerisSettings { get; set; }

        #endregion

        #region Constructor

        public FormEphemerisResult(EphemerisSettings settings, int tag)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.EphemerisSettings = settings;
            this.Tag = tag;
        }

        #endregion

        #region Form_Load

        private void FormEphemerisResult_Load(object sender, System.EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.SetWindowMenuItemVisible(true);

            LoadResults();
        }

        #endregion

        #region Form_Closing

        private void FormEphemerisResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.RemoveWindowMenuItem((int)this.Tag);
            main.SetWindowMenuItemVisible(main.MdiChildren.Length > 1 ? true : false);
        }

        #endregion

        #region LoadResults

        public async void LoadResults()
        {
            this.Text = this.Tag + " " + EphemerisSettings.ToString();
            richTextBox.Text = await EphemerisHelper.GenerateEphemeris(EphemerisSettings);
            EphemerisSettings.Results.Clear();
        }

        #endregion
    }
}
