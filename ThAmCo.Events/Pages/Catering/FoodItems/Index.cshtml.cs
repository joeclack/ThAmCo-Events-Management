using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodItems
{
    public class FoodItemsModel : PageModel
    {

        public List<FoodItemDTO> FoodItems { get; set; } = [];
        public CateringService _cateringService;

        public FoodItemsModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {

        }

    }
}
