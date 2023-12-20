using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;
using static backend.Services.Validador;


namespace backend.Services;

public abstract class FrenosService
{
    public static async Task<Frenos> Create(FrenosDto frenos)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(frenos.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        var create = new Frenos
        {
            Marca = frenos.Marca,
            Modelo = frenos.Modelo,
            Tipo = frenos.Tipo,
            Proveedor = proveedor,
            Precio = frenos.Precio
        };


        await DataRepository.Frenos.AddAsync(create);
        return create;
    }

    public static async Task<Frenos> Find(int id)
    {
        var frenos = await DataRepository.Frenos.FindAsync(id);
        if (frenos == null)
        {
            throw new ErrorDeCliente("Frenos no encontrado", HttpStatusCode.NotFound);
        }

        return frenos;
    }


    public static Task<List<Frenos>> GetAll()
    {
        return DataRepository.Frenos.ToListAsync();
    }

    public static async Task<IResult> Update(int id, FrenosDto frenos)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Auto no encontrado", HttpStatusCode.NotFound);
        }

        var proveedor = await DataRepository.Proveedor.FindAsync(frenos.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Marca = frenos.Marca;
        encontrado.Modelo = frenos.Modelo;
        encontrado.Tipo = frenos.Tipo;
        encontrado.Proveedor = proveedor;
        encontrado.Precio = frenos.Precio;
        
        await DataRepository.Frenos.UpdateAsync(encontrado);
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var frenos = await Find(id);

        if (frenos == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Frenos == frenos), "autos");

        await DataRepository.Frenos.RemoveAsync(frenos);
    }
}