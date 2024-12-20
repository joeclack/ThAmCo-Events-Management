namespace ThAmCo.Events.Pages.Catering.Menus
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="MenusModel" />
	/// </summary>
	public class MenusModel : PageModel
	{
		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		private readonly CateringService _cateringService;

		/// <summary>
		/// Initializes a new instance of the <see cref="MenusModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public MenusModel(CateringService cateringService)
		{
			_cateringService = cateringService;
		}

		/// <summary>
		/// Gets or sets the Menus
		/// </summary>
		public ICollection<MenuGetDTO> Menus { get; set; } = [];

		/// <summary>
		/// The OnGet
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGet()
		{
			Menus = await _cateringService.GetMenus();
		}

		/// <summary>
		/// The OnPostDeleteMenu
		/// </summary>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteMenu(int menuId)
		{
			await _cateringService.DeleteMenu(menuId);

			return Redirect("./Menus");
		}
	}
}
