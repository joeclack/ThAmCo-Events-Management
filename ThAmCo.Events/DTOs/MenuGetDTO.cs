namespace ThAmCo.Events.DTOs
{
    public class MenuGetDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public ICollection<FoodItemGetDTO> MenuFoodItems { get; set; } = [];
        public ICollection<FoodBookingDTO> FoodBookings { get; set; } = [];
    }
}
