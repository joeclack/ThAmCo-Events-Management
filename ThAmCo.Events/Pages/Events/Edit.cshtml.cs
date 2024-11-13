using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;
using ThAmCo.Events.DTOs;
using Microsoft.Extensions.Logging;

namespace ThAmCo.Events.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly EventService _eventService;
        private readonly StaffService _staffService;
        private readonly GuestService _guestService;
        [BindProperty]
        public int? SelectedStaffId { get; set; }

        [BindProperty]
        public int? SelectedGuestId { get; set; }
        public List<ThAmCo.Events.Models.Staff> AvailableStaff { get; set; } = [];
        public List<Guest> AvailableGuests { get; set; } = [];

        //[BindProperty]
        //public int EventId { get; set; }
        [BindProperty]
        public Event Event { get; set; } = default!;
        public int EventId { get; set; }

        public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == id);


            if (_event == null)
            {
                return NotFound();
            }

            EventId = _event.EventId;
            Event = _event;

            AvailableStaff = await _staffService.GetAvailableStaff(Event.EventId);
            AvailableGuests = await _guestService.GetAvailableGuests(Event.EventId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Event == null)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EventId))
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

        public async Task<IActionResult> OnPostAddStaff(int staffId, int eventId)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.StaffId == staffId);
            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == eventId);
            await _eventService.AddStaffToEventStaffing(staff, _event);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostRemoveStaff(int staffId, int eventId)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.StaffId == staffId);
            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == eventId);
            await _eventService.RemoveStaffFromEventStaffing(staff, _event);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostCancelGuestBooking(int guestId, int eventId)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == guestId);
            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == eventId);
            await _eventService.CancelGuestBooking(guest, _event);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostAddGuest(int guestId, int eventId)
        {
            var guest = await _context.Guests
                .Include(g => g.GuestBookings) 
                .FirstOrDefaultAsync(x => x.GuestId == guestId);


            var _event = await _context.Events
                .Include(e => e.Staffings)
                    .ThenInclude(s => s.Staff)
                .Include(e => e.GuestBookings)
                    .ThenInclude(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventId == eventId);
            await _eventService.AddGuestToEventGuests(guest, _event);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}