using Comets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Comets.Core.Managers
{
	public static class CometManager
	{
		#region Const

		public static string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		public static string[] ImportResults = { "New comets", "Updates", "No changes", "All comets" };
		public static double MinimumMinutesForRecalculate = 5;

		public static char[] CometTypes = new char[]
		{
			'P', // periodic
			'C', // not periodic
			'X', // orbit cannot be computed
			'D', // disappeared
			'I', // interstellar
			'A', // asteroids in cometary orbits
		};

		public static Dictionary<PropertyEnum, double> EqualValueOffset = new Dictionary<PropertyEnum, double>()
		{
			{ PropertyEnum.Tn,               1.00 },
			{ PropertyEnum.q,                0.10 },
			{ PropertyEnum.PerihEarthDist,   0.10 },
			{ PropertyEnum.PerihMag,         0.20 },
			{ PropertyEnum.CurrentSunDist,   0.10 },
			{ PropertyEnum.CurrentEarthDist, 0.10 },
			{ PropertyEnum.CurrentMag,       0.50 },
			{ PropertyEnum.P,                0.10 },
			{ PropertyEnum.Q,                0.10 },
			{ PropertyEnum.a,                0.10 },
			{ PropertyEnum.e,                0.01 },
			{ PropertyEnum.i,                1.00 },
			{ PropertyEnum.N,                1.00 },
			{ PropertyEnum.w,                1.00 }
		};

		/*=============================
		test examples:
		3D
		3D-B/Biela
		3D-A Biela
		C/-146 P1
		C/1014 C1
		C/1860 D1-A (Liais)
		C/1860 D1-A Liais
		D/1993 F2-P2 (Shoemaker-Levy 9)
		D/1993 F2-P1 Shoemaker-Levy 9
		=============================*/
		private static readonly Regex _regFull00 = new Regex(@"^(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ *[a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)[ \/]*\(*(?<name>['`a-zA-Z0-9- ]+)*\)*");

		/*=============================
		test examples:
		P/PANSTARRS (470P)
		P/PANSTARRS 2 (470P-A)
		471P-A
		P/Ikeya-Murakami (332P)
		P/Ikeya-Murakami 2 (332P-A)
		Hale-Bopp (C/-1995 O1)
		Hale-Bopp 2 (C/-1995 O1-A)
		Hale-Bopp (C/1995 O1)
		Hale-Bopp 2 (C/1995 O1-A)
		A/-2019 G3
		A/-2019 G3-A
		A/2019 G3
		A/2019 G3-A
		PANSTARRS (470P)
		PANSTARRS (470P-A1)
		 (471P)
		 (471P-A1)
		=============================*/
		private static readonly Regex _regFull02 = new Regex(@"^([PCXDIA]\/)*((?<name>['`a-zA-Z- ]+ *[0-9]*)* )*\(*(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ [a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)\)*");

		private static readonly Regex _regAlphaNum = new Regex("(?<letters>[a-zA-Z]*)(?<digits>[0-9]*)");

		#endregion

		#region Enum

		public enum PropertyEnum
		{
			full = 0,
			name,
			id,
			Tn, //T,
			q,
			PerihEarthDist,
			PerihMag,
			CurrentSunDist,
			CurrentEarthDist,
			CurrentMag,
			P,
			Q,
			a,
			e,
			i,
			N,
			w,
			sortkey
		};

		public enum ImportResult
		{
			New = 0,
			Update,
			NoChanges,
			All
		}

		public enum SelectionType
		{
			Brightest = 0,
			ClosestToPerihelion,
			ClosestToEarth,
			ClosestToSun,
		}

		#endregion

		#region GetSortkey

		/// <summary>
		/// Calculates Sortkey
		/// </summary>
		public static string GetSortkey(string id, string fragment)
		{
			string key = String.Empty;

			string per = "0"; // periodic:     1P
			string neg = "1"; // negative year C/-YYYY
			string pos = "2"; // positive year C/YYYY
			string ins = "3"; // interstellar: 1I

			if (Char.IsDigit(id[0])) // 1P
				key = id.EndsWith("I") ? ins : per;
			else
				key = id[2] == '-' ? neg : pos;

			string number;
			string codeL = "000";
			string codeD = "000";

			if (Char.IsDigit(id[0])) // 1P
			{
				number = id.Substring(0, id.Length - 1).PadLeft(4, '0');
			}
			else
			{
				string[] yc = id.Split(' ');
				int year = yc[0].Split('/')[1].Int();

				if (year < 0)
					year += 10000;

				number = year.ToString().PadLeft(4, '0');

				string code = yc[1];
				Match m1 = _regAlphaNum.Match(code);
				codeL = m1.Groups["letters"].Value.PadLeft(3, '0');
				codeD = m1.Groups["digits"].Value.PadLeft(3, '0');
			}

			string fragL = "00";
			string fragD = "00";

			if (fragment != String.Empty)
			{
				Match m2 = _regAlphaNum.Match(fragment);
				fragL = m2.Groups["letters"].Value.PadLeft(2, '0');
				fragD = m2.Groups["digits"].Value.PadLeft(2, '0'); ;
			}

			/*===================================
			some examples, expanded for clarity
			1P            >  0 0001 000 000 00 00  ; numbered periodic comets with leading '0'
			3D-A          >  0 0003 000 000 0A 00
			5D-B1         >  0 0005 000 000 0B 01
			240P          >  0 0240 000 000 00 00
			C/-146 P1     >  1 9854 00P 001 00 00  ; negative years with leading '1'
			C/-43 K1      >  1 9957 00K 001 00 00
			C/240 V1      >  2 0240 00V 001 00 00  ; positive years with leading '2'; not the same as 240 above
			C/2000 A1-B2  >  2 2000 00A 001 0B 02
			1I            >  3 0001 000 000 00 00  ; interstellar objects moved to the end with leading '3'
			===================================*/

			key = $"{key}{number}{codeL}{codeD}{fragL}{fragD}";
			return key;
		}

		#endregion

		#region GetPeriod

		/// <summary>
		/// Calculates Period (P)
		/// </summary>
		/// <param name="q">Perihelion distance (q)</param>
		/// <param name="e">Eccentricity (e)</param>
		/// <returns></returns>
		public static double GetPeriod(double q, double e)
		{
			double retval = 0.0;

			if (e < 1.0)
				retval = Math.Pow((q / (1.0 - e)), 1.5);
			else if (e > 1.0)
				retval = Math.Pow((q / (e - 1.0)), 1.5);
			else //if (e == 1.0)
				retval = Math.Pow((q / (1 - 0.999999)), 1.5); //for sorting only

			return retval;
		}

		#endregion

		#region GetSemimajorAxis

		/// <summary>
		/// Calculates Semimajor axis (a)
		/// </summary>
		/// <param name="q">Perihelion distance (q)</param>
		/// <param name="e">Eccentricity (e)</param>
		/// <returns></returns>
		public static double GetSemimajorAxis(double q, double e)
		{
			double retval = 0.0;

			if (e < 1.0)
				retval = q / (1 - e);
			else if (e > 1.0)
				retval = -(q / (1 - e));
			else //if (e == 1.0)
				retval = q / (1 - 0.999999); //for sorting only

			return retval;
		}

		#endregion

		#region GetAphelionDistance

		/// <summary>
		/// Calculates Aphelion distance (Q)
		/// </summary>
		/// <param name="e">Eccentricity (e)</param>
		/// <param name="a">Semimajor axis (a)</param>
		/// <returns></returns>
		public static double GetAphelionDistance(double e, double a)
		{
			double retval = 0.0;

			if (e < 1.0)
				retval = a * (1 + e);
			else if (e > 1.0)
				retval = a * (1 + (2 - e));
			else //if (e == 1.0)
				retval = a * (1 + 0.999999); //for sorting only

			return retval;
		}

		#endregion

		#region GetMeanMotion

		/// <summary>
		/// Calculates Mean motion (n)
		/// </summary>
		/// <param name="e">Eccentricity (e)</param>
		/// <param name="P">Period (P)</param>
		/// <returns></returns>
		public static double GetMeanMotion(double e, double P)
		{
			double retval = 0.0;

			if (e < 1.0)
				retval = 0.9856076686 / P; // Gaussian gravitational constant (degrees)

			return retval;
		}

		#endregion

		#region GetIdNameFromFull

		/// <summary>
		/// Gets Comet ID and Name from Full
		/// </summary>
		/// <param name="full">Full comet name</param>
		/// <returns></returns>
		private static void GetIdNameFromFull(Regex reg, string full, out string id, out string name, out string fragment)
		{
			Match match = reg.Match(full);
			if (!match.Success)
				throw new ArgumentException("Error parsing comet name");

			id = match.Groups["id"].Value;
			name = match.Groups["name"].Value;
			fragment = match.Groups["fragment"].Value;
		}

		public static void GetIdNameFromFull(string full, out string id, out string name, out string fragment)
		{
			GetIdNameFromFull(_regFull00, full, out id, out name, out fragment);
		}

		public static void GetIdNameFromFull2(string full, out string id, out string name, out string fragment)
		{
			GetIdNameFromFull(_regFull02, full, out id, out name, out fragment);
		}

		#endregion

		#region GetFullFromIdName

		/// <summary>
		/// Gets Full from ID and Name
		/// </summary>
		/// <param name="id">Comet ID</param>
		/// <param name="name">Comet Name</param>
		/// <returns></returns>
		public static string GetFullFromIdName(string id, string name, string fragment)
		{
			string full = id;

			if (fragment != String.Empty)
				full += "-" + fragment;

			if (id.Contains('/') && name != String.Empty)
			{
				full += " (" + name + ")";
			}
			else if (name != String.Empty)
			{
				full += "/" + name;
			}

			return full;
		}

		#endregion

		#region GetCometBySelectionType

		public static Comet GetCometBySelectionType(CometCollection comets, SelectionType type, Comet defaultComet = null)
		{
			Comet comet = null;

			if (defaultComet != null && comets.Contains(defaultComet))
			{
				comet = defaultComet;
			}
			else
			{
				switch (type)
				{
					case SelectionType.Brightest:
						comet = comets.OrderBy(x => x.CurrentMag).First();
						break;
					case SelectionType.ClosestToPerihelion:
						decimal jdNow = DateTime.UtcNow.JD();
						comet = comets.OrderBy(x => Math.Abs(x.Tn - jdNow)).First();
						break;
					case SelectionType.ClosestToEarth:
						comet = comets.OrderBy(x => x.CurrentEarthDist).First();
						break;
					case SelectionType.ClosestToSun:
						comet = comets.OrderBy(x => x.CurrentSunDist).First();
						break;
				}
			}

			return comet;
		}

		#endregion

		#region OpenJplInfo

		//public static void OpenJplInfo(string id)
		//{
		//	Process.Start(new ProcessStartInfo("https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=" + id) { UseShellExecute = true });
		//}

		#endregion
	}
}
