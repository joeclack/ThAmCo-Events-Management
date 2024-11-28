using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodBookings
{
    public class EditModel : PageModel
    {
        public FoodBookingDTO Booking { get; set; }
        public CateringService _cateringService;

        public EditModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {

        }
    }
}
