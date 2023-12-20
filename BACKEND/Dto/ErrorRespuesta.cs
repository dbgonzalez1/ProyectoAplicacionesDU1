using System.Text.Json;

namespace backend.Dto;

public class ErrorRespuesta(string mensaje)
{
    public string message { get; set; } = mensaje;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}