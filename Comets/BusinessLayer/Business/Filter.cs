using Comets.BusinessLayer.Extensions;
using System;
using System.Collections.Generic;
using PropertyEnum = Comets.BusinessLayer.Managers.CometManager.PropertyEnum;
using DataTypeEnum = Comets.BusinessLayer.Managers.FilterManager.DataTypeEnum;

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
		private int _compareIndex;
		private bool _isValid;

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

		public int CompareIndex
		{
			get { return _compareIndex; }
		}

		public bool IsValid
		{
			get { return _isValid; }
		}

		#endregion

		#region Constructor

		public Filter(PropertyEnum property, DataTypeEnum dataType, bool isChecked, string text, int index)
		{
			_property = property;
			_dataType = dataType;
			_checked = isChecked;
			_text = text;
			_compareIndex = index;

			_isValid = !String.IsNullOrWhiteSpace(text);

			if (_dataType == DataTypeEnum.Double)
				_value = _text.Double();
		}

		#endregion
	}

	#region FilterCollection

	public class FilterCollection : List<Filter>
	{

	}

	#endregion
}
