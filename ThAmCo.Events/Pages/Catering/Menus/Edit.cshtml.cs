namespace ThAmCo.Events.Pages.Catering.Menus
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="EditModel" />
	/// </summary>
	public class EditModel : PageModel
	{
		/// <summary>
		/// Gets or sets the Menu
		/// </summary>
		[BindProperty]
		public MenuGetDTO Menu { get; set; }

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

		/// <summary>
		/// Gets or sets the AvailableFoodItems
		/// </summary>
		public List<FoodItemGetDTO> AvailableFoodItems { get; set; } = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="EditModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public EditModel(CateringService cateringService)
		{
			_cateringService = cateringService;
		}

		/// <summary>
		/// The OnGet
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGet(int id)
		{
			Menu               = await _cateringService.GetMenu(id);
			AvailableFoodItems = await _cateringService.GetAvailableFoodItems(Menu);
		}

		/// <summary>
		/// The OnPostDeleteMenuFoodItem
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteMenuFoodItem(int foodItemId, int menuId)
		{
			await _cateringService.DeleteMenuFoodItem(foodItemId, menuId);
			return Redirect($"../Menus/Edit?id={menuId}");
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
			if (Menu == null)
			{
				return Page();
			}
			await _cateringService.UpdateMenu(Menu);
			return RedirectToPage("./Index");
		}

		/// <summary>
		/// The OnPostAssignFoodItemToMenu
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAssignFoodItemToMenu(int foodItemId, int menuId)
		{
			await _cateringService.CreateMenuFoodItem(foodItemId, menuId);
			return Redirect($"../Menus/Edit?id={menuId}");
		}
	}
}
