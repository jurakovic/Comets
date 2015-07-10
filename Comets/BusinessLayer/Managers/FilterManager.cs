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
						{
							checks.Add(d > f.Value);
						}
						else if (f.ValueCompare == ValueCompareEnum.Equal)
						{
							if (f.Property == PropertyEnum.PerihelionDate)
								checks.Add(Math.Abs((Utils.JDToDateTime(d) - Utils.JDToDateTime(f.Value)).TotalDays) <= 1.0);
							else
								checks.Add((Math.Round(d, 3) == Math.Round(f.Value, 3)));
						}
						else
						{
							checks.Add(d < f.Value);
						}
					}
				}

				if (!checks.Any(x => x == false))
					list.Add(comet);
			}

			return list;
		}

		#region Obsolete

		[Obsolete]
		private static List<Comet> FilterList_Obsolete(List<Comet> MainList, FilterCollection fs)
		{
			List<Comet> list = new List<Comet>();
			string[] names = fs.full.Text.Split(',');

			foreach (Comet c in MainList)
			{
				if (fs.full.Checked && fs.full.ValueCompare == ValueCompareEnum.Contains && !names.Any(x => c.full.ToLower().Contains(x.Trim().ToLower()))) continue;
				if (fs.full.Checked && fs.full.ValueCompare == ValueCompareEnum.DoesNotContain && names.Any(x => c.full.ToLower().Contains(x.Trim().ToLower()))) continue;

				if (fs.T.Checked && fs.T.ValueCompare == ValueCompareEnum.Greather && c.T < fs.T.Value) continue;
				if (fs.T.Checked && fs.T.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.T) == Math.Round(fs.T.Value))) continue;
				if (fs.T.Checked && fs.T.ValueCompare == ValueCompareEnum.Less && c.T > fs.T.Value) continue;

				if (fs.q.Checked && fs.q.ValueCompare == ValueCompareEnum.Greather && c.q < fs.q.Value) continue;
				if (fs.q.Checked && fs.q.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.q, 3) == Math.Round(fs.q.Value, 3))) continue;
				if (fs.q.Checked && fs.q.ValueCompare == ValueCompareEnum.Less && c.q > fs.q.Value) continue;

				if (fs.e.Checked && fs.e.ValueCompare == ValueCompareEnum.Greather && c.e < fs.e.Value) continue;
				if (fs.e.Checked && fs.e.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.e, 3) == Math.Round(fs.e.Value, 3))) continue;
				if (fs.e.Checked && fs.e.ValueCompare == ValueCompareEnum.Less && c.e > fs.e.Value) continue;

				if (fs.N.Checked && fs.N.ValueCompare == ValueCompareEnum.Greather && c.N < fs.N.Value) continue;
				if (fs.N.Checked && fs.N.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.N, 3) == Math.Round(fs.N.Value, 3))) continue;
				if (fs.N.Checked && fs.N.ValueCompare == ValueCompareEnum.Less && c.N > fs.N.Value) continue;

				if (fs.w.Checked && fs.w.ValueCompare == ValueCompareEnum.Greather && c.w < fs.w.Value) continue;
				if (fs.w.Checked && fs.w.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.w, 3) == Math.Round(fs.w.Value, 3))) continue;
				if (fs.w.Checked && fs.w.ValueCompare == ValueCompareEnum.Less && c.w > fs.w.Value) continue;

				if (fs.i.Checked && fs.i.ValueCompare == ValueCompareEnum.Greather && c.i < fs.i.Value) continue;
				if (fs.i.Checked && fs.i.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.i, 3) == Math.Round(fs.i.Value, 3))) continue;
				if (fs.i.Checked && fs.i.ValueCompare == ValueCompareEnum.Less && c.i > fs.i.Value) continue;

				if (fs.P.Checked && fs.P.ValueCompare == ValueCompareEnum.Greather && c.P < fs.P.Value) continue;
				if (fs.P.Checked && fs.P.ValueCompare == ValueCompareEnum.Equal && !(Math.Round(c.P, 3) == Math.Round(fs.P.Value, 3))) continue;
				if (fs.P.Checked && fs.P.ValueCompare == ValueCompareEnum.Less && c.P > fs.P.Value) continue;

				list.Add(c);
			}

			return list;
		}

		#endregion

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

				if (f.Checked && f.Property == PropertyEnum.Name && String.IsNullOrEmpty(f.Text) ||
				   (f.Checked && f.Property != PropertyEnum.Name && f.Value == 0.0))
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
					case 1: retval = ValueCompareEnum.Equal; break;
					case 2: retval = ValueCompareEnum.Less; break;
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
				case ValueCompareEnum.Equal:
					retval = 1;
					break;

				case ValueCompareEnum.Less:
					retval = 2;
					break;
			}

			return retval;
		}

		#endregion
	}
}
