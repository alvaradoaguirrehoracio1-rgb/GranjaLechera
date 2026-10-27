using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposAnalisisController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public TiposAnalisisController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAnalisis>>> GetTiposAnalisis()
        {
            return await _context.TiposAnalisis.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoAnalisis>> GetTipoAnalisis(string id)
        {
            TipoAnalisis? tipo =
                await _context.TiposAnalisis.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return tipo;
        }

        [HttpPost]
        public async Task<ActionResult<TipoAnalisis>> PostTipoAnalisis(
            TipoAnalisis tipo)
        {
            _context.TiposAnalisis.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTipoAnalisis),
                new { id = tipo.IdTipoAnalisis },
                tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAnalisis(
            string id,
            TipoAnalisis tipo)
        {
            if (id != tipo.IdTipoAnalisis)
            {
                return BadRequest();
            }

            _context.Entry(tipo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAnalisis(string id)
        {
            TipoAnalisis? tipo =
                await _context.TiposAnalisis.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            tipo.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}