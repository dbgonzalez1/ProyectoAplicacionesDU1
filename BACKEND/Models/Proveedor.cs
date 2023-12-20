using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Proveedor : HasEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La direccion debe tener entre 2 y 50 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "El numero de telefono debe tener 10 digitos númericos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string Correo { get; set; }
    }
}