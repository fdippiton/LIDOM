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
    public class TemporadasController : ControllerBase
    {
        private readonly LidomContext _context;

        public TemporadasController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Temporadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temporada>>> GetTemporadas()
        {
          if (_context.Temporadas == null)
          {
              return NotFound();
          }
            return await _context.Temporadas.ToListAsync();
        }

        // GET: api/Temporadas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Temporada>> GetTemporada(int id)
        {
          if (_context.Temporadas == null)
          {
              return NotFound();
          }
            var temporada = await _context.Temporadas.FindAsync(id);

            if (temporada == null)
            {
                return NotFound();
            }

            return temporada;
        }

        // PUT: api/Temporadas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporada(int id, Temporada temporada)
        {
            if (id != temporada.TemId)
            {
                return BadRequest();
            }

            _context.Entry(temporada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporadaExists(id))
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

        // POST: api/Temporadas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Temporada>> PostTemporada(Temporada temporada)
        {
          if (_context.Temporadas == null)
          {
              return Problem("Entity set 'LidomContext.Temporadas'  is null.");
          }
            _context.Temporadas.Add(temporada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemporada", new { id = temporada.TemId }, temporada);
        }

        // DELETE: api/Temporadas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemporada(int id)
        {
            if (_context.Temporadas == null)
            {
                return NotFound();
            }
            var temporada = await _context.Temporadas.FindAsync(id);
            if (temporada == null)
            {
                return NotFound();
            }

            _context.Temporadas.Remove(temporada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemporadaExists(int id)
        {
            return (_context.Temporadas?.Any(e => e.TemId == id)).GetValueOrDefault();
        }
    }
}
