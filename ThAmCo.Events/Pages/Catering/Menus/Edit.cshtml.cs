using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Catering.Menus
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public MenuGetDTO Menu { get; set; }
        public CateringService _cateringService;
        public List<FoodItemGetDTO> AvailableFoodItems { get; set; } = [];

        public EditModel(CateringService cateringService)
        {
            _cateringService = cateringService;
        }

        public async Task OnGet(int id)
        {
            Menu = await _cateringService.GetMenu(id);
            AvailableFoodItems = await _cateringService.GetAvailableFoodItems(Menu);
        }

        public async Task<IActionResult> OnPostDeleteMenuFoodItem(int foodItemId, int menuId)
        {
            await _cateringService.DeleteMenuFoodItem(foodItemId, menuId);
            return Redirect($"../Menus/Edit?id={menuId}");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Menu == null)
            {
                return Page();
            }
            await _cateringService.UpdateMenu(Menu);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAssignFoodItemToMenu(int foodItemId, int menuId)
        {
            await _cateringService.CreateMenuFoodItem(foodItemId, menuId);
            return Redirect($"../Menus/Edit?id={menuId}");
        }
    }
}
