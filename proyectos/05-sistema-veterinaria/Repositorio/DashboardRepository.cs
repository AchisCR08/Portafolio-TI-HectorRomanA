using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;

public class DashboardRepository
{
    private readonly VeterinariaDbContext _context;

    public DashboardRepository()
    {
        _context = new VeterinariaDbContext();
    }


    public DashboardKPI ObtenerKPI(int idSucursal)
    {
        using var conn = _context.CreateConnection();
        return conn.QueryFirstOrDefault<DashboardKPI>(
            "sp_Dashboard_ResumenKPI",
            new { id_sucursal = idSucursal },
            commandType: CommandType.StoredProcedure)
            ?? new DashboardKPI();
    }

    public IEnumerable<CitaDashboard> ObtenerCitasHoy(int idSucursal)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<CitaDashboard>(
            "sp_Dashboard_CitasHoy",
            new { id_sucursal = idSucursal },
            commandType: CommandType.StoredProcedure);
    }
}
