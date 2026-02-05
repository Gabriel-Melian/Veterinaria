using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{
    public class Ficha
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdDuenio { get; set; }

        [Required]
        public int IdMascota { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        public float? Peso { get; set; }

        public int? FrecCardiaca { get; set; }

        public int? FrecRespiratoria { get; set; }

        public float? TempCentral { get; set; }

        public float? TempInterdigital { get; set; }

        public int? TiempoLlenadoCap { get; set; }

        public int? PresionSistolica { get; set; }

        public int? PresionDiastolica { get; set; }

        public int? PresionMediaArterial { get; set; }

        public int? FraccionAcortamiento { get; set; }

        public float? DiametroAuriculaIzq { get; set; }

        public float? DiametroAorta { get; set; }

        [Required]
        [StringLength(500)]
        public string ApartadoOcular { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string ApartadoAuditivo { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string AparatoDigestivo { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string AparatoUrinario { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Cardiologia { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string SistemaEndocrino { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string SistemaOsteomuscular { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string SistemaNervioso { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string SistemaReproductivo { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string SistemaRespiratorio { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string SistemaTegumento { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Tratamiento { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observaciones { get; set; } = string.Empty;

        [StringLength(200)]
        public string? DiagnosticoPresuntivo { get; set; } = string.Empty;

        //Relaciones
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }

        [ForeignKey(nameof(IdDuenio))]
        public Cliente Duenio { get; set; }

        [ForeignKey(nameof(IdMascota))]
        public Mascota Mascota { get; set; }

        public ICollection<Img>? Imgs { get; set; } = new List<Img>();

    }
}

