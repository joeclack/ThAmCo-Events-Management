using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodItems
{
    public class FoodItemsModel : PageModel
    {

        public List<FoodItemGetDTO> FoodItems { get; set; } = [];
        public CateringService _cateringService;

        public FoodItemsModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            FoodItems = await _cateringService.GetFoodItems();
        }

        public async Task<IActionResult> OnPostDeleteFoodItem(int foodItemId)
        {
            await _cateringService.DeleteFoodItem(foodItemId);

            return Redirect("./FoodItems");
        }

    }
}
