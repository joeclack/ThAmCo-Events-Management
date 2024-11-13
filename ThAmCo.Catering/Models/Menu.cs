using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        [Required]
        [MaxLength(50)]
        public string MenuName { get; set; } = string.Empty;
        public ICollection<MenuFoodItem> MenuFoodItems { get; set; } = [];
        public ICollection<FoodBooking> FoodBookings { get; set; } = [];

    }
}
