using Comets.Core;
using System;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.DateAndTime
{
	public partial class SelectDateControl : UserControl
	{
		#region Events

		public event Action<DateTime> OnSelectedDatetimeChanged;

		#endregion

		#region Fields

		private DateTime _selectedDateTime;
		private DateTime? _defaultDateTime;
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
				_selectedDateTime = dateTimeMenuControl.SelectedDateTime = value;
				btnSelectDate.Text = _selectedDateTime.ToString(DateTimeFormat.Full);
			}
		}

		public DateTime? DefaultDateTime
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
			dateTimeMenuControl.OnSelectedDatetimeChanged += OnSelectedDatetimeChangedInternal;
			dateTimeMenuControl.ReferenceControl = this.btnSelectDate;
		}

		#endregion

		#region EventHandling

		private void btnSelectDate_Click(object sender, EventArgs e)
		{
			using (FormDateTime fdt = new FormDateTime(SelectedDateTime, DefaultDateTime, PerihelionDate))
			{
				fdt.TopMost = this.ParentForm.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
					OnSelectedDatetimeChangedInternal(fdt.SelectedDateTime);
			}
		}

		#endregion

		#region Methods

		private void OnSelectedDatetimeChangedInternal(DateTime dateTime)
		{
			SelectedDateTime = dateTime;
			OnSelectedDatetimeChanged?.Invoke(dateTime);
		}

		#endregion
	}
}
