using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class ProveedorEndpoints
{
    public static void MapProveedorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Proveedor").RequireAuthorization();

        group.MapGet("/", ProveedorService.GetAll)
            .WithName("GetAllProveedors")
            .Produces<List<Proveedor>>();

        group.MapGet("/{id:int}", ProveedorService.Find)
            .WithName("GetProveedorById")
            .Produces<Proveedor>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", ProveedorService.Update)
            .WithName("UpdateProveedor")
            .Accepts<ProveedorDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", ProveedorService.Create)
            .WithName("CreateProveedor")
            .Accepts<ProveedorDto>("application/json")
            .Produces<Proveedor>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", ProveedorService.Delete)
            .WithName("DeleteProveedor")
            .Produces(StatusCodes.Status404NotFound);
    }
}