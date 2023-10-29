using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LIDOM_API.Models;

namespace LIDOM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioJuegosController : ControllerBase
    {
        private readonly LidomContext _context;

        public CalendarioJuegosController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/CalendarioJuegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarioJuego>>> GetCalendarioJuegos()
        {
          if (_context.CalendarioJuegos == null)
          {
              return NotFound();
          }
            return await _context.CalendarioJuegos.ToListAsync();
        }

        // GET: api/CalendarioJuegos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarioJuego>> GetCalendarioJuego(int id)
        {
          if (_context.CalendarioJuegos == null)
          {
              return NotFound();
          }
            var calendarioJuego = await _context.CalendarioJuegos.FindAsync(id);

            if (calendarioJuego == null)
            {
                return NotFound();
            }

            return calendarioJuego;
        }

        // PUT: api/CalendarioJuegos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarioJuego(int id, CalendarioJuego calendarioJuego)
        {
            if (id != calendarioJuego.CalJuegoId)
            {
                return BadRequest();
            }

            _context.Entry(calendarioJuego).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarioJuegoExists(id))
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

        // POST: api/CalendarioJuegos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalendarioJuego>> PostCalendarioJuego(CalendarioJuego calendarioJuego)
        {
          if (_context.CalendarioJuegos == null)
          {
              return Problem("Entity set 'LidomContext.CalendarioJuegos'  is null.");
          }
            _context.CalendarioJuegos.Add(calendarioJuego);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendarioJuego", new { id = calendarioJuego.CalJuegoId }, calendarioJuego);
        }

        // DELETE: api/CalendarioJuegos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarioJuego(int id)
        {
            if (_context.CalendarioJuegos == null)
            {
                return NotFound();
            }
            var calendarioJuego = await _context.CalendarioJuegos.FindAsync(id);
            if (calendarioJuego == null)
            {
                return NotFound();
            }

            _context.CalendarioJuegos.Remove(calendarioJuego);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarioJuegoExists(int id)
        {
            return (_context.CalendarioJuegos?.Any(e => e.CalJuegoId == id)).GetValueOrDefault();
        }
    }
}
