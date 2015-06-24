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

		public FormGraph(GraphSettings settings)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.GraphSettings = settings;
		}

		#endregion

		#region Form_Load

		private void FormGraph_Load(object sender, System.EventArgs e)
		{
			LoadGraph();
		}

		#endregion

		#region LoadGraph

		public void LoadGraph()
		{
			this.Text = GraphSettings.ToString();
			EphemerisManager.GenerateGraph(GraphSettings, this.chart1);
			GraphSettings.Results.Clear();
		}

		#endregion
	}
}
