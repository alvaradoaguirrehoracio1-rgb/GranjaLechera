using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalVeterinariosController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public AnimalVeterinariosController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalVeterinario>>> GetAnimalVeterinarios()
        {
            return await _context.AnimalesVeterinarios.ToListAsync();
        }

        [HttpGet("{idAnimal}/{idVeterinario}")]
        public async Task<ActionResult<AnimalVeterinario>> GetAnimalVeterinario(
            string idAnimal,
            string idVeterinario)
        {
            AnimalVeterinario? animalVeterinario =
                await _context.AnimalesVeterinarios.FindAsync(idAnimal, idVeterinario);

            if (animalVeterinario == null)
            {
                return NotFound();
            }

            return animalVeterinario;
        }

        [HttpPost]
        public async Task<ActionResult<AnimalVeterinario>> PostAnimalVeterinario(
            AnimalVeterinario animalVeterinario)
        {
            _context.AnimalesVeterinarios.Add(animalVeterinario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAnimalVeterinario),
                new
                {
                    idAnimal = animalVeterinario.IdAnimal,
                    idVeterinario = animalVeterinario.IdVeterinario
                },
                animalVeterinario);
        }

        [HttpPut("{idAnimal}/{idVeterinario}")]
        public async Task<IActionResult> PutAnimalVeterinario(
            string idAnimal,
            string idVeterinario,
            AnimalVeterinario animalVeterinario)
        {
            if (idAnimal != animalVeterinario.IdAnimal ||
                idVeterinario != animalVeterinario.IdVeterinario)
            {
                return BadRequest();
            }

            _context.Entry(animalVeterinario).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idAnimal}/{idVeterinario}")]
        public async Task<IActionResult> DeleteAnimalVeterinario(
            string idAnimal,
            string idVeterinario)
        {
            AnimalVeterinario? animalVeterinario =
                await _context.AnimalesVeterinarios.FindAsync(idAnimal, idVeterinario);

            if (animalVeterinario == null)
            {
                return NotFound();
            }

            animalVeterinario.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}