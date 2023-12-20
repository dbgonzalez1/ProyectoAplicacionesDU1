namespace backend.Exceptions;

public class ErrorDeValidacionDeAtrr(List<string> errors) : Exception
{
    public List<string> Errors { get; set; } = errors;
}