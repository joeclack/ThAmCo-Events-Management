using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NHibernate.Mapping;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly StaffService _staffService;
        private readonly GuestService _guestService;
        private readonly CateringService _cateringService;
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public IndexModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _staffService = serviceProvider.GetRequiredService<StaffService>();
            _guestService = serviceProvider.GetRequiredService<GuestService>();
			_cateringService = serviceProvider.GetRequiredService<CateringService>();
		}

        public IList<Event> Events { get;set; } = default!;
		public List<EventTypeDTO> EventTypes { get; set; } = [];
        public string EventTypeId { get; set; }

		public async Task OnGetAsync()
        {
            Events = await _eventService.GetAllEvents();
			EventTypes = await _eventService.GetEventTypes();
		}

        public async Task<IActionResult> OnPostCreateEventFromEventType(string eventTypeId)
        {
            return RedirectToPage($"./Create", new { id = eventTypeId });
        }

		public async Task<IActionResult> OnPostCancelEvent(int eventId)
        {
            var _event = await _eventService.GetEvent(eventId);
			await _eventService.CancelEvent(eventId);

			if (_event.FoodBookingId != -1)
            {
                var foodbooking = await _cateringService.GetFoodBooking(_event.FoodBookingId);
                if(foodbooking != null)
                {
                    await _cateringService.DeleteFoodBooking(foodbooking.FoodBookingId);
                }
            }
            if(_event.ReservationId != string.Empty)
            {
                var reservation = await _eventService.GetReservation(_event.ReservationId);
                if(reservation !=null)
                {
                    await _eventService.DeleteReservation(reservation.Reference);
                }
            }

			return Redirect($"../Events");
        }
    }
}
