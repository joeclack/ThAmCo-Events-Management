using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class CreateModel : PageModel
    {
        private readonly CateringService _cateringService;

        public CreateModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet()
        {
            
        }
    }
}
