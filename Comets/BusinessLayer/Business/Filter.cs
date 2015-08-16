using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Comets.BusinessLayer.Business
{
	public class Filter
	{
		#region Enum

		public enum DataTypeEnum { String, Double };

		public enum ValueCompareEnum { Greather_Contains, Less_DoesNotContain };

		#endregion

		#region Const

		public static Dictionary<string, string> PropertyName = new Dictionary<string, string>()
		{
			{ "full", "Name" },
			{ "NextT", "Perihelion date" },
			{ "P", "Period" },
			{ "q", "Perihelion distance" },
			{ "PerihEarthDist", "Perihelion Earth distance" },
			{ "PerihMag", "Perihelion magnitude" },
			{ "CurrentSunDist", "Current Sun distance" },
			{ "CurrentEarthDist", "Current Earth distance" },
			{ "CurrentMag", "Current magnitude" },
			{ "Q", "Aphelion distance" },
			{ "a", "Semi-major axis" },
			{ "e", "Eccentricity" },
			{ "i", "Inclination" },
			{ "N", "Longitude of Ascending Node" },
			{ "w", "Argument of Pericenter"}
		};

		#endregion

		#region Fields

		private DataTypeEnum _dataType;
		private bool _checked;
		private string _text;
		private double _value;
		private int _index;
		private ValueCompareEnum _valueCompare;

		#endregion

		#region Properties

		public DataTypeEnum DataType
		{
			get { return _dataType; }
			protected set { _dataType = value; }
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

				if (_dataType == DataTypeEnum.Double)
					_value = _text.Double();
				else
					_value = String.IsNullOrEmpty(_text) ? 0.0 : 1.0; //just for validation
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
				_valueCompare = FilterManager.GetValueCompareFromIndex(_dataType, _index);
			}
		}

		public ValueCompareEnum ValueCompare
		{
			get { return _valueCompare; }
		}

		#endregion

		#region Constructor

		public Filter(DataTypeEnum dataType = DataTypeEnum.Double)
		{
			DataType = dataType;
		}

		#endregion
	}
}
