using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioGranja.Models
{
    public class EntregaFabrica
    {
        [Key]
        public string IdEntrega { get; set; } = "";

        [Required]
        public string IdLote { get; set; } = "";

        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        // Volumen de leche
        [Column(TypeName = "decimal(10,2)")]
        public decimal VolumenSolicitado { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal VolumenEntregado { get; set; }

        public string ResponsableRecepcion { get; set; } = "";

        // Estados del flujo de la solicitud
        // Valores posibles: "Pendiente", "Aceptado", "Rechazado"
        public string EstadoSolicitud { get; set; } = "Pendiente";

        // Estados del transporte (Distribución)
        // Valores posibles: "Activo", "En proceso", "Recogido", "Entregado", "Finalizado"
        public string EstadoEnvio { get; set; } = "Activo";

        // Control de tachos de leche
        public int CantidadTachos { get; set; }
        public int TachosAceptados { get; set; }
        public int TachosRechazados { get; set; }
        public bool TachosVaciosDevueltos { get; set; } = false;

        // Borrado lógico ("Activo" / "Inactivo")
        public string Estado { get; set; } = "Activo";

        public LoteLeche? LoteLeche { get; set; }
    }
}
