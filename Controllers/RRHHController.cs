using MicroservicioGranja.Models;
using MicroservicioGranja.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MicroservicioGranja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RRHHController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string RrhhBaseUrl = "https://bhgj3lnh-7263.brs.devtunnels.ms";

        public RRHHController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. Obtener empleados por Área (ejemplo: Área de Granja)
        // GET: api/RRHH/empleados/area/1
        [HttpGet("empleados/area/{idArea}")]
        public async Task<IActionResult> BuscarPorArea(string idArea)
        {
            return await ConsumirEndpointRrhh($"/api/rrhh/empleados/BuscarPorArea/{idArea}");
        }

        // 2. Obtener solo Veterinarios de RRHH
        // GET: api/RRHH/veterinarios
        [HttpGet("veterinarios")]
        public async Task<IActionResult> ObtenerVeterinarios([FromQuery] string idArea = "1")
        {
            var result = await ConsumirListaEmpleados($"/api/rrhh/empleados/BuscarPorArea/{idArea}");
            if (result.Error != null) return result.Error;

            // Filtramos en memoria los empleados cuyo cargo sea "Veterinario"
            var veterinarios = result.Empleados?
                .Where(e => e.Cargo != null && e.Cargo.Contains("Veterinario", StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(veterinarios ?? result.Empleados);
        }

        // 3. Obtener solo Operarios de Granja de RRHH
        // GET: api/RRHH/operarios-granja
        [HttpGet("operarios-granja")]
        public async Task<IActionResult> ObtenerOperariosGranja([FromQuery] string idArea = "1")
        {
            var result = await ConsumirListaEmpleados($"/api/rrhh/empleados/BuscarPorArea/{idArea}");
            if (result.Error != null) return result.Error;

            // Filtramos los empleados con cargo "Operario" o similar
            var operarios = result.Empleados?
                .Where(e => e.Cargo != null && (
                    e.Cargo.Contains("Operario", StringComparison.OrdinalIgnoreCase) ||
                    e.Cargo.Contains("Granja", StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return Ok(operarios ?? result.Empleados);
        }

        // -----------------------------------------------------------------
        // MÉTODOS AUXILIARES PARA PETICIONES HTTP
        // -----------------------------------------------------------------

        private async Task<(List<EmpleadoDto>? Empleados, IActionResult? Error)> ConsumirListaEmpleados(string path)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Tunnel-Skip-Anti-Phishing-Page", "true");

            try
            {
                var response = await client.GetAsync($"{RrhhBaseUrl}{path}");
                if (!response.IsSuccessStatusCode)
                {
                    return (null, StatusCode((int)response.StatusCode, "Error al consultar el microservicio de RRHH."));
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var empleados = JsonSerializer.Deserialize<List<EmpleadoDto>>(jsonString, options);

                return (empleados, null);
            }
            catch (Exception ex)
            {
                return (null, StatusCode(500, new { mensaje = "Error de comunicación con RRHH", detalle = ex.Message }));
            }
        }

        private async Task<IActionResult> ConsumirEndpointRrhh(string path)
        {
            var (empleados, error) = await ConsumirListaEmpleados(path);
            if (error != null) return error;
            return Ok(empleados);
        }
    }
}