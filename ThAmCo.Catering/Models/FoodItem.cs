using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Models
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(50), Required]
        public string Description { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        public ICollection<MenuFoodItem> MenuFoodItems { get; set; } = [];

    }

}
