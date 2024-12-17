using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Services
{
	public class EventService
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
		private readonly HttpClient _httpClient; 
        const string VenuesServiceBaseUrl = "https://localhost:7088/api";
		const string EventTypesEndPoint = "/EventTypes"; 
		const string AvailabilityEndPoint = "/Availability"; 
		const string ReservationEndPoint = "/Reservations";
		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public EventService(HttpClient httpClient, ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context; 
            _httpClient = httpClient;
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

        public async Task<Event> GetUpcomingEvent()
        {
            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .OrderBy(e => e.Date)
                .FirstOrDefaultAsync(x=>x.Date >= DateTime.Today);

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

		internal async Task<List<Event>> GetUpcomingEvents()
		{
            var events = await GetAllEvents();
            return new List<Event>(events.Where(x => x.Date >= DateTime.Today));
        }

		internal async Task<List<Event>> GetPastEvents()
		{
			var events = await GetAllEvents();
			return new List<Event>(events.Where(x => x.Date < DateTime.Today));
		}

		public async Task<List<EventTypeDTO>> GetEventTypes()
		{
			var response = await _httpClient.GetAsync(VenuesServiceBaseUrl + EventTypesEndPoint);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var types = JsonSerializer.Deserialize<List<EventTypeDTO>>(jsonResponse, jsonOptions);
				if (types == null)
				{
					throw new ArgumentNullException(nameof(response), "Event types were null");
				}
				return types;
			}

			return [];
		}

		public async Task<List<VenueDTO>> GetAvailableVenuesForEventDate(Event @event)
		{
			string type = @event.EventTypeId;
			string startDate = @event.Date.ToString("yyyy-MM-dd");
			string endDate = @event.Date.ToString("yyyy-MM-dd");
			var response = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{AvailabilityEndPoint}" +
			                                          $"?eventType={type}" +
			                                          $"&beginDate={startDate}" +
			                                          $"&endDate={endDate}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var venues = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);
				if (venues == null)
				{
					throw new ArgumentNullException(nameof(response), "Availabilities were null");
				}
				return venues;
			}

			return [];
		}

		public async Task<List<VenueDTO>> GetAllAvailableVenues(string eventType)
		{
			string type = eventType;
			string startDate = DateTime.Today.Date.AddYears(-100).ToString("yyyy-MM-dd");
			string endDate = DateTime.Today.Date.AddYears(100).ToString("yyyy-MM-dd");
			var response = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{AvailabilityEndPoint}" +
													  $"?eventType={type}" +
													  $"&beginDate={startDate}" +
													  $"&endDate={endDate}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var venues = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);
				if (venues == null)
				{
					throw new ArgumentNullException(nameof(response), "Availabilities were null");
				}
				return venues;
			}

			return [];
		}

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

		internal async Task<ReservationGetDTO> GetReservation(string reference)
		{
			var response = await _httpClient.GetAsync($"{VenuesServiceBaseUrl}{ReservationEndPoint}/" + reference);
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var reservationDto = JsonSerializer.Deserialize<ReservationGetDTO>(jsonResponse, jsonOptions);
				return reservationDto;
			}
			return new();
		}

		internal async Task DeleteReservation(string reservationId)
		{
			await _httpClient.DeleteAsync($"{VenuesServiceBaseUrl}{ReservationEndPoint}{reservationId}");
			var events = await GetAllEvents();
			var _event = events.FirstOrDefault(x => x.ReservationId == reservationId);
			if(_event != null)
			{
				_event.ReservationId = string.Empty;
				await UpdateEvent(_event);
			}
		}
	}
}
