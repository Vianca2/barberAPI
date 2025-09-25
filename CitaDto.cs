using System.ComponentModel.DataAnnotations;

namespace BarberappAPI.DTOs
{
    public class CitaDto
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoCorte { get; set; }
        public string? Descripcion { get; set; }
        public string Estado { get; set; }
        public string? CodigoConfirmacion { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CrearCitaDto
    {
        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El tipo de corte es requerido")]
        [StringLength(100, ErrorMessage = "El tipo de corte no puede exceder los 100 caracteres")]
        public string TipoCorte { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
        public string? Descripcion { get; set; }
    }

    public class ActualizarEstadoCitaDto
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public string Estado { get; set; }

        public string? CodigoConfirmacion { get; set; }
    }
}