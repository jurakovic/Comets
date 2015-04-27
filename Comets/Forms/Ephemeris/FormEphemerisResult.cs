using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Forms.Ephemeris
{
    public partial class FormEphemerisResult : Form
    {
        public FormEphemerisResult(string text)
        {
            InitializeComponent();
            richTextBox1.Text = text;
        }

        private void FormEphemerisResult_Load(object sender, EventArgs e)
        {

        }
    }
}
