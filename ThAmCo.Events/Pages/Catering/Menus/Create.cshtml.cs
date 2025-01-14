namespace ThAmCo.Events.Pages.Catering.Menus
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="CreateModel" />
	/// </summary>
	
	public class CreateModel : PageModel
	{
		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		private readonly CateringService _cateringService;

		/// <summary>
		/// Gets or sets the Menu
		/// </summary>
		[BindProperty]
		public MenuPostDTO Menu { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public CreateModel(CateringService cateringService)
		{
			_cateringService = cateringService;
		}

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var existing        = await _cateringService.GetMenus();
			if (!existing.Any(x => x.MenuName == Menu.MenuName))
			{
				await _cateringService.CreateMenu(Menu);
			}

			return Redirect("../Menus");
		}
	}
}
