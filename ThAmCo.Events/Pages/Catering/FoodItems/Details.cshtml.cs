namespace ThAmCo.Events.Pages.Catering.FoodItems
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="DetailsModel" />
	/// </summary>
	public class DetailsModel : PageModel
	{
		/// <summary>
		/// Gets or sets the FoodItem
		/// </summary>
		public FoodItemGetDTO FoodItem { get; set; }

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

		/// <summary>
		/// Initializes a new instance of the <see cref="DetailsModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public DetailsModel(CateringService cateringService)
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
	}
}
