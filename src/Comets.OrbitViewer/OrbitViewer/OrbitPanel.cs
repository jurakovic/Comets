using Comets.Core;
using Comets.Core.Managers;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

		private const string VertexShaderSource = @"
#version 330 core
layout (location = 0) in vec3 aPos;
uniform mat4 uMVP;
out float vZ;
void main()
{
    vZ = aPos.z;
    gl_Position = uMVP * vec4(aPos, 1.0);
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
        float d = length(gl_PointCoord - vec2(0.5));
        float alpha = 1.0 - smoothstep(0.5 - fwidth(d), 0.5, d);
        if (alpha == 0.0) discard;
        FragColor = vec4(uColorUpper.rgb, uColorUpper.a * alpha);
    }
    else
    {
        FragColor = vZ >= 0.0 ? uColorUpper : uColorLower;
    }
}";

		private const string TextVertexShaderSource = @"
#version 330 core
layout(location = 0) in vec2 aPos;
layout(location = 1) in vec2 aUV;
out vec2 vUV;
void main() {
    gl_Position = vec4(aPos, 0.0, 1.0);
    vUV = aUV;
}";

		private const string TextFragmentShaderSource = @"
#version 330 core
in vec2 vUV;
uniform sampler2D uTex;
out vec4 FragColor;
void main() {
    FragColor = texture(uTex, vUV);
}";

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
		protected Color ColorGrid = Color.FromArgb(30, 40, 100);
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

		private List<Xyz> CometsPos;

		private Dictionary<Object, PlanetOrbit> PlanetsOrbit;
		private Dictionary<Object, Xyz?> PlanetsPos;

		private Matrix MtxToEcl;

		// GL rendering
		private bool _glLoaded = false;
		private int _shaderProgram = 0;
		private int _uMVP;
		private Matrix4 _mvp;
		private Matrix4 _view;
		private float _orthoHalfH;
		private int _uColorUpper;
		private int _uColorLower;
		private int _uMode;
		private int _bodyVao = 0;
		private int _bodyVbo = 0;
		private Dictionary<Object, (int vao, int vbo, int count)> _planetOrbitBuffers;
		private Dictionary<int, (int vao, int vbo, int count)> _cometOrbitBuffers = new Dictionary<int, (int, int, int)>();
		private bool _planetVbosNeedUpdate = false;
		private bool _cometVbosDirty = false;

		private int _cometBatchVao = 0;
		private int _cometBatchVbo = 0;
		private int[] _cometBatchStarts = Array.Empty<int>();
		private int[] _cometBatchCounts = Array.Empty<int>();
		private int _cometBatchDrawCount = 0;

		// Text overlay
		private int _textShaderProgram = 0;
		private int _uTextSampler;
		private int _textQuadVao = 0;
		private int _textQuadVbo = 0;
		private int _textTex = 0;

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
				UpdatePositions(value);
				UpdatePlanetOrbit(value);
				UpdateRotationMatrix(value);
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
		public bool ShowGrid { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double GridExtent { get; set; } = 150.0;

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

		public OrbitPanel() : base(new GLControlSettings { NumberOfSamples = 8, DepthBits = 24 })
		{
			PlanetsPos = new Dictionary<Object, Xyz?>();
			Planets.ForEach(planet => PlanetsPos.Add(planet, null));
			PlanetsOrbit = InitializeDictionary<PlanetOrbit>();

			Comets = new List<OVComet>();
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

			IsPaintEnabled = false;
		}

		#endregion

		#region LoadPanel

		public void LoadPanel(OVComet comet, ATime atime)
		{
			IsPaintEnabled = true;

			if (!MultipleMode)
				Comets.Clear();

			if (comet != null && !Comets.Contains(comet))
				Comets.Add(comet);

			SelectedIndex = Comets.IndexOf(comet);

			ATime = atime;
			_cometVbosDirty = true;
		}

		public void LoadPanel(List<OVComet> comets, ATime atime, int index)
		{
			List<OVComet> marked = MarkedComets.ToList();

			IsPaintEnabled = true;
			MultipleMode = true;

			Comets.Clear();
			Comets = comets;
			SelectedIndex = index;
			CenteredIndex = index;

			foreach (OVComet c in Comets)
				if (c.IsMarked && !marked.Contains(c))
					c.IsMarked = false;

			ATime = atime;
			_cometVbosDirty = true;
		}

		#endregion

		#region OnPaint / OnResize

		protected override void OnPaint(PaintEventArgs e)
		{
			try { MakeCurrent(); }
			catch { return; }

			if (!_glLoaded)
				InitGL();

			// Resolve CenteredIndex for camera target centering
			if (CenteredObject != Object.Comet)
				CenteredIndex = -1;

			if (IsPaintEnabled && MtxToEcl != null && CenteredObject == Object.Comet && CenteredIndex == -1)
				CenteredIndex = SelectedIndex;

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

		#region RenderScene

		private void InitGL()
		{
			GL.ClearColor(0f, 0f, 0f, 1f);
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Less);
			GL.Enable(EnableCap.Multisample);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
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

			_uMVP = GL.GetUniformLocation(_shaderProgram, "uMVP");
			_uColorUpper = GL.GetUniformLocation(_shaderProgram, "uColorUpper");
			_uColorLower = GL.GetUniformLocation(_shaderProgram, "uColorLower");
			_uMode = GL.GetUniformLocation(_shaderProgram, "uMode");

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

			if (Antialiasing)
			{
				GL.Enable(EnableCap.LineSmooth);
				GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			}

			// Text overlay shader
			int tvs = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(tvs, TextVertexShaderSource);
			GL.CompileShader(tvs);

			int tfs = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(tfs, TextFragmentShaderSource);
			GL.CompileShader(tfs);

			_textShaderProgram = GL.CreateProgram();
			GL.AttachShader(_textShaderProgram, tvs);
			GL.AttachShader(_textShaderProgram, tfs);
			GL.LinkProgram(_textShaderProgram);
			GL.DeleteShader(tvs);
			GL.DeleteShader(tfs);

			_uTextSampler = GL.GetUniformLocation(_textShaderProgram, "uTex");

			// Fullscreen quad: NDC xy + UV, two triangles
			// UV y is flipped so bitmap top-left maps to screen top-left
			float[] quadVerts = {
				-1f, -1f,  0f, 1f,
				 1f, -1f,  1f, 1f,
				 1f,  1f,  1f, 0f,
				-1f, -1f,  0f, 1f,
				 1f,  1f,  1f, 0f,
				-1f,  1f,  0f, 0f,
			};

			_textQuadVao = GL.GenVertexArray();
			_textQuadVbo = GL.GenBuffer();
			GL.BindVertexArray(_textQuadVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _textQuadVbo);
			GL.BufferData(BufferTarget.ArrayBuffer, quadVerts.Length * sizeof(float), quadVerts, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
			GL.EnableVertexAttribArray(1);
			GL.BindVertexArray(0);

			_textTex = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, _textTex);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
			GL.BindTexture(TextureTarget.Texture2D, 0);

			_glLoaded = true;
			_planetVbosNeedUpdate = true;
		}

		private void UploadOrbitsToGpu()
		{
			if (MtxToEcl == null || !IsPaintEnabled)
			{
				_planetVbosNeedUpdate = false;
				_cometVbosDirty = false;
				return;
			}

			// Planet VBOs: rebuild when date or rotation matrix changed (8 planets, fast).
			if (_planetVbosNeedUpdate)
			{
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
						verts[i * 3] = (float)p.X;
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
			}

			// Comet VBOs: only rebuild when comet data changes, never on every date tick.
			if (_cometVbosDirty)
			{
				// Individual VBOs for selected comet and marked comets (small count, special colors).
				var required = new HashSet<int>();
				if (PreserveSelectedOrbit && SelectedIndex >= 0 && SelectedIndex < Comets.Count)
					required.Add(SelectedIndex);
				for (int i = 0; i < Comets.Count; i++)
					if (Comets[i].IsMarked) required.Add(i);

				foreach (int key in _cometOrbitBuffers.Keys.ToList())
				{
					if (!required.Contains(key))
					{
						var (vao, vbo, _) = _cometOrbitBuffers[key];
						if (vao != 0) GL.DeleteVertexArray(vao);
						if (vbo != 0) GL.DeleteBuffer(vbo);
						_cometOrbitBuffers.Remove(key);
					}
				}

				foreach (int i in required)
				{
					var orbit = new CometOrbit(Comets[i]);
					int n = orbit.PointCount;
					float[] verts = new float[n * 3];
					for (int j = 0; j < n; j++)
					{
						Xyz p = orbit.GetAt(j).Rotate(MtxToEcl);
						verts[j * 3] = (float)p.X;
						verts[j * 3 + 1] = (float)p.Y;
						verts[j * 3 + 2] = (float)p.Z;
					}

					int vao, vbo;
					if (_cometOrbitBuffers.TryGetValue(i, out var existing))
					{
						vao = existing.vao;
						vbo = existing.vbo;
					}
					else
					{
						vao = GL.GenVertexArray();
						vbo = GL.GenBuffer();
					}

					GL.BindVertexArray(vao);
					GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
					GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.DynamicDraw);
					GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
					GL.EnableVertexAttribArray(0);
					GL.BindVertexArray(0);

					_cometOrbitBuffers[i] = (vao, vbo, n);
				}

				// Batch VBO: all visible comets concatenated into one buffer for a single draw call.
				// Skip selected/marked — they get individual draws with highlight colors, and
				// drawing the same vertices twice fails depth test (DepthFunc.Less + equal depth).
				if (OrbitDisplay.Contains(Object.Comet))
				{
					var batchVerts = new List<float[]>();
					for (int i = 0; i < Comets.Count; i++)
					{
						if (!Comets[i].IsVisible) continue;
						if (Comets[i].IsMarked) continue;
						if (PreserveSelectedOrbit && i == SelectedIndex) continue;
						var orbit = new CometOrbit(Comets[i]);
						int n = orbit.PointCount;
						float[] verts = new float[n * 3];
						for (int j = 0; j < n; j++)
						{
							Xyz p = orbit.GetAt(j).Rotate(MtxToEcl);
							verts[j * 3] = (float)p.X;
							verts[j * 3 + 1] = (float)p.Y;
							verts[j * 3 + 2] = (float)p.Z;
						}
						batchVerts.Add(verts);
					}

					_cometBatchStarts = new int[batchVerts.Count];
					_cometBatchCounts = new int[batchVerts.Count];
					_cometBatchDrawCount = batchVerts.Count;

					if (batchVerts.Count > 0)
					{
						int totalFloats = batchVerts.Sum(v => v.Length);
						float[] combined = new float[totalFloats];
						int pos = 0;
						for (int k = 0; k < batchVerts.Count; k++)
						{
							_cometBatchStarts[k] = pos;
							_cometBatchCounts[k] = batchVerts[k].Length / 3;
							System.Buffer.BlockCopy(batchVerts[k], 0, combined, pos * 3 * sizeof(float), batchVerts[k].Length * sizeof(float));
							pos += batchVerts[k].Length / 3;
						}

						if (_cometBatchVao == 0)
						{
							_cometBatchVao = GL.GenVertexArray();
							_cometBatchVbo = GL.GenBuffer();
							GL.BindVertexArray(_cometBatchVao);
							GL.BindBuffer(BufferTarget.ArrayBuffer, _cometBatchVbo);
							GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
							GL.EnableVertexAttribArray(0);
							GL.BindVertexArray(0);
						}

						GL.BindVertexArray(_cometBatchVao);
						GL.BindBuffer(BufferTarget.ArrayBuffer, _cometBatchVbo);
						GL.BufferData(BufferTarget.ArrayBuffer, totalFloats * sizeof(float), combined, BufferUsageHint.DynamicDraw);
						GL.BindVertexArray(0);
					}
				}
				else
				{
					_cometBatchDrawCount = 0;
				}
			}

			_planetVbosNeedUpdate = false;
			_cometVbosDirty = false;
		}

		public void InvalidateCometVbos()
		{
			_cometVbosDirty = true;
		}

		/// <summary>
		/// Recomputes _mvp, _view, and _orthoHalfH from the current camera parameters.
		/// Pure CPU math — no GL calls. Safe to call before the first rendered frame.
		/// </summary>
		private void UpdateMVP()
		{
			// Build MVP: orthographic projection, view built directly from rotation angles.
			// Matches the original RotateX(RotateVert)·RotateZ(RotateHorz) scene transform exactly,
			// with a -camDist Z translation added so depth-based calculations still work.
			// No LookAt needed — avoids all gimbal/singularity issues.
			// orthoHalfH is derived from a 45° reference FOV so the scene scale matches what a
			// 45° perspective camera at camDist would show at the centre plane.
			const float refFovY = MathF.PI / 4f; // 45° reference — defines scale, not frustum shape
			float aspect = Width > 0 && Height > 0 ? (float)Width / Height : 1f;
			float camDist = 1800f / (float)Zoom;
			float orthoHalfH = camDist * MathF.Tan(refFovY / 2f);

			float h = (float)(RotateHorz * Math.PI / 180.0);
			float v = (float)(RotateVert * Math.PI / 180.0);

			// Effective scene rotation: R = RotateX_std(-v) * RotateZ_std(-h)  (matches old GPU convention).
			// View = [R | translation], camera at R^T*(0,0,D).  R*eye = (0,0,D) so translation = (0,0,-D).
			// OpenTK stores matrices transposed vs math convention: OpenTK Row i = Math column i of V.
			//
			// Verification: Z+ world -> y_eye = +sin(v)  (positive = UP on screen) ✓
			//               X+ world -> y_eye = -cos(v)*sin(h)                     ✓
			Matrix4 view = new Matrix4(
				new Vector4(MathF.Cos(h), -MathF.Cos(v) * MathF.Sin(h), MathF.Sin(v) * MathF.Sin(h), 0),
				new Vector4(MathF.Sin(h), MathF.Cos(v) * MathF.Cos(h), -MathF.Sin(v) * MathF.Cos(h), 0),
				new Vector4(0, MathF.Sin(v), MathF.Cos(v), 0),
				new Vector4(0, 0, -camDist, 1)
			);

			// Centering: translate the world so the target object is at the camera's look-at point.
			Vector3 target = Vector3.Zero;
			if (CenteredObject == Object.Comet && CenteredIndex >= 0 && CenteredIndex < CometsPos.Count && MtxToEcl != null)
			{
				Xyz p = CometsPos[CenteredIndex].Rotate(MtxToEcl);
				target = new Vector3((float)p.X, (float)p.Y, (float)p.Z);
			}
			else if (Planets.Contains(CenteredObject) && PlanetsPos[CenteredObject] != null)
			{
				Xyz p = PlanetsPos[CenteredObject].Value;
				target = new Vector3((float)p.X, (float)p.Y, (float)p.Z);
			}
			Matrix4 model = Matrix4.CreateTranslation(-target);

			// Orthographic projection with a symmetric depth range: near = -(camDist + 500),
			// far = +(camDist + 500).  The near plane sits 500+ AU behind the camera, so orbits
			// that cross the camera plane are never clipped mid-screen — they continue as complete,
			// undistorted ellipses.  In a parallel projection there is no "viewpoint", so showing
			// the full orbit (including the portion behind the camera position) is correct.
			float halfDepth = camDist + 500f;
			Matrix4 projection = Matrix4.CreateOrthographic(orthoHalfH * aspect * 2f, orthoHalfH * 2f, -halfDepth, halfDepth);
			_mvp = model * view * projection; // OpenTK row-major: reversed order, transpose:false
			_view = view;
			_orthoHalfH = orthoHalfH;
		}

		private void RenderScene()
		{
			if (!IsPaintEnabled || _shaderProgram == 0)
				return;

			if (_planetVbosNeedUpdate || _cometVbosDirty)
				UploadOrbitsToGpu();

			UpdateMVP();

			if (Antialiasing)
			{
				GL.Enable(EnableCap.Multisample);
				GL.Enable(EnableCap.LineSmooth);
				GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			}
			else
			{
				GL.Disable(EnableCap.Multisample);
				GL.Disable(EnableCap.LineSmooth);
			}

			GL.UseProgram(_shaderProgram);
			GL.UniformMatrix4(_uMVP, false, ref _mvp);
			GL.LineWidth(1.5f);

			if (ShowGrid)
				RenderGrid();

			// Planet orbits
			foreach (Object planet in Planets)
			{
				if (!OrbitDisplay.Contains(planet) || !_planetOrbitBuffers.ContainsKey(planet))
					continue;

				if (Zoom * GetPlanetAU(planet) < 15.0)
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

			// Comet orbits: one batched draw for all visible, then individual draws for selected/marked colors.
			if (_cometBatchDrawCount > 0)
			{
				GL.Uniform4(_uColorUpper, ColorCometOrbitUpper.R / 255f, ColorCometOrbitUpper.G / 255f, ColorCometOrbitUpper.B / 255f, 1f);
				GL.Uniform4(_uColorLower, ColorCometOrbitLower.R / 255f, ColorCometOrbitLower.G / 255f, ColorCometOrbitLower.B / 255f, 1f);
				GL.BindVertexArray(_cometBatchVao);
				GL.MultiDrawArrays(PrimitiveType.LineStrip, _cometBatchStarts, _cometBatchCounts, _cometBatchDrawCount);
			}

			int markedCount = MarkedComets.Count();

			for (int i = 0; i < Comets.Count; i++)
			{
				bool visibleSelected = PreserveSelectedOrbit && i == SelectedIndex;
				bool isCometMarked = Comets[i].IsMarked;

				if (!visibleSelected && !isCometMarked) continue;

				if (!_cometOrbitBuffers.TryGetValue(i, out var cometBuf)) continue;

				bool visibleComet = Comets[i].IsVisible;
				bool useWeakColor = !visibleComet && FilterOnDateShowInWeakColor && !visibleSelected && !isCometMarked;
				bool useSelectedColor = visibleSelected && MultipleMode &&
					(OrbitDisplay.Contains(Object.Comet) ||
					 (markedCount > 0 && !isCometMarked) ||
					 (markedCount > 1 && isCometMarked));

				var (vao, _, count) = cometBuf;
				if (vao == 0 || count == 0) continue;

				Color upper, lower;

				if (useWeakColor)
				{
					upper = lower = FilterOnDateWeakColorOrbit;
				}
				else if (useSelectedColor)
				{
					upper = ColorSelectedCometOrbitUpper;
					lower = ColorSelectedCometOrbitLower;
				}
				else
				{
					upper = ColorCometOrbitUpper;
					lower = ColorCometOrbitLower;
				}

				GL.Uniform4(_uColorUpper, upper.R / 255f, upper.G / 255f, upper.B / 255f, 1f);
				GL.Uniform4(_uColorLower, lower.R / 255f, lower.G / 255f, lower.B / 255f, 1f);

				GL.BindVertexArray(vao);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, count);
			}

			if (ShowAxes)
				RenderAxes();

			RenderBodies();
			RenderLabels();

			GL.BindVertexArray(0);
			GL.UseProgram(0);
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

		#region UpdateCometPanelLocations

		private void UpdateCometPanelLocations()
		{
			if (!IsPaintEnabled || MtxToEcl == null || !_glLoaded) return;

			UpdateMVP();

			for (int i = 0; i < Comets.Count && i < CometsPos.Count; i++)
			{
				Xyz eclXyz = CometsPos[i].Rotate(MtxToEcl);
				Comets[i].PanelLocation = MvpProject(eclXyz);
			}
		}

		#endregion

		#region UpdatePlanetOrbit

		private void UpdatePlanetOrbit(ATime atime)
		{
			Planets.ForEach(p => PlanetsOrbit[p] = new PlanetOrbit(p, atime));
			_planetVbosNeedUpdate = true;
		}

		#endregion

		#region UpdateRotationMatrix

		private void UpdateRotationMatrix(ATime atime)
		{
			Matrix mtxPrec = Matrix.PrecMatrix(Astro.JD2000, atime.JD);
			Matrix mtxEqt2Ecl = Matrix.RotateX(ATime.GetEp(atime.JD));
			MtxToEcl = mtxEqt2Ecl.Mul(mtxPrec);
			_planetVbosNeedUpdate = true;
		}

		#endregion

		#region MvpProject

		/// <summary>
		/// Projects a world-space ecliptic position to screen pixels using the current MVP matrix.
		/// </summary>
		private Point MvpProject(Xyz xyz)
		{
			var v = new Vector4((float)xyz.X, (float)xyz.Y, (float)xyz.Z, 1.0f) * _mvp;
			float ndcX = v.X / v.W;
			float ndcY = v.Y / v.W;
			int screenX = (int)((ndcX + 1f) / 2f * Width);
			int screenY = (int)((1f - ndcY) / 2f * Height);
			return new Point(screenX, screenY);
		}

		#endregion

		#region RenderAxes

		private void RenderAxes()
		{
			const double SizeAU = 150.0;

			// 3 minus-axis lines: origin→-X, origin→-Y, origin→-Z (6 vertices)
			// 3 plus-axis lines:  origin→+X, origin→+Y, origin→+Z (6 vertices)
			float[] minus = new float[6 * 3];
			float[] plus = new float[6 * 3];

			(double X, double Y, double Z)[] dirs = {
				(-SizeAU, 0, 0), (0, -SizeAU, 0), (0, 0, -SizeAU),
				( SizeAU, 0, 0), (0,  SizeAU, 0), (0, 0,  SizeAU),
			};

			for (int i = 0; i < 3; i++)
			{
				var d = dirs[i];
				int b = i * 6;
				minus[b] = 0f; minus[b + 1] = 0f; minus[b + 2] = 0f;
				var tip = PseudoPerspective(new Xyz(d.X, d.Y, d.Z));
				minus[b + 3] = (float)tip.X; minus[b + 4] = (float)tip.Y; minus[b + 5] = (float)tip.Z;
			}
			for (int i = 0; i < 3; i++)
			{
				var d = dirs[i + 3];
				int b = i * 6;
				plus[b] = 0f; plus[b + 1] = 0f; plus[b + 2] = 0f;
				var tip = PseudoPerspective(new Xyz(d.X, d.Y, d.Z));
				plus[b + 3] = (float)tip.X; plus[b + 4] = (float)tip.Y; plus[b + 5] = (float)tip.Z;
			}

			GL.Uniform1(_uMode, 0);
			GL.BindVertexArray(_bodyVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _bodyVbo);

			GL.BufferData(BufferTarget.ArrayBuffer, minus.Length * sizeof(float), minus, BufferUsageHint.StreamDraw);
			GL.Uniform4(_uColorUpper, ColorAxisMinus.R / 255f, ColorAxisMinus.G / 255f, ColorAxisMinus.B / 255f, 1f);
			GL.Uniform4(_uColorLower, ColorAxisMinus.R / 255f, ColorAxisMinus.G / 255f, ColorAxisMinus.B / 255f, 1f);
			GL.DrawArrays(PrimitiveType.Lines, 0, 6);

			GL.BufferData(BufferTarget.ArrayBuffer, plus.Length * sizeof(float), plus, BufferUsageHint.StreamDraw);
			GL.Uniform4(_uColorUpper, ColorAxisPlus.R / 255f, ColorAxisPlus.G / 255f, ColorAxisPlus.B / 255f, 1f);
			GL.Uniform4(_uColorLower, ColorAxisPlus.R / 255f, ColorAxisPlus.G / 255f, ColorAxisPlus.B / 255f, 1f);
			GL.DrawArrays(PrimitiveType.Lines, 0, 6);
		}

		#endregion

		#region PseudoPerspective

		// Applies the same f = 800/(800-vd) depth-scaling used by the grid to any ecliptic-plane
		// (Z=0) world-space point so that axes and labels stay aligned with the grid boundary.
		// Points with Z≠0 (the ecliptic-pole axis) are returned unchanged.
		private Xyz PseudoPerspective(Xyz xyz)
		{
			if (xyz.Z != 0.0) return xyz;
			const double D = 800.0;
			double sinV = Math.Sin(RotateVert * Math.PI / 180.0);
			double sinH = Math.Sin(RotateHorz * Math.PI / 180.0);
			double cosH = Math.Cos(RotateHorz * Math.PI / 180.0);
			double vd   = sinV * (xyz.X * sinH - xyz.Y * cosH);
			double f    = D / Math.Max(1.0, D - vd);
			return new Xyz(f * xyz.X, f * xyz.Y, 0.0);
		}

		#endregion

		#region RenderGrid

		private static readonly double[] GridNiceSteps = { 0.1, 0.2, 0.5, 1, 2, 5, 10, 25 };

		private void RenderGrid()
		{
			double extent               = Math.Max(0.01, GridExtent);
			const double idealSpacingPx = 50.0;  // px — target screen spacing between lines
			const double minSpacingPx   = 15.0;  // px — minimum spacing before stopping step-down
			const int    minCells       = 9;     // minimum cells per side
			const double elevFullMul    = 3.86;  // 1/sin(15°) — full opacity above 15° from ecliptic
			const double fadeExp        = 1.5;
			const double zoomFadeLo     = 5.0;   // px/AU — outer zoom: fade suppressed (grid always visible)
			const double zoomFadeHi     = 100.0; // px/AU — inner zoom: elevation fade fully active
			const double perspD         = 800.0; // virtual camera distance for pseudo-perspective (AU)

			// elevFactor = |cos(RotateVert)|: 1 when top-down (v=0°), 0 when edge-on (v=90°)
			double elevFactor  = Math.Abs(Math.Cos(RotateVert * Math.PI / 180.0));
			double pixelsPerAU = Height / (2.0 * _orthoHalfH);

			// Adaptive step: target idealSpacingPx between lines, corrected for foreshortening
			double idealStep = idealSpacingPx / (pixelsPerAU * Math.Max(0.15, elevFactor));
			int stepIdx = Array.FindIndex(GridNiceSteps, s => s >= idealStep);
			if (stepIdx < 0) stepIdx = GridNiceSteps.Length - 1;

			// Step down to finer grid to keep >= minCells cells within the extent
			while (stepIdx > 0
				&& extent / GridNiceSteps[stepIdx] < minCells
				&& GridNiceSteps[stepIdx - 1] * pixelsPerAU >= minSpacingPx)
			{
				stepIdx--;
			}

			double step = GridNiceSteps[stepIdx];

			// Elevation fade: full opacity above 15° from ecliptic, fades only in the last 15°
			double rawAlpha = Math.Pow(Math.Min(1.0, elevFactor * elevFullMul), fadeExp);
			// Zoom modulation: suppress elevation fade when zoomed out so grid stays visible
			double zoomNorm = Math.Min(1.0, Math.Max(0.0, (pixelsPerAU - zoomFadeLo) / (zoomFadeHi - zoomFadeLo)));
			double alpha    = rawAlpha + (1.0 - rawAlpha) * (1.0 - zoomNorm);
			if (alpha < 0.01) return;

			// Pseudo-perspective: apply a soft perspective factor f = perspD / (perspD - vd) to
			// each vertex, where vd is its depth in the camera direction. For ecliptic points
			// (pz=0) this simplifies to scaling (px, py) by f — proved by expanding the
			// camera-space transform and back-projecting. D=800 AU keeps f within ~±7% for the
			// solar system, matching the orb reference without distorting orbital paths.
			double sinV = Math.Sin(RotateVert * Math.PI / 180.0);
			double sinH = Math.Sin(RotateHorz * Math.PI / 180.0);
			double cosH = Math.Cos(RotateHorz * Math.PI / 180.0);

			double R        = extent;
			int lineCount   = (int)Math.Round(2.0 * R / step) + 1;
			float[] verts   = new float[lineCount * 4 * 3];
			int idx         = 0;
			int vertexCount = 0;

			for (int k = 0; k < lineCount; k++)
			{
				double u = -R + k * step;

				// When axes are visible, skip the u=0 grid lines (Y=0 and X=0) — they are
				// geometrically identical to the X and Y axis lines and cause Z-fighting.
				if (ShowAxes && Math.Abs(u) < step * 0.001)
					continue;

				// Line parallel to X axis (constant Y = u)
				AddPerspVertex(verts, ref idx, -R, u, sinV, sinH, cosH, perspD);
				AddPerspVertex(verts, ref idx,  R, u, sinV, sinH, cosH, perspD);
				// Line parallel to Y axis (constant X = u)
				AddPerspVertex(verts, ref idx, u, -R, sinV, sinH, cosH, perspD);
				AddPerspVertex(verts, ref idx, u,  R, sinV, sinH, cosH, perspD);
				vertexCount += 4;
			}

			if (vertexCount == 0) return;

			float r = ColorGrid.R / 255f;
			float g = ColorGrid.G / 255f;
			float b = ColorGrid.B / 255f;
			float a = (float)alpha;

			GL.Uniform1(_uMode, 0);
			GL.Uniform4(_uColorUpper, r, g, b, a);
			GL.Uniform4(_uColorLower, r, g, b, a);
			GL.BindVertexArray(_bodyVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _bodyVbo);
			GL.BufferData(BufferTarget.ArrayBuffer, idx * sizeof(float), verts, BufferUsageHint.StreamDraw);
			GL.DrawArrays(PrimitiveType.Lines, 0, vertexCount);
		}

		private static void AddPerspVertex(float[] buf, ref int idx,
			double px, double py, double sinV, double sinH, double cosH, double D)
		{
			double vd = sinV * (px * sinH - py * cosH);
			double f  = D / Math.Max(1.0, D - vd);
			buf[idx++] = (float)(f * px);
			buf[idx++] = (float)(f * py);
			buf[idx++] = 0f;
		}

		#endregion

		#region GetPlanetAU

		private static double GetPlanetAU(Object planet)
		{
			switch (planet)
			{
				case Object.Mercury: return 0.387;
				case Object.Venus: return 0.723;
				case Object.Earth: return 1.0;
				case Object.Mars: return 1.524;
				case Object.Jupiter: return 5.2;
				case Object.Saturn: return 9.58;
				case Object.Uranus: return 19.2;
				case Object.Neptune: return 30.1;
				default: return 1.0;
			}
		}

		#endregion

		#region RenderBodies

		private void RenderBodies()
		{
			if (!IsPaintEnabled || MtxToEcl == null) return;

			GL.BindVertexArray(_bodyVao);
			GL.BindBuffer(BufferTarget.ArrayBuffer, _bodyVbo);

			float[] buf = new float[24]; // enough for 8 × vec3

			// Sun at origin
			GL.Uniform1(_uMode, 1);
			buf[0] = 0f; buf[1] = 0f; buf[2] = 0f;
			GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), buf, BufferUsageHint.StreamDraw);
			GL.Uniform4(_uColorUpper, ColorSun.R / 255f, ColorSun.G / 255f, ColorSun.B / 255f, 1f);
			GL.PointSize(7f);
			GL.DrawArrays(PrimitiveType.Points, 0, 1);

			// Planet bodies — PlanetsPos is ecliptic, no MtxToEcl needed
			GL.PointSize(6f);
			GL.Uniform4(_uColorUpper, ColorPlanet.R / 255f, ColorPlanet.G / 255f, ColorPlanet.B / 255f, 1f);
			foreach (Object planet in Planets)
			{
				if (Zoom * GetPlanetAU(planet) < 15.0) continue;
				if (PlanetsPos[planet] == null) continue;

				Xyz p = PlanetsPos[planet].Value;
				buf[0] = (float)p.X; buf[1] = (float)p.Y; buf[2] = (float)p.Z;
				GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), buf, BufferUsageHint.StreamDraw);
				GL.DrawArrays(PrimitiveType.Points, 0, 1);
			}

			// Comet bodies — CometsPos is equatorial J2000, apply MtxToEcl
			for (int i = 0; i < Comets.Count && i < CometsPos.Count; i++)
			{
				bool visibleComet = Comets[i].IsVisible;
				bool visibleSelected = PreserveSelectedLabel && i == SelectedIndex;
				bool visibleMarker = ShowMarker && i == SelectedIndex;
				bool isCometMarked = Comets[i].IsMarked;

				GetCometColorAndDiameter(Comets[i], out int diameter, out Color color);

				if (!visibleComet)
				{
					color = FilterOnDateWeakColorComet;
					if (FilterOnDateShowInWeakColor)
						visibleComet = true;
				}

				Xyz p = CometsPos[i].Rotate(MtxToEcl);

				if (visibleComet || visibleSelected || isCometMarked)
				{
					GL.Uniform1(_uMode, 1);
					buf[0] = (float)p.X; buf[1] = (float)p.Y; buf[2] = (float)p.Z;
					GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float), buf, BufferUsageHint.StreamDraw);
					GL.Uniform4(_uColorUpper, color.R / 255f, color.G / 255f, color.B / 255f, 1f);
					GL.PointSize(diameter * 1.25f + 1f);
					GL.DrawArrays(PrimitiveType.Points, 0, 1);
				}

				if (visibleMarker || isCometMarked)
				{
					// Build crosshair arms in world (ecliptic) space using camera right/up vectors.
					// Column 0/1 of the view matrix are the camera right/up directions in world space.
					Vector3 right = _view.Column0.Xyz;
					Vector3 upVec = _view.Column1.Xyz;
					var pVec = new Vector3((float)p.X, (float)p.Y, (float)p.Z);

					float pxSize = _orthoHalfH / (Height > 0 ? Height / 2f : 1f);
					float off = (diameter + 4) * pxSize;
					float len = (diameter + 8) * pxSize;

					// 8 endpoints: pairs forming 4 arms (up, down, left, right)
					Xyz[] arms = {
						new Xyz(p.X + upVec.X * off, p.Y + upVec.Y * off, p.Z + upVec.Z * off),
						new Xyz(p.X + upVec.X * len, p.Y + upVec.Y * len, p.Z + upVec.Z * len),
						new Xyz(p.X - upVec.X * off, p.Y - upVec.Y * off, p.Z - upVec.Z * off),
						new Xyz(p.X - upVec.X * len, p.Y - upVec.Y * len, p.Z - upVec.Z * len),
						new Xyz(p.X - right.X * off, p.Y - right.Y * off, p.Z - right.Z * off),
						new Xyz(p.X - right.X * len, p.Y - right.Y * len, p.Z - right.Z * len),
						new Xyz(p.X + right.X * off, p.Y + right.Y * off, p.Z + right.Z * off),
						new Xyz(p.X + right.X * len, p.Y + right.Y * len, p.Z + right.Z * len),
					};
					for (int k = 0; k < 8; k++)
					{
						buf[k * 3] = (float)arms[k].X;
						buf[k * 3 + 1] = (float)arms[k].Y;
						buf[k * 3 + 2] = (float)arms[k].Z;
					}

					Color cmarker = isCometMarked ? ColorMarkedCometMarker : ColorSelectedCometMarker;
					GL.Uniform1(_uMode, 0);
					GL.BufferData(BufferTarget.ArrayBuffer, 24 * sizeof(float), buf, BufferUsageHint.StreamDraw);
					GL.Uniform4(_uColorUpper, cmarker.R / 255f, cmarker.G / 255f, cmarker.B / 255f, 1f);
					GL.Uniform4(_uColorLower, cmarker.R / 255f, cmarker.G / 255f, cmarker.B / 255f, 1f);
					GL.LineWidth(3f);
					GL.DrawArrays(PrimitiveType.Lines, 0, 8);
				}
			}

			GL.Uniform1(_uMode, 0);
		}

		#endregion

		#region RenderLabels

		private void RenderLabels()
		{
			if (!IsPaintEnabled || Width <= 0 || Height <= 0 || _textShaderProgram == 0) return;

			using var bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

				{
					int labelMargin = 8;
					using var infoBrush = new SolidBrush(ColorInformation);

					// — Information overlay —
					if (SelectedComet != null)
					{
						g.DrawString(SelectedComet.Name, FontInformation, infoBrush, labelMargin, labelMargin);

						if (ShowDistance)
						{
							Ephemeris ep = GetEphemeris(SelectedComet);
							double fs = FontInformation.Size;
							string mstr = String.Format("Magnitude:      {0,5}", ep.Magnitude.ToString("0.00"));
							string dstr = String.Format("Earth Distance: {0,9} AU", ep.EarthDist.ToString("0.000000"));
							string rstr = String.Format("Sun Distance:   {0,9} AU", ep.SunDist.ToString("0.000000"));
							g.DrawString(mstr, FontInformation, infoBrush, labelMargin, Height - labelMargin - (float)(fs * 5.0));
							g.DrawString(dstr, FontInformation, infoBrush, labelMargin, Height - labelMargin - (float)(fs * 3.5));
							g.DrawString(rstr, FontInformation, infoBrush, labelMargin, Height - labelMargin - (float)(fs * 2.0));
						}
					}

					if (ShowDate && ATime != null)
					{
						string strDate = String.Format("{0:00} {1} {2} {3:00}:{4:00}:{5:00} UT",
							ATime.Day, ATime.MonthString, ATime.Year, ATime.Hour, ATime.Minute, ATime.Second);
						float strWidth = g.MeasureString(strDate, FontInformation).Width;
						double fs = FontInformation.Size;
						g.DrawString(strDate, FontInformation, infoBrush,
							Width - strWidth - labelMargin,
							Height - labelMargin - (float)(fs * 2.0));
					}

					// — Axis labels —
					if (ShowAxes && _glLoaded)
					{
						using var grayBrush = new SolidBrush(Color.Gray);
						const double sizeAU = 150.0;
						(Xyz xyz, string label)[] axisLabels = {
						(new Xyz(-sizeAU, 0, 0), "Autumnal equinox"),
						(new Xyz(0, -sizeAU, 0), "Winter solstice"),
						(new Xyz(0, 0, -sizeAU), "South ecliptic pole"),
						(new Xyz( sizeAU, 0, 0), "Vernal equinox"),
						(new Xyz(0,  sizeAU, 0), "Summer solstice"),
						(new Xyz(0, 0,  sizeAU), "North ecliptic pole"),
					};
						foreach (var (xyz, label) in axisLabels)
						{
							Point pt = MvpProject(PseudoPerspective(xyz));
							g.DrawString(label, FontAxisLabel, grayBrush, pt.X, pt.Y);
						}
					}

					// — Planet name labels —
					if (_glLoaded)
					{
						using var planetBrush = new SolidBrush(ColorPlanetName);
						foreach (Object planet in Planets)
						{
							if (Zoom * GetPlanetAU(planet) < 15.0) continue;
							if (PlanetsPos[planet] == null) continue;
							if (!LabelDisplay.Contains(planet)) continue;

							Point pt = MvpProject(PlanetsPos[planet].Value);
							g.DrawString(planet.ToString(), FontPlanetName, planetBrush, pt.X + 5, pt.Y);
						}
					}

					// — Comet name labels —
					if (MtxToEcl != null && _glLoaded)
					{
						bool visibleLabelAll = LabelDisplay.Contains(Object.Comet);
						for (int i = 0; i < Comets.Count && i < CometsPos.Count; i++)
						{
							bool visibleComet = Comets[i].IsVisible;
							bool visibleSelected = PreserveSelectedLabel && i == SelectedIndex;
							bool isCometMarked = Comets[i].IsMarked;
							bool visibleLabel = visibleLabelAll;

							if (!visibleComet)
							{
								if (FilterOnDateShowInWeakColor)
								{
									visibleComet = true;
									if (!visibleSelected)
										visibleLabel = false;
								}
							}

							if (!(visibleComet || visibleSelected || isCometMarked)) continue;
							if (!(visibleLabel || visibleSelected || isCometMarked)) continue;

							Color nameColor = (MultipleMode && Comets.Count > 1 && visibleLabel && visibleSelected)
								? ColorCometNameSelected
								: ColorCometName;

							Point pt = MvpProject(CometsPos[i].Rotate(MtxToEcl));
							using var nameBrush = new SolidBrush(nameColor);
							g.DrawString(Comets[i].Name, FontObjectName, nameBrush, pt.X + 5, pt.Y);
						}
					}
				}
			}

			var bmpData = bmp.LockBits(
				new Rectangle(0, 0, Width, Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.BindTexture(TextureTarget.Texture2D, _textTex);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
				Width, Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
			bmp.UnlockBits(bmpData);

			GL.UseProgram(_textShaderProgram);
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, _textTex);
			GL.Uniform1(_uTextSampler, 0);
			GL.BindVertexArray(_textQuadVao);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
			GL.BindVertexArray(0);
			GL.UseProgram(0);
		}

		#endregion

		#region ClearComets

		public void ClearComets(bool clearAll = false)
		{
			OVComet comet = SelectedComet;

			Comets.Clear();
			SelectedIndex = -1;
			CenteredIndex = -1;

			if (!clearAll && comet != null)
			{
				Comets.Add(comet);
				SelectedIndex = 0;

				UpdatePositions(ATime);
				UpdatePlanetOrbit(ATime);
				UpdateRotationMatrix(ATime);
			}

			_planetVbosNeedUpdate = true;
			_cometVbosDirty = true;
			_cometBatchDrawCount = 0;
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
