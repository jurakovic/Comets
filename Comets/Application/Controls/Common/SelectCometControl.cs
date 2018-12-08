using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.Controls.Common
{
	public partial class SelectCometControl : UserControl
	{
		#region Events

		public Action<DateTime?> OnSelectedCometChanged;
		public Action<int> OnCometsFiltered;

		#endregion

		#region Properties

		public Comet SelectedComet
		{
			get { return Comets.ElementAtOrDefault(cbComet.SelectedIndex); }
			private set { cbComet.SelectedIndex = Comets.IndexOf(value); }
		}

		public CometCollection Comets { get; set; }
		public FilterCollection Filters { get; set; }
		public string SortProperty { get; set; }
		public bool SortAscending { get; set; }

		#endregion

		#region Constructor

		public SelectCometControl()
		{
			InitializeComponent();
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

		private void btnFilter_Click(object sender, EventArgs e)
		{
			using (FormDatabase fdb = new FormDatabase(CommonManager.MainCollection, Filters, SortProperty, SortAscending, true) { Owner = this.ParentForm })
			{
				fdb.TopMost = this.ParentForm.TopMost;

				if (fdb.ShowDialog() == DialogResult.OK)
				{
					Comet comet = SelectedComet;

					this.Comets = fdb.CometsFiltered;
					this.Filters = fdb.Filters;
					this.SortProperty = fdb.SortProperty;
					this.SortAscending = fdb.SortAscending;

					DataBind(comet);

					OnCometsFiltered(this.Comets.Count);
					OnSelectedCometChangedInternal();
				}
			}
		}

		#endregion

		#region Events

		private void OnSelectedCometChangedInternal()
		{
			DateTime? perihelionDate = EphemerisManager.JDToLocalDateTimeSafe(this.SelectedComet?.Tn);

			if (this.SelectedComet != null)
			{
				Comet c = this.SelectedComet;

				lblPerihelionDateValue.Text = perihelionDate.Value.ToString("dd MMM yyyy HH:mm:ss");
				lblPerihelionDistanceValue.Text = c.q.ToString("0.000000") + " AU";
				lblPeriodValue.Text = c.P < 10000 ? c.P.ToString("0.000000") + " years" : "-";
			}
			else
			{
				lblPerihelionDateValue.Text =
				lblPerihelionDistanceValue.Text =
				lblPeriodValue.Text = String.Empty;
			}

			OnSelectedCometChanged(perihelionDate);
		}

		#endregion

		#endregion

		#region Methods

		public void DataBind(Comet selectedComet)
		{
			if (this.Comets != null)
			{
				cbComet.DisplayMember = CometManager.PropertyEnum.full.ToString();
				cbComet.DataSource = this.Comets;

				if (this.Comets.Count > 0)
				{
					if (selectedComet != null && this.Comets.Contains(selectedComet))
					{
						this.SelectedComet = selectedComet;
					}
					else
					{
						//comet with nearest perihelion date
						decimal jdNow = DateTime.Now.JD();
						this.SelectedComet = this.Comets.OrderBy(x => Math.Abs(x.Tn - jdNow)).First();
					}
				}
			}
		}

		#endregion
	}
}
