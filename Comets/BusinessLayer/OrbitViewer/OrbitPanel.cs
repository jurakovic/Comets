using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
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
			Mercury = 1,
			Venus = 2,
			Earth = 3,
			Mars = 4,
			Jupiter = 5,
			Saturn = 6,
			Uranus = 7,
			Neptune = 8,
			Pluto = 9,
			Comet = 10
		}

		#endregion

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

		private readonly List<Object> DefaultLabelDisplay = new List<Object>
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

		private List<int> OrbitHistory;

		private PlanetOrbit[] PlanetsOrbit;
		private Xyz[] PlanetsPos;
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

			PlanetsPos = new Xyz[8];
			PlanetsOrbit = new PlanetOrbit[8];

			Comets = new List<OVComet>();
			CometOrbits = new List<CometOrbit>();
			CometsPos = new List<Xyz>();

			OrbitHistory = new List<int>();

			OrbitDisplay = DefaultOrbitDisplay;
			LabelDisplay = DefaultLabelDisplay;
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
			OrbitHistoryAdd(SelectedIndex);

			ATime = atime;

			//UpdatePositions(atime);
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
			OrbitHistory.Clear();

			Comets = comets;

			int ix = 0;
			foreach (OVComet c in Comets)
			{
				CometOrbits.Add(new CometOrbit(c));
				OrbitHistoryAdd(ix);

				if (c.IsMarked && !marked.Contains(c))
					c.IsMarked = false;

				ix++;
			}

			SelectedIndex = index;
			CenteredIndex = index;
			OrbitHistoryAdd(SelectedIndex);

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
			else if (CenteredObject >= Object.Mercury)
			{
				xyz = PlanetsPos[(int)CenteredObject - 1].Rotate(MtxRotate);
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

				//if (Zoom * 39.5 >= zoom)
				//{
				//	if (OrbitDisplay[(int)OrbitDisplayEnum.Pluto])
				//		DrawPlanetOrbit(graphics, PlanetOrbit[(int)Object.Pluto - 1]);

				//	DrawPlanetBody(graphics, FontPlanetName, PlanetPos[(int)Object.Pluto - 1], Object.Pluto);
				//}

				if (Zoom * 30.1 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Neptune))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Neptune - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Neptune - 1], Object.Neptune);
				}

				if (Zoom * 19.2 >= zoom)
				{

					if (OrbitDisplay.Contains(Object.Uranus))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Uranus - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Uranus - 1], Object.Uranus);
				}

				if (Zoom * 9.58 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Saturn))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Saturn - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Saturn - 1], Object.Saturn);
				}

				if (Zoom * 5.2 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Jupiter))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Jupiter - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Jupiter - 1], Object.Jupiter);
				}

				if (Zoom * 1.524 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Mars))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Mars - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Mars - 1], Object.Mars);
				}

				if (Zoom * 1.0 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Earth))
						DrawEarthOrbit(graphics, PlanetsOrbit[(int)Object.Earth - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Earth - 1], Object.Earth);
				}

				if (Zoom * 0.723 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Venus))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Venus - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Venus - 1], Object.Venus);
				}

				if (Zoom * 0.387 >= zoom)
				{
					if (OrbitDisplay.Contains(Object.Mercury))
						DrawPlanetOrbit(graphics, PlanetsOrbit[(int)Object.Mercury - 1]);

					DrawPlanetBody(graphics, FontPlanetName, PlanetsPos[(int)Object.Mercury - 1], Object.Mercury);
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

		#region UpdatePositions

		private void UpdatePositions(ATime atime)
		{
			if (IsPaintEnabled)
			{
				CometsPos.Clear();
				foreach (OVComet c in Comets)
					CometsPos.Add(c.GetPos(atime.JD));

				for (int i = (int)Object.Mercury; i <= (int)Object.Neptune; i++)
				{
					PlanetsPos[i - 1] = Planet.GetPos(i, atime);
				}
			}
		}

		#endregion

		#region UpdatePlanetOrbit

		private void UpdatePlanetOrbit(ATime atime)
		{
			for (int i = (int)Object.Mercury; i <= (int)Object.Neptune; i++)
			{
				PlanetsOrbit[i - 1] = new PlanetOrbit(i, atime);
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
				bool visibleComet = GetCometVisibility(Comets[i], FilterOnDateSunDist, FilterOnDateEarthDist, FilterOnDateMagnitude);
				bool visibleSelected = PreserveSelectedOrbit && i == SelectedIndex;
				bool visibleOrbit = OrbitDisplay.Contains(Object.Comet) && OrbitHistory.Contains(i);
				bool isCometMarked = Comets[i].IsMarked;

				bool useWeakColor = false;
				bool useSelectedColor = visibleSelected &&
					((OrbitDisplay.Contains(Object.Comet) && MultipleMode && Comets.Count > 1) ||
					(!OrbitDisplay.Contains(Object.Comet) && (markedCount > 0 && !isCometMarked) || (markedCount > 1 && isCometMarked)));

				if (!visibleComet)
				{
					useWeakColor = FilterOnDateShowInWeakColor && !visibleSelected && !isCometMarked;

					if (!FilterOnDateShowInWeakColor)
						visibleOrbit = visibleSelected;
				}

				if (visibleOrbit || visibleSelected || isCometMarked)
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

				bool visibleComet = GetCometVisibility(Comets[i], FilterOnDateSunDist, FilterOnDateEarthDist, FilterOnDateMagnitude);
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

					Pen p = new Pen(cmarker);
					p.Width = 3;

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

			if (LabelDisplay.Contains(obj))
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
			OrbitHistory.Clear();
			SelectedIndex = -1;
			CenteredIndex = -1;

			if (!clearAll && comet != null)
			{
				Comets.Add(comet);
				CometOrbits.Add(new CometOrbit(comet));

				SelectedIndex = 0;
				OrbitHistoryAdd(SelectedIndex);

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

			int range = 5;
			int minX = point.X - range;
			int minY = point.Y - range;
			int maxX = point.X + range;
			int maxY = point.Y + range;

			OVComet comet = Comets.FirstOrDefault(c =>
				c.PanelLocation.X >= minX && c.PanelLocation.X <= maxX &&
				c.PanelLocation.Y >= minY && c.PanelLocation.Y <= maxY);

			if (comet != null)
				name = comet.Name;

			return name;
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
			double T = comet.T;
			double e = comet.e;
			double q = comet.q;
			double w = comet.w / (Math.PI / 180.0);
			double N = comet.N / (Math.PI / 180.0);
			double i = comet.i / (Math.PI / 180.0);
			double g = comet.g;
			double k = comet.k;

			return EphemerisManager.GetEphemeris(T, q, e, w, N, i, g, k, ATime.JD, 0.0, 0.0);
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

		#region OrbitHistoryAdd

		private void OrbitHistoryAdd(int index)
		{
			if (OrbitHistory.Contains(index))
				OrbitHistory.Remove(index);

			OrbitHistory.Add(index);

			if (OrbitHistory.Count > MaximumOrbits)
				OrbitHistory.RemoveAt(0);
		}

		#endregion

		#endregion
	}
}