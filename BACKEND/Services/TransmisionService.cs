using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;
using static backend.Services.Validador;

namespace backend.Services;

public abstract class TransmisionService
{
    public static async Task<Transmision> Create(TransmisionDto transmision)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(transmision.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        var creado = new Transmision
        {
            Tipo = transmision.Tipo,
            Marca = transmision.Marca,
            Modelo = transmision.Modelo,
            Relacion = transmision.Relacion,
            Eficiencia = transmision.Eficiencia,
            Proveedor = proveedor,
            Consumo = transmision.Consumo,
            NumeroDeMarchas = transmision.NumeroDeMarchas,
        };


        await DataRepository.Transmision.AddAsync(creado);

        return creado;
    }

    public static async Task<Transmision> Find(int id)
    {
        var transmision = await DataRepository.Transmision.FindAsync(id);
        if (transmision == null)
        {
            throw new ErrorDeCliente("Transmision no encontrado", HttpStatusCode.NotFound);
        }

        return transmision;
    }


    public static Task<List<Transmision>> GetAll()
    {
        return DataRepository.Transmision.ToListAsync();
    }

    public static async Task<IResult> Update(int id, TransmisionDto transmision)
    {
        var encontrado = await Find(id);

        var proveedor = await DataRepository.Proveedor.FindAsync(transmision.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Tipo = transmision.Tipo;
        encontrado.Marca = transmision.Marca;
        encontrado.Modelo = transmision.Modelo;
        encontrado.Relacion = transmision.Relacion;
        encontrado.Eficiencia = transmision.Eficiencia;
        encontrado.Consumo = transmision.Consumo;
        encontrado.NumeroDeMarchas = transmision.NumeroDeMarchas;


        await DataRepository.Transmision.UpdateAsync(encontrado);
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var transmision = await Find(id);

        if (transmision == null)
        {
            throw new ErrorDeCliente("Suspension no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Transmision == transmision), "autos");

        await DataRepository.Transmision.RemoveAsync(transmision);
    }
}