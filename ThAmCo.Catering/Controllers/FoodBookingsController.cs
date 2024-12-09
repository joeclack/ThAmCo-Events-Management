using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.DTOs;
using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodBookingsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodBookingDTO>>> GetFoodBookings()
        {
            var bookings = await _context.FoodBookings.ToListAsync();
            var dto = bookings.Select(b => new FoodBookingDTO().CreateDTO(b)).ToList();

            return dto; 
        }

        // GET: api/FoodBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodBookingDTO>> GetFoodBooking(int id)
        {
            var foodBooking = await _context.FoodBookings.FindAsync(id);

            if (foodBooking == null)
            {
                return NotFound();
            }

            return new FoodBookingDTO().CreateDTO(foodBooking);
        }

        // PUT: api/FoodBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditFoodBooking(int id, FoodBookingDTO foodBooking)
        {
            if (id != foodBooking.FoodBookingId)
            {
                return BadRequest();
            }

            var bookingToEdit = await _context.FoodBookings.FindAsync(id);
            if(bookingToEdit == null)
            {
                return NotFound();
            }

            bookingToEdit.ClientReferenceId = foodBooking.ClientReferenceId;
            bookingToEdit.MenuId = foodBooking.MenuId;
            bookingToEdit.FoodBookingDate = foodBooking.FoodBookingDate;
            bookingToEdit.NumberOfGuests = foodBooking.NumberOfGuests;

            _context.Entry(bookingToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            return NoContent();
        }

        // POST: api/FoodBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodBookingDTO>> CreateFoodBooking(FoodBookingDTO foodBooking)
        {
            var newFoodBooking = new FoodBooking()
            {
                FoodBookingId = foodBooking.FoodBookingId,
                MenuId = foodBooking.MenuId,
                NumberOfGuests = foodBooking.NumberOfGuests,
                ClientReferenceId = foodBooking.ClientReferenceId,
                FoodBookingDate = foodBooking.FoodBookingDate
            };
            try
            {
                _context.FoodBookings.Add(newFoodBooking);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var dto = new FoodBookingDTO().CreateDTO(newFoodBooking);

            return new ObjectResult(dto.FoodBookingId) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/FoodBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodBooking(int id)
        {
            var foodBooking = await _context.FoodBookings.FindAsync(id);
            if (foodBooking == null)
            {
                return NotFound();
            }

            try
            {
                _context.FoodBookings.Remove(foodBooking);
                await _context.SaveChangesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        private bool FoodBookingExists(int id)
        {
            return _context.FoodBookings.Any(e => e.FoodBookingId == id);
        }
    }
}
