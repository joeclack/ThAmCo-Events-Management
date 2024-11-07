using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {
        [Key]
        public int GuestBookingId { get; set; }

        [ForeignKey("GuestId")]
        public int GuestId { get; set; }

        [ForeignKey("EventId")]
        public int EventId { get; set; }

    }
}
