using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.ModulOrbit
{
	public partial class FormOrbitViewer : Form
	{
		#region Consts

		const int DefaultScrollVert = 220;
		const int DefaultScrollHorz = 255;
		const int DefaultScrollZoom = 145;

		const bool DefaultToolBoxVisible = true;

		readonly string[] TimeStepItems = {
			"1 Hour",
			"6 Hours",
			"12 Hours",
			"1 Day",
			"3 Days",
			"10 Days",
			"1 Month",
			"3 Months",
			"6 Months",
			"1 Year",
			"3 Years",
			"10 Years"
		};

		readonly ATimeSpan[] TimeStepSpan = {
			new ATimeSpan( 0, 0, 0, 1, 0, 0),
			new ATimeSpan( 0, 0, 0, 6, 0, 0),
			new ATimeSpan( 0, 0, 0,12, 0, 0),
			new ATimeSpan( 0, 0, 1, 0, 0, 0),
			new ATimeSpan( 0, 0, 3, 0, 0, 0),
			new ATimeSpan( 0, 0,10, 0, 0, 0),
			new ATimeSpan( 0, 1, 0, 0, 0, 0),
			new ATimeSpan( 0, 3, 0, 0, 0, 0),
			new ATimeSpan( 0, 6, 0, 0, 0, 0),
			new ATimeSpan( 1, 0, 0, 0, 0, 0),
			new ATimeSpan( 3, 0, 0, 0, 0, 0),
			new ATimeSpan(10, 0, 0, 0, 0, 0)
		};

		#endregion

		#region Fields

		private bool IsKeyboardScroll;
		private bool IsMouseRotate;
		private bool IsMouseWheelZoom;
		private Point StartDrag;

		private Timer Timer;

		private ATimeSpan TimeStep;
		private int SimulationDirection;
		private bool IsSimulationStarted;

		private bool ValueChangedByEvent;

		List<Comet> Comets;
		private FilterCollection Filters;
		private string SortProperty;
		private bool SortAscending;

		private List<OVComet> OVComets;
		private OVComet SelectedComet;

		private DateTime DefaultDateTime;

		#endregion

		#region Properties

		private DateTime _selectedDateTime;
		private DateTime SelectedDateTime
		{
			get { return _selectedDateTime; }
			set
			{
				_selectedDateTime = FormDateTime.RangeDateTime(value);
				btnDate.Text = _selectedDateTime.ToString(FormMain.DateTimeFormat);

				if (IsSimulationStarted && !ValueChangedByEvent)
					Timer.Stop();

				if (orbitPanel.IsPaintEnabled)
				{
					orbitPanel.ATime = new ATime(_selectedDateTime, FormMain.Settings.Location.Timezone);
					orbitPanel.Invalidate();
				}
			}
		}

		private bool _toolboxVisible;
		public bool ToolboxVisible
		{
			get { return _toolboxVisible; }
			set
			{
				_toolboxVisible = value;

				if (!ValueChangedByEvent)
					(this.MdiParent as FormMain).SetToolBoxMenuItemChecked(value);
			}
		}

		#endregion

		#region Constructor

		public FormOrbitViewer(List<Comet> comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			InitializeComponent();

			Timer = new Timer();
			Timer.Interval = 50;
			Timer.Tick += new System.EventHandler(this.timer_Tick);

			Comets = comets;
			Filters = filters;
			SortProperty = sortProperty;
			SortAscending = sortAscending;

			OVComets = TransformComets(comets);
			DefaultDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0, DateTimeKind.Local);

			ValueChangedByEvent = true;

			scrollVert.Value = DefaultScrollVert;
			scrollHorz.Value = DefaultScrollHorz;
			scrollZoom.Value = DefaultScrollZoom;

			SelectedDateTime = DefaultDateTime;
			ToolboxVisible = DefaultToolBoxVisible;
			SimulationDirection = ATime.TimeIncrement;

			ValueChangedByEvent = false;
		}

		#endregion

		#region Form_Load

		private void FormOrbit_Load(object sender, System.EventArgs e)
		{
			BindList();

			cbxLabelComet.Enabled = cbxOrbitComet.Enabled = rbtnCenterComet.Enabled = OVComets.Any();
			cbxLabelComet.Checked = cbxOrbitComet.Checked = OVComets.Any();

			cboTimestep.DataSource = TimeStepItems;
			cboTimestep.SelectedIndex = 3;

			ATime atime = new ATime(SelectedDateTime, FormMain.Settings.Location.Timezone);
			orbitPanel.LoadPanel(SelectedComet, atime);
			orbitPanel.Invalidate();
		}

		#endregion

		#region Form_Activated

		private void FormOrbitViewer_Activated(object sender, EventArgs e)
		{
			(this.MdiParent as FormMain).SetToolBoxMenuItemChecked(ToolboxVisible);
		}

		#endregion

		#region Form_Deactivate

		private void FormOrbitViewer_Deactivate(object sender, EventArgs e)
		{
			PauseSimulation();
		}

		#endregion

		#region orbitPanel_Resize

		private void orbitPanel_Resize(object sender, EventArgs e)
		{
			orbitPanel.Offscreen = new Bitmap(orbitPanel.Width, orbitPanel.Height);
			orbitPanel.Invalidate();
		}

		#endregion

		#region + ToolBox

		#region Comet

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedComet = OVComets.ElementAt(cboComet.SelectedIndex);

			if (orbitPanel.IsPaintEnabled)
			{
				orbitPanel.LoadPanel(SelectedComet, orbitPanel.ATime);
				ClearOrbits();
				orbitPanel.Invalidate();
			}

			this.Text = "Orbit Viewer - " + SelectedComet.Name;
		}

		private void btnFilter_Click(object sender, EventArgs e)
		{
			string lastSelected = SelectedComet.Name;

			using (FormDatabase fdb = new FormDatabase(Comets, Filters, SortProperty, SortAscending, true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					Comets = fdb.Comets;
					Filters = fdb.Filters;
					SortProperty = fdb.SortProperty;
					SortAscending = fdb.SortAscending;

					OVComets = TransformComets(Comets);

					BindList();

					if (OVComets.Any(x => x.Name == lastSelected))
						cboComet.SelectedIndex = OVComets.IndexOf(OVComets.First(x => x.Name == lastSelected));
				}
			}
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			if (OVComets.Any() && orbitPanel.Comets.Count != OVComets.Count)
			{
				ValueChangedByEvent = true;

				orbitPanel.LoadPanel(OVComets.ToList(), orbitPanel.ATime, cboComet.SelectedIndex);
				ClearOrbits();
				rbtnMultipleMode.Checked = true;
				orbitPanel.Invalidate();

				ValueChangedByEvent = false;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			orbitPanel.ClearComets();
			orbitPanel.Invalidate();
		}

		private void comboBoxCommon_MouseEnter(object sender, EventArgs e)
		{
			(sender as ComboBox).Focus();
		}

		#endregion

		#region Mode

		private void rbtnMode_CheckedChanged(object sender, EventArgs e)
		{
			bool tempChanged = ValueChangedByEvent;
			ValueChangedByEvent = true;

			orbitPanel.MultipleMode = rbtnMultipleMode.Checked;
			cbxSelectedOrbit.Enabled = rbtnMultipleMode.Checked && !cbxOrbitComet.Checked;
			cbxSelectedLabel.Enabled = rbtnMultipleMode.Checked && !cbxLabelComet.Checked;

			ClearOrbits();

			ValueChangedByEvent = tempChanged;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		#endregion

		#region Orbits, Labels, Center

		private void btnNoOrbits_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Orbit", false, true);
		}

		private void btnAllOrbits_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Orbit", true, true);
		}

		private void btnNoLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Label", false, true);
		}

		private void btnAllLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Label", true, true);
		}

		private void btnAllOrbitsLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Orbit", true);
			SetMultipleCheckBoxes("Label", true);
			orbitPanel.Invalidate();
		}

		private void btnDefaultOrbitsLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Orbit", true);
			SetMultipleCheckBoxes("Label", true);

			ValueChangedByEvent = true;
			cbxOrbitSaturn.Checked = false;
			cbxOrbitUranus.Checked = false;
			cbxOrbitNeptune.Checked = false;
			rbtnCenterSun.Checked = true;
			cbxSelectedOrbit.Checked = true;
			cbxSelectedLabel.Checked = true;
			ValueChangedByEvent = false;

			orbitPanel.Invalidate();
		}

		private void SetMultipleCheckBoxes(string name, bool isChecked, bool refresh = false)
		{
			ValueChangedByEvent = true;

			foreach (Control c in gbxOrbitsLabelsCenter.Controls)
				if (c is CheckBox && c.Enabled && c.Name.Contains(name))
					(c as CheckBox).Checked = isChecked;

			ValueChangedByEvent = false;

			if (refresh)
				orbitPanel.Invalidate();
		}

		private void cbxOrbitCommon_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cbx = sender as CheckBox;
			string name = cbx.Name.Replace("cbxOrbit", "");
			OrbitPanel.Object orbit = (OrbitPanel.Object)Enum.Parse(typeof(OrbitPanel.Object), name);

			if (cbx.Checked && !orbitPanel.OrbitDisplay.Contains(orbit))
				orbitPanel.OrbitDisplay.Add(orbit);
			else
				orbitPanel.OrbitDisplay.Remove(orbit);

			if (orbit == OrbitPanel.Object.Comet)
				cbxSelectedOrbit.Enabled = rbtnMultipleMode.Checked && !cbx.Checked;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void cbxLabelCommon_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cbx = sender as CheckBox;
			string name = cbx.Name.Replace("cbxLabel", "");
			OrbitPanel.Object label = (OrbitPanel.Object)Enum.Parse(typeof(OrbitPanel.Object), name);

			if (cbx.Checked && !orbitPanel.LabelDisplay.Contains(label))
				orbitPanel.LabelDisplay.Add(label);
			else
				orbitPanel.LabelDisplay.Remove(label);

			if (label == OrbitPanel.Object.Comet)
				cbxSelectedLabel.Enabled = rbtnMultipleMode.Checked && !cbx.Checked;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void rbtnCenterCommon_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtn = sender as RadioButton;
			string name = rbtn.Name.Replace("rbtnCenter", "");
			OrbitPanel.Object centerObject = (OrbitPanel.Object)Enum.Parse(typeof(OrbitPanel.Object), name);
			orbitPanel.CenteredObject = centerObject;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void cbxOrbit_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.PreserveSelectedOrbit = cbxSelectedOrbit.Checked;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void cbxLabel_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.PreserveSelectedLabel = cbxSelectedLabel.Checked;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void cbxMarker_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowMarker = cbxMarker.Checked;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void ChangeObjectDisplay(OrbitPanel.Object obj, bool control, bool shift)
		{
			if (control && !shift)
				ChangeVisibleOrbit(obj);
			else if (shift && !control)
				ChangeVisibleLabel(obj);
			else if (!control && !shift)
				ChangeCenterObject(obj);
		}

		private void ChangeVisibleOrbit(OrbitPanel.Object orbit)
		{
			CheckBox cbx = GetControlBoxFromObjectEnum(orbit, typeof(CheckBox), false) as CheckBox;

			if (cbx != null && cbx.Enabled)
				cbx.Checked = !orbitPanel.OrbitDisplay.Contains(orbit);
		}

		private void ChangeVisibleLabel(OrbitPanel.Object label)
		{
			CheckBox cbx = GetControlBoxFromObjectEnum(label, typeof(CheckBox)) as CheckBox;

			if (cbx != null && cbx.Enabled)
				cbx.Checked = !orbitPanel.LabelDisplay.Contains(label);
		}

		private void ChangeCenterObject(OrbitPanel.Object centeredObject)
		{
			RadioButton rbtn = GetControlBoxFromObjectEnum(centeredObject, typeof(RadioButton)) as RadioButton;

			if (rbtn != null && rbtn.Enabled)
				rbtn.Checked = true;
		}

		private Control GetControlBoxFromObjectEnum(OrbitPanel.Object obj, Type type, bool isLabel = true)
		{
			List<Control> controls = new List<Control>();
			Control control = null;

			foreach (Control c in gbxOrbitsLabelsCenter.Controls)
				if (c.Name.EndsWith(obj.ToString()))
					controls.Add(c);

			string name;

			if (type == typeof(CheckBox) && isLabel)
				name = "Label";
			else if (type == typeof(CheckBox) && !isLabel)
				name = "Orbit";
			else //if(type == typeof(RadioButton))
				name = "Center";

			control = controls.First(x => x.GetType() == type && x.Name.Contains(name));

			return control;
		}

		#endregion

		#region Date and Time

		private void btnDate_Click(object sender, EventArgs e)
		{
			ShowDateTimeForm();
		}

		private void ShowDateTimeForm()
		{
			using (FormDateTime fdt = new FormDateTime(DefaultDateTime, SelectedDateTime, GetT()))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					SelectedDateTime = fdt.SelectedDateTime;
			}
		}

		private double? GetT()
		{
			double? T = null;

			if (OVComets.Any())
				T = OVComets.ElementAt(cboComet.SelectedIndex).T;

			return T;
		}

		private void btnNow_Click(object sender, EventArgs e)
		{
			SelectedDateTime = DateTime.Now;
		}

		private void btnPerihDate_Click(object sender, EventArgs e)
		{
			if (OVComets.Any())
				SelectedDateTime = Utils.JDToDateTime(OVComets.ElementAt(cboComet.SelectedIndex).T).ToLocalTime();
		}

		#endregion

		#region Simulation

		private void btnRevPlay_Click(object sender, EventArgs e)
		{
			PlaySimulation(ATime.TimeDecrement);
		}

		private void btnRevStep_Click(object sender, EventArgs e)
		{
			PauseSimulation();
			ChangeDate(ATime.TimeDecrement);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			PauseSimulation();
		}

		private void btnForStep_Click(object sender, EventArgs e)
		{
			PauseSimulation();
			ChangeDate(ATime.TimeIncrement);
		}

		private void btnForPlay_Click(object sender, EventArgs e)
		{
			PlaySimulation(ATime.TimeIncrement);
		}

		private void cboTimestep_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimeStep = TimeStepSpan[cboTimestep.SelectedIndex];
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			ChangeDate(SimulationDirection);
		}

		private void ChangeDate(int direction)
		{
			ClearOrbits();

			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, direction);

			if (atime < ATime.Minimum || atime > ATime.Maximum)
				PauseSimulation();

			if (atime < ATime.Minimum)
				atime = new ATime(ATime.Minimum);

			if (atime > ATime.Maximum)
				atime = new ATime(ATime.Maximum);

			ValueChangedByEvent = true;
			SelectedDateTime = new DateTime(orbitPanel.ATime.Year, orbitPanel.ATime.Month, orbitPanel.ATime.Day, orbitPanel.ATime.Hour, orbitPanel.ATime.Minute, orbitPanel.ATime.Second, DateTimeKind.Utc);
			ValueChangedByEvent = false;
		}

		private void PlaySimulation(int direction)
		{
			SimulationDirection = direction;
			ClearOrbits();
			Timer.Start();
			IsSimulationStarted = true;
		}

		private void PauseSimulation()
		{
			Timer.Stop();
			IsSimulationStarted = false;
		}

		private void FasterSimulation()
		{
			if (!IsSimulationStarted)
			{
				PlaySimulation(SimulationDirection);
			}
			else
			{
				if (cboTimestep.SelectedIndex < cboTimestep.Items.Count - 1)
				{
					cboTimestep.SelectedIndex++;
				}
			}
		}

		private void SlowerSimulation()
		{
			if (!IsSimulationStarted)
			{
				PlaySimulation(SimulationDirection);
			}
			else
			{
				if (cboTimestep.SelectedIndex > 0)
					cboTimestep.SelectedIndex--;
				else
					PauseSimulation();
			}
		}

		private void InvertSimulation()
		{
			SimulationDirection = SimulationDirection * -1;
		}

		#endregion

		#region Other

		private void cbxShowAxes_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowAxes = cbxShowAxes.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxAntialiasing_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.Antiliasing = cbxAntialiasing.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxMagDist_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDistance = cbxMagDist.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDate = cbxDateTime.Checked;
			orbitPanel.Invalidate();
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			SaveImage();
		}

		#endregion

		#endregion

		#region Keyboad shortcuts

		private void FormOrbitViewer_KeyDown(object sender, KeyEventArgs e)
		{
			if (cboComet.Focused || cboTimestep.Focused) return;

			bool handled = true;
			bool ctrl = Control.ModifierKeys == Keys.Control;
			bool shift = Control.ModifierKeys == Keys.Shift;

			switch (e.KeyCode)
			{
				case Keys.Left:
					handled = MoveScroll(scrollHorz, false);
					break;
				case Keys.Right:
					handled = MoveScroll(scrollHorz, true);
					break;

				case Keys.Up:
					handled = MoveScroll(scrollVert, false);
					break;
				case Keys.Down:
					handled = MoveScroll(scrollVert, true);
					break;

				case Keys.Add:
					handled = MoveScroll(scrollZoom, true, false);
					break;
				case Keys.Subtract:
					handled = MoveScroll(scrollZoom, false, false);
					break;

				case Keys.D1:
					ChangeObjectDisplay(OrbitPanel.Object.Mercury, ctrl, shift);
					break;

				case Keys.D2:
					ChangeObjectDisplay(OrbitPanel.Object.Venus, ctrl, shift);
					break;

				case Keys.D3:
					ChangeObjectDisplay(OrbitPanel.Object.Earth, ctrl, shift);
					break;

				case Keys.D4:
					ChangeObjectDisplay(OrbitPanel.Object.Mars, ctrl, shift);
					break;

				case Keys.D5:
					ChangeObjectDisplay(OrbitPanel.Object.Jupiter, ctrl, shift);
					break;

				case Keys.D6:
					ChangeObjectDisplay(OrbitPanel.Object.Saturn, ctrl, shift);
					break;

				case Keys.D7:
					ChangeObjectDisplay(OrbitPanel.Object.Uranus, ctrl, shift);
					break;

				case Keys.D8:
					ChangeObjectDisplay(OrbitPanel.Object.Neptune, ctrl, shift);
					break;

				case Keys.D9:
				case Keys.C:
					ChangeObjectDisplay(OrbitPanel.Object.Comet, ctrl, shift);
					break;

				case Keys.D0:
				case Keys.S:
					ChangeCenterObject(OrbitPanel.Object.Sun);
					break;

				case Keys.Space:
				case Keys.P:
					if (IsSimulationStarted)
						PauseSimulation();
					else
						PlaySimulation(SimulationDirection);
					break;

				case Keys.J:
					InvertSimulation();
					break;

				case Keys.K:
					SlowerSimulation();
					break;

				case Keys.L:
					FasterSimulation();
					break;

				case Keys.D:
					if (ctrl)
						ShowDateTimeForm();
					break;

				case Keys.B:
					if (ctrl && OVComets.Any())
						SelectedDateTime = Utils.JDToDateTime(OVComets.ElementAt(cboComet.SelectedIndex).T).ToLocalTime();
					break;

				case Keys.N:
					if (ctrl)
						SelectedDateTime = DateTime.Now;
					break;

				default:
					handled = !(cboComet.Focused || cboTimestep.Focused);
					break;
			}

			e.Handled = handled;
		}

		private bool MoveScroll(ScrollBar scrollbar, bool isIncrement, bool continuous = true)
		{
			bool handled = false;

			if (IsKeyboardScroll)
			{
				int value = scrollbar.LargeChange * (isIncrement ? 1 : -1);
				int newValue = scrollbar.Value + value;

				if (continuous)
				{
					while (newValue > scrollbar.Maximum)
						newValue = scrollbar.Minimum;

					while (newValue < scrollbar.Minimum)
						newValue = scrollbar.Maximum;
				}
				else
				{
					if (newValue > scrollbar.Maximum)
						newValue = scrollbar.Maximum;

					if (newValue < scrollbar.Minimum)
						newValue = scrollbar.Minimum;
				}

				scrollbar.Value = newValue;

				handled = true;
			}

			return handled;
		}

		#endregion

		#region Mouse Controls

		private void orbitPanel_MouseEnter(object sender, EventArgs e)
		{
			orbitPanel.Focus();
			IsMouseWheelZoom = true;
			IsKeyboardScroll = true;
		}

		private void orbitPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				IsMouseRotate = true;
				ClearOrbits();
				StartDrag = e.Location;
			}
		}

		private void orbitPanel_MouseUp(object sender, MouseEventArgs e)
		{
			IsMouseRotate = false;
		}

		private void orbitPanel_MouseLeave(object sender, EventArgs e)
		{
			IsMouseRotate = false;
			IsMouseWheelZoom = false;
			IsKeyboardScroll = false;
		}

		private void orbitPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				string name = orbitPanel.SelectComet(e.Location);

				if (name != null)
					cboComet.SelectedIndex = OVComets.IndexOf(OVComets.First(x => x.Name == name));
			}
		}

		private void orbitPanel_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				string name = orbitPanel.SelectComet(e.Location);

				if (name != null)
				{
					cboComet.SelectedIndex = OVComets.IndexOf(OVComets.First(x => x.Name == name));

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
						cbxSelectedOrbit.Checked = !cbxSelectedOrbit.Checked;
						cbxSelectedLabel.Checked = !cbxSelectedLabel.Checked;
					}
				}
			}
		}

		private void orbitPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (IsMouseRotate)
			{
				double xRatio = orbitPanel.MinimumSize.Width / scrollHorz.Maximum;
				double yRatio = orbitPanel.MinimumSize.Height / scrollVert.Maximum;

				int deltaX = e.X - StartDrag.X;
				int deltaY = e.Y - StartDrag.Y;

				double koef = 0.3;

				double newHv = xRatio * deltaX * koef;
				double newVv = yRatio * deltaY * koef;

				if (newHv > 0 && newHv < 1) newHv = 1;
				else if (newHv < 0 && newHv > -1) newHv = -1;
				else if (newVv > 0 && newVv < 1) newVv = 1;
				else if (newVv < 0 && newVv > -1) newVv = -1;

				int newHorizValue = scrollHorz.Value + (int)newHv;
				int newVertValue = scrollVert.Value + (int)newVv;

				while (newHorizValue > scrollHorz.Maximum)
					newHorizValue -= scrollHorz.Maximum;

				while (newHorizValue < scrollHorz.Minimum)
					newHorizValue += scrollHorz.Maximum;

				while (newVertValue > scrollVert.Maximum)
					newVertValue -= scrollVert.Maximum;

				while (newVertValue < scrollVert.Minimum)
					newVertValue += scrollVert.Maximum;

				scrollHorz.Value = newHorizValue;
				scrollVert.Value = newVertValue;

				StartDrag = e.Location;
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (IsMouseWheelZoom)
			{
				double koef = e.Delta < 0 ? 0.23 : 0.30;
				int value = (int)(scrollZoom.Value * koef);
				int newZvalue = scrollZoom.Value + (e.Delta < 0 ? -value : value);

				if (newZvalue < scrollZoom.Minimum)
					scrollZoom.Value = scrollZoom.Minimum;
				else if (newZvalue > scrollZoom.Maximum)
					scrollZoom.Value = scrollZoom.Maximum;
				else
					scrollZoom.Value = newZvalue;
			}
		}

		#endregion

		#region Scrollbars

		private void scrollVert_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateVert = (double)(270 - scrollVert.Value);

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void scrollHorz_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateHorz = (double)(270 - scrollHorz.Value);

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		private void scrollZoom_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.Zoom = (double)scrollZoom.Value;

			if (!ValueChangedByEvent)
				orbitPanel.Invalidate();
		}

		#endregion

		#region Methods

		private void BindList()
		{
			cboComet.DisplayMember = "Name";
			cboComet.DataSource = OVComets;

			if (OVComets.Any() && FormMain.UserList.Count == FormMain.MainList.Count)
			{
				//comet with nearest perihelion date
				OVComet c = OVComets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First();
				cboComet.SelectedIndex = OVComets.IndexOf(c);
			}
		}

		public void ShowToolbox(bool visible)
		{
			ToolboxVisible = visible;
			pnlToolbox.Visible = visible;
		}

		private List<OVComet> TransformComets(List<Comet> comets)
		{
			List<OVComet> list = new List<OVComet>();

			foreach (Comet c in comets)
				list.Add(new OVComet(c));

			return list;
		}

		private void ClearOrbits()
		{
			//if more comets than max number, then turn off orbit display for comets
			if (orbitPanel.Comets.Count > OrbitPanel.MaximumOrbits)
			{
				ValueChangedByEvent = true;

				cbxOrbitComet.Checked = false;
				cbxLabelComet.Checked = false;

				ValueChangedByEvent = false;
			}
		}

		public void SaveImage()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				if (!String.IsNullOrEmpty(FormMain.Settings.LastUsedExportDirectory))
					sfd.InitialDirectory = FormMain.Settings.LastUsedExportDirectory;
				else
					sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

				sfd.Filter = "BMP (*.bmp)|*.bmp|" +
							"GIF (*.gif)|*.gif|" +
							"JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" +
							"PNG (*.png)|*.png";
				sfd.FilterIndex = 4;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					ImageFormat format;

					switch (sfd.FilterIndex)
					{
						case 1:
							format = ImageFormat.Bmp;
							break;
						case 2:
							format = ImageFormat.Gif;
							break;
						case 3:
							format = ImageFormat.Jpeg;
							break;
						default:
							format = ImageFormat.Png;
							break;
					}

					Bitmap bmp = new Bitmap(this.orbitPanel.Width, this.orbitPanel.Height);
					this.orbitPanel.DrawToBitmap(bmp, this.orbitPanel.DisplayRectangle);
					bmp.Save(sfd.FileName, format);
					FormMain.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Orbit saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
