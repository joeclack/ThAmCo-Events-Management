using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class FoodBookingDTO
    {
        public int FoodBookingId { get; set; }
        public int? ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }
        public DateTime FoodBookingDate { get; set; }


    }
}
