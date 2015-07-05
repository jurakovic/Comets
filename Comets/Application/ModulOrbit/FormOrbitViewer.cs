using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.ModulOrbit
{
	public partial class FormOrbitViewer : Form
	{
		#region + Consts

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

		bool IsMouseRotate { get; set; }
		bool IsMouseWheelZoom { get; set; }
		Point StartDrag { get; set; }

		Timer Timer { get; set; }

		const int InitialScrollVert = 220;
		const int InitialScrollHorz = 255;
		const int InitialScrollZoom = 145;

		//OrbitDisplayEnum
		bool[] OrbitDisplayDefault = { true, true, true, true, true, true, false, false, false, false };
		bool[] OrbitDisplay = { true, true, true, true, true, true, false, false, false, false };

		ATimeSpan TimeStep { get; set; }
		int SimulationDirection { get; set; }
		bool SimulationStarted { get; set; }

		List<OVComet> Comets { get; set; }

		OVComet SelectedComet { get; set; }

		OrbitViewerSettings settings;
		public OrbitViewerSettings Settings
		{
			get { return settings; }
			set
			{
				settings = value;
				ApplySettings(this.Settings, true);
			}
		}

		#endregion

		#region Constructor

		public FormOrbitViewer(List<Comet> comets)
		{
			InitializeComponent();

			txtDay.Tag = LeMiMa.LDay;
			txtMonth.Tag = LeMiMa.LMonth;
			txtYear.Tag = LeMiMa.LYear;

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
				OVComet c = Comets.Where(x => x.T - DateTime.Now.JD() > 0).OrderBy(y => y.T).FirstOrDefault();
				cboObject.SelectedIndex = c != null ? Comets.IndexOf(c) : 0;
			}

			txtDay.Text = DateTime.Now.Day.ToString();
			txtMonth.Text = DateTime.Now.Month.ToString();
			txtYear.Text = DateTime.Now.Year.ToString();

			ATime atime = CollectATime();

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
		}

		private void orbitPanel_MouseUp(object sender, MouseEventArgs e)
		{
			IsMouseRotate = false;
		}

		private void orbitPanel_MouseLeave(object sender, EventArgs e)
		{
			IsMouseRotate = false;
			IsMouseWheelZoom = false;
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

				while (newHorizValue >= scrollHorz.Maximum)
					newHorizValue -= scrollHorz.Maximum;

				while (newHorizValue < scrollHorz.Minimum)
					newHorizValue += scrollHorz.Maximum;

				while (newVertValue >= scrollVert.Maximum)
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
			bool control = Control.ModifierKeys == Keys.Control;

			switch (e.KeyCode)
			{
				case Keys.D1:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Mercury);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Mercury;
					break;

				case Keys.D2:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Venus);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Venus;
					break;

				case Keys.D3:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Earth);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Earth;
					break;

				case Keys.D4:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Mars);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Mars;
					break;

				case Keys.D5:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Jupiter);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Jupiter;
					break;

				case Keys.D6:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Saturn);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Saturn;
					break;

				case Keys.D7:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Uranus);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Uranus;
					break;

				case Keys.D8:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.Neptune);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Neptune;
					break;

				case Keys.D9:
				case Keys.C:
					if (control)
						ChangeVisibleOrbit((int)OrbitPanel.OrbitsEnum.CometAsteroid);
					else
						cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.CometAsteroid;
					break;

				case Keys.D0:
				case Keys.S:
					cboCenter.SelectedIndex = (int)OrbitPanel.CenteredObjectEnum.Sun;
					break;

				case Keys.Space:
				case Keys.P:
					if (SimulationStarted)
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
			}
		}

		#endregion

		#region Date controls

		private void txtDateCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtDateCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtMonthYear_TextChanged(object sender, EventArgs e)
		{
			if (txtMonth.Text.Length > 0 && txtYear.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(txtYear.Int(), txtMonth.Int());

				LeMiMa o = txtDay.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				if (txtDay.Text.Length > 0 && txtDay.Int() > n.Max)
					txtDay.Text = n.Max.ToString();

				txtDay.Tag = n;
			}
		}

		private void btnNow_Click(object sender, EventArgs e)
		{
			txtDay.Text = DateTime.Now.Day.ToString();
			txtMonth.Text = DateTime.Now.Month.ToString();
			txtYear.Text = DateTime.Now.Year.ToString();
		}

		private void btnSet_Click(object sender, EventArgs e)
		{
			Timer.Stop();
			orbitPanel.ATime = CollectATime();
			orbitPanel.Invalidate();
		}

		private ATime CollectATime()
		{
			int day = txtDay.Int();
			int month = txtMonth.Int();
			int year = txtYear.Int();

			ATime atime = new ATime(year, month, day, 0.0);
			return atime;
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

			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, ATime.TimeDecrement);
			orbitPanel.ATime = atime;
			orbitPanel.Invalidate();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			PauseSimulation();
		}

		private void btnForStep_Click(object sender, EventArgs e)
		{
			PauseSimulation();

			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, ATime.TimeIncrement);
			orbitPanel.ATime = atime;
			orbitPanel.Invalidate();
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
			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, SimulationDirection);

			if (atime < ATime.Minimum || atime > ATime.Maximum)
				PauseSimulation();

			orbitPanel.ATime = atime;
			orbitPanel.Invalidate();
		}

		private void PlaySimulation(int direction)
		{
			SimulationDirection = direction;
			Timer.Start();
			SimulationStarted = true;
		}

		private void PauseSimulation()
		{
			Timer.Stop();
			SimulationStarted = false;
		}

		private void FasterSimulation()
		{
			if (!SimulationStarted)
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
			if (!SimulationStarted)
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

			if (orbitPanel.PaintEnabled)
			{
				orbitPanel.LoadPanel(SelectedComet, orbitPanel.ATime);
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
			ChangeVisibleOrbit(cboOrbits.SelectedIndex);
		}

		private void ChangeVisibleOrbit(int index)
		{
			if (index == (int)OrbitPanel.OrbitsEnum.Default)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = OrbitDisplayDefault[i];
			}
			else if (index == (int)OrbitPanel.OrbitsEnum.AllOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = true;
			}
			else if (index == (int)OrbitPanel.OrbitsEnum.NoOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = false;
			}
			else if (index == (int)OrbitPanel.OrbitsEnum.Space)
			{
				return;
			}
			else if (index >= (int)OrbitPanel.OrbitsEnum.CometAsteroid)
			{
				OrbitDisplay[index - 4] = !OrbitDisplay[index - 4];
			}

			orbitPanel.SelectOrbits(OrbitDisplay);
			orbitPanel.Invalidate();
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
			orbitPanel.EclipticAxis = ovs.EclipticAxis;
			orbitPanel.Antiliasing = ovs.Antialiasing;
			orbitPanel.ShowPlanetName = ovs.ShowPlanetName;
			orbitPanel.ShowCometName = ovs.ShowCometName;
			orbitPanel.ShowMagnitude = ovs.ShowMagnitute;
			orbitPanel.ShowDistance = ovs.ShowDistance;
			orbitPanel.ShowDate = ovs.ShowDate;

			if (refresh)
				orbitPanel.Invalidate();
		}

		public void ClearComets()
		{
			orbitPanel.ClearComets();
			orbitPanel.Invalidate();
		}

		public void SaveImage()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
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
					MessageBox.Show(String.Format("Orbit saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
