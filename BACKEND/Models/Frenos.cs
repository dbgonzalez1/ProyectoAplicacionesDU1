using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Frenos : HasEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de freno es requerido")]
        [StringLength(50, ErrorMessage = "El tipo de freno debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(50, ErrorMessage = "La marca debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        [StringLength(50, ErrorMessage = "El modelo debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Modelo { get; set; }


        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Precio { get; set; }

        public Proveedor Proveedor { get; set; }
    }
}