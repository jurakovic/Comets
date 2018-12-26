using Comets.Application.Common.Controls.Common;
using Comets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Object = Comets.OrbitViewer.Object;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class DisplayControl : ValueChangeControl
	{
		#region Events

		public event Action OnDisplayChanged;
		public event Action<bool, Object> OnOrbitDisplayChanged;
		public event Action<bool, Object> OnLabelDisplayChanged;
		public event Action<Object> OnCenterObjectChanged;
		public event Action<bool> OnPreserveSelectedOrbitChanged;
		public event Action<bool> OnPreserveSelectedLabelChanged;
		public event Action<bool> OnShowMarkerChanged;

		#endregion

		#region Const

		const string LabelPart = "Label";
		const string OrbitPart = "Orbit";
		const string CenterPart = "Center";

		#endregion

		#region Constructor

		public DisplayControl()
		{
			InitializeComponent();
		}

		#endregion

		#region +EventHandling

		#region Button

		private void btnNoOrbits_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(OrbitPart, false, true);
		}

		private void btnAllOrbits_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(OrbitPart, true, true);
		}

		private void btnNoLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(LabelPart, false, true);
		}

		private void btnAllLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(LabelPart, true, true);
		}

		private void btnDefaultOrbitsLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(OrbitPart, true);
			SetMultipleCheckBoxes(LabelPart, true);

			ValueChangedInternal = true;

			cbxOrbitSaturn.Checked = false;
			cbxOrbitUranus.Checked = false;
			cbxOrbitNeptune.Checked = false;
			cbxOrbitComet.Checked = false;
			cbxLabelComet.Checked = false;
			rbtnCenterSun.Checked = true;
			cbxSelectedOrbit.Checked = true;
			cbxSelectedLabel.Checked = true;

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		private void btnAllOrbitsLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes(OrbitPart, true);
			SetMultipleCheckBoxes(LabelPart, true);
			OnDisplayChanged();
		}

		#endregion

		#region CheckBox

		private void cbxOrbitCommon_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cbx = sender as CheckBox;
			string name = cbx.Name.Replace("cbx" + OrbitPart, "");
			Object orbit = (Object)Enum.Parse(typeof(Object), name);

			OnOrbitDisplayChanged(cbx.Checked, orbit);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		private void cbxLabelCommon_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cbx = sender as CheckBox;
			string name = cbx.Name.Replace("cbx" + LabelPart, "");
			Object label = (Object)Enum.Parse(typeof(Object), name);

			OnLabelDisplayChanged(cbx.Checked, label);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		private void cbxOrbit_CheckedChanged(object sender, EventArgs e)
		{
			OnPreserveSelectedOrbitChanged(cbxSelectedOrbit.Checked);

			if (!ValueChangedInternal)
				OnDisplayChanged();
		}

		private void cbxLabel_CheckedChanged(object sender, EventArgs e)
		{
			OnPreserveSelectedLabelChanged(cbxSelectedLabel.Checked);

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

		#region RadioButton

		private void rbtnCenterCommon_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtn = sender as RadioButton;
			if (rbtn.Checked)
			{
				string name = rbtn.Name.Replace("rbtn" + CenterPart, "");
				Object centerObject = (Object)Enum.Parse(typeof(Object), name);
				OnCenterObjectChanged(centerObject);

				if (!ValueChangedInternal)
					OnDisplayChanged();
			}
		}

		#endregion

		#endregion

		#region Methods

		public void ChangeVisibleOrbit(Object orbit, bool isChecked)
		{
			ValueChangedInternal = true;
			CheckBox cbx = GetControlBoxFromObjectEnum(orbit, typeof(CheckBox), false) as CheckBox;

			if (cbx != null && cbx.Enabled)
				cbx.Checked = isChecked;

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		public void ChangeVisibleLabel(Object label, bool isChecked)
		{
			ValueChangedInternal = true;
			CheckBox cbx = GetControlBoxFromObjectEnum(label, typeof(CheckBox)) as CheckBox;

			if (cbx != null && cbx.Enabled)
				cbx.Checked = isChecked;

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		public void ChangeCenterObject(Object centerObject)
		{
			ValueChangedInternal = true;
			RadioButton rbtn = GetControlBoxFromObjectEnum(centerObject, typeof(RadioButton)) as RadioButton;

			if (rbtn != null && rbtn.Enabled)
				rbtn.Checked = true;

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		public void SetDoubleClickDisplay(Func<bool> centerSelectedCometFunc)
		{
			ValueChangedInternal = true;
			rbtnCenterComet.Checked = true;

			bool isCometCentered = centerSelectedCometFunc(); //orbitPanel.CenterSelectedComet();

			if (!isCometCentered)
			{
				if (cbxSelectedOrbit.Checked && !cbxSelectedLabel.Checked)
				{
					cbxSelectedLabel.Checked = true;
				}
				else if (cbxSelectedLabel.Checked && !cbxSelectedOrbit.Checked)
				{
					cbxSelectedOrbit.Checked = true;
				}
				else
				{
					cbxSelectedOrbit.InvertChecked();
					cbxSelectedLabel.InvertChecked();
				}
			}

			ValueChangedInternal = false;
			OnDisplayChanged();
		}

		public void InvertSelectedCometOrbit()
		{
			cbxSelectedOrbit.InvertChecked();
		}

		public void InvertSelectedCometLabel()
		{
			cbxSelectedLabel.InvertChecked();
		}

		public void InvertMarker()
		{
			cbxMarker.InvertChecked();
		}

		private void SetMultipleCheckBoxes(string namePart, bool isChecked, bool refresh = false)
		{
			ValueChangedInternal = true;

			foreach (CheckBox c in panel.Controls.OfType<CheckBox>())
			{
				if (c.Enabled && c.Name.Contains(namePart))
					c.Checked = isChecked;
			}

			ValueChangedInternal = false;

			if (refresh)
				OnDisplayChanged();
		}

		private Control GetControlBoxFromObjectEnum(Object obj, Type type, bool isLabel = true)
		{
			List<Control> controls = new List<Control>();
			Control control = null;

			foreach (Control c in panel.Controls)
				if (c.Name.EndsWith(obj.ToString()))
					controls.Add(c);

			string name;

			if (type == typeof(CheckBox) && isLabel)
				name = LabelPart;
			else if (type == typeof(CheckBox) && !isLabel)
				name = OrbitPart;
			else //if(type == typeof(RadioButton))
				name = CenterPart;

			control = controls.First(x => x.GetType() == type && x.Name.Contains(name));

			return control;
		}

		#endregion
	}
}
