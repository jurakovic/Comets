using Comets.Application.Common.General;
using Comets.Application.Common.Managers;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
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

		public string SortProperty
		{
			get { return sortMenuControl.SortProperty; }
			set { sortMenuControl.SortProperty = value; }
		}

		public bool SortAscending
		{
			get { return sortMenuControl.SortAscending; }
			set { sortMenuControl.SortAscending = value; }
		}

		#endregion

		#region Constructor

		public FormDatabase(CometCollection collection, bool deleteVisible, FilterCollection filters, string sortProperty, bool sortAscending, bool isForFiltering, bool isForImportResult = false)
		{
			InitializeComponent();

			sortMenuControl.OnSort += this.OnCometsSorted;
			filterControl.OnFilterApply += this.OnFilterApply;
			filterControl.OnClose += this.btnFilters_Click;

			this.btnDelete.Visible = deleteVisible;

			CometsInitial = new CometCollection(collection);
			CometsFiltered = new CometCollection(collection);
			Filters = filters;

			SortProperty = sortProperty;
			SortAscending = sortAscending;

			pnlDetails.Visible = !isForFiltering;
			filterControl.Visible = isForFiltering;

			sortMenuControl.Comets = CometsInitial;
			filterControl.DataBind(Filters);

			if (isForImportResult)
			{
				cbxImportResult.Visible = true;
				cbxImportResult.DataSource = CometManager.ImportResults;
				lbxDatabase.Top = 39;
				lbxDatabase.Height = 368;
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

			//https://stackoverflow.com/q/3144004
			(this.Owner as Form).Activate();
		}

		#endregion

		#region UserControl

		private void filterControl_VisibleChanged(object sender, EventArgs e)
		{
			this.btnFilters.Text = filterControl.Visible ? "FILTERS ▲" : "FILTERS ▼";
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			Comet c = SelectedComet;

			if (c != null && SettingsBase.ValidateCometDelete(c.full))
			{
				int selectedIndex = lbxDatabase.SelectedIndex;

				CometsFiltered.Remove(c);
				CometsInitial.Remove(c);
				CommonManager.IsDataChanged = true;

				sortMenuControl.SortCollection(CometsFiltered);

				lbxDatabase.SelectedIndex = selectedIndex.Range(0, CometsFiltered.Count - 1);
			}
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Filters = null;
			filterControl.DataBind(null);

			SortProperty = CommonManager.DefaultSortProperty;
			SortAscending = CommonManager.DefaultSortAscending;

			ApplyFilters(CometsInitial);
		}

		private void btnFilters_Click(object sender, EventArgs e)
		{
			SwitchVisible();
		}

		#endregion

		#region Event

		private void OnCometsSorted()
		{
			this.CometsFiltered = sortMenuControl.Comets;
			this.SortProperty = sortMenuControl.SortProperty;
			this.SortAscending = sortMenuControl.SortAscending;

			BindCollection();
		}

		private void OnFilterApply()
		{
			ApplyFilters(CometsInitial);
		}

		#endregion

		#endregion

		#region +Methods

		#region BindCollection

		private void BindCollection()
		{
			if (CometsFiltered.Count == 0)
			{
				//clear textboxes if no comets
				ephemerisControl.StopTimer();
				elementsControl.ClearData();
				ephemerisControl.ClearData();
			}

			lbxDatabase.DataSource = CometsFiltered;
			lbxDatabase.DisplayMember = PropertyEnum.full.ToString();

			int count = CometsFiltered.Count;
			int total = CometsInitial.Count;

			string text = String.Format("Database ({0}", count);

			if (count < total)
				text += String.Format("/{0}", total);

			text += " comets)";

			this.Text = text;
		}

		#endregion

		#region Switch

		private void SwitchTab()
		{
			tbcDetails.SelectedIndex = tbcDetails.SelectedIndex == 0 ? 1 : 0;
			lbxDatabase.Focus();
		}

		private void SwitchVisible()
		{
			pnlDetails.InvertVisible();
			filterControl.InvertVisible();
			lbxDatabase.Focus();
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
			sortMenuControl.SortCollection(CometsFiltered);
		}

		#endregion

		#endregion
	}
}
