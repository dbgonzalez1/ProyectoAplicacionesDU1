namespace backend.Dto;

public class SuspensionDto
{
    public string Marca { get; set; }

    public string Modelo { get; set; }

    public float Rigidez { get; set; }

    public float Altura { get; set; }

    public float CapacidadDeCarga { get; set; }

    public int IdAmortiguador { get; set; }

    public int IdProveedor { get; set; }

}