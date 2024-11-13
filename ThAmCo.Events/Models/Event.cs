using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Models
{
    public class Event
    {
        public int EventId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; } = string.Empty;
        public ICollection<GuestBooking> GuestBookings { get; set; } = [];
        public ICollection<Staffing> Staffings { get; set; } = [];
        public string EventTypeId { get; set; } = string.Empty;
        public string ReservationId { get; set; } = string.Empty;
        public int FoodBookingId { get; set; }
    }
}
