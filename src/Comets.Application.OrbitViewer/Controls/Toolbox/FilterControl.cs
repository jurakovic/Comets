using Comets.Application.Common.Controls.Common;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.OrbitViewer.Controls
{
	public partial class FilterControl : ValueChangeControl
	{
		#region Events

		public event Action<FilterValue, double?> OnFilterValueChanged;
		public event Action<bool> OnShowInWeakColorChanged;

		#endregion

		#region Enum

		public enum FilterValue
		{
			SunDistance,
			EarthDistance,
			Magnitude
		}

		#endregion

		#region Properties

		public new bool Focused
		{
			get { return txtFodSunDist.Focused || txtFodEarthDist.Focused || txtFodMagnitude.Focused; }
		}

		#endregion

		#region Constructor

		public FilterControl()
		{
			InitializeComponent();

			txtFodSunDist.Tag = new ValNum(0.0, 99.99, 2);
			txtFodEarthDist.Tag = new ValNum(0.0, 99.99, 2);
			txtFodMagnitude.Tag = ValNum.VMagnitude;
		}

		#endregion

		#region EventHandling

		private void filterOnDateTxtCbxCommon_TextChangedCheckedChanged(object sender, EventArgs e)
		{
			if (!ValueChangedInternal)
			{
				TextBox txt = null;
				CheckBox cbx = null;

				bool isTxt = false;
				bool isCbx = false;

				string namePart = null;
				double? value = null;

				FilterValue filter;

				if (sender is TextBox)
				{
					txt = sender as TextBox;
					namePart = txt.Name.Replace("txt", String.Empty);
					cbx = pnlFilterOnDate.Controls.Find("cbx" + namePart, false).Single() as CheckBox;
					isTxt = true;
				}

				if (sender is CheckBox)
				{
					cbx = sender as CheckBox;
					namePart = cbx.Name.Replace("cbx", String.Empty);
					txt = pnlFilterOnDate.Controls.Find("txt" + namePart, false).Single() as TextBox;
					isCbx = true;
				}

				if (!String.IsNullOrEmpty(txt.Text) && (isTxt || (isCbx && cbx.Checked)))
					value = txt.Double();

				switch (namePart)
				{
					case "FodSunDist":
						filter = FilterValue.SunDistance;
						break;
					case "FodEarthDist":
						filter = FilterValue.EarthDistance;
						break;
					case "FodMagnitude":
						filter = FilterValue.Magnitude;
						break;
					default:
						throw new InvalidOperationException();
				}

				if (isTxt)
				{
					ValueChangedInternal = true;
					cbx.Checked = value != null;
					ValueChangedInternal = false;
				}

				OnFilterValueChanged(filter, value);
			}
		}

		private void txtFilterOnDateCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		private void cbxWeakColor_CheckedChanged(object sender, EventArgs e)
		{
			OnShowInWeakColorChanged(cbxWeakColor.Checked);
		}

		#endregion
	}
}
