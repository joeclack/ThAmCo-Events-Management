namespace ThAmCo.Events.Data
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EventId { get; set; }
        public ICollection<GuestBooking> GuestBookings { get; set; }
    }
}
