using Comets.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PropertyNameEnum = Comets.BusinessLayer.Business.Filter.PropertyNameEnum;
using ValueResolveEnum = Comets.BusinessLayer.Business.Filter.ValueResolveEnum;

namespace Comets.BusinessLayer.Managers
{
	public static class FilterManager
	{
		#region FilterList

		public static List<Comet> FilterList(List<Comet> MainList, FilterCollection fs)
		{
			List<Comet> list = new List<Comet>();
			string[] names = fs.Name.Text.Split(',');

			foreach (Comet c in MainList)
			{
				if (fs.Name.Checked && fs.Name.ValueResolve == ValueResolveEnum.Contains && !names.Any(x => c.full.ToLower().Contains(x.Trim().ToLower()))) continue;
				if (fs.Name.Checked && fs.Name.ValueResolve == ValueResolveEnum.DoesNotContain && names.Any(x => c.full.ToLower().Contains(x.Trim().ToLower()))) continue;

				if (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Greather && c.T < fs.PerihelionDate.Value) continue;
				if (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.T) == Math.Round(fs.PerihelionDate.Value))) continue;
				if (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Less && c.T > fs.PerihelionDate.Value) continue;

				if (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Greather && c.q < fs.PerihelionDistance.Value) continue;
				if (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.q, 3) == Math.Round(fs.PerihelionDistance.Value, 3))) continue;
				if (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Less && c.q > fs.PerihelionDistance.Value) continue;

				if (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Greather && c.e < fs.Eccentricity.Value) continue;
				if (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.e, 3) == Math.Round(fs.Eccentricity.Value, 3))) continue;
				if (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Less && c.e > fs.Eccentricity.Value) continue;

				if (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Greather && c.N < fs.LongOfAscendingNode.Value) continue;
				if (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.N, 3) == Math.Round(fs.LongOfAscendingNode.Value, 3))) continue;
				if (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Less && c.N > fs.LongOfAscendingNode.Value) continue;

				if (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Greather && c.w < fs.ArgumentOfPericenter.Value) continue;
				if (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.w, 3) == Math.Round(fs.ArgumentOfPericenter.Value, 3))) continue;
				if (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Less && c.w > fs.ArgumentOfPericenter.Value) continue;

				if (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Greather && c.i < fs.Inclination.Value) continue;
				if (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.i, 3) == Math.Round(fs.Inclination.Value, 3))) continue;
				if (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Less && c.i > fs.Inclination.Value) continue;

				if (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Greather && c.P < fs.Period.Value) continue;
				if (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.P, 3) == Math.Round(fs.Period.Value, 3))) continue;
				if (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Less && c.P > fs.Period.Value) continue;

				list.Add(c);
			}

			return list;
		}

		#endregion

		#region ValidateFilters

		public static bool ValidateFilters(FilterCollection filters)
		{
			bool retval = true;

			Type type = filters.GetType();
			List<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

			foreach (PropertyInfo prop in props)
			{
				Filter f = prop.GetValue(filters, null) as Filter;

				if (f.PropertyName == PropertyNameEnum.Name && f.Checked && String.IsNullOrEmpty(f.Text) ||
				   (f.PropertyName != PropertyNameEnum.Name && f.Checked && f.Value == 0.0))
				{
					string message = String.Format("Please enter value for \"{0}\" \t\t", Filter.PropertyNameString[(int)f.PropertyName]);
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
			bool retval = false;

			Type type = filters.GetType();
			List<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

			retval = props.Any(x => (x.GetValue(filters, null) as Filter).Checked);

			return retval;
		}

		#endregion

		#region GetValueResolveFromIndex

		public static ValueResolveEnum GetValueResolveFromIndex(PropertyNameEnum properyNameEnum, int index)
		{
			ValueResolveEnum retval = ValueResolveEnum.Undefined;

			if (properyNameEnum == PropertyNameEnum.Name)
			{
				switch (index)
				{
					case 0: retval = ValueResolveEnum.Contains; break;
					case 1: retval = ValueResolveEnum.DoesNotContain; break;
				}
			}
			else
			{
				switch (index)
				{
					case 0: retval = ValueResolveEnum.Greather; break;
					case 1: retval = ValueResolveEnum.Equal; break;
					case 2: retval = ValueResolveEnum.Less; break;
				}
			}

			return retval;
		}

		#endregion

		#region GetIndexFromValueResolve

		public static int GetIndexFromValueResolve(ValueResolveEnum valueResolveEnum)
		{
			int retval = -1;

			switch (valueResolveEnum)
			{
				case ValueResolveEnum.Contains:
				case ValueResolveEnum.Greather:
					retval = 0;
					break;

				case ValueResolveEnum.DoesNotContain:
				case ValueResolveEnum.Equal:
					retval = 1;
					break;

				case ValueResolveEnum.Less:
					retval = 2;
					break;
			}

			return retval;
		}

		#endregion
	}
}
