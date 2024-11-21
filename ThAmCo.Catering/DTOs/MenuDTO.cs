using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public ICollection<MenuFoodItem> MenuFoodItems { get; set; } = [];
        public ICollection<FoodBooking> FoodBookings { get; set; } = [];

        public MenuDTO CreateDTO(Menu menu)
        {
            return new MenuDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuFoodItems = menu.MenuFoodItems,
                FoodBookings = menu.FoodBookings
            };
        }

    }
}
