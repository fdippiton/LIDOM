using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LIDOM_API.Models;
using LIDOM_API.ViewModels;

namespace LIDOM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly LidomContext _context;

        public PartidosController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Partidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidoViewModel>>> GetPartidos()
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            return await _context.Partidos
                .Include(x => x.ParEquipoGanadorNavigation)
                .Include(x => x.ParEquipoPerdedorNavigation)
                .Include(x => x.ParJuegoNavigation.CalEquipoLocalNavigation!)
                .Include(x => x.ParJuegoNavigation.CalEquipoLocalNavigation!.EqEstadioNavigation!)
                .Include(x => x.ParJuegoNavigation.CalEquipoVisitanteNavigation!)
                .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!)
                .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!)
                .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!.FecTemporadaNavigation!)
                .Select(match => new PartidoViewModel
                {
                    ParId = match.ParId,
                    ParJuego = match.ParJuegoNavigation.CalJuegoId,
                    FecId = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecId,
                    FecFechaPartido = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecFechaPartido,
                    FecHora = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecHora,
                    ParEquipoGanador = match.ParEquipoGanadorNavigation.EqNombre,
                    ParEquipoPerdedor = match.ParEquipoPerdedorNavigation.EqNombre,
                    TemNombre = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecTemporadaNavigation!.TemNombre,
                    EquipoLocal = match.ParJuegoNavigation.CalEquipoLocalNavigation!.EqNombre,
                    EquipoVisitante = match.ParJuegoNavigation.CalEquipoVisitanteNavigation!.EqNombre,
                    EstNombre = match.ParJuegoNavigation.CalEquipoLocalNavigation!.EqEstadioNavigation!.EstNombre,
                    ParEquipoGanadorId = match.ParEquipoPerdedorNavigation.EqId,
                    ParEquipoPerdedorId = match.ParEquipoPerdedorNavigation.EqId,

                }).ToListAsync();
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PartidoCalendarioViewModel>>> GetPartidoss()
        //{
        //    if (_context.Partidos == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Partidos
        //        .Include(x => x.ParEquipoGanadorNavigation)
        //        .Include(x => x.ParEquipoPerdedorNavigation)
        //        .Include(x => x.ParJuegoNavigation.CalEquipoLocalNavigation!)
        //        .Include(x => x.ParJuegoNavigation.CalEquipoLocalNavigation!.EqEstadioNavigation!)
        //        .Include(x => x.ParJuegoNavigation.CalEquipoVisitanteNavigation!)
        //        .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!)
        //        .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!)
        //        .Include(x => x.ParJuegoNavigation.CalFechaPartidoNavigation!.FecTemporadaNavigation!)
        //        .Select(match => new PartidoCalendarioViewModel
        //        {
        //            ParId = match.ParId,
        //            ParJuego = match.ParJuegoNavigation.CalJuegoId,
        //            FecId = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecId,
        //            FecFechaPartido = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecFechaPartido,
        //            FecHora = match.ParJuegoNavigation.CalFechaPartidoNavigation!.FecHora,
        //            ParEquipoGanador = match.ParEquipoGanadorNavigation.EqNombre,
        //            ParEquipoPerdedor = match.ParEquipoPerdedorNavigation.EqNombre,
        //            ParEquipoGanadorId = match.ParEquipoPerdedorNavigation.EqId,
        //            ParEquipoPerdedorId = match.ParEquipoPerdedorNavigation.EqId,

        //        }).ToListAsync();
        //}

        // GET: api/Partidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partido>> GetPartido(int id)
        {
          if (_context.Partidos == null)
          {
              return NotFound();
          }
            var partido = await _context.Partidos.FindAsync(id);

            if (partido == null)
            {
                return NotFound();
            }

            return partido;
        }

        // PUT: api/Partidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartido(int id, Partido partido)
        {
            if (id != partido.ParId)
            {
                return BadRequest();
            }

            _context.Entry(partido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidoExists(id))
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

        // POST: api/Partidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partido>> PostPartido(Partido partido)
        {
          if (_context.Partidos == null)
          {
              return Problem("Entity set 'LidomContext.Partidos'  is null.");
          }
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartido", new { id = partido.ParId }, partido);
        }

        // DELETE: api/Partidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(int id)
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartidoExists(int id)
        {
            return (_context.Partidos?.Any(e => e.ParId == id)).GetValueOrDefault();
        }
    }
}
