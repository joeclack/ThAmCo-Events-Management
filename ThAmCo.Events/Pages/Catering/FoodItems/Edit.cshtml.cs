namespace ThAmCo.Events.Pages.Catering.FoodItems
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
		/// Gets or sets the FoodItem
		/// </summary>
		[BindProperty]
		public FoodItemGetDTO FoodItem { get; set; }

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

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
			FoodItem = await _cateringService.GetFoodItem(id);
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
			if (FoodItem == null)
			{
				return Page();
			}
			await _cateringService.UpdateFoodItem(FoodItem);
			return RedirectToPage("./Index");
		}
	}
}
