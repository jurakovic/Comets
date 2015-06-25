using Comets.Application.ModulEphemeris;
using Comets.Application.ModulGraph;
using Comets.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application
{
	public partial class FormSelectComets : Form
	{
		BindingList<Comet> LList;
		BindingList<Comet> RList;

		public FormSelectComets(List<Comet> comets)
		{
			InitializeComponent();

			if (comets.SequenceEqual(FormMain.UserList))
			{
				LList = new BindingList<Comet>(FormMain.MainList);
				RList = new BindingList<Comet>();
			}
			else
			{
				LList = new BindingList<Comet>(FormMain.MainList.Except(comets).ToList());
				RList = new BindingList<Comet>(comets);
			}
		}

		private void FormSelectComets_Load(object sender, EventArgs e)
		{
			BindLists();
		}

		private void lbxLeft_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int i = lbxLeft.SelectedIndex;
			RList.Add(LList.ElementAt(i));
			LList.RemoveAt(i);
		}

		private void lbxRight_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int i = lbxLeft.SelectedIndex;
			LList.Add(RList.ElementAt(i));
			RList.RemoveAt(i);
		}

		private void btnAddAll_Click(object sender, EventArgs e)
		{
			if (RList.Count != FormMain.MainList.Count)
			{
				RList = new BindingList<Comet>(FormMain.MainList);
				LList.Clear();
				BindLists();
			}
		}

		private void btnAddSelected_Click(object sender, EventArgs e)
		{
			if (lbxLeft.SelectedItems.Count > 0)
			{
				foreach (Comet c in lbxLeft.SelectedItems.OfType<Comet>().ToList())
				{
					RList.Add(c);
					LList.Remove(c);
				}
				BindLists();
			}
		}

		private void btnRemoveSelected_Click(object sender, EventArgs e)
		{
			if (lbxRight.SelectedItems.Count > 0)
			{
				foreach (Comet c in lbxRight.SelectedItems.OfType<Comet>().ToList())
				{
					LList.Add(c);
					RList.Remove(c);
				}
				BindLists();
			}
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			if (LList.Count != FormMain.MainList.Count)
			{
				LList = new BindingList<Comet>(FormMain.MainList);
				RList.Clear();
				BindLists();
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (this.Owner is FormGraphSettings)
			{
				FormGraphSettings fgs = this.Owner as FormGraphSettings;

				if (!fgs.GraphSettings.Comets.SequenceEqual(RList))
				{
					if (RList.Any())
						fgs.GraphSettings.Comets = RList.ToList();
					else
						fgs.GraphSettings.Comets = FormMain.UserList.ToList();
				}
			}
			else if (this.Owner is FormEphemerisSettings)
			{
				FormEphemerisSettings fes = this.Owner as FormEphemerisSettings;

				if (!fes.EphemerisSettings.Comets.SequenceEqual(RList))
				{
					if (RList.Any())
						fes.EphemerisSettings.Comets = RList.ToList();
					else
						fes.EphemerisSettings.Comets = FormMain.UserList.ToList();
				}
			}

			this.Close();
		}

		private void BindLists()
		{
			LList = new BindingList<Comet>(LList.OrderBy(x => x.sortkey).ToList());
			RList = new BindingList<Comet>(RList.OrderBy(x => x.sortkey).ToList());

			lbxLeft.DisplayMember = "full";
			lbxLeft.DataSource = LList;

			lbxRight.DisplayMember = "full";
			lbxRight.DataSource = RList;

			lbxLeft.ClearSelected();
			lbxRight.ClearSelected();
		}
	}
}
