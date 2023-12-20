using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;

namespace backend.Services;

using static Validador;

public abstract class MarcaAutoService
{
    public static async Task<MarcaAuto> Create(MarcaAutoDto marca)
    {
        var create = new MarcaAuto
        {
            Nombre = marca.Nombre
        };

        await DataRepository.MarcaAuto.AddAsync(create);
        return create;
    }

    public static async Task<MarcaAuto> Find(int id)
    {
        var marca = await DataRepository.MarcaAuto.FindAsync(id);
        if (marca == null)
        {
            throw new ErrorDeCliente(" de auto no encontrada", HttpStatusCode.NotFound);
        }

        return marca;
    }


    public static Task<List<MarcaAuto>> GetAll()
    {
        return DataRepository.MarcaAuto.ToListAsync();
    }

    public static async Task<IResult> Update(int id, MarcaAutoDto marca)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Auto no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Nombre = marca.Nombre;
        
        await DataRepository.MarcaAuto.AddAsync(encontrado);
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var marcaAuto = await Find(id);

        if (marcaAuto == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Auto.AnyAsync(x => x.Marca == marcaAuto), "autos");

        await DataRepository.MarcaAuto.RemoveAsync(marcaAuto);
    }
}