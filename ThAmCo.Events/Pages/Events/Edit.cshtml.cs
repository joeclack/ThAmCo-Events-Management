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
		private readonly CateringService _cateringService;

		public List<MenuGetDTO> AvailableMenus { get; set; }

		[BindProperty]
        public int? SelectedStaffId { get; set; }
        public FoodBookingDTO FoodBooking { get; set; }
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
            _cateringService = serviceProvider.GetRequiredService<CateringService>();
		}
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var _event = await _eventService.GetEvent(id);
            if (_event == null)
            {
                return NotFound();
            }

            Event = _event;
            EventId = _event.EventId;
			AvailableMenus = await _cateringService.GetMenus();
			AvailableStaff = await _staffService.GetAvailableStaff(EventId);
            AvailableGuests = await _guestService.GetAvailableGuests(EventId);
            FoodBooking = await _cateringService.GetFoodBooking(Event.FoodBookingId);
            return Page();
        }

        public int NewMenuId { get; set; }

        public async Task<IActionResult> OnPostEditFoodBookingMenu(int foodBookingId, int menuId, int eventId)
        {
            var _event = await _eventService.GetEvent(eventId);

			FoodBooking = await _cateringService.GetFoodBooking(foodBookingId);
			if (FoodBooking.MenuId != menuId)
			{
				var menu = await _cateringService.GetMenuInfo(menuId);
				FoodBooking.MenuName = menu.MenuName;
				FoodBooking.MenuId = menu.MenuId;
				await _cateringService.UpdateFoodBooking(FoodBooking);
			}

			return Redirect($"../Events/Edit?id={eventId}");
		}
		public async Task<IActionResult> OnPostCreateFoodBookingAsync(int eventId, int menuId)
		{
			Event = await _eventService.GetEvent(eventId);
			var FoodBooking = new FoodBookingDTO();
			FoodBooking.NumberOfGuests = Event.GuestBookings.Count();
			FoodBooking.FoodBookingDate = Event.Date;
			FoodBooking.MenuId = menuId;

			var confirmationBookingId = await _cateringService.CreateFoodBooking(FoodBooking);
			if (confirmationBookingId == -1)
			{
				AvailableMenus = await _cateringService.GetMenus();
				return Page();
			}
			else
			{
				Event.FoodBookingId = confirmationBookingId;
				if (Event == null)
				{
					return Page();
				}

				_context.Attach(Event).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return Redirect($"../Events/Edit?id={eventId}");
			}
		}

		public async Task<IActionResult> OnPostAsync()
        {

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

        public async Task<IActionResult> OnPostCreateStaffing(int staffId, int eventId)
        {
            await _staffService.CreateStaffing(staffId, await _eventService.GetEvent(eventId));
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostRemoveStaffing(int staffId, int eventId)
        {
            await _staffService.CancelStaffing(staffId, eventId);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostCancelGuestBooking(int guestId, int eventId)
        {
            await _guestService.CancelGuestBooking(guestId, eventId);
            return Redirect($"../Events/Edit?id={eventId}");
        }

        public async Task<IActionResult> OnPostCreateGuestBooking(int guestId, int eventId)
        {
            await _guestService.CreateBooking(guestId, await _eventService.GetEvent(eventId));
            return Redirect($"../Events/Edit?id={eventId}");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}