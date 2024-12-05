namespace ThAmCo.Events.DTOs
{
    public class FoodItemPostDTO
    {
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; } = string.Empty;
        public double FoodItemPrice { get; set; }
    }
}
