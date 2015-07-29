
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Comets.OrbitViewer
{
	public class OrbitPanel : Panel
	{
		#region Enum

		public enum Object
		{
			Sun = 0,
			Mercury,
			Venus,
			Earth,
			Mars,
			Jupiter,
			Saturn,
			Uranus,
			Neptune,
			Comet
		}

		#endregion

		#region Const

		public const int MaxNumberOfComets = 30;
		private const bool DefaultShowMarker = true;
		private const bool DefaultPreserveOrbit = true;
		private const bool DefaultPreserveLabel = true;
		private const bool DefaultShowDistance = true;
		private const bool DefaultShowDate = true;
		private const Object DefaultCenterObject = Object.Sun;

		private readonly List<Object> DefaultOrbitDisplay = new List<Object> 
		{
			Object.Mercury,
			Object.Venus,
			Object.Earth,
			Object.Mars,
			Object.Jupiter,
			Object.Comet
		};

		private readonly List<Object> DefaultLabelDisplay = new List<Object> 
		{ 
			Object.Mercury,
			Object.Venus,
			Object.Earth,
			Object.Mars,
			Object.Jupiter, 
			Object.Saturn,
			Object.Uranus,
			Object.Neptune,
			Object.Comet
		};

		#endregion

		#region Colors

		//https://msdn.microsoft.com/en-us/library/aa358803%28v=vs.85%29.aspx
		protected Color ColorCometOrbitUpper = Color.Tomato;
		protected Color ColorCometOrbitLower = Color.Firebrick;
		protected Color ColorCometMarker = Color.Red;
		protected Color ColorCometNameSelected = Color.White;
		protected Color ColorCometName = Color.Peru;
		//protected Color ColorPlanetOrbitUpper = Color.White;
		//protected Color ColorPlanetOrbitLower = Color.DimGray;
		protected Color ColorPlanetOrbitUpper = Color.SteelBlue;
		protected Color ColorPlanetOrbitLower = Color.DarkSlateBlue;
		protected Color ColorPlanet = Color.Lime;
		protected Color ColorPlanetName = Color.LimeGreen;
		protected Color ColorSun = Color.Orange;
		protected Color ColorAxisPlus = Color.Yellow;
		protected Color ColorAxisMinus = Color.DarkOliveGreen;
		protected Color ColorInformation = Color.White;

		#endregion

		#region Fonts

		protected Font FontObjectName = new Font("Helvetica", 10, FontStyle.Regular);
		protected Font FontPlanetName = new Font("Helvetica", 10, FontStyle.Regular);
		protected Font FontInformation = new Font("Helvetica", 10, FontStyle.Bold);
		protected Font FontAxisLabel = new Font("Helvetica", 8.5F, FontStyle.Regular);

		#endregion

		#region Fields

		private int SelectedIndex;

		private List<CometOrbit> CometOrbits;
		private List<Xyz> CometsPos;

		private PlanetOrbit[] PlanetOrbit;
		private Xyz[] PlanetPos;
		private double EpochPlanetOrbit;

		private Matrix MtxToEcl;
		private double EpochToEcl;
		private Matrix MtxRotate;
		private int X0;
		private int Y0;

		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<OVComet> Comets { get; private set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		OVComet SelectedComet
		{
			get
			{
				OVComet comet = null;

				if (SelectedIndex >= 0 && Comets.Any())
					comet = Comets[SelectedIndex];

				return comet;
			}
		}

		private ATime _atime;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ATime ATime
		{
			get { return this._atime; }
			set
			{
				if (value < ATime.Minimum)
					this._atime = new ATime(ATime.Minimum);
				else if (value > ATime.Maximum)
					this._atime = new ATime(ATime.Maximum);
				else
					this._atime = value;

				UpdatePositions(ATime);
			}
		}

		private bool _multipleMode;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MultipleMode
		{
			get { return _multipleMode; }
			set
			{
				_multipleMode = value;

				if (!_multipleMode && IsPaintEnabled)
					ClearComets();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowMarker { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool PreserveSelectedOrbit { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool PreserveSelectedLabel { get; set; }


		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPaintEnabled { get; private set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Offscreen { get; set; }


		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Object> OrbitDisplay { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Object> LabelDisplay { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Object CenteredObject { get; set; }


		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double RotateHorz { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double RotateVert { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double Zoom { get; set; }


		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowDistance { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowDate { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowAxes { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Antiliasing { get; set; }

		#endregion

		#region Consctructor

		public OrbitPanel()
		{
			this.DoubleBuffered = true;

			PlanetPos = new Xyz[8];
			PlanetOrbit = new PlanetOrbit[8];

			Comets = new List<OVComet>();
			CometOrbits = new List<CometOrbit>();
			CometsPos = new List<Xyz>();

			OrbitDisplay = DefaultOrbitDisplay;
			LabelDisplay = DefaultLabelDisplay;
			CenteredObject = DefaultCenterObject;
			ShowMarker = DefaultShowMarker;
			PreserveSelectedOrbit = DefaultPreserveOrbit;
			PreserveSelectedLabel = DefaultPreserveLabel;

			ShowDistance = DefaultShowDistance;
			ShowDate = DefaultShowDate;

			Offscreen = null;
			IsPaintEnabled = false;
		}

		#endregion

		#region LoadPanel

		public void LoadPanel(OVComet comet, ATime atime)
		{
			IsPaintEnabled = true;

			if (!MultipleMode)
			{
				Comets.Clear();
				CometOrbits.Clear();
			}

			if (comet != null && !Comets.Contains(comet))
			{
				Comets.Add(comet);
				CometOrbits.Add(new CometOrbit(comet, CometOrbit.MaxDivisions));
			}

			SelectedIndex = Comets.IndexOf(comet);

			ATime = atime;

			//UpdatePositions(atime);
			UpdatePlanetOrbit(atime);
			UpdateRotationMatrix(atime);
		}

		public void LoadPanel(List<OVComet> comets, ATime atime, int index)
		{
			IsPaintEnabled = true;
			MultipleMode = true;

			Comets.Clear();
			CometOrbits.Clear();

			Comets = comets;

			foreach (OVComet c in Comets)
				CometOrbits.Add(new CometOrbit(c, CometOrbit.MaxDivisions));

			SelectedIndex = index;

			ATime = atime;

			//UpdatePositions(atime);
			UpdatePlanetOrbit(atime);
			UpdateRotationMatrix(atime);
		}

		#endregion

		#region OnPaint

		protected override void OnPaint(PaintEventArgs e)
		{
			if (IsPaintEnabled)
			{
				if (Offscreen == null)
					Offscreen = new Bitmap(Size.Width, Size.Height);

				Update(e.Graphics);
			}
		}

		#endregion

		#region Update

		public void Update(Graphics g)
		{
			Point point3;
			Xyz xyz;

			// Calculate Drawing Parameter
			Matrix mtxRotH = Matrix.RotateZ(RotateHorz * Math.PI / 180.0);
			Matrix mtxRotV = Matrix.RotateX(RotateVert * Math.PI / 180.0);
			MtxRotate = mtxRotV.Mul(mtxRotH);

			X0 = Size.Width / 2;
			Y0 = Size.Height / 2;

			if (Math.Abs(EpochToEcl - ATime.JD) > 365.2422 * 5)
			{
				UpdateRotationMatrix(ATime);
			}

			if (CenteredObject == Object.Comet && SelectedIndex >= 0)
			{
				xyz = CometsPos[SelectedIndex].Rotate(MtxToEcl).Rotate(MtxRotate);
				point3 = GetDrawPoint(xyz);

				X0 = Size.Width - point3.X;
				Y0 = Size.Height - point3.Y;
			}
			else if (CenteredObject >= Object.Mercury && CenteredObject != Object.Comet)
			{
				xyz = PlanetPos[(int)CenteredObject - 1].Rotate(MtxRotate);
				point3 = GetDrawPoint(xyz);

				X0 = Size.Width - point3.X;
				Y0 = Size.Height - point3.Y;
			}

			using (Graphics graphics = Graphics.FromImage(Offscreen))
			{
				// Clear bacground
				SolidBrush sb = new SolidBrush(Color.Black);
				graphics.FillRectangle(sb, 0, 0, Size.Width, Size.Height);

				if (ShowAxes)
					DrawAxes(graphics);

				// Draw Sun
				sb.Color = ColorSun;
				int diameter = 3;
				int radius = diameter * 2;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.FillPie(sb, X0 - diameter, Y0 - diameter, radius, radius, 0, 360);

				//  Draw Orbit of Planets
				if (Math.Abs(EpochPlanetOrbit - ATime.JD) > 365.2422 * 5)
					UpdatePlanetOrbit(ATime);

				double zoom = 30.0;

				//if (Zoom * 39.5 >= zoom)
				//{
				//	if (OrbitDisplay[(int)OrbitDisplayEnum.Pluto])
				//		DrawPlanetOrbit(graphics, PlanetOrbit[Planet.PLUTO - 1]);

				//	DrawPlanetBody(graphics, FontPlanetName, PlanetPos[8], "Pluto");
				//}

				if (Zoom * 30.1 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Neptune))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Neptune - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[7], Object.Neptune);
				}

				if (Zoom * 19.2 >= zoom)
				{

					if (OrbitDisplay.Contains(Object.Uranus))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Uranus - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[6], Object.Uranus);
				}

				if (Zoom * 9.58 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Saturn))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Saturn - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[5], Object.Saturn);
				}

				if (Zoom * 5.2 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Jupiter))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Jupiter - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[4], Object.Jupiter);
				}

				if (Zoom * 1.524 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Mars))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Mars - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[3], Object.Mars);
				}

				if (Zoom * 1.0 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Earth))
						DrawEarthOrbit(graphics, PlanetOrbit[Planet.Earth - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[2], Object.Earth);
				}

				if (Zoom * 0.723 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Venus))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Venus - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[1], Object.Venus);
				}

				if (Zoom * 0.387 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Mercury))
						DrawPlanetOrbit(graphics, PlanetOrbit[Planet.Mercury - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetPos[0], Object.Mercury);
				}

				DrawCometOrbit(graphics, CometOrbits);
				DrawCometBody(graphics);

				// Information
				sb.Color = ColorInformation;

				// Object Name string
				int labelMargin = 8;
				double fontSize = (double)FontInformation.Size;

				Point point1 = new Point(labelMargin, labelMargin);

				if (SelectedComet != null)
				{
					graphics.DrawString(SelectedComet.Name, FontInformation, sb, point1.X, point1.Y);

					if (ShowDistance)
					{
						double[] mdr = GetMagnutideAndDistances(SelectedComet, SelectedIndex);

						string mstr = String.Format("Magnitude:       {0:#0.0}", mdr[0]);
						string dstr = String.Format("Earth Distance: {0:#0.0000} AU", mdr[1]);
						string rstr = String.Format("Sun Distance:   {0:#0.0000} AU", mdr[2]);

						point1.Y = Size.Height - labelMargin - (int)(fontSize * 5.0);
						graphics.DrawString(mstr, FontInformation, sb, point1.X, point1.Y);

						point1.Y = Size.Height - labelMargin - (int)(fontSize * 3.5);
						graphics.DrawString(dstr, FontInformation, sb, point1.X, point1.Y);

						point1.Y = Size.Height - labelMargin - (int)(fontSize * 2.0);
						graphics.DrawString(rstr, FontInformation, sb, point1.X, point1.Y);
					}
				}

				if (ShowDate)
				{
					// Date string
					string strDate = String.Format("{0:00} {1} {2} {3:00}:{4:00}:{5:00}", ATime.Day, ATime.MonthAbbr(ATime.Month), ATime.Year, ATime.Hour, ATime.Minute, ATime.Second);
					point1.X = Size.Width - (int)graphics.MeasureString(strDate, FontInformation).Width - labelMargin;
					point1.Y = Size.Height - labelMargin - (int)(fontSize * 2.0);
					graphics.DrawString(strDate, FontInformation, sb, point1.X, point1.Y);
				}
			}

			g.DrawImage(Offscreen, 0, 0);
		}

		#endregion

		#region + Methods

		#region UpdatePositions

		private void UpdatePositions(ATime atime)
		{
			if (IsPaintEnabled)
			{
				CometsPos.Clear();
				foreach (OVComet c in Comets)
					CometsPos.Add(c.GetPos(atime.JD));

				for (int i = 0; i < 8; i++)
				{
					PlanetPos[i] = Planet.GetPos(Planet.Mercury + i, atime);
				}
			}
		}

		#endregion

		#region UpdatePlanetOrbit

		private void UpdatePlanetOrbit(ATime atime)
		{
			int divisions = 300;

			for (int i = Planet.Mercury; i <= Planet.Neptune; i++)
			{
				PlanetOrbit[i - Planet.Mercury] = new PlanetOrbit(i, atime, divisions);
			}

			EpochPlanetOrbit = atime.JD;
		}

		#endregion

		#region UpdateRotationMatrix

		private void UpdateRotationMatrix(ATime atime)
		{
			Matrix mtxPrec = Matrix.PrecMatrix(Astro.JD2000, atime.JD);
			Matrix mtxEqt2Ecl = Matrix.RotateX(ATime.GetEp(atime.JD));
			MtxToEcl = mtxEqt2Ecl.Mul(mtxPrec);
			EpochToEcl = atime.JD;
		}

		#endregion

		#region GetDrawPoint

		private Point GetDrawPoint(Xyz xyz)
		{
			double mul = (Zoom * (double)this.MinimumSize.Width) / (1500.0 * (1.0 + xyz.Z / 625.0));
			int X = X0 + (int)Math.Round(xyz.X * mul);
			int Y = Y0 - (int)Math.Round(xyz.Y * mul);
			return new Point(X, Y);
		}

		#endregion

		#region DrawAxes

		private void DrawAxes(Graphics graphics)
		{
			graphics.SmoothingMode = Antiliasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

			Pen pen = new Pen(ColorAxisMinus);
			Xyz xyz;
			Point point;
			double sizeAU = 50.0;

			// -X
			xyz = new Xyz(-sizeAU, 0.0, 0.0).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("Autumnal equinox", FontAxisLabel, new SolidBrush(Color.Gray), point);

			// -Y
			xyz = new Xyz(0.0, -sizeAU, 0.0).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("Winter solstice", FontAxisLabel, new SolidBrush(Color.Gray), point);

			// -Z
			xyz = new Xyz(0.0, 0.0, -sizeAU).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("South ecliptic pole", FontAxisLabel, new SolidBrush(Color.Gray), point);

			pen.Color = ColorAxisPlus;

			// +X
			xyz = new Xyz(sizeAU, 0.0, 0.0).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("Vernal equinox", FontAxisLabel, new SolidBrush(Color.Gray), point);

			// +Y
			xyz = new Xyz(0.0, sizeAU, 0.0).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("Summer solstice", FontAxisLabel, new SolidBrush(Color.Gray), point);

			// +Z
			xyz = new Xyz(0.0, 0.0, sizeAU).Rotate(MtxRotate);
			point = GetDrawPoint(xyz);
			graphics.DrawLine(pen, X0, Y0, point.X, point.Y);
			graphics.DrawString("North ecliptic pole", FontAxisLabel, new SolidBrush(Color.Gray), point);
		}

		#endregion

		#region DrawCometOrbit

		private void DrawCometOrbit(Graphics graphics, List<CometOrbit> cometOrbits)
		{
			graphics.SmoothingMode = Antiliasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

			for (int i = 0; i < Comets.Count; i++)
			{
				if (OrbitDisplay.Contains(Object.Comet) || (MultipleMode && PreserveSelectedOrbit && i == SelectedIndex))
				{
					Xyz xyz = cometOrbits[i].GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);
					Pen pen = new Pen(Color.White);
					Point point1, point2;
					point1 = GetDrawPoint(xyz);

					for (int j = 1; j <= cometOrbits[i].Division; j++)
					{
						xyz = cometOrbits[i].GetAt(j).Rotate(MtxToEcl);
						pen.Color = xyz.Z >= 0.0 ? ColorCometOrbitUpper : ColorCometOrbitLower;
						xyz = xyz.Rotate(MtxRotate);
						point2 = GetDrawPoint(xyz);
						graphics.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
						point1 = point2;
					}
				}
			}
		}

		#endregion

		#region DrawCometBody

		private void DrawCometBody(Graphics graphics)
		{
			graphics.SmoothingMode = SmoothingMode.AntiAlias;

			int diameter = 2;

			for (int i = 0; i < Comets.Count; i++)
			{
				Xyz xyz = CometsPos[i].Rotate(MtxToEcl).Rotate(MtxRotate);

				Point point1 = GetDrawPoint(xyz);
				Comets[i].PanelLocation = point1;

				Color color = GetCometColorAndDiameter(Comets[i], i, out diameter);
				SolidBrush sb = new SolidBrush(color);
				graphics.FillPie(sb, point1.X - diameter, point1.Y - diameter, diameter * 2, diameter * 2, 0, 360);

				if (ShowMarker && i == SelectedIndex)
				{
					int offset = diameter + 4;
					int length = diameter + 8;

					Pen p = new Pen(ColorCometMarker);
					p.Width = 3;

					graphics.DrawLine(p, new Point(point1.X, point1.Y - length), new Point(point1.X, point1.Y - offset));
					graphics.DrawLine(p, new Point(point1.X, point1.Y + length), new Point(point1.X, point1.Y + offset));
					graphics.DrawLine(p, new Point(point1.X - length, point1.Y), new Point(point1.X - offset, point1.Y));
					graphics.DrawLine(p, new Point(point1.X + length, point1.Y), new Point(point1.X + offset, point1.Y));
				}

				if ((LabelDisplay.Contains(Object.Comet)) || (MultipleMode && PreserveSelectedLabel && i == SelectedIndex))
				{
					if (MultipleMode && i == SelectedIndex)
						sb.Color = ColorCometNameSelected;
					else
						sb.Color = ColorCometName;

					graphics.DrawString(Comets[i].Name, FontObjectName, sb, point1.X + 5, point1.Y);
				}
			}
		}

		#endregion

		#region DrawPlanetOrbit

		private void DrawPlanetOrbit(Graphics graphics, PlanetOrbit planetOrbit)
		{
			graphics.SmoothingMode = Antiliasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

			Pen pen = new Pen(ColorPlanetOrbitUpper);
			Point point1, point2;
			Xyz xyz = planetOrbit.GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);

			point1 = GetDrawPoint(xyz);

			for (int i = 1; i <= planetOrbit.Division; i++)
			{
				xyz = planetOrbit.GetAt(i).Rotate(MtxToEcl);

				pen.Color = xyz.Z >= 0.0 ? ColorPlanetOrbitUpper : ColorPlanetOrbitLower;

				xyz = xyz.Rotate(MtxRotate);
				point2 = GetDrawPoint(xyz);
				graphics.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
				point1 = point2;
			}
		}

		#endregion

		#region DrawEarthOrbit

		private void DrawEarthOrbit(Graphics graphics, PlanetOrbit planetOrbit)
		{
			graphics.SmoothingMode = Antiliasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

			Pen pen = new Pen(ColorPlanetOrbitUpper);
			Point point1, point2;
			Xyz xyz = planetOrbit.GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);

			point1 = GetDrawPoint(xyz);

			for (int i = 1; i <= planetOrbit.Division; i++)
			{
				xyz = planetOrbit.GetAt(i).Rotate(MtxToEcl);
				xyz = xyz.Rotate(MtxRotate);
				point2 = GetDrawPoint(xyz);
				graphics.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
				point1 = point2;
			}
		}

		#endregion

		#region DrawPlanetBody

		private void DrawPlanetBody(Graphics graphics, Font font, Xyz planetPos, Object obj)
		{
			graphics.SmoothingMode = SmoothingMode.AntiAlias; //body always antialiased

			Xyz xyz = planetPos.Rotate(MtxRotate);
			Point point = GetDrawPoint(xyz);
			SolidBrush sb = new SolidBrush(ColorPlanet);

			graphics.FillPie(sb, point.X - 2, point.Y - 2, 5, 5, 0, 360);

			if (LabelDisplay.Contains(obj))
			{
				sb.Color = ColorPlanetName;
				string name = obj.ToString();
				graphics.DrawString(name, font, sb, point.X + 5, point.Y);
			}
		}

		#endregion

		#region ClearComets

		public void ClearComets()
		{
			if (Comets.Count > 1)
			{
				OVComet comet = SelectedComet;

				Comets.Clear();
				CometOrbits.Clear();

				Comets.Add(comet);
				CometOrbits.Add(new CometOrbit(comet, CometOrbit.MaxDivisions));

				SelectedIndex = 0;

				UpdatePositions(ATime);
				UpdatePlanetOrbit(ATime);
				UpdateRotationMatrix(ATime);
			}
		}

		#endregion

		#region SelectComet

		public string SelectComet(Point point)
		{
			string name = null;

			if (MultipleMode && Comets.Count > 1)
			{
				int offset = 5;
				int range = 7;

				int x0 = point.X - offset;
				int y0 = point.Y - offset;

				List<Point> points = new List<Point>();

				for (int x = 0; x < range; x++)
					for (int y = 0; y < range; y++)
						points.Add(new Point(x0 + x, y0 + y));

				OVComet comet = Comets.FirstOrDefault(c => c.PanelLocation == points.FirstOrDefault(p => p == c.PanelLocation));

				if (comet != null)
					name = comet.Name;
			}

			return name;
		}

		#endregion

		#region GetMagnutideAndDistances

		private double[] GetMagnutideAndDistances(OVComet comet, int index)
		{
			double m, d, r;
			double xdiff, ydiff, zdiff;

			Xyz xyz = CometsPos[index].Rotate(MtxToEcl).Rotate(MtxRotate);

			Xyz xyz1 = PlanetPos[2].Rotate(MtxRotate);
			r = Math.Sqrt((xyz.X * xyz.X) + (xyz.Y * xyz.Y) + (xyz.Z * xyz.Z)) + .0005;
			xdiff = xyz.X - xyz1.X;
			ydiff = xyz.Y - xyz1.Y;
			zdiff = xyz.Z - xyz1.Z;
			d = Math.Sqrt((xdiff * xdiff) + (ydiff * ydiff) + (zdiff * zdiff)) + .0005;

			m = comet.g + 5 * Math.Log10(d) + 2.5 * comet.k * Math.Log10(r);

			return new double[] { m, d, r };
		}

		#endregion

		#region GetCometColorAndDiameter

		private Color GetCometColorAndDiameter(OVComet comet, int index, out int diameter)
		{
			Color color;

			double mag = GetMagnutideAndDistances(comet, index)[0];

			if (mag < 2)
			{
				diameter = 5;
				color = Color.White;
			}
			else if (mag >= 2 && mag < 5)
			{
				diameter = 4;
				color = Color.White;
			}
			else if (mag >= 5 && mag < 8)
			{
				diameter = 4;
				color = Color.Silver;
			}
			else if (mag >= 8 && mag < 11)
			{
				diameter = 3;
				color = Color.Silver;
			}
			else if (mag >= 11 && mag < 15)
			{
				diameter = 2;
				color = Color.DarkGray;
			}
			else
			{
				diameter = 2;
				color = Color.DimGray;
			}

			return color;
		}

		#endregion

		#endregion
	}
}