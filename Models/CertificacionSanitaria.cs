using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class CertificacionSanitaria
    {
        [Key]
        public string IdCertificacion { get; set; } = "";

        [Required]
        public string NombreNorma { get; set; } = "";

        public string OrganismoEmisor { get; set; } = "";

        public string Estado { get; set; } = "Activo";

        public ICollection<AnimalCertificacion> Animales { get; set; } = new List<AnimalCertificacion>();
    }
}   