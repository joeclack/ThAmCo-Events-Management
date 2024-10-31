using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [MaxLength(50)]
        public string MenuName { get; set; }
        public Collection<MenuFoodItem> MenuFoodItems { get; set; }
        public Collection<FoodBooking> FoodBookings { get; set; }
    }
}
