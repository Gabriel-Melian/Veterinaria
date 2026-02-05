using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;//Evitar mostrar la clave hasheada en el JSON
namespace Veterinaria.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        public int Estado { get; set; }
    }
}