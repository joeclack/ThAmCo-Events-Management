namespace ThAmCo.Events.Services
{
	using Microsoft.EntityFrameworkCore;
	using System.Text.Json;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;

	/// <summary>
	/// A service that provides methods to interact with the Event data
	/// </summary>
	public class EventService
	{
		private readonly ThAmCo.Events.Data.EventsDbContext _context;
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Defines the Venues API Base Url
		/// </summary>
		const string VenuesServiceBaseUrl = "https://localhost:7088/api";

		/// <summary>
		/// Defines the EventTypes EndPoint
		/// </summary>
		const string EventTypesEndPoint = "/EventTypes";

		/// <summary>
		/// Defines the Availability EndPoint
		/// </summary>
		const string AvailabilityEndPoint = "/Availability";

		/// <summary>
		/// Defines the Reservation EndPoint
		/// </summary>
		const string ReservationEndPoint = "/Reservations";

		/// <summary>
		/// Defines the jsonOptions
		/// </summary>
		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public EventService(HttpClient httpClient, ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context;
			_httpClient = httpClient;
		}

		/// <summary>
		/// Retrieves all events with its staffings and guest bookings
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
		/// Retrieves request event
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
		/// Gets events that are either cancelled or in the past
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		public async Task<List<Event>> GetPastCancelledEvents()
		{
			var events = await GetAllEvents();
			var pastAndCancelledEvents = events.Where(x => x.IsCanceled || x.Date < DateTime.Today).ToList();
			return pastAndCancelledEvents;
		}

		/// <summary>
		/// Permanently deletes an event. This is not a soft delete
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task PermDeleteEvent(int eventId)
		{
			var _event = await GetEvent(eventId);
			_context.Events.Remove(_event);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Soft deletes the event by setting the IsCancelled bool
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
		/// Retrieves events that a guest can be booked onto
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
		/// Creates a new event
		/// </summary>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateEvent(Event @event)
		{
			_context.Events.Add(@event);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Retrieves events that a staff member can be assigned to
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
		/// Updates event details
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
		/// Retrieves future events
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		internal async Task<List<Event>> GetUpcomingEvents()
		{
			var events = await GetAllEvents();
			return new List<Event>(events.Where(x => x.Date >= DateTime.Today && !x.IsCanceled));
		}

		/// <summary>
		/// Retrieves past events
		/// </summary>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		internal async Task<List<Event>> GetPastEvents()
		{
			var events = await GetAllEvents();
			return new List<Event>(events.Where(x => x.Date < DateTime.Today));
		}

		/// <summary>
		/// Retrieves event types from the Venues API
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
		/// Retrieves available venues for an event that has already been created
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
		/// Retrieves all available venues for event type
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
		/// Retrieves available venues for event type within the date range supplied
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
		/// Creates a reservation
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
		/// Retrieves requested reservation 
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
		/// Deletes reservation, freeing up the venue
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
