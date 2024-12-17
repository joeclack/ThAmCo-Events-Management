using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
	public class MenuFoodItemPostDTO
	{
		public int FoodItemId { get; set; }
		public string FoodItemName { get; set; } = string.Empty;
		public string FoodItemDescription { get; set; } = string.Empty;
		public float FoodItemUnitPrice { get; set; }

		public MenuFoodItemPostDTO CreateDTO(MenuFoodItem item)
		{
			return new MenuFoodItemPostDTO
			{
				FoodItemId = item.FoodItemId,
				FoodItemName = item.FoodItem.Name,
				FoodItemDescription = item.FoodItem.Description,
				FoodItemUnitPrice = item.FoodItem.UnitPrice

			};
		}
	}
}
