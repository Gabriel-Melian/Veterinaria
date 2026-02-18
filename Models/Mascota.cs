using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{

    public enum SexoMascota
    {
        Macho = 1,
        Hembra = 2
    }
    
    public class Mascota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Especie { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Raza { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]//Fecha, sin hora
        public DateTime? FechaNac { get; set; }

        [Required]
        public SexoMascota Sexo { get; set; }

        [Required]
        public bool Esterilizado { get; set; }

        public int? IdCliente { get; set; }

        public int Estado { get; set; }

        //Relacion con Cliente
        [ForeignKey(nameof(IdCliente))]
        public Cliente? Cliente { get; set; }

        public ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}