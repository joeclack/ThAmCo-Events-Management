using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float UnitPrice { get; set; }
        public ICollection<MenuFoodItemDTO> MenuFoodItems { get; set; } = [];
    }
}
