using Comets.Core;
using Comets.Core.Managers;
using OpenTK;
using OpenTK.WinForms;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Comets.OrbitViewer
{
	public class OrbitPanel : GLControl
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

		private const string VertexShaderSource = @"
#version 330 core
layout (location = 0) in vec3 aPos;
uniform mat4 uRot;
uniform float uHalfX;
uniform float uHalfY;
out float vZ;
void main()
{
    vZ = aPos.z;
    vec4 v = uRot * vec4(aPos, 1.0);
    float w = 1.0 + v.z / 625.0;
    gl_Position = vec4(v.x / uHalfX, v.y / uHalfY, 0.0, w);
}";

		private const string FragmentShaderSource = @"
#version 330 core
in float vZ;
uniform vec4 uColorUpper;
uniform vec4 uColorLower;
uniform int uMode; // 0 = orbit line, 1 = body dot
out vec4 FragColor;
void main()
{
    if (uMode == 1)
    {
        vec2 c = gl_PointCoord - vec2(0.5);
        if (dot(c, c) > 0.25) discard;
        FragColor = uColorUpper;
    }
    else
    {
        FragColor = vZ >= 0.0 ? uColorUpper : uColorLower;
    }
}";

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
		protected Font FontInformation = new Font("Consolas", 10, FontStyle.Bold);
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

		// GL rendering
		private int _shaderProgram = 0;
		private int _uRot;
		private int _uHalfX;
		private int _uHalfY;
		private int _uColorUpper;
		private int _uColorLower;
		private int _uMode;
		private int _bodyVao = 0;
		private int _bodyVbo = 0;
		private Dictionary<Object, (int vao, int vbo, int count)> _planetOrbitBuffers;
		private List<(int vao, int vbo, int count)> _cometOrbitBuffers = new List<(int, int, int)>();
		private bool _vbosNeedUpdate = false;

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

		private bool _glLoaded = false;

		public OrbitPanel()
		{
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

		#region OnPaint / OnResize

		protected override void OnPaint(PaintEventArgs e)
		{
			MakeCurrent();

			if (!_glLoaded)
				InitGL();

			// Keep MtxRotate/X0/Y0 current for click-detection (SelectComet uses PanelLocation)
			Matrix mtxRotH = Matrix.RotateZ(RotateHorz * Math.PI / 180.0);
			Matrix mtxRotV = Matrix.RotateX(RotateVert * Math.PI / 180.0);
			MtxRotate = mtxRotV.Mul(mtxRotH);
			X0 = Width / 2;
			Y0 = Height / 2;

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			RenderScene();
			SwapBuffers();
			UpdateCometPanelLocations();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (_glLoaded)
			{
				MakeCurrent();
				GL.Viewport(0, 0, Width, Height);
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

						string mstr = String.Format("Magnitude:      {0,5}", ep.Magnitude.ToString("0.00"));
						string dstr = String.Format("Earth Distance: {0,9} AU", ep.EarthDist.ToString("0.000000"));
						string rstr = String.Format("Sun Distance:   {0,9} AU", ep.SunDist.ToString("0.000000"));

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
					string strDate = String.Format("{0:00} {1} {2} {3:00}:{4:00}:{5:00} UT", ATime.Day, ATime.MonthString, ATime.Year, ATime.Hour, ATime.Minute, ATime.Second);
					point1.X = Size.Width - (int)graphics.MeasureString(strDate, FontInformation).Width - labelMargin;
					point1.Y = Size.Height - labelMargin - (int)(fontSize * 2.0);
					graphics.DrawString(strDate, FontInformation, sb, point1.X, point1.Y);
				}
			}

			g.DrawImage(Image, 0, 0);
		}

		#endregion

		#region GL

		private void InitGL()
		{
			GL.ClearColor(0f, 0f, 0f, 1f);
			GL.Disable(EnableCap.DepthTest);
			GL.Viewport(0, 0, Width, Height);

			int vs = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vs, VertexShaderSource);
			GL.CompileShader(vs);

			int fs = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fs, FragmentShaderSource);
			GL.CompileShader(fs);

			_shaderProgram = GL.CreateProgram();
			GL.AttachShader(_shaderProgram, vs);
			GL.AttachShader(_shaderProgram, fs);
			GL.LinkProgram(_shaderProgram);
			GL.DeleteShader(vs);
			GL.DeleteShader(fs);

			_uRot    = GL.GetUniformLocation(_shaderProgram, "uRot");
			_uHalfX  = GL.GetUniformLocation(_shaderProgram, "uHalfX");
			_uHalfY  = GL.GetUniformLocation(_shaderProgram, "uHalfY");
			_uColorUpper = GL.GetUniformLocation(_shaderProgram, "uColorUpper");
			_uColorLower = GL.GetUniformLocation(_shaderProgram, "uColorLower");
			_uMode   = GL.GetUniformLocation(_shaderProgram, "uMode");

			_planetOrbitBuffers = new Dictionary<Object, (int, int, int)>();
			foreach (Object planet in Planets)
				_planetOrbitBuffers[planet] = (0, 0, 0);

			// Body dot VAO/VBO (positions uploaded per frame)
			_bodyVao = GL.GenVertexArray();
			_bodyVbo = GL.GenBuffer();
			GL.BindVertexArray(_bodyVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _bodyVbo);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			GL.BindVertexArray(0);

			_glLoaded = true;
			_vbosNeedUpdate = true;
		}

		private void UploadOrbitsToGpu()
		{
			if (MtxToEcl == null || !IsPaintEnabled)
			{
				_vbosNeedUpdate = false;
				return;
			}

			foreach (Object planet in Planets)
			{
				if (PlanetsOrbit[planet] == null)
					continue;

				PlanetOrbit orbit = PlanetsOrbit[planet];
				int n = PlanetOrbit.OrbitDivisionCount + 1;
				float[] verts = new float[n * 3];

				for (int i = 0; i <= PlanetOrbit.OrbitDivisionCount; i++)
				{
					Xyz p = orbit.GetAt(i).Rotate(MtxToEcl);
					verts[i * 3]     = (float)p.X;
					verts[i * 3 + 1] = (float)p.Y;
					verts[i * 3 + 2] = (float)p.Z;
				}

				var (vao, vbo, _) = _planetOrbitBuffers[planet];
				if (vao == 0) { vao = GL.GenVertexArray(); vbo = GL.GenBuffer(); }

				GL.BindVertexArray(vao);
				GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
				GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
				GL.BindVertexArray(0);

				_planetOrbitBuffers[planet] = (vao, vbo, n);
			}

			foreach (var (vao, vbo, _) in _cometOrbitBuffers)
			{
				if (vao != 0) GL.DeleteVertexArray(vao);
				if (vbo != 0) GL.DeleteBuffer(vbo);
			}
			_cometOrbitBuffers.Clear();

			for (int i = 0; i < CometOrbits.Count; i++)
			{
				int n = CometOrbit.OrbitDivisionCount + 1;
				float[] verts = new float[n * 3];

				for (int j = 0; j <= CometOrbit.OrbitDivisionCount; j++)
				{
					Xyz p = CometOrbits[i].GetAt(j).Rotate(MtxToEcl);
					verts[j * 3]     = (float)p.X;
					verts[j * 3 + 1] = (float)p.Y;
					verts[j * 3 + 2] = (float)p.Z;
				}

				int vao = GL.GenVertexArray();
				int vbo = GL.GenBuffer();

				GL.BindVertexArray(vao);
				GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
				GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
				GL.BindVertexArray(0);

				_cometOrbitBuffers.Add((vao, vbo, n));
			}

			_vbosNeedUpdate = false;
		}

		private void RenderScene()
		{
			if (!IsPaintEnabled || _shaderProgram == 0)
				return;

			if (_vbosNeedUpdate)
				UploadOrbitsToGpu();

			// Build view rotation: matches original MtxRotate = RotateX(RotateVert) * RotateZ(RotateHorz)
			// OpenTK row-major convention: GLSL sees (rotH*rotV)^T = rotV^T * rotH^T = MtxRotate
			Matrix4 rotH = Matrix4.CreateRotationZ((float)(RotateHorz * Math.PI / 180.0));
			Matrix4 rotV = Matrix4.CreateRotationX((float)(RotateVert * Math.PI / 180.0));
			Matrix4 rot = rotH * rotV;

			// Scale matching original: mul = Zoom*682 / (1500*(1+Z/625))
			// Shader applies weak perspective via gl_Position.w = 1 + Z_view/625
			float halfX = (float)(Width  * 750.0 / (Zoom * 682.0));
			float halfY = (float)(Height * 750.0 / (Zoom * 682.0));

			GL.UseProgram(_shaderProgram);
			GL.UniformMatrix4(_uRot, false, ref rot);
			GL.Uniform1(_uHalfX, halfX);
			GL.Uniform1(_uHalfY, halfY);

			// Planet orbits
			foreach (Object planet in Planets)
			{
				if (!OrbitDisplay.Contains(planet) || !_planetOrbitBuffers.ContainsKey(planet))
					continue;

				if (Zoom * GetPlanetAU(planet) < 30.0)
					continue;

				var (vao, _, count) = _planetOrbitBuffers[planet];
				if (vao == 0 || count == 0) continue;

				// Earth orbit has no upper/lower colour split in the original
				Color upper = ColorPlanetOrbitUpper;
				Color lower = planet == Object.Earth ? ColorPlanetOrbitUpper : ColorPlanetOrbitLower;

				GL.Uniform4(_uColorUpper, upper.R / 255f, upper.G / 255f, upper.B / 255f, 1f);
				GL.Uniform4(_uColorLower, lower.R / 255f, lower.G / 255f, lower.B / 255f, 1f);

				GL.BindVertexArray(vao);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, count);
			}

			// Comet orbits
			int markedCount = MarkedComets.Count();

			for (int i = 0; i < Comets.Count && i < _cometOrbitBuffers.Count; i++)
			{
				bool visibleSelected = PreserveSelectedOrbit && i == SelectedIndex;
				bool isCometMarked = Comets[i].IsMarked;

				if (!visibleSelected && !isCometMarked) continue;

				bool visibleComet = Comets[i].IsVisible;
				bool useWeakColor = !visibleComet && FilterOnDateShowInWeakColor && !visibleSelected && !isCometMarked;
				bool useSelectedColor = visibleSelected && MultipleMode &&
					((markedCount > 0 && !isCometMarked) || (markedCount > 1 && isCometMarked));

				var (vao, _, count) = _cometOrbitBuffers[i];
				if (vao == 0 || count == 0) continue;

				Color upper, lower;
				if (useWeakColor)
					upper = lower = FilterOnDateWeakColorOrbit;
				else if (useSelectedColor)
					{ upper = ColorSelectedCometOrbitUpper; lower = ColorSelectedCometOrbitLower; }
				else
					{ upper = ColorCometOrbitUpper; lower = ColorCometOrbitLower; }

				GL.Uniform4(_uColorUpper, upper.R / 255f, upper.G / 255f, upper.B / 255f, 1f);
				GL.Uniform4(_uColorLower, lower.R / 255f, lower.G / 255f, lower.B / 255f, 1f);

				GL.BindVertexArray(vao);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, count);
			}

			RenderBodies();

			GL.BindVertexArray(0);
			GL.UseProgram(0);
		}

		private void RenderBodies()
		{
			if (!IsPaintEnabled || MtxToEcl == null) return;

			GL.Uniform1(_uMode, 1);
			GL.BindVertexArray(_bodyVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _bodyVbo);

			float[] pos = new float[3];

			// Sun at origin (same in all coordinate systems)
			pos[0] = 0f; pos[1] = 0f; pos[2] = 0f;
			GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), pos, BufferUsageHint.StreamDraw);
			GL.Uniform4(_uColorUpper, ColorSun.R / 255f, ColorSun.G / 255f, ColorSun.B / 255f, 1f);
			GL.PointSize(6f);
			GL.DrawArrays(PrimitiveType.Points, 0, 1);

			// Planet bodies — PlanetsPos is ecliptic, no MtxToEcl needed
			GL.PointSize(5f);
			GL.Uniform4(_uColorUpper, ColorPlanet.R / 255f, ColorPlanet.G / 255f, ColorPlanet.B / 255f, 1f);
			foreach (Object planet in Planets)
			{
				if (Zoom * GetPlanetAU(planet) < 30.0) continue;
				if (PlanetsPos[planet] == null) continue;

				Xyz p = PlanetsPos[planet];
				pos[0] = (float)p.X; pos[1] = (float)p.Y; pos[2] = (float)p.Z;
				GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), pos, BufferUsageHint.StreamDraw);
				GL.DrawArrays(PrimitiveType.Points, 0, 1);
			}

			// Comet bodies — CometsPos is equatorial J2000, apply MtxToEcl
			for (int i = 0; i < Comets.Count && i < CometsPos.Count; i++)
			{
				bool visibleComet = Comets[i].IsVisible;
				bool visibleSelected = PreserveSelectedLabel && i == SelectedIndex;
				bool isCometMarked = Comets[i].IsMarked;

				GetCometColorAndDiameter(Comets[i], out int diameter, out Color color);

				if (!visibleComet)
				{
					color = FilterOnDateWeakColorComet;
					if (FilterOnDateShowInWeakColor)
						visibleComet = true;
				}

				if (!visibleComet && !visibleSelected && !isCometMarked)
					continue;

				Xyz p = CometsPos[i].Rotate(MtxToEcl);
				pos[0] = (float)p.X; pos[1] = (float)p.Y; pos[2] = (float)p.Z;
				GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), pos, BufferUsageHint.StreamDraw);
				GL.Uniform4(_uColorUpper, color.R / 255f, color.G / 255f, color.B / 255f, 1f);
				GL.PointSize(diameter * 2f);
				GL.DrawArrays(PrimitiveType.Points, 0, 1);
			}

			GL.Uniform1(_uMode, 0);
		}

		private void UpdateCometPanelLocations()
		{
			if (!IsPaintEnabled || MtxToEcl == null || MtxRotate == null) return;

			for (int i = 0; i < Comets.Count && i < CometsPos.Count; i++)
			{
				Xyz viewXyz = CometsPos[i].Rotate(MtxToEcl).Rotate(MtxRotate);
				Comets[i].PanelLocation = GetDrawPoint(viewXyz);
			}
		}

		private static double GetPlanetAU(Object planet)
		{
			switch (planet)
			{
				case Object.Mercury: return 0.387;
				case Object.Venus:   return 0.723;
				case Object.Earth:   return 1.0;
				case Object.Mars:    return 1.524;
				case Object.Jupiter: return 5.2;
				case Object.Saturn:  return 9.58;
				case Object.Uranus:  return 19.2;
				case Object.Neptune: return 30.1;
				default:             return 1.0;
			}
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
			_vbosNeedUpdate = true;
		}

		#endregion

		#region UpdateRotationMatrix

		private void UpdateRotationMatrix(ATime atime)
		{
			Matrix mtxPrec = Matrix.PrecMatrix(Astro.JD2000, atime.JD);
			Matrix mtxEqt2Ecl = Matrix.RotateX(ATime.GetEp(atime.JD));
			MtxToEcl = mtxEqt2Ecl.Mul(mtxPrec);
			EpochToEcl = atime.JD;
			_vbosNeedUpdate = true;
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
			double sizeAU = 150.0;

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
					xyz = xyz.Rotate(MtxRotate);
					point2 = GetDrawPoint(xyz);
					graphics.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
					point1 = point2;
				}
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

			_vbosNeedUpdate = true;
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
