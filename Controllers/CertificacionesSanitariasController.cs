using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificacionesSanitariasController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public CertificacionesSanitariasController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificacionSanitaria>>> GetCertificaciones()
        {
            return await _context.CertificacionesSanitarias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CertificacionSanitaria>> GetCertificacion(string id)
        {
            CertificacionSanitaria? certificacion = await _context.CertificacionesSanitarias.FindAsync(id);

            if (certificacion == null)
            {
                return NotFound();
            }

            return certificacion;
        }

        [HttpPost]
        public async Task<ActionResult<CertificacionSanitaria>> PostCertificacion(CertificacionSanitaria certificacion)
        {
            _context.CertificacionesSanitarias.Add(certificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCertificacion),
                new { id = certificacion.IdCertificacion },
                certificacion
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificacion(
            string id,
            CertificacionSanitaria certificacion)
        {
            if (id != certificacion.IdCertificacion)
            {
                return BadRequest();
            }

            _context.Entry(certificacion).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificacion(string id)
        {
            CertificacionSanitaria? certificacion =
                await _context.CertificacionesSanitarias.FindAsync(id);

            if (certificacion == null)
            {
                return NotFound();
            }

            certificacion.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}