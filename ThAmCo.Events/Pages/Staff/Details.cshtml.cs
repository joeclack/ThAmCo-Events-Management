namespace ThAmCo.Events.Pages.Staff
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System;
	using System.Collections.Generic;
	using System.Linq;
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
		/// Defines the _staffService
		/// </summary>
		private readonly StaffService _staffService;

		/// <summary>
		/// Defines the _eventsService
		/// </summary>
		private EventService _eventsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="DetailsModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context       = context;
			_staffService  = serviceProvider.GetRequiredService<StaffService>();
			_eventsService = serviceProvider.GetRequiredService<EventService>();
		}

		/// <summary>
		/// Gets or sets the Staff
		/// </summary>
		public ThAmCo.Events.Models.Staff Staff { get; set; } = default!;

		/// <summary>
		/// Gets or sets the UpcomingEvents
		/// </summary>
		public List<Event> UpcomingEvents { get; set; } = [];

		/// <summary>
		/// Gets or sets the PastEvents
		/// </summary>
		public List<Event> PastEvents { get; set; } = [];

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

			var staff = await _staffService.GetStaffMember(id);
			if (staff == null)
			{
				return NotFound();
			}
			else
			{
				Staff          = staff;
				var events     = await _staffService.GetStaffMemberEvents(id);
				UpcomingEvents = events.Where(e => e.Date >= DateTime.Today && !e.IsCanceled).ToList();
				PastEvents     = events.Where(e => e.Date < DateTime.Today || !e.IsCanceled).ToList();
			}
			return Page();
		}
	}
}
