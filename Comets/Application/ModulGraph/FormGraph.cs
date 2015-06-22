using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System.Windows.Forms;

namespace Comets.Application.ModulGraph
{
	public partial class FormGraph : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; set; }

		#endregion

		#region Constructor

		public FormGraph(GraphSettings settings, int tag)
		{
			InitializeComponent();

			this.DoubleBuffered = true;

			this.GraphSettings = settings;
			this.Tag = tag;
		}

		#endregion

		#region Form_Load

		private void FormMagnitude_Load(object sender, System.EventArgs e)
		{
			FormMain main = this.MdiParent as FormMain;
			main.SetWindowMenuItemVisible(true);

			LoadGraph();
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
			this.Text = this.Tag + " " + GraphSettings.ToString();
			EphemerisManager.GenerateGraph(GraphSettings, this.chart1);
			GraphSettings.Results.Clear();
		}

		#endregion
	}
}
