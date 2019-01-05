using System;

namespace Comets.Core.Managers
{
	public static class CommonManager
	{
		#region Const

		public static string DefaultSortProperty = CometManager.PropertyEnum.sortkey.ToString();
		public static bool DefaultSortAscending = true;

		#endregion

		#region Fields

		public static CometCollection _mainCollection;
		public static CometCollection _userCollection;
		public static bool _isDataChanged;

		public static FilterCollection _filters;
		public static string _sortProperty;
		public static bool? _sortAscending;

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

		public static string SortProperty
		{
			get
			{
				if (String.IsNullOrEmpty(_sortProperty))
					_sortProperty = DefaultSortProperty;

				return _sortProperty;
			}
			set
			{
				_sortProperty = value;
			}
		}

		public static bool SortAscending
		{
			get
			{
				if (_sortAscending == null)
					_sortAscending = DefaultSortAscending;

				return _sortAscending.Value;
			}
			set
			{
				_sortAscending = value;
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
					DateTime dt = DateTime.UtcNow.AddMonths(-1);
					_defaultDateStart = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, DateTimeKind.Utc);
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
					DateTime dt = DateTime.UtcNow.AddMonths(2);
					_defaultDateEnd = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, DateTimeKind.Utc);
				}

				return _defaultDateEnd.Value;
			}
		}

		#endregion
	}
}
