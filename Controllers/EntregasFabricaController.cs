using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using MicroservicioGranja.Models;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregasFabricaController : ControllerBase
    {
        private readonly GranjaDbContext _context;

        public EntregasFabricaController(GranjaDbContext context)
        {
            _context = context;
        }

        // GET: api/EntregasFabrica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntregaFabrica>>> GetEntregas()
        {
            return await _context.EntregasFabrica
                .Include(e => e.LoteLeche)
                .Where(e => e.Estado == "Activo")
                .ToListAsync();
        }

        // GET: api/EntregasFabrica/ENT-001
        [HttpGet("{id}")]
        public async Task<ActionResult<EntregaFabrica>> GetEntrega(string id)
        {
            var entrega = await _context.EntregasFabrica
                .Include(e => e.LoteLeche)
                .FirstOrDefaultAsync(e => e.IdEntrega == id);

            if (entrega == null) return NotFound("Entrega no encontrada.");

            return entrega;
        }

        // POST: api/EntregasFabrica (Crear Solicitud desde Fábrica)
        [HttpPost]
        public async Task<ActionResult<EntregaFabrica>> PostEntrega([FromBody] EntregaFabrica entrega)
        {
            if (await _context.EntregasFabrica.AnyAsync(e => e.IdEntrega == entrega.IdEntrega))
            {
                return BadRequest($"Ya existe una entrega con el ID '{entrega.IdEntrega}'.");
            }

            var loteExiste = await _context.LotesLeche.AnyAsync(l => l.IdLote == entrega.IdLote);
            if (!loteExiste)
            {
                return BadRequest($"El Lote '{entrega.IdLote}' no existe en la base de datos.");
            }

            entrega.EstadoSolicitud = "Pendiente";
            entrega.EstadoEnvio = "Activo";
            entrega.Estado = "Activo";
            entrega.FechaEnvio = DateTime.UtcNow;

            _context.EntregasFabrica.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEntrega), new { id = entrega.IdEntrega }, entrega);
        }

        // POST: api/EntregasFabrica/ENT-001/aceptar (Granja Acepta)
        [HttpPost("{id}/aceptar")]
        public async Task<IActionResult> AceptarSolicitud(string id)
        {
            var entrega = await _context.EntregasFabrica.FindAsync(id);
            if (entrega == null) return NotFound();

            entrega.EstadoSolicitud = "Aceptado";
            await _context.SaveChangesAsync();

            return Ok(entrega);
        }

        // POST: api/EntregasFabrica/ENT-001/rechazar (Granja Rechaza)
        [HttpPost("{id}/rechazar")]
        public async Task<IActionResult> RechazarSolicitud(string id)
        {
            var entrega = await _context.EntregasFabrica.FindAsync(id);
            if (entrega == null) return NotFound();

            entrega.EstadoSolicitud = "Rechazado";
            await _context.SaveChangesAsync();

            return Ok(entrega);
        }

        // PUT: api/EntregasFabrica/ENT-001/estado-envio (Distribución actualiza estados)
        [HttpPut("{id}/estado-envio")]
        public async Task<IActionResult> UpdateEstadoEnvio(string id, [FromBody] EstadoEnvioDto dto)
        {
            var entrega = await _context.EntregasFabrica.FindAsync(id);
            if (entrega == null) return NotFound();

            entrega.EstadoEnvio = dto.EstadoEnvio;
            await _context.SaveChangesAsync();

            return Ok(entrega);
        }

        // POST: api/EntregasFabrica/ENT-001/recepcion-fabrica (Fábrica recibe + Granja vacíos)
        [HttpPost("{id}/recepcion-fabrica")]
        public async Task<IActionResult> RecepcionFabrica(string id, [FromBody] RecepcionFabricaDto dto)
        {
            var entrega = await _context.EntregasFabrica.FindAsync(id);
            if (entrega == null) return NotFound();

            if (dto.VolumenEntregado.HasValue) entrega.VolumenEntregado = dto.VolumenEntregado.Value;
            if (dto.TachosAceptados.HasValue) entrega.TachosAceptados = dto.TachosAceptados.Value;
            if (dto.TachosRechazados.HasValue) entrega.TachosRechazados = dto.TachosRechazados.Value;
            if (!string.IsNullOrEmpty(dto.ResponsableRecepcion)) entrega.ResponsableRecepcion = dto.ResponsableRecepcion;
            if (dto.TachosVaciosDevueltos.HasValue) entrega.TachosVaciosDevueltos = dto.TachosVaciosDevueltos.Value;

            await _context.SaveChangesAsync();
            return Ok(entrega);
        }
    }

    // DTOs auxiliares para peticiones parciales
    public class EstadoEnvioDto
    {
        public string EstadoEnvio { get; set; } = "Activo";
    }

    public class RecepcionFabricaDto
    {
        public decimal? VolumenEntregado { get; set; }
        public int? TachosAceptados { get; set; }
        public int? TachosRechazados { get; set; }
        public string? ResponsableRecepcion { get; set; }
        public bool? TachosVaciosDevueltos { get; set; }
    }
}
