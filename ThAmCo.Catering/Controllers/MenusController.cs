using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.DTOs;
using ThAmCo.Catering.Models;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenusController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenus()
        {
            var menus = await _context.Menus.ToListAsync();

            if(menus == null)
            {
                return NotFound();
            }

            var meunDTOS = menus.Select(m => new MenuDTO().CreateDTO(m)).ToList();
            return meunDTOS;
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(int id)
        {
            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .Include(m => m.FoodBookings)
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null)
            {
                return NotFound();
            }

            return new MenuDTO().CreateDTO(menu);
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMenu(int id, MenuDTO menu)
        {
            if (id != menu.MenuId)
            {
                return BadRequest();
            }

            var menuToEdit = await _context.Menus.FindAsync(id);

            menuToEdit.MenuId = menu.MenuId;
            menuToEdit.MenuFoodItems = menu.MenuFoodItems;
            menuToEdit.MenuName = menu.MenuName;
            menuToEdit.FoodBookings = menu.FoodBookings;

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuDTO>> CreateMenu(MenuDTO menu)
        {
            var newMenu = new Menu()
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuFoodItems = menu.MenuFoodItems,
                FoodBookings = menu.FoodBookings
            };

            try
            {
                _context.Menus.Add(newMenu);
                await _context.SaveChangesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return CreatedAtAction("GetMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            try
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
