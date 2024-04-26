using Comets.Core;
using Comets.Core.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.Application.Common.Controls.Common
{
	public partial class SortMenuControl : UserControl
	{
		#region Events

		public event Action OnSort;

		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CometCollection Comets { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SortProperty { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SortAscending { get; set; }

		public string Title
		{
			get { return btnSort.Text; }
			set { btnSort.Text = value; }
		}

		#endregion

		#region Constructor

		public SortMenuControl()
		{
			InitializeComponent();

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
		}

		#endregion

		#region +EventHandling

		#region Button

		private void btnSort_Click(object sender, EventArgs e)
		{
			SetSortItems();

			Button btn = sender as Button;
			contextSort.Show(this, new Point(btn.Left + 1, btn.Top + btn.Height - 1));
		}

		#endregion

		#region MenuItem

		private void menuItemSortCommon_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem mni = sender as ToolStripMenuItem;

			if (!mni.Checked)
			{
				SortAscending = mnuAsc.Checked;

				foreach (ToolStripMenuItem item in contextSort.Items.OfType<ToolStripMenuItem>())
					item.Checked = false;

				mni.Checked = true;
				SortProperty = mni.Tag as string;

				mnuAsc.Checked = SortAscending;
				mnuDesc.Checked = !SortAscending;

				SortCollection(this.Comets);
			}
		}

		private void menuItemSortAscDesc_Click(object sender, EventArgs e)
		{
			mnuAsc.InvertChecked();
			mnuDesc.InvertChecked();
			SortAscending = mnuAsc.Checked;
			SortCollection(this.Comets);
		}

		#endregion

		#endregion

		#region Methods

		public void SetSortItems()
		{
			foreach (ToolStripMenuItem menuitem in contextSort.Items.OfType<ToolStripMenuItem>())
			{
				if (menuitem.Tag as string == SortProperty)
					menuitem.Checked = true;
				else
					menuitem.Checked = false;
			}

			mnuAsc.Checked = SortAscending;
			mnuDesc.Checked = !SortAscending;
		}

		public void SortCollection(CometCollection comets)
		{
			PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Comet)).Find(SortProperty, false);

			if (prop == null)
				throw new ArgumentException(String.Format("Unknown SortProperty: \"{0}\"", SortProperty));

			this.Comets = SortAscending
				? new CometCollection(comets.OrderBy(x => prop.GetValue(x)))
				: new CometCollection(comets.OrderByDescending(x => prop.GetValue(x)));

			OnSort();
		}

		#endregion
	}
}
