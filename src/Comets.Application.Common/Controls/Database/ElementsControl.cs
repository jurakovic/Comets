using Comets.Core;
using Comets.Core.Managers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Database
{
	public partial class ElementsControl : UserControl
	{
		#region Properties

		private Comet SelectedComet { get; set; }

		#endregion

		#region Constructor

		public ElementsControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void btnJplInfo_Click(object sender, EventArgs e)
		{
			if (SelectedComet != null)
				CometManager.OpenJplInfo(SelectedComet.id);
		}

		#endregion

		#region Methods

		public void DataBind(Comet c)
		{
			this.SelectedComet = c;
			this.ctxMenu.SelectedComet = c;

			string format6 = "0.000000";
			string format4 = "0.0000";
			int minPeriod = 1000;

			txtName.Text = c.full;

			txtPerihDate.Text = String.Format("{0}-{1:00}-{2:00}.{3:0000}", c.Ty, c.Tm, c.Td, c.Th);
			txtPeriod.Text = c.P < minPeriod ? c.P.ToString(format6) : String.Empty;
			txtMeanMotion.Text = c.P < minPeriod ? c.n.ToString(format6) : String.Empty;

			txtPerihDist.Text = c.q.ToString(format6);
			txtAphDist.Text = c.P < minPeriod ? c.Q.ToString(format6) : String.Empty;
			txtSemiMajorAxis.Text = c.P < minPeriod ? c.a.ToString(format6) : String.Empty;

			txtEcc.Text = c.e.ToString(format6);
			txtAscNode.Text = c.N.ToString(format4);
			txtMagG.Text = c.g.ToString("0.0");
			txtMagK.Text = c.k.ToString("0.0");

			txtIncl.Text = c.i.ToString(format4);
			txtArgPeri.Text = c.w.ToString(format4);
			txtEquinox.Text = "2000.0";

			//t_sortKey.Text = c.sortkey.ToString("0.00000000000");
			//t_sortKey.Text = c.idKey;
		}

		public void ClearData()
		{
			this.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
		}

		#endregion
	}
}
