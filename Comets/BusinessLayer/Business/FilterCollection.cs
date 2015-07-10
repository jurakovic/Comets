using PropertyName = Comets.BusinessLayer.Business.Filter.PropertyEnum;

namespace Comets.BusinessLayer.Business
{
	public class FilterCollection
	{
		#region Properties

		public Filter full { get; set; }
		public Filter T { get; set; }
		public Filter q { get; set; }
		public Filter e { get; set; }
		public Filter N { get; set; }
		public Filter w { get; set; }
		public Filter i { get; set; }
		public Filter P { get; set; }

		#endregion

		#region Constructor

		public FilterCollection()
		{
			full = new Filter(PropertyName.Name);
			T = new Filter(PropertyName.PerihelionDate);
			q = new Filter(PropertyName.PerihelionDistance);
			e = new Filter(PropertyName.Eccentricity);
			N = new Filter(PropertyName.LongOfAscendingNode);
			w = new Filter(PropertyName.ArgOfPericenter);
			i = new Filter(PropertyName.Inclination);
			P = new Filter(PropertyName.Period);
		}

		#endregion
	}
}
