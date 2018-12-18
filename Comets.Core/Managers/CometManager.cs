using Comets.Core.Extensions;
using System;
using System.Collections.Generic;
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
			'D'  // disappeared
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

		#endregion

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
					v += (fragmLetters[i] - 64) / (double)divider;

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
				// broj dijelim sa 10000000; pretpostavka da se moze pojaviti najvise troznamenkasti broj

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
		public static void GetIdNameFromFull(string full, out string id, out string name)
		{
			id = String.Empty;
			name = String.Empty;

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
		}

		#endregion

		#region GetFullFromIdName

		/// <summary>
		/// Gets Full from ID and Name
		/// </summary>
		/// <param name="id">Comet ID</param>
		/// <param name="name">Comet Name</param>
		/// <returns></returns>
		public static string GetFullFromIdName(string id, string name)
		{
			string full = id;

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

		#region OpenJplInfo

		public static void OpenJplInfo(string id)
		{
			System.Diagnostics.Process.Start("https://ssd.jpl.nasa.gov/sbdb.cgi?sstr=" + id + ";orb=0;old=0;cov=0;log=0;cad=0");
		}

		#endregion
	}
}
