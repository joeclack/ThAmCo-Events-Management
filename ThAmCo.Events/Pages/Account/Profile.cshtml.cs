using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Security.Claims;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Pages.Account
{
	public class ProfileModel : PageModel
	{
		private readonly IConfiguration _configuration;

		public ProfileModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		internal UserProfile UserProfile { get; set; } = new();
		public void OnGet()
		{
			string rolesClaimType = _configuration["Autho0:Domain"] + "/roles";
			var claimsIdentity = User.Identity as ClaimsIdentity;

			if (claimsIdentity != null)
			{
				UserProfile = new UserProfile()
				{
					EmailAddress = claimsIdentity.Claims.FirstOrDefault(c=>c.Type == "email")?.Value,
					Name = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
					ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
					UserRoles = User.Claims
					.Where(c => c.Type == rolesClaimType)
					.Select(c => c.Value)
					.ToList()
				};
			}
		}
	}
}
