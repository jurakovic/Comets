using System;

namespace Comets.OrbitViewer
{
	/// <summary>
	/// Common Mathematic Functions
	/// </summary>
	public class UdMath
	{
		/// <summary>
		/// modulo for double value
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static double fmod(double x, double y)
		{
			return x - Math.Ceiling(x / y) * y;
		}

		/// <summary>
		/// sin for degree
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double udsin(double x)
		{
			return Math.Sin(x * Math.PI / 180.0);
		}

		/// <summary>
		/// cos for degree
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double udcos(double x)
		{
			return Math.Cos(x * Math.PI / 180.0);
		}

		/// <summary>
		/// tan for degree
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double udtan(double x)
		{
			return Math.Tan(x * Math.PI / 180.0);
		}

		/// <summary>
		/// Rounding degree angle between 0 to 360
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double degmal(double x)
		{
			double y = 360.0 * (x / 360.0 - Math.Floor(x / 360.0));

			if (y < 0.0)
			{
				y += 360.0;
			}
			if (y >= 360.0)
			{
				y -= 360.0;
			}

			return y;
		}

		/// <summary>
		/// Rounding radian angle between 0 to 2 * PI
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double radmal(double x)
		{
			double y = Math.PI * 2.0 * (x / (Math.PI * 2.0) - Math.Floor(x / (Math.PI * 2.0)));

			if (y < 0.0)
			{
				y += Math.PI * 2.0;
			}
			if (y >= Math.PI * 2.0)
			{
				y -= Math.PI * 2.0;
			}

			return y;
		}

		/// <summary>
		/// Degree to Radian
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double deg2rad(double x)
		{
			return x * Math.PI / 180.0;
		}

		/// <summary>
		/// Radian to Degree
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double rad2deg(double x)
		{
			return x * 180.0 / Math.PI;
		}

		/// <summary>
		/// arccosh
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double arccosh(double x)
		{
			return Math.Log(x + Math.Sqrt(x * x - 1.0));
		}

		/// <summary>
		/// sinh
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double sinh(double x)
		{
			return (Math.Exp(x) - Math.Exp(-x)) / 2.0;
		}

		/// <summary>
		/// cosh
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double cosh(double x)
		{
			return (Math.Exp(x) + Math.Exp(-x)) / 2.0;
		}
	}
}