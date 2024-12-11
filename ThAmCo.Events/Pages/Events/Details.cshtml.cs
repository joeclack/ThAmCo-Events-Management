using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHibernate.Hql.Ast.ANTLR.Tree;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
		public CateringService _cateringService;

		public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _cateringService = serviceProvider.GetRequiredService<CateringService>();
			_context = context;
        }
        [BindProperty]
        public Event Event { get; set; } = default!;
		public List<MenuGetDTO> AvailableMenus { get; set; }

		public bool FirstAiderPresent { get; set; }
        public bool IsUnderStaffed { get; set; }
        public int StaffRequiredForEvent { get; set; }
        public string UnderStaffAlertText { get; set; } = string.Empty;
        public MenuInfoDTO FoodBookingMenuInfo { get; set; }

        public bool IsEventUnderStaffed()
        {
            int staffAmount = Event.Staffings.Count;
            int guestBookingAmount = Event.GuestBookings.Count;
            if (guestBookingAmount == 0)
            {
                return false;
            }
            int rem = (guestBookingAmount/10) % 10;
            StaffRequiredForEvent = rem + 1;
            if(staffAmount <= StaffRequiredForEvent) 
            {
                if(StaffRequiredForEvent == 1)
                {
                    UnderStaffAlertText = $"This event is under staffed. {StaffRequiredForEvent} staff member is required for this event";
                }
                else
                {
                    UnderStaffAlertText = $"This event is under staffed. {StaffRequiredForEvent} staff members are required for this event";
                }
                return true;
            }
            
            return false;
        }
        
        public bool IsFirstAiderPresent()
        {
            return Event.Staffings.Any(x=>x.Staff.IsFirstAider);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _eventService.GetEvent(id);
            if (_event == null)
            {
                return NotFound();
            }
            else
            {
                Event = _event;
                IsUnderStaffed = IsEventUnderStaffed();
                FirstAiderPresent = IsFirstAiderPresent();
                if(Event.FoodBookingId != -1)
                {
                    FoodBookingMenuInfo = await _cateringService.FetchMenuInfoForBooking(Event.FoodBookingId);
                }
                
				AvailableMenus = await _cateringService.GetMenus();
			}
			return Page();
        }

		
	}
}
