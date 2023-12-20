using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;
using static backend.Services.Validador;

namespace backend.Services;

public abstract class ProveedorService
{
    public static async Task<Proveedor> Create(ProveedorDto proveedor)
    {
        if (await DataRepository.Proveedor.AnyAsync(el => el.Nombre == proveedor.Nombre))
        {
            throw new ErrorDeCliente("Ya existe un proveedor con ese nombre");
        }

        if (await DataRepository.Proveedor.AnyAsync(el => el.Correo == proveedor.Correo))
        {
            throw new ErrorDeCliente("Ya existe un proveedor con ese correo");
        }


        var create = new Proveedor
        {
            Nombre = proveedor.Nombre,
            Direccion = proveedor.Direccion,
            Telefono = proveedor.Telefono,
            Correo = proveedor.Correo
        };

        await DataRepository.Proveedor.AddAsync(create);
        return create;
    }

    public static async Task<Proveedor> Find(int id)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(id);
        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        return proveedor;
    }


    public static Task<List<Proveedor>> GetAll()
    {
        return DataRepository.Proveedor.ToListAsync();
    }

    public static async Task<IResult> Update(int id, ProveedorDto proveedor)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Nombre = proveedor.Nombre;
        encontrado.Direccion = proveedor.Direccion;
        encontrado.Telefono = proveedor.Telefono;
        encontrado.Correo = proveedor.Correo;

        await DataRepository.Proveedor.UpdateAsync(encontrado);
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var proveedor = await Find(id);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Amortiguador.AnyAsync(el => el.Proveedor == proveedor), "amortiguadores");
        ValidarRelaciones(await DataRepository.Frenos.AnyAsync(el => el.Proveedor == proveedor), "frenos");
        ValidarRelaciones(await DataRepository.Motor.AnyAsync(el => el.Proveedor == proveedor), "motores");
        ValidarRelaciones(await DataRepository.Neumatico.AnyAsync(el => el.Proveedor == proveedor), "neumaticos");
        ValidarRelaciones(await DataRepository.Suspension.AnyAsync(el => el.Proveedor == proveedor), "suspensiones");
        ValidarRelaciones(await DataRepository.Transmision.AnyAsync(el => el.Proveedor == proveedor), "suspensiones");


        await DataRepository.Proveedor.RemoveAsync(proveedor);
    }
}