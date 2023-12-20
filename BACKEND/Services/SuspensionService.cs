using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;
using static backend.Services.Validador;

namespace backend.Services;

public abstract class SuspensionService
{
    public static async Task<Suspension> Create(SuspensionDto auto)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(auto.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        var amortiguador = await DataRepository.Amortiguador.FindAsync(auto.IdAmortiguador);

        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        var create = new Suspension
        {
            Marca = auto.Marca,
            Modelo = auto.Modelo,
            Rigidez = auto.Rigidez,
            Altura = auto.Altura,
            CapacidadDeCarga = auto.CapacidadDeCarga,
            Proveedor = proveedor,
            Amortiguador = amortiguador
        };

        await DataRepository.Suspension.AddAsync(create);
        return create;
    }

    public static async Task<Suspension> Find(int id)
    {
        var suspension = await DataRepository.Suspension.FindAsync(id);
        if (suspension == null)
        {
            throw new ErrorDeCliente("Suspension no encontrado", HttpStatusCode.NotFound);
        }

        return suspension;
    }


    public static Task<List<Suspension>> GetAll()
    {
        return DataRepository.Suspension.ToListAsync();
    }

    public static async Task<IResult> Update(int id, SuspensionDto suspension)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Suspension no encontrado", HttpStatusCode.NotFound);
        }

        var amortiguador = await DataRepository.Amortiguador.FindAsync(suspension.IdAmortiguador);

        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        var proveedor = await DataRepository.Proveedor.FindAsync(suspension.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Marca = suspension.Marca;
        encontrado.Modelo = suspension.Modelo;
        encontrado.Rigidez = suspension.Rigidez;
        encontrado.Altura = suspension.Altura;
        encontrado.CapacidadDeCarga = suspension.CapacidadDeCarga;
        encontrado.Amortiguador = amortiguador;
        encontrado.Proveedor = proveedor;

        await DataRepository.Suspension.UpdateAsync(encontrado);
      
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var suspension = await Find(id);

        if (suspension == null)
        {
            throw new ErrorDeCliente("Suspension no encontrado", HttpStatusCode.NotFound);
        }


        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Suspension == suspension), "autos");

        await DataRepository.Suspension.RemoveAsync(suspension);
    }
}