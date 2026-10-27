namespace MicroservicioGranja.Models
{
    public class EmpleadoDto
    {
        public object? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public object? IdArea { get; set; }
        public string? Cargo { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
    }
}