using Comets.Application.Common.General;
using Comets.Application.Common.Managers;
using Comets.Core;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.Application
{
	public partial class FormDatabase : Form
	{
		#region Properties

		public CometCollection CometsInitial { get; private set; }
		public CometCollection CometsFiltered { get; private set; }

		public Comet SelectedComet
		{
			get { return CometsFiltered.ElementAtOrDefault(lbxDatabase.SelectedIndex); }
		}

		public FilterCollection Filters { get; private set; }
		public string SortProperty { get; private set; }
		public bool SortAscending { get; private set; }

		#endregion

		#region Constructor

		public FormDatabase(CometCollection collection, FilterCollection filters, string sortProperty, bool sortAscending, bool isForFiltering, bool isForImportResult = false)
		{
			InitializeComponent();

			filterControl.OnFilterApply += this.OnFilterApply;
			filterControl.OnClose += this.btnFilters_Click;

			this.mnuDesig.Tag = PropertyEnum.sortkey.ToString();
			this.mnuDiscoverer.Tag = PropertyEnum.name.ToString();
			this.mnuPerihDate.Tag = PropertyEnum.Tn.ToString();
			this.mnuPerihDist.Tag = PropertyEnum.q.ToString();
			this.mnuPerihEarthDist.Tag = PropertyEnum.PerihEarthDist.ToString();
			this.mnuPerihMag.Tag = PropertyEnum.PerihMag.ToString();
			this.mnuCurrSunDist.Tag = PropertyEnum.CurrentSunDist.ToString();
			this.mnuCurrEarthDist.Tag = PropertyEnum.CurrentEarthDist.ToString();
			this.mnuCurrMag.Tag = PropertyEnum.CurrentMag.ToString();
			this.mnuPeriod.Tag = PropertyEnum.P.ToString();
			this.mnuAphDistance.Tag = PropertyEnum.Q.ToString();
			this.mnuSemiMajorAxis.Tag = PropertyEnum.a.ToString();
			this.mnuEcc.Tag = PropertyEnum.e.ToString();
			this.mnuIncl.Tag = PropertyEnum.i.ToString();
			this.mnuAscNode.Tag = PropertyEnum.N.ToString();
			this.mnuArgPeri.Tag = PropertyEnum.w.ToString();

			CometsInitial = new CometCollection(collection);
			CometsFiltered = new CometCollection(collection);
			Filters = filters;

			SortProperty = sortProperty;
			SortAscending = sortAscending;

			pnlDetails.Visible = !isForFiltering;
			filterControl.Visible = isForFiltering;

			filterControl.DataBind(Filters);
			SetSortItems(SortAscending);

			if (isForImportResult)
			{
				cbxImportResult.Visible = true;
				cbxImportResult.DataSource = CometManager.ImportResults;
				lbxDatabase.Top = 53;
				lbxDatabase.Height = 354;
			}
			else
			{
				cbxImportResult.Visible = false;
				lbxDatabase.Top = 11;
				lbxDatabase.Height = 396;
			}
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormDatabase_Load(object sender, EventArgs e)
		{
			ApplyFilters(CometsFiltered);
		}

		private void FormDatabase_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (filterControl.Visible)
					SwitchVisible();
				else
					this.Close();
			}
		}

		private void FormDatabase_FormClosing(object sender, FormClosingEventArgs e)
		{
			Filters = filterControl.CollectFilters();
			ephemerisControl.DisposeTimer();
		}

		#endregion

		#region UserControl

		private void filterControl_VisibleChanged(object sender, EventArgs e)
		{
			this.btnFilters.Text = filterControl.Visible ? "Filters ▲" : "Filters ▼";
			this.btnOk.TabStop = this.btnCancel.TabStop = pnlDetails.Visible;
			this.AcceptButton = filterControl.Visible ? filterControl.AcceptButton : btnOk;
		}

		#endregion

		#region ListBox

		private void lbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			ephemerisControl.DataBind(SelectedComet);
			elementsControl.DataBind(SelectedComet);
			ephemerisControl.StartTimer();
		}

		private void lbxDatabase_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (filterControl.Visible)
				SwitchVisible();
			else
				SwitchTab();
		}

		#endregion

		#region ComboBox

		private void cbxImportResult_SelectedIndexChanged(object sender, EventArgs e)
		{
			CometManager.ImportResult result = (CometManager.ImportResult)cbxImportResult.SelectedIndex;
			CometsFiltered = new CometCollection(CometsInitial.Where(x => x.ImportResult == result || result == CometManager.ImportResult.All));
			ApplyFilters(CometsFiltered);
		}

		#endregion

		#region Button

		private void btnSort_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			contextSort.Show(this, new Point(btn.Left + 1, btn.Top + btn.Height - 1));
		}

		private void btnResetAllFilters_Click(object sender, EventArgs e)
		{
			Filters = null;
			filterControl.DataBind(null);

			SortProperty = CommonManager.DefaultSortProperty;
			SortAscending = CommonManager.DefaultSortAscending;

			SetSortItems(SortAscending);
			ApplyFilters(CometsInitial);

			ephemerisControl.StopTimer();
			ephemerisControl.StartTimer();
		}

		private void btnFilters_Click(object sender, EventArgs e)
		{
			SwitchVisible();
		}

		#endregion

		#region ContextMenu

		private void menuItemSortCommon_Click(object sender, EventArgs e)
		{
			MenuItem mni = sender as MenuItem;

			if (!mni.Checked)
			{
				SortAscending = mnuAsc.Checked;

				foreach (MenuItem item in contextSort.MenuItems)
					item.Checked = false;

				mni.Checked = true;
				SortProperty = mni.Tag as string;

				mnuAsc.Checked = SortAscending;
				mnuDesc.Checked = !SortAscending;

				SortCollection();
			}
		}

		private void menuItemSortAscDesc_Click(object sender, EventArgs e)
		{
			mnuAsc.Checked = !mnuAsc.Checked;
			mnuDesc.Checked = !mnuDesc.Checked;
			SortAscending = mnuAsc.Checked;
			SortCollection();
		}

		#endregion

		#region Event

		private void OnFilterApply()
		{
			ApplyFilters(CometsInitial);
		}

		#endregion

		#endregion

		#region +Methods

		#region Switch

		private void SwitchTab()
		{
			tbcDetails.SelectedIndex = tbcDetails.SelectedIndex == 0 ? 1 : 0;
			lbxDatabase.Focus();
		}

		private void SwitchVisible()
		{
			pnlDetails.Visible = !pnlDetails.Visible;
			filterControl.Visible = !filterControl.Visible;
			lbxDatabase.Focus();
		}

		#endregion

		#region Sort

		private void SetSortItems(bool isAscending)
		{
			foreach (MenuItem menuitem in contextSort.MenuItems)
			{
				if (menuitem.Tag as string == SortProperty)
					menuitem.Checked = true;
				else
					menuitem.Checked = false;
			}

			mnuAsc.Checked = isAscending;
			mnuDesc.Checked = !isAscending;
		}

		private void SortCollection()
		{
			CometCollection temp = new CometCollection(CometsFiltered);
			CometsFiltered.Clear();

			PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Comet)).Find(SortProperty, false);

			if (prop == null)
				throw new ArgumentException(String.Format("Unknown SortPropertyName: \"{0}\"", SortProperty));

			CometsFiltered = SortAscending
				? new CometCollection(temp.OrderBy(x => prop.GetValue(x)))
				: new CometCollection(temp.OrderByDescending(x => prop.GetValue(x)));

			if (CometsFiltered.Count == 0)
			{
				//clear textboxes if no comets
				ephemerisControl.StopTimer();
				elementsControl.ClearData();
				ephemerisControl.ClearData();
			}

			lbxDatabase.DataSource = CometsFiltered;
			lbxDatabase.DisplayMember = PropertyEnum.full.ToString();

			lblTotal.Text = "Comets: " + CometsFiltered.Count;
		}

		#endregion

		#region Filters

		public void ApplyFilters(CometCollection colletion)
		{
			Filters = filterControl.CollectFilters();

			string message = FilterPanelManager.ValidateFilters(Filters);

			if (message != null)
				throw new ValidationException(message);

			CometsFiltered = FilterManager.ApplyFilters(colletion, Filters);
			SortCollection();
		}

		#endregion

		#endregion
	}
}
