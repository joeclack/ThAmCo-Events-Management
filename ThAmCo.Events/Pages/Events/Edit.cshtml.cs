namespace ThAmCo.Events.Pages.Events
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.EntityFrameworkCore;
	using System;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="EditModel" />
	/// </summary>

	public class EditModel : PageModel
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

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
		/// Gets or sets the AvailableMenus
		/// </summary>
		public List<MenuGetDTO> AvailableMenus { get; set; } = [];

		/// <summary>
		/// Gets or sets the EventTypes
		/// </summary>
		public List<EventTypeDTO> EventTypes { get; set; } = [];

		/// <summary>
		/// Gets or sets the SelectedStaffId
		/// </summary>
		[BindProperty]
		public int? SelectedStaffId { get; set; }

		/// <summary>
		/// Gets or sets the FoodBooking
		/// </summary>
		public FoodBookingDTO FoodBooking { get; set; }

		/// <summary>
		/// Gets or sets the SelectedGuestId
		/// </summary>
		[BindProperty]
		public int? SelectedGuestId { get; set; }

		/// <summary>
		/// Gets or sets the AvailableStaff
		/// </summary>
		public List<ThAmCo.Events.Models.Staff> AvailableStaff { get; set; } = [];

		/// <summary>
		/// Gets or sets the AvailableGuests
		/// </summary>
		public List<Guest> AvailableGuests { get; set; } = [];

		/// <summary>
		/// Gets or sets the AvailableVenuesForEventDate
		/// </summary>
		public List<VenueDTO> AvailableVenuesForEventDate { get; set; } = [];

		/// <summary>
		/// Gets or sets the AllAvailableVenues
		/// </summary>
		public List<VenueDTO> AllAvailableVenues { get; set; } = [];

		/// <summary>
		/// Gets or sets the Event
		/// </summary>
		[BindProperty]
		public Event Event { get; set; } = default!;

		/// <summary>
		/// Gets or sets the EventId
		/// </summary>
		public int EventId { get; set; }

		/// <summary>
		/// Gets or sets the SelectedMenuId
		/// </summary>
		public string SelectedMenuId { get; set; }

		/// <summary>
		/// Gets or sets the Reservation
		/// </summary>
		public ReservationGetDTO Reservation { get; set; } = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="EditModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context         = context;
			_eventService    = serviceProvider.GetRequiredService<EventService>();
			_staffService    = serviceProvider.GetRequiredService<StaffService>();
			_guestService    = serviceProvider.GetRequiredService<GuestService>();
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
		}

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnGetAsync(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var _event = await _eventService.GetEvent(id);
			if (_event == null)
			{
				return NotFound();
			}

			Event   = _event;
			EventId = _event.EventId;

			AvailableMenus              = await _cateringService.GetMenus();
			AvailableStaff              = await _staffService.GetAvailableStaff(Event);
			AvailableGuests             = await _guestService.GetAvailableGuests(Event);
			FoodBooking                 = await _cateringService.GetFoodBooking(Event.FoodBookingId);
			EventTypes                  = await _eventService.GetEventTypes();
			AvailableVenuesForEventDate = await _eventService.GetAvailableVenuesForEventDate(Event);
			AllAvailableVenues          = await _eventService.GetAllAvailableVenues(Event.EventTypeId);
			Reservation                 = await _eventService.GetReservation(Event.ReservationId);
			return Page();
		}

		/// <summary>
		/// Gets or sets the NewMenuId
		/// </summary>
		public int NewMenuId { get; set; }

		/// <summary>
		/// The OnPostEditFoodBookingMenu
		/// </summary>
		/// <param name="foodBookingId">The foodBookingId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostEditFoodBookingMenu(int foodBookingId, int menuId, int eventId)
		{
			var _event = await _eventService.GetEvent(eventId);

			FoodBooking             = await _cateringService.GetFoodBooking(foodBookingId);
			if (FoodBooking.MenuId != menuId)
			{
				var menu             = await _cateringService.GetMenuInfo(menuId);
				FoodBooking.MenuName = menu.MenuName;
				FoodBooking.MenuId   = menu.MenuId;
				await _cateringService.UpdateFoodBooking(FoodBooking);
			}

			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostDeleteReservation
		/// </summary>
		/// <param name="reservationId">The reservationId<see cref="string"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteReservation(string reservationId, int eventId)
		{
			await _eventService.DeleteReservation(reservationId);
			return RedirectToPage($"../Events/Edit", new { id = eventId });
		}

		/// <summary>
		/// The OnPostCreateReservation
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="eventDate">The eventDate<see cref="DateTime"/></param>
		/// <param name="venueCode">The venueCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateReservation(int eventId, DateTime eventDate, string venueCode)
		{
			ReservationPostDTO reservationPostDTO = new() { EventDate = eventDate, StaffId = "0", VenueCode = venueCode };
			string reference                      = await _eventService.CreateReservation(reservationPostDTO);
			Event                                 = await _eventService.GetEvent(eventId);
			Event.ReservationId                   = reference;
			await _eventService.UpdateEvent(Event);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostCreateFoodBookingAsync
		/// </summary>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateFoodBookingAsync(int eventId, int menuId)
		{
			Event                       = await _eventService.GetEvent(eventId);
			var FoodBooking             = new FoodBookingDTO();
			FoodBooking.NumberOfGuests  = Event.GuestBookings.Count();
			FoodBooking.FoodBookingDate = Event.Date;
			FoodBooking.MenuId          = menuId;

			var confirmationBookingId = await _cateringService.CreateFoodBooking(FoodBooking);
			if (confirmationBookingId == -1)
			{
				AvailableMenus = await _cateringService.GetMenus();
				return Page();
			}
			else
			{
				Event.FoodBookingId = confirmationBookingId;
				if (Event == null)
				{
					return Page();
				}

				_context.Attach(Event).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return Redirect($"../Events/Edit?id={eventId}");
			}
		}

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{

			if (Event == null)
			{
				return Page();
			}

			_context.Attach(Event).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EventExists(Event.EventId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Redirect($"../Events/Edit?id={Event.EventId}");
		}

		/// <summary>
		/// The OnPostCreateStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateStaffing(int staffId, int eventId)
		{
			await _staffService.CreateStaffing(staffId, await _eventService.GetEvent(eventId));
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostRemoveStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostRemoveStaffing(int staffId, int eventId)
		{
			await _staffService.CancelStaffing(staffId, eventId);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostCancelGuestBooking
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCancelGuestBooking(int guestId, int eventId)
		{
			await _guestService.CancelGuestBooking(guestId, eventId);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostCancelFoodBooking
		/// </summary>
		/// <param name="foodBookingId">The foodBookingId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCancelFoodBooking(int foodBookingId, int eventId)
		{
			await _cateringService.DeleteFoodBooking(foodBookingId);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostCreateGuestBooking
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateGuestBooking(int guestId, int eventId)
		{
			await _guestService.CreateBooking(guestId, await _eventService.GetEvent(eventId));
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The EventExists
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="bool"/></returns>
		private bool EventExists(int id)
		{
			return _context.Events.Any(e => e.EventId == id);
		}

		/// <summary>
		/// The OnPostUpdateGuestAttendance
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="didAttend">The didAttend<see cref="bool"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostUpdateGuestAttendance(int guestId, int eventId, bool didAttend)
		{
			await _guestService.UpdateGuestAttendance(guestId, eventId, didAttend);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// The OnPostUpdateStaffAttendance
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="didAttend">The didAttend<see cref="bool"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostUpdateStaffAttendance(int staffId, int eventId, bool didAttend)
		{
			await _staffService.UpdateStaffAttendance(staffId, eventId, didAttend);
			return Redirect($"../Events/Edit?id={eventId}");
		}

		/// <summary>
		/// Gets a value indicating whether IsEventEditable
		/// </summary>
		public bool IsEventEditable => !Event.IsCanceled && Event.Date > DateTime.Now;
	}
}
