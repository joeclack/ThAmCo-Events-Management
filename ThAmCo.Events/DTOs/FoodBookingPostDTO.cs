namespace ThAmCo.Events.DTOs
{
	internal class FoodBookingPostDTO
	{
		public int FoodBookingId { get; set; }
		public int? ClientReferenceId { get; set; }
		public int NumberOfGuests { get; set; }
		public int MenuId { get; set; }
		public MenuInfoDTO MenuInfo { get; set; }
		public DateTime FoodBookingDate { get; set; }

		public FoodBookingPostDTO CreateDTO(FoodBookingDTO foodBooking)
		{
			return new FoodBookingPostDTO()
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