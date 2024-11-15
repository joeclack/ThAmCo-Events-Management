using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Services
{
    public class EventService
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public EventService(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            var events = await _context.Events
            .Include(e => e.Staffings)
                .ThenInclude(s => s.Staff)
            .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
            .ToListAsync();

            return events;
        }
        
        public async Task<Event> GetEvent(int? eventId)
        {
            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == eventId);

            return _event;
        }
        
        public async Task RemoveStaffFromEventStaffing(Staff staffMember, int eventId)
        {
            var _event = await GetEvent(eventId);
            var staffing = _event.Staffings.FirstOrDefault(s => s.StaffId == staffMember.StaffId);
            try
            {
                staffMember.Staffings.Remove(staffing);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("StaffMember not removed from event staffing");
            }

            Console.WriteLine("StaffMember removed from event staffing");
        }

        public async Task CancelEvent(int eventId)
        {
            try
            {
                var _event = await GetEvent(eventId);
                _context.Events.Remove(_event);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Event not cancelled");
            }

            Console.WriteLine("Event cancelled");
        }
        
        public async Task<List<Event>> GetAvailableEventsForGuest(Guest guest)
        {
            List<Event> availableEvents = new List<Event>();

            var events = await GetAllEvents();
            availableEvents = events
                .Where(e => !e.GuestBookings.Any(gb => gb.GuestId == guest.GuestId))
                .ToList();

            return availableEvents;
        }

        public async Task CreateEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAvailableEventsForStaffMember(Staff staff)
        {
            List<Event> availableEvents = new List<Event>();

            var events = await GetAllEvents();
            availableEvents = events
                .Where(e => !e.Staffings.Any(s =>s.StaffId == staff.StaffId))
                .ToList();

            return availableEvents;
        }
    }
}
