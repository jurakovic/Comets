using System;

namespace Comets.OrbitViewer
{
	public class ATime
	{
		#region Const

		public static string[] MonthAbbrConst = { "Jan.", "Feb.", "Mar.", "Apr.", "May ", "June", "July", "Aug.", "Sep.", "Oct.", "Nov.", "Dec." };

		public static int TIME_INCREMENT = 1;
		public static int TIME_DECREMENT = -1;

		#endregion

		#region Properties

		public int Year { get; private set; }
		public int Month { get; private set; }
		public int Day { get; private set; }
		public int Hour { get; private set; }
		public int Minute { get; private set; }
		public int Second { get; private set; }
		public double Timezone { get; private set; }
		public double JD { get; private set; }

		/// <summary>
		/// Origin 1974/12/31  0h ET
		/// </summary>
		public double T { get; private set; }

		/// <summary>
		/// Origin 2000/01/01 12h ET
		/// </summary>
		public double T2 { get; private set; }

		#endregion

		#region Constructor

		public ATime(int year, int month, int day, int hour, int min, int sec, double timezone)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
			this.Hour = hour;
			this.Minute = min;
			this.Second = sec;
			this.JD = GetJD() - timezone / 24.0;
			this.Timezone = timezone;
			this.T = GetT();
			this.T2 = GetT2();
		}

		public ATime(int year, int month, double day, double timezone)
		{
			this.Year = year;
			this.Month = month;
			this.Day = (int)Math.Floor(day);
			double hour = (day - (double)this.Day) * 24.0;
			this.Hour = (int)Math.Floor(hour);
			double min = (hour - (double)this.Hour) * 60.0;
			this.Minute = (int)Math.Floor(min);
			this.Second = (int)((min - (double)this.Minute) * 60.0);
			this.JD = GetJD() - timezone / 24.0;
			this.Timezone = timezone;
			this.T = GetT();
			this.T2 = GetT2();
		}

		public ATime(double jd, double timezone)
		{
			this.JD = jd;
			this.Timezone = timezone;
			GetDate(jd + timezone / 24.0);
			this.T = GetT();
			this.T2 = GetT2();
		}

		public ATime(ATime atime)
		{
			this.Year = atime.Year;
			this.Month = atime.Month;
			this.Day = atime.Day;
			this.Hour = atime.Hour;
			this.Minute = atime.Minute;
			this.Second = atime.Second;
			this.JD = atime.JD;
			this.Timezone = atime.Timezone;
			this.T = atime.T;
			this.T2 = atime.T2;
		}

		#endregion

		#region MonthAbbr

		/// <summary>
		/// Get Abbreviated Month Name
		/// </summary>
		/// <param name="month"></param>
		/// <returns></returns>
		static public string MonthAbbr(int month)
		{
			return MonthAbbrConst[month - 1];
		}

		#endregion

		#region GetEp

		/// <summary>
		/// Obliquity of Ecliptic (Static Function)
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		static public double GetEp(double jd)
		{
			double t = (jd - Astro.JD2000) / 36525.0;

			if (t > 30.0)
			{		// Out of Calculation Range
				t = 30.0;
			}
			else if (t < -30.0)
			{
				t = -30.0;
			}

			double Ep = 23.43929111
					- 46.8150 / 60.0 / 60.0 * t
					- 0.00059 / 60.0 / 60.0 * t * t
					+ 0.001813 / 60.0 / 60.0 * t * t * t;

			return Ep * Math.PI / 180.0;
		}

		#endregion

		#region + Methods

		#region GetJD

		/// <summary>
		/// YMD/HMS -> JD
		/// </summary>
		/// <returns></returns>
		private double GetJD()
		{
			int year = this.Year;
			int month = this.Month;
			double day = (double)this.Day
						+ (double)this.Hour / 24.0
						+ (double)this.Minute / 24.0 / 60.0
						+ this.Second / 24.0 / 60.0 / 60.0;

			if (month < 3)
			{
				month += 12;
				year -= 1;
			}

			double jd = Math.Floor(365.25 * (double)year) + Math.Floor(30.59 * (double)(month - 2)) + day + 1721086.5;

			if (jd > 2299160.5)
			{
				jd += Math.Floor((double)year / 400.0) - Math.Floor((double)year / 100.0) + 2.0;
			}

			return jd;
		}

		#endregion

		#region GetT

		/// <summary>
		/// Time Parameter Origin of 1974/12/31  0h ET
		/// </summary>
		/// <returns></returns>
		private double GetT()
		{
			// 2442412.5 = 1974.12.31 0h ET
			double t = (this.JD - 2442412.5) / 365.25;
			double T = t + (0.0317 * t + 1.43) * 0.000001;
			return T;
		}

		#endregion

		#region GetT2

		/// <summary>
		/// Time Parameter Origin of 2000/01/01 12h ET
		/// </summary>
		/// <returns></returns>
		private double GetT2()
		{
			double t2 = (this.JD - Astro.JD2000) / 36525.0;
			return t2;
		}

		#endregion

		#region GetDate

		/// <summary>
		/// JD -> YMD/HMS
		/// </summary>
		/// <param name="jd"></param>
		private void GetDate(double jd)
		{
			jd += 0.5;
			double a = Math.Floor(jd);

			if (a >= 2299160.5)
			{
				double t = Math.Floor((a - 1867216.25) / 36524.25);
				a += t - Math.Floor(t / 4.0) + 1.0;
			}

			double b = Math.Floor(a) + 1524;
			double c = Math.Floor((b - 122.1) / 365.25);
			double k = Math.Floor((365.25) * c);
			double e = Math.Floor((b - k) / 30.6001);

			double day = b - k - Math.Floor(30.6001 * e) + (jd - Math.Floor(jd));

			this.Month = (int)Math.Floor(e - ((e >= 13.5) ? 13 : 1) + 0.5);
			this.Year = (int)Math.Floor(c - ((this.Month > 2) ? 4716 : 4715) + 0.5);
			this.Day = (int)Math.Floor(day);

			double hour = (day - this.Day) * 24.0;
			this.Hour = (int)Math.Floor(hour);

			double min = (hour - this.Hour) * 60.0;
			this.Minute = (int)Math.Floor(min);
			this.Second = (int)((min - this.Minute) * 60.0);
		}

		#endregion

		#region ChangeDate

		public void ChangeDate(ATimeSpan span, int direction)
		{
			//
			// First, calculate new Hour,Minute,Second
			//
			double hms1 = this.Hour * 60.0 * 60.0 + this.Minute * 60.0 + this.Second;
			double hms2 = span.Hour * 60.0 * 60.0 + span.Minute * 60.0 + span.Second;

			hms1 += hms2 * direction;

			int day;
			if (0.0 <= hms1 && hms1 < 24.0 * 60.0 * 60.0)
			{
				day = 0;
			}
			else if (hms1 >= 24.0 * 60.0 * 60.0)
			{
				day = (int)Math.Floor(hms1 / 24.0 / 60.0 / 60.0);
				hms1 = UdMath.fmod(hms1, 24.0 * 60.0 * 60.0);
			}
			else
			{
				day = (int)Math.Ceiling(hms1 / 24.0 / 60.0 / 60.0) - 1;
				hms1 = UdMath.fmod(hms1, 24.0 * 60.0 * 60.0) + 24.0 * 60.0 * 60.0;
			}

			int newHour = (int)Math.Floor(hms1 / 60.0 / 60.0);
			int newMin = (int)Math.Floor(hms1 / 60.0) - newHour * 60;
			int newSec = (int)(hms1 - ((double)newHour * 60.0 * 60.0 + (double)newMin * 60.0));

			//
			// Next, calculate new Year, Month, Day
			//
			ATime newDate = new ATime(this.Year, this.Month, this.Day, 12, 0, 0, 0.0);

			double jd = newDate.JD;
			jd += day + span.Day * direction;
			newDate = new ATime(jd, 0.0);

			int newYear = newDate.Year;
			int newMonth = newDate.Month;
			int newDay = newDate.Day;

			newMonth += span.Month * direction;
			if (1 > newMonth)
			{
				newYear -= newMonth / 12 + 1;
				newMonth = 12 + newMonth % 12;
			}
			else if (newMonth > 12)
			{
				newYear += newMonth / 12;
				newMonth = 1 + (newMonth - 1) % 12;
			}
			newYear += span.Year * direction;

			// check bound between julian and gregorian
			if (newYear == 1582 && newMonth == 10)
			{
				if (5 <= newDay && newDay < 10)
				{
					newDay = 4;
				}
				else if (10 <= newDay && newDay < 15)
				{
					newDay = 15;
				}
			}

			newDate = new ATime(newYear, newMonth, newDay, 12, 0, 0, 0.0);
			newYear = newDate.Year;
			newMonth = newDate.Month;
			newDay = newDate.Day;

			this.Year = newYear;
			this.Month = newMonth;
			this.Day = newDay;
			this.Hour = newHour;
			this.Minute = newMin;
			this.Second = newSec;
			this.JD = GetJD() - this.Timezone / 24.0;
			this.T = GetT();
			this.T2 = GetT2();
		}

		#endregion

		#endregion

		#region ToString

		/// <summary>
		/// Print to Standard Output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Year + "/" + this.Month + "/" + this.Day + " " + this.Hour + ":" + this.Minute + ":" + this.Second + " = " + this.JD + " (TZ:" + this.Timezone + ")";
		}

		#endregion

		#region operators

		public static bool operator <(ATime a1, ATime a2)
		{
			return a1.JD < a2.JD;
		}

		public static bool operator >(ATime a1, ATime a2)
		{
			return a1.JD > a2.JD;
		}

		#endregion
	}
}