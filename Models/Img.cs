using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{
    public class Img
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Ficha")]
        public int FichaId { get; set; }

        [Required]
        [StringLength(255)]
        public string ImagenUrl { get; set; } = string.Empty;

        public string? Descripcion { get; set; } = string.Empty;

        public DateTime FechaSubida { get; set; } = DateTime.Now;

        //Relacion con Ficha
        public Ficha? Ficha { get; set; }
    }
}