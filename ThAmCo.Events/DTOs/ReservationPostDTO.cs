using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.DTOs
{
	public class ReservationPostDTO
	{
		public DateTime EventDate { get; set; }
		public string VenueCode { get; set; } = string.Empty;
		public string StaffId { get; set; } = string.Empty;
	}
}
