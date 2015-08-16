using DataType = Comets.BusinessLayer.Business.Filter.DataTypeEnum;

namespace Comets.BusinessLayer.Business
{
	public class FilterCollection
	{
		#region Properties

		//ephemeris
		public Filter full { get; set; }
		public Filter NextT { get; set; }
		public Filter P { get; set; }
		public Filter q { get; set; }
		public Filter PerihEarthDist { get; set; }
		public Filter PerihMag { get; set; }
		public Filter CurrentSunDist { get; set; }
		public Filter CurrentEarthDist { get; set; }
		public Filter CurrentMag { get; set; }

		//elements
		public Filter Q { get; set; }
		public Filter a { get; set; }
		public Filter e { get; set; }
		public Filter i { get; set; }
		public Filter N { get; set; }
		public Filter w { get; set; }

		#endregion

		#region Constructor

		public FilterCollection()
		{
			full = new Filter(DataType.String);
			NextT = new Filter();
			P = new Filter();
			q = new Filter();
			PerihEarthDist = new Filter();
			PerihMag = new Filter();
			CurrentSunDist = new Filter();
			CurrentEarthDist = new Filter();
			CurrentMag = new Filter();

			Q = new Filter();
			a = new Filter();
			e = new Filter();
			i = new Filter();
			N = new Filter();
			w = new Filter();
		}

		#endregion
	}
}
