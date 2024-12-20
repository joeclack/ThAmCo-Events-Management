namespace ThAmCo.Events.Services
{
	using Microsoft.EntityFrameworkCore;
	using ThAmCo.Events.Models;

	/// <summary>
	/// Defines the <see cref="GuestService" />
	/// </summary>
	public class GuestService
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="GuestService"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		public GuestService(ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// The GetAllGuests
		/// </summary>
		/// <returns>The <see cref="Task{List{Guest}}"/></returns>
		public async Task<List<Guest>> GetAllGuests()
		{
			var guests = await _context.Guests
				.Include(s => s.GuestBookings)
				.ThenInclude(gb => gb.Event)
				.ToListAsync();

			return guests;
		}

		/// <summary>
		/// The GetGuest
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int?"/></param>
		/// <returns>The <see cref="Task{Guest}"/></returns>
		public async Task<Guest> GetGuest(int? guestId)
		{
			var guest = await _context.Guests
				.Include(e => e.GuestBookings)
				.ThenInclude(ev => ev.Event)
				.FirstOrDefaultAsync(s => s.GuestId == guestId);

			return guest;
		}

		/// <summary>
		/// The GetBookings
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{GuestBooking}}"/></returns>
		internal async Task<List<GuestBooking>> GetBookings(int guestId)
		{
			var guest                   = await GetGuest(guestId);
			List<GuestBooking> bookings = guest.GuestBookings.Where(gb => gb.Event.Date >= DateTime.Today && !gb.Event.IsCanceled).OrderBy(x => x.Event.Date).ToList();
			return bookings;
		}

		/// <summary>
		/// The GetPastBookings
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{GuestBooking}}"/></returns>
		internal async Task<List<GuestBooking>> GetPastBookings(int guestId)
		{
			var guest                   = await GetGuest(guestId);
			List<GuestBooking> bookings = guest.GuestBookings.Where(gb => gb.Event.Date < DateTime.Today || gb.Event.IsCanceled).OrderBy(x => x.Event.Date).ToList();
			return bookings;
		}

		/// <summary>
		/// The GetAvailableGuests
		/// </summary>
		/// <param name="_event">The _event<see cref="Event"/></param>
		/// <returns>The <see cref="Task{List{Guest}}"/></returns>
		public async Task<List<Guest>> GetAvailableGuests(Event _event)
		{
			List<Guest> AvailableGuests = [];
			var guests                  = await GetAllGuests();

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

		/// <summary>
		/// The DeleteGuest
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
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

		/// <summary>
		/// The CancelGuestBooking
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CancelGuestBooking(int guestId, int eventId)
		{
			var guest        = await GetGuest(guestId);
			var guestBooking = await GetBooking(guestId, eventId);
			guest.GuestBookings.Remove(guestBooking);
			_context.SaveChanges();
		}

		/// <summary>
		/// The GetBooking
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{GuestBooking}"/></returns>
		private async Task<GuestBooking> GetBooking(int guestId, int eventId)
		{
			var bookings = await GetBookings(guestId);
			return bookings.Where(x => x.EventId == eventId).FirstOrDefault();
		}

		/// <summary>
		/// The CreateBooking
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateBooking(int guestId, Event @event)
		{
			if (guestId == 0)
			{
				return;
			}
			var guest        = await _context.Guests.FindAsync(guestId);
			var guestBooking = new GuestBooking() { Guest = guest, Event = @event };

			if (guestBooking.Guest == null)
			{
				return;
			}

			guest.GuestBookings.Add(guestBooking);
			_context.SaveChanges();

		}

		/// <summary>
		/// The CreateGuest
		/// </summary>
		/// <param name="guest">The guest<see cref="Guest"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateGuest(Guest guest)
		{
			await _context.Guests.AddAsync(guest);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// The UpdateGuestAttendance
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="didAttend">The didAttend<see cref="bool"/></param>
		/// <returns>The <see cref="Task"/></returns>
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
