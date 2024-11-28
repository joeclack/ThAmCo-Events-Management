using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Emit;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.FoodItems
{
    public class DetailsModel : PageModel
    {
        public FoodItemDTO FoodItem { get; set; }
        public CateringService _cateringService;

        public DetailsModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            
        }
    }
}
