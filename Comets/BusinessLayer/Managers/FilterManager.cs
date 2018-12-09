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

		public enum StringCompareEnum { Contains, DoesNotContain, StartsWith, EndsWith };

		public enum DoubleCompareEnum { GreatherThan, LessThan, Equals };

		#endregion

		#region ApplyFilters

		public static CometCollection ApplyFilters(CometCollection mainCollection, FilterCollection filters)
		{
			CometCollection collection;

			IEnumerable<Filter> checkedFilters = filters.Where(x => x.Checked);

			if (checkedFilters.Any())
			{
				collection = new CometCollection();
				List<bool> checks = new List<bool>();

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

							switch (f.CompareIndex)
							{
								case (int)StringCompareEnum.Contains:
									checks.Add(names.Any(x => full.Contains(x.Trim())));
									break;
								case (int)StringCompareEnum.DoesNotContain:
									checks.Add(!names.Any(x => full.Contains(x.Trim())));
									break;
								case (int)StringCompareEnum.StartsWith:
									checks.Add(names.Any(x => full.StartsWith(x.Trim())));
									break;
								case (int)StringCompareEnum.EndsWith:
									checks.Add(names.Any(x => full.EndsWith(x.Trim())));
									break;
							}
						}
						else
						{
							double d = Convert.ToDouble(value);
							double offset = CometManager.EqualValueOffset[f.Property];

							switch (f.CompareIndex)
							{
								case (int)DoubleCompareEnum.GreatherThan:
									checks.Add(d > f.Value);
									break;
								case (int)DoubleCompareEnum.LessThan:
									checks.Add(d < f.Value);
									break;
								case (int)DoubleCompareEnum.Equals:
									checks.Add(Math.Abs(d - f.Value) <= offset);
									break;
							}
						}
					}

					if (checks.All(x => x == true))
						collection.Add(comet);
				}
			}
			else
			{
				collection = new CometCollection(mainCollection);
			}

			return collection;
		}

		#endregion

		#region ValidateFilters

		public static string ValidateFilters(FilterCollection filters)
		{
			string retval = null;

			Filter filter = filters.FirstOrDefault(x => x.Checked && !x.IsValid);
			if (filter != null)
				retval = String.Format("Please enter value for \"{0}\"", FilterPanelManager.GetText(filter.Property));

			return retval;
		}

		#endregion
	}
}
