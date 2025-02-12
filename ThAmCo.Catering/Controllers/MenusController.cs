﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<MenuPostDTO>>> GetMenus()
        {
            var menus = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .ThenInclude(m => m.FoodItem)
                .Include(f => f.FoodBookings).ToListAsync();

            if(menus == null)
            {
                return NotFound();
            }

            List<MenuPostDTO> meunDTOS = menus.Select(m => new MenuPostDTO().CreateDTO(m)).ToList();
            return meunDTOS;
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuPostDTO>> GetMenu(int id)
        {
            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .ThenInclude(m => m.FoodItem)
                .Include(f => f.FoodBookings)
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null)
            {
                return NotFound();
            }
			MenuPostDTO dto = new MenuPostDTO().CreateDTO(menu);

            return dto;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, MenuDTO Menu)
        {
            if (id != Menu.MenuId)
            {
                return BadRequest();
            }

            var menuToEdit = await _context.Menus.FindAsync(id);
            menuToEdit.MenuName = Menu.MenuName;

            _context.Entry(menuToEdit).State = EntityState.Modified;

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

            return Created();
        }

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuDTO>> CreateMenu(MenuDTO menu)
        {
            var newMenu = new MenuDTO().CreateModel(menu);

            try
            {
                _context.Menus.Add(newMenu);
                await _context.SaveChangesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return Created();
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
