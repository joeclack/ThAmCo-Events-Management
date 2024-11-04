using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThAmCo.Events.Data;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        [Key]
        public int FoodBookingId { get; set; }
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }
        [ForeignKey("EventId")]
        public Event EventId { get; set; }
    }
}
