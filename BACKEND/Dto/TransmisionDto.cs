namespace backend.Dto;

public class TransmisionDto
{
    public string Tipo { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public float Relacion { get; set; }
    public float Eficiencia { get; set; }
    public float Consumo { get; set; }
    public float NumeroDeMarchas { get; set; }
    public int IdProveedor { get; set; }
}