using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
		public readonly double MaxMagnitude = 30.0;
		public readonly double MaxAphSmaxis = 999.0;

		#endregion

		#region Properties

		public List<Comet> Comets { get; private set; }

		public FilterCollection Filters { get; private set; }
		public string SortProperty { get; private set; }
		public bool SortAscending { get; private set; }

		private bool IsTextChangedByFilters { get; set; }

		private ToolTip ToolTip { get; set; }

		private Timer Timer { get; set; }
		private EphemerisResult PreviousEphemeris { get; set; }

		private DateTime _dateTime;
		private DateTime DateTime
		{
			get { return _dateTime; }
			set
			{
				_dateTime = value;
				btnPerihDate.Text = _dateTime.ToString(FormMain.DateTimeFormat);
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
				DateTime = Filters.NextT.Value == 0.0 ? FormMain.DefaultDateStart : Utils.JDToDateTime(Filters.NextT.Value).ToLocalTime();

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

			Timer = new Timer();
			Timer.Interval = 1000;
			Timer.Tick += Timer_Tick;
		}

		#endregion

		#region FormDatabase_Load

		private void FormDatabase_Load(object sender, EventArgs e)
		{
			PopulateFilters(Filters);
			ApllyContextMenuVisibility();

			SetSortItems(SortAscending);
			SortList(Comets);
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
			Timer.Stop();

			lblEphemSunDistIndicator.Text = String.Empty;
			lblEphemSunDistIndicator.Text = String.Empty;
			lblEphemEarthDistIndicator.Text = String.Empty;
			lblEphemMagIndicator.Text = String.Empty;
			lblEphemRaIndicator.Text = String.Empty;
			lblEphemDecIndicator.Text = String.Empty;
			lblEphemAltIndicator.Text = String.Empty;
			lblEphemAzIndicator.Text = String.Empty;
			lblEphemElongationIndicator.Text = String.Empty;

			string format6 = "0.000000";
			string format4 = "0.0000";
			int minPeriod = 1000;

			Comet c = Comets.ElementAt(lbxDatabase.SelectedIndex);
			PreviousEphemeris = EphemerisManager.GetEphemeris(c, DateTime.Now.AddMinutes(-1).JD(), FormMain.Settings.Location);

			string commonName = c.full;
			string commonPerihDist = c.q.ToString(format6);
			string commonPeriod = c.P < minPeriod ? c.P.ToString(format6) : String.Empty;
			string commonAphDist = c.P < minPeriod ? c.Q.ToString(format6) : String.Empty;

			//ephemeris
			txtInfoName.Text = commonName;

			txtInfoNextPerihDate.Text = Utils.JDToDateTime(c.NextT).ToLocalTime().ToString(FormMain.DateTimeFormat);
			txtInfoPeriod.Text = commonPeriod;
			txtInfoAphSunDist.Text = commonAphDist;

			txtInfoPerihDist.Text = commonPerihDist;
			txtInfoPerihEarthDist.Text = c.PerihEarthDist.ToString(format6);
			txtInfoPerihMag.Text = c.PerihMag.ToString("0.00");

			CalculateEphemeris();
			Timer.Start();

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

		#region btnResetAllFilters_Click

		private void btnResetAllFilters_Click(object sender, EventArgs e)
		{
			Filters = null;
			PopulateFilters(Filters);

			SortProperty = FormMain.DefaultSortProperty;
			SortAscending = FormMain.DefaultSortAscending;

			SetSortItems(SortAscending);
			ApplyFilters();
		}

		#endregion

		#region tbcCommon_SelectedIndexChanged

		private void tbcCommon_SelectedIndexChanged(object sender, EventArgs e)
		{
			ApllyContextMenuVisibility();
		}

		#endregion

		#region ApllyContextMenuVisibility

		private void ApllyContextMenuVisibility()
		{
			if ((tbcDetails.Visible && tbcDetails.SelectedIndex == 1) || (tbcFilters.Visible && tbcFilters.SelectedIndex == 1))
			{
				mnuAphDistance.Visible = true;
				mnuSemiMajorAxis.Visible = true;
				mnuEcc.Visible = true;
				mnuIncl.Visible = true;
				mnuAscNode.Visible = true;
				mnuArgPeri.Visible = true;

				mnuPerihEarthDist.Visible = false;
				mnuCurrSunDist.Visible = false;
				mnuCurrEarthDist.Visible = false;
				mnuPerihMag.Visible = false;
				mnuCurrMag.Visible = false;
			}
			else
			{
				mnuAphDistance.Visible = false;
				mnuSemiMajorAxis.Visible = false;
				mnuEcc.Visible = false;
				mnuIncl.Visible = false;
				mnuAscNode.Visible = false;
				mnuArgPeri.Visible = false;

				mnuPerihEarthDist.Visible = true;
				mnuCurrSunDist.Visible = true;
				mnuCurrEarthDist.Visible = true;
				mnuPerihMag.Visible = true;
				mnuCurrMag.Visible = true;
			}
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
			if (pnlDetails.Visible)
				tbcFilters.SelectedIndex = tbcDetails.SelectedIndex;
			else
				tbcDetails.SelectedIndex = tbcFilters.SelectedIndex;

			pnlDetails.Visible = !pnlDetails.Visible;
			pnlFilters.Visible = !pnlFilters.Visible;

			ApllyContextMenuVisibility();
			lbxDatabase.Focus();
		}

		#endregion

		#region Timer

		void Timer_Tick(object sender, EventArgs e)
		{
			CalculateEphemeris();
		}

		private void CalculateEphemeris()
		{
			if (lbxDatabase.SelectedIndex >= 0)
			{
				Comet c = Comets.ElementAt(lbxDatabase.SelectedIndex);
				EphemerisResult er = EphemerisManager.GetEphemeris(c, DateTime.Now.JD(), FormMain.Settings.Location);

				txtInfoCurrSunDist.Text = er.SunDist.ToString("0.000000");
				txtInfoCurrEarthDist.Text = er.EarthDist.ToString("0.000000");
				txtInfoCurrMag.Text = er.Magnitude.ToString("0.00");

				txtEphemRA.Text = (EphemerisManager.HMSString(er.RA / 15.0)).Trim();
				txtEphemDec.Text = (EphemerisManager.AngleString(er.Dec, false, true)).Trim();
				txtEphemAlt.Text = er.Alt.ToString("0.00") + "°";
				txtEphemAz.Text = er.Az.ToString("0.00") + "°";
				txtEphemElongation.Text = er.Elongation.ToString("0.00") + "°" + (er.PositionAngle >= 180 ? " W" : " E");

				bool rHigher = er.SunDist >= PreviousEphemeris.SunDist;
				bool dHigher = er.EarthDist >= PreviousEphemeris.EarthDist;
				bool mHigher = er.Magnitude >= PreviousEphemeris.Magnitude;
				bool raHigher = er.RA >= PreviousEphemeris.RA;
				bool decHigher = er.Dec >= PreviousEphemeris.Dec;
				bool altHigher = er.Alt >= PreviousEphemeris.Alt;
				bool azHigher = er.Az >= PreviousEphemeris.Az;
				bool eloHigher = er.Elongation >= PreviousEphemeris.Elongation;

				lblEphemSunDistIndicator.Text = rHigher ? "▲" : "▼";
				lblEphemSunDistIndicator.ForeColor = rHigher ? Color.Red : Color.Green;

				lblEphemEarthDistIndicator.Text = dHigher ? "▲" : "▼";
				lblEphemEarthDistIndicator.ForeColor = dHigher ? Color.Red : Color.Green;

				lblEphemMagIndicator.Text = mHigher ? "▲" : "▼";
				lblEphemMagIndicator.ForeColor = mHigher ? Color.Red : Color.Green;

				lblEphemRaIndicator.Text = raHigher ? "▲" : "▼";
				lblEphemRaIndicator.ForeColor = Color.Black;

				lblEphemDecIndicator.Text = decHigher ? "▲" : "▼";
				lblEphemDecIndicator.ForeColor = decHigher ? Color.Green : Color.Red;

				lblEphemAltIndicator.Text = altHigher ? "▲" : "▼";
				lblEphemAltIndicator.ForeColor = altHigher ? Color.Green : Color.Red;

				lblEphemAzIndicator.Text = azHigher ? "▲" : "▼";
				lblEphemAzIndicator.ForeColor = Color.Black;

				lblEphemElongationIndicator.Text = eloHigher ? "▲" : "▼";
				lblEphemElongationIndicator.ForeColor = eloHigher ? Color.Green : Color.Red;

				PreviousEphemeris = er;
			}
		}

		#endregion

		#region + Sort

		#region SetSortItems

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
			{
				foreach (TabPage t in tbcDetails.TabPages)
					foreach (Control c in t.Controls)
						if (c is TextBox)
							c.Text = String.Empty;

				Timer.Stop();
			}

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
					cbxPerihDate.Checked = true;
				}
			}
		}

		private double? GetT()
		{
			double? T = null;

			if (lbxDatabase.SelectedIndex >= 0)
				T = Comets.ElementAt(lbxDatabase.SelectedIndex).NextT;

			return T;
		}

		#endregion

		#region CollectFilters

		private FilterCollection CollectFilters()
		{
			FilterCollection filters = new FilterCollection();
			Type type = filters.GetType();
			List<PropertyInfo> filterProps = type.GetProperties().ToList();

			foreach (TabPage t in tbcFilters.TabPages)
			{
				foreach (Control p in t.Controls)
				{
					if (p is Panel)
					{
						string propertyName = (string)p.Tag;

						PropertyInfo pinfo = filterProps.FirstOrDefault(x => x.Name == propertyName);

						if (pinfo != null)
						{
							Filter f = pinfo.GetValue(filters, null) as Filter;

							if (f != null)
							{
								bool isChecked = false;
								string text = String.Empty;
								int index = 0;

								foreach (Control c in p.Controls)
								{
									if (c is CheckBox)
										isChecked = (c as CheckBox).Checked;
									else if (c is TextBox)
										text = (c as TextBox).Text.Trim();
									else if (c is ComboBox)
										index = (c as ComboBox).SelectedIndex;
									else if (c is Button)
										text = DateTime.JD().ToString();
								}

								f.Checked = isChecked;
								f.Text = text;
								f.Index = index;

								pinfo.SetValue(filters, f, null);
							}
						}
					}
				}
			}

			return filters;
		}

		#endregion

		#region PopulateFilters

		private void PopulateFilters(FilterCollection filters)
		{
			IsTextChangedByFilters = true;

			if (filters == null)
			{
				foreach (TabPage t in tbcFilters.TabPages)
					foreach (Control p in t.Controls)
						if (p is Panel)
							foreach (Control c in p.Controls)
								if (c is CheckBox)
									(c as CheckBox).Checked = false;
								else if (c is TextBox)
									(c as TextBox).Text = String.Empty;
								else if (c is ComboBox)
									(c as ComboBox).SelectedIndex = 1;

				cboName.SelectedIndex = 0; //contains
				cboPerihDate.SelectedIndex = 0; //greather
			}
			else
			{
				List<PropertyInfo> filterProps = filters.GetType().GetProperties().ToList();

				foreach (TabPage t in tbcFilters.TabPages)
				{
					foreach (Control p in t.Controls)
					{
						if (p is Panel)
						{
							string propertyName = (string)p.Tag;

							PropertyInfo pinfo = filterProps.FirstOrDefault(x => x.Name == propertyName);

							if (pinfo != null)
							{
								Filter f = pinfo.GetValue(filters, null) as Filter;

								if (f != null)
								{
									foreach (Control c in p.Controls)
									{
										if (c is CheckBox)
											(c as CheckBox).Checked = f.Checked;
										else if (c is TextBox)
											(c as TextBox).Text = f.Text;
										else if (c is ComboBox)
											(c as ComboBox).SelectedIndex = FilterManager.GetIndexFromValueCompare(f.ValueCompare);
										else if (c is Button && f.Value > 0.0) //button for date
											DateTime = Utils.JDToDateTime(f.Value).ToLocalTime();
									}
								}
							}
						}
					}
				}
			}

			IsTextChangedByFilters = false;
		}

		#endregion

		#region btnFiltersApply_Click

		private void btnFiltersApply_Click(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		#endregion

		#region  ApplyFilters

		private void ApplyFilters()
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

		#region + txtFilters_KeyPress

		#region Ephemeris

		private void txtFiltersPeriod_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 4, 4, Minimum, MaxPeriod);
		}

		private void txtPerihelionDistance_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 2, 6, Minimum, MaxPerihDist);
		}

		private void txtMagnitudeCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 4, Minimum, MaxMagnitude);
		}

		#endregion

		#region Elements

		private void txtAphSmaxis_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 6, Minimum, MaxAphSmaxis);
		}

		private void txtEccentricity_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 1, 6, Minimum, MaxEcc);
		}

		private void txtInclination_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 4, Minimum, MaxIncl);
		}

		private void txtFiltersNodePeri_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e, 3, 4, Minimum, MaxLongNode);
		}

		#endregion

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
