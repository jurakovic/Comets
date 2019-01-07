using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{
	public partial class SelectCometControl : UserControl
	{
		#region Events

		public event Action<DateTime?> OnSelectedCometChanged;
		public event Action<CometCollection, FilterCollection, string, bool> OnCometsFiltered;

		#endregion

		#region Fields

		private CometCollection _comets;

		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Comet SelectedComet
		{
			get { return Comets.ElementAtOrDefault(cbComet.SelectedIndex - 1); }
			private set { cbComet.SelectedIndex = Comets.IndexOf(value) + 1; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSelectedAll
		{
			get { return cbComet.SelectedIndex == 0; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CometCollection Comets
		{
			get { return _comets; }
			set { _comets = sortMenuControl.Comets = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FilterCollection Filters { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SortProperty
		{
			get { return sortMenuControl.SortProperty; }
			set { sortMenuControl.SortProperty = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SortAscending
		{
			get { return sortMenuControl.SortAscending; }
			set { sortMenuControl.SortAscending = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private ToolTip ToolTip { get; set; }

		#endregion

		#region Constructor

		public SelectCometControl()
		{
			InitializeComponent();

			sortMenuControl.OnSort += OnCometsSorted;

			ToolTip = new ToolTip();
		}

		#endregion

		#region +EventHandling

		#region ComboBox

		private void cbComet_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedCometChangedInternal();
		}

		#endregion

		#region Button

		private void btnAll_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void btnFilter_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(CommonManager.MainCollection, false, Filters, SortProperty, SortAscending, true) { Owner = this.ParentForm })
			{
				fdb.TopMost = this.ParentForm.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					Comet comet = SelectedComet;

					this.Comets = fdb.CometsFiltered;
					this.Filters = fdb.Filters;
					this.SortProperty = fdb.SortProperty;
					this.SortAscending = fdb.SortAscending;
					sortMenuControl.Comets = this.Comets;

					DataBind(comet, this.IsSelectedAll);

					OnCometsFiltered(this.Comets, this.Filters, this.SortProperty, this.SortAscending);
					OnSelectedCometChangedInternal();
				}
			}
		}

		#endregion

		#region Events

		private void OnSelectedCometChangedInternal()
		{
			DateTime? perihelionDate = EphemerisManager.JDToDateTimeSafe(this.SelectedComet?.Tn);

			if (this.SelectedComet != null)
			{
				Comet c = this.SelectedComet;

				string text = String.Format("T: {0}\nP: {1}\nq: {2:0.000000} AU\nr: {3:0.000000} AU\nd: {4:0.000000} AU\nm: {5:0.00}",
					perihelionDate.Value.ToString(DateTimeFormat.PerihelionDate),
					c.P < 10000 ? c.P.ToString("0.0000") + " years" : "-",
					c.q,
					c.CurrentSunDist,
					c.CurrentEarthDist,
					c.CurrentMag);

				ToolTip.ToolTipTitle = c.full;
				ToolTip.SetToolTip(cbComet, text);
			}
			else
			{
				ToolTip.RemoveAll();
			}

			OnSelectedCometChanged(perihelionDate);
		}

		private void OnCometsSorted()
		{
			Comet comet = SelectedComet;

			this.Comets = sortMenuControl.Comets;
			this.SortProperty = sortMenuControl.SortProperty;
			this.SortAscending = sortMenuControl.SortAscending;

			DataBind(comet, IsSelectedAll);
		}

		#endregion

		#endregion

		#region Methods

		public void DataBind(Comet selectedComet, bool selectAll)
		{
			if (this.Comets != null)
			{
				bool selectComet = false;
				CometCollection comets = new CometCollection();

				if (this.Comets.Count > 0)
				{
					selectComet = true;
					comets.Add(new Comet() { full = String.Format("[ALL COMETS ({0})]", this.Comets.Count) });
					comets.AddRange(this.Comets);
				}

				cbComet.DisplayMember = CometManager.PropertyEnum.full.ToString();
				cbComet.DataSource = comets;

				if (selectAll)
				{
					SelectAll();
				}
				else if (selectComet)
				{
					if (selectedComet != null && this.Comets.Contains(selectedComet))
					{
						this.SelectedComet = selectedComet;
					}
					else
					{
						//comet with nearest perihelion date
						decimal jdNow = DateTime.UtcNow.JD();
						this.SelectedComet = this.Comets.OrderBy(x => Math.Abs(x.Tn - jdNow)).First();
					}
				}
			}
		}

		private void SelectAll()
		{
			if (this.Comets.Count > 0)
				cbComet.SelectedIndex = 0;
		}

		#endregion
	}
}
