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
    public class UsuariosController : ControllerBase
    {
        private readonly LidomContext _context;

        public UsuariosController(LidomContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [Route("/O")]
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUsuario(string us, string pass)
        {
            Usuario authUser = _context.Usuarios.Where(x => x.UsuNombre == us && x.UsuPassword == pass).FirstOrDefault();

            if (authUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "ha ocurrido un error");
            }

            return StatusCode(StatusCodes.Status200OK, authUser);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //
        //public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        //{
        //  if (_context.Usuarios == null)
        //  {
        //      return Problem("Entity set 'LidomContext.Usuarios'  is null.");
        //  }
        //    _context.Usuarios.Add(usuario);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUsuario", new { id = usuario.UsuId }, usuario);
        //}

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuId == id)).GetValueOrDefault();
        }
    }
}
