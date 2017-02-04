using Comets.BusinessLayer.Business;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.BusinessLayer.Managers.CometManager.PropertyEnum;

namespace Comets.Application.ModulOrbit
{
	public partial class FormFind : Form
	{
		#region Fields

		private CometCollection _comets;
		private CometCollection _filteredComets;

		#endregion

		#region Properties

		public Comet SelectedComet { get; private set; }

		#endregion

		#region Constructor

		public FormFind(CometCollection collection)
		{
			InitializeComponent();

			_comets = new CometCollection(collection.OrderBy(x => x.orderkey));
			_filteredComets = new CometCollection(_comets);

			lbxFilter.DisplayMember = PropertyEnum.full.ToString();
			lbxFilter.DataSource = _filteredComets;
		}

		#endregion

		#region Events

		private void txtInfoName_TextChanged(object sender, EventArgs e)
		{
			string text = txtInfoName.Text.ToLower();
			_filteredComets = new CometCollection(_comets.Where(x => x.full.ToLower().Contains(text)));
			lbxFilter.DataSource = _filteredComets;
		}

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

		private void lbxFilter_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void FormFind_FormClosing(object sender, FormClosingEventArgs e)
		{
			SelectedComet = _filteredComets.ElementAtOrDefault(lbxFilter.SelectedIndex);
		}

		#endregion
	}
}
