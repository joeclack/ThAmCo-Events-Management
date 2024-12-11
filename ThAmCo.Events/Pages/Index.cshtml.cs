using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EventService _eventsService;
        public List<Event> UpcomingEvents { get; set; } = [];
        public List<Event> PastEvents { get; set; } = [];
        public List<EventTypeDTO> EventTypes { get; set; } = [];
		public IndexModel(ILogger<IndexModel> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _eventsService = serviceProvider.GetRequiredService<EventService>();
        }

        public async Task OnGet()
        {
            UpcomingEvents = await _eventsService.GetUpcomingEvents();
            PastEvents = await _eventsService.GetPastEvents();
            EventTypes = await _eventsService.GetEventTypes();
		}
    }
}