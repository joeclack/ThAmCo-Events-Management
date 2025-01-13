namespace ThAmCo.Events.Pages.Guests
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="EditModel" />
	/// </summary>
	[Authorize(Roles = "Manager")]
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
		/// Defines the _guestService
		/// </summary>
		private GuestService _guestService;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context      = context;
			_eventService = serviceProvider.GetRequiredService<EventService>();
			_guestService = serviceProvider.GetRequiredService<GuestService>();
		}

		/// <summary>
		/// Gets or sets the Guest
		/// </summary>
		[BindProperty]
		public Guest Guest { get; set; } = default!;

		/// <summary>
		/// Gets or sets the AvailableEvents
		/// </summary>
		public List<Event> AvailableEvents { get; set; } = [];

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnGetAsync(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var guest = await _guestService.GetGuest(id);
			if (guest == null)
			{
				return NotFound();
			}
			Guest           = guest;
			AvailableEvents = await _eventService.GetAvailableEventsForGuest(guest);
			return Page();
		}

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Guest).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GuestExists(Guest.GuestId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
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
			return Redirect($"../Guests/Edit?id={guestId}");
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
			return Redirect($"../Guests/Edit?id={guestId}");
		}

		/// <summary>
		/// The GuestExists
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="bool"/></returns>
		private bool GuestExists(int id)
		{
			return _context.Guests.Any(e => e.GuestId == id);
		}
	}
}
