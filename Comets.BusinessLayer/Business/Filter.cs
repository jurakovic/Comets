using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

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
				else if (_checked && _propertyName == PropertyNameEnum.PerihelionDate)
				{

					string[] dt = _text.Split('.');
					try
					{
						_value = EphemerisManager.jd0(Convert.ToInt32(dt[2]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]), 0);
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
