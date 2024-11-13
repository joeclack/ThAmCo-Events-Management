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

        public async Task AddStaffToEventStaffing(Staff staff, Event _event)
        {
            var staffing = new Staffing() { Staff = staff, Event = _event };
            
            try
            {
                staff.Staffings.Add(staffing);
                //_event.Staffings.Add(staffing);
                _context.SaveChanges();
            } catch
            {
                Console.WriteLine("StaffMembers not added to event staffing");
            }

            Console.WriteLine("StaffMembers added to event staffing");
        }

        public async Task RemoveStaffFromEventStaffing(Staff staff, Event _event)
        {
            var staffing = _event.Staffings.FirstOrDefault(s => s.StaffId == staff.StaffId);
            try
            {
                staff.Staffings.Remove(staffing);
                //_event.Staffings.Remove(staffing);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("StaffMembers not removed from event staffing");
            }

            Console.WriteLine("StaffMembers removed from event staffing");
        }

        public async Task CancelGuestBooking(Guest guest, Event _event)
        {
            var guestBooking = _event.GuestBookings.FirstOrDefault(g => g.GuestId == guest.GuestId);
            try
            {
                guest.GuestBookings.Remove(guestBooking);
                //_event.GuestBookings.Remove(guestBooking);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("StaffMembers not removed from event staffing");
            }

            Console.WriteLine("StaffMembers removed from event staffing");
        }

        public async Task CancelEvent(Event _event)
        {
            try
            {
                _context.Events.Remove(_event);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Event not cancelled");
            }

            Console.WriteLine("Event cancelled");
        }

        public async Task AddGuestToEventGuests(Guest? guest, Event? _event)
        {
            var guestBooking = new GuestBooking() { Guest = guest, Event = _event };
            try
            {
                guest.GuestBookings.Add(guestBooking);
                //_event.GuestBookings.Add(guestBooking);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("StaffMembers not added to event staffing");
            }

            Console.WriteLine("StaffMembers added to event staffing");
        }
    }
}
