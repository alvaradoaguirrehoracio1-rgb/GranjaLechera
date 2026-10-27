using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class LoteAnalisisCalidad
    {
        [Key]
        public string IdLote { get; set; } = "";

        [Required]
        public string IdTipoAnalisis { get; set; } = "";

        public string Resultado { get; set; } = "";

        public DateTime FechaAnalisis { get; set; }

        public string Estado { get; set; } = "Activo";

        public LoteLeche? LoteLeche { get; set; }

        public TipoAnalisis? TipoAnalisis { get; set; }
    }
}