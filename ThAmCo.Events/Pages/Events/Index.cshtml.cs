﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly StaffService _staffService;
        private readonly GuestService _guestService;
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public IndexModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
        }

        public IList<Event> Events { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Events = await _eventService.GetAllEvents();
        }

        public async Task<IActionResult> OnPostDeleteEvent(int eventId)
        {
            var _event = await _context.Events.FirstOrDefaultAsync(m => m.EventId == eventId);
            await _eventService.CancelEvent(_event);
            return Redirect($"../Events");
        }
    }
}
