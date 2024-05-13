using OpenTK.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Formats.Asn1.AsnWriter;
using System;
using THREE;
using OpenTK;
using OpenTK.WinForms;
using System.Diagnostics;
using OpenTK.Graphics.ES30;

namespace Comets.Application.Application
{
	public partial class FormOV3D : Form
	{
		Example example = null;// new FirstSceneExample();
		private System.Windows.Forms.Timer _timer;
		private int timeInterval = 10;


		public FormOV3D()
		{
			InitializeComponent();
		}


		private void glControl_Load(object sender, EventArgs e)
		{
			RunSample();

#if NET6_0_OR_GREATER
			this.glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
#endif
		}

		private void glControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (example != null)
				example.KeyDown(e.KeyCode);
		}

		private void glControl_Paint(object sender, PaintEventArgs e)
		{
			this.Render();
		}

		private void glControl_Resize(object sender, EventArgs e)
		{
			var control = sender as Control;

			if (control.ClientSize.Height == 0)
				control.ClientSize = new Size(control.ClientSize.Width, 1);

			GL.Viewport(0, 0, control.ClientSize.Width, control.ClientSize.Height);

			if (example != null)
				example.Resize(control.ClientSize);
		}

		private void FormOV3D_Load(object sender, EventArgs e)
		{

		}

		private void Render()
		{
			this.glControl.MakeCurrent();
			if (null != example)
				example.Render();

			//stats.update();

			this.glControl.SwapBuffers();
		}

		private void FormOV3D_Shown(object sender, EventArgs e)
		{
			RunSample();
		}

		void RunSample()
		{
			if (null != example)
			{
				example.Unload();

				example = null;

				if (_timer != null)
				{
					_timer.Stop();
					_timer.Dispose();
				}
			}

			example = new FirstSceneExample();
			if (null != example)
			{
				this.glControl.MakeCurrent();

				example.Load(this.glControl);

				_timer = new System.Windows.Forms.Timer();
				_timer.Interval = timeInterval;
				_timer.Tick += (sender, e) =>
				{
					Render();
				};
				_timer.Start();


				GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
				example.Resize(glControl.ClientSize);
			}
		}

		private void FormOV3D_FormClosed(object sender, FormClosedEventArgs e)
		{
			example.Unload();

			example = null;

			if (_timer != null)
			{
				_timer.Stop();
				_timer.Dispose();
			}
		}
	}

	abstract public class Example
	{
		public GLRenderer renderer;
		protected readonly Random random = new Random();
		protected readonly Stopwatch stopWatch = new Stopwatch();
		public GLControl glControl;

		public virtual void Load(GLControl control)
		{
			Debug.Assert(null != control);

			glControl = control;
			this.renderer = new THREE.GLRenderer();

			this.renderer.Context = control.Context;
			this.renderer.Width = control.Width;
			this.renderer.Height = control.Height;

			this.renderer.Init();

			stopWatch.Start();
		}

		public virtual void Resize(Size clientSize)
		{
			if (renderer != null)
				renderer.Resize(clientSize.Width, clientSize.Height);
		}

		public virtual void MouseUp(Size clientSize, Point point)
		{

		}

		public virtual void MouseWheel(Size clientSize, Point point, int delta)
		{

		}

		public virtual void MouseMove(Size clientSize, Point point)
		{

		}

		public virtual void MouseDown(Size clientSize, Point point)
		{

		}

		public abstract void Render();

		public virtual void Unload()
		{
			this.renderer.Dispose();
		}

		public virtual void KeyDown(Keys keyCode)
		{

		}
		public virtual void KeyUp(Keys keyCode)
		{

		}
	}

	public class FirstSceneExample : Example
	{
		Scene scene;
		Camera camera;
		TrackballControls controls;

		public FirstSceneExample() : base()
		{
			camera = new PerspectiveCamera();
			scene = new Scene();
		}

		private void InitRenderer()
		{
			this.renderer.SetClearColor(new THREE.Color().SetHex(0x000000));
			this.renderer.ShadowMap.Enabled = true;
			this.renderer.ShadowMap.type = Constants.PCFSoftShadowMap;
		}

		private void InitCamera()
		{
			camera.Fov = 45.0f;
			camera.Aspect = this.glControl.AspectRatio;
			camera.Near = 0.1f;
			camera.Far = 1000.0f;
			camera.Position.X = -30;
			camera.Position.Y = 50;
			camera.Position.Z = 40;
			camera.LookAt(THREE.Vector3.Zero());
		}

		private void InitCameraController()
		{
			controls = new TrackballControls(this.glControl, camera);
			controls.StaticMoving = false;
			controls.RotateSpeed = 3.0f;
			controls.ZoomSpeed = 2;
			controls.PanSpeed = 2;
			controls.NoZoom = false;
			controls.NoPan = false;
			controls.NoRotate = false;
			controls.StaticMoving = true;
			controls.DynamicDampingFactor = 0.2f;
		}

		public override void Load(GLControl glControl)
		{
			base.Load(glControl);

			InitRenderer();

			InitCamera();

			InitCameraController();

			scene.Background = THREE.Color.Hex(0xffffff);

			var axes = new AxesHelper(20);
			scene.Add(axes);

			var planeGeometry = new PlaneGeometry(60, 20);
			var planeMaterial = new MeshBasicMaterial() { Color = THREE.Color.Hex(0xcccccc) };
			var plane = new Mesh(planeGeometry, planeMaterial);

			plane.Rotation.X = (float)(-0.5 * Math.PI);
			plane.Position.Set(15, 0, 0);

			scene.Add(plane);

			// create a cube
			var cubeGeometry = new BoxGeometry(4, 4, 4);
			var cubeMaterial = new MeshBasicMaterial() { Color = THREE.Color.Hex(0xff0000), Wireframe = true };
			var cube = new Mesh(cubeGeometry, cubeMaterial);

			// position the cube
			cube.Position.Set(-4, 3, 0);

			// add the cube to the scene

			scene.Add(cube);

			//      // create a sphere
			var sphereGeometry = new SphereGeometry(4, 20, 20);
			var sphereMaterial = new MeshBasicMaterial() { Color = THREE.Color.Hex(0x7777ff), Wireframe = true };
			var sphere = new Mesh(sphereGeometry, sphereMaterial);

			//      // position the sphere
			sphere.Position.Set(20, 4, 2);

			//      // add the sphere to the scene
			scene.Add(sphere);
		}

		public override void Render()
		{
			controls.Update();
			this.renderer.Render(scene, camera);
		}

		public override void Resize(System.Drawing.Size clientSize)
		{
			base.Resize(clientSize);
			camera.Aspect = this.glControl.AspectRatio;
			camera.UpdateProjectionMatrix();
		}
	}
}
