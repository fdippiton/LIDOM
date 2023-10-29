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
    public class PosicionesController : ControllerBase
    {
        private readonly LidomContext _context;

        public PosicionesController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Posiciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posicione>>> GetPosiciones()
        {
          if (_context.Posiciones == null)
          {
              return NotFound();
          }
            return await _context.Posiciones.ToListAsync();
        }

        // GET: api/Posiciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Posicione>> GetPosicione(int id)
        {
          if (_context.Posiciones == null)
          {
              return NotFound();
          }
            var posicione = await _context.Posiciones.FindAsync(id);

            if (posicione == null)
            {
                return NotFound();
            }

            return posicione;
        }

        // PUT: api/Posiciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosicione(int id, Posicione posicione)
        {
            if (id != posicione.PosId)
            {
                return BadRequest();
            }

            _context.Entry(posicione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosicioneExists(id))
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

        // POST: api/Posiciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Posicione>> PostPosicione(Posicione posicione)
        {
          if (_context.Posiciones == null)
          {
              return Problem("Entity set 'LidomContext.Posiciones'  is null.");
          }
            _context.Posiciones.Add(posicione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosicione", new { id = posicione.PosId }, posicione);
        }

        // DELETE: api/Posiciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosicione(int id)
        {
            if (_context.Posiciones == null)
            {
                return NotFound();
            }
            var posicione = await _context.Posiciones.FindAsync(id);
            if (posicione == null)
            {
                return NotFound();
            }

            _context.Posiciones.Remove(posicione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PosicioneExists(int id)
        {
            return (_context.Posiciones?.Any(e => e.PosId == id)).GetValueOrDefault();
        }
    }
}
