using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class AnimalCertificacion
    {
        [Key]
        public string IdAnimal { get; set; } = "";

        [Required]
        public string IdCertificacion { get; set; } = "";

        public DateTime FechaEmision { get; set; }

        public string Estado { get; set; } = "Activo";

        public Animal? Animal { get; set; }

        public CertificacionSanitaria? CertificacionSanitaria { get; set; }
    }
}