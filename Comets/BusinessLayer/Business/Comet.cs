using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comets.BusinessLayer.Business
{
	public class Comet
	{
		#region Fields

		private string _full;
		private string _name;
		private string _id;
		private decimal _T;
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
		private double _P;
		private double _a;
		private double _Q;
		private double _n;
		private double _sortkey;
		private string _idKey;

		private Ephemeris _epPerihelion;
		private Ephemeris _epCurrent;
		private DateTime _lastEphemerisUpdate;
		private decimal? _Tn;

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
		public decimal T
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
			get { return _P; }
			set { _P = value; }
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
			get { return _a; }
			set { _a = value; }
		}

		/// <summary>
		/// Aphelion distance
		/// </summary>
		public double Q
		{
			get { return _Q; }
			set { _Q = value; }
		}

		/// <summary>
		/// Mean motion
		/// </summary>
		public double n
		{
			get { return _n; }
			set { _n = value; }
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

		private Ephemeris PerihelionEphemeris
		{
			get
			{
				if (_epPerihelion == null)
					_epPerihelion = EphemerisManager.GetEphemeris(this, Tn, CommonManager.Settings.Location);

				return _epPerihelion;
			}
		}

		private Ephemeris CurrentEphemeris
		{
			get
			{
				if (_epCurrent == null || (DateTime.Now - _lastEphemerisUpdate).TotalMinutes > CometManager.MinimumMinutesForRecalculate)
				{
					_lastEphemerisUpdate = DateTime.Now;
					_epCurrent = EphemerisManager.GetEphemeris(this, DateTime.Now.JD(), CommonManager.Settings.Location);
				}

				return _epCurrent;
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

		/// <summary>
		/// Nearest perihelion date
		/// </summary>
		public decimal Tn
		{
			get
			{
				if (_Tn == null)
				{
					if (IsPeriodic)
					{
						List<decimal> t_all = new List<decimal>();

						decimal t = T;
						decimal periodDays = Convert.ToDecimal(P) * 365.25m;

						decimal min = EphemerisManager.JD(Comets.Application.FormDateTime.Minimum);
						decimal max = EphemerisManager.JD(Comets.Application.FormDateTime.Maximum);

						//going to earliest T
						while (t - periodDays > min)
							t -= periodDays;

						//from earliest to last T
						while (t < max)
						{
							t_all.Add(t);
							t += periodDays;
						}

						_Tn = t_all.OrderBy(x => Math.Abs(x - DateTime.Now.JD())).First();
					}
					else
					{
						_Tn = T;
					}
				}

				return _Tn.GetValueOrDefault();
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
	}

	#region CometCollection

	public class CometCollection : List<Comet>
	{
		public CometCollection()
		{

		}

		public CometCollection(IEnumerable<Comet> comets)
			: base(comets)
		{

		}
	}

	#endregion
}
