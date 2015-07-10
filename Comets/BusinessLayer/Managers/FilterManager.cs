using Comets.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PropertyEnum = Comets.BusinessLayer.Business.Filter.PropertyEnum;
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

					if (f.Property == PropertyEnum.Name)
					{
						string full = value.ToString();
						string[] names = f.Text.Split(',');

						if (f.ValueCompare == ValueCompareEnum.Contains)
							checks.Add(names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
						else
							checks.Add(!names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
					}
					else
					{
						double d = Convert.ToDouble(value);

						if (f.ValueCompare == ValueCompareEnum.Greather)
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

			List<PropertyInfo> props = filters.GetType().GetProperties().ToList();

			foreach (PropertyInfo prop in props)
			{
				Filter f = prop.GetValue(filters, null) as Filter;

				if (f.Checked && f.Value == 0.0)
				{
					string message = String.Format("Please enter value for \"{0}\" \t\t", Filter.PropertyNameString[(int)f.Property]);
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

		#region GetValueCompareFromIndex

		public static ValueCompareEnum GetValueCompareFromIndex(PropertyEnum properyNameEnum, int index)
		{
			ValueCompareEnum retval = ValueCompareEnum.Undefined;

			if (properyNameEnum == PropertyEnum.Name)
			{
				switch (index)
				{
					case 0: retval = ValueCompareEnum.Contains; break;
					case 1: retval = ValueCompareEnum.DoesNotContain; break;
				}
			}
			else
			{
				switch (index)
				{
					case 0: retval = ValueCompareEnum.Greather; break;
					case 1: retval = ValueCompareEnum.Less; break;
				}
			}

			return retval;
		}

		#endregion

		#region GetIndexFromValueCompare

		public static int GetIndexFromValueCompare(ValueCompareEnum valueResolveEnum)
		{
			int retval = -1;

			switch (valueResolveEnum)
			{
				case ValueCompareEnum.Contains:
				case ValueCompareEnum.Greather:
					retval = 0;
					break;

				case ValueCompareEnum.DoesNotContain:
				case ValueCompareEnum.Less:
					retval = 1;
					break;
			}

			return retval;
		}

		#endregion
	}
}
