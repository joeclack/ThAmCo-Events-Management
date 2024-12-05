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
    public class FoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItemDTO>>> GetFoodItems()
        {
            var items = await _context.FoodItems.ToListAsync();
            var dto = items.Select(fi => new FoodItemDTO().CreateDTO(fi)).ToList();

            return dto;
        }

        // GET: api/FoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDTO>> GetFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return new FoodItemDTO().CreateDTO(foodItem);
        }

        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditFoodItem(int id, FoodItemDTO foodItem)
        {
            if (id != foodItem.FoodItemId)
            {
                return BadRequest();
            }

            var foodItemToEdit = await _context.FoodItems.FindAsync(id);

            foodItemToEdit.Description = foodItem.Description;
            foodItemToEdit.Name = foodItem.Name;
            foodItemToEdit.UnitPrice = foodItem.UnitPrice;

            _context.Entry(foodItemToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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

        // POST: api/FoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodItemDTO>> CreateFoodItem(FoodItemDTO foodItem)
        {
            var newItem = new FoodItem()
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Description = foodItem.Description,
                UnitPrice = foodItem.UnitPrice,
                MenuFoodItems = foodItem.MenuFoodItems
            };

            try
            {
                _context.FoodItems.Add(newItem);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var dto = new FoodItemDTO().CreateDTO(newItem);

            return new ObjectResult(dto.FoodItemId) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            try
            {
                _context.FoodItems.Remove(foodItem);
                await _context.SaveChangesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemId == id);
        }
    }
}
