using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
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
		private bool IsFilterText { get; set; }
		ToolTip ToolTip { get; set; }
		private string SortPropertyName { get; set; }

		#endregion

		#region Constructor

		public FormDatabase(List<Comet> list, FilterCollection filters, bool isForFiltering)
		{
			InitializeComponent();

			Comets = list.ToList();
			Filters = filters;

			SortPropertyName = "sortkey";

			pnlDetails.Visible = !isForFiltering;
			pnlFilters.Visible = isForFiltering;

			txtPerihelionDateD.Tag = LeMiMa.LDay;
			txtPerihelionDateM.Tag = LeMiMa.LMonth;
			txtPerihelionDateY.Tag = LeMiMa.LYear;

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
			SortList(Comets);
			PopulateFilters(Filters);
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
			int ind = lbxDatabase.SelectedIndex;
			Comet c = Comets.ElementAt(ind);

			t_full.Text = c.full;
			t_T.Text = c.Ty.ToString() + "-" + c.Tm.ToString("00") + "-" + c.Td.ToString("00") + "." + c.Th.ToString("0000");
			t_q1.Text = c.q.ToString("0.000000");
			t_e.Text = c.e.ToString("0.000000");
			t_i.Text = c.i.ToString("0.0000");
			t_N1.Text = c.N.ToString("0.0000");
			t_w.Text = c.w.ToString("0.0000");

			if (c.P < 10000)
			{
				t_P.Text = c.P.ToString("0.000000");
				t_Q2.Text = c.Q.ToString("0.000000");
				t_a.Text = c.a.ToString("0.000000");
				t_n2.Text = c.n.ToString("0.000000");
			}
			else
			{
				t_P.Text = String.Empty;
				t_Q2.Text = String.Empty;
				t_a.Text = String.Empty;
				t_n2.Text = String.Empty;
			}

			t_g.Text = c.g.ToString("0.0");
			t_k.Text = c.k.ToString("0.0");

			//t_sortKey.Text = c.sortkey.ToString("0.00000000000");
			//t_sortKey.Text = c.idKey;

			tEquinox.Text = "2000.0";
		}

		#endregion

		#region lbxDatabase_MouseDoubleClick

		private void lbxDatabase_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			pnlFilters.Visible = false;
			pnlDetails.Visible = true;
		}

		#endregion

		#region Sort

		#region btnSort_Click

		private void btnSort_Click(object sender, EventArgs e)
		{
			contextSort.Show(this, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
		}

		#endregion

		#region menuItemSort_Click

		private void menuItemSort_Click(object sender, EventArgs e)
		{
			MenuItem mni = sender as MenuItem;

			if (!mni.Checked)
			{
				bool order = menuItemAsc.Checked;

				foreach (MenuItem item in contextSort.MenuItems)
					item.Checked = false;

				mni.Checked = true;
				SortPropertyName = mni.Tag as string;

				menuItemAsc.Checked = order;
				menuItemDesc.Checked = !order;

				SortList(Comets);
			}
		}

		private void menuItemAsc_Click(object sender, EventArgs e)
		{
			menuItemAsc.Checked = true;
			menuItemDesc.Checked = false;
			SortList(Comets);
		}

		private void menuItemDesc_Click(object sender, EventArgs e)
		{
			menuItemAsc.Checked = false;
			menuItemDesc.Checked = true;
			SortList(Comets);
		}

		#endregion

		#region SortList

		public void SortList(List<Comet> list)
		{
			List<Comet> tempList = list.ToList();
			Comets.Clear();

			PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Comet)).Find(SortPropertyName, false);

			if (prop == null)
				throw new ArgumentException(String.Format("Unknown SortPropertyName: \"{0}\"", SortPropertyName));

			if (menuItemAsc.Checked)
				Comets = tempList.OrderBy(x => prop.GetValue(x)).ToList();
			else
				Comets = tempList.OrderByDescending(x => prop.GetValue(x)).ToList();

			//clear textboxes if no comets
			if (!Comets.Any())
				foreach (Control c in gbDetails.Controls)
					if (c is TextBox)
						c.Text = String.Empty;

			lbxDatabase.DataSource = Comets;
			lbxDatabase.DisplayMember = "full";

			lblTotal.Text = "Comets: " + Comets.Count;
		}

		#endregion

		#endregion

		#region Filters

		#region btnFilters_Click

		private void btnFilters_Click(object sender, EventArgs e)
		{
			pnlDetails.Visible = !pnlDetails.Visible;
			pnlFilters.Visible = !pnlFilters.Visible;
		}

		#endregion

		#region CollectFilters

		private FilterCollection CollectFilters()
		{
			FilterCollection fc = new FilterCollection();

			fc.Name.Checked = cbxName.Checked;
			fc.Name.Text = txtName.Text.Trim();
			fc.Name.Index = cboName.SelectedIndex;

			fc.PerihelionDate.Checked = cbxPerihelionDate.Checked;
			fc.PerihelionDate.Text = txtPerihelionDateD.Text + "." + txtPerihelionDateM.Text + "." + txtPerihelionDateY.Text;
			fc.PerihelionDate.Index = cboPerihelionDate.SelectedIndex;

			fc.PerihelionDistance.Checked = cbxPerihelionDistance.Checked;
			fc.PerihelionDistance.Text = txtPerihelionDistance.Text;
			fc.PerihelionDistance.Index = cboPerihelionDistance.SelectedIndex;

			fc.Eccentricity.Checked = cbxEccentricity.Checked;
			fc.Eccentricity.Text = txtEccentricity.Text;
			fc.Eccentricity.Index = cboEccentricity.SelectedIndex;

			fc.LongOfAscendingNode.Checked = cbxLongOfAscendingNode.Checked;
			fc.LongOfAscendingNode.Text = txtLongOfAscendingNode.Text;
			fc.LongOfAscendingNode.Index = cboLongOfAscendingNode.SelectedIndex;

			fc.ArgumentOfPericenter.Checked = cbxArgumentOfPericenter.Checked;
			fc.ArgumentOfPericenter.Text = txtArgumentOfPericenter.Text;
			fc.ArgumentOfPericenter.Index = cboArgumentOfPericenter.SelectedIndex;

			fc.Inclination.Checked = cbxInclination.Checked;
			fc.Inclination.Text = txtInclination.Text;
			fc.Inclination.Index = cboInclination.SelectedIndex;

			fc.Period.Checked = cbxPeriod.Checked;
			fc.Period.Text = txtPeriod.Text;
			fc.Period.Index = cboPeriod.SelectedIndex;

			return fc;
		}

		#endregion

		#region PopulateFilters

		private void PopulateFilters(FilterCollection Filters)
		{
			IsFilterText = true;

			if (Filters == null)
			{
				cbxName.Checked = false;
				cboName.SelectedIndex = 0;
				txtName.Text = String.Empty;

				cbxPerihelionDate.Checked = false;
				cboPerihelionDate.SelectedIndex = 0;
				txtPerihelionDateD.Text = String.Empty;
				txtPerihelionDateM.Text = String.Empty;
				txtPerihelionDateY.Text = String.Empty;

				cbxPerihelionDistance.Checked = false;
				cboPerihelionDistance.SelectedIndex = 2;
				txtPerihelionDistance.Text = String.Empty;

				cbxEccentricity.Checked = false;
				cboEccentricity.SelectedIndex = 2;
				txtEccentricity.Text = String.Empty;

				cbxLongOfAscendingNode.Checked = false;
				cboLongOfAscendingNode.SelectedIndex = 2;
				txtLongOfAscendingNode.Text = String.Empty;

				cbxArgumentOfPericenter.Checked = false;
				cboArgumentOfPericenter.SelectedIndex = 2;
				txtArgumentOfPericenter.Text = String.Empty;

				cbxInclination.Checked = false;
				cboInclination.SelectedIndex = 2;
				txtInclination.Text = String.Empty;

				cbxPeriod.Checked = false;
				cboPeriod.SelectedIndex = 2;
				txtPeriod.Text = String.Empty;
			}
			else
			{
				cbxName.Checked = Filters.Name.Checked;
				cboName.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.Name.ValueResolve);
				txtName.Text = Filters.Name.Text;

				cbxPerihelionDate.Checked = Filters.PerihelionDate.Checked;
				cboPerihelionDate.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.PerihelionDate.ValueResolve);
				if (!String.IsNullOrEmpty(Filters.PerihelionDate.Text))
				{
					string[] date = Filters.PerihelionDate.Text.Split('.');
					txtPerihelionDateD.Text = date[0];
					txtPerihelionDateM.Text = date[1];
					txtPerihelionDateY.Text = date[2];
				}
				else
				{
					txtPerihelionDateD.Text = String.Empty;
					txtPerihelionDateM.Text = String.Empty;
					txtPerihelionDateY.Text = String.Empty;
				}

				cbxPerihelionDistance.Checked = Filters.PerihelionDistance.Checked;
				cboPerihelionDistance.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.PerihelionDistance.ValueResolve);
				txtPerihelionDistance.Text = Filters.PerihelionDistance.Text;

				cbxEccentricity.Checked = Filters.Eccentricity.Checked;
				cboEccentricity.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.Eccentricity.ValueResolve);
				txtEccentricity.Text = Filters.Eccentricity.Text;

				cbxLongOfAscendingNode.Checked = Filters.LongOfAscendingNode.Checked;
				cboLongOfAscendingNode.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.LongOfAscendingNode.ValueResolve);
				txtLongOfAscendingNode.Text = Filters.LongOfAscendingNode.Text;

				cbxArgumentOfPericenter.Checked = Filters.ArgumentOfPericenter.Checked;
				cboArgumentOfPericenter.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.ArgumentOfPericenter.ValueResolve);
				txtArgumentOfPericenter.Text = Filters.ArgumentOfPericenter.Text;

				cbxInclination.Checked = Filters.Inclination.Checked;
				cboInclination.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.Inclination.ValueResolve);
				txtInclination.Text = Filters.Inclination.Text;

				cbxPeriod.Checked = Filters.Period.Checked;
				cboPeriod.SelectedIndex = FilterManager.GetIndexFromValueResolve(Filters.Period.ValueResolve);
				txtPeriod.Text = Filters.Period.Text;
			}

			IsFilterText = false;
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

		#region btnPerihelionDateNow_Click

		private void btnPerihelionDateNow_Click(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now.AddDays(-20);
			txtPerihelionDateD.Text = "1";
			txtPerihelionDateM.Text = dt.Month.ToString();
			txtPerihelionDateY.Text = dt.Year.ToString();
		}

		#endregion

		#region txtFilters_TextChanged

		private void txtFiltersCommon_TextChanged(object sender, EventArgs e)
		{
			if (!IsFilterText)
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

		private void txtPerihelionDateMY_TextChanged(object sender, EventArgs e)
		{
			if (txtPerihelionDateM.Text.Length > 0 && txtPerihelionDateY.Text.Length > 0)
			{
				int max = DateTime.DaysInMonth(txtPerihelionDateY.Int(), txtPerihelionDateM.Int());

				LeMiMa o = txtPerihelionDateD.Tag as LeMiMa;
				LeMiMa n = new LeMiMa(o.Len, o.Min, max);

				if (txtPerihelionDateD.Text.Length > 0 && txtPerihelionDateD.Int() > n.Max)
					txtPerihelionDateD.Text = n.Max.ToString();

				txtPerihelionDateD.Tag = n;
			}

			txtFiltersCommon_TextChanged(sender, e);
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
			//this.CancelButton = pnlFilters.Visible ? btnFiltersClose : btnCancel;
			this.AcceptButton = pnlFilters.Visible ? btnFiltersApply : btnOk;
		}

		#endregion

		#endregion
	}
}
