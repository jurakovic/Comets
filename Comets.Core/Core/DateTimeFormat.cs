
namespace Comets.Core
{
	public struct DateTimeFormat
	{
		/// <summary>
		/// dd.MM.yyyy. HH:mm:ss
		/// </summary>
		public static readonly string Full = "dd.MM.yyyy. HH:mm:ss";

		/// <summary>
		/// dd MMM yyyy HH:mm:ss
		/// </summary>
		public static readonly string PerihelionDate = "dd MMM yyyy HH:mm:ss";

		/// <summary>
		/// dd.MM.yyyy HH:mm
		/// </summary>
		public static readonly string Ephemeris = "dd.MM.yyyy HH:mm";

		/// <summary>
		/// dd MMM yyyy HH:mm
		/// </summary>
		public static readonly string GraphLong = "dd MMM yyyy HH:mm";

		/// <summary>
		/// dd MMM yyyy
		/// </summary>
		public static readonly string GraphShort = "dd MMM yyyy";

		/// <summary>
		/// yyyy-MM-dd_HH-mm-ss
		/// </summary>
		public static readonly string Filename = "yyyy-MM-dd_HH-mm-ss";
	}
}
