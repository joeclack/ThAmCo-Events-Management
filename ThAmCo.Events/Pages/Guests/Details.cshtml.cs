namespace ThAmCo.Events.Pages.Guests
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="DetailsModel" />
	/// </summary>
	public class DetailsModel : PageModel
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
		/// Initializes a new instance of the <see cref="DetailsModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context = context;
			_eventService = serviceProvider.GetRequiredService<EventService>();
			_staffService = serviceProvider.GetRequiredService<StaffService>();
			_guestService = serviceProvider.GetRequiredService<GuestService>();
		}

		/// <summary>
		/// Gets or sets the Guest
		/// </summary>
		public Guest Guest { get; set; } = default!;

		/// <summary>
		/// Gets or sets the Bookings
		/// </summary>
		public List<GuestBooking> Bookings { get; set; } = [];

		/// <summary>
		/// Gets or sets the PastBookings
		/// </summary>
		public List<GuestBooking> PastBookings { get; set; } = [];

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

			var guest = await _guestService.GetGuest(id);
			if (guest == null)
			{
				return NotFound();
			}
			else
			{
				Guest        = guest;
				Bookings     = await _guestService.GetBookings(id);
				PastBookings = await _guestService.GetPastBookings(id);
			}
			return Page();
		}

		/// <summary>
		/// The OnPostViewEvent
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostViewEvent(int id)
		{
			if (_eventService.GetEvent(id) == null)
			{
				return NotFound();
			}
			return RedirectToPage("/Events/Details", new { id = id });
		}
	}
}
