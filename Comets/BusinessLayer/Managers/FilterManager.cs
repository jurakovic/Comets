using Comets.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DataTypeEnum = Comets.BusinessLayer.Business.Filter.DataTypeEnum;
using ValueCompareEnum = Comets.BusinessLayer.Business.Filter.ValueCompareEnum;

namespace Comets.BusinessLayer.Managers
{
	public static class FilterManager
	{
		#region FilterList

		public static List<Comet> FilterList(List<Comet> mainList, FilterCollection fc)
		{
			List<PropertyInfo> filterProps = fc.GetType().GetProperties().Where(x => (x.GetValue(fc, null) as Filter).Checked).ToList();

			List<Comet> list = new List<Comet>();
			List<bool> checks = new List<bool>();

			foreach (Comet comet in mainList)
			{
				checks.Clear();

				foreach (PropertyInfo fp in filterProps)
				{
					Filter f = fp.GetValue(fc, null) as Filter;
					object value = comet.GetType().GetProperty(fp.Name).GetValue(comet, null);

					if (f.DataType == DataTypeEnum.String)
					{
						string full = value.ToString();
						string[] names = f.Text.Split(',');

						if (f.ValueCompare == ValueCompareEnum.Greather_Contains)
							checks.Add(names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
						else
							checks.Add(!names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
					}
					else
					{
						double d = Convert.ToDouble(value);

						if (f.ValueCompare == ValueCompareEnum.Greather_Contains)
							checks.Add(d > f.Value);
						else
							checks.Add(d < f.Value);
					}
				}

				if (!checks.Any(x => x == false))
					list.Add(comet);
			}

			return list;
		}

		#endregion

		#region ValidateFilters

		public static bool ValidateFilters(FilterCollection filters)
		{
			bool retval = true;

			List<PropertyInfo> props = filters.GetType().GetProperties().Where(x => (x.GetValue(filters, null) as Filter).Checked).ToList();

			foreach (PropertyInfo prop in props)
			{
				Filter f = prop.GetValue(filters, null) as Filter;

				if (f.Value == 0.0)
				{
					string message = String.Format("Please enter value for \"{0}\" \t\t", Filter.PropertyName[prop.Name]);
					MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					retval = false;
					break;
				}
			}

			return retval;
		}

		#endregion

		#region HasAnythingToFilter

		public static bool HasAnythingToFilter(FilterCollection filters)
		{
			List<PropertyInfo> props = filters.GetType().GetProperties().ToList();
			return props.Any(x => (x.GetValue(filters, null) as Filter).Checked);
		}

		#endregion
	}
}
