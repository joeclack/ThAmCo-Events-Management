using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Guests
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context; 
        private readonly EventService _eventService;
        private readonly StaffService _staffService;
        private readonly GuestService _guestService;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
        }

        public Guest Guest { get; set; } = default!;
        public List<GuestBooking> Bookings { get; set; } = [];
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuest(id);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                Guest = guest;
                Bookings = await _guestService.GetBookings(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostViewEvent(int id)
        {
            if(_eventService.GetEvent(id) == null)
            {
                return NotFound();
            }
            return RedirectToPage("/Events/Details", new { id = id });
        }
    }
}
