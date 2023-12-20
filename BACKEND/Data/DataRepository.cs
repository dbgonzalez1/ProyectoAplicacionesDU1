using backend.Models;

namespace backend.Data;

public static class DataRepository
{
    
    public static RepositoryCollection<MarcaAuto> MarcaAuto { get; } = [];
    public static RepositoryCollection<Auto> Auto { get; } = [];
    public static RepositoryCollection<Proveedor> Proveedor { get; } = [];
    public static RepositoryCollection<Amortiguador> Amortiguador { get; } = [];
    public static RepositoryCollection<Frenos> Frenos { get; } = [];
    public static RepositoryCollection<Motor> Motor { get; } =[];
    public static RepositoryCollection<Neumatico> Neumatico { get; } = [];
    public static RepositoryCollection<Suspension> Suspension { get; } = [];
    public static RepositoryCollection<Transmision> Transmision { get; } = [];
}