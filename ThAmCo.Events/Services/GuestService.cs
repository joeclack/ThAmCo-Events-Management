namespace ThAmCo.Events.Services
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using ThAmCo.Events.Models;

	/// <summary>
	/// A service that provides methods to interact with Guest data
	/// </summary>
	public class GuestService
	{
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		public GuestService(ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Retieves all guests and their bookings
		/// </summary>
		/// <returns>The <see cref="Task{List{Guest}}"/></returns>
		public async Task<List<Guest>> GetAllGuests()
		{
			var guests = await _context.Guests
				.Include(s => s.GuestBookings)
				.ThenInclude(gb => gb.Event)
				.Where(x=>x.IsAnonymised == false)
				.ToListAsync();

			return guests;
		}

		/// <summary>
		/// Retrieves requested Guest
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
		/// Retrieves bookings associated with the guest that are not cancelled and are upcoming
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
		/// Retrieves all guest bookings no matter the date or IsCancelled
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{GuestBooking}}"/></returns>
		internal async Task<List<GuestBooking>> GetAllGuestBookings(int guestId)
		{
			var guest = await GetGuest(guestId);
			List<GuestBooking> bookings = guest.GuestBookings.ToList();
			return bookings;
		}

		/// <summary>
		/// Retrieves past guest bookings
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
		/// Retrieves available guests to book onto an event
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
		/// Soft deletes requested guest 
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task SoftDeleteGuest(int guestId)
		{
			var userToDelete = await GetGuest(guestId);
			if (userToDelete != null)
			{
				AnonymiseUser(userToDelete);
				_context.Entry(userToDelete).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}

		}

		/// <summary>
		/// Anonymises data by hashing the string
		/// </summary>
		/// <param name="value">The guestId<see cref="string"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public static string Pseudonymise(string value)
		{
			return "Anonymised_" + value.GetHashCode();
		}

		/// <summary>
		/// Anonymises guest personal data and sets IsAnonymised to true
		/// </summary>
		/// <param name="value">The guestId<see cref="string"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public static void AnonymiseUser(Guest guest)
		{
			// Pseudonymise guest data
			guest.FirstName = Pseudonymise(guest.FirstName);
			guest.LastName = Pseudonymise(guest.LastName);
			guest.Email = Pseudonymise(guest.Email);
			guest.IsAnonymised = true;
		}

		/// <summary>
		/// Removes the guest booking freeing that guest from that event
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
		/// Retrieves guest booking for event
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
		/// Creates new guest booking
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
		/// Creates a new guest 
		/// </summary>
		/// <param name="guest">The guest<see cref="Guest"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateGuest(Guest guest)
		{
			await _context.Guests.AddAsync(guest);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Updates guest booking attendance for previous event
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="didAttend">The didAttend<see cref="bool"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task UpdateGuestAttendance(int guestId, int eventId, bool didAttend)
		{
			var bookings = await GetAllGuestBookings(guestId);
			var booking = bookings.Where(x => x.EventId == eventId).FirstOrDefault();

			if (booking != null)
			{
				booking.DidAttend = didAttend;
				await _context.SaveChangesAsync();
			}
			
		}
	}
}
