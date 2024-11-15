using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Guests
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly EventService _eventService;
        private GuestService _guestService;

        public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
        }

        [BindProperty]
        public Guest Guest { get; set; } = default!;

        public List<Event> AvailableEvents { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest =  await _guestService.GetGuest(id);
            if (guest == null)
            {
                return NotFound();
            }
            Guest = guest;
            AvailableEvents = await _eventService.GetAvailableEventsForGuest(guest);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(Guest.GuestId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostCreateGuestBooking(int guestId, int eventId)
        {
            await _guestService.CreateBooking(guestId, await _eventService.GetEvent(eventId));
            return Redirect($"../Guests/Edit?id={guestId}");
        }

        public async Task<IActionResult> OnPostCancelGuestBooking(int guestId, int eventId)
        {
            await _guestService.CancelGuestBooking(guestId, eventId);
            return Redirect($"../Guests/Edit?id={guestId}");
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.GuestId == id);
        }
    }
}
