namespace ThAmCo.Events.Pages.Catering.FoodItems
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="FoodItemsModel" />
	/// </summary>
	public class FoodItemsModel : PageModel
	{
		/// <summary>
		/// Gets or sets the FoodItems
		/// </summary>
		public List<FoodItemGetDTO> FoodItems { get; set; } = [];

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

		/// <summary>
		/// Initializes a new instance of the <see cref="FoodItemsModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public FoodItemsModel(CateringService cateringService)
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
			FoodItems = await _cateringService.GetFoodItems();
		}

		/// <summary>
		/// The OnPostDeleteFoodItem
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteFoodItem(int foodItemId)
		{
			await _cateringService.DeleteFoodItem(foodItemId);

			return Redirect("./FoodItems");
		}
	}
}
