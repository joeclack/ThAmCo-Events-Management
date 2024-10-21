using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Venues.Data
{
    public class Venue
    {
        [Key, MaxLength(5)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, Int32.MaxValue)]
        public int Capacity { get; set; }

        public List<Suitability> SuitableEventTypes { get; set; }

        public List<Availability> AvailableDates { get; set; }
    }
}
