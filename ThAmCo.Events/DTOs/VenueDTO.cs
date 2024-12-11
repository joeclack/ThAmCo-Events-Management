using System.ComponentModel.DataAnnotations;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.DTOs
{
	public class VenueDTO
	{
		public string Code { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public List<Suitability> SuitableEventTypes { get; set; } = [];
		public List<Availability> AvailableDates { get; set; } = [];
	}
}
