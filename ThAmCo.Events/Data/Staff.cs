namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public ICollection<Staffing> Staffing { get; set; }
    }
}
