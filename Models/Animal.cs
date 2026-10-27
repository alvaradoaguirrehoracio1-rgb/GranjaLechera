using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioGranja.Models
{
    public class Animal
    {
        [Key]
        public string IdAnimal { get; set; } = "";

        [Required]
        public string IdGranja { get; set; } = "";

        public DateTime FechaNacimiento { get; set; }

        public string EstadoSalud { get; set; } = "";

        public string Estado { get; set; } = "Activo";

        [ForeignKey("IdGranja")]
        public Granja? Granja { get; set; }

        public ICollection<AnimalVeterinario> Veterinarios { get; set; } = new List<AnimalVeterinario>();

        public ICollection<AnimalDieta> Dietas { get; set; } = new List<AnimalDieta>();

        public ICollection<AnimalCertificacion> Certificaciones { get; set; } = new List<AnimalCertificacion>();

        public ICollection<TratamientoSanitario> Tratamientos { get; set; } = new List<TratamientoSanitario>();
    }
}