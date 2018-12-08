using System.Windows.Forms;

namespace Comets.Application.Controls.ModulEphemeris
{
	public partial class OutputDataControl : UserControl
	{
		#region Properties

		public bool LocalTime
		{
			get
			{
				return radioLocalTime.Checked;
			}
			set
			{
				radioLocalTime.Checked = value;
				radioUnivTime.Checked = !value;
			}
		}

		public bool RA
		{
			get { return chRA.Checked; }
			set { chRA.Checked = value; }
		}

		public bool Dec
		{
			get { return chDec.Checked; }
			set { chDec.Checked = value; }
		}

		public bool EcLon
		{
			get { return chEcLon.Checked; }
			set { chEcLon.Checked = value; }
		}

		public bool EcLat
		{
			get { return chEcLat.Checked; }
			set { chEcLat.Checked = value; }
		}

		public bool HelioDist
		{
			get { return chHelioDist.Checked; }
			set { chHelioDist.Checked = value; }
		}

		public bool GeoDist
		{
			get { return chGeoDist.Checked; }
			set { chGeoDist.Checked = value; }
		}

		public bool Alt
		{
			get { return chAlt.Checked; ; }
			set { chAlt.Checked = value; }
		}

		public bool Az
		{
			get { return chAz.Checked; }
			set { chAz.Checked = value; }
		}

		public bool Elongation
		{
			get { return chElong.Checked; }
			set { chElong.Checked = value; }
		}

		public bool Magnitude
		{
			get { return chMag.Checked; }
			set { chMag.Checked = value; }
		}

		#endregion

		#region Constructor

		public OutputDataControl()
		{
			InitializeComponent();
		}

		#endregion
	}
}
