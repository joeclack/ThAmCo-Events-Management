namespace ThAmCo.Events.Pages.Catering.FoodItems
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
		/// Gets or sets the FoodItem
		/// </summary>
		[BindProperty]
		public FoodItemGetDTO FoodItem { get; set; }

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

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
		/// <returns>The <see cref ="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var existing        = await _cateringService.GetFoodItems();
			if (!existing.Any(x => x.Name == FoodItem.Name))
			{
				await _cateringService.CreateFoodItem(FoodItem);
			}

			return Redirect("../FoodItems");
		}
	}
}
