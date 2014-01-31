using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cometary_Workshop
{
    public partial class FiltersForm : Form
    {
        public FiltersForm()
        {
            InitializeComponent();
        }

        private void chFilterName_CheckedChanged(object sender, EventArgs e)
        {
            comboName.Enabled = (sender as CheckBox).Checked;
            tbName.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPerihDate_CheckedChanged(object sender, EventArgs e)
        {
            comboPerihDate.Enabled = (sender as CheckBox).Checked;
            tbPerihDateD.Enabled = (sender as CheckBox).Checked;
            tbPerihDateM.Enabled = (sender as CheckBox).Checked;
            tbPerihDateY.Enabled = (sender as CheckBox).Checked;
            btnPerihDateNow.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPerihDist_CheckedChanged(object sender, EventArgs e)
        {
            comboPerihDist.Enabled = (sender as CheckBox).Checked;
            tbPerihDist.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterAphDist_CheckedChanged(object sender, EventArgs e)
        {
            comboAphDist.Enabled = (sender as CheckBox).Checked;
            tbAphDist.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterEcc_CheckedChanged(object sender, EventArgs e)
        {
            comboEcc.Enabled = (sender as CheckBox).Checked;
            tbEcc.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterAscNode_CheckedChanged(object sender, EventArgs e)
        {
            comboAscNode.Enabled = (sender as CheckBox).Checked;
            tbAscNode.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterLongPeric_CheckedChanged(object sender, EventArgs e)
        {
            comboLongPeric.Enabled = (sender as CheckBox).Checked;
            tbLongPeric.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterIncl_CheckedChanged(object sender, EventArgs e)
        {
            comboIncl.Enabled = (sender as CheckBox).Checked;
            tbIncl.Enabled = (sender as CheckBox).Checked;
        }

        private void chFilterPeriod_CheckedChanged(object sender, EventArgs e)
        {
            comboPeriod.Enabled = (sender as CheckBox).Checked;
            tbPeriod.Enabled = (sender as CheckBox).Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (chName.Checked && comboName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Contains or Does not contain", "Error", MessageBoxButtons.OK);
                return;
            }

            if ((chPerihDate.Checked && comboPerihDate.SelectedIndex == -1) ||
                (chPerihDist.Checked && comboPerihDist.SelectedIndex == -1) ||
                (chAphDist.Checked && comboAphDist.SelectedIndex == -1) ||
                (chEcc.Checked && comboEcc.SelectedIndex == -1) ||
                (chAscNode.Checked && comboAscNode.SelectedIndex == -1) ||
                (chLongPeric.Checked && comboLongPeric.SelectedIndex == -1) ||
                (chPeriod.Checked && comboPeriod.SelectedIndex == -1))
            {
                MessageBox.Show("Please select Greather than (>) or Less than (<)", "Error", MessageBoxButtons.OK);
                return;
            }

            if (chName.Checked && tbName.Text.Length == 0)
            {
                MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK);
                return;
            }

            if ((chPerihDate.Checked && tbPerihDateD.Text.Length == 0) || 
                (chPerihDate.Checked && tbPerihDateM.Text.Length == 0) || 
                (chPerihDate.Checked && tbPerihDateY.Text.Length == 0) ||
                (chPerihDist.Checked && tbPerihDist.Text.Length == 0) || 
                (chAphDist.Checked && tbAphDist.Text.Length == 0) || 
                (chEcc.Checked && tbEcc.Text.Length == 0) || 
                (chAscNode.Checked && tbAscNode.Text.Length == 0) ||
                (chLongPeric.Checked && tbLongPeric.Text.Length == 0) ||
                (chPeriod.Checked && tbPeriod.Text.Length == 0))
            {
                MessageBox.Show("Please enter value", "Error", MessageBoxButtons.OK);
                return;
            }

            //check date
            if (chPerihDate.Checked)
            {
                try
                {
                    DateTime test = new DateTime(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text));
                }
                catch
                {
                    MessageBox.Show("Invalid date", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            //clear all
            for (int i = 0; i < 18; i++) Form1.filterFlags[i] = false;
            for (int i = 0; i < 9; i++) Form1.filterValues[i] = 0.0;
            Form1.filterName = null;

            //get values
            if (chName.Checked)
            {
                //Form1.filterValue[0]
                Form1.filterName = tbName.Text; 

                if (comboName.SelectedIndex == 0) Form1.filterFlags[0] = true;
                if (comboName.SelectedIndex == 1) Form1.filterFlags[1] = true;
            }

            if (chPerihDate.Checked)
            {
                Form1.filterValues[1] = Comet.GregToJul(Convert.ToInt32(tbPerihDateY.Text), Convert.ToInt32(tbPerihDateM.Text), Convert.ToInt32(tbPerihDateD.Text), 0);

                if (comboPerihDate.SelectedIndex == 0) Form1.filterFlags[2] = true;
                if (comboPerihDate.SelectedIndex == 1) Form1.filterFlags[3] = true;
            }
            if (chPerihDist.Checked)
            {
                Form1.filterValues[2] = Convert.ToDouble(tbPerihDist.Text);

                if (comboPerihDist.SelectedIndex == 0) Form1.filterFlags[4] = true;
                if (comboPerihDist.SelectedIndex == 1) Form1.filterFlags[5] = true;
            }
            if (chAphDist.Checked)
            {
                Form1.filterValues[3] = Convert.ToDouble(tbAphDist.Text);

                if (comboAphDist.SelectedIndex == 0) Form1.filterFlags[6] = true;
                if (comboAphDist.SelectedIndex == 1) Form1.filterFlags[7] = true;
            }
            if (chEcc.Checked)
            {
                Form1.filterValues[4] = Convert.ToDouble(tbEcc.Text);

                if (comboEcc.SelectedIndex == 0) Form1.filterFlags[8] = true;
                if (comboEcc.SelectedIndex == 1) Form1.filterFlags[9] = true;
            }
            if (chAscNode.Checked)
            {
                Form1.filterValues[5] = Convert.ToDouble(tbAscNode.Text);

                if (comboAscNode.SelectedIndex == 0) Form1.filterFlags[10] = true;
                if (comboAscNode.SelectedIndex == 1) Form1.filterFlags[11] = true;
            }
            if (chLongPeric.Checked)
            {
                Form1.filterValues[6] = Convert.ToDouble(tbLongPeric.Text);

                if (comboLongPeric.SelectedIndex == 0) Form1.filterFlags[12] = true;
                if (comboLongPeric.SelectedIndex == 1) Form1.filterFlags[13] = true;
            }
            if (chIncl.Checked)
            {
                Form1.filterValues[7] = Convert.ToDouble(tbIncl.Text);

                if (comboIncl.SelectedIndex == 0) Form1.filterFlags[14] = true;
                if (comboIncl.SelectedIndex == 1) Form1.filterFlags[15] = true;
            }
            if (chPeriod.Checked)
            {
                Form1.filterValues[8] = Convert.ToDouble(tbPeriod.Text);

                if (comboPeriod.SelectedIndex == 0) Form1.filterFlags[16] = true;
                if (comboPeriod.SelectedIndex == 1) Form1.filterFlags[17] = true;
            }

            Form1.masterFilterFlag = true;
        }

        private void btnPerihDateNow_Click(object sender, EventArgs e)
        {
            tbPerihDateD.Text = DateTime.Now.Day.ToString("00");
            tbPerihDateM.Text = DateTime.Now.Month.ToString("00");
            tbPerihDateY.Text = DateTime.Now.Year.ToString("0000");
        }
    }
}
