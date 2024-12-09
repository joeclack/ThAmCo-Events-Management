using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodBookings
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FoodBookingDTO FoodBooking { get; set; }
        public List<MenuGetDTO> AvailableMenus { get; set; }

		public CateringService _cateringService;

        public EditModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            AvailableMenus = await _cateringService.GetMenus();
            FoodBooking = await _cateringService.GetFoodBooking(id);
        }

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
