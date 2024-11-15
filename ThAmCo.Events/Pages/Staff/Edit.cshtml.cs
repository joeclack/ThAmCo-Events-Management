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

namespace ThAmCo.Events.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private readonly EventService _eventService;
        private readonly StaffService _staffService;

        public EditModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
        }

        [BindProperty]
        public ThAmCo.Events.Models.Staff Staff { get; set; } = default!;
        public List<Event> AvailableEvents { get; set; } = [];
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff =  await _context.Staff.FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }
            Staff = staff;
            AvailableEvents = await _eventService.GetAvailableEventsForStaffMember(staff);

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

            _context.Attach(Staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(Staff.StaffId))
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

        public async Task<IActionResult> OnPostCreateStaffing(int staffId, int eventId)
        {
            var @event = await _eventService.GetEvent(eventId);
            await _staffService.CreateStaffing(staffId, @event);
            return Redirect($"../Staff/Edit?id={staffId}");
        }

        public async Task<IActionResult> OnPostCancelStaffing(int staffId, int eventId)
        {
            await _staffService.CancelStaffing(staffId, eventId);
            return Redirect($"../Staff/Edit?id={staffId}");
        }

        
        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
