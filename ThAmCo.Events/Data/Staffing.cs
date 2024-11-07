using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        [ForeignKey("StaffId")]
        public int StaffId { get; set; }

        [ForeignKey("EventId")]
        public int EventId { get; set; }

    }
}
