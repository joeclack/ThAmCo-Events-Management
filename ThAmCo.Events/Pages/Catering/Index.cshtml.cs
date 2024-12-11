using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering
{
    public class IndexModel : PageModel
    {
		private readonly CateringService _cateringService;

		public IndexModel(IServiceProvider serviceProvider)
		{
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
		}

		public List<FoodBookingDTO> UpcomingFoodBookings { get; set; } = [];
		public List<FoodBookingDTO> PastFoodBookings { get; set; } = [];
		public async Task OnGetAsync()
        {
			UpcomingFoodBookings = await _cateringService.GetUpcomingFoodBookings();
			PastFoodBookings = await _cateringService.GetPastFoodBookings();
		}
    }
}
