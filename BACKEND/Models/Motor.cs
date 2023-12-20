using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Motor : HasEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La marca es requerida")]
    [StringLength(50, ErrorMessage = "La marca debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
    public string Marca { get; set; }

    [Required(ErrorMessage = "El modelo es requerido")]
    [StringLength(50, ErrorMessage = "El modelo debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "La potencia es requerida")]
    [Range(0, float.MaxValue, ErrorMessage = "La potencia debe ser mayor a 0")]
    public float Potencia { get; set; }

    [Required(ErrorMessage = "El torque es requerido")]
    [Range(0, float.MaxValue, ErrorMessage = "El torque debe ser mayor a 0")]
    public float Torque { get; set; }

    [Required(ErrorMessage = "La eficiencia es requerida")]
    [Range(0, float.MaxValue, ErrorMessage = "La eficiencia debe ser mayor a 0")]
    public float Eficiencia { get; set; }

    [Required(ErrorMessage = "El consumo es requerido")]
    [Range(0, float.MaxValue, ErrorMessage = "El consumo debe ser mayor a 0")]
    public float Consumo { get; set; }

    [Required(ErrorMessage = "La cilindrada es requerida")]
    [Range(0, float.MaxValue, ErrorMessage = "La cilindrada debe ser mayor a 0")]
    public float Cilindrada { get; set; }

    public Proveedor? Proveedor { get; set; }
}