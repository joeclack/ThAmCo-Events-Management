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

        public async Task<Staff> GetStaffMember(int? staffId)
        {
            var staffMember = await _context.Staff
                .Include(e => e.Staffings)
                .ThenInclude(s => s.Event)
                .FirstOrDefaultAsync(s => s.StaffId == staffId);
            

            return staffMember;
        }

		public async Task<List<Staff>> GetAvailableStaff(Event _event)
		{
			List<Staff> AvailableStaff = [];
			var staff = await GetAllStaff();

			foreach (var s in staff)
			{
				var staffings = s.Staffings;

				if (staffings.Count == 0)
				{
					AvailableStaff.Add(s);
					continue;
				}

				bool isAvailable = true;
				foreach (var staffing in staffings)
				{
					if (staffing.EventId == _event.EventId || staffing.Event.Date == _event.Date.Date)
					{
						isAvailable = false;
						break;
					}
				}

				if (isAvailable)
				{
					AvailableStaff.Add(s);
				}
			}

			return AvailableStaff;
		}


		public async Task DeleteStaffMember(int staffId)
        {
            try
            {
                var staffMember = await GetStaffMember(staffId);
                _context.Staff.Remove(staffMember);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Staff not deleted");
            }

            Console.WriteLine("Staff deleted");
        }

        internal async Task<List<Event>> GetStaffMemberEvents(int staffId)
        {
            var staffMember = await GetStaffMember(staffId);
            List<Event> events = staffMember.Staffings.Select(x=>x.Event).ToList();
            return events;
        }

        public async Task CreateStaffing(int staffId, Event @event)
        {
            if (staffId == 0)
            {
                return;
            }
            var staff = await GetStaffMember(staffId);

            if(staff == null)
            {
                return;
            }
            try
            {
                staff.Staffings.Add(new Staffing() { Staff = staff, Event = @event });
                _context.SaveChanges();
            } catch
            {
                Console.WriteLine("StaffMember not added to event staffing");
            }

            Console.WriteLine("StaffMember added to event staffing");
        }
        
        public async Task CancelStaffing(int staffId, int eventId)
        {
            var staffMember  = await GetStaffMember(staffId);
            var staffing = await GetStaffing(staffId, eventId);
            try
            {
                staffMember.Staffings.Remove(staffing);
                _context.SaveChanges();
            }
            catch
            {
            }
        }

        private async Task<Staffing> GetStaffing(int staffId, int eventId)
        {
            var staffings = await GetStaffings(staffId, eventId);
            return staffings.Where(x => x.EventId == eventId).FirstOrDefault();
        }

        private async Task<List<Staffing>> GetStaffings(int staffId, int eventId)
        {
            var staffMember = await GetStaffMember(staffId);
            List<Staffing> staffing = staffMember.Staffings.OrderBy(x=>x.Event.Date).ToList();
            return staffing;
        }

        public async Task CreateStaff(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStaffAttendance(int staffId, int eventId, bool didAttend)
        {
            var staffing = await GetStaffing(staffId, eventId);
            
            if (staffing != null)
            {
                staffing.DidAttend = didAttend;
                await _context.SaveChangesAsync();
            }
        }
    }
}
