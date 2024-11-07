using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public int MenuId { get; set; }

        public int FoodItemId { get; set; }
    }
}
