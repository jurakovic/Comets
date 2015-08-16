using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Globalization;

namespace Comets.BusinessLayer.Business
{
	public class Filter
	{
		#region Enum

		public enum PropertyEnum { Undefined = 0, Name, PerihelionDate, PerihelionDistance, Eccentricity, LongOfAscendingNode, ArgOfPericenter, Inclination, Period };

		public enum ValueCompareEnum { Undefined = 0, Greather, Less, Contains, DoesNotContain };

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

				if (_property == PropertyEnum.Name)
				{
					_value = String.IsNullOrEmpty(_text) ? 0.0 : 1.0; //just for validation
				}
				else if (_property == PropertyEnum.PerihelionDate)
				{
					_value = DateTime.ParseExact(_text, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture).JD();
				}
				else
				{
					_value = _text.Double();
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
