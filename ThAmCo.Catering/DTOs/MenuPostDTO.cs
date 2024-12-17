using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
	public class MenuPostDTO
	{
		public int MenuId { get; set; }
		public string MenuName { get; set; } = string.Empty;
		public ICollection<MenuFoodItemPostDTO> MenuFoodItems { get; set; } = [];
		public ICollection<FoodBookingDTO> FoodBookings { get; set; } = [];

		public MenuPostDTO CreateDTO(Menu menu)
		{
			return new MenuPostDTO
			{
				MenuId = menu.MenuId,
				MenuName = menu.MenuName,
				MenuFoodItems = menu.MenuFoodItems.Select(mfi => new MenuFoodItemPostDTO().CreateDTO(mfi)).ToList(),
				FoodBookings = menu.FoodBookings.Select(fb => new FoodBookingDTO().CreateDTO(fb)).ToList()
			};
		}
		public Menu CreateModel(MenuDTO menuDTO)
		{
			return new Menu
			{
				MenuId = menuDTO.MenuId,
				MenuName = menuDTO.MenuName,
				//MenuFoodItems = menuDTO.MenuFoodItems.Select(mfi => new MenuFoodItemDTO().CreateModel(mfi)).ToList(),
				FoodBookings = menuDTO.FoodBookings.Select(fb => new FoodBookingDTO().CreateModel(fb)).ToList()
			};
		}
	}
}
