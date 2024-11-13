using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Events.Models
{
    public class Staffing
    {
        public int StaffId { get; set; }
        public int EventId { get; set; }

        // Navigation props
        public Staff Staff { get; set; } = default!;
        public Event Event { get; set; } = default!;

    }
}
