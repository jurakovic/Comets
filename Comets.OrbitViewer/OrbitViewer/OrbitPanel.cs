using Comets.Core;
using Comets.Core.Managers;
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
		#region Const

		public const int MaximumOrbits = 50;
		private const bool DefaultShowMarker = true;
		private const bool DefaultPreserveOrbit = true;
		private const bool DefaultPreserveLabel = true;
		private const bool DefaultShowDistance = true;
		private const bool DefaultShowDate = true;
		private const bool DefaultFilterOnDateShowInWeakColor = true;
		private const Object DefaultCenterObject = Object.Sun;

		private readonly List<Object> DefaultOrbitDisplay = new List<Object>
		{
			Object.Mercury,
			Object.Venus,
			Object.Earth,
			Object.Mars,
			Object.Jupiter
		};

		private readonly List<Object> Planets = new List<Object>
		{
			Object.Mercury,
			Object.Venus,
			Object.Earth,
			Object.Mars,
			Object.Jupiter,
			Object.Saturn,
			Object.Uranus,
			Object.Neptune
		};

		#endregion

		#region Colors

		//https://msdn.microsoft.com/en-us/library/aa358803%28v=vs.85%29.aspx
		protected Color ColorCometOrbitUpper = Color.Tomato;
		protected Color ColorCometOrbitLower = Color.Firebrick;
		protected Color ColorSelectedCometOrbitUpper = Color.Gold;
		protected Color ColorSelectedCometOrbitLower = Color.DarkOrange;
		protected Color ColorSelectedCometMarker = Color.Red;
		protected Color ColorMarkedCometMarker = Color.DeepSkyBlue;
		protected Color ColorCometNameSelected = Color.White;
		protected Color ColorCometName = Color.Peru;
		protected Color ColorPlanetOrbitUpper = Color.SteelBlue;
		protected Color ColorPlanetOrbitLower = Color.DarkSlateBlue;
		protected Color ColorPlanet = Color.Lime;
		protected Color ColorPlanetName = Color.LimeGreen;
		protected Color ColorSun = Color.Orange;
		protected Color ColorAxisPlus = Color.Yellow;
		protected Color ColorAxisMinus = Color.DarkOliveGreen;
		protected Color ColorInformation = Color.White;
		protected Color FilterOnDateWeakColorComet = Color.FromArgb(25, 25, 70);
		protected Color FilterOnDateWeakColorOrbit = Color.FromArgb(25, 25, 50);

		#endregion

		#region Fonts

		protected Font FontObjectName = new Font("Helvetica", 10, FontStyle.Regular);
		protected Font FontPlanetName = new Font("Helvetica", 10, FontStyle.Regular);
		protected Font FontInformation = new Font("Helvetica", 10, FontStyle.Bold);
		protected Font FontAxisLabel = new Font("Helvetica", 8.5F, FontStyle.Regular);

		#endregion

		#region Fields

		private int SelectedIndex;
		private int CenteredIndex;

		private List<CometOrbit> CometOrbits;
		private List<Xyz> CometsPos;

		private Dictionary<Object, PlanetOrbit> PlanetsOrbit;
		private Dictionary<Object, Xyz> PlanetsPos;
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
			get { return Comets.ElementAtOrDefault(SelectedIndex); }
		}

		private ATime _atime;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ATime ATime
		{
			get { return this._atime; }
			set
			{
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
		public Image Image { get; set; }


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
		public bool Antialiasing { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? FilterOnDateSunDist { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? FilterOnDateEarthDist { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? FilterOnDateMagnitude { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool FilterOnDateShowInWeakColor { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<OVComet> MarkedComets
		{
			get { return Comets.Where(x => x.IsMarked); }
		}

		#endregion

		#region Consctructor

		public OrbitPanel()
		{
			this.DoubleBuffered = true;

			PlanetsPos = InitializeDictionary<Xyz>();
			PlanetsOrbit = InitializeDictionary<PlanetOrbit>();

			Comets = new List<OVComet>();
			CometOrbits = new List<CometOrbit>();
			CometsPos = new List<Xyz>();

			OrbitDisplay = DefaultOrbitDisplay;
			LabelDisplay = Planets.ToList(); //DefaultLabelDisplay
			CenteredObject = DefaultCenterObject;
			ShowMarker = DefaultShowMarker;
			PreserveSelectedOrbit = DefaultPreserveOrbit;
			PreserveSelectedLabel = DefaultPreserveLabel;
			FilterOnDateShowInWeakColor = DefaultFilterOnDateShowInWeakColor;

			ShowDistance = DefaultShowDistance;
			ShowDate = DefaultShowDate;

			Image = null;
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
				CometOrbits.Add(new CometOrbit(comet));
			}

			SelectedIndex = Comets.IndexOf(comet);

			ATime = atime;
			UpdatePlanetOrbit(atime);
			UpdateRotationMatrix(atime);
		}

		public void LoadPanel(List<OVComet> comets, ATime atime, int index)
		{
			List<OVComet> marked = MarkedComets.ToList();

			IsPaintEnabled = true;
			MultipleMode = true;

			Comets.Clear();
			CometOrbits.Clear();

			Comets = comets;

			int ix = 0;
			foreach (OVComet c in Comets)
			{
				CometOrbits.Add(new CometOrbit(c));

				if (c.IsMarked && !marked.Contains(c))
					c.IsMarked = false;

				ix++;
			}

			SelectedIndex = index;
			CenteredIndex = index;

			ATime = atime;
			UpdatePlanetOrbit(atime);
			UpdateRotationMatrix(atime);
		}

		#endregion

		#region OnPaint

		protected override void OnPaint(PaintEventArgs e)
		{
			if (IsPaintEnabled)
			{
				if (Image == null)
					Image = new Bitmap(Size.Width, Size.Height);

				Update(e.Graphics);
			}
		}

		#endregion

		#region Update

		public void Update(Graphics g)
		{
			Point point;
			Xyz xyz;

			// Calculate Drawing Parameter
			Matrix mtxRotH = Matrix.RotateZ(RotateHorz * Math.PI / 180.0);
			Matrix mtxRotV = Matrix.RotateX(RotateVert * Math.PI / 180.0);
			MtxRotate = mtxRotV.Mul(mtxRotH);

			X0 = Size.Width / 2;
			Y0 = Size.Height / 2;

			if (Math.Abs(EpochToEcl - ATime.JD) > 365.2422 * 5)
				UpdateRotationMatrix(ATime);

			if (CenteredObject != Object.Comet)
				CenteredIndex = -1;

			if (CenteredObject == Object.Comet)
			{
				if (CenteredIndex == -1)
					CenteredIndex = SelectedIndex;

				if (CenteredIndex >= 0)
				{
					xyz = CometsPos[CenteredIndex].Rotate(MtxToEcl).Rotate(MtxRotate);
					point = GetDrawPoint(xyz);

					X0 = Size.Width - point.X;
					Y0 = Size.Height - point.Y;
				}
			}
			else if (Planets.Contains(CenteredObject))
			{
				xyz = PlanetsPos[CenteredObject].Rotate(MtxRotate);
				point = GetDrawPoint(xyz);

				X0 = Size.Width - point.X;
				Y0 = Size.Height - point.Y;
			}

			using (Graphics graphics = Graphics.FromImage(Image))
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

				if (Zoom * 30.1 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Neptune]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Neptune], Object.Neptune);
				}

				if (Zoom * 19.2 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Uranus]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Uranus], Object.Uranus);
				}

				if (Zoom * 9.58 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Saturn]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Saturn], Object.Saturn);
				}

				if (Zoom * 5.2 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Jupiter]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Jupiter], Object.Jupiter);
				}

				if (Zoom * 1.524 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Mars]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Mars], Object.Mars);
				}

				if (Zoom * 1.0 >= zoom)
				{
					DrawEarthOrbit(graphics, PlanetsOrbit[Object.Earth]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Earth], Object.Earth);
				}

				if (Zoom * 0.723 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Venus]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Venus], Object.Venus);
				}

				if (Zoom * 0.387 >= zoom)
				{
					DrawPlanetOrbit(graphics, PlanetsOrbit[Object.Mercury]);
					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[Object.Mercury], Object.Mercury);
				}

				DrawCometOrbit(graphics);
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
						Ephemeris ep = GetEphemeris(SelectedComet);

						string mstr = String.Format("Magnitude:       {0:#0.00}", ep.Magnitude);
						string dstr = String.Format("Earth Distance: {0:#0.000000} AU", ep.EarthDist);
						string rstr = String.Format("Sun Distance:   {0:#0.000000} AU", ep.SunDist);

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
					string strDate = String.Format("{0:00} {1} {2} {3:00}:{4:00}:{5:00}", ATime.Day, ATime.MonthString, ATime.Year, ATime.Hour, ATime.Minute, ATime.Second);
					point1.X = Size.Width - (int)graphics.MeasureString(strDate, FontInformation).Width - labelMargin;
					point1.Y = Size.Height - labelMargin - (int)(fontSize * 2.0);
					graphics.DrawString(strDate, FontInformation, sb, point1.X, point1.Y);
				}
			}

			g.DrawImage(Image, 0, 0);
		}

		#endregion

		#region + Methods

		#region InitializeDictionary

		private Dictionary<Object, T> InitializeDictionary<T>() where T : class
		{
			Dictionary<Object, T> retval = new Dictionary<Object, T>();
			Planets.ForEach(planet => retval.Add(planet, null));
			return retval;
		}

		#endregion

		#region UpdatePositions

		private void UpdatePositions(ATime atime)
		{
			if (IsPaintEnabled)
			{
				CometsPos.Clear();

				Comets.ForEach(c => CometsPos.Add(c.GetPos(atime.JD)));
				Planets.ForEach(p => PlanetsPos[p] = Planet.GetPos(p, atime));

				UpdateCometVisibility();
			}
		}

		#endregion

		#region UpdateCometVisibility

		public void UpdateCometVisibility()
		{
			Comets.ForEach(c => c.IsVisible = GetCometVisibility(c, FilterOnDateSunDist, FilterOnDateEarthDist, FilterOnDateMagnitude));
		}

		#endregion

		#region UpdatePlanetOrbit

		private void UpdatePlanetOrbit(ATime atime)
		{
			Planets.ForEach(p => PlanetsOrbit[p] = new PlanetOrbit(p, atime));
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
			graphics.SmoothingMode = Antialiasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

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

		private void DrawCometOrbit(Graphics graphics)
		{
			graphics.SmoothingMode = Antialiasing ? SmoothingMode.AntiAlias : SmoothingMode.None;
			int markedCount = MarkedComets.Count();

			for (int i = 0; i < Comets.Count; i++)
			{
				bool visibleComet = Comets[i].IsVisible; //GetCometVisibility(Comets[i], FilterOnDateSunDist, FilterOnDateEarthDist, FilterOnDateMagnitude);
				bool visibleSelected = PreserveSelectedOrbit && i == SelectedIndex;
				bool visibleOrbit = OrbitDisplay.Contains(Object.Comet);
				bool isCometMarked = Comets[i].IsMarked;

				bool useWeakColor = false;
				bool useSelectedColor = visibleSelected && MultipleMode &&
					((markedCount > 0 && !isCometMarked) || (markedCount > 1 && isCometMarked));

				if (!visibleComet)
				{
					useWeakColor = FilterOnDateShowInWeakColor && !visibleSelected && !isCometMarked;

					//if (!FilterOnDateShowInWeakColor)
					//	visibleOrbit = visibleSelected;
				}

				if (/*visibleOrbit ||*/ visibleSelected || isCometMarked)
				{
					Xyz xyz = CometOrbits[i].GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);
					Pen pen = new Pen(Color.White);
					Point point1, point2;
					point1 = GetDrawPoint(xyz);

					for (int j = 1; j <= CometOrbit.OrbitDivisionCount; j++)
					{
						xyz = CometOrbits[i].GetAt(j).Rotate(MtxToEcl);

						if (useWeakColor)
							pen.Color = FilterOnDateWeakColorOrbit;
						else if (useSelectedColor)
							pen.Color = xyz.Z >= 0.0 ? ColorSelectedCometOrbitUpper : ColorSelectedCometOrbitLower;
						else
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

			for (int i = 0; i < Comets.Count; i++)
			{
				Xyz xyz = CometsPos[i].Rotate(MtxToEcl).Rotate(MtxRotate);

				Point point1 = GetDrawPoint(xyz);
				Comets[i].PanelLocation = point1;

				int diameter = 2;
				Color color = Color.Black;

				bool visibleComet = Comets[i].IsVisible;
				bool visibleSelected = PreserveSelectedLabel && i == SelectedIndex;
				bool visibleLabel = LabelDisplay.Contains(Object.Comet);
				bool visibleMarker = ShowMarker && i == SelectedIndex;
				bool isCometMarked = Comets[i].IsMarked;

				GetCometColorAndDiameter(Comets[i], out diameter, out color);

				if (!visibleComet)
				{
					color = FilterOnDateWeakColorComet;

					if (FilterOnDateShowInWeakColor)
					{
						visibleComet = true;

						if (!visibleSelected)
							visibleLabel = false;
					}
				}

				SolidBrush sb = new SolidBrush(color);

				if (visibleComet || visibleSelected || isCometMarked)
				{
					graphics.FillPie(sb, point1.X - diameter, point1.Y - diameter, diameter * 2, diameter * 2, 0, 360);

					if (visibleLabel || visibleSelected || isCometMarked)
					{
						if (MultipleMode && Comets.Count > 1 && visibleLabel && visibleSelected)
							sb.Color = ColorCometNameSelected;
						else
							sb.Color = ColorCometName;

						graphics.DrawString(Comets[i].Name, FontObjectName, sb, point1.X + 5, point1.Y);
					}
				}

				if (visibleMarker || isCometMarked)
				{
					int offset = diameter + 4;
					int length = diameter + 8;

					Color cmarker = ColorSelectedCometMarker;

					if (isCometMarked)
						cmarker = ColorMarkedCometMarker;

					Pen p = new Pen(cmarker) { Width = 3 };

					graphics.DrawLine(p, new Point(point1.X, point1.Y - length), new Point(point1.X, point1.Y - offset));
					graphics.DrawLine(p, new Point(point1.X, point1.Y + length), new Point(point1.X, point1.Y + offset));
					graphics.DrawLine(p, new Point(point1.X - length, point1.Y), new Point(point1.X - offset, point1.Y));
					graphics.DrawLine(p, new Point(point1.X + length, point1.Y), new Point(point1.X + offset, point1.Y));
				}
			}
		}

		#endregion

		#region DrawPlanetOrbit

		private void DrawPlanetOrbit(Graphics graphics, PlanetOrbit planetOrbit)
		{
			if (this.OrbitDisplay.Contains(planetOrbit.Planet))
			{
				graphics.SmoothingMode = Antialiasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

				Pen pen = new Pen(ColorPlanetOrbitUpper);
				Point point1, point2;
				Xyz xyz = planetOrbit.GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);

				point1 = GetDrawPoint(xyz);

				for (int i = 1; i <= PlanetOrbit.OrbitDivisionCount; i++)
				{
					xyz = planetOrbit.GetAt(i).Rotate(MtxToEcl);

					pen.Color = xyz.Z >= 0.0 ? ColorPlanetOrbitUpper : ColorPlanetOrbitLower;

					xyz = xyz.Rotate(MtxRotate);
					point2 = GetDrawPoint(xyz);
					graphics.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
					point1 = point2;
				}
			}
		}

		#endregion

		#region DrawEarthOrbit

		private void DrawEarthOrbit(Graphics graphics, PlanetOrbit planetOrbit)
		{
			graphics.SmoothingMode = Antialiasing ? SmoothingMode.AntiAlias : SmoothingMode.None;

			Pen pen = new Pen(ColorPlanetOrbitUpper);
			Point point1, point2;
			Xyz xyz = planetOrbit.GetAt(0).Rotate(MtxToEcl).Rotate(MtxRotate);

			point1 = GetDrawPoint(xyz);

			for (int i = 1; i <= PlanetOrbit.OrbitDivisionCount; i++)
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

			if (this.LabelDisplay.Contains(obj))
			{
				sb.Color = ColorPlanetName;
				string name = obj.ToString();
				graphics.DrawString(name, font, sb, point.X + 5, point.Y);
			}
		}

		#endregion

		#region ClearComets

		public void ClearComets(bool clearAll = false)
		{
			OVComet comet = SelectedComet;

			Comets.Clear();
			CometOrbits.Clear();
			SelectedIndex = -1;
			CenteredIndex = -1;

			if (!clearAll && comet != null)
			{
				Comets.Add(comet);
				CometOrbits.Add(new CometOrbit(comet));

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
			int range = 5;
			int minX = point.X - range;
			int minY = point.Y - range;
			int maxX = point.X + range;
			int maxY = point.Y + range;

			OVComet comet = Comets.FirstOrDefault(c =>
				(c.IsVisible || c.IsMarked || FilterOnDateShowInWeakColor) &&
				c.PanelLocation.X >= minX && c.PanelLocation.X <= maxX &&
				c.PanelLocation.Y >= minY && c.PanelLocation.Y <= maxY);

			return comet?.Name;
		}

		#endregion

		#region CenterSelectedComet

		public bool CenterSelectedComet()
		{
			bool isCentered = false;

			int index = Comets.IndexOf(SelectedComet);

			isCentered = CenteredIndex != index;
			CenteredIndex = index;

			return isCentered;
		}

		#endregion

		#region GetEphemeris

		private Ephemeris GetEphemeris(OVComet comet)
		{
			decimal T = comet.T;
			double e = comet.e;
			double q = comet.q;
			double w = comet.w / (Math.PI / 180.0);
			double N = comet.N / (Math.PI / 180.0);
			double i = comet.i / (Math.PI / 180.0);
			double g = comet.g;
			double k = comet.k;
			decimal jd = Convert.ToDecimal(ATime.JD);

			return EphemerisManager.GetEphemeris(T, q, e, w, N, i, g, k, jd, 0.0, 0.0);
		}

		#endregion

		#region GetCometColorAndDiameter

		private void GetCometColorAndDiameter(OVComet comet, out int diameter, out Color color)
		{
			double mag = GetEphemeris(comet).Magnitude;

			if (mag < 0)
			{
				diameter = 6;
				color = Color.White;
			}
			else if (mag >= 0 && mag < 2)
			{
				diameter = 5;
				color = Color.White;
			}
			else if (mag >= 2 && mag < 3)
			{
				diameter = 4;
				color = Color.White;
			}
			else if (mag >= 3 && mag < 5)
			{
				diameter = 4;
				color = Color.Silver;
			}
			else if (mag >= 5 && mag < 8)
			{
				diameter = 3;
				color = Color.Silver;
			}
			else if (mag >= 8 && mag < 12)
			{
				diameter = 2;
				color = Color.DarkGray;
			}
			else
			{
				diameter = 2;
				color = Color.FromArgb(70, 70, 70);
			}
		}

		#endregion

		#region GetCometVisibility

		private bool GetCometVisibility(OVComet comet, double? r, double? dist, double? mag)
		{
			bool retval = true;

			bool check = r != null || dist != null || mag != null;

			if (check)
			{
				Ephemeris ep = GetEphemeris(comet);

				List<bool> checks = new List<bool>();

				if (r != null)
					checks.Add(ep.SunDist <= r.Value);

				if (dist != null)
					checks.Add(ep.EarthDist <= dist.Value);

				if (mag != null)
					checks.Add(ep.Magnitude <= mag.Value);

				retval = checks.All(x => x);
			}

			return retval;
		}

		#endregion

		#endregion
	}
}