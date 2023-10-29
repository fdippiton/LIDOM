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
    public class ResultadoEquiposController : ControllerBase
    {
        private readonly LidomContext _context;

        public ResultadoEquiposController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/ResultadoEquipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadoEquipo>>> GetResultadoEquipos()
        {
          if (_context.ResultadoEquipos == null)
          {
              return NotFound();
          }
            return await _context.ResultadoEquipos.ToListAsync();
        }

        // GET: api/ResultadoEquipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultadoEquipo>> GetResultadoEquipo(int id)
        {
          if (_context.ResultadoEquipos == null)
          {
              return NotFound();
          }
            var resultadoEquipo = await _context.ResultadoEquipos.FindAsync(id);

            if (resultadoEquipo == null)
            {
                return NotFound();
            }

            return resultadoEquipo;
        }

        // PUT: api/ResultadoEquipos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultadoEquipo(int id, ResultadoEquipo resultadoEquipo)
        {
            if (id != resultadoEquipo.ResId)
            {
                return BadRequest();
            }

            _context.Entry(resultadoEquipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadoEquipoExists(id))
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

        // POST: api/ResultadoEquipos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResultadoEquipo>> PostResultadoEquipo(ResultadoEquipo resultadoEquipo)
        {
          if (_context.ResultadoEquipos == null)
          {
              return Problem("Entity set 'LidomContext.ResultadoEquipos'  is null.");
          }
            _context.ResultadoEquipos.Add(resultadoEquipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultadoEquipo", new { id = resultadoEquipo.ResId }, resultadoEquipo);
        }

        // DELETE: api/ResultadoEquipos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultadoEquipo(int id)
        {
            if (_context.ResultadoEquipos == null)
            {
                return NotFound();
            }
            var resultadoEquipo = await _context.ResultadoEquipos.FindAsync(id);
            if (resultadoEquipo == null)
            {
                return NotFound();
            }

            _context.ResultadoEquipos.Remove(resultadoEquipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultadoEquipoExists(int id)
        {
            return (_context.ResultadoEquipos?.Any(e => e.ResId == id)).GetValueOrDefault();
        }
    }
}
