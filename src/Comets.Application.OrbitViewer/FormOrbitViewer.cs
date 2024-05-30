using Comets.Application.Common.General;
using Comets.Core;
using System;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer
{
	public partial class FormOrbitViewer : ThemeForm, ISave
	{
		#region Events

		public event Action<bool> OnToolboxVisibleChanged;

		#endregion

		#region Constructor

		public FormOrbitViewer(CometCollection comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			InitializeComponent();

			orbitViewerControl.LoadControl(comets, filters, sortProperty, sortAscending);
		}

		#endregion

		#region EventHandling

		private void FormOrbitViewer_Activated(object sender, EventArgs e)
		{
			OnToolboxVisibleChanged(orbitViewerControl.ToolboxVisible);
		}

		private void FormOrbitViewer_Deactivate(object sender, EventArgs e)
		{
			orbitViewerControl.StopSimulation();
		}

		private void FormOrbitViewer_KeyDown(object sender, KeyEventArgs e)
		{
			orbitViewerControl.OrbitViewerControl_KeyDown(sender, e);
		}

		#endregion

		#region Methods

		public void ShowToolbox(bool visible)
		{
			orbitViewerControl.ShowToolbox(visible);
		}

		#endregion

		#region ISave

		public void Save()
		{
			orbitViewerControl.Save();
		}

		#endregion
	}
}
