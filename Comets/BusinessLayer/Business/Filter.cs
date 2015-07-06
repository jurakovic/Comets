using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;

namespace Comets.BusinessLayer.Business
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
					_value = _text.Double();
				}
				else if (_propertyName == PropertyNameEnum.PerihelionDate)
				{
					string[] dt = _text.Split('.');
					_value = Utils.JDToDateTime(EphemerisManager.jd(dt[2].Int(), dt[1].Int(), dt[0].Int(), dt[3].Int(), dt[4].Int(), dt[5].Int())).ToUniversalTime().JD();
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
				_valueResolve = FilterManager.GetValueResolveFromIndex(_propertyName, _index);
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
	}
}
