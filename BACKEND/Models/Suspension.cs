using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Suspension : HasEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [MinLength(3, ErrorMessage = "La marca debe tener al menos 3 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        [MinLength(3, ErrorMessage = "El modelo debe tener al menos 3 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "La rigidez es requerida")]
        [Range(0, float.MaxValue, ErrorMessage = "La rigidez debe ser mayor a 0")]
        public float Rigidez { get; set; }

        [Required(ErrorMessage = "La altura es requerida")]
        [Range(0, float.MaxValue, ErrorMessage = "La altura debe ser mayor a 0")]
        public float Altura { get; set; }

        [Required(ErrorMessage = "La capacidad de carga es requerida")]
        [Range(0, float.MaxValue, ErrorMessage = "La capacidad de carga debe ser mayor a 0")]
        public float CapacidadDeCarga { get; set; }

        public Amortiguador? Amortiguador { get; set; }

        public Proveedor? Proveedor { get; set; }
    }
}