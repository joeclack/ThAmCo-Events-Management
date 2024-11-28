using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodBookings
{
    public class CreateModel : PageModel
    {
        public FoodBookingDTO Booking { get; set; }
        public CateringService _cateringService;

        public CreateModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public void OnGet()
        {
        }
    }
}
