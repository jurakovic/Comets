using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Comets.Application.Graph
{
	public partial class ChartOptionsControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color MagnitudeColor
		{
			get { return pnlMagnitudeColor.BackColor; }
			set { pnlMagnitudeColor.BackColor = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool NowLineChecked
		{
			get { return cbxNowLine.Checked; }
			set { cbxNowLine.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color NowLineColor
		{
			get { return pnlNowLineColor.BackColor; }
			set { pnlNowLineColor.BackColor = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool PerihelionLineChecked
		{
			get { return cbxPerihelionLine.Checked; }
			set { cbxPerihelionLine.Checked = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color PerihelionLineColor
		{
			get { return pnlPerihLineColor.BackColor; }
			set { pnlPerihLineColor.BackColor = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AntialiasingChecked
		{
			get { return cbxAntialiasing.Checked; }
			set { cbxAntialiasing.Checked = value; }
		}

		#endregion

		#region Constructor

		public ChartOptionsControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void pnlColorCommon_Click(object sender, EventArgs e)
		{
			Panel pnl = sender as Panel;

			using (ColorDialog cd = new ColorDialog())
			{
				cd.Color = pnl.BackColor;
				cd.FullOpen = true;

				if (cd.ShowDialog() == DialogResult.OK)
					pnl.BackColor = cd.Color;
			}
		}

		#endregion
	}
}
