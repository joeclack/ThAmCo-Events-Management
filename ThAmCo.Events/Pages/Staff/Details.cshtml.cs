using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly StaffService _staffService;
        private EventService _eventsService;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _eventsService = serviceProvider.GetRequiredService<EventService>();
        }

        public ThAmCo.Events.Models.Staff Staff { get; set; } = default!;
        public List<Event> Events { get; set; } = [];

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
                Staff = staff;
                Events = await _staffService.GetStaffMemberEvents(id);
            }
            return Page();
        }
    }
}
