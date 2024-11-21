using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class MenuFoodItemDTO
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
        public MenuDTO Menu { get; set; }
        public FoodItemDTO FoodItem { get; set; }
    }
}
