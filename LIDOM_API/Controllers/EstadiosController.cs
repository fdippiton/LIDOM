﻿using System;
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
    public class EstadiosController : ControllerBase
    {
        private readonly LidomContext _context;

        public EstadiosController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Estadios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estadio>>> GetEstadios()
        {
          if (_context.Estadios == null)
          {
              return NotFound();
          }
            return await _context.Estadios.ToListAsync();
        }

        // GET: api/Estadios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estadio>> GetEstadio(int id)
        {
          if (_context.Estadios == null)
          {
              return NotFound();
          }
            var estadio = await _context.Estadios.FindAsync(id);

            if (estadio == null)
            {
                return NotFound();
            }

            return estadio;
        }

        // PUT: api/Estadios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadio(int id, Estadio estadio)
        {
            if (id != estadio.EstId)
            {
                return BadRequest();
            }

            _context.Entry(estadio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadioExists(id))
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

        // POST: api/Estadios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estadio>> PostEstadio(Estadio estadio)
        {
          if (_context.Estadios == null)
          {
              return Problem("Entity set 'LidomContext.Estadios'  is null.");
          }
            _context.Estadios.Add(estadio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadio", new { id = estadio.EstId }, estadio);
        }

        // DELETE: api/Estadios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadio(int id)
        {
            if (_context.Estadios == null)
            {
                return NotFound();
            }
            var estadio = await _context.Estadios.FindAsync(id);
            if (estadio == null)
            {
                return NotFound();
            }

            _context.Estadios.Remove(estadio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadioExists(int id)
        {
            return (_context.Estadios?.Any(e => e.EstId == id)).GetValueOrDefault();
        }
    }
}
