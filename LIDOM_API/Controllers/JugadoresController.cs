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
    public class JugadoresController : ControllerBase
    {
        private readonly LidomContext _context;

        public JugadoresController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Jugadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugadore>>> GetJugadores()
        {
          if (_context.Jugadores == null)
          {
              return NotFound();
          }
            return await _context.Jugadores.OrderBy(g => g.JugEquipo).ThenBy(g => g.JugEdad).ToListAsync();
        }

        // GET: api/Jugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugadore>> GetJugadore(int id)
        {
          if (_context.Jugadores == null)
          {
              return NotFound();
          }
            var jugadore = await _context.Jugadores.FindAsync(id);

            if (jugadore == null)
            {
                return NotFound();
            }

            return jugadore;
        }

        // PUT: api/Jugadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJugadore(int id, Jugadore jugadore)
        {
            if (id != jugadore.JugId)
            {
                return BadRequest();
            }

            _context.Entry(jugadore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadoreExists(id))
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

        // POST: api/Jugadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jugadore>> PostJugadore(Jugadore jugadore)
        {
          if (_context.Jugadores == null)
          {
              return Problem("Entity set 'LidomContext.Jugadores'  is null.");
          }
            _context.Jugadores.Add(jugadore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJugadore", new { id = jugadore.JugId }, jugadore);
        }

        // DELETE: api/Jugadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJugadore(int id)
        {
            if (_context.Jugadores == null)
            {
                return NotFound();
            }
            var jugadore = await _context.Jugadores.FindAsync(id);
            if (jugadore == null)
            {
                return NotFound();
            }

            _context.Jugadores.Remove(jugadore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JugadoreExists(int id)
        {
            return (_context.Jugadores?.Any(e => e.JugId == id)).GetValueOrDefault();
        }
    }
}
