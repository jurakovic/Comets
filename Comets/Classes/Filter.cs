using Comets.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Classes
{
    public class Filter
    {
        #region Enum

        public enum PropertyNameEnum { Undefined = 0, Name, PerihelionDate, PerihelionDistance, Eccentricity, LongOfAscendingNode, ArgOfPericenter, Inclination, Period };

        public enum ValueResolveEnum { Undefined = 0, Greather, Equal, Less, Contains, DoesNotContain };

        #endregion

        #region Const

        public static string[] PropertyNameString = { "Undefined", "Name", "Perihelion date", "Perihelion distance", "Eccentricity", "Longitude of Ascending Node", "Argument of Pericenter", "Inclination", "Period" };

        #endregion

        #region Fields

        private PropertyNameEnum _propertyName;
        private bool _checked;
        private string _text;
        private double _value;
        private int _index;
        private ValueResolveEnum _valueResolve;

        #endregion

        #region Properties

        public PropertyNameEnum PropertyName
        {
            get { return _propertyName; }
            protected set { _propertyName = value; }
        }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                if (_checked && _propertyName != PropertyNameEnum.Name && _propertyName != PropertyNameEnum.PerihelionDate)
                {
                    _value = Utils.ConvertToDouble(_text);
                }
                else if (_checked && _propertyName == PropertyNameEnum.PerihelionDate)
                {

                    string[] dt = _text.Split('.');
                    try
                    {
                        _value = EphemerisHelper.jd0(Convert.ToInt32(dt[2]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]), 0);
                    }
                    catch
                    {
                        _value = 0.0;
                    }
                }
            }
        }

        public double Value
        {
            get { return _value; }
        }

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                _valueResolve = GetValueResolveFromIndex(_propertyName, _index);
            }
        }

        public ValueResolveEnum ValueResolve
        {
            get { return _valueResolve; }
        }

        #endregion

        #region Constructor

        public Filter(PropertyNameEnum propertyName)
        {
            PropertyName = propertyName;
        }

        #endregion

        #region FilterList

        public static List<Comet> FilterList(List<Comet> MainList, FilterCollection fs)
        {
            List<Comet> list = new List<Comet>();

            foreach (Comet c in MainList)
            {
                if ((fs.Name.Checked && fs.Name.ValueResolve == ValueResolveEnum.Contains && !c.full.ToLower().Contains(fs.Name.Text))
                || (fs.Name.Checked && fs.Name.ValueResolve == ValueResolveEnum.DoesNotContain && c.full.ToLower().Contains(fs.Name.Text))

                || (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Greather && c.T < fs.PerihelionDate.Value)
                || (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Equal && !((c.Td.ToString("00") + "." + c.Tm.ToString("00") + "." + c.Ty) == fs.PerihelionDate.Text))
                || (fs.PerihelionDate.Checked && fs.PerihelionDate.ValueResolve == ValueResolveEnum.Less && c.T > fs.PerihelionDate.Value)

                || (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Greather && c.q < fs.PerihelionDistance.Value)
                || (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.q, 3) == Math.Round(fs.PerihelionDistance.Value, 3)))
                || (fs.PerihelionDistance.Checked && fs.PerihelionDistance.ValueResolve == ValueResolveEnum.Less && c.q > fs.PerihelionDistance.Value)

                || (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Greather && c.e < fs.Eccentricity.Value)
                || (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.e, 3) == Math.Round(fs.Eccentricity.Value, 3)))
                || (fs.Eccentricity.Checked && fs.Eccentricity.ValueResolve == ValueResolveEnum.Less && c.e > fs.Eccentricity.Value)

                || (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Greather && c.N < fs.LongOfAscendingNode.Value)
                || (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.N, 3) == Math.Round(fs.LongOfAscendingNode.Value, 3)))
                || (fs.LongOfAscendingNode.Checked && fs.LongOfAscendingNode.ValueResolve == ValueResolveEnum.Less && c.N > fs.LongOfAscendingNode.Value)

                || (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Greather && c.w < fs.ArgumentOfPericenter.Value)
                || (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.w, 3) == Math.Round(fs.ArgumentOfPericenter.Value, 3)))
                || (fs.ArgumentOfPericenter.Checked && fs.ArgumentOfPericenter.ValueResolve == ValueResolveEnum.Less && c.w > fs.ArgumentOfPericenter.Value)

                || (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Greather && c.i < fs.Inclination.Value)
                || (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.i, 3) == Math.Round(fs.Inclination.Value, 3)))
                || (fs.Inclination.Checked && fs.Inclination.ValueResolve == ValueResolveEnum.Less && c.i > fs.Inclination.Value)

                || (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Greather && c.P < fs.Period.Value)
                || (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Equal && !(Math.Round(c.P, 3) == Math.Round(fs.Period.Value, 3)))
                || (fs.Period.Checked && fs.Period.ValueResolve == ValueResolveEnum.Less && c.P > fs.Period.Value)) continue;

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
