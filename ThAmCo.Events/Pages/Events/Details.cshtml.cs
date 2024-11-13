﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly ThAmCo.Events.Data.EventsDbContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
        {
            _eventService = serviceProvider.GetRequiredService<EventService>();
            _context = context;
        }

        public Event Event { get; set; } = default!;

        public bool IsUnderStaffed { get; set; }
        public int StaffRequiredForEvent { get; set; }
        public string UnderStaffAlertText { get; set; } = string.Empty;

        public bool IsEventUnderStaffed()
        {
            int staffAmount = Event.Staffings.Count;
            int guestBookingAmount = Event.GuestBookings.Count;
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
            }
            return Page();
        }
    }
}
