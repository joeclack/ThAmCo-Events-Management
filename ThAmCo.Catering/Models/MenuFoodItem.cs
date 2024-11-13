using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Catering.Models
{
    public class MenuFoodItem
    {
        [Required]
        public int MenuId { get; set; }
        [Required]
        public int FoodItemId { get; set; }

        // Navigation Properties
        public Menu Menu { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
