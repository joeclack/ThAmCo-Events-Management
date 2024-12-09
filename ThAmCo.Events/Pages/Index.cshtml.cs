using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EventService _eventsService;
        public Event UpcomingEvent { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _eventsService = serviceProvider.GetRequiredService<EventService>();
        }

        public async Task OnGet()
        {
            UpcomingEvent = await _eventsService.GetUpcomingEvent();
        }
    }
}