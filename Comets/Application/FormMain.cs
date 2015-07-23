using Comets.Application.ModulEphemeris;
using Comets.Application.ModulGraph;
using Comets.Application.ModulOrbit;
using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormMain : Form
	{
		#region Const

		public static string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
		public static string DefaultSortProperty = "sortkey";
		public static bool DefaultSortAscending = true;

		#endregion

		#region Properties

		public static List<Comet> MainList { get; set; }
		public static List<Comet> UserList { get; set; }
		public static bool IsDataChanged { get; set; }

		public static FilterCollection Filters { get; set; }
		public static string SortProperty { get; set; }
		public static bool SortAscending { get; set; }

		public static Settings Settings { get; set; }

		public static DateTime DefaultDateStart { get; private set; }
		public static DateTime DefaultDateEnd { get; private set; }

		private Size InitialFormSize { get; set; }
		private Size CurrentFormSize { get; set; }
		private Point InitialFormLocation { get; set; }
		private Point CurrentFormLocation { get; set; }

		#endregion

		#region Constructor

		public FormMain()
		{
			InitializeComponent();

			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			MainList = new List<Comet>();
			UserList = new List<Comet>();

			SortProperty = DefaultSortProperty;
			SortAscending = DefaultSortAscending;

			Settings = SettingsManager.LoadSettings();

			DateTime dt = DateTime.Now.AddMonths(-1);
			DefaultDateStart = new DateTime(dt.Year, dt.Month, 1, 22, 00, 00, DateTimeKind.Local);
			dt = DateTime.Now.AddMonths(1);
			DefaultDateEnd = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 22, 00, 00, DateTimeKind.Local);

			if (Settings.RememberWindowPosition)
			{
				int margin = 250;
				this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
				this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
				this.WindowState = Settings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
			}

			this.menuItemViewStatusBar.Checked = Settings.ShowStatusBar;
			this.statusStrip.Visible = Settings.ShowStatusBar;
		}

		#endregion

		#region + FormMain Events

		#region FormMain_Load

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (Settings.RememberWindowPosition && (Settings.Left > 0 || Settings.Top > 0 || Settings.Width > 0 || Settings.Height > 0))
			{
				this.Left = Settings.Left;
				this.Top = Settings.Top;
				this.Width = Settings.Width;
				this.Height = Settings.Height;
				this.StartPosition = FormStartPosition.Manual;
			}

			if (File.Exists(SettingsManager.Database))
			{
				List<Comet> list = ImportManager.ImportMain(MainList, ElementTypes.Type.MPC, SettingsManager.Database, true);
				MainList = list.ToList();
				UserList = list.ToList();
				SetStatusCometsLabel(UserList.Count, MainList.Count);
			}
		}

		#endregion

		#region FormMain_Shown

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (Settings.AutomaticUpdate && Settings.LastUpdateDate != null)
			{
				int daysLastUpdate = (int)(DateTime.Now - Settings.LastUpdateDate.Value).TotalDays;

				if (daysLastUpdate >= Settings.UpdateInterval)
				{
					if (MessageBox.Show("Last update was " + daysLastUpdate + " days ago. Update now?\t\t",
						"Comets",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Information) == DialogResult.Yes)
					{
						using (FormImport fi = new FormImport(true) { Owner = this })
						{
							fi.ShowDialog();
							SetStatusCometsLabel(UserList.Count, MainList.Count);
						}
					}
				}
			}

			InitialFormSize = this.Size;
			CurrentFormSize = this.Size;
			InitialFormLocation = this.Location;
			CurrentFormLocation = this.Location;
		}

		#endregion

		#region FormMain_Resize

		private void FormMain_Resize(object sender, EventArgs e)
		{
			CurrentFormSize = this.Size;
		}

		#endregion

		#region FormMain_Move

		private void FormMain_Move(object sender, EventArgs e)
		{
			CurrentFormLocation = this.Location;
		}

		#endregion

		#region FormMain_FormClosing

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!FormMain.Settings.ExitWithoutConfirm && this.MdiChildren.Any())
			{
				e.Cancel = MessageBox.Show(
					"Do you really want to exit?\t\t\t\t",
					"Confirm",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.No;
			}
		}

		#endregion

		#region FormMain_FormClosed

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			if ((IsDataChanged || !File.Exists(SettingsManager.Database) || Settings.IsSettingsChanged) && MainList.Any())
			{
				ExporManager.ExportMain(ElementTypes.Type.MPC, SettingsManager.Database, MainList);
			}

			bool isFormSizeLocationChanged = CurrentFormLocation != InitialFormLocation || CurrentFormSize != InitialFormSize;

			if (Settings.RememberWindowPosition && isFormSizeLocationChanged && this.WindowState != FormWindowState.Minimized)
			{
				Settings.Maximized = this.WindowState == FormWindowState.Maximized;
				Settings.Left = this.Left;
				Settings.Top = this.Top;
				Settings.Width = this.Width;
				Settings.Height = this.Height;
				Settings.IsSettingsChanged = true;
			}

			if (Settings.IsSettingsChanged || Settings.IsDirty)
				SettingsManager.SaveSettings(Settings);
		}

		#endregion

		#region FormMain_MdiChildActivate

		private void FormMain_MdiChildActivate(object sender, EventArgs e)
		{
			this.menuItemWindow.Visible = this.ActiveMdiChild != null;

			this.menuItemEphemeris.Visible = this.ActiveMdiChild is FormEphemeris;
			this.menuItemGraph.Visible = this.ActiveMdiChild is FormGraph;
			this.menuItemOrbit.Visible = this.ActiveMdiChild is FormOrbitViewer;
		}

		#endregion

		#endregion

		#region Menu: File

		private void menuItemFileEphemerides_Click(object sender, EventArgs e)
		{
			using (FormEphemerisSettings fes = new FormEphemerisSettings(Filters) { Owner = this })
			{
				fes.TopMost = this.TopMost;
				fes.ShowDialog();
			}
		}

		private void menuItemFileGraph_Click(object sender, EventArgs e)
		{
			using (FormGraphSettings fgs = new FormGraphSettings(Filters) { Owner = this })
			{
				fgs.TopMost = this.TopMost;
				fgs.ShowDialog();
			}
		}

		private void menuItemFileOrbit_Click(object sender, EventArgs e)
		{
			FormOrbitViewer fo = new FormOrbitViewer(UserList);
			fo.WindowState = FormWindowState.Maximized;
			fo.MdiParent = this;
			fo.Show();
		}

		private void menuItemExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Menu: Ephemeris

		private void menuItemEphemSettings_Click(object sender, EventArgs e)
		{
			if (this.ActiveMdiChild is FormEphemeris)
			{
				FormEphemeris fe = this.ActiveMdiChild as FormEphemeris;
				using (FormEphemerisSettings fes = new FormEphemerisSettings(Filters, fe.EphemerisSettings) { Owner = this })
				{
					fes.TopMost = this.TopMost;
					fes.ShowDialog();
				}
			}
		}

		private void menuItemEphemerisSaveAs_Click(object sender, EventArgs e)
		{
			CommonSaveAs_Click();
		}

		#endregion

		#region Menu: Graph

		private void menuItemGraphSettings_Click(object sender, EventArgs e)
		{
			if (this.ActiveMdiChild is FormGraph)
			{
				FormGraph fg = this.ActiveMdiChild as FormGraph;
				using (FormGraphSettings fgs = new FormGraphSettings(Filters, fg.GraphSettings) { Owner = this })
				{
					fgs.TopMost = this.TopMost;
					fgs.ShowDialog();
				}
			}
		}

		private void menuItemGraphSaveAs_Click(object sender, EventArgs e)
		{
			CommonSaveAs_Click();
		}

		#endregion

		#region Menu: Orbit

		private void menuItemOrbitMultiple_Click(object sender, EventArgs e)
		{
			(sender as MenuItem).Checked = !(sender as MenuItem).Checked;
			(this.ActiveMdiChild as FormOrbitViewer).Settings = CollectOrbitSettings();
		}

		private void menuItemOrbitClearComets_Click(object sender, EventArgs e)
		{
			(this.ActiveMdiChild as FormOrbitViewer).ClearComets();
		}

		private OrbitViewerSettings CollectOrbitSettings()
		{
			OrbitViewerSettings ovs = new OrbitViewerSettings();

			ovs.MultipleMode = this.menuItemOrbitMultiple.Checked;
			ovs.ShowAxes = this.menuItemOrbitShowAxes.Checked;
			ovs.Antialiasing = this.menuItemOrbitAntialiasing.Checked;
			ovs.ShowCometName = this.menuItemOrbitComet.Checked;
			ovs.ShowPlanetName = this.menuItemOrbitPlanet.Checked;
			ovs.ShowMagnitute = this.menuItemOrbitMagnitude.Checked;
			ovs.ShowDistance = this.menuItemOrbitDistance.Checked;
			ovs.ShowDate = this.menuItemOrbitDate.Checked;

			return ovs;
		}

		private void menuItemOrbitSaveImage_Click(object sender, EventArgs e)
		{
			CommonSaveAs_Click();
		}

		#endregion

		#region Menu: Edit

		private void menuItemDatabase_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(UserList, Filters, SortProperty, SortAscending, false) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					UserList = fdb.Comets;
					Filters = fdb.Filters;
					SortProperty = fdb.SortProperty;
					SortAscending = fdb.SortAscending;
				}

				SetStatusCometsLabel(UserList.Count, MainList.Count);
			}
		}

		private void menuItemImport_Click(object sender, EventArgs e)
		{
			using (FormImport formImport = new FormImport() { Owner = this })
			{
				formImport.TopMost = this.TopMost;
				formImport.ShowDialog();
			}
		}

		private void menuItemExport_Click(object sender, EventArgs e)
		{
			using (FormExport formExport = new FormExport() { Owner = this })
			{
				formExport.TopMost = this.TopMost;
				formExport.ShowDialog();
			}
		}

		private void menuItemSettings_Click(object sender, EventArgs e)
		{
			using (FormSettings fs = new FormSettings())
			{
				fs.TopMost = this.TopMost;
				fs.ShowDialog();

				menuItemViewStatusBar.Checked = Settings.ShowStatusBar;
				this.statusStrip.Visible = Settings.ShowStatusBar;
			}
		}

		#endregion

		#region Menu: View

		private void menuItemViewAlwaysOnTop_Click(object sender, EventArgs e)
		{
			this.menuItemViewAlwaysOnTop.Checked = !this.menuItemViewAlwaysOnTop.Checked;
			this.TopMost = this.menuItemViewAlwaysOnTop.Checked;
		}

		private void menuItemViewStatusBar_Click(object sender, EventArgs e)
		{
			menuItemViewStatusBar.Checked = !menuItemViewStatusBar.Checked;
			Settings.ShowStatusBar = menuItemViewStatusBar.Checked;
			this.statusStrip.Visible = menuItemViewStatusBar.Checked;
		}

		#endregion

		#region Menu: Window

		private void menuItemTileHoriz_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItemTileVert_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void menuItemCascade_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuItemMinimizeAll_Click(object sender, EventArgs e)
		{
			foreach (Form child in this.MdiChildren)
				child.WindowState = FormWindowState.Minimized;
		}

		private void menuItemRestoreAll_Click(object sender, EventArgs e)
		{
			foreach (Form child in this.MdiChildren)
				child.WindowState = FormWindowState.Normal;
		}

		private void menuItemClose_Click(object sender, EventArgs e)
		{
			if (this.ActiveMdiChild != null)
				this.ActiveMdiChild.Close();
		}

		#endregion

		#region Methods

		private void CommonSaveAs_Click()
		{
			if (this.ActiveMdiChild is FormEphemeris)
				(this.ActiveMdiChild as FormEphemeris).SaveEphemeris();

			else if (this.ActiveMdiChild is FormGraph)
				(this.ActiveMdiChild as FormGraph).SaveGraph();

			else if (this.ActiveMdiChild is FormOrbitViewer)
				(this.ActiveMdiChild as FormOrbitViewer).SaveImage();
		}

		private void SetStatusCometsLabel(int count, int total)
		{
			if (count < total)
				this.statusComets.Text = String.Format("Comets: {0} ({1})", count, total);
			else
				this.statusComets.Text = String.Format("Comets: {0}", count);
		}

		public void SetOrbitMenuItems(OrbitViewerSettings settings)
		{
			this.menuItemOrbitMultiple.Checked = settings.MultipleMode;
			this.menuItemOrbitShowAxes.Checked = settings.ShowAxes;
			this.menuItemOrbitAntialiasing.Checked = settings.Antialiasing;
			this.menuItemOrbitComet.Checked = settings.ShowCometName;
			this.menuItemOrbitPlanet.Checked = settings.ShowPlanetName;
			this.menuItemOrbitMagnitude.Checked = settings.ShowMagnitute;
			this.menuItemOrbitDistance.Checked = settings.ShowDistance;
			this.menuItemOrbitDate.Checked = settings.ShowDate;
		}

		#endregion
	}
}
