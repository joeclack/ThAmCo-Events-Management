using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDbContext _context;
        private EventService _eventService;
		public List<EventTypeDTO> EventTypes { get; set; } = [];
		public List<VenueDTO> AllAvailableVenues { get; set; } = [];
        [BindProperty]
        public VenueDTO SelectedVenue { get; set; } = new();
		[BindProperty]
		public string SelectedVenueIdentifier { get; set;  }

		private void FindVenueFromIdentifier()
		{
			var parts = SelectedVenueIdentifier.Split('|');
			var selectedCode = parts[0];
			DateTime.TryParse(parts[1], out var selectedDate);
			
			SelectedVenue = AllAvailableVenues.FirstOrDefault(v => v.Code == parts[0] && v.Date == selectedDate);
		}

		public CreateModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider provider)
        {
            _context = context;
            _eventService = provider.GetRequiredService<EventService>();
        }

		public async Task<IActionResult> OnGetAsync(string? id)
		{
            if (id != null)
            {
                Event.EventTypeId = id;
    			AllAvailableVenues = await _eventService.GetAllAvailableVenues(id);
            }
			return Page();
		}

		[BindProperty]
        public Event Event { get; set; } = new();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string eventType)
        {
            if(eventType != null)
            {
			    AllAvailableVenues = await _eventService.GetAllAvailableVenues(eventType);
			    FindVenueFromIdentifier();
			
			    Event.EventTypeId = eventType;
			    Event.Date = SelectedVenue.Date;
                ReservationPostDTO resDTO = new ReservationPostDTO()
                {
                    EventDate = SelectedVenue.Date,
                    StaffId = "0",
                    VenueCode = SelectedVenue.Code
                };
                var result = await _eventService.CreateReservation(resDTO);
                Event.ReservationId = result;
            }
            await _eventService.CreateEvent(Event);
			return RedirectToPage("./Index");
        }
    }
}
