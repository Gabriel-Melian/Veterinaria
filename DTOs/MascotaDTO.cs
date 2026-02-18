using Veterinaria.Models;
using System.ComponentModel.DataAnnotations;

public class MascotaDTO
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Especie { get; set; } = string.Empty;

    [Required]
    public string Raza { get; set; } = string.Empty;

    public DateTime? FechaNac { get; set; }

    [Required]
    public SexoMascota Sexo { get; set; }

    [Required]
    public bool Esterilizado { get; set; }

    public int? IdCliente { get; set; }
}