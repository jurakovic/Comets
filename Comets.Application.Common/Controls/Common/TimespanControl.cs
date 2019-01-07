using Comets.Application.Common.General;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{
	public partial class TimespanControl : UserControl
	{
		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime DateStart
		{
			get { return selectDateControlStart.SelectedDateTime; }
			set { selectDateControlStart.SelectedDateTime = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime DateEnd
		{
			get { return selectDateControlEnd.SelectedDateTime; }
			set { selectDateControlEnd.SelectedDateTime = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime? PerihelionDate
		{
			set { selectDateControlStart.PerihelionDate = selectDateControlEnd.PerihelionDate = value; }
		}

		#endregion

		#region Constructor

		public TimespanControl()
		{
			InitializeComponent();

			selectDateControlStart.DefaultDateTime = CommonManager.DefaultDateStart;
			selectDateControlEnd.DefaultDateTime = CommonManager.DefaultDateEnd;
		}

		#endregion

		#region ValidateData

		public void ValidateData()
		{
			if (this.DateEnd <= this.DateStart)
				throw new ValidationException("End date must be greather than start date", selectDateControlStart);

			if ((this.DateEnd - this.DateStart).TotalDays >= 300 * 365.25)
				throw new ValidationException("Timespan must be less than 300 years", selectDateControlStart);
		}

		#endregion
	}
}
