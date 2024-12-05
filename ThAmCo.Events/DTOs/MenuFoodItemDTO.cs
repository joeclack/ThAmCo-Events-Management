using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class MenuFoodItemDTO
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
        public FoodItemGetDTO FoodItem { get; set; }
    }
}
