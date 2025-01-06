namespace ThAmCo.Events.Services
{
	using Microsoft.EntityFrameworkCore;
	using System.Text.Json;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;

	/// <summary>
	/// Defines the <see cref="EventService" />
	/// </summary>
	public class EventService
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Defines the _httpClient
		/// </summary>
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Defines the VenuesServiceBaseUrl
		/// </summary>
		const string VenuesServiceBaseUrl = "https://localhost:7088/api";

		/// <summary>
		/// Defines the EventTypesEndPoint
		/// </summary>
		const string EventTypesEndPoint = "/EventTypes";

		/// <summary>
		/// Defines the AvailabilityEndPoint
		/// </summary>
		const string AvailabilityEndPoint = "/Availability";

		/// <summary>
		/// Defines the ReservationEndPoint
		/// </summary>
		const string ReservationEndPoint = "/Reservations";

		/// <summary>
		/// Defines the jsonOptions
		/// </summary>
		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="EventService"/> class.
		/// </summary>
		/// <param name="httpClient">The httpClient<see cref="HttpClient"/></param>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		public EventService(HttpClient httpClient, ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context;
			_httpClient = httpClient;
		}

		/// <summary>
		/// The GetAllEvents
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
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

		/// <summary>
		/// The GetEvent
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int?"/></param>
		/// <returns>The <see cref="Task{Event}"/></returns>
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

		/// <summary>
		/// The GetPastCancelledEvents
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		public async Task<List<Event>> GetPastCancelledEvents(IList<Event> events)
		{
			var pastAndCancelledEvents = events.Where(x => x.IsCanceled || x.Date < DateTime.Today).ToList();
			return pastAndCancelledEvents;
		}

		/// <summary>
		/// The GetUpcomingEvent
		/// </summary>
		/// <returns>The <see cref="Task{Event}"/></returns>
		public async Task<Event> GetUpcomingEvent()
		{
			var _event = await _context.Events
				.Include(e => e.Staffings)
				.ThenInclude(s => s.Staff)
				.Include(e => e.GuestBookings)
				.ThenInclude(g => g.Guest)
				.OrderBy(e => e.Date)
				.FirstOrDefaultAsync(x => x.Date >= DateTime.Today);

			return _event;
		}

		/// <summary>
		/// The RemoveStaffFromEventStaffing
		/// </summary>
		/// <param name="staffMember">The staffMember<see cref="Staff"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task RemoveStaffFromEventStaffing(Staff staffMember, int eventId)
		{
			var _event   = await GetEvent(eventId);
			var staffing = _event.Staffings.FirstOrDefault(s => s.StaffId == staffMember.StaffId);

			staffMember.Staffings.Remove(staffing);
			_context.SaveChanges();

		}

		/// <summary>
		/// The CancelEvent
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CancelEvent(int eventId)
		{
			var _event        = await GetEvent(eventId);
			_event.IsCanceled = true;
			await UpdateEvent(_event);	
		}

		/// <summary>
		/// The GetAvailableEventsForGuest
		/// </summary>
		/// <param name="guest">The guest<see cref="Guest"/></param>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		public async Task<List<Event>> GetAvailableEventsForGuest(Guest guest)
		{
			List<Event> availableEvents = new List<Event>();

			var events      = await GetUpcomingEvents();
			availableEvents = events
				.Where(e => !e.GuestBookings.Any(gb => gb.GuestId == guest.GuestId))
				.ToList();

			return availableEvents;
		}

		/// <summary>
		/// The CreateEvent
		/// </summary>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateEvent(Event @event)
		{
			_context.Events.Add(@event);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// The GetAvailableEventsForStaffMember
		/// </summary>
		/// <param name="staff">The staff<see cref="Staff"/></param>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		public async Task<List<Event>> GetAvailableEventsForStaffMember(Staff staff)
		{
			List<Event> availableEvents = new List<Event>();

			var events		= await GetUpcomingEvents();
			availableEvents = events
				.Where(e => !e.Staffings.Any(s => s.StaffId == staff.StaffId))
				.ToList();

			return availableEvents;
		}

		/// <summary>
		/// The UpdateEvent
		/// </summary>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task"/></returns>
		internal async Task UpdateEvent(Event @event)
		{
			_context.Attach(@event).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{

			}
		}

		/// <summary>
		/// The GetUpcomingEvents
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		internal async Task<List<Event>> GetUpcomingEvents()
		{
			var events = await GetAllEvents();
			return new List<Event>(events.Where(x => x.Date >= DateTime.Today && !x.IsCanceled));
		}

		/// <summary>
		/// The GetPastEvents
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		internal async Task<List<Event>> GetPastEvents()
		{
			var events = await GetAllEvents();
			return new List<Event>(events.Where(x => x.Date < DateTime.Today));
		}

		/// <summary>
		/// The GetEventTypes
		/// </summary>
		/// <returns>The <see cref="Task{List{EventTypeDTO}}"/></returns>
		public async Task<List<EventTypeDTO>> GetEventTypes()
		{
			var response = await _httpClient.GetAsync(VenuesServiceBaseUrl + EventTypesEndPoint);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var types        = JsonSerializer.Deserialize<List<EventTypeDTO>>(jsonResponse, jsonOptions);

				if (types == null)
				{
					throw new ArgumentNullException(nameof(response), "Event types were null");
				}
				return types;
			}

			return [];
		}

		/// <summary>
		/// The GetAvailableVenuesForEventDate
		/// </summary>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task{List{VenueDTO}}"/></returns>
		public async Task<List<VenueDTO>> GetAvailableVenuesForEventDate(Event @event)
		{
			string type                                           = @event.EventTypeId;
			string startDate                                      = @event.Date.ToString("yyyy-MM-dd");
			string endDate                                        = @event.Date.ToString("yyyy-MM-dd");
			var response                                          = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{AvailabilityEndPoint}" +
													  $"?eventType={type}" +
													  $"&beginDate={startDate}" +
													  $"&endDate  ={endDate}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var venues       = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);

				if (venues == null)
				{
					throw new ArgumentNullException(nameof(response), "Availabilities were null");
				}
				return venues;
			}

			return [];
		}

		/// <summary>
		/// The GetAllAvailableVenues
		/// </summary>
		/// <param name="eventType">The eventType<see cref="string"/></param>
		/// <returns>The <see cref="Task{List{VenueDTO}}"/></returns>
		public async Task<List<VenueDTO>> GetAllAvailableVenues(string eventType)
		{
			string type                                           = eventType;
			string startDate                                      = DateTime.Today.Date.AddYears(-100).ToString("yyyy-MM-dd");
			string endDate                                        = DateTime.Today.Date.AddYears(100).ToString("yyyy-MM-dd");
			var response                                          = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{AvailabilityEndPoint}" +
													  $"?eventType={type}" +
													  $"&beginDate={startDate}" +
													  $"&endDate  ={endDate}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var venues       = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);

				if (venues == null)
				{
					throw new ArgumentNullException(nameof(response), "Availabilities were null");
				}
				return venues;
			}

			return [];
		}

		/// <summary>
		/// The GetAvailableVenuesTimePeriod
		/// </summary>
		/// <param name="eventType">The eventType<see cref="string"/></param>
		/// <param name="startDate">The startDate<see cref="DateTime"/></param>
		/// <param name="endDate">The endDate<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{List{VenueDTO}}"/></returns>
		public async Task<List<VenueDTO>> GetAvailableVenuesTimePeriod(string eventType, DateTime startDate, DateTime endDate)
		{
			string type                                           = eventType;
			string start                                          = startDate.ToString("yyyy-MM-dd");
			string end                                            = endDate.ToString("yyyy-MM-dd");
			var response                                          = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{AvailabilityEndPoint}" +
													  $"?eventType={type}" +
													  $"&beginDate={start}" +
													  $"&endDate={end}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var venues       = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);

				if (venues == null)
				{
					throw new ArgumentNullException(nameof(response), "Availabilities were null");
				}
				return venues;
			}

			return [];
		}

		/// <summary>
		/// The CreateReservation
		/// </summary>
		/// <param name="reservationPostDTO">The reservationPostDTO<see cref="ReservationPostDTO"/></param>
		/// <returns>The <see cref="Task{string}"/></returns>
		internal async Task<string> CreateReservation(ReservationPostDTO reservationPostDTO)
		{
			var response = await _httpClient.PostAsJsonAsync($"{VenuesServiceBaseUrl}{ReservationEndPoint}", reservationPostDTO);
			if (response.IsSuccessStatusCode)
			{
				var reservationDto = await response.Content.ReadFromJsonAsync<ReservationGetDTO>();
				return reservationDto.Reference;
			}
			return string.Empty;
		}

		/// <summary>
		/// The GetReservation
		/// </summary>
		/// <param name="reference">The reference<see cref="string"/></param>
		/// <returns>The <see cref="Task{ReservationGetDTO}"/></returns>
		internal async Task<ReservationGetDTO> GetReservation(string reference)
		{
			var response = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{ReservationEndPoint}/" + reference);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse   = await response.Content.ReadAsStringAsync();
				var reservationDto = JsonSerializer.Deserialize<ReservationGetDTO>(jsonResponse, jsonOptions);
				return reservationDto;
			}
			return new();
		}

		/// <summary>
		/// The DeleteReservation
		/// </summary>
		/// <param name="reservationId">The reservationId<see cref="string"/></param>
		/// <returns>The <see cref="Task"/></returns>
		internal async Task DeleteReservation(string reservationId)
		{
			await _httpClient.DeleteAsync($"{VenuesServiceBaseUrl}{ReservationEndPoint}{reservationId}");
			var events  = await GetAllEvents();
			var _event  = events.FirstOrDefault(x => x.ReservationId == reservationId);
			if (_event != null)
			{
				_event.ReservationId = string.Empty;
				await UpdateEvent(_event);
			}
		}
	}
}
