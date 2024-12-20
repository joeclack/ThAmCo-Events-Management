namespace ThAmCo.Events.Pages.Events
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="DetailsModel" />
	/// </summary>
	public class DetailsModel : PageModel
	{
		/// <summary>
		/// Defines the _eventService
		/// </summary>
		private readonly EventService _eventService;

		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Defines the _cateringService
		/// </summary>
		public CateringService _cateringService;

		/// <summary>
		/// Initializes a new instance of the <see cref="DetailsModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_eventService    = serviceProvider.GetRequiredService<EventService>();
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
			_context         = context;
		}

		/// <summary>
		/// Gets or sets the Event
		/// </summary>
		[BindProperty]
		public Event Event { get; set; } = default!;

		/// <summary>
		/// Gets or sets the AvailableMenus
		/// </summary>
		public List<MenuGetDTO> AvailableMenus { get; set; }

		/// <summary>
		/// Gets or sets the Reservation
		/// </summary>
		public ReservationGetDTO Reservation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether FirstAiderPresent
		/// </summary>
		public bool FirstAiderPresent { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether IsUnderStaffed
		/// </summary>
		public bool IsUnderStaffed { get; set; }

		/// <summary>
		/// Gets or sets the StaffRequiredForEvent
		/// </summary>
		public int StaffRequiredForEvent { get; set; }

		/// <summary>
		/// Gets or sets the UnderStaffAlertText
		/// </summary>
		public string UnderStaffAlertText { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the FoodBookingMenuInfo
		/// </summary>
		public MenuInfoDTO FoodBookingMenuInfo { get; set; }

		/// <summary>
		/// The IsEventUnderStaffed
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		public bool IsEventUnderStaffed()
		{
			int staffAmount        = Event.Staffings.Count;
			int guestBookingAmount = Event.GuestBookings.Count;
			if (guestBookingAmount == 0)
			{
				return false;
			}
			int rem               = (guestBookingAmount / 10) % 10;
			StaffRequiredForEvent = rem + 1;
			if (staffAmount      <= StaffRequiredForEvent)
			{
				if (StaffRequiredForEvent == 1)
				{
					UnderStaffAlertText = $"This event is under staffed. {StaffRequiredForEvent} staff member is required for this event";
				}
				else
				{
					UnderStaffAlertText = $"This event is under staffed. {StaffRequiredForEvent} staff members are required for this event";
				}
				return true;
			}

			return false;
		}

		/// <summary>
		/// The IsFirstAiderPresent
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		public bool IsFirstAiderPresent()
		{
			return Event.Staffings.Any(x => x.Staff.IsFirstAider);
		}

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <param name="id">The id<see cref="int?"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var _event = await _eventService.GetEvent(id);
			if (_event == null)
			{
				return NotFound();
			}
			else
			{
				Event                    = _event;
				IsUnderStaffed           = IsEventUnderStaffed();
				FirstAiderPresent        = IsFirstAiderPresent();
				if (Event.FoodBookingId != -1)
				{
					FoodBookingMenuInfo = await _cateringService.FetchMenuInfoForBooking(Event.FoodBookingId);
				}
				Reservation    = await _eventService.GetReservation(Event.ReservationId);
				AvailableMenus = await _cateringService.GetMenus();
			}
			return Page();
		}
	}
}
