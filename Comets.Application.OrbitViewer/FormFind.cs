using Comets.Core;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.OrbitViewer
{
	public partial class FormFind : Form
	{
		#region Properties

		public Comet SelectedComet { get; private set; }

		private CometCollection Comets { get; set; }
		private CometCollection FilteredComets { get; set; }

		#endregion

		#region Constructor

		public FormFind(CometCollection collection)
		{
			InitializeComponent();

			Comets = new CometCollection(collection.OrderBy(x => x.sortkey));
			FilteredComets = new CometCollection(Comets);

			lbxFilter.DisplayMember = PropertyEnum.full.ToString();
			lbxFilter.DataSource = FilteredComets;
		}

		#endregion

		#region +EventHandling

		#region Form

		private void FormFind_FormClosing(object sender, FormClosingEventArgs e)
		{
			SelectedComet = FilteredComets.ElementAtOrDefault(lbxFilter.SelectedIndex);
		}

		#endregion

		#region TextBox

		private void txtInfoName_TextChanged(object sender, EventArgs e)
		{
			string text = txtInfoName.Text.ToLower();
			FilteredComets = new CometCollection(Comets.Where(x => x.full.ToLower().Contains(text)));
			lbxFilter.DataSource = FilteredComets;
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

		private void lbxFilter_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		#endregion

		#endregion
	}
}
