using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using ThAmCo.Events.Data;

namespace ThAmCo.Catering.Models
{
    public class FoodBooking
    {
        public int FoodBookingId { get; set; }
        public int? ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }
        public DateTime FoodBookingDate { get; set; }

        // Navigation properties
        public Menu Menu { get; set; }
    }
}
