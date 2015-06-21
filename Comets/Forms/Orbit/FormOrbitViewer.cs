using Comets.Classes;
using Comets.Helpers;
using Comets.OrbitViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Forms.Orbit
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

		public enum CenteredObjectEnum
		{
			Sun = 0,
			CometAsteroid,
			Mercury,
			Venus,
			Earth,
			Mars,
			Jupiter,
			Saturn,
			Uranus,
			Neptune
			//Pluto
		}

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

		public enum OrbitsEnum
		{
			Default = 0,
			AllOrbits,
			NoOrbits,
			Space, //------
			CometAsteroid,
			Mercury,
			Venus,
			Earth,
			Mars,
			Jupiter,
			Saturn,
			Uranus,
			Neptune
			//Pluto
		}

		public enum OrbitDisplayEnum
		{
			CometAsteroid = 0,
			Mercury,
			Venus,
			Earth,
			Mars,
			Jupiter,
			Saturn,
			Uranus,
			Neptune
			//Pluto
		}

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

		#region MaxValues

		int MaxDay = 31;
		int MaxMonth = 12;
		int MaxYear = 9999;

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

		//ATime minATime = new ATime(1000, 1, 1, 0, 0, 0.0, 0.0);
		//ATime maxATime = new ATime(5000, 1, 1, 0, 0, 0.0, 0.0);

		//OrbitDisplayEnum
		bool[] OrbitDisplayDefault = { true, true, true, true, true, true, false, false, false, false };
		bool[] OrbitDisplay = { true, true, true, true, true, true, false, false, false, false };

		ATimeSpan TimeStep { get; set; }
		int SimulationDirection { get; set; }
		bool SimulationStarted { get; set; }

		List<OVComet> Comets { get; set; }

		OVComet SelectedComet { get; set; }

		#endregion

		#region Constructor

		public FormOrbitViewer(List<Comet> comets, int tag)
		{
			InitializeComponent();

			this.Tag = tag;

			Comets = TransformComets(comets);

			Timer = new Timer();
			Timer.Interval = 50;
			Timer.Tick += new System.EventHandler(this.timer_Tick);

			SimulationDirection = ATime.TIME_INCREMENT;
		}

		#endregion

		#region TransformComets

		private List<OVComet> TransformComets(List<Comet> comets)
		{
			List<OVComet> list = new List<OVComet>();

			foreach (Comet c in comets)
				list.Add(new OVComet(c));

			return list;
		}

		#endregion

		#region Form_Load

		private void FormOrbit_Load(object sender, System.EventArgs e)
		{
			cboObject.DisplayMember = "Name";
			cboObject.DataSource = Comets;

			txtDay.Text = DateTime.Now.Day.ToString();
			txtMonth.Text = DateTime.Now.Month.ToString();
			txtYear.Text = DateTime.Now.Year.ToString();

			ATime atime = CollectATime();

			scrollVert.Value = InitialScrollVert;
			scrollHorz.Value = InitialScrollHorz;
			scrollZoom.Value = InitialScrollZoom;

			cboCenter.DataSource = CenterObjectItems;
			cboCenter.SelectedIndex = (int)CenteredObjectEnum.Sun;

			cboOrbits.DataSource = OrbitsDisplayItems;
			cboOrbits.SelectedIndex = (int)OrbitsEnum.Default;

			cboTimestep.DataSource = TimeStepItems;
			cboTimestep.SelectedIndex = 3;

			orbitPanel.ShowPlanetName = cbxPlanet.Checked;
			orbitPanel.ShowObjectName = cbxObject.Checked;
			orbitPanel.ShowDistanceLabel = cbxDistance.Checked;
			orbitPanel.ShowDateLabel = cbxDate.Checked;

			orbitPanel.LoadPanel(SelectedComet, atime);
			//orbitPanel.LoadPanel(Comets, cboObject.SelectedIndex, atime);
			orbitPanel.Invalidate();
		}

		#endregion

		#region Form_Closing

		private void FormOrbitViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			PauseSimulation();

			FormMain main = this.MdiParent as FormMain;
			main.RemoveWindowMenuItem((int)this.Tag);
			main.SetWindowMenuItemVisible(main.MdiChildren.Length > 1 ? true : false);
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
			//if (e.Button == MouseButtons.Left)
			//{
			//	IsMouseRotate = false;
			//}
			//else if (e.Button == MouseButtons.Right)
			//{
			//	IsMouseRotate = true;
			//}

			IsMouseRotate = true;
			StartDrag = e.Location;
		}

		private void orbitPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (IsMouseRotate)
			{
				double horizontalMax = scrollHorz.Maximum;
				double verticalMax = scrollVert.Maximum;

				double width = orbitPanel.Width;
				double height = orbitPanel.Height;

				int deltaX = e.X - StartDrag.X;
				int deltaY = e.Y - StartDrag.Y;

				double hkoef = 2.0;
				double vkoef = 3.0;

				double a = (double)orbitPanel.Size.Width / (double)orbitPanel.MinimumSize.Width;
				double b = (double)orbitPanel.Size.Height / (double)orbitPanel.MinimumSize.Height;

				double x = (horizontalMax / width) * a * hkoef;
				double y = (verticalMax / height) * b * vkoef;

				int newHv = (int)(x * deltaX);
				int newVv = (int)(y * deltaY);

				int newHorizValue = scrollHorz.Value + newHv;
				int newVertValue = scrollVert.Value + newVv;

				while (newHorizValue >= scrollHorz.Maximum)
					newHorizValue -= scrollHorz.Maximum;

				while (newHorizValue < scrollHorz.Minimum)
					newHorizValue += scrollHorz.Maximum;

				while (newVertValue >= scrollVert.Maximum)
					newVertValue -= scrollVert.Maximum;

				while (newVertValue < scrollVert.Minimum)
					newVertValue += scrollVert.Maximum;

				scrollVert.Value = newVertValue;
				scrollHorz.Value = newHorizValue;

				//if (newHorizValue == scrollHorz.Minimum)
				//	scrollHorz.Value = newHorizValue;
				//else if (newHorizValue < scrollHorz.Minimum)
				//	scrollHorz.Value = scrollHorz.Maximum + newHorizValue;
				//else if (newHorizValue > scrollHorz.Maximum)
				//	scrollHorz.Value = newHorizValue % scrollHorz.Maximum;
				//else
				//	scrollHorz.Value = newHorizValue;

				//if (newVertValue < scrollVert.Minimum)
				//	scrollVert.Value = scrollVert.Minimum;
				//else if (newVertValue > scrollVert.Maximum)
				//	scrollVert.Value = scrollVert.Maximum;
				//else
				//	scrollVert.Value = newVertValue;

				StartDrag = e.Location;
			}

			//else if (IsMouseZoom)
			//{
			//	double zoommax = scrollZoom.Maximum;
			//	double height = orbitPanel.Height;
			//	double deltaZ = StartDrag.Y - e.Y;

			//	double zkoef = 0.75;

			//	double z = (zoommax / height) * zkoef;

			//	int newZv = (int)(z * deltaZ);

			//	int newZvalue = scrollZoom.Value + newZv;

			//	if (newZvalue < scrollZoom.Minimum)
			//		scrollZoom.Value = scrollZoom.Minimum;
			//	else if (newZvalue > scrollZoom.Maximum)
			//		scrollZoom.Value = scrollZoom.Maximum;
			//	else
			//		scrollZoom.Value = newZvalue;

			//	StartDrag = e.Location;
			//}
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
						ChangeVisibleOrbit((int)OrbitsEnum.Mercury);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Mercury;
					break;

				case Keys.D2:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Venus);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Venus;
					break;

				case Keys.D3:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Earth);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Earth;
					break;

				case Keys.D4:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Mars);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Mars;
					break;

				case Keys.D5:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Jupiter);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Jupiter;
					break;

				case Keys.D6:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Saturn);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Saturn;
					break;

				case Keys.D7:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Uranus);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Uranus;
					break;

				case Keys.D8:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.Neptune);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.Neptune;
					break;

				case Keys.D9:
				case Keys.C:
					if (control)
						ChangeVisibleOrbit((int)OrbitsEnum.CometAsteroid);
					else
						cboCenter.SelectedIndex = (int)CenteredObjectEnum.CometAsteroid;
					break;

				case Keys.D0:
				case Keys.S:
					cboCenter.SelectedIndex = (int)CenteredObjectEnum.Sun;
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

		#region Date

		private void txtDateCommon_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e, 1, GetMaximumValueForControl(((TextBox)sender).Name));
		}

		private void txtDateCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			string name = ((TextBox)sender).Name;
			e.Handled = Utils.HandleKeyPress(sender, e, GetLengthValueForControl(name), GetMaximumValueForControl(name));
		}

		private int GetLengthValueForControl(string controlName)
		{
			int length = 0;

			if (controlName.ToLower().Contains("day"))
				length = 2;
			else if (controlName.ToLower().Contains("month"))
				length = 2;
			else if (controlName.ToLower().Contains("year"))
				length = 4;
			else
				throw new Exception("Unknown textbox name");

			return length;
		}

		private int GetMaximumValueForControl(string controlName)
		{
			int maximum = 0;

			if (controlName.ToLower().Contains("day"))
				maximum = MaxDay;
			else if (controlName.ToLower().Contains("month"))
				maximum = MaxMonth;
			else if (controlName.ToLower().Contains("year"))
				maximum = MaxYear;
			else
				throw new Exception("Unknown textbox name");

			return maximum;
		}

		private void txtMonthYear_TextChanged(object sender, EventArgs e)
		{
			if (txtMonth.Text.Length > 0 && txtYear.Text.Length > 0)
			{
				MaxDay = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));
				if (txtDay.Text.Length > 0 && Convert.ToInt32(txtDay.Text) > MaxDay)
					txtDay.Text = MaxDay.ToString();
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
			int day = Convert.ToInt32(txtDay.Text);
			int month = Convert.ToInt32(txtMonth.Text);
			int year = Convert.ToInt32(txtYear.Text);

			ATime atime = new ATime(year, month, day, 0.0);
			return atime;
		}

		#endregion

		#region Simulation

		private void btnRevPlay_Click(object sender, EventArgs e)
		{
			PlaySimulation(ATime.TIME_DECREMENT);
		}

		private void btnRevStep_Click(object sender, EventArgs e)
		{
			PauseSimulation();

			ATime atime = orbitPanel.ATime;
			atime.ChangeDate(TimeStep, ATime.TIME_DECREMENT);
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
			atime.ChangeDate(TimeStep, ATime.TIME_INCREMENT);
			orbitPanel.ATime = atime;
			orbitPanel.Invalidate();
		}

		private void btnForPlay_Click(object sender, EventArgs e)
		{
			PlaySimulation(ATime.TIME_INCREMENT);
		}

		private void cboTimestep_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimeStep = timeStepSpan[cboTimestep.SelectedIndex];
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			double oldJD = 0.0;

			ATime atime = orbitPanel.ATime;
			oldJD = atime.JD;
			atime.ChangeDate(TimeStep, SimulationDirection);

			if (oldJD == atime.JD)
				PauseSimulation(); //reached min or max datetime value

			orbitPanel.ATime = atime;
			orbitPanel.Invalidate();
		}

		private void PlaySimulation(int direction = 1)
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
				PlaySimulation();
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
				PlaySimulation();
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
				if (orbitPanel.Multiple)
					orbitPanel.SelectedIndex = cboObject.SelectedIndex;
				else
					orbitPanel.Comet = SelectedComet;

				orbitPanel.Invalidate();
			}

			this.Text = this.Tag + " Orbit Viewer - " + SelectedComet.Name;

			FormMain main = this.MdiParent as FormMain;
			main.RenameWindowItem((int)this.Tag, this.Text);
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
			if (index == (int)OrbitsEnum.Default)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = OrbitDisplayDefault[i];
			}
			else if (index == (int)OrbitsEnum.AllOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = true;
			}
			else if (index == (int)OrbitsEnum.NoOrbits)
			{
				for (int i = 0; i < OrbitDisplay.Length; i++)
					OrbitDisplay[i] = false;
			}
			else if (index == (int)OrbitsEnum.Space)
			{
				return;
			}
			else if (index >= (int)OrbitsEnum.CometAsteroid)
			{
				OrbitDisplay[index - 4] = !OrbitDisplay[index - 4];
			}

			orbitPanel.SelectOrbits(OrbitDisplay);
			orbitPanel.Invalidate();
		}

		#endregion

		#region CheckBoxes

		private void cbxObject_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowObjectName = cbxObject.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxPlanet_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowPlanetName = cbxPlanet.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxDistance_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDistanceLabel = cbxDistance.Checked;
			orbitPanel.Invalidate();
		}

		private void cbxDate_CheckedChanged(object sender, EventArgs e)
		{
			orbitPanel.ShowDateLabel = cbxDate.Checked;
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

		#region GetControl

		private Control GetControl(string name)
		{
			Control c = null;

			//switch (name)
			//{

			//}

			return c;
		}

		#endregion
	}
}
