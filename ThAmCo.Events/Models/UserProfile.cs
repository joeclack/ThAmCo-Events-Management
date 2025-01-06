using Microsoft.AspNetCore.Identity;

namespace ThAmCo.Events.Models
{
	public class UserProfile
	{
		public string EmailAddress { get; set; }

		public string Name { get; set; }

		public string ProfileImage { get; set; }
		public List<string> UserRoles {get; set;}
	}
}
