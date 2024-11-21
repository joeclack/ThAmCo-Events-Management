using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float UnitPrice { get; set; }
        public ICollection<Catering.Models.MenuFoodItem> MenuFoodItems { get; set; } = [];

        public FoodItemDTO CreateDTO(Catering.Models.FoodItem foodItem)
        {
            return new FoodItemDTO
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Description = foodItem.Description,
                UnitPrice = foodItem.UnitPrice,
                MenuFoodItems = foodItem.MenuFoodItems
            };
        }
    }
}
