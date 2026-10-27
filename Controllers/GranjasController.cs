using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GranjasController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public GranjasController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Granja>>> GetGranjas()
        {
            return await _context.Granjas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Granja>> GetGranja(string id)
        {
            Granja? granja = await _context.Granjas.FindAsync(id);

            if (granja == null)
            {
                return NotFound();
            }

            return granja;
        }

        [HttpPost]
        public async Task<ActionResult<Granja>> PostGranja(Granja granja)
        {
            _context.Granjas.Add(granja);
             await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGranja), new { id = granja.IdGranja }, granja);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGranja(string id, Granja granja)
        {
            if (id != granja.IdGranja)
            {
                return BadRequest();
            }

            _context.Entry(granja).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGranja(string id)
        {
            Granja? granja = await _context.Granjas.FindAsync(id);

            if (granja == null)
            {
                return NotFound();
            }

            granja.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}