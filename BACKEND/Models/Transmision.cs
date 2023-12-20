using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Transmision : HasEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El tipo debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marca debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Marca { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El modelo debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Modelo { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "La relacion debe ser mayor a 0")]
        public float Relacion { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "La eficiencia debe ser mayor a 0")]
        public float Eficiencia { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "El consumo debe ser mayor a 0")]
        public float Consumo { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "El numero de marchas debe ser mayor a 0")]
        public float NumeroDeMarchas { get; set; }

        public Proveedor? Proveedor { get; set; }
    }
}