using Comets.BusinessLayer.Business;
using System;

namespace Comets.BusinessLayer.Managers
{
	public static class CommonManager
	{
		#region Const

		public static string DefaultOrderProperty = CometManager.PropertyEnum.orderkey.ToString();
		public static bool DefaultOrderAscending = true;

		#endregion

		#region Fields

		public static CometCollection _mainCollection;
		public static CometCollection _userCollection;
		public static bool _isDataChanged;

		public static FilterCollection _filters;
		public static string _orderProperty;
		public static bool? _orderAscending;

		public static Settings _settings;

		public static DateTime? _defaultDateStart;
		public static DateTime? _defaultDateEnd;

		#endregion

		#region Properties

		public static CometCollection MainCollection
		{
			get
			{
				if (_mainCollection == null)
					_mainCollection = new CometCollection();

				return _mainCollection;
			}
			set
			{
				_mainCollection = value;
			}
		}

		public static CometCollection UserCollection
		{
			get
			{
				if (_userCollection == null)
					_userCollection = new CometCollection();

				return _userCollection;
			}
			set
			{
				_userCollection = value;
			}
		}

		public static bool IsDataChanged
		{
			get { return _isDataChanged; }
			set { _isDataChanged = value; }
		}

		public static FilterCollection Filters
		{
			get { return _filters; }
			set { _filters = value; }
		}

		public static string OrderProperty
		{
			get
			{
				if (String.IsNullOrEmpty(_orderProperty))
					_orderProperty = DefaultOrderProperty;

				return _orderProperty;
			}
			set
			{
				_orderProperty = value;
			}
		}

		public static bool OrderAscending
		{
			get
			{
				if (_orderAscending == null)
					_orderAscending = DefaultOrderAscending;

				return _orderAscending.Value;
			}
			set
			{
				_orderAscending = value;
			}
		}

		public static Settings Settings
		{
			get
			{
				if (_settings == null)
					_settings = SettingsManager.LoadSettings();

				return _settings;
			}
			set
			{
				_settings = value;
			}
		}

		public static DateTime DefaultDateStart
		{
			get
			{
				if (_defaultDateStart == null)
				{
					DateTime dt = DateTime.Now.AddMonths(-1);
					_defaultDateStart = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, DateTimeKind.Local);
				}

				return _defaultDateStart.Value;
			}
		}

		public static DateTime DefaultDateEnd
		{
			get
			{
				if (_defaultDateEnd == null)
				{
					DateTime dt = DateTime.Now.AddMonths(2);
					_defaultDateEnd = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, DateTimeKind.Local);
				}

				return _defaultDateEnd.Value;
			}
		}

		#endregion
	}
}
