namespace ThAmCo.Events.Pages.Catering
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="IndexModel" />
	/// </summary>
	public class IndexModel : PageModel
	{
		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		private readonly CateringService _cateringService;

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexModel"/> class.
		/// </summary>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public IndexModel(IServiceProvider serviceProvider)
		{
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
		}

		/// <summary>
		/// Gets or sets the UpcomingFoodBookings
		/// </summary>
		public List<FoodBookingDTO> UpcomingFoodBookings { get; set; } = [];

		/// <summary>
		/// Gets or sets the PastFoodBookings
		/// </summary>
		public List<FoodBookingDTO> PastFoodBookings { get; set; } = [];

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGetAsync()
		{
			UpcomingFoodBookings = await _cateringService.GetUpcomingFoodBookings();
			PastFoodBookings     = await _cateringService.GetPastFoodBookings();
		}
	}
}
