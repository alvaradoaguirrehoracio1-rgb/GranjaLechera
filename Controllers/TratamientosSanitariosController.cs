using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamientosSanitariosController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public TratamientosSanitariosController(GranjaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoSanitario>>> GetTratamientos()
        {
            return await _context.TratamientosSanitarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TratamientoSanitario>> GetTratamiento(string id)
        {
            TratamientoSanitario? tratamiento =
                await _context.TratamientosSanitarios.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return tratamiento;
        }

        [HttpPost]
        public async Task<ActionResult<TratamientoSanitario>> PostTratamiento(
            TratamientoSanitario tratamiento)
        {
            _context.TratamientosSanitarios.Add(tratamiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTratamiento),
                new { id = tratamiento.IdTratamiento },
                tratamiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamiento(
            string id,
            TratamientoSanitario tratamiento)
        {
            if (id != tratamiento.IdTratamiento)
            {
                return BadRequest();
            }

            _context.Entry(tratamiento).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamiento(string id)
        {
            TratamientoSanitario? tratamiento =
                await _context.TratamientosSanitarios.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            tratamiento.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}