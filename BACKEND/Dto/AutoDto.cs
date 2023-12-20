namespace backend.Dto;

public class AutoDto
{
    public string Modelo { get; set; } = null!;
    public int IdMarca { get; set; }
    public int IdAmortiguador { get; set; }
    public int IdTransmision { get; set; }
    public int IdSuspension { get; set; }
    public int IdMotor { get; set; }
    public int IdFrenos { get; set; }
    public int IdNeumatico { get; set; }
}