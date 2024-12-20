namespace ThAmCo.Events.Pages
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using ThAmCo.Events.DTOs;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="IndexModel" />
	/// </summary>
	public class IndexModel : PageModel
	{
		/// <summary>
		/// Defines the _logger
		/// </summary>
		private readonly ILogger<IndexModel> _logger;

		/// <summary>
		/// Defines the _eventsService
		/// </summary>
		private readonly EventService _eventsService;

		/// <summary>
		/// Gets or sets the UpcomingEvents
		/// </summary>
		public List<Event> UpcomingEvents { get; set; } = [];

		/// <summary>
		/// Gets or sets the PastEvents
		/// </summary>
		public List<Event> PastEvents { get; set; } = [];

		/// <summary>
		/// Gets or sets the EventTypes
		/// </summary>
		public List<EventTypeDTO> EventTypes { get; set; }

		/// <summary>
		/// Gets or sets the AvailableVenues
		/// </summary>
		public List<VenueDTO> AvailableVenues { get; set; } = [];

		/// <summary>
		/// Gets or sets the StartDate
		/// </summary>
		[BindProperty]
		public DateTime StartDate { get; set; } = DateTime.Today.Date;

		/// <summary>
		/// Gets or sets the EndDate
		/// </summary>
		[BindProperty]
		public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7).Date;

		/// <summary>
		/// Gets or sets the SelectedEventType
		/// </summary>
		[BindProperty]
		public string SelectedEventType { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexModel"/> class.
		/// </summary>
		/// <param name="logger">The logger<see cref="ILogger{IndexModel}"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public IndexModel(
			ILogger<IndexModel> logger,
			IServiceProvider serviceProvider)
		{
			_logger = logger;
			_eventsService = serviceProvider.GetRequiredService<EventService>();
		}

		/// <summary>
		/// The OnGetAsync
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		public async Task OnGetAsync()
		{
			await LoadEventTypes();
			UpcomingEvents = await _eventsService.GetUpcomingEvents();
			PastEvents = await _eventsService.GetPastCancelledEvents();
		}

		/// <summary>
		/// The OnPostSearchVenuesAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostSearchVenuesAsync()
		{
			await LoadEventTypes();

			try
			{
				AvailableVenues = await _eventsService.GetAvailableVenuesTimePeriod(
					SelectedEventType,
					StartDate,
					EndDate
				);

				UpcomingEvents = await _eventsService.GetUpcomingEvents();
				PastEvents = await _eventsService.GetPastCancelledEvents();

				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error searching for venues");
				ModelState.AddModelError(string.Empty, "Error searching for venues. Please try again.");
				return Page();
			}
		}

		/// <summary>
		/// The LoadEventTypes
		/// </summary>
		/// <returns>The <see cref="Task"/></returns>
		private async Task LoadEventTypes()
		{
			EventTypes = await _eventsService.GetEventTypes();
		}
	}
}
