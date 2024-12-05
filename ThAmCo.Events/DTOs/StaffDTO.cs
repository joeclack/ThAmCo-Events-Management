using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsFirstAider { get; set; }
    }
}
