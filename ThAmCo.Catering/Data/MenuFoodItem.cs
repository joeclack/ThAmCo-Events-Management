using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        [Key]
        public Menu MenuId { get; set; }
        [Key]
        public FoodItem FoodItemId { get; set; }

    }
}
