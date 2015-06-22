using PropertyName = Comets.BusinessLayer.Business.Filter.PropertyNameEnum;

namespace Comets.BusinessLayer.Business
{
	public class FilterCollection
	{
		#region Properties

		public Filter Name { get; set; }
		public Filter PerihelionDate { get; set; }
		public Filter PerihelionDistance { get; set; }
		public Filter Eccentricity { get; set; }
		public Filter LongOfAscendingNode { get; set; }
		public Filter ArgumentOfPericenter { get; set; }
		public Filter Inclination { get; set; }
		public Filter Period { get; set; }

		#endregion

		#region Constructor

		public FilterCollection()
		{
			Name = new Filter(PropertyName.Name);
			PerihelionDate = new Filter(PropertyName.PerihelionDate);
			PerihelionDistance = new Filter(PropertyName.PerihelionDistance);
			Eccentricity = new Filter(PropertyName.Eccentricity);
			LongOfAscendingNode = new Filter(PropertyName.LongOfAscendingNode);
			ArgumentOfPericenter = new Filter(PropertyName.ArgOfPericenter);
			Inclination = new Filter(PropertyName.Inclination);
			Period = new Filter(PropertyName.Period);
		}

		#endregion
	}
}
