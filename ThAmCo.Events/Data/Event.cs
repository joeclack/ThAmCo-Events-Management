using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [ForeignKey("EventTypeId")]
        public int EventTypeId { get; set; }
        [ForeignKey("FoodBookingId")]
        public int FoodBookingId { get; set; }
        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public ICollection<GuestBooking> GuestBookings { get; set; }
        public ICollection<Staffing> Staffing { get; set; }
    }
}
