using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Venues.Data
{
    public class Reservation
    {
        [Key, MinLength(13), MaxLength(13)]
        public string Reference { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        [MinLength(5), MaxLength(5)]
        public string VenueCode { get; set; }

        [ForeignKey(nameof(EventDate) + ", " + nameof(VenueCode))]
        public Availability Availability { get; set; }

        public DateTime WhenMade { get; set; }

        [Required]
        public string StaffId { get; set; }
    }
}
