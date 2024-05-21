using Comets.Application.Common.General;
using Comets.Application.Edit;
using Comets.Application.Ephemeris;
using Comets.Application.Graph;
using Comets.Application.Help;
using Comets.Application.OrbitViewer;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormMain : ThemeForm
	{
		#region Properties

		private Size InitialFormSize { get; set; }
		private Size CurrentFormSize { get; set; }
		private Point InitialFormLocation { get; set; }
		private Point CurrentFormLocation { get; set; }

		public IProgress<int> Progress { get; set; }

		#endregion

		#region Constructor

		public FormMain()
		{
			InitializeComponent();

			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
			Settings settings = CommonManager.Settings;

			if (settings.RememberWindowPosition && (settings.Left > 0 || settings.Top > 0 || settings.Width > 0 || settings.Height > 0))
			{
				this.Left = settings.Left;
				this.Top = settings.Top;
				this.Width = settings.Width;
				this.Height = settings.Height;
				this.WindowState = CommonManager.Settings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
				this.StartPosition = FormStartPosition.Manual;
			}
			else
			{
				int margin = 250;
				this.Width = Screen.PrimaryScreen.WorkingArea.Width - margin;
				this.Height = Screen.PrimaryScreen.WorkingArea.Height - margin;
				this.StartPosition = FormStartPosition.CenterScreen;
			}

			this.menuItemViewStatusBar.Checked = CommonManager.Settings.ShowStatusBar;
			this.statusStrip.Visible = CommonManager.Settings.ShowStatusBar;
			this.Progress = new Progress<int>(ReportProgress);
		}

		#endregion

		#region +EventHandling

		#region +Form

		#region FormMain_Load

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.DatabaseFilename))
			{
				CometCollection collection = ImportManager.ImportMain(CommonManager.MainCollection, ElementTypesManager.Type.MPC, SettingsManager.DatabaseFilename);
				CommonManager.MainCollection = new CometCollection(collection);
				CommonManager.UserCollection = new CometCollection(collection);
				SetStatusCometsLabel();
			}
		}

		#endregion

		#region FormMain_Shown

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (CommonManager.Settings.AutomaticUpdate && CommonManager.Settings.LastUpdateDate != null)
			{
				int daysLastUpdate = (int)(DateTime.UtcNow - CommonManager.Settings.LastUpdateDate.Value).TotalDays;

				if (daysLastUpdate >= CommonManager.Settings.UpdateInterval)
				{
					if (MessageBox.Show("Last update was " + daysLastUpdate + " days ago. Update now?\t\t",
						"Comets",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Information) == DialogResult.Yes)
					{
						using (FormUpdate fu = new FormUpdate(true) { Owner = this })
						{
							fu.TopMost = this.TopMost;
							fu.OnImportCompleted += this.SetStatusCometsLabel;
							fu.ShowDialog();
						}
					}
				}
			}

			InitialFormSize = CurrentFormSize = this.Size;
			InitialFormLocation = CurrentFormLocation = this.Location;
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
			if (!CommonManager.Settings.ExitWithoutConfirm && this.MdiChildren.Length > 0)
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
			if ((CommonManager.IsDataChanged || !File.Exists(SettingsManager.DatabaseFilename)) && CommonManager.MainCollection.Count > 0)
			{
				string elements = ExportManager.ExportMain(ElementTypesManager.Type.MPC, CommonManager.MainCollection);
				ExportManager.WriteToFile(SettingsManager.DatabaseFilename, elements);
			}

			bool isFormSizeLocationChanged = CurrentFormLocation != InitialFormLocation || CurrentFormSize != InitialFormSize;

			Settings settings = CommonManager.Settings;

			if (settings.RememberWindowPosition && isFormSizeLocationChanged && this.WindowState != FormWindowState.Minimized)
			{
				settings.Maximized = this.WindowState == FormWindowState.Maximized;
				settings.Left = this.Left;
				settings.Top = this.Top;
				settings.Width = this.Width;
				settings.Height = this.Height;
				settings.IsSettingsChanged = true;
			}

			if (settings.IsSettingsChanged)
				SettingsManager.SaveSettings(settings);
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

		#region +MenuItem

		#region Menu: File

		private void menuItemFileEphemerides_Click(object sender, EventArgs e)
		{
			using (FormEphemerisSettings fes = new FormEphemerisSettings(CommonManager.Filters, this.Progress) { Owner = this })
			{
				fes.TopMost = this.TopMost;
				fes.OnProgressBegin += SetProgressMaximumValue;
				fes.OnProgressEnd += HideProgress;
				fes.OnProgressEnd += UpdateMenuItemText;
				fes.ShowDialog();
			}
		}

		private void menuItemFileGraph_Click(object sender, EventArgs e)
		{
			using (FormGraphSettings fgs = new FormGraphSettings(CommonManager.Filters, this.Progress) { Owner = this })
			{
				fgs.TopMost = this.TopMost;
				fgs.OnProgressBegin += SetProgressMaximumValue;
				fgs.OnProgressEnd += HideProgress;
				fgs.OnProgressEnd += UpdateMenuItemText;
				fgs.ShowDialog();
			}
		}

		private void menuItemFileOrbit_Click(object sender, EventArgs e)
		{
			FormOrbitViewer fo = new FormOrbitViewer(CommonManager.UserCollection, CommonManager.Filters, CommonManager.SortProperty, CommonManager.SortAscending);
			fo.OnToolboxVisibleChanged += SetToolBoxMenuItemChecked;
			fo.WindowState = FormWindowState.Maximized;
			fo.MdiParent = this;
			fo.Show();
		}

		private void menuItemOrbitalElements_Click(object sender, EventArgs e)
		{
			FormElements fe = new FormElements();
			fe.WindowState = FormWindowState.Maximized;
			fe.MdiParent = this;
			fe.OnElementsTextWriteEnd += UpdateMenuItemText;
			fe.Show();
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
				using (FormEphemerisSettings fes = new FormEphemerisSettings(CommonManager.Filters, this.Progress, fe.EphemerisSettings) { Owner = this })
				{
					fes.TopMost = this.TopMost;
					fes.OnProgressBegin += SetProgressMaximumValue;
					fes.OnProgressEnd += HideProgress;
					fes.OnProgressEnd += UpdateMenuItemText;
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
				using (FormGraphSettings fgs = new FormGraphSettings(CommonManager.Filters, this.Progress, fg.GraphSettings) { Owner = this })
				{
					fgs.TopMost = this.TopMost;
					fgs.OnProgressBegin += SetProgressMaximumValue;
					fgs.OnProgressEnd += HideProgress;
					fgs.OnProgressEnd += UpdateMenuItemText;
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

		private void mnuShowToolbox_Click(object sender, EventArgs e)
		{
			if (this.ActiveMdiChild is FormOrbitViewer)
			{
				mnuShowToolbox.InvertChecked();
				(this.ActiveMdiChild as FormOrbitViewer).ShowToolbox(mnuShowToolbox.Checked);
			}
		}

		#endregion

		#region Menu: Edit

		private void menuItemDatabase_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(CommonManager.MainCollection, true, CommonManager.Filters, CommonManager.SortProperty, CommonManager.SortAscending, false) { Owner = this })
			{
				fdb.TopMost = this.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					CommonManager.MainCollection = fdb.CometsInitial;
					CommonManager.UserCollection = fdb.CometsFiltered;
					CommonManager.Filters = fdb.Filters;
					CommonManager.SortProperty = fdb.SortProperty;
					CommonManager.SortAscending = fdb.SortAscending;
				}

				SetStatusCometsLabel();
			}
		}

		private void menuItemImport_Click(object sender, EventArgs e)
		{
			using (FormUpdate formImport = new FormUpdate() { Owner = this })
			{
				formImport.TopMost = this.TopMost;
				formImport.OnImportCompleted += this.SetStatusCometsLabel;
				formImport.ShowDialog();
			}
		}

		private void menuItemSettings_Click(object sender, EventArgs e)
		{
			using (FormSettings fs = new FormSettings())
			{
				fs.TopMost = this.TopMost;
				fs.ShowDialog();

				menuItemViewStatusBar.Checked = CommonManager.Settings.ShowStatusBar;
				this.statusStrip.Visible = CommonManager.Settings.ShowStatusBar;
			}
		}

		#endregion

		#region Menu: View

		private void menuItemViewAlwaysOnTop_Click(object sender, EventArgs e)
		{
			this.menuItemViewAlwaysOnTop.InvertChecked();
			this.TopMost = this.menuItemViewAlwaysOnTop.Checked;
		}

		private void menuItemViewStatusBar_Click(object sender, EventArgs e)
		{
			this.menuItemViewStatusBar.InvertChecked();
			CommonManager.Settings.ShowStatusBar = menuItemViewStatusBar.Checked;
			CommonManager.Settings.IsSettingsChanged = true;
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
			this.ActiveMdiChild?.Close();
		}

		#endregion

		#region Menu: Help

		private void menuItemControls_Click(object sender, EventArgs e)
		{
			using (FormControls fc = new FormControls())
			{
				fc.TopMost = this.TopMost;
				fc.ShowDialog();
			}
		}

		private void menuItemAbout_Click(object sender, EventArgs e)
		{
			using (FormAbout fa = new FormAbout())
			{
				fa.TopMost = this.TopMost;
				fa.ShowDialog();
			}
		}

		#endregion

		#endregion

		#region StatusStrip

		private void statusComets_Click(object sender, EventArgs e)
		{
			menuItemDatabase_Click(sender, e);
		}

		#endregion

		#endregion

		#region Methods

		private void CommonSaveAs_Click()
		{
			ISave isave = this.ActiveMdiChild as ISave;
			isave?.Save();
		}

		private void SetStatusCometsLabel()
		{
			int count = CommonManager.UserCollection.Count;
			int total = CommonManager.MainCollection.Count;

			string text = String.Format("Comets: {0}", count);

			if (count < total)
				text += String.Format(" ({0})", total);

			this.statusComets.Text = text;
		}

		private void SetToolBoxMenuItemChecked(bool isChecked)
		{
			this.mnuShowToolbox.Checked = isChecked;
		}

		private void SetProgressMaximumValue(int value)
		{
			statusProgressBar.Visible = true;
			statusProgressBar.Maximum = value;
		}

		private void ReportProgress(int value)
		{
			//http://derekwill.com/2014/06/24/combating-the-lag-of-the-winforms-progressbar/

			if (value >= statusProgressBar.Maximum)
			{
				statusProgressBar.Maximum = value + 1;
				statusProgressBar.Value = value + 1;
				statusProgressBar.Maximum = value;
			}
			else
			{
				statusProgressBar.Value = value + 1;
			}

			statusProgressBar.Value = value;
		}

		private void HideProgress()
		{
			statusProgressBar.Value = statusProgressBar.Minimum;
			statusProgressBar.Visible = false;
		}

		private void UpdateMenuItemText()
		{
			// wrong window item text workaround
			// bug in framework: if active mdi child window text changed, menu item text not updated until another child activated

			ToolStripMenuItem item = menuItemWindow.DropDownItems.OfType<ToolStripMenuItem>().Where(x => x.Checked).SingleOrDefault();
			if (item != null)
			{
				string num = item.Text.Substring(0, item.Text.IndexOf(' '));
				item.Text = $"{num} {this.ActiveMdiChild.Text}";
			}
		}

		#endregion
	}
}
