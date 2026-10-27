using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteAnalisisCalidadController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public LoteAnalisisCalidadController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteAnalisisCalidad>>> GetLotesAnalisis()
        {
            return await _context.LotesAnalisisCalidad.ToListAsync();
        }

        [HttpGet("{idLote}/{idTipoAnalisis}")]
        public async Task<ActionResult<LoteAnalisisCalidad>> GetLoteAnalisis(
            string idLote,
            string idTipoAnalisis)
        {
            LoteAnalisisCalidad? analisis =
                await _context.LotesAnalisisCalidad.FindAsync(
                    idLote,
                    idTipoAnalisis);

            if (analisis == null)
            {
                return NotFound();
            }

            return analisis;
        }

        [HttpPost]
        public async Task<ActionResult<LoteAnalisisCalidad>> PostLoteAnalisis(
            LoteAnalisisCalidad analisis)
        {
            _context.LotesAnalisisCalidad.Add(analisis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLoteAnalisis),
                new
                {
                    idLote = analisis.IdLote,
                    idTipoAnalisis = analisis.IdTipoAnalisis
                },
                analisis);
        }

        [HttpPut("{idLote}/{idTipoAnalisis}")]
        public async Task<IActionResult> PutLoteAnalisis(
            string idLote,
            string idTipoAnalisis,
            LoteAnalisisCalidad analisis)
        {
            if (idLote != analisis.IdLote ||
                idTipoAnalisis != analisis.IdTipoAnalisis)
            {
                return BadRequest();
            }

            _context.Entry(analisis).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idLote}/{idTipoAnalisis}")]
        public async Task<IActionResult> DeleteLoteAnalisis(
            string idLote,
            string idTipoAnalisis)
        {
            LoteAnalisisCalidad? analisis =
                await _context.LotesAnalisisCalidad.FindAsync(
                    idLote,
                    idTipoAnalisis);

            if (analisis == null)
            {
                return NotFound();
            }

            analisis.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}