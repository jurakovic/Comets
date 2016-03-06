using Comets.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comets.BusinessLayer.Managers
{
	public static class FilterManager
	{
		#region Enum

		public enum DataTypeEnum { String, Double };

		public enum ValueCompareEnum { Greather_Contains, Less_DoesNotContain };

		#endregion

		#region ApplyFilters

		public static CometCollection ApplyFilters(CometCollection mainCollection, FilterCollection filters)
		{
			CometCollection collection = new CometCollection();
			List<bool> checks = new List<bool>();

			List<Filter> checkedFilters = filters.Where(x => x.Checked).ToList();

			foreach (Comet comet in mainCollection)
			{
				checks.Clear();

				foreach (Filter f in checkedFilters)
				{
					object value = comet.GetType().GetProperty(f.Property.ToString()).GetValue(comet, null);

					if (f.DataType == DataTypeEnum.String)
					{
						string full = value.ToString().ToLower();
						string[] names = f.Text.ToLower().Split(',');

						if (f.ValueCompare == ValueCompareEnum.Greather_Contains)
							checks.Add(names.Any(x => full.Contains(x.Trim())));
						else
							checks.Add(!names.Any(x => full.Contains(x.Trim())));
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

				if (checks.All(x => x == true))
					collection.Add(comet);
			}

			return collection;
		}

		#endregion

		#region ValidateFilters

		public static string ValidateFilters(FilterCollection filters)
		{
			string retval = null;

			foreach (Filter f in filters.Where(x => x.Checked))
			{
				if (f.Value == 0.0)
				{
					retval = String.Format("Please enter value for \"{0}\" \t\t", FilterPanelManager.GetText(f.Property));
					break;
				}
			}

			return retval;
		}

		#endregion
	}
}
