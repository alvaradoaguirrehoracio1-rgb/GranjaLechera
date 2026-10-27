using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalCertificacionesController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public AnimalCertificacionesController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalCertificacion>>> GetAnimalCertificaciones()
        {
            return await _context.AnimalesCertificaciones.ToListAsync();
        }

        [HttpGet("{idAnimal}/{idCertificacion}")]
        public async Task<ActionResult<AnimalCertificacion>> GetAnimalCertificacion(
            string idAnimal,
            string idCertificacion)
        {
            AnimalCertificacion? animalCertificacion =
                await _context.AnimalesCertificaciones.FindAsync(
                    idAnimal,
                    idCertificacion);

            if (animalCertificacion == null)
            {
                return NotFound();
            }

            return animalCertificacion;
        }

        [HttpPost]
        public async Task<ActionResult<AnimalCertificacion>> PostAnimalCertificacion(
            AnimalCertificacion animalCertificacion)
        {
            _context.AnimalesCertificaciones.Add(animalCertificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAnimalCertificacion),
                new
                {
                    idAnimal = animalCertificacion.IdAnimal,
                    idCertificacion = animalCertificacion.IdCertificacion
                },
                animalCertificacion);
        }

        [HttpPut("{idAnimal}/{idCertificacion}")]
        public async Task<IActionResult> PutAnimalCertificacion(
            string idAnimal,
            string idCertificacion,
            AnimalCertificacion animalCertificacion)
        {
            if (idAnimal != animalCertificacion.IdAnimal ||
                idCertificacion != animalCertificacion.IdCertificacion)
            {
                return BadRequest();
            }

            _context.Entry(animalCertificacion).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idAnimal}/{idCertificacion}")]
        public async Task<IActionResult> DeleteAnimalCertificacion(
            string idAnimal,
            string idCertificacion)
        {
            AnimalCertificacion? animalCertificacion =
                await _context.AnimalesCertificaciones.FindAsync(
                    idAnimal,
                    idCertificacion);

            if (animalCertificacion == null)
            {
                return NotFound();
            }

            animalCertificacion.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}