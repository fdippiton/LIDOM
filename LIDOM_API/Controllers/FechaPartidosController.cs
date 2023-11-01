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
    public class FechaPartidosController : ControllerBase
    {
        private readonly LidomContext _context;

        public FechaPartidosController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/FechaPartidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FechaPartido>>> GetFechaPartidos()
        {
          if (_context.FechaPartidos == null)
          {
              return NotFound();
          }
            return await _context.FechaPartidos.ToListAsync();
        }

        // GET: api/FechaPartidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FechaPartido>> GetFechaPartido(int id)
        {
          if (_context.FechaPartidos == null)
          {
              return NotFound();
          }
            var fechaPartido = await _context.FechaPartidos.FindAsync(id);

            if (fechaPartido == null)
            {
                return NotFound();
            }

            return fechaPartido;
        }

        // PUT: api/FechaPartidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFechaPartido(FechaPartido fechaPartido)
        {

            try
            {
                _context.FechaPartidos.Update(fechaPartido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/FechaPartidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FechaPartido>> PostFechaPartido(FechaPartido fechaPartido)
        {
          if (_context.FechaPartidos == null)
          {
              return Problem("Entity set 'LidomContext.FechaPartidos'  is null.");
          }
            _context.FechaPartidos.Add(fechaPartido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFechaPartido", new { id = fechaPartido.FecId }, fechaPartido);
        }

        // DELETE: api/FechaPartidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFechaPartido(int id)
        {
            if (_context.FechaPartidos == null)
            {
                return NotFound();
            }
            var fechaPartido = await _context.FechaPartidos.FindAsync(id);
            if (fechaPartido == null)
            {
                return NotFound();
            }

            _context.FechaPartidos.Remove(fechaPartido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FechaPartidoExists(int id)
        {
            return (_context.FechaPartidos?.Any(e => e.FecId == id)).GetValueOrDefault();
        }
    }
}
