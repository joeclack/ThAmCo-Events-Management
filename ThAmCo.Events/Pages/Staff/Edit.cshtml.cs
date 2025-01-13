namespace ThAmCo.Events.Pages.Staff
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
		/// Defines the _staffService
		/// </summary>
		private readonly StaffService _staffService;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context      = context;
			_eventService = serviceProvider.GetRequiredService<EventService>();
			_staffService = serviceProvider.GetRequiredService<StaffService>();
		}

		/// <summary>
		/// Gets or sets the Staff
		/// </summary>
		[BindProperty]
		public ThAmCo.Events.Models.Staff Staff { get; set; } = default!;

		/// <summary>
		/// Gets or sets the AvailableEvents
		/// </summary>
		public List<Event> AvailableEvents { get; set; } = [];

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

			var staff = await _context.Staff.FirstOrDefaultAsync(m => m.StaffId == id);
			if (staff == null)
			{
				return NotFound();
			}
			Staff = staff;
			AvailableEvents = await _eventService.GetAvailableEventsForStaffMember(staff);

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

			_context.Attach(Staff).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StaffExists(Staff.StaffId))
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
		/// The OnPostCreateStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCreateStaffing(int staffId, int eventId)
		{
			var @event = await _eventService.GetEvent(eventId);
			await _staffService.CreateStaffing(staffId, @event);
			return Redirect($"../Staff/Edit?id={staffId}");
		}

		/// <summary>
		/// The OnPostCancelStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostCancelStaffing(int staffId, int eventId)
		{
			await _staffService.CancelStaffing(staffId, eventId);
			return Redirect($"../Staff/Edit?id={staffId}");
		}

		/// <summary>
		/// The StaffExists
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="bool"/></returns>
		private bool StaffExists(int id)
		{
			return _context.Staff.Any(e => e.StaffId == id);
		}
	}
}
