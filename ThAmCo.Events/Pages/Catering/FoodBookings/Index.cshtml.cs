using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodBookings
{
    public class FoodBookingsModel : PageModel
    {

        public List<FoodBookingDTO> FoodBookings { get; set; } = [];
        public CateringService _cateringService;
		public List<FoodBookingDTO> UpcomingFoodBookings { get; set; } = [];
		public List<FoodBookingDTO> PastFoodBookings { get; set; } = [];
		public FoodBookingsModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            FoodBookings = await _cateringService.GetFoodBookings();
			UpcomingFoodBookings = await _cateringService.GetUpcomingFoodBookings();
			PastFoodBookings = await _cateringService.GetPastFoodBookings();
		}

		public async Task<IActionResult> OnPostDeleteFoodBooking(int foodBookingId)
		{
			await _cateringService.DeleteFoodBooking(foodBookingId);

			return Redirect("./FoodBookings");
		}
	}
}
