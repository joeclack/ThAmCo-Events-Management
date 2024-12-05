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
    public class MenuFoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenuFoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/MenuFoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuFoodItem>>> GetMenuFoodItems()
        {
            return await _context.MenuFoodItems.ToListAsync();
        }

        //// GET: api/MenuFoodItems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MenuFoodItem>> GetMenuFoodItem(int id)
        //{
        //    var menuFoodItem = await _context.MenuFoodItems.FindAsync(id);

        //    if (menuFoodItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return menuFoodItem;
        //}

        // PUT: api/MenuFoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{menuId}/{foodItemId}")]
        public async Task<IActionResult> PutMenuFoodItem(int menuId, int foodItemId, MenuFoodItem menuFoodItem)
        {
            if (menuId != menuFoodItem.MenuId || foodItemId != menuFoodItem.FoodItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuFoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuFoodItemExists(menuId, foodItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MenuFoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{menuId}/{foodItemId}")]
        public async Task<ActionResult<MenuFoodItemDTO>> PostMenuFoodItem(int foodItemId, int menuId)
        {
            var mfi = new MenuFoodItem()
            {
                MenuId = menuId,
                FoodItemId = foodItemId,
                Menu = _context.Menus.FirstOrDefault(m => m.MenuId == menuId),
                FoodItem = _context.FoodItems.FirstOrDefault(fi => fi.FoodItemId == foodItemId)
            };
            _context.MenuFoodItems.Add(mfi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuFoodItemExists(menuId, foodItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenuFoodItem", new { id = menuId }, mfi);
        }

        // DELETE: api/MenuFoodItems/5
        [HttpDelete("{menuId}/{foodItemId}")]
        public async Task<IActionResult> DeleteMenuFoodItem(int menuId, int foodItemId)
        {
            var menuFoodItem = await _context.MenuFoodItems.FirstOrDefaultAsync(mfi => mfi.MenuId == menuId && mfi.FoodItemId == foodItemId);
            if (menuFoodItem == null)
            {
                return NotFound();
            }

            _context.MenuFoodItems.Remove(menuFoodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuFoodItemExists(int menuId, int foodItemId)
        {
            return _context.MenuFoodItems.Any(e => e.MenuId == menuId && e.FoodItemId == foodItemId);
        }
    }
}
