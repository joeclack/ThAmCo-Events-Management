namespace ThAmCo.Events.Pages.Events
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System.Runtime.InteropServices;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="IndexModel" />
	/// </summary>
	public class IndexModel : PageModel
	{
		/// <summary>
		/// Defines the _eventService
		/// </summary>
		private readonly EventService _eventService;

		/// <summary>
		/// Defines the _staffService
		/// </summary>
		private readonly StaffService _staffService;

		/// <summary>
		/// Defines the _guestService
		/// </summary>
		private readonly GuestService _guestService;

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		private readonly CateringService _cateringService;

		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public IndexModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context = context;
			_eventService = serviceProvider.GetRequiredService<EventService>();
			_staffService = serviceProvider.GetRequiredService<StaffService>();
			_guestService = serviceProvider.GetRequiredService<GuestService>();
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
		}

		/// <summary>
		/// Gets or sets the Events
		/// </summary>
		public IList<Event> Events { get; set; } = default!;
		public IList<Event> CancelledEvents { get; set; } = default!;
		public IList<Event> PastAndCancelledEvents { get; set; } = default!;


		/// <summary>
		/// Gets or sets the EventTypes
		/// </summary>
		public List<EventTypeDTO> EventTypes { get; set; } = [];

		/// <summary>
		/// Gets or sets the EventTypeId
		/// </summary>
		public string EventTypeId { get; set; }

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGetAsync()
		{
			Events = await _eventService.GetUpcomingEvents();
			EventTypes = await _eventService.GetEventTypes();
			PastAndCancelledEvents = await _eventService.GetPastCancelledEvents();
		}

		/// <summary>
		/// The OnPostCreateEventFromEventType
		/// </summary>
		/// <param name="eventTypeId">The eventTypeId<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateEventFromEventType(string eventTypeId)
		{
			return RedirectToPage($"./Create", new { id = eventTypeId });
		}

		/// <summary>
		/// The OnPostCancelEvent
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCancelEvent(int eventId)
		{
			if (eventId == 0)
			{
				return BadRequest();
			}
			var _event = await _eventService.GetEvent(eventId);
			await _eventService.CancelEvent(eventId);

			if (_event.FoodBookingId != -1)
			{
				var foodbooking = await _cateringService.GetFoodBooking(_event.FoodBookingId);
				if (foodbooking != null)
				{
					await _cateringService.DeleteFoodBooking(foodbooking.FoodBookingId);
				}
			}
			if (_event.ReservationId != string.Empty)
			{
				var reservation = await _eventService.GetReservation(_event.ReservationId);
				if (reservation != null)
				{
					await _eventService.DeleteReservation(reservation.Reference);
				}
			}

			return Redirect($"../Events");
		}
	}
}
