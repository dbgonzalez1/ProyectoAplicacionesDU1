using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Amortiguador: HasEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria")]
    [StringLength(50, ErrorMessage = "La marca  debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
    public string Marca { get; set; }

    [Required(ErrorMessage = "El modelo es obligatorio")]
    [StringLength(50, ErrorMessage = "El modelo debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
    public string Modelo { get; set; }


    [Required(ErrorMessage = "El recorrido es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El recorrido debe ser mayor a 0")]
    public float Recorrido { get; set; }

    [Required(ErrorMessage = "La fuerza es obligatoria")]
    [Range(0, int.MaxValue, ErrorMessage = "La fuerza debe ser mayor a 0")]
    public float Fuerza { get; set; }

    [Required(ErrorMessage = "Los años de utilidad son obligatorios")]
    [Range(0, int.MaxValue, ErrorMessage = "Los años de utilidad deben ser mayor a 0")]
    public int AñosUtil { get; set; }


    [Required(ErrorMessage = "El diametro del piston es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El diametro del piston debe ser mayor a 0")]
    public float DiametroDelPiston { get; set; }

    public Proveedor? Proveedor { get; set; }
}