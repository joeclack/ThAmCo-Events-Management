namespace ThAmCo.Events.Data
{
    public class Event
    {
        public string EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<Staff> Staff { get; set; }
        public List<Guest> Guests { get; set; }
    }
}
