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
        public FormDatabase()
        {
            InitializeComponent();
        }

        private void FormDatabase_Load(object sender, EventArgs e)
        {
            sortList(FormMain.userList);
        }

        private void lbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = lbxDatabase.SelectedIndex;
            Comet c = FormMain.userList.ElementAt(ind);

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

        private void btnSort_Click(object sender, EventArgs e)
        {
            contextSort.Show(this, new Point((sender as Button).Left + 1, (sender as Button).Top + (sender as Button).Height - 1));
        }

        private void menuItemSort_Click(object sender, EventArgs e)
        {
            //ako kliknem na item koji je vec odabran
            if ((sender as MenuItem).Checked) return;

            //da zapamti kako su zadnja 2 odabrana
            bool order = menuItemAsc.Checked;

            //prvo sve odznačiti
            foreach (MenuItem item in contextSort.MenuItems) (item as MenuItem).Checked = false;

            //pa označiti onog koji je kliknut
            (sender as MenuItem).Checked = true;

            menuItemAsc.Checked = order;
            menuItemDesc.Checked = !order;

            sortList(FormMain.userList);
        }

        private void menuItemAsc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = true;
            menuItemDesc.Checked = false;
            sortList(FormMain.userList);
        }

        private void menuItemDesc_Click(object sender, EventArgs e)
        {
            menuItemAsc.Checked = false;
            menuItemDesc.Checked = true;
            sortList(FormMain.userList);
        }

        public void sortList(List<Comet> list)
        {
            if (list.Count == 0)
                return;

            List<Comet> tempList = list.ToList();
            FormMain.userList.Clear();

            if (menuItemDesig.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.sortkey).ToList();

            else if (menuItemDesig.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.sortkey).ToList();

            else if (menuItemName.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.name).ToList();

            else if (menuItemName.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.name).ToList();

            else if (menuItemPerihDate.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.T).ToList();

            else if (menuItemPerihDate.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.T).ToList();

            else if (menuItemPerihDist.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.q).ToList();

            else if (menuItemPerihDist.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.q).ToList();

            else if (menuItemIncl.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.i).ToList();

            else if (menuItemIncl.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.i).ToList();

            else if (menuItemEcc.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.e).ToList();

            else if (menuItemEcc.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.e).ToList();

            else if (menuItemAscNode.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.N).ToList();

            else if (menuItemAscNode.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.N).ToList();

            else if (menuItemArgPeri.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.w).ToList();

            else if (menuItemArgPeri.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.w).ToList();

            else if (menuItemPeriod.Checked && menuItemAsc.Checked)
                FormMain.userList = tempList.OrderBy(Comet => Comet.P).ToList();

            else if (menuItemPeriod.Checked && menuItemDesc.Checked)
                FormMain.userList = tempList.OrderByDescending(Comet => Comet.P).ToList();

            lbxDatabase.DataSource = FormMain.userList;
            lbxDatabase.DisplayMember = "full";
        }
    }
}
