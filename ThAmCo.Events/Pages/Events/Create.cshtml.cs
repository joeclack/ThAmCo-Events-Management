using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private EventService _eventService;
		public List<EventTypeDTO> EventTypes { get; set; } = [];

		public CreateModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider provider)
        {
            _context = context;
            _eventService = provider.GetRequiredService<EventService>();
        }

        public async Task<IActionResult> OnGet()
        {
            EventTypes = await _eventService.GetEventTypes();
            return Page();
		}

        [BindProperty]
        public Event Event { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _eventService.CreateEvent(Event);

            return RedirectToPage("./Index");
        }
    }
}
