using Comets.Application.Common.Controls.Common;
using Comets.OrbitViewer;
using System;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class SimulationControl : ValueChangeControl
	{
		#region Events

		public event Action<SimulationEvent> OnSimulationEvent;
		public event Action<ATimeSpan> OnTimespanChanged;

		#endregion

		#region Enum

		public enum SimulationEvent
		{
			PlayBack,
			StepBack,
			Stop,
			StepForward,
			PlayForward
		}

		#endregion

		#region Const

		private readonly string[] TimeStepItems =
		{
			"1 Hour",
			"6 Hours",
			"12 Hours",
			"1 Day",
			"3 Days",
			"10 Days",
			"1 Month",
			"3 Months",
			"6 Months",
			"1 Year",
			"3 Years",
			"10 Years"
		};

		private readonly ATimeSpan[] TimeStepSpan =
		{
			new ATimeSpan( 0, 0, 0, 1, 0, 0),
			new ATimeSpan( 0, 0, 0, 6, 0, 0),
			new ATimeSpan( 0, 0, 0,12, 0, 0),
			new ATimeSpan( 0, 0, 1, 0, 0, 0),
			new ATimeSpan( 0, 0, 3, 0, 0, 0),
			new ATimeSpan( 0, 0,10, 0, 0, 0),
			new ATimeSpan( 0, 1, 0, 0, 0, 0),
			new ATimeSpan( 0, 3, 0, 0, 0, 0),
			new ATimeSpan( 0, 6, 0, 0, 0, 0),
			new ATimeSpan( 1, 0, 0, 0, 0, 0),
			new ATimeSpan( 3, 0, 0, 0, 0, 0),
			new ATimeSpan(10, 0, 0, 0, 0, 0)
		};

		#endregion

		#region Properties

		public new bool Focused
		{
			get { return cboTimestep.Focused; }
		}

		#endregion

		#region Constructor

		public SimulationControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void SimulationControl_Load(object sender, EventArgs e)
		{
			cboTimestep.DataSource = TimeStepItems;
			cboTimestep.SelectedIndex = 3;
		}

		private void btnRevPlay_Click(object sender, EventArgs e)
		{
			OnSimulationEvent(SimulationEvent.PlayBack);
		}

		private void btnRevStep_Click(object sender, EventArgs e)
		{
			OnSimulationEvent(SimulationEvent.StepBack);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			OnSimulationEvent(SimulationEvent.Stop);
		}

		private void btnForStep_Click(object sender, EventArgs e)
		{
			OnSimulationEvent(SimulationEvent.StepForward);
		}

		private void btnForPlay_Click(object sender, EventArgs e)
		{
			OnSimulationEvent(SimulationEvent.PlayForward);
		}

		private void cboTimestep_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetTimespan();
		}

		#endregion

		#region Methods

		public void FasterSimulation()
		{
			if (cboTimestep.SelectedIndex < cboTimestep.Items.Count - 1)
				cboTimestep.SelectedIndex++;
		}

		public bool SlowerSimulation()
		{
			bool retval = true;

			if (cboTimestep.SelectedIndex > 0)
				cboTimestep.SelectedIndex--;
			else
				retval = false;

			return retval;
		}

		private void SetTimespan()
		{
			//?.Invoke because of error in designer
			OnTimespanChanged?.Invoke(TimeStepSpan[cboTimestep.SelectedIndex]);
		}

		#endregion
	}
}
