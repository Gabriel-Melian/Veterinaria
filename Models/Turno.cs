using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{
    public enum EstadoTurno
    {
        Cancelado = 0,
        Programado = 1,
        Completado = 2
    }
    public class Turno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Cliente")]
        public int? IdCliente { get; set; }

        [ForeignKey("Mascota")]
        public int? IdMascota { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        public string? Motivo { get; set; } = string.Empty;

        public EstadoTurno Estado { get; set; }

        //Relaciones
        public Usuario? Usuario { get; set; }
        public Cliente? Cliente { get; set; }
        public Mascota? Mascota { get; set; }
    }
}