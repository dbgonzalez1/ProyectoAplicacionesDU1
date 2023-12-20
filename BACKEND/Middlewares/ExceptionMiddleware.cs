using System.Net;
using System.Text.Json;
using backend.Dto;
using backend.Exceptions;

namespace backend.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            logger.LogInformation("Request: " + httpContext.Request.Path);
            await next(httpContext);
        }
        catch (ErrorDeCliente ex)
        {
            logger.LogWarning(ex.Message);
            await HandleErrorDeClienteAsync(httpContext, ex);
        }
        catch (ErrorDeValidacionDeAtrr ex)
        {
            logger.LogWarning(ex.Message);
            await HandleErrorDeValidacionDeAttrAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            await HandleExceptionAsync(httpContext);
        }
    }

    private static Task HandleErrorDeValidacionDeAttrAsync(HttpContext context, ErrorDeValidacionDeAtrr ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errores = ex
            .Errors
            .Select(error => new ErrorRespuesta(error))
            .ToList();

        return context.Response.WriteAsync(JsonSerializer.Serialize(errores));
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(new ErrorRespuesta("Error en el servidor, contacte con el administardor").ToString());
    }

    private static Task HandleErrorDeClienteAsync(HttpContext context, ErrorDeCliente ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)ex.StatusCode;

        return context.Response.WriteAsync(new ErrorRespuesta(ex.Message).ToString());
    }
}