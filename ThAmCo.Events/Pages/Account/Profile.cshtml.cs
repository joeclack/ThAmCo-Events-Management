using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Pages.Account
{
	public class ProfileModel : PageModel
	{
		internal UserProfile UserProfile { get; set; } = new();
		public void OnGet()
		{
			UserProfile = new UserProfile()
			{
				Name = User.Identity.Name,
				EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
				ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
			};
		}
	}
}
