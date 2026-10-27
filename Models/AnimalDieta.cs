using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class AnimalDieta
    {
        [Key]
        public string IdAnimal { get; set; } = "";

        [Required]
        public string IdInsumo { get; set; } = "";

        public decimal ProporcionDiaria { get; set; }

        public string Estado { get; set; } = "Activo";

        public Animal? Animal { get; set; }
    }
}