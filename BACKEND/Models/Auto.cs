using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Auto : HasEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Modelo { get; set; }

        public MarcaAuto? Marca { get; set; }
        public Amortiguador? Amortiguador { get; set; }
        public Transmision? Transmision { get; set; }
        public Suspension? Suspension { get; set; }
        public Motor? Motor { get; set; }
        public Frenos? Frenos { get; set; }
        public Neumatico? Neumatico { get; set; }
    }
}