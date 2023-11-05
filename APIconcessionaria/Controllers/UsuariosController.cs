using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIconcessionaria.Models;
using static APIconcessionaria.Context.AppdbContext;
using APIconcessionaria.ViewModel;

namespace Teste1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
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
            return _context.Usuarios.Any(e => e.Id == id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioViewModel model)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Email == model.Email && u.Senha == model.Senha)
                .FirstOrDefaultAsync();

            if (usuario != null)
            {
                var usuarioComTipo = new
                {
                    Id = usuario.Id,
                    NomeUsuario = usuario.NomeUsuario,
                    Tipo = usuario.Tipo
                };

                if (!string.IsNullOrEmpty(model.Email))
                {
                    var usuarioComEmail = await _context.Usuarios
                        .Where(u => u.Email == model.Email)
                        .Select(u => new UsuarioComEmailViewModel { Id = u.Id })
                        .FirstOrDefaultAsync();

                    if (usuarioComEmail != null)
                    {
                        return Ok(new
                        {
                            Usuario = usuarioComTipo,
                            IdDoEmail = usuarioComEmail.Id
                        });
                    }
                }

                return Ok(usuarioComTipo);
            }

            return Unauthorized("Credenciais inválidas.");
        }

    }
}