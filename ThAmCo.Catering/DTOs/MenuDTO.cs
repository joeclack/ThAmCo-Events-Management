using Humanizer;
using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public ICollection<FoodItemDTO> MenuFoodItems { get; set; } = [];
        public ICollection<FoodBookingDTO> FoodBookings { get; set; } = [];

        public MenuDTO CreateDTO(Menu menu)
        {
            return new MenuDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuFoodItems = menu.MenuFoodItems.Select(mfi => new FoodItemDTO().CreateDTOFromMFI(mfi)).ToList(),
                FoodBookings = menu.FoodBookings.Select(fb => new FoodBookingDTO().CreateDTO(fb)).ToList()
            };
        }
        public Menu CreateModel(MenuDTO menuDTO)
        {
            return new Menu
            {
                MenuId = menuDTO.MenuId,
                MenuName = menuDTO.MenuName,
                //MenuFoodItems = menuDTO.MenuFoodItems.Select(mfi => new MenuFoodItemDTO().CreateModel(mfi)).ToList(),
                FoodBookings = menuDTO.FoodBookings.Select(fb => new FoodBookingDTO().CreateModel(fb)).ToList()
            };
        }

    }
}
