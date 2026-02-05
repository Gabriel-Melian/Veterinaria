using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{

    public class Calendario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(1000)]
        public string? Anotaciones { get; set; } = string.Empty;

        //Relacion con Usuario
        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }

    }
}