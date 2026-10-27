using System.ComponentModel.DataAnnotations;

namespace MicroservicioGranja.Models
{
    public class TipoAnalisis
    {
        [Key]
        public string IdTipoAnalisis { get; set; } = "";

        [Required]
        public string Nombre { get; set; } = "";

        public string Descripcion { get; set; } = "";
        
        public string Estado { get; set; } = "Activo";

        public ICollection<LoteAnalisisCalidad> Lotes { get; set; } = new List<LoteAnalisisCalidad>();
    }
}