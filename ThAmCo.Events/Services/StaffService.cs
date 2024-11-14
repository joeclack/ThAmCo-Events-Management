using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Services
{
    public class StaffService
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public StaffService(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Staff>> GetAllStaff()
        {
            var staffMembers = await _context.Staff
                .Include(s => s.Staffings)
                    .ThenInclude(s => s.Event)

            .ToListAsync();

            return staffMembers;
        }

        public async Task<Staff> GetStaff(int? staffId)
        {
            var staffMember = await _context.Staff
                .Include(e => e.Staffings)
                .FirstOrDefaultAsync(s => s.StaffId == staffId);

            return staffMember;
        }

        public async Task<List<Staff>> GetAvailableStaff(int eventId)
        {
            List<Staff> AvailableStaff = [];
            var staff = await _context.Staff.ToListAsync();
            foreach(var s in staff)
            {
                var staffing = s.Staffings;
                if(staffing.Count == 0)
                {
                    AvailableStaff.Add(s);
                }
                foreach(var x in staffing)
                {
                    if(x.EventId != eventId)
                    {
                        AvailableStaff.Add(s);
                    }
                }
            }
            return AvailableStaff;
        }

        public async Task DeleteStaffMember(Staff? staffMember)
        {
            try
            {
                _context.Staff.Remove(staffMember);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Staff not deleted");
            }

            Console.WriteLine("Staff deleted");
        }

        internal async Task<List<Event>> GetEvents(Staff staff)
        {
            List<Event> events = await _context.Events
                .Where(e => e.Staffings
                .Any(s => s.StaffId == staff.StaffId)).ToListAsync();
            return events;
        }

        //public async Task GetAssignedEvents(int staffId)
        //{
        //    var events = _context.
        //}
    }
}
