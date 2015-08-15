using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormDatabase : Form
	{
		#region Const

		public readonly double Minimum = 0;
		public readonly double MaxPerihDist = 15.0;
		public readonly double MaxEcc = 1.2;
		public readonly double MaxLongNode = 359.9999; // i MaxArgPeri
		public readonly double MaxIncl = 179.9999;
		public readonly double MaxPeriod = 9999.0;

		#endregion

		#region Properties

		public List<Comet> Comets { get; private set; }

		public FilterCollection Filters { get; private set; }
		public string SortProperty { get; private set; }
		public bool SortAscending { get; private set; }

		private bool IsTextChangedByFilters { get; set; }

		private ToolTip ToolTip { get; set; }

		private DateTime _dateTime;
		private DateTime DateTime
		{
			get { return _dateTime; }
			set
			{
				_dateTime = value;
				btnDate.Text = _dateTime.ToString(FormMain.DateTimeFormat);
			}
		}

		#endregion

		#region Constructor

		public FormDatabase(List<Comet> list, FilterCollection filters, string sortProperty, bool sortAscending, bool isForFiltering)
		{
			InitializeComponent();

			Comets = list.ToList();
			Filters = filters;

			if (Filters == null)
				DateTime = FormMain.DefaultDateStart;
			else
				DateTime = Filters.T.Value == 0.0 ? FormMain.DefaultDateStart : Utils.JDToDateTime(Filters.T.Value).ToLocalTime();

			SortProperty = sortProperty;
			SortAscending = sortAscending;

			pnlDetails.Visible = !isForFiltering;
			pnlFilters.Visible = isForFiltering;

			ToolTip = new ToolTip();
			ToolTip.SetToolTip(this.txtPerihelionDistance, String.Format("Maximum perihelion distance is {0} AU", MaxPerihDist));
			ToolTip.SetToolTip(this.txtEccentricity, String.Format("Maximum eccentricity value is {0}", MaxEcc));
			ToolTip.SetToolTip(this.txtLongOfAscendingNode, String.Format("Maximum Longitude of Ascending Node value is {0}°", MaxLongNode));
			ToolTip.SetToolTip(this.txtArgumentOfPericenter, String.Format("Maximum Argument of Pericenter value is {0}°", MaxLongNode));
			ToolTip.SetToolTip(this.txtInclination, String.Format("Maximum Inclination value is {0}°", MaxIncl));
			ToolTip.SetToolTip(this.cboPeriod, String.Format("Maximum Period value is {0} years", MaxPeriod));
		}

		#endregion

		#region FormDatabase_Load

		private void FormDatabase_Load(object sender, EventArgs e)
		{
			SetSortItems(SortAscending);
			SortList(Comets);
			PopulateFilters(Filters);
			ApllyContextMenuVisibility();
		}

		#endregion

		#region FormDatabase_KeyDown

		private void FormDatabase_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (pnlFilters.Visible)
					InvertPanelsVisibility();
				else
					this.Close();
			}
		}

		#endregion

		#region FormDatabase_FormClosing

		private void FormDatabase_FormClosing(object sender, FormClosingEventArgs e)
		{
			Filters = CollectFilters();
		}

		#endregion

		#region lbxDatabase_SelectedIndexChanged

		private void lbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			string format6 = "0.000000";
			string format4 = "0.0000";
			int minPeriod = 1000;

			Comet c = Comets.ElementAt(lbxDatabase.SelectedIndex);

			string commonName = c.full;
			string commonPerihDist = c.q.ToString(format6);
			string commonPeriod = c.P < minPeriod ? c.P.ToString(format6) : String.Empty;
			string commonAphDist = c.P < minPeriod ? c.Q.ToString(format6) : String.Empty;

			//info
			txtInfoName.Text = commonName;

			txtInfoPerihDate.Text = Utils.JDToDateTime(c.T).ToLocalTime().ToString(FormMain.DateTimeFormat);
			txtInfoPeriod.Text = commonPeriod;
			txtInfoAphSunDist.Text = commonAphDist;

			txtInfoPerihDist.Text = commonPerihDist;
			txtInfoPerihEarthDist.Text = c.PerihEarthDist.ToString(format6);
			txtInfoPerihMag.Text = c.PerihMag.ToString("0.00");

			txtInfoCurrSunDist.Text = c.CurrentSunDist.ToString(format6);
			txtInfoCurrEarthDist.Text = c.CurrentEarthDist.ToString(format6);
			txtInfoCurrMag.Text = c.CurrentMag.ToString("0.00");

			//orbital elements
			txtElemName.Text = commonName;

			txtElemPerihDate.Text = c.Ty.ToString() + "-" + c.Tm.ToString("00") + "-" + c.Td.ToString("00") + "." + c.Th.ToString("0000");
			txtElemPeriod.Text = commonPeriod;
			txtElemMeanMotion.Text = c.P < minPeriod ? c.n.ToString(format6) : String.Empty;

			txtElemPerihDist.Text = commonPerihDist;
			txtElemAphDist.Text = commonAphDist;
			txtElemSemiMajorAxis.Text = c.P < minPeriod ? c.a.ToString(format6) : String.Empty;

			txtElemEcc.Text = c.e.ToString(format6);
			txtElemAscNode.Text = c.N.ToString(format4);
			txtElemMagG.Text = c.g.ToString("0.0");
			txtElemMagK.Text = c.k.ToString("0.0");

			txtElemIncl.Text = c.i.ToString(format4);
			txtElemArgPeri.Text = c.w.ToString(format4);
			txtElemEquinox.Text = "2000.0";

			//t_sortKey.Text = c.sortkey.ToString("0.00000000000");
			//t_sortKey.Text = c.idKey;
		}

		#endregion

		#region lbxDatabase_MouseDoubleClick

		private void lbxDatabase_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (pnlFilters.Visible)
				InvertPanelsVisibility();
			else
				InvertTabs();
		}

		#endregion

		#region tbcDetails_SelectedIndexChanged

		private void tbcDetails_SelectedIndexChanged(object sender, EventArgs e)
		{
			ApllyContextMenuVisibility();
		}

		#endregion

		#region ApllyContextMenuVisibility

		private void ApllyContextMenuVisibility()
		{
			mnuIncl.Visible = tbcDetails.SelectedIndex == 1;
			mnuEcc.Visible = tbcDetails.SelectedIndex == 1;
			mnuAscNode.Visible = tbcDetails.SelectedIndex == 1;
			mnuArgPeri.Visible = tbcDetails.SelectedIndex == 1;

			mnuPerihEarthDist.Visible = tbcDetails.SelectedIndex == 0 || pnlFilters.Visible;
			mnuCurrSunDist.Visible = tbcDetails.SelectedIndex == 0 || pnlFilters.Visible;
			mnuCurrEarthDist.Visible = tbcDetails.SelectedIndex == 0 || pnlFilters.Visible;
			mnuPerihMag.Visible = tbcDetails.SelectedIndex == 0 || pnlFilters.Visible;
			mnuCurrMag.Visible = tbcDetails.SelectedIndex == 0 || pnlFilters.Visible;
		}

		#endregion

		#region InvertTabs

		private void InvertTabs()
		{
			if (tbcDetails.SelectedIndex == 0)
				tbcDetails.SelectedIndex = 1;
			else
				tbcDetails.SelectedIndex = 0;

			lbxDatabase.Focus();

			ApllyContextMenuVisibility();
		}

		#endregion

		#region InvertPanelsVisibility

		private void InvertPanelsVisibility()
		{
			pnlDetails.Visible = !pnlDetails.Visible;
			pnlFilters.Visible = !pnlFilters.Visible;
		}

		#endregion

		#region + Sort

		#region SetSortItems

		private void SetSortItems(bool isAscending)
		{
			foreach (MenuItem menuitem in contextSort.MenuItems)
			{
				if (menuitem.Tag as string == SortProperty)
				{
					menuitem.Checked = true;
					break;
				}
			}

			mnuAsc.Checked = isAscending;
			mnuDesc.Checked = !isAscending;
		}

		#endregion

		#region btnSort_Click

		private void btnSort_Click(object sender, EventArgs e)
		{
			contextSort.Show(this, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
		}

		#endregion

		#region menuItemSort_Click

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

				SortList(Comets);
			}
		}

		private void menuItemSortAscDesc_Click(object sender, EventArgs e)
		{
			mnuAsc.Checked = !mnuAsc.Checked;
			mnuDesc.Checked = !mnuDesc.Checked;
			SortAscending = mnuAsc.Checked;
			SortList(Comets);
		}

		#endregion

		#region SortList

		public void SortList(List<Comet> list)
		{
			List<Comet> tempList = list.ToList();
			Comets.Clear();

			PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Comet)).Find(SortProperty, false);

			if (prop == null)
				throw new ArgumentException(String.Format("Unknown SortPropertyName: \"{0}\"", SortProperty));

			if (SortAscending)
				Comets = tempList.OrderBy(x => prop.GetValue(x)).ToList();
			else
				Comets = tempList.OrderByDescending(x => prop.GetValue(x)).ToList();

			//clear textboxes if no comets
			if (!Comets.Any())
				foreach (TabPage t in tbcDetails.TabPages)
					foreach (Control c in t.Controls)
						if (c is TextBox)
							c.Text = String.Empty;

			lbxDatabase.DataSource = Comets;
			lbxDatabase.DisplayMember = "full";

			lblTotal.Text = "Comets: " + Comets.Count;
		}

		#endregion

		#endregion

		#region + Filters

		#region btnFilters_Click

		private void btnFilters_Click(object sender, EventArgs e)
		{
			InvertPanelsVisibility();
		}

		#endregion

		#region Date control

		private void btnDate_Click(object sender, EventArgs e)
		{
			using (FormDateTime fdt = new FormDateTime(FormMain.DefaultDateStart, DateTime, GetT()))
			{
				fdt.TopMost = this.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
				{
					DateTime = fdt.SelectedDateTime;
					cbxPerihelionDate.Checked = true;
				}
			}
		}

		private double? GetT()
		{
			double? T = null;

			if (lbxDatabase.SelectedIndex >= 0)
				T = Comets.ElementAt(lbxDatabase.SelectedIndex).T;

			return T;
		}

		#endregion

		#region CollectFilters

		private FilterCollection CollectFilters()
		{
			FilterCollection fc = new FilterCollection();

			fc.full.Checked = cbxName.Checked;
			fc.full.Text = txtName.Text.Trim();
			fc.full.Index = cboName.SelectedIndex;

			fc.T.Checked = cbxPerihelionDate.Checked;
			fc.T.Text = DateTime.ToString(FormMain.DateTimeFormat);
			fc.T.Index = cboPerihelionDate.SelectedIndex;

			fc.q.Checked = cbxPerihelionDistance.Checked;
			fc.q.Text = txtPerihelionDistance.Text;
			fc.q.Index = cboPerihelionDistance.SelectedIndex;

			fc.e.Checked = cbxEccentricity.Checked;
			fc.e.Text = txtEccentricity.Text;
			fc.e.Index = cboEccentricity.SelectedIndex;

			fc.N.Checked = cbxLongOfAscendingNode.Checked;
			fc.N.Text = txtLongOfAscendingNode.Text;
			fc.N.Index = cboLongOfAscendingNode.SelectedIndex;

			fc.w.Checked = cbxArgumentOfPericenter.Checked;
			fc.w.Text = txtArgumentOfPericenter.Text;
			fc.w.Index = cboArgumentOfPericenter.SelectedIndex;

			fc.i.Checked = cbxInclination.Checked;
			fc.i.Text = txtInclination.Text;
			fc.i.Index = cboInclination.SelectedIndex;

			fc.P.Checked = cbxPeriod.Checked;
			fc.P.Text = txtPeriod.Text;
			fc.P.Index = cboPeriod.SelectedIndex;

			return fc;
		}

		#endregion

		#region PopulateFilters

		private void PopulateFilters(FilterCollection Filters)
		{
			IsTextChangedByFilters = true;

			if (Filters == null)
			{
				cbxName.Checked = false;
				cboName.SelectedIndex = 0;
				txtName.Text = String.Empty;

				cbxPerihelionDate.Checked = false;
				cboPerihelionDate.SelectedIndex = 0;
				DateTime = FormMain.DefaultDateStart;

				cbxPerihelionDistance.Checked = false;
				cboPerihelionDistance.SelectedIndex = 1;
				txtPerihelionDistance.Text = String.Empty;

				cbxEccentricity.Checked = false;
				cboEccentricity.SelectedIndex = 1;
				txtEccentricity.Text = String.Empty;

				cbxLongOfAscendingNode.Checked = false;
				cboLongOfAscendingNode.SelectedIndex = 1;
				txtLongOfAscendingNode.Text = String.Empty;

				cbxArgumentOfPericenter.Checked = false;
				cboArgumentOfPericenter.SelectedIndex = 1;
				txtArgumentOfPericenter.Text = String.Empty;

				cbxInclination.Checked = false;
				cboInclination.SelectedIndex = 1;
				txtInclination.Text = String.Empty;

				cbxPeriod.Checked = false;
				cboPeriod.SelectedIndex = 1;
				txtPeriod.Text = String.Empty;
			}
			else
			{
				cbxName.Checked = Filters.full.Checked;
				cboName.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.full.ValueCompare);
				txtName.Text = Filters.full.Text;

				cbxPerihelionDate.Checked = Filters.T.Checked;
				cboPerihelionDate.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.T.ValueCompare);
				if (Filters.T.Value > 0.0)
					DateTime = Utils.JDToDateTime(Filters.T.Value).ToLocalTime();

				cbxPerihelionDistance.Checked = Filters.q.Checked;
				cboPerihelionDistance.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.q.ValueCompare);
				txtPerihelionDistance.Text = Filters.q.Text;

				cbxEccentricity.Checked = Filters.e.Checked;
				cboEccentricity.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.e.ValueCompare);
				txtEccentricity.Text = Filters.e.Text;

				cbxLongOfAscendingNode.Checked = Filters.N.Checked;
				cboLongOfAscendingNode.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.N.ValueCompare);
				txtLongOfAscendingNode.Text = Filters.N.Text;

				cbxArgumentOfPericenter.Checked = Filters.w.Checked;
				cboArgumentOfPericenter.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.w.ValueCompare);
				txtArgumentOfPericenter.Text = Filters.w.Text;

				cbxInclination.Checked = Filters.i.Checked;
				cboInclination.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.i.ValueCompare);
				txtInclination.Text = Filters.i.Text;

				cbxPeriod.Checked = Filters.P.Checked;
				cboPeriod.SelectedIndex = FilterManager.GetIndexFromValueCompare(Filters.P.ValueCompare);
				txtPeriod.Text = Filters.P.Text;
			}

			IsTextChangedByFilters = false;
		}

		#endregion

		#region btnFiltersApply_Click

		private void btnFiltersApply_Click(object sender, EventArgs e)
		{
			Filters = CollectFilters();

			if (FilterManager.ValidateFilters(Filters))
			{
				if (FilterManager.HasAnythingToFilter(Filters))
					Comets = FilterManager.FilterList(FormMain.MainList, Filters);
				else
					Comets = FormMain.MainList.ToList();

				SortList(Comets);
			}
		}

		#endregion

		#region txtFilters_TextChanged

		private void txtFiltersCommon_TextChanged(object sender, EventArgs e)
		{
			if (!IsTextChangedByFilters)
			{
				TextBox txt = sender as TextBox;

				foreach (Control c in txt.Parent.Controls)
				{
					if (c is CheckBox)
					{
						(c as CheckBox).Checked = txt.Text.Trim().Length > 0;
						break;
					}
				}
			}
		}

		#endregion

		#region txtFilters_KeyPress

		private void txtPerihelionDate_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = Utils.TextBoxValueUpDown(sender, e);
		}

		private void txtPerihelionDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		private void txtPerihelionDistance_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 2, 6, Minimum, MaxPerihDist);
		}

		private void txtEccentricity_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 1, 6, Minimum, MaxEcc);
		}

		private void txtFiltersNodePeri_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 4, Minimum, MaxLongNode);
		}

		private void txtInclination_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 4, Minimum, MaxIncl);
		}

		private void txtFiltersPeriod_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 4, 4, Minimum, MaxPeriod);
		}

		#endregion

		#region pnlFilters_VisibleChanged

		private void pnlFilters_VisibleChanged(object sender, EventArgs e)
		{
			this.btnFilters.Text = pnlFilters.Visible ? "Filters ▲" : "Filters ▼";
			this.btnOk.TabStop = this.btnCancel.TabStop = pnlDetails.Visible;
			this.AcceptButton = pnlFilters.Visible ? btnFiltersApply : btnOk;
		}

		#endregion

		#endregion
	}
}
