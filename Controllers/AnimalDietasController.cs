using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalDietasController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public AnimalDietasController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDieta>>> GetAnimalDietas()
        {
            return await _context.AnimalesDietas.ToListAsync();
        }

        [HttpGet("{idAnimal}/{idInsumo}")]
        public async Task<ActionResult<AnimalDieta>> GetAnimalDieta(
            string idAnimal,
            string idInsumo)
        {
            AnimalDieta? animalDieta =
                await _context.AnimalesDietas.FindAsync(idAnimal, idInsumo);

            if (animalDieta == null)
            {
                return NotFound();
            }

            return animalDieta;
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDieta>> PostAnimalDieta(
            AnimalDieta animalDieta)
        {
            _context.AnimalesDietas.Add(animalDieta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAnimalDieta),
                new
                {
                    idAnimal = animalDieta.IdAnimal,
                    idInsumo = animalDieta.IdInsumo
                },
                animalDieta);
        }

        [HttpPut("{idAnimal}/{idInsumo}")]
        public async Task<IActionResult> PutAnimalDieta(
            string idAnimal,
            string idInsumo,
            AnimalDieta animalDieta)
        {
            if (idAnimal != animalDieta.IdAnimal ||
                idInsumo != animalDieta.IdInsumo)
            {
                return BadRequest();
            }

            _context.Entry(animalDieta).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idAnimal}/{idInsumo}")]
        public async Task<IActionResult> DeleteAnimalDieta(
            string idAnimal,
            string idInsumo)
        {
            AnimalDieta? animalDieta =
                await _context.AnimalesDietas.FindAsync(idAnimal, idInsumo);

            if (animalDieta == null)
            {
                return NotFound();
            }

            animalDieta.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}