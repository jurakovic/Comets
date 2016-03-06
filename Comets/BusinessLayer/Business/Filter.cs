using Comets.BusinessLayer.Extensions;
using System;
using System.Collections.Generic;
using PropertyEnum = Comets.BusinessLayer.Managers.CometManager.PropertyEnum;
using DataTypeEnum = Comets.BusinessLayer.Managers.FilterManager.DataTypeEnum;
using ValueCompareEnum = Comets.BusinessLayer.Managers.FilterManager.ValueCompareEnum;

namespace Comets.BusinessLayer.Business
{
	public class Filter
	{
		#region Fields

		private PropertyEnum _property;
		private DataTypeEnum _dataType;
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
		}

		public DataTypeEnum DataType
		{
			get { return _dataType; }
		}

		public bool Checked
		{
			get { return _checked; }
		}

		public string Text
		{
			get { return _text; }
		}

		public double Value
		{
			get { return _value; }
		}

		public int Index
		{
			get { return _index; }
		}

		public ValueCompareEnum ValueCompare
		{
			get { return _valueCompare; }
		}

		#endregion

		#region Constructor

		public Filter(PropertyEnum property, DataTypeEnum dataType, bool isChecked, string text, int index)
		{
			_property = property;
			_dataType = dataType;
			_checked = isChecked;
			_text = text;

			if (_dataType == DataTypeEnum.Double)
				_value = _text.Double();
			else// if (_dataType == DataTypeEnum.String)
				_value = String.IsNullOrEmpty(_text) ? 0.0 : 1.0; //only for validation

			_index = index;

			if (_index == 0)
				_valueCompare = ValueCompareEnum.Greather_Contains;
			else
				_valueCompare = ValueCompareEnum.Less_DoesNotContain;
		}

		#endregion
	}

	#region FilterCollection

	public class FilterCollection : List<Filter>
	{

	}

	#endregion
}
