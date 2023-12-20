namespace backend.Dto;

public class MotorDto
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public float Potencia { get; set; }
    public float Torque { get; set; }
    public float Eficiencia { get; set; }
    public float Consumo { get; set; }
    public float Cilindrada { get; set; }
    public int IdProveedor { get; set; }
}