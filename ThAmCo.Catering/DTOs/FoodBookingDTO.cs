using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.DTOs
{
    public class FoodBookingDTO
    {
        public int FoodBookingId { get; set; }
        public int? ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }
        public DateTime FoodBookingDate { get; set; }

        public FoodBookingDTO CreateDTO(Catering.Models.FoodBooking foodBooking)
        {
            return new FoodBookingDTO
            {
                FoodBookingId = foodBooking.FoodBookingId,
                ClientReferenceId = foodBooking.ClientReferenceId,
                NumberOfGuests = foodBooking.NumberOfGuests,
                MenuId = foodBooking.MenuId,
                FoodBookingDate = foodBooking.FoodBookingDate
            };
        }

    }
}
