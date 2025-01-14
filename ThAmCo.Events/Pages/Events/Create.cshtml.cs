namespace ThAmCo.Events.Pages.Events
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="CreateModel" />
	/// </summary>
	
	public class CreateModel : PageModel
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Defines the _eventService
		/// </summary>
		private EventService _eventService;
		
		/// <summary>
		/// Gets or sets the VenueCode
		/// </summary>
		[BindProperty]
		public string VenueCode { get; set; } = string.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="provider">The provider<see cref="IServiceProvider"/></param>
		public CreateModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider provider)
		{
			_context      = context;
			_eventService = provider.GetRequiredService<EventService>();
		}

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <param name="eventType">The eventType<see cref="string"/></param>
		/// <param name="code">The code<see cref="string"/></param>
		/// <param name="date">The date<see cref="string"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGetAsync(string eventType, string code, string date, string location)
		{

			if (eventType != null)
			{
				DateTime.TryParse(date, out var result);
				Event.EventTypeId = eventType;
				VenueCode         = code;
				Event.Date        = result;
				Event.Location    = location;
			}
		}

		/// <summary>
		/// Gets or sets the Event
		/// </summary>
		[BindProperty]
		public Event Event { get; set; } = new();

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			ReservationPostDTO resDTO = new ReservationPostDTO()
			{
				EventDate = Event.Date,
				StaffId   = "0",
				VenueCode = VenueCode
			};
			var result          = await _eventService.CreateReservation(resDTO);
			Event.ReservationId = result;

			await _eventService.CreateEvent(Event);
			return RedirectToPage("./Index");
		}
	}
}
