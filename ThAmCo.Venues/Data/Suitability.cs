using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Venues.Data
{
    public class Suitability
    {
        public string EventTypeId { get; set; }

        public EventType EventType { get; set; }

        [MinLength(5), MaxLength(5)]
        public string VenueCode { get; set; }

        public Venue Venue { get; set; }
    }
}
