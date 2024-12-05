using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class CreateModel : PageModel
    {
        private readonly CateringService _cateringService;
        [BindProperty]
        public MenuPostDTO Menu { get; set; }

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
            var existing = await _cateringService.GetMenus();
            if(!existing.Any(x=>x.MenuName == Menu.MenuName))
            {
                await _cateringService.CreateMenu(Menu);
            }

            return Redirect("../Menus");
        }
    }
}
