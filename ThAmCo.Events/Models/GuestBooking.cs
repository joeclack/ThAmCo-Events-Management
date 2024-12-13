using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Events.Models
{
    public class GuestBooking
    {
        public int GuestId { get; set; }
        public int EventId { get; set; }
        public bool IsCancled { get; set; }
        // public bool DidAttendance { get; set; }

        // Navigation

        public Guest Guest { get; set; }
        public Event Event { get; set; }

    }
}
