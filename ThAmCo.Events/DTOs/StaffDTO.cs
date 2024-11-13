using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public StaffDTO CreateDTO(Staff staff)
        {
            return new StaffDTO()
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Role = staff.Role
            };
        }
    }
}
