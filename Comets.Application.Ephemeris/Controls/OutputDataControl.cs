using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Ephemeris
{
	public partial class OutputDataControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool RA
		{
			get { return chRA.Checked; }
			set { chRA.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Dec
		{
			get { return chDec.Checked; }
			set { chDec.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EcLon
		{
			get { return chEcLon.Checked; }
			set { chEcLon.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EcLat
		{
			get { return chEcLat.Checked; }
			set { chEcLat.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HelioDist
		{
			get { return chHelioDist.Checked; }
			set { chHelioDist.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool GeoDist
		{
			get { return chGeoDist.Checked; }
			set { chGeoDist.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Alt
		{
			get { return chAlt.Checked; ; }
			set { chAlt.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Az
		{
			get { return chAz.Checked; }
			set { chAz.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Elongation
		{
			get { return chElong.Checked; }
			set { chElong.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
