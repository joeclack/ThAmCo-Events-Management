using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class GuestDTO
    {
        public int GuestId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public GuestDTO CreateDTO(Guest guest)
        {
            return new GuestDTO()
            {
                GuestId = guest.GuestId,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Email = guest.Email
            };

        }
    }
}
