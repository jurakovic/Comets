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
		#region + Consts

		const int InitialScrollVert = 220;
		const int InitialScrollHorz = 255;
		const int InitialScrollZoom = 145;

		//OrbitDisplayEnum
		bool[] OrbitDisplayDefault = { true, true, true, true, true, true, false, false, false, false };
		bool[] OrbitDisplay = { true, true, true, true, true, true, false, false, false, false };

		#region CenterObject

		readonly string[] CenterObjectItems = {
			"Sun",
			"Comet",
			"Mercury",
			"Venus",
			"Earth",
			"Mars",
			"Jupiter",
			"Saturn",
			"Uranus",
			"Neptune"
			//"Pluto"
		};

		#endregion

		#region OrbitsDisplay

		readonly string[] OrbitsDisplayItems = {
			"Default Orbits",
			"All Orbits",
			"No Orbits",
			"------",
			"Comet",
			"Mercury",
			"Venus",
			"Earth",
			"Mars",
			"Jupiter",
			"Saturn",
			"Uranus",
			"Neptune"
			//"Pluto"
		};

		#endregion

		#region TimeStep

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

		static ATimeSpan[] timeStepSpan = {
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

		#endregion

		#region Properties

		bool IsKeyboardScroll { get; set; }
		bool IsMouseRotate { get; set; }
		bool IsMouseWheelZoom { get; set; }
		Point StartDrag { get; set; }

		Timer Timer { get; set; }

		ATimeSpan TimeStep { get; set; }
		int SimulationDirection { get; set; }
		bool IsSimulationStarted { get; set; }
		bool IsTimeChangedByTimer { get; set; }

		List<OVComet> Comets { get; set; }
		OVComet SelectedComet { get; set; }

		OrbitViewerSettings _settings;
		public OrbitViewerSettings Settings
		{
			get { return _settings; }
			set
			{
				_settings = value;
				ApplySettings(this.Settings, true);
			}
		}

		private DateTime DefaultDateTime { get; set; }

		private DateTime _selectedDateTime;
		private DateTime SelectedDateTime
		{
			get { return _selectedDateTime; }
			set
			{
				_selectedDateTime = FormDateTime.RangeDateTime(value);
				btnDate.Text = _selectedDateTime.ToString(FormMain.DateTimeFormat);

				if (IsSimulationStarted && !IsTimeChangedByTimer)
					Timer.Stop();

				if (orbitPanel.IsPaintEnabled)
				{
					orbitPanel.ATime = new ATime(_selectedDateTime, FormMain.Settings.Location.Timezone);
					orbitPanel.Invalidate();
				}
			}
		}

		#endregion

		#region Constructor

		public FormOrbitViewer(List<Comet> comets)
		{
			InitializeComponent();

			DefaultDateTime = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0, DateTimeKind.Local);
			SelectedDateTime = DefaultDateTime;

			Comets = TransformComets(comets);

			Timer = new Timer();
			Timer.Interval = 50;
			Timer.Tick += new System.EventHandler(this.timer_Tick);

			SimulationDirection = ATime.TimeIncrement;

			Settings = new OrbitViewerSettings();
		}

		#endregion

		#region Form_Load

		private void FormOrbit_Load(object sender, System.EventArgs e)
		{
			cboObject.DisplayMember = "Name";
			cboObject.DataSource = Comets;

			if (Comets.Any() && FormMain.UserList.Count == FormMain.MainList.Count)
			{
				//comet with nearest perihelion date
				OVComet c = Comets.OrderBy(x => Math.Abs(x.T - DateTime.Now.JD())).First();
				cboObject.SelectedIndex = Comets.IndexOf(c);
			}

			scrollVert.Value = InitialScrollVert;
			scrollHorz.Value = InitialScrollHorz;
			scrollZoom.Value = InitialScrollZoom;

			cboCenter.DataSource = CenterObjectItems;
			cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Sun;

			cboOrbits.DataSource = OrbitsDisplayItems;
			cboOrbits.SelectedIndex = (int)OrbitPanel.OrbitsEnum.Default;

			cboTimestep.DataSource = TimeStepItems;
			cboTimestep.SelectedIndex = 3;

			ApplySettings(Settings, false);

			ATime atime = new ATime(SelectedDateTime, FormMain.Settings.Location.Timezone);
			orbitPanel.LoadPanel(SelectedComet, atime);
			orbitPanel.Invalidate();
		}

		#endregion

		#region Form_Activated

		private void FormOrbitViewer_Activated(object sender, EventArgs e)
		{
			(this.MdiParent as FormMain).SetOrbitMenuItems(this.Settings);
		}

		#endregion

		#region Form_Closing

		private void FormOrbitViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			PauseSimulation();
		}

		#endregion

		#region Scrollbars

		private void scrollVert_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateVert = (double)(270 - scrollVert.Value);
			orbitPanel.Invalidate();
		}

		private void scrollHorz_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.RotateHorz = (double)(270 - scrollHorz.Value);
			orbitPanel.Invalidate();
		}

		private void scrollZoom_ValueChanged(object sender, EventArgs e)
		{
			orbitPanel.Zoom = (double)scrollZoom.Value;
			orbitPanel.Invalidate();
		}

		#endregion

		#region Mouse Controls

		private void orbitPanel_MouseEnter(object sender, EventArgs e)
		{
			orbitPanel.Focus();
			IsMouseWheelZoom = true;
			IsKeyboardScroll = true;
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

		private void orbitPanel_MouseDown(object sender, MouseEventArgs e)
		{
			IsMouseRotate = true;
			StartDrag = e.Location;
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

		#region Keyboad shortcuts

		private void FormOrbitViewer_KeyDown(object sender, KeyEventArgs e)
		{
			bool handled = true;
			bool control = Control.ModifierKeys == Keys.Control;

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
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Mercury);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Mercury);
					break;

				case Keys.D2:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Venus);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Venus);
					break;

				case Keys.D3:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Earth);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Earth);
					break;

				case Keys.D4:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Mars);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Mars);
					break;

				case Keys.D5:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Jupiter);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Jupiter);
					break;

				case Keys.D6:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Saturn);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Saturn);
					break;

				case Keys.D7:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Uranus);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Uranus);
					break;

				case Keys.D8:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.Neptune);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Neptune);
					break;

				case Keys.D9:
				case Keys.C:
					if (control)
						ChangeVisibleOrbit(OrbitPanel.OrbitsEnum.CometAsteroid);
					else
						ChangeCenterObject(OrbitPanel.CenteredObjectEnum.CometAsteroid);
					break;

				case Keys.D0:
				case Keys.S:
					ChangeCenterObject(OrbitPanel.CenteredObjectEnum.Sun);
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

				default:
					handled = !(cboObject.Focused || cboCenter.Focused || cboOrbits.Focused || cboTimestep.Focused);
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

		#region Date controls

		private void btnDate_Click(object sender, EventArgs e)
		{
			using (FormDateTime fdt = new FormDateTime(DefaultDateTime, SelectedDateTime, GetT()))
			{
				if (fdt.ShowDialog() == DialogResult.OK)
					SelectedDateTime = fdt.SelectedDateTime;
			}
		}

		private double? GetT()
		{
			double? T = null;

			if (cboObject.SelectedIndex >= 0)
				T = Comets.ElementAt(cboObject.SelectedIndex).T;

			return T;
		}

		private void btnNow_Click(object sender, EventArgs e)
		{
			SelectedDateTime = DateTime.Now;
		}

		private void btnPerihDate_Click(object sender, EventArgs e)
		{
			if (cboObject.SelectedIndex >= 0)
			{
				SelectedDateTime = Utils.JDToDateTime(Comets.ElementAt(cboObject.SelectedIndex).T).ToLocalTime();
			}
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
			TimeStep = timeStepSpan[cboTimestep.SelectedIndex];
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

			IsTimeChangedByTimer = true;
			SelectedDateTime = new DateTime(orbitPanel.ATime.Year, orbitPanel.ATime.Month, orbitPanel.ATime.Day, orbitPanel.ATime.Hour, orbitPanel.ATime.Minute, orbitPanel.ATime.Second, DateTimeKind.Utc);
			IsTimeChangedByTimer = false;
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

		#region ComboBoxes

		private void cboObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedComet = Comets.ElementAt(cboObject.SelectedIndex);

			if (orbitPanel.IsPaintEnabled)
			{
				orbitPanel.LoadPanel(SelectedComet, orbitPanel.ATime);
				ValidateCometsCount();
				orbitPanel.Invalidate();
			}

			this.Text = "Orbit Viewer - " + SelectedComet.Name;
		}

		private void cboCenter_SelectedIndexChanged(object sender, EventArgs e)
		{
			orbitPanel.CenterObjectSelected = cboCenter.SelectedIndex;
			orbitPanel.Invalidate();
		}

		private void cboOrbits_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChangeVisibleOrbit((OrbitPanel.OrbitsEnum)cboOrbits.SelectedIndex);
		}

		private void comboBoxCommon_MouseEnter(object sender, EventArgs e)
		{
			(sender as ComboBox).Focus();
		}

		private void ChangeVisibleOrbit(OrbitPanel.OrbitsEnum orbit)
		{
			if (orbit == (int)OrbitPanel.OrbitsEnum.Default)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = OrbitDisplayDefault[i];
			}
			else if (orbit == OrbitPanel.OrbitsEnum.AllOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = true;
			}
			else if (orbit == OrbitPanel.OrbitsEnum.NoOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = false;
			}
			else if (orbit == OrbitPanel.OrbitsEnum.Space)
			{
				return;
			}
			else if (orbit >= OrbitPanel.OrbitsEnum.CometAsteroid)
			{
				OrbitDisplay[(int)orbit - 4] = !OrbitDisplay[(int)orbit - 4];
			}

			orbitPanel.SelectOrbits(OrbitDisplay);
			orbitPanel.Invalidate();
		}

		private void ChangeCenterObject(OrbitPanel.CenteredObjectEnum centeredObject)
		{
			cboCenter.SelectedIndex = (int)centeredObject;
		}

		#endregion

		#region orbitPanel_Resize

		private void orbitPanel_Resize(object sender, EventArgs e)
		{
			orbitPanel.Offscreen = new Bitmap(orbitPanel.Width, orbitPanel.Height);
			orbitPanel.Invalidate();
		}

		#endregion

		#region Methods

		private List<OVComet> TransformComets(List<Comet> comets)
		{
			List<OVComet> list = new List<OVComet>();

			foreach (Comet c in comets)
				list.Add(new OVComet(c));

			return list;
		}

		public void ApplySettings(OrbitViewerSettings ovs, bool refresh)
		{
			orbitPanel.MultipleMode = ovs.MultipleMode;
			orbitPanel.ShowAxes = ovs.ShowAxes;
			orbitPanel.Antiliasing = ovs.Antialiasing;
			orbitPanel.ShowPlanetName = ovs.ShowPlanetName;
			orbitPanel.ShowCometName = ovs.ShowCometName;
			orbitPanel.ShowMagnitude = ovs.ShowMagnitute;
			orbitPanel.ShowDistance = ovs.ShowDistance;
			orbitPanel.ShowDate = ovs.ShowDate;
			orbitPanel.PreserveSelected = ovs.PreserveSelected;

			if (refresh)
				orbitPanel.Invalidate();
		}

		public void ShowAllComets()
		{
			if (this.Comets.Any())
			{
				orbitPanel.Comets = this.Comets.ToList();
				ValidateCometsCount();
				orbitPanel.SelectedIndex = cboObject.SelectedIndex;
				orbitPanel.Invalidate();
			}
		}

		public void ClearComets()
		{
			orbitPanel.ClearComets();
			orbitPanel.Invalidate();
		}

		private void ValidateCometsCount()
		{
			if (orbitPanel.Comets.Count > OrbitPanel.MaxNumberOfComets &&
				OrbitDisplay[(int)OrbitPanel.OrbitsEnum.CometAsteroid - 4])
			{
				//if more comets than max number, then turn off orbit display for comets
				//user can manually turn it on, but performances could be low
				OrbitDisplay[(int)OrbitPanel.OrbitsEnum.CometAsteroid - 4] = false;
				orbitPanel.SelectOrbits(OrbitDisplay);
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
