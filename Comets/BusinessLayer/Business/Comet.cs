using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Comets.BusinessLayer.Business
{
	public class Comet
	{
		#region Const

		public static string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		private static double MinimumMinutesForRefresh = 5;

		#endregion

		#region Fields

		private string _full;
		private string _name;
		private string _id;
		private double _T;
		private int _Ty;
		private int _Tm;
		private int _Td;
		private int _Th;
		private double _q;
		private double _e;
		private double _i;
		private double _N;
		private double _w;
		private double _g;
		private double _k;
		private double _sortkey;
		private string _idKey;

		private EphemerisResult _erPerihelion;
		private EphemerisResult _erCurrent;
		private DateTime _lastEphemerisUpdate;
		private double? _nextT;

		#endregion

		#region Properties

		/// <summary>
		/// Full (e.g. 1P/Halley)
		/// </summary>
		public string full
		{
			get { return _full; }
			set { _full = value; }
		}

		/// <summary>
		/// Name (e.g. Halley)
		/// </summary>
		public string name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Id (e.g. 1P)
		/// </summary>
		public string id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// Epoch (Julian day)
		/// </summary>
		public double T
		{
			get { return _T; }
			set { _T = value; }
		}

		/// <summary>
		/// Epoch year
		/// </summary>
		public int Ty
		{
			get { return _Ty; }
			set { _Ty = value; }
		}

		/// <summary>
		/// Epoch month
		/// </summary>
		public int Tm
		{
			get { return _Tm; }
			set { _Tm = value; }
		}

		/// <summary>
		/// Epoch day
		/// </summary>
		public int Td
		{
			get { return _Td; }
			set { _Td = value; }
		}

		/// <summary>
		/// Epoch hour (#### format)
		/// </summary>
		public int Th
		{
			get { return _Th; }
			set { _Th = value; }
		}

		/// <summary>
		/// Is comet periodic
		/// </summary>
		public bool IsPeriodic
		{
			get
			{
				return ((Char.IsDigit(id[0]) && id.EndsWith("P")) || id.StartsWith("P/"));
			}
		}

		/// <summary>
		/// Period
		/// </summary>
		public double P
		{
			get
			{
				double retval = 0.0;

				if (e < 1.0)
					retval = Math.Pow((q / (1.0 - e)), 1.5);
				else if (e > 1.0)
					retval = Math.Pow((q / (e - 1.0)), 1.5);
				else
					retval = Math.Pow((q / (1 - 0.999999)), 1.5); //only for sorting and celestia format

				return retval;
			}
		}

		/// <summary>
		/// Perihelion distance
		/// </summary>
		public double q
		{
			get { return _q; }
			set { _q = value; }
		}

		/// <summary>
		/// Eccentricity
		/// </summary>
		public double e
		{
			get { return _e; }
			set { _e = value; }
		}

		/// <summary>
		/// Inclination
		/// </summary>
		public double i
		{
			get { return _i; }
			set { _i = value; }
		}

		/// <summary>
		/// Longitude of the Ascending Node
		/// </summary>
		public double N
		{
			get { return _N; }
			set { _N = value; }
		}

		/// <summary>
		/// Argument of Pericenter
		/// </summary>
		public double w
		{
			get { return _w; }
			set { _w = value; }
		}

		/// <summary>
		/// Semimajor Axis
		/// </summary>
		public double a
		{
			get
			{
				double retval = 0.0;

				if (e < 1.0)
					retval = q / (1 - e);
				else if (e > 1.0)
					retval = -(q / (1 - e));
				else
					retval = Double.PositiveInfinity; //retval = q / (1 - 0.999999);

				return retval;
			}
		}

		/// <summary>
		/// Aphelion distance
		/// </summary>
		public double Q
		{
			get
			{
				double retval = 0.0;

				if (e < 1.0)
					retval = a * (1 + e);
				else if (e > 1.0)
					retval = a * (1 + (2 - e));
				else
					retval = Double.PositiveInfinity; //retval = a * (1 + 0.999999);

				return retval;
			}
		}

		/// <summary>
		/// Mean motion
		/// </summary>
		public double n
		{
			get
			{
				double retval = 0.0;

				if (e < 1.0)
					retval = 0.9856076686 / P; // Gaussian gravitational constant (degrees)

				return retval;
			}
		}

		/// <summary>
		/// Absolute magnitude
		/// </summary>
		public double g
		{
			get { return _g; }
			set { _g = value; }
		}

		/// <summary>
		/// Slope parameter
		/// </summary>
		public double k
		{
			get { return _k; }
			set { _k = value; }
		}

		/// <summary>
		/// Sortkey
		/// </summary>
		public double sortkey
		{
			get { return _sortkey; }
			set { _sortkey = value; }
		}

		/// <summary>
		/// IdKey
		/// </summary>
		public string idKey
		{
			get { return _idKey; }
			set { _idKey = value; }
		}

		public EphemerisResult PerihelionEphemeris
		{
			get
			{
				if (_erPerihelion == null)
					_erPerihelion = EphemerisManager.GetEphemeris(this, NextT, Comets.Application.FormMain.Settings.Location);

				return _erPerihelion;
			}
		}

		public EphemerisResult CurrentEphemeris
		{
			get
			{
				if (_erCurrent == null || (DateTime.Now - _lastEphemerisUpdate).TotalMinutes > MinimumMinutesForRefresh)
				{
					_lastEphemerisUpdate = DateTime.Now;
					_erCurrent = EphemerisManager.GetEphemeris(this, DateTime.Now.JD(), Comets.Application.FormMain.Settings.Location);
				}

				return _erCurrent;
			}
		}

		public double PerihEarthDist
		{
			get { return PerihelionEphemeris.EarthDist; }
		}

		public double PerihMag
		{
			get { return PerihelionEphemeris.Magnitude; }
		}

		public double CurrentSunDist
		{
			get { return CurrentEphemeris.SunDist; }
		}

		public double CurrentEarthDist
		{
			get { return CurrentEphemeris.EarthDist; }
		}

		public double CurrentMag
		{
			get { return CurrentEphemeris.Magnitude; }
		}

		public double NextT
		{
			get
			{
				if (_nextT == null)
				{
					double nextT = T;

					if (IsPeriodic)
					{
						double period = P * 365.25;
						double now = DateTime.Now.JD();

						while (nextT < now)
							nextT += period;

						//if actual T is closer to now than nextT, then choose T instead
						if (nextT - now > now - T)
							nextT = T;
					}

					_nextT = nextT;
				}

				return _nextT.GetValueOrDefault();
			}
		}

		#endregion

		#region Constructor

		public Comet()
		{

		}

		#endregion

		#region ToString

		public override string ToString()
		{
			return full;
		}

		#endregion

		#region Methods

		#region GetSortkey

		/// <summary>
		/// Calculates Sortkey
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns></returns>
		public static double GetSortkey(string id)
		{
			double sort = 0.0;
			double v = 0.0;
			double offset = 2000.0;
			string fragm = String.Empty;

			//http://stackoverflow.com/questions/3720012/regular-expression-to-split-string-and-number
			Regex numAlpha = new Regex("(?<letters>[a-zA-Z]*)(?<digits>[0-9]*)");

			if (id.Contains('-') && id[2] != '-') // 128P-B, C/-146 P1
			{
				string[] fi = id.Split('-');
				id = fi[0];
				fragm = fi[1];

				var match = numAlpha.Match(fragm);

				string fragmLetters = match.Groups["letters"].Value;
				string fragmDigits = match.Groups["digits"].Value;

				for (int i = 0, divider = 1000000000; i < fragmLetters.Length; i++, divider *= 100)
				{
					v += (fragmLetters[i] - 64) / (double)divider;
				}

				if (fragmDigits != String.Empty)
					v += fragmDigits.Double() / 10000000000000.0;
			}

			if (Char.IsDigit(id[0]))
			{
				sort = id.Substring(0, id.Length - 1).Double();
			}
			else
			{
				string[] yc = id.Split(' ');
				sort = yc[0].Split('/')[1].Double() + offset; //da npr C/240 V1 ne bude isto kao i 240P/NEAT i slicno...

				string code = yc[1];

				var match = numAlpha.Match(code);

				string codeLetters = match.Groups["letters"].Value;
				string codeDigits = match.Groups["digits"].Value;

				// pretpostavka da mogu doci najvise 2 slova u id-u
				// 1. slovo dijelim sa 100
				// 2. slovo dijelim sa 10000
				// pretpostavka da se moze pojaviti najvise troznamenkasti broj
				// njega dijelim sa 10000000

				for (int i = 0, divider = 100; i < codeLetters.Length; i++, divider *= 100)
				{
					v += (codeLetters[i] - 64) / (double)divider;
				}

				if (codeDigits != String.Empty)
					v += codeDigits.Double() / 10000000.0;
			}

			sort += v;

			return sort;
		}

		#endregion

		#region GetIdKey

		/// <summary>
		/// Returns IDKey
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns></returns>
		public static string GetIdKey(string id)
		{
			string key = String.Empty;

			if (Char.IsDigit(id[0]))
			{
				key = id;

				for (int i = key.Length; i < 5; i++)
					key = '0' + key;
			}
			else
			{
				key = id.Remove(0, 2).Replace("-", String.Empty).Replace(" ", String.Empty);
			}

			return key;
		}

		#endregion

		#region GetIdNameFromFull

		/// <summary>
		/// Sets Comet ID and Name from Full
		/// </summary>
		/// <param name="full">Full comet name</param>
		/// <returns></returns>
		public static string[] GetIdNameFromFull(string full)
		{
			string id = String.Empty, name = String.Empty;

			if (Char.IsDigit(full[0]))
			{
				if (full.Contains('/'))
				{
					string[] idname = full.Split('/');
					id = idname[0];
					name = idname[1];
				}
				else
				{
					id = full;
				}
			}
			else
			{
				if (full.Contains('('))
				{
					string[] idname = full.Split('(');
					id = idname[0].Trim();
					name = idname[1].TrimEnd(')');
				}
				else
				{
					id = full;
				}
			}

			return new string[] { id, name };
		}

		#endregion

		#region GetFullFromIdName

		/// <summary>
		/// Sets Full from ID and Name
		/// </summary>
		/// <param name="id">Comet ID</param>
		/// <param name="name">Comet Name</param>
		/// <returns></returns>
		public static string GetFullFromIdName(string id, string name)
		{
			string full = id;

			if (id.Contains('/'))
			{
				if (name != String.Empty)
				{
					full += " (" + name + ")";
				}
			}
			else
			{
				if (name != String.Empty)
				{
					full += "/" + name;
				}
			}

			return full;
		}

		#endregion

		#region GregorianToJulian

		/// <summary>
		/// Converts Gregorian date to Julian day
		/// </summary>
		/// <param name="y">Year</param>
		/// <param name="m">Month</param>
		/// <param name="d">Day</param>
		/// <param name="h">Hour (0000)</param>
		/// <returns></returns>
		public static double GregorianToJulian(int y, int m, int d, int h)
		{
			return 367 * y - (7 * (y + (m + 9) / 12)) / 4 - ((3 * (y + (m - 9) / 7)) / 100 + 1) / 4 + (275 * m) / 9 + d + 1721029 + ((double)h / 10000);
		}

		#endregion

		#endregion
	}
}
