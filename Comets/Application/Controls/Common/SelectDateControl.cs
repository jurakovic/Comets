using System;
using System.Windows.Forms;

namespace Comets.Application.Controls.Common
{
	public partial class SelectDateControl : UserControl
	{
		#region Fields

		private DateTime _selectedDateTime;
		private DateTime _defaultDateTime;
		private DateTime? _perihelionDate;

		#endregion

		#region Properties

		public DateTime SelectedDateTime
		{
			get
			{
				return _selectedDateTime;
			}
			set
			{
				_selectedDateTime = value;
				btnSelectDate.Text = _selectedDateTime.ToString(FormMain.DateTimeFormatMain);
			}
		}

		public DateTime DefaultDateTime
		{
			get { return _defaultDateTime; }
			set { _defaultDateTime = dateTimeMenuControl.DefaultDateTime = value; }
		}

		public DateTime? PerihelionDate
		{
			get { return _perihelionDate; }
			set { _perihelionDate = dateTimeMenuControl.PerihelionDate = value; }
		}

		#endregion

		#region Constructor

		public SelectDateControl()
		{
			InitializeComponent();
			dateTimeMenuControl.OnSelectedDatetimeChanged += OnSelectedDateTimeChanged;
		}

		#endregion

		#region EventHandling

		private void btnSelectDate_Click(object sender, EventArgs e)
		{
			using (FormDateTime fdt = new FormDateTime(DefaultDateTime, SelectedDateTime, PerihelionDate))
			{
				fdt.TopMost = this.ParentForm.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					OnSelectedDateTimeChanged(fdt.SelectedDateTime);
			}
		}

		public void OnSelectedDateTimeChanged(DateTime dateTime)
		{
			SelectedDateTime = dateTime;
		}

		#endregion
	}
}
