using Comets.Application.Common.Controls.Common;
using System;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class ModeControl : ValueChangeControl
	{
		#region Events

		public event Action OnModeChanged;

		#endregion

		#region Properties

		public bool MultipleMode
		{
			get { return rbtnMultipleMode.Checked; }
		}

		#endregion

		#region Constructor

		public ModeControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void rbtnCommon_CheckedChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
				OnModeChanged();
		}

		#endregion

		#region Methods

		public void SetMode(bool isMultiple)
		{
			ValueChangedInternal = true;

			if (isMultiple)
				rbtnMultipleMode.Checked = true;
			else
				rbtnSingleMode.Checked = true;

			ValueChangedInternal = false;
		}

		#endregion
	}
}
