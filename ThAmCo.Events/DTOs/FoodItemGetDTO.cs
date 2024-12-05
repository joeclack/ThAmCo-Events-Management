using ThAmCo.Events.Models;

namespace ThAmCo.Events.DTOs
{
    public class FoodItemGetDTO
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; }

    }
}
