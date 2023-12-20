
namespace backend.Dto;

public class AmortiguadorDto
{
    public string Marca { get; set; } = null!;
    public string Modelo { get; set; } = null!;
    public float Recorrido { get; set; }
    public float Fuerza { get; set; }
    public int AñosUtil { get; set; }
    public float DiametroDelPiston { get; set; }
    public int IdProveedor { get; set; }
}