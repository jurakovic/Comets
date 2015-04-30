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
    }
}
