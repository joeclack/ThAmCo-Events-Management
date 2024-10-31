using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public Collection<MenuFoodItem> MenuFoodItems { get; set; }
    }
}
