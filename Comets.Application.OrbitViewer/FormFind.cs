using Comets.Application.Common.Controls.Common;
using Comets.Core;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.OrbitViewer
{
	public partial class FormFind : ValueChangeForm
	{
		#region Events

		public event Action<string> OnSelectedCometChanged;

		#endregion

		#region Properties

		private OVComet SelectedComet
		{
			get
			{
				Comet c = lbxFilter.SelectedItem as Comet;
				return c != null ? new OVComet(c) : null;
			}
		}

		private CometCollection Comets { get; set; }

		#endregion

		#region Constructor

		public FormFind(CometCollection collection)
		{
			InitializeComponent();

			Comets = new CometCollection(collection.OrderBy(x => x.sortkey));
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormFind_Load(object sender, EventArgs e)
		{
			ValueChangedInternal = true;
			BindCollection(this.Comets);
			ValueChangedInternal = false;
		}

		#endregion

		#region TextBox

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			string text = txtName.Text.ToLower();
			CometCollection filteredComets = new CometCollection(Comets.Where(x => x.full.ToLower().Contains(text)));
			BindCollection(filteredComets);
		}

		#endregion

		#region ListBox

		private void lbxFilter_DrawItem(object sender, DrawItemEventArgs e)
		{
			//http://stackoverflow.com/questions/3663704/how-to-change-listbox-selection-background-color

			if (e.Index >= 0)
			{
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected, e.ForeColor, SystemColors.GrayText);

				e.DrawBackground();
				e.Graphics.DrawString(lbxFilter.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
				e.DrawFocusRectangle();
			}
		}

		private void lbxFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
				OnSelectedCometChanged(SelectedComet?.Name);
		}

		private void lbxFilter_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		#endregion

		#endregion

		#region Methods

		private void BindCollection(CometCollection comets)
		{
			lbxFilter.DisplayMember = PropertyEnum.full.ToString();
			lbxFilter.DataSource = comets;
		}

		#endregion
	}
}
