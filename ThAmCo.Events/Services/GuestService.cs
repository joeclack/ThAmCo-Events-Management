using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Services
{
    public class GuestService
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public GuestService(ThAmCo.Events.Data.EventsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> GetAllGuests()
        {
            var guests = await _context.Guests
                .Include(s => s.GuestBookings)
                    .ThenInclude(gb=>gb.Event)
            .ToListAsync();

            return guests;
        }

        public async Task<Guest> GetGuest(int? guestId)
        {
            var guest = await _context.Guests
                .Include(e => e.GuestBookings)
                .ThenInclude(ev => ev.Event)
                .FirstOrDefaultAsync(s => s.GuestId == guestId);

            return guest;
        }

        internal async Task<List<GuestBooking>> GetBookings(Guest guest)
        {
            List<GuestBooking> bookings = guest.GuestBookings.ToList();
            return bookings;
        }

        public async Task<List<Guest>> GetAvailableGuests(int eventId)
        {
            List<Guest> AvailableGuests = [];
            var guest = await _context.Guests.ToListAsync();
            foreach (var g in guest)
            {
                var bookings = g.GuestBookings;
                if (bookings.Count == 0)
                {
                    AvailableGuests.Add(g);
                }
                foreach (var x in bookings)
                {
                    if (x.EventId != eventId)
                    {
                        AvailableGuests.Add(g);
                    }
                }
            }
            return AvailableGuests;
        }
    }
}
