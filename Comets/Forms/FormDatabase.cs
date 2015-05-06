using Comets.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Forms
{
    public partial class FormDatabase : Form
    {
        #region Constructor

        public FormDatabase()
        {
            InitializeComponent();
        }

        #endregion

        #region Form_Load

        private void FormDatabase_Load(object sender, EventArgs e)
        {
            SortList(FormMain.UserList);
        }

        #endregion

        #region lbxDatabase_SelectedIndexChanged

        private void lbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = lbxDatabase.SelectedIndex;
            Comet c = FormMain.UserList.ElementAt(ind);

            t_id.Text = c.id;
            t_name.Text = c.name;
            t_T.Text = c.Ty.ToString() + "-" + c.Tm.ToString("00") + "-" + c.Td.ToString("00") + "." + c.Th.ToString("0000");
            t_q1.Text = c.q.ToString("0.000000");
            t_e.Text = c.e.ToString("0.000000");
            t_i.Text = c.i.ToString("0.0000");
            t_N1.Text = c.N.ToString("0.0000");
            t_w.Text = c.w.ToString("0.0000");

            if (c.P > 10000 || c.e > 0.98)
            {
                t_P.Text = "";
                t_Q2.Text = "";
                t_a.Text = "";
                t_n2.Text = "";
            }
            else
            {
                t_P.Text = c.P.ToString("0.000000");
                t_Q2.Text = c.Q.ToString("0.000000");
                t_a.Text = c.a.ToString("0.000000");
                t_n2.Text = c.n.ToString("0.000000");
            }

            t_g.Text = c.g.ToString("0.0");
            t_k.Text = c.k.ToString("0.0");

            t_sortKey.Text = c.sortkey.ToString("0.00000000000");
            //t_sortKey.Text = c.idKey;

            tEquinox.Text = "2000.0";
        }

        #endregion

        #region btnSort_Click

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
        }

        #endregion

        #region menuItemSort_Click

        private void menuItemSort_Click(object sender, EventArgs e)
        {
            if (!(sender as MenuItem).Checked)
            {
                bool order = menuItemAsc.Checked;

                foreach (MenuItem item in contextSort.MenuItems)
                    item.Checked = false;

                (sender as MenuItem).Checked = true;

                menuItemAsc.Checked = order;
                menuItemDesc.Checked = !order;

                SortList(FormMain.UserList);
            }
        }

        private void menuItemAsc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = true;
            menuItemDesc.Checked = false;
            SortList(FormMain.UserList);
        }

        private void menuItemDesc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = false;
            menuItemDesc.Checked = true;
            SortList(FormMain.UserList);
        }

        #endregion

        #region SortList

        public void SortList(List<Comet> list)
        {
            if (list.Count == 0) return;

            List<Comet> tempList = list.ToList();
            FormMain.UserList.Clear();

            if (menuItemDesig.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.sortkey).ToList();

            else if (menuItemDesig.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.sortkey).ToList();

            else if (menuItemName.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.name).ToList();

            else if (menuItemName.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.name).ToList();

            else if (menuItemPerihDate.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.T).ToList();

            else if (menuItemPerihDate.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.T).ToList();

            else if (menuItemPerihDist.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.q).ToList();

            else if (menuItemPerihDist.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.q).ToList();

            else if (menuItemIncl.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.i).ToList();

            else if (menuItemIncl.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.i).ToList();

            else if (menuItemEcc.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.e).ToList();

            else if (menuItemEcc.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.e).ToList();

            else if (menuItemAscNode.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.N).ToList();

            else if (menuItemAscNode.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.N).ToList();

            else if (menuItemArgPeri.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.w).ToList();

            else if (menuItemArgPeri.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.w).ToList();

            else if (menuItemPeriod.Checked && menuItemAsc.Checked)
                FormMain.UserList = tempList.OrderBy(x => x.P).ToList();

            else if (menuItemPeriod.Checked && menuItemDesc.Checked)
                FormMain.UserList = tempList.OrderByDescending(x => x.P).ToList();

            lbxDatabase.DataSource = FormMain.UserList;
            lbxDatabase.DisplayMember = "full";
        }

        #endregion
    }
}
