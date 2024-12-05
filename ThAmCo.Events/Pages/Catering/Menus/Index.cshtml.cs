using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class MenusModel : PageModel
    {

        private readonly CateringService _cateringService;
        public MenusModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }
        public ICollection<MenuGetDTO> Menus { get; set; } = [];

        public async Task OnGet()
        {
            Menus = await _cateringService.GetMenus();
        }

    }
}
