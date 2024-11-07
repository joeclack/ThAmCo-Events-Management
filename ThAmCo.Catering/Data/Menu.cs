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

        public ICollection<MenuFoodItem> MenuFoodItems { get; set; }

        public ICollection<FoodBooking> FoodBookings { get; set; }

    }
}
