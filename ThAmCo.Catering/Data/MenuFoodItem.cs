using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        [Key]
        public int MenuFoodItemId { get; set; }
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }

    }
}
