using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Role")]
        public string Role { get; set; } = string.Empty;
        [DisplayName("Is First Aider")]
        public bool IsFirstAider { get; set; }
        [DisplayName("Events")]
        public ICollection<Staffing>? Staffings { get; set; } = [];
        public bool IsAnonymised { get; set; }

    }
}
