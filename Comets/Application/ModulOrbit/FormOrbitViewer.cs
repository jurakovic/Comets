using Comets.BusinessLayer.Business;
using System;
using System.Windows.Forms;

namespace Comets.Application.ModulOrbit
{
	public partial class FormOrbitViewer : Form, ISave
	{
		#region Constructor

		public FormOrbitViewer(CometCollection comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			InitializeComponent();
			orbitViewerControl.LoadControl(comets, filters, sortProperty, sortAscending);
		}

		#endregion

		#region + Form

		private void FormOrbitViewer_Activated(object sender, EventArgs e)
		{
			(this.MdiParent as FormMain).SetToolBoxMenuItemChecked(orbitViewerControl.ToolboxVisible);
		}

		private void FormOrbitViewer_Deactivate(object sender, EventArgs e)
		{
			orbitViewerControl.StopSimulation();
		}

		private void FormOrbitViewer_KeyDown(object sender, KeyEventArgs e)
		{
			orbitViewerControl.OrbitViewerControl_KeyDown(sender, e);
		}

		public void ShowToolbox(bool visible)
		{
			orbitViewerControl.ShowToolbox(visible);
		}

		public void Save()
		{
			orbitViewerControl.Save();
		}

		#endregion
	}
}
