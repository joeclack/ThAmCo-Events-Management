using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Emit;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class DetailsModel : PageModel
    {
        public MenuGetDTO Menu { get; set; }
        public CateringService _cateringService;

        public DetailsModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            Menu = await _cateringService.GetMenu(id);
        }
    }
}
