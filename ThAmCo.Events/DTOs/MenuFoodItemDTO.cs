using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class MenuFoodItemDTO
    {
		public int FoodItemId { get; set; }
		public string FoodItemName { get; set; } = string.Empty;
		public string FoodItemDescription { get; set; } = string.Empty;
		public float FoodItemUnitPrice { get; set; }
	}
}
