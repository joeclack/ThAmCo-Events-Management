using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public ICollection<MenuFoodItemDTO> MenuFoodItems { get; set; } = [];
        public ICollection<FoodBookingDTO> FoodBookings { get; set; } = [];

    }
}
