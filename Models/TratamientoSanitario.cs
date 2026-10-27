using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class TratamientoSanitario
    {
        [Key]
        public string IdTratamiento { get; set; } = "";

        [Required]
        public string IdAnimal { get; set; } = "";

        [Required]
        public string TipoMedicamento { get; set; } = "";

        public DateTime FechaAplicacion { get; set; }

        public int PeriodoRetiroLeche { get; set; }

        public string Estado { get; set; } = "Activo";

        public Animal? Animal { get; set; }
    }
}