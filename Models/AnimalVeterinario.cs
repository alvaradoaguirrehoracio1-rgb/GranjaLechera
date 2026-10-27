using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class AnimalVeterinario
    {
        [Key]
        public string IdAnimal { get; set; } = "";

        [Required]
        public string IdVeterinario { get; set; } = "";

        public string Estado { get; set; } = "Activo";

        public Animal? Animal { get; set; }
    }
}