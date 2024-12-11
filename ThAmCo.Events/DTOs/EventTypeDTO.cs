using System.ComponentModel.DataAnnotations;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.DTOs
{
	public class EventTypeDTO
	{
		public string Id { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
	}
}
