using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesLecheController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public LotesLecheController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteLeche>>> GetLotesLeche()
        {
            return await _context.LotesLeche.ToListAsync();
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<LoteLeche>> GetLoteLeche(string id)
        {
            LoteLeche? lote =
                await _context.LotesLeche.FindAsync(id);

            if (lote == null)
            {
                return NotFound();
            }

            return lote;
        }

        [HttpPost]
        public async Task<ActionResult<LoteLeche>> PostLoteLeche(
            LoteLeche lote)
        {
            _context.LotesLeche.Add(lote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLoteLeche),
                new { id = lote.IdLote },
                lote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoteLeche(
            string id,
            LoteLeche lote)
        {
            if (id != lote.IdLote)
            {
                return BadRequest();
            }

            _context.Entry(lote).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoteLeche(string id)
        {
            LoteLeche? lote =
                await _context.LotesLeche.FindAsync(id);

            if (lote == null)
            {
                return NotFound();
            }

            lote.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}