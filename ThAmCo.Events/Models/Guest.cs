using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Events.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Bookings")]
        public ICollection<GuestBooking> GuestBookings { get; set; } = [];

    }
}
