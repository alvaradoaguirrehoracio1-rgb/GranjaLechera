using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalesController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public AnimalesController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimales()
        {
            return await _context.Animales.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(string id)
        {
            Animal? animal = await _context.Animales.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animales.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.IdAnimal }, animal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(string id, Animal animal)
        {
            if (id != animal.IdAnimal)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(string id)
        {
            Animal? animal = await _context.Animales.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            animal.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}