using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioGranja.Models
{
    public class LoteLeche
    {
        [Key]
        public string IdLote { get; set; } = "";

        [Required]
        public string IdGranja { get; set; } = "";

        public DateTime FechaOrdenio { get; set; }

        public string Turno { get; set; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public decimal VolumenLitros { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal Temperatura { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal Acidez { get; set; }

        public string Estado { get; set; } = "";

        public Granja? Granja { get; set; }

        public ICollection<LoteAnalisisCalidad> AnalisisCalidad { get; set; } = new List<LoteAnalisisCalidad>();

        public ICollection<EntregaFabrica> Entregas { get; set; } = new List<EntregaFabrica>();
    }
}