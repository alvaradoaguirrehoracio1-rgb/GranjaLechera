using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class Granja
    {
        [Key]
        public string IdGranja { get; set; } = "";

        [Required]
        public string Nombre { get; set; } = "";

        public string Ubicacion { get; set; } = "";

        public string Estado { get; set; } = "Activo";

        public ICollection<Animal> Animales { get; set; } = new List<Animal>();

        public ICollection<LoteLeche> LotesLeche { get; set; } = new List<LoteLeche>();
    }
}