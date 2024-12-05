using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodItems
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public FoodItemGetDTO FoodItem { get; set; }
        public CateringService _cateringService;

        public CreateModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var existing = await _cateringService.GetFoodItems();
            if (!existing.Any(x => x.Name == FoodItem.Name))
            {
                await _cateringService.CreateFoodItem(FoodItem);
            }

            return Redirect("../FoodItems");
        }
    }
}
