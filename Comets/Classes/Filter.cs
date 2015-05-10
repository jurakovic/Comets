using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Classes
{
    public class Filter
    {
        #region Enum

        public enum PropertyNameEnum { Undefined, Name, PerihelionDate, PerihelionDistance, Eccentricity, LongOfAscendingNode, ArgumentOfPericenter, Inclination, Period };

        public enum ValueResolveEnum { Undefined, Greather, Equal, Less, Contains, DoesNotContain };

        #endregion

        #region Properties

        public PropertyNameEnum PropertyName { get; set; }
        public bool IsChecked { get; set; }
        public string Text { get; set; }
        public double Value { get; set; }
        public ValueResolveEnum ValueResolve { get; set; }
        
        #endregion

        #region Constructor

        public Filter(PropertyNameEnum propertyName, bool isChecked, string text, int index)
        {
            PropertyName = propertyName;
            IsChecked = isChecked;
            Text = text;
            Value = (propertyName != PropertyNameEnum.Name && propertyName != PropertyNameEnum.PerihelionDate && !String.IsNullOrEmpty(text)) ? Convert.ToDouble(text) : -1.0;
            ValueResolve = GetValueResolveFromIndex(propertyName, index);
        }

        #endregion

        #region FilterList

        public static List<Comet> FilterList(List<Comet> MainList, List<Filter> fs)
        {
            List<Comet> list = new List<Comet>();

            foreach (Comet c in MainList)
            {
                if ((fs[0].IsChecked && fs[0].ValueResolve == ValueResolveEnum.Contains && !c.full.ToLower().Contains(fs[0].Text))
                 || (fs[0].IsChecked && fs[0].ValueResolve == ValueResolveEnum.DoesNotContain && c.full.ToLower().Contains(fs[0].Text))

                 || (fs[1].IsChecked && fs[1].ValueResolve == ValueResolveEnum.Greather && c.T < fs[1].Value)
                 || (fs[1].IsChecked && fs[1].ValueResolve == ValueResolveEnum.Equal && !((c.Td.ToString("00") + "." + c.Tm.ToString("00") + "." + c.Ty) == fs[1].Text))
                 || (fs[1].IsChecked && fs[1].ValueResolve == ValueResolveEnum.Less && c.T > fs[1].Value)

                 || (fs[2].IsChecked && fs[2].ValueResolve == ValueResolveEnum.Greather && c.q < fs[2].Value)
                 || (fs[2].IsChecked && fs[2].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.q, 3) == Math.Round(fs[2].Value, 3)))
                 || (fs[2].IsChecked && fs[2].ValueResolve == ValueResolveEnum.Less && c.q > fs[2].Value)

                 || (fs[3].IsChecked && fs[3].ValueResolve == ValueResolveEnum.Greather && c.e < fs[3].Value)
                 || (fs[3].IsChecked && fs[3].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.e, 3) == Math.Round(fs[3].Value, 3)))
                 || (fs[3].IsChecked && fs[3].ValueResolve == ValueResolveEnum.Less && c.e > fs[3].Value)

                 || (fs[4].IsChecked && fs[4].ValueResolve == ValueResolveEnum.Greather && c.N < fs[4].Value)
                 || (fs[4].IsChecked && fs[4].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.N, 3) == Math.Round(fs[4].Value, 3)))
                 || (fs[4].IsChecked && fs[4].ValueResolve == ValueResolveEnum.Less && c.N > fs[4].Value)

                 || (fs[5].IsChecked && fs[5].ValueResolve == ValueResolveEnum.Greather && c.w < fs[5].Value)
                 || (fs[5].IsChecked && fs[5].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.w, 3) == Math.Round(fs[5].Value, 3)))
                 || (fs[5].IsChecked && fs[5].ValueResolve == ValueResolveEnum.Less && c.w > fs[5].Value)

                 || (fs[6].IsChecked && fs[6].ValueResolve == ValueResolveEnum.Greather && c.i < fs[6].Value)
                 || (fs[6].IsChecked && fs[6].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.i, 3) == Math.Round(fs[6].Value, 3)))
                 || (fs[6].IsChecked && fs[6].ValueResolve == ValueResolveEnum.Less && c.i > fs[6].Value)

                 || (fs[7].IsChecked && fs[7].ValueResolve == ValueResolveEnum.Greather && c.P < fs[7].Value)
                 || (fs[7].IsChecked && fs[7].ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.P, 3) == Math.Round(fs[7].Value, 3)))
                 || (fs[7].IsChecked && fs[7].ValueResolve == ValueResolveEnum.Less && c.P > fs[7].Value)) continue;

                list.Add(c);
            }

            return list;
        }

        #region FilterList old

        //public static List<Comet> FilterList(List<Comet> MainList, List<Filter> fs)
        //{
        //    List<Comet> list = new List<Comet>();

        //    foreach (Comet c in MainList)
        //    {
        //        if (fs[0].IsChecked && fs[0].ValueResolve == FilterValueResolve.Contains && !c.full.ToLower().Contains(fs[0].Text)) continue;
        //        if (fs[0].IsChecked && fs[0].ValueResolve == FilterValueResolve.DoesNotContain && c.full.ToLower().Contains(fs[0].Text)) continue;

        //        if (fs[1].IsChecked && fs[1].ValueResolve == FilterValueResolve.Greather && c.T < fs[1].Value) continue;
        //        if (fs[1].IsChecked && fs[1].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.T) == fs[1].Value)) continue;
        //        if (fs[1].IsChecked && fs[1].ValueResolve == FilterValueResolve.Less && c.T > fs[1].Value) continue;

        //        if (fs[2].IsChecked && fs[2].ValueResolve == FilterValueResolve.Greather && c.q < fs[2].Value) continue;
        //        if (fs[2].IsChecked && fs[2].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.q) == fs[2].Value)) continue;
        //        if (fs[2].IsChecked && fs[2].ValueResolve == FilterValueResolve.Less && c.q > fs[2].Value) continue;

        //        if (fs[3].IsChecked && fs[3].ValueResolve == FilterValueResolve.Greather && c.e < fs[3].Value) continue;
        //        if (fs[3].IsChecked && fs[3].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.e) == fs[3].Value)) continue;
        //        if (fs[3].IsChecked && fs[3].ValueResolve == FilterValueResolve.Less && c.e > fs[3].Value) continue;

        //        if (fs[4].IsChecked && fs[4].ValueResolve == FilterValueResolve.Greather && c.N < fs[4].Value) continue;
        //        if (fs[4].IsChecked && fs[4].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.N) == fs[4].Value)) continue;
        //        if (fs[4].IsChecked && fs[4].ValueResolve == FilterValueResolve.Less && c.N > fs[4].Value) continue;

        //        if (fs[5].IsChecked && fs[5].ValueResolve == FilterValueResolve.Greather && c.w < fs[5].Value) continue;
        //        if (fs[5].IsChecked && fs[5].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.w) == fs[5].Value)) continue;
        //        if (fs[5].IsChecked && fs[5].ValueResolve == FilterValueResolve.Less && c.w > fs[5].Value) continue;

        //        if (fs[6].IsChecked && fs[6].ValueResolve == FilterValueResolve.Greather && c.i < fs[6].Value) continue;
        //        if (fs[6].IsChecked && fs[6].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.i) == fs[6].Value)) continue;
        //        if (fs[6].IsChecked && fs[6].ValueResolve == FilterValueResolve.Less && c.i > fs[6].Value) continue;

        //        if (fs[7].IsChecked && fs[7].ValueResolve == FilterValueResolve.Greather && c.P < fs[7].Value) continue;
        //        if (fs[7].IsChecked && fs[7].ValueResolve == FilterValueResolve.Equal && !(Math.Floor(c.P) == fs[7].Value)) continue;
        //        if (fs[7].IsChecked && fs[7].ValueResolve == FilterValueResolve.Less && c.P > fs[7].Value) continue;

        //        list.Add(c);
        //    }

        //    return list;
        //}

        #endregion

        #endregion

        #region ValidateFilters

        public static bool ValidateFilters(List<Filter> filters)
        {
            bool retval = true;

            foreach (Filter f in filters)
            {
                if (f.PropertyName == PropertyNameEnum.Name && f.IsChecked && String.IsNullOrEmpty(f.Text) || (f.IsChecked && f.Value < 0))
                {
                    MessageBox.Show(String.Format("Invalid \"{0}\" property value\t\t\t", f.PropertyName.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    retval = false;
                    break;
                }
            }

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
