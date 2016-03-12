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

		readonly string[] TimeStepItems =
		{
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

		readonly ATimeSpan[] TimeStepSpan =
		{
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

		private CometCollection Comets;
		private FilterCollection Filters;
		private string SortProperty;
		private bool SortAscending;

		private List<OVComet> OVComets;

		private DateTime DefaultDateTime;

		#endregion

		#region Properties

		private OVComet SelectedComet
		{
			get
			{
				OVComet comet = null;

				if (cboComet.SelectedIndex >= 0)
					comet = OVComets.ElementAt(cboComet.SelectedIndex);

				return comet;
			}
		}

		private DateTime _selectedDateTime;
		private DateTime SelectedDateTime
		{
			get
			{
				return _selectedDateTime;
			}
			set
			{
				_selectedDateTime = FormDateTime.RangeDateTime(value);
				btnDate.Text = _selectedDateTime.ToString(FormMain.DateTimeFormat);

				if (IsSimulationStarted && !ValueChangedByEvent)
					Timer.Stop();

				if (orbitPanel.IsPaintEnabled)
				{
					orbitPanel.ATime = new ATime(_selectedDateTime, _selectedDateTime.Timezone());
					RefreshPanel();
				}
			}
		}

		private bool _toolboxVisible;
		public bool ToolboxVisible
		{
			get
			{
				return _toolboxVisible;
			}
			set
			{
				_toolboxVisible = value;

				if (!ValueChangedByEvent)
					(this.MdiParent as FormMain).SetToolBoxMenuItemChecked(value);
			}
		}

		#endregion

		#region Constructor

		public FormOrbitViewer(CometCollection comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			InitializeComponent();

			txtFodSunDist.Tag = new ValNum(0.0, 99.99, 2);
			txtFodEarthDist.Tag = new ValNum(0.0, 99.99, 2);
			txtFodMagnitude.Tag = ValNum.VMagnitude;

			Timer = new Timer();
			Timer.Interval = 50;
			Timer.Tick += new EventHandler(this.timer_Tick);

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
			BindCollection();

			cboTimestep.DataSource = TimeStepItems;
			cboTimestep.SelectedIndex = 3;

			ATime atime = new ATime(SelectedDateTime, SelectedDateTime.Timezone());
			orbitPanel.LoadPanel(SelectedComet, atime);
			RefreshPanel();
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
			RefreshPanel();
		}

		#endregion

		#region + ToolBox

		#region Comet

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!ValueChangedByEvent)
			{
				orbitPanel.LoadPanel(SelectedComet, orbitPanel.ATime);
				RefreshPanel();
				SetFormText();
			}
		}

		private void btnFilter_Click(object sender, EventArgs e)
		{
			string lastSelected = SelectedComet != null ? SelectedComet.Name : null;

			using (FormDatabase fdb = new FormDatabase(Comets, Filters, SortProperty, SortAscending, true) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					orbitPanel.ClearComets(true);

					Comets = fdb.Comets;
					Filters = fdb.Filters;
					SortProperty = fdb.SortProperty;
					SortAscending = fdb.SortAscending;

					OVComets = TransformComets(Comets);

					BindCollection(lastSelected);

					if (rbtnMultipleMode.Checked)
						orbitPanel.LoadPanel(OVComets.ToList(), orbitPanel.ATime, cboComet.SelectedIndex);

					SetFormText();
					RefreshPanel();
				}
			}
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			if (OVComets.Count > 0 && orbitPanel.Comets.Count != OVComets.Count)
				orbitPanel.LoadPanel(OVComets.ToList(), orbitPanel.ATime, cboComet.SelectedIndex);

			SetFormText();

			ValueChangedByEvent = true;
			rbtnMultipleMode.Checked = true;
			ValueChangedByEvent = false;

			RefreshPanel();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			orbitPanel.ClearComets();
			SetFormText();
			RefreshPanel();
		}

		private void comboBoxCommon_MouseHover(object sender, EventArgs e)
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

			ValueChangedByEvent = tempChanged;

			SetFormText();

			if (!ValueChangedByEvent)
				RefreshPanel();
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
			RefreshPanel();
		}

		private void btnDefaultOrbitsLabels_Click(object sender, EventArgs e)
		{
			SetMultipleCheckBoxes("Orbit", true);
			SetMultipleCheckBoxes("Label", true);

			ValueChangedByEvent = true;
			cbxOrbitSaturn.Checked = false;
			cbxOrbitUranus.Checked = false;
			cbxOrbitNeptune.Checked = false;
			cbxOrbitComet.Checked = false;
			cbxLabelComet.Checked = false;
			rbtnCenterSun.Checked = true;
			cbxSelectedOrbit.Checked = true;
			cbxSelectedLabel.Checked = true;
			ValueChangedByEvent = false;

			RefreshPanel();
		}

		private void SetMultipleCheckBoxes(string name, bool isChecked, bool refresh = false)
		{
			ValueChangedByEvent = true;

			foreach (Control c in gbxOrbitsLabelsCenter.Controls)
			{
				if (c is CheckBox && c.Enabled && c.Name.Contains(name))
				{
					(c as CheckBox).Checked = isChecked;
				}
			}

			ValueChangedByEvent = false;

			if (refresh)
				RefreshPanel();
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

			if (!ValueChangedByEvent)
				RefreshPanel();
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

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		private void rbtnCenterCommon_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtn = sender as RadioButton;
			if (rbtn.Checked)
			{
				string name = rbtn.Name.Replace("rbtnCenter", "");
				OrbitPanel.Object centerObject = (OrbitPanel.Object)Enum.Parse(typeof(OrbitPanel.Object), name);
				orbitPanel.CenteredObject = centerObject;

				if (!ValueChangedByEvent)
					RefreshPanel();
			}
		}

		private void cbxOrbit_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.PreserveSelectedOrbit = cbxSelectedOrbit.Checked;

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		private void cbxLabel_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.PreserveSelectedLabel = cbxSelectedLabel.Checked;

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		private void cbxMarker_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowMarker = cbxMarker.Checked;

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		private void ChangeObjectDisplay(OrbitPanel.Object obj, bool control, bool shift)
		{
			if (!control && !shift)
				ChangeCenterObject(obj);
			else if (control && !shift)
				ChangeVisibleOrbit(obj);
			else if (shift && !control)
				ChangeVisibleLabel(obj);
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
			double? T = SelectedComet != null ? (double?)SelectedComet.T : null;

			using (FormDateTime fdt = new FormDateTime(DefaultDateTime, SelectedDateTime, T))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					SelectedDateTime = fdt.SelectedDateTime;
			}
		}

		private void btnNow_Click(object sender, EventArgs e)
		{
			SelectedDateTime = DateTime.Now;
		}

		private void btnPerihDate_Click(object sender, EventArgs e)
		{
			if (SelectedComet != null)
				SelectedDateTime = EphemerisManager.JDToDateTime(SelectedComet.T).ToLocalTime();
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
			RefreshPanel();
		}

		private void cbxAntialiasing_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.Antialiasing = cbxAntialiasing.Checked;
			RefreshPanel();
		}

		private void cbxMagDist_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDistance = cbxMagDist.Checked;
			RefreshPanel();
		}

		private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDate = cbxDateTime.Checked;
			RefreshPanel();
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			SaveImage();
		}

		#endregion

		#region Filter on date

		private void cbxFodSunDist_CheckedChanged(object sender, EventArgs e)
		{
			if (!ValueChangedByEvent)
			{
				CheckBox cbx = sender as CheckBox;

				string namePart = cbx.Name.Replace("cbx", String.Empty);
				TextBox txt = gbxFilterOnDate.Controls.Find("txt" + namePart, false).Single() as TextBox;
				double? value = cbx.Checked && !String.IsNullOrEmpty(txt.Text) ? txt.Double() : (double?)null;

				switch (cbx.Name)
				{
					case "cbxFodSunDist":
						orbitPanel.FilterOnDateSunDist = value;
						break;
					case "cbxFodEarthDist":
						orbitPanel.FilterOnDateEarthDist = value;
						break;
					case "cbxFodMagnitude":
						orbitPanel.FilterOnDateMagnitude = value;
						break;
					default:
						throw new NotImplementedException(cbx.Name);
				}

				RefreshPanel();
			}
		}

		private void txtFilterOnDateCommon_TextChanged(object sender, EventArgs e)
		{
			TextBox txt = sender as TextBox;

			string namePart = txt.Name.Replace("txt", String.Empty);
			CheckBox cbx = gbxFilterOnDate.Controls.Find("cbx" + namePart, false).Single() as CheckBox;
			double? value = !String.IsNullOrEmpty(txt.Text) ? txt.Double() : (double?)null;

			switch (txt.Name)
			{
				case "txtFodSunDist":
					orbitPanel.FilterOnDateSunDist = value;
					break;
				case "txtFodEarthDist":
					orbitPanel.FilterOnDateEarthDist = value;
					break;
				case "txtFodMagnitude":
					orbitPanel.FilterOnDateMagnitude = value;
					break;
				default:
					throw new NotImplementedException(txt.Name);
			}

			ValueChangedByEvent = true;
			cbx.Checked = value != null;
			ValueChangedByEvent = false;

			RefreshPanel();
		}

		private void txtFilterOnDateCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void cbxWeakColor_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.FilterOnDateShowInWeakColor = cbxWeakColor.Checked;
			RefreshPanel();
		}

		#endregion

		#endregion

		#region Keyboad shortcuts

		private void FormOrbitViewer_KeyDown(object sender, KeyEventArgs e)
		{
			bool handled = true;
			bool ctrl = Control.ModifierKeys == Keys.Control;
			bool shift = Control.ModifierKeys == Keys.Shift;

			switch (e.KeyCode)
			{
				case Keys.Left:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollHorz, false);
					break;
				case Keys.Right:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollHorz, true);
					break;

				case Keys.Up:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollVert, false);
					break;
				case Keys.Down:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollVert, true);
					break;

				case Keys.Add:
				case Keys.Q:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollZoom, true, false);
					break;
				case Keys.Subtract:
				case Keys.A:
					if (!ctrl && !shift)
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
					if (!ctrl && !shift)
						ChangeCenterObject(OrbitPanel.Object.Sun);
					break;

				case Keys.Space:
				case Keys.P:
					if (!ctrl && !shift)
					{
						if (IsSimulationStarted)
							PauseSimulation();
						else
							PlaySimulation(SimulationDirection);
					}
					break;

				case Keys.J:
					if (!ctrl && !shift)
						InvertSimulation();
					break;

				case Keys.K:
					if (!ctrl && !shift)
						SlowerSimulation();
					break;

				case Keys.L:
					if (!ctrl && !shift)
						FasterSimulation();
					break;

				case Keys.D:
					if (ctrl && !shift)
						ShowDateTimeForm();
					break;

				case Keys.B:
					if (ctrl && !shift && OVComets.Count > 0)
						SelectedDateTime = EphemerisManager.JDToDateTime(OVComets.ElementAt(cboComet.SelectedIndex).T).ToLocalTime();
					break;

				case Keys.N:
					if (ctrl && !shift)
						SelectedDateTime = DateTime.Now;
					break;

				case Keys.G:
					if (!ctrl && !shift)
						cbxSelectedOrbit.Checked = !cbxSelectedOrbit.Checked;
					break;

				case Keys.H:
					if (!ctrl && !shift)
						cbxSelectedLabel.Checked = !cbxSelectedLabel.Checked;
					break;

				case Keys.M:
					if (!ctrl && !shift)
						cbxMarker.Checked = !cbxMarker.Checked;
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
			IsMouseWheelZoom = true;
			IsKeyboardScroll = true;
		}

		private void orbitPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				IsMouseRotate = true;
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

				//if (name == null)
				//	cboComet.SelectedIndex = -1;
				//else 
				if (name != null && OVComets.Any(x => x.Name == name))
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
					bool isCometCentered = false;

					//center on comet on double click
					if (orbitPanel.CenteredObject == OrbitPanel.Object.Comet)
						isCometCentered = orbitPanel.CenterSelectedComet();

					if (!isCometCentered)
					{
						ValueChangedByEvent = true;

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

						ValueChangedByEvent = false;
					}

					RefreshPanel();
				}
			}
		}

		private void orbitPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!orbitPanel.Focused)
				orbitPanel.Focus();

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
				RefreshPanel();
		}

		private void scrollHorz_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateHorz = (double)(270 - scrollHorz.Value);

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		private void scrollZoom_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.Zoom = (double)scrollZoom.Value;

			if (!ValueChangedByEvent)
				RefreshPanel();
		}

		#endregion

		#region BindCollection

		private void BindCollection(string name = null)
		{
			ValueChangedByEvent = true;

			cboComet.DisplayMember = "Name";
			cboComet.DataSource = OVComets;

			if (OVComets.Count > 0)
			{
				if (name != null && OVComets.Any(x => x.Name == name))
				{
					cboComet.SelectedIndex = OVComets.IndexOf(OVComets.First(x => x.Name == name));
				}
				else
				{
					//comet with nearest perihelion date
					OVComet c = OVComets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First();
					cboComet.SelectedIndex = OVComets.IndexOf(c);
				}
			}

			ValueChangedByEvent = false;
		}

		private void RefreshPanel()
		{
			orbitPanel.Invalidate();
		}

		public void ShowToolbox(bool visible)
		{
			ToolboxVisible = visible;
			pnlToolbox.Visible = visible;
		}

		private List<OVComet> TransformComets(CometCollection comets)
		{
			List<OVComet> list = new List<OVComet>();

			foreach (Comet c in comets)
				list.Add(new OVComet(c));

			return list;
		}

		public void SaveImage()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
					CommonManager.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Orbit saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void SetFormText()
		{
			string text = "Orbit Viewer";

			if (SelectedComet != null)
				text += " - " + SelectedComet.Name;

			if (orbitPanel.MultipleMode && orbitPanel.Comets.Count > 1)
				text += " (" + orbitPanel.Comets.Count + " comets)";

			this.Text = text;
		}

		#endregion
	}
}
