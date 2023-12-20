using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class MarcaAuto : HasEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }
    }
}