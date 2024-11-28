using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class EditModel : PageModel
    {
        public FlatMenuDTO Menu { get; set; }
        public CateringService _cateringService;
        public List<FlatFoodDTO> AvailableFoodItems { get; set; } = [];

        public EditModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            Menu = await _cateringService.GetMenu(id);
        }
    }
}
