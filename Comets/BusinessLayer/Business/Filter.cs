using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;

namespace Comets.BusinessLayer.Business
{
	public class Filter
	{
		#region Enum

		public enum PropertyEnum { Undefined = 0, Name, PerihelionDate, PerihelionDistance, Eccentricity, LongOfAscendingNode, ArgOfPericenter, Inclination, Period };

		public enum ValueCompareEnum { Undefined = 0, Greather, Equal, Less, Contains, DoesNotContain };

		#endregion

		#region Const

		public static string[] PropertyNameString = { "Undefined", "Name", "Perihelion date", "Perihelion distance", "Eccentricity", "Longitude of Ascending Node", "Argument of Pericenter", "Inclination", "Period" };

		#endregion

		#region Fields

		private PropertyEnum _property;
		private bool _checked;
		private string _text;
		private double _value;
		private int _index;
		private ValueCompareEnum _valueCompare;

		#endregion

		#region Properties

		public PropertyEnum Property
		{
			get { return _property; }
			protected set { _property = value; }
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

				if (_property != PropertyEnum.Name && _property != PropertyEnum.PerihelionDate)
				{
					_value = _text.Double();
				}
				else if (_property == PropertyEnum.PerihelionDate)
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
				_valueCompare = FilterManager.GetValueCompareFromIndex(_property, _index);
			}
		}

		public ValueCompareEnum ValueCompare
		{
			get { return _valueCompare; }
		}

		#endregion

		#region Constructor

		public Filter(PropertyEnum property)
		{
			Property = property;
		}

		#endregion
	}
}
