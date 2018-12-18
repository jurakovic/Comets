using Comets.Core;
using System.Windows.Forms;

namespace Comets.Application.Graph
{
	public partial class ChartTypeControl : UserControl
	{
		#region Properties

		public GraphSettings.ChartType ChartType
		{
			get
			{
				GraphSettings.ChartType retval;

				if (rbtnMagnitude.Checked)
					retval = GraphSettings.ChartType.Magnitude;
				else if (rbtnSunDistance.Checked)
					retval = GraphSettings.ChartType.SunDistance;
				else// if (rbtnEarthDistance.Checked)
					retval = GraphSettings.ChartType.EarthDistance;

				return retval;
			}
			set
			{
				switch (value)
				{
					case GraphSettings.ChartType.Magnitude:
						rbtnMagnitude.Checked = true;
						break;
					case GraphSettings.ChartType.SunDistance:
						rbtnSunDistance.Checked = true;
						break;
					case GraphSettings.ChartType.EarthDistance:
						rbtnEarthDistance.Checked = true;
						break;
				}
			}
		}

		#endregion

		#region Constructor

		public ChartTypeControl()
		{
			InitializeComponent();
		}

		#endregion
	}
}
