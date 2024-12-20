namespace ThAmCo.Events.Pages.Catering.FoodBookings
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="IndexModel" />
	/// </summary>
	public class IndexModel : PageModel
	{
		/// <summary>
		/// Gets or sets the FoodBookings
		/// </summary>
		public List<FoodBookingDTO> FoodBookings { get; set; } = [];

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

		/// <summary>
		/// Gets or sets the UpcomingFoodBookings
		/// </summary>
		public List<FoodBookingDTO> UpcomingFoodBookings { get; set; } = [];

		/// <summary>
		/// Gets or sets the PastFoodBookings
		/// </summary>
		public List<FoodBookingDTO> PastFoodBookings { get; set; } = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexModel"/> class.
		/// </summary>
		/// <param name="cateringService">The cateringService<see cref="CateringService"/></param>
		public IndexModel(CateringService cateringService)
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
			FoodBookings         = await _cateringService.GetFoodBookings();
			UpcomingFoodBookings = await _cateringService.GetUpcomingFoodBookings();
			PastFoodBookings     = await _cateringService.GetPastFoodBookings();
		}

		/// <summary>
		/// The OnPostDeleteFoodBooking
		/// </summary>
		/// <param name="foodBookingId">The foodBookingId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteFoodBooking(int foodBookingId)
		{
			await _cateringService.DeleteFoodBooking(foodBookingId);

			return Redirect("./FoodBookings");
		}
	}
}
