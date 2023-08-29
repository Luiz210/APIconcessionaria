using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIconcessionaria.Models;
using static APIconcessionaria.Context.AppdbContext;

namespace Teste1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarrosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> GetCarros()
        {
            return await _context.Carros.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetCarro(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }
        [HttpPost]
        public async Task<ActionResult<Carro>> PostCarro(Carro carro)
        {
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarro), new { id = carro.Id }, carro);
        }

        [HttpPost("{carroId}/UploadImagem")]
        public async Task<IActionResult> UploadImagemCarro(int carroId, IFormFile file)
        {
            var carro = await _context.Carros.FindAsync(carroId);

            if (carro == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido");
            }
            var nomeArquivo = imageToByte(file);
            var imagemCarro = new ImagemCarro
            {
                NomeArquivo = nomeArquivo,
                CarroId = carroId
            };
             

            _context.Add(imagemCarro);
            await _context.SaveChangesAsync();

            return Ok("Imagem enviada com sucesso!");
        }
        [HttpGet("{carroId}/Imagem")]
        public async Task<IActionResult> GetImagemCarro(int carroId)
        {
            var imagemCarro = await _context.ImagensCarros.FirstOrDefaultAsync(ic => ic.CarroId == carroId);

            if (imagemCarro == null || imagemCarro.NomeArquivo == null)
            {
                return NotFound();
            }

            return File(imagemCarro.NomeArquivo, "image/jpeg");
        }


        private byte[] imageToByte(IFormFile img)
        {
            using (var ms = new MemoryStream())
            {
                img.CopyTo(ms);
                return ms.ToArray();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarro(int id, Carro carro)
        {
            if (id != carro.Id)
            {
                return BadRequest();
            }

            _context.Entry(carro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarroExists(id))
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
        public async Task<IActionResult> DeleteCarro(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }

            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CarroExists(int id)
        {
            return _context.Carros.Any(e => e.Id == id);
        }
    }
}