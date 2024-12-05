using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodItems
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FoodItemGetDTO FoodItem { get; set; }
        public CateringService _cateringService;

        public EditModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            FoodItem = await _cateringService.GetFoodItem(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodItem == null)
            {
                return Page();
            }
            await _cateringService.UpdateFoodItem(FoodItem);
            return RedirectToPage("./Index");
        }
    }
}
