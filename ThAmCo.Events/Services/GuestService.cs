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

        internal async Task<List<GuestBooking>> GetBookings(int guestId)
        {
            var guest = await GetGuest(guestId);
            List<GuestBooking> bookings = guest.GuestBookings.OrderBy(x=>x.Event.Date).ToList();
            return bookings;
        }

		public async Task<List<Guest>> GetAvailableGuests(Event _event)
		{
			List<Guest> AvailableGuests = [];
			var guests = await GetAllGuests();

			foreach (var g in guests)
			{
				var bookings = g.GuestBookings;

				if (bookings.Count == 0)
				{
					AvailableGuests.Add(g);
					continue;
				}
				bool isAvailable = true;
				foreach (var booking in bookings)
				{
					if (booking.EventId == _event.EventId || booking.Event.Date == _event.Date.Date)
					{
						isAvailable = false;
						break;
					}
				}

				if (isAvailable)
				{
					AvailableGuests.Add(g);
				}
			}

			return AvailableGuests;
		}


		public async Task DeleteGuest(int guestId)
        {
            try
            {
                var guest = await GetGuest(guestId);
                _context.Guests.Remove(guest);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Guest not deleted");
            }

            Console.WriteLine("Guest deleted");
        }
        
        public async Task CancelGuestBooking(int guestId, int eventId)
        {
            var guest  = await GetGuest(guestId);
            var guestBooking = await GetBooking(guestId, eventId);
            try
            {
                guest.GuestBookings.Remove(guestBooking);
                _context.SaveChanges();
            }
            catch
            {
            }
        }

        private async Task<GuestBooking> GetBooking(int guestId, int eventId)
        {
            var bookings = await GetBookings(guestId);
            return bookings.Where(x => x.EventId == eventId).FirstOrDefault();
        }

        public async Task CreateBooking(int guestId, Event @event)
        {
            if (guestId == 0)
            {
                return;
            }
            var guest = await _context.Guests.FindAsync(guestId);
            var guestBooking = new GuestBooking() { Guest = guest, Event = @event };
            if (guestBooking.Guest == null)
            {
                return;
            }
            try
            {
                guest.GuestBookings.Add(guestBooking);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Guest booking not added");
            }

            Console.WriteLine("Guest booking added");
        }

        public async Task CreateGuest(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGuestAttendance(int guestId, int eventId, bool didAttend)
        {
            var guestBooking = await GetBooking(guestId, eventId);
            
            if (guestBooking != null)
            {
                guestBooking.DidAttend = didAttend;
                await _context.SaveChangesAsync();
            }
        }
    }
}
