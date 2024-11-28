namespace ThAmCo.Events.DTOs
{
    public class FlatMenuDTO
    {
        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public List<FlatFoodDTO> FoodItems { get; set; }
    }

    public  class FlatFoodDTO
    {
        public int FoodItemId { get; set; }

        public string FoodItemName { get; set; }

    }
}
