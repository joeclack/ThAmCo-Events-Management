using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Location { get; set; }

        public ICollection<GuestBooking> GuestBookings { get; set; }

        public ICollection<Staffing> Staffings { get; set; }

        public string EventTypeId { get; set; }

        public string ReservationId { get; set; }

        public int FoodBookingId { get; set; }
    }
}
