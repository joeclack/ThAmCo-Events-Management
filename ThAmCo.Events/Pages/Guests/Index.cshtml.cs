using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Guests
{
    public class IndexModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly EventService _eventService;
        private readonly StaffService _staffService;
        private readonly GuestService _guestService;
        public IndexModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context; 
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
        }

        public IList<Guest> Guests { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Guests = await _guestService.GetAllGuests();
        }

        public async Task<IActionResult> OnPostDeleteGuest(int guestId)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == guestId);
            await _guestService.DeleteGuest(guest);
            return Redirect($"../Guests");
        }
    }
}
