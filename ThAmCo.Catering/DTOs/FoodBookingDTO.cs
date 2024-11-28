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

        public FoodBookingDTO CreateDTO(FoodBooking foodBooking)
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
        
        public FoodBooking CreateModel(FoodBookingDTO foodBookingDTO)
        {
            return new FoodBooking
            {
                FoodBookingId = foodBookingDTO.FoodBookingId,
                ClientReferenceId = foodBookingDTO.ClientReferenceId,
                NumberOfGuests = foodBookingDTO.NumberOfGuests,
                MenuId = foodBookingDTO.MenuId,
                FoodBookingDate = foodBookingDTO.FoodBookingDate
            };
        }

    }
}
