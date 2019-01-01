using Comets.Application.Common.Controls.Common;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class CometControl : ValueChangeControl
	{
		#region Events

		public event Action OnSelectedCometChanged;
		public event Action OnFilter;
		public event Action OnMark;
		public event Action<bool> OnPreserveSelectedOrbitChanged;
		public event Action<bool> OnPreserveSelectedLabelChanged;
		public event Action<bool> OnShowMarkerChanged;
		public event Action OnDisplayChanged;

		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<OVComet> Comets { get; private set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OVComet SelectedComet
		{
			get { return Comets?.ElementAtOrDefault(cboComet.SelectedIndex); }
			set { cboComet.SelectedIndex = Comets.IndexOf(value); }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get { return cboComet.SelectedIndex; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Focused
		{
			get { return cboComet.Focused; }
		}

		#endregion

		#region Constructor

		public CometControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
				OnSelectedCometChanged();
		}

		//private void comboBoxCommon_MouseHover(object sender, EventArgs e)
		//{
		//	(sender as ComboBox).Focus();
		//}

		private void btnFilter_Click(object sender, EventArgs e)
		{
			OnFilter();
		}

		private void btnMark_Click(object sender, EventArgs e)
		{
			OnMark();
		}

		private void cbxOrbit_CheckedChanged(object sender, EventArgs e)
		{
			OnPreserveSelectedOrbitChanged(cbxOrbit.Checked);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		private void cbxLabel_CheckedChanged(object sender, EventArgs e)
		{
			OnPreserveSelectedLabelChanged(cbxLabel.Checked);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		private void cbxMarker_CheckedChanged(object sender, EventArgs e)
		{
			OnShowMarkerChanged(cbxMarker.Checked);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		#endregion

		#region Methods

		public void BindCollection(CometCollection comets, string name = null)
		{
			ValueChangedInternal = true;

			List<OVComet> ovcomets = new List<OVComet>();
			comets.ForEach(x => ovcomets.Add(new OVComet(x)));

			this.Comets = ovcomets;

			cboComet.DisplayMember = "Name";
			cboComet.DataSource = Comets;

			if (Comets.Count > 0)
			{
				if (name != null && Comets.Any(x => x.Name == name))
					SelectedComet = Comets.First(x => x.Name == name);
				else
					SelectedComet = Comets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First(); //comet with nearest perihelion date
			}
			else
			{
				OnSelectedCometChanged();
			}

			ValueChangedInternal = false;
		}

		public void SelectCometByName(string name)
		{
			SelectedComet = Comets.FirstOrDefault(x => x.Name == name);
		}

		public void UnmarkComets()
		{
			Comets.ForEach(x => x.IsMarked = false);
		}

		public void SetDoubleClickDisplay(Func<bool> centerSelectedCometFunc)
		{
			ValueChangedInternal = true;

			bool isCometCentered = centerSelectedCometFunc(); //orbitPanel.CenterSelectedComet();

			if (!isCometCentered)
			{
				if (cbxOrbit.Checked && !cbxLabel.Checked)
				{
					cbxLabel.Checked = true;
				}
				else if (cbxLabel.Checked && !cbxOrbit.Checked)
				{
					cbxOrbit.Checked = true;
				}
				else
				{
					cbxOrbit.InvertChecked();
					cbxLabel.InvertChecked();
				}
			}

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		public void InvertSelectedCometOrbit()
		{
			cbxOrbit.InvertChecked();
		}

		public void InvertSelectedCometLabel()
		{
			cbxLabel.InvertChecked();
		}

		public void InvertMarker()
		{
			cbxMarker.InvertChecked();
		}

		#endregion
	}
}
