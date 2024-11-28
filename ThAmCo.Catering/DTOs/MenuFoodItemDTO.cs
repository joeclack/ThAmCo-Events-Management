using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class MenuFoodItemDTO
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }

        public MenuFoodItemDTO CreateDTO(MenuFoodItem menuFoodItem)
        {
            return new MenuFoodItemDTO
            {
                MenuId = menuFoodItem.MenuId,
                FoodItemId = menuFoodItem.FoodItemId
            };
        }

        public MenuFoodItem CreateModel(MenuFoodItemDTO menuFoodItemDTO)
        {
            return new MenuFoodItem
            {
                MenuId = menuFoodItemDTO.MenuId,
                FoodItemId = menuFoodItemDTO.FoodItemId
            };
        }
    }
}
