using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class DateTimeControl : UserControl
	{
		#region Events

		public event Action<object, DateTime> OnSelectedDatetimeChanged
		{
			add { this.selectDateControl.OnSelectedDatetimeChanged += value; }
			remove { this.selectDateControl.OnSelectedDatetimeChanged -= value; }
		}

		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime SelectedDateTime
		{
			get { return selectDateControl.SelectedDateTime; }
			set { selectDateControl.SelectedDateTime = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime? DefaultDateTime
		{
			set { selectDateControl.DefaultDateTime = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime? PerihelionDate
		{
			set { selectDateControl.PerihelionDate = value; }
		}

		#endregion

		#region Constructor

		public DateTimeControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ShowDateTimeForm()
		{
			selectDateControl.ShowDateTimeForm();
		}

		#endregion
	}
}
