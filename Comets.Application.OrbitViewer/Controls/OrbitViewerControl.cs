using Comets.Application.Common.Controls.Common;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using Comets.OrbitViewer;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Object = Comets.OrbitViewer.Object;
using SimulationEvent = Comets.Application.OrbitViewer.Controls.SimulationControl.SimulationEvent;
using FilterValue = Comets.Application.OrbitViewer.Controls.FilterControl.FilterValue;

namespace Comets.Application.OrbitViewer
{
	public partial class OrbitViewerControl : ValueChangeControl
	{
		#region Consts

		const int DefaultScrollVert = 20;
		const int DefaultScrollHorz = 75;
		const int DefaultScrollZoom = 145;

		#endregion

		#region Fields

		private bool IsLeftButtonMoving;
		private bool IsKeyboardScroll;
		private bool IsMouseWheelZoom;
		private Point StartDrag;

		private System.Windows.Forms.Timer Timer;
		private ATimeSpan TimeStep;

		private CometCollection Comets;
		private FilterCollection Filters;
		private string SortProperty;
		private bool SortAscending;

		#endregion

		#region Properties

		private OVComet SelectedComet
		{
			get { return cometControl.SelectedComet; }
			set { cometControl.SelectedComet = value; }
		}

		private DateTime SelectedDateTime
		{
			get { return dateTimeControl.SelectedDateTime; }
			set
			{
				DateTime selectedDateTime;
				bool isOutOfRange = FormDateTime.RangeDateTime(value, out selectedDateTime);
				dateTimeControl.SelectedDateTime = selectedDateTime;

				if (isOutOfRange || (IsSimulationStarted && !ValueChangedInternal))
					StopSimulation();

				if (orbitPanel.IsPaintEnabled)
				{
					orbitPanel.ATime = new ATime(selectedDateTime, selectedDateTime.Timezone());
					RefreshPanel();
				}
			}
		}

		public bool ToolboxVisible { get; private set; } = true;

		private bool IsSimulationForward { get; set; } = true;

		private bool IsSimulationStarted
		{
			get { return Timer != null && Timer.Enabled; }
		}

		#endregion

		#region Constructor

		public OrbitViewerControl()
		{
			InitializeComponent();

			Timer = new System.Windows.Forms.Timer();
			Timer.Interval = 50;
			Timer.Tick += new EventHandler(this.timer_Tick);

			cometControl.OnSelectedCometChanged += LoadSelectedComet;
			cometControl.OnFilter += FilterComets;
			cometControl.OnMark += MarkComet;
			cometControl.OnPreserveSelectedOrbitChanged += SetPreserveSelectedOrbit;
			cometControl.OnPreserveSelectedLabelChanged += SetPreserveSelectedLabel;
			cometControl.OnShowMarkerChanged += SetShowMarker;
			cometControl.OnDisplayChanged += RefreshPanel;

			modeControl.OnModeChanged += SetMode;

			displayControl.OnDisplayChanged += RefreshPanel;
			displayControl.OnOrbitDisplayChanged += ChangeOrbitDisplay;
			displayControl.OnLabelDisplayChanged += ChangeLabelDisplay;
			displayControl.OnCenterObjectChanged += SetCenterObject;

			dateTimeControl.OnSelectedDatetimeChanged += SetDateTime;

			simulationControl.OnSimulationEvent += ExecuteSimulationEvent;
			simulationControl.OnTimespanChanged += SetTimeStep;

			filterControl.OnFilterValueChanged += SetFilterOnDateValue;
			filterControl.OnShowInWeakColorChanged += SetFilterOnDateShowInWeakColor;

			infoLabelsControl.OnShowDistanceLabelChanged += SetShowDistanceLabel;
			infoLabelsControl.OnShowDateLabelChanged += SetShowDateLabel;

			miscControl.OnShowAxesChanged += SetShowAxes;
			miscControl.OnAntialiasingChanged += SetAntialiasing;
			miscControl.OnSaveImage += Save;
		}

		#endregion

		#region LoadControl

		public void LoadControl(CometCollection comets, FilterCollection filters, string sortProperty, bool sortAscending)
		{
			cpnlDisplay.IsCollapsed = true;

			Comets = comets;
			Filters = filters;
			SortProperty = sortProperty;
			SortAscending = sortAscending;

			SelectedDateTime = DateTime.UtcNow.Date;

			ValueChangedInternal = true;

			scrollVert.Value = DefaultScrollVert;
			scrollHorz.Value = DefaultScrollHorz;
			scrollZoom.Value = DefaultScrollZoom;

			ValueChangedInternal = false;

			cometControl.BindCollection(comets);

			//modeControl.SetMode(false);
			//LoadSelectedComet();
			LoadAllComets();
		}

		#endregion

		#region +EventHandling

		#region OrbitPanel

		private void orbitPanel_Resize(object sender, EventArgs e)
		{
			orbitPanel.Image = new Bitmap(orbitPanel.Width, orbitPanel.Height);
			RefreshPanel();
		}

		#endregion

		#region + ToolBox

		#region Comet

		private void LoadSelectedComet()
		{
			orbitPanel.LoadPanel(SelectedComet, orbitPanel.ATime ?? new ATime(SelectedDateTime, SelectedDateTime.Timezone()));
			SetPerihelionDate();
			SetFormText();
			RefreshPanel();
		}

		private void FindComet()
		{
			using (FormFind ff = new FormFind(Comets))
			{
				Point panelLocation = this.orbitPanel.PointToScreen(Point.Empty);
				Size margin = new Size(7, 7);

				ff.TopMost = this.ParentForm.TopMost;
				ff.OnSelectedCometChanged += cometControl.SelectCometByName;
				ff.Location = panelLocation + margin;
				ff.ShowDialog();
			}
		}

		private void SetPreserveSelectedOrbit(bool preserveSelectedOrbit)
		{
			orbitPanel.PreserveSelectedOrbit = preserveSelectedOrbit;
		}

		private void SetPreserveSelectedLabel(bool preserveSelectedLabel)
		{
			orbitPanel.PreserveSelectedLabel = preserveSelectedLabel;
		}

		private void SetShowMarker(bool showMarker)
		{
			orbitPanel.ShowMarker = showMarker;
		}

		private void MarkComet()
		{
			if (orbitPanel.MultipleMode)
			{
				if (SelectedComet != null)
					SelectedComet.IsMarked = !SelectedComet.IsMarked;

				if (orbitPanel.IsPaintEnabled && !IsSimulationStarted)
					RefreshPanel();
			}
		}

		private void FilterComets()
		{
			bool simStarted = IsSimulationStarted;

			string lastSelected = SelectedComet?.Name;

			using (FormDatabase fdb = new FormDatabase(CommonManager.MainCollection, false, Filters, SortProperty, SortAscending, true) { Owner = this.ParentForm })
			{
				fdb.TopMost = this.ParentForm.TopMost;

				StopSimulation();

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					orbitPanel.ClearComets(true);

					Comets = fdb.CometsFiltered;
					Filters = fdb.Filters;
					SortProperty = fdb.SortProperty;
					SortAscending = fdb.SortAscending;

					cometControl.BindCollection(Comets, lastSelected);

					if (modeControl.MultipleMode)
						orbitPanel.LoadPanel(cometControl.Comets.ToList(), orbitPanel.ATime, cometControl.SelectedIndex);

					SetPerihelionDate();
					SetFormText();
					RefreshPanel();
				}
			}

			if (simStarted)
				StartSimulation();
		}

		private void LoadAllComets()
		{
			if (cometControl.Comets.Count > 0 && orbitPanel.Comets.Count != cometControl.Comets.Count)
			{
				ATime atime = orbitPanel.ATime ?? new ATime(SelectedDateTime, SelectedDateTime.Timezone());
				orbitPanel.LoadPanel(cometControl.Comets.ToList(), atime, cometControl.SelectedIndex);
				modeControl.SetMode(true);

				SetFormText();
				RefreshPanel();
			}
		}

		#endregion

		#region Mode

		private void SetMode(bool isMultiple)
		{
			orbitPanel.MultipleMode = isMultiple;

			if (isMultiple)
				LoadAllComets();
			//else
			//	orbitPanel.ClearComets();

			SetFormText();
			RefreshPanel();
		}

		#endregion

		#region Display

		private void ChangeOrbitDisplay(bool isChecked, Object orbit)
		{
			if (isChecked && !orbitPanel.OrbitDisplay.Contains(orbit))
				orbitPanel.OrbitDisplay.Add(orbit);
			else
				orbitPanel.OrbitDisplay.Remove(orbit);
		}

		private void ChangeLabelDisplay(bool isChecked, Object orbit)
		{
			if (isChecked && !orbitPanel.LabelDisplay.Contains(orbit))
				orbitPanel.LabelDisplay.Add(orbit);
			else
				orbitPanel.LabelDisplay.Remove(orbit);
		}

		private void SetCenterObject(Object centerObject)
		{
			orbitPanel.CenteredObject = centerObject;
		}

		private void ChangeObjectDisplay(Object obj, bool control, bool shift)
		{
			if (!control && !shift)
				displayControl.ChangeCenterObject(obj);
			else if (control && !shift)
				displayControl.ChangeVisibleOrbit(obj, !orbitPanel.OrbitDisplay.Contains(obj));
			else if (shift && !control)
				displayControl.ChangeVisibleLabel(obj, !orbitPanel.LabelDisplay.Contains(obj));
		}

		#endregion

		#region Date and Time

		private void SetDateTime(object sender, DateTime dateTime)
		{
			SelectedDateTime = dateTime;
		}

		private void SetPerihelionDate()
		{
			dateTimeControl.PerihelionDate = EphemerisManager.JDToDateTimeSafe(SelectedComet?.T);
		}

		#endregion

		#region Simulation

		private void ExecuteSimulationEvent(SimulationEvent simulationEvent)
		{
			switch (simulationEvent)
			{
				case SimulationEvent.PlayBack:
					IsSimulationForward = false;
					StartSimulation();
					break;
				case SimulationEvent.StepBack:
					StopSimulation();
					ChangeSimulationDate(false);
					break;
				case SimulationEvent.Stop:
					StopSimulation();
					break;
				case SimulationEvent.StepForward:
					StopSimulation();
					ChangeSimulationDate(true);
					break;
				case SimulationEvent.PlayForward:
					IsSimulationForward = true;
					StartSimulation();
					break;
			}
		}

		private void SetTimeStep(ATimeSpan timeStep)
		{
			TimeStep = timeStep;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			ChangeSimulationDate(IsSimulationForward);
		}

		private void ChangeSimulationDate(bool isForward)
		{
			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, isForward);

			ValueChangedInternal = true;
			SelectedDateTime = new DateTime(atime.Year, atime.Month, atime.Day, atime.Hour, atime.Minute, atime.Second, DateTimeKind.Utc);
			ValueChangedInternal = false;
		}

		private void StartSimulation()
		{
			Timer.Start();
		}

		public void StopSimulation()
		{
			Timer.Stop();
		}

		private void FasterSimulation()
		{
			if (!IsSimulationStarted)
				StartSimulation();
			else
				simulationControl.FasterSimulation();
		}

		private void SlowerSimulation()
		{
			if (!IsSimulationStarted)
				StartSimulation();
			else if (!simulationControl.SlowerSimulation())
				StopSimulation();
		}

		#endregion

		#region Filter on date

		private void SetFilterOnDateValue(FilterValue filter, double? value)
		{
			switch (filter)
			{
				case FilterValue.SunDistance:
					orbitPanel.FilterOnDateSunDist = value;
					break;
				case FilterValue.EarthDistance:
					orbitPanel.FilterOnDateEarthDist = value;
					break;
				case FilterValue.Magnitude:
					orbitPanel.FilterOnDateMagnitude = value;
					break;
			}

			orbitPanel.UpdateCometVisibility();
			RefreshPanel();
		}

		private void SetFilterOnDateShowInWeakColor(bool showInWeakColor)
		{
			orbitPanel.FilterOnDateShowInWeakColor = showInWeakColor;
			RefreshPanel();
		}

		#endregion

		#region Info labels

		private void SetShowDistanceLabel(bool showDistanceLabel)
		{
			orbitPanel.ShowDistance = showDistanceLabel;
			RefreshPanel();
		}

		private void SetShowDateLabel(bool showDateLabel)
		{
			orbitPanel.ShowDate = showDateLabel;
			RefreshPanel();
		}

		#endregion

		#region Misc

		private void SetShowAxes(bool showAxes)
		{
			orbitPanel.ShowAxes = showAxes;
			RefreshPanel();
		}

		private void SetAntialiasing(bool antialiasing)
		{
			orbitPanel.Antialiasing = antialiasing;
			RefreshPanel();
		}

		#endregion

		#endregion

		#region Keyboad shortcuts

		private void orbitPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					e.IsInputKey = true;
					break;
			}
		}

		public void OrbitViewerControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (filterControl.Focused
				&& e.KeyCode.In(Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.Back))
				return;

			bool handled = false;
			bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			bool shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

			switch (e.KeyCode)
			{
				case Keys.Menu:
					handled = false;
					break;
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
					if (!ctrl && !shift)
						handled = MoveScroll(scrollZoom, false, false);
					break;

				case Keys.A:
					if (!ctrl && !shift)
						handled = MoveScroll(scrollZoom, false, false);
					break;

				case Keys.D1:
					ChangeObjectDisplay(Object.Mercury, ctrl, shift);
					handled = true;
					break;

				case Keys.D2:
					ChangeObjectDisplay(Object.Venus, ctrl, shift);
					handled = true;
					break;

				case Keys.D3:
					ChangeObjectDisplay(Object.Earth, ctrl, shift);
					handled = true;
					break;

				case Keys.D4:
					ChangeObjectDisplay(Object.Mars, ctrl, shift);
					handled = true;
					break;

				case Keys.D5:
					ChangeObjectDisplay(Object.Jupiter, ctrl, shift);
					handled = true;
					break;

				case Keys.D6:
					ChangeObjectDisplay(Object.Saturn, ctrl, shift);
					handled = true;
					break;

				case Keys.D7:
					ChangeObjectDisplay(Object.Uranus, ctrl, shift);
					handled = true;
					break;

				case Keys.D8:
					ChangeObjectDisplay(Object.Neptune, ctrl, shift);
					handled = true;
					break;

				case Keys.D9:
					if (!ctrl && !shift)
					{
						ChangeObjectDisplay(Object.Comet, ctrl, shift);
						handled = true;
					}
					break;

				case Keys.C:
					if (!ctrl && !shift)
					{
						ChangeObjectDisplay(Object.Comet, ctrl, shift);
						handled = true;
					}
					break;

				case Keys.D0:
				case Keys.S:
					if (!ctrl && !shift)
					{
						displayControl.ChangeCenterObject(Object.Sun);
						handled = true;
					}
					break;

				case Keys.Space:
				case Keys.P:
					if (!ctrl && !shift)
					{
						if (IsSimulationStarted)
							StopSimulation();
						else
							StartSimulation();

						handled = true;
					}
					break;

				case Keys.J:
					if (!ctrl && !shift)
					{
						// invert simulation
						IsSimulationForward = !IsSimulationForward;
						handled = true;
					}
					break;

				case Keys.K:
					if (!ctrl && !shift)
					{
						SlowerSimulation();
						handled = true;
					}
					break;

				case Keys.L:
					if (!ctrl && !shift)
					{
						FasterSimulation();
						handled = true;
					}
					break;

				case Keys.D:
					if (ctrl && !shift)
					{
						dateTimeControl.ShowDateTimeForm();
						handled = true;
					}
					break;

				case Keys.B:
					if (ctrl && !shift && SelectedComet != null)
					{
						SelectedDateTime = EphemerisManager.JDToDateTime(SelectedComet.T);
						handled = true;
					}
					break;

				case Keys.N:
					if (ctrl && !shift)
					{
						SelectedDateTime = DateTime.UtcNow.Date;
						handled = true;
					}
					break;

				case Keys.G:
					if (!ctrl && !shift)
					{
						cometControl.InvertSelectedCometOrbit();
						handled = true;
					}
					break;

				case Keys.H:
					if (!ctrl && !shift)
					{
						cometControl.InvertSelectedCometLabel();
						handled = true;
					}
					break;

				case Keys.M:
					if (!ctrl && !shift)
					{
						cometControl.InvertMarker();
						handled = true;
					}
					break;

				case Keys.Enter:
					if (!ctrl && !shift)
					{
						MarkComet();
						handled = true;
					}
					break;

				case Keys.Escape:
					if (!ctrl && !shift)
					{
						SelectedComet = null;
						handled = true;
					}
					break;

				case Keys.Delete:
					if (!ctrl && !shift && orbitPanel.MultipleMode)
					{
						cometControl.UnmarkComets();

						if (orbitPanel.IsPaintEnabled && !IsSimulationStarted)
							RefreshPanel();

						handled = true;
					}
					break;

				case Keys.F:
					if (ctrl && !shift)
					{
						FindComet();
						handled = true;
					}
					else if (ctrl && shift)
					{
						FilterComets();
						handled = true;
					}
					break;

				case Keys.I:
					if (ctrl && !shift && SelectedComet != null)
					{
						//dodati id i na ovcomet?
						Comet c = Comets.ElementAt(cometControl.SelectedIndex);
						CometManager.OpenJplInfo(c.id);
						handled = true;
					}
					break;
				default:
					handled = !(cometControl.Focused || simulationControl.Focused);
					break;
			}

			e.Handled = handled;
		}

		private bool MoveScroll(ScrollBar scrollbar, bool isIncrement, bool continuous = true)
		{
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
			}

			return IsKeyboardScroll;
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
				StartDrag = e.Location;
		}

		private void orbitPanel_MouseLeave(object sender, EventArgs e)
		{
			IsMouseWheelZoom = false;
			IsKeyboardScroll = false;
		}

		private void orbitPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (!IsLeftButtonMoving && e.Button == MouseButtons.Left && orbitPanel.MultipleMode)
			{
				string name = orbitPanel.SelectComet(e.Location);
				cometControl.SelectCometByName(name);
			}
		}

		private void orbitPanel_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left &&
				orbitPanel.SelectComet(e.Location) != null)
			{
				displayControl.ChangeCenterObject(Object.Comet);
				cometControl.SetDoubleClickDisplay(orbitPanel.CenterSelectedComet);
			}
		}

		private void orbitPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!orbitPanel.Focused)
				orbitPanel.Focus();

			IsLeftButtonMoving = e.Button == MouseButtons.Left;

			if (e.Button == MouseButtons.Right)
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
			orbitPanel.RotateVert = (double)(90 - scrollVert.Value);

			if (!ValueChangedInternal)
				RefreshPanel();
		}

		private void scrollHorz_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateHorz = (double)(90 - scrollHorz.Value);

			if (!ValueChangedInternal)
				RefreshPanel();
		}

		private void scrollZoom_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.Zoom = (double)scrollZoom.Value;

			if (!ValueChangedInternal)
				RefreshPanel();
		}

		#endregion

		#endregion

		#region Methods

		private void RefreshPanel()
		{
			orbitPanel.Invalidate();
		}

		public void ShowToolbox(bool visible)
		{
			ToolboxVisible = visible;
			pnlToolbox.Visible = visible;
		}

		public void Save()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				sfd.FileName = "Comets_OrbitViewer_" + DateTime.Now.ToString(DateTimeFormat.Filename);
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

			this.ParentForm.Text = text;
		}

		#endregion
	}
}
