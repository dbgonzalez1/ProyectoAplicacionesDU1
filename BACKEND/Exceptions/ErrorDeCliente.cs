using System.Net;

namespace backend.Exceptions;

public class ErrorDeCliente(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : Exception(message)
{
     public HttpStatusCode StatusCode { get; } = statusCode;
     
}