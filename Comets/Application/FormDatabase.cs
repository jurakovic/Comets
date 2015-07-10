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
		private bool IsTextChangedByFilters { get; set; }
		private ToolTip ToolTip { get; set; }
		private string SortPropertyName { get; set; }

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

		public FormDatabase(List<Comet> list, FilterCollection filters, bool isForFiltering)
		{
			InitializeComponent();

			Comets = list.ToList();
			Filters = filters;

			if (Filters == null)
				DateTime = FormMain.DefaultDateStart;
			else
				DateTime = Filters.T.Value == 0.0 ? FormMain.DefaultDateStart : Utils.JDToDateTime(Filters.T.Value).ToLocalTime();

			SortPropertyName = "sortkey";

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
			Comet c = Comets.ElementAt(lbxDatabase.SelectedIndex);

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

		#region Date control

		private void btnDate_Click(object sender, EventArgs e)
		{
			using (FormDateTime fdt = new FormDateTime(FormMain.DefaultDateStart, DateTime, GetT()))
			{
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
			fc.T.Text = DateTime.Day + "." + DateTime.Month + "." + DateTime.Year + "." + DateTime.Hour + "." + DateTime.Minute + "." + DateTime.Second;
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
