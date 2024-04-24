using Comets.Application.Common.Controls.DateAndTime;
using Comets.Application.Common.Managers;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.Application.Common.Controls.Database
{
	public partial class FilterControl : UserControl
	{
		#region Const

		List<PropertyEnum> DefaultFilters = new List<PropertyEnum>
		{
			PropertyEnum.full,
			PropertyEnum.Tn,
			PropertyEnum.q,
			PropertyEnum.CurrentMag
		};

		#endregion

		#region Events

		public event Action OnFilterApply;
		public event EventHandler OnClose;

		#endregion

		#region Properties

		public IButtonControl AcceptButton { get { return this.btnApply; } }

		#endregion

		#region Constructor

		public FilterControl()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void btnAddNew_Click(object sender, EventArgs e)
		{
			AddNewFilterPanel(null, PropertyEnum.full);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			OnFilterApply();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			OnClose(sender, e);
		}

		#endregion

		#region Methods

		public void DataBind(FilterCollection filters)
		{
			if (filters != null)
			{
				filters.ForEach(f => AddNewFilterPanel(f, null));
			}
			else
			{
				ClearFilters();
				DefaultFilters.ForEach(x => AddNewFilterPanel(null, x));
			}
		}

		public FilterCollection CollectFilters()
		{
			FilterCollection filters = new FilterCollection();

			foreach (Panel p in this.Controls.OfType<Panel>())
			{
				bool isChecked = false;
				int propertyIndex = 0;
				int compareIndex = 0;
				string txtStr = String.Empty;
				string txtVal = String.Empty;
				DateTime dt = DateTime.UtcNow;

				foreach (Control c in p.Controls)
				{
					if (c is CheckBox)
						isChecked = (c as CheckBox).Checked;
					else if (c is ComboBox && c.Name == FilterPanelManager.PropertyName)
						propertyIndex = (c as ComboBox).SelectedIndex;
					else if (c is ComboBox && c.Name == FilterPanelManager.CompareName)
						compareIndex = (c as ComboBox).SelectedIndex;
					else if (c is TextBox && c.Name == FilterPanelManager.StringName)
						txtStr = (c as TextBox).Text.Trim();
					else if (c is TextBox && c.Name == FilterPanelManager.ValueName)
						txtVal = (c as TextBox).Text.Trim();
					else if (c is SelectDateControl && c.Name == FilterPanelManager.DateName)
						dt = (c as SelectDateControl).SelectedDateTime;
				}

				if (propertyIndex >= 0)
				{
					PropertyEnum property = (PropertyEnum)propertyIndex;

					FilterManager.DataTypeEnum dataType = FilterPanelManager.GetDataType(property);

					string text;

					if (dataType == FilterManager.DataTypeEnum.String)
						text = txtStr;
					else if (property == PropertyEnum.Tn)
						text = dt.JD().ToString();
					else
						text = txtVal;

					Filter f = new Filter(property, dataType, isChecked, text, compareIndex);
					filters.Add(f);
				}
			}

			return filters;
		}

		private void AddNewFilterPanel(Filter filter, PropertyEnum? property)
		{
			Point location = new Point(1, 11);

			int id = 0;
			int count = 0;
			int offset = 31;
			int margin = 6;

			foreach (Panel p in this.Controls.OfType<Panel>())
			{
				count++;

				int pid = p.Name.Int();
				if (pid > id)
					id = pid;

				offset = p.Height + margin;
				location.Y += offset;
			}

			FilterPanelManager.CreateFilterPanel(this, ++id, location, CommonManager.DefaultDateStart, filter, property);

			btnAddNew.Location = new Point(btnAddNew.Location.X, btnAddNew.Location.Y + offset);
			btnAddNew.Visible = count < 9;
		}

		private void ClearFilters()
		{
			List<Panel> toRemove = this.Controls.OfType<Panel>().ToList();
			toRemove.ForEach(x => this.Controls.Remove(x));

			btnAddNew.Location = new Point(20, 7);
		}

		#endregion

		#region Overrides

		protected override CreateParams CreateParams
		{
			//http://stackoverflow.com/questions/2612487/how-to-fix-the-flickering-in-user-controls
			//http://social.msdn.microsoft.com/forums/en-US/winforms/thread/aaed00ce-4bc9-424e-8c05-c30213171c2c/

			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}

		#endregion
	}
}
