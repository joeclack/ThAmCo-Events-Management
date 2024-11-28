using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float UnitPrice { get; set; }
        public ICollection<MenuFoodItem> MenuFoodItems { get; set; } = [];

        public FoodItemDTO CreateDTO(FoodItem foodItem)
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

        public FoodItem CreateModel(FoodItemDTO foodItemDTO)
        {
            return new FoodItem
            {
                FoodItemId = foodItemDTO.FoodItemId,
                Name = foodItemDTO.Name,
                Description = foodItemDTO.Description,
                UnitPrice = foodItemDTO.UnitPrice,
                MenuFoodItems = foodItemDTO.MenuFoodItems
            };
        }
    }
}
