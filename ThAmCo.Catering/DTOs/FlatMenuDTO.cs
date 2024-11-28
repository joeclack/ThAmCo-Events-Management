using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class FlatMenuDTO
    {
        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public List<FlatFoodDTO> FoodItems { get; set; }
        public FlatMenuDTO CreateFlatMenuDTO(Menu menu)
        {
            var dto = new FlatMenuDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                FoodItems = menu.MenuFoodItems.Select(f => new FlatFoodDTO
                {
                    FoodItemId = f.FoodItem.FoodItemId,
                    FoodItemName = f.FoodItem.Name
                }).ToList()
            };
            return dto;
        }
    }

    public  class FlatFoodDTO
    {
        public int FoodItemId { get; set; }

        public string FoodItemName { get; set; }

        public FlatFoodDTO CreateFlatFoodDTO(FoodItem item)
        {
            var dto = new FlatFoodDTO
            {
                FoodItemId = item.FoodItemId,
                FoodItemName = item.Name
            };
            return dto;
        }
    }
}
