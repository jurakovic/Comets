using System.Windows.Forms;
using Comets.Classes;

namespace Comets.Forms.Magnitude
{
    public partial class FormMagnitudeGraph : Form
    {
        #region Properties

        public GraphSettings GraphSettings { get; set; }

        #endregion

        #region Constructor

        public FormMagnitudeGraph(GraphSettings settings, int tag)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.GraphSettings = settings;
            this.Tag = tag;
            LoadGraph();
        }

        #endregion

        #region Form_Load

        private void FormMagnitude_Load(object sender, System.EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.SetWindowMenuItemVisible(true);
        }

        #endregion

        #region Form_Closing

        private void FormMagnitudeGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.RemoveWindowMenuItem((int)this.Tag);
            main.SetWindowMenuItemVisible(main.MdiChildren.Length > 1 ? true : false);
        }

        #endregion

        #region LoadGraph

        public void LoadGraph()
        {
            this.Text = this.Tag + " " + "bla bla";
            // TO DO
        }

        #endregion
    }
}
