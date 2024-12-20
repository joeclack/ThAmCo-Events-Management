namespace ThAmCo.Events.Pages.Catering.FoodBookings
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
		/// Gets or sets the FoodBooking
		/// </summary>
		[BindProperty]
		public FoodBookingDTO FoodBooking { get; set; }

		/// <summary>
		/// Gets or sets the AvailableMenus
		/// </summary>
		public List<MenuGetDTO> AvailableMenus { get; set; }

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
			AvailableMenus = await _cateringService.GetMenus();
			FoodBooking    = await _cateringService.GetFoodBooking(id);
		}

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				AvailableMenus = await _cateringService.GetMenus();
				return Page();
			}

			await _cateringService.UpdateFoodBooking(FoodBooking);
			return RedirectToPage("Index");
		}
	}
}
