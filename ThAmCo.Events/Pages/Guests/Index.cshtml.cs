namespace ThAmCo.Events.Pages.Guests
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="IndexModel" />
	/// </summary>
	public class IndexModel : PageModel
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
		/// Initializes a new instance of the <see cref="IndexModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public IndexModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context      = context;
			_eventService = serviceProvider.GetRequiredService<EventService>();
			_staffService = serviceProvider.GetRequiredService<StaffService>();
			_guestService = serviceProvider.GetRequiredService<GuestService>();
		}

		/// <summary>
		/// Gets or sets the Guests
		/// </summary>
		public IList<Guest> Guests { get; set; } = default!;

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGetAsync()
		{
			Guests = await _guestService.GetAllGuests();
		}

		/// <summary>
		/// The OnPostDeleteGuest
		/// </summary>
		/// <param name="guestId">The guestId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostDeleteGuest(int guestId)
		{
			await _guestService.SoftDeleteGuest(guestId);
			return Redirect($"../Guests");
		}
	}
}
