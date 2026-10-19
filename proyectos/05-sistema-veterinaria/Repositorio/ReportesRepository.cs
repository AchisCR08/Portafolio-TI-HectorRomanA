using Dapper;
using Entidades;
using System.Collections.Generic;

namespace Repositorio
{
    public class ReportesRepository
    {
        private readonly VeterinariaDbContext _context;

        public ReportesRepository()
        {
            _context = new VeterinariaDbContext();
        }

        public IEnumerable<ReporteClienteActivo> ClientesActivos()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteClienteActivo>("SELECT * FROM dbo.vw_ClientesActivos");
        }

        public IEnumerable<ReporteMascotaDetalle> MascotasDetalle()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteMascotaDetalle>("SELECT * FROM dbo.vw_MascotasDetalle");
        }

        public IEnumerable<ReporteDuenoMascota> DuenosMascotas()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteDuenoMascota>("SELECT * FROM dbo.vw_DuenosMascotas");
        }

        public IEnumerable<ReporteCitaVeterinaria> CitasVeterinarias()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteCitaVeterinaria>("SELECT * FROM dbo.vw_CitasVeterinarias");
        }

        public IEnumerable<ReporteCitaGrooming> CitasGrooming()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteCitaGrooming>("SELECT * FROM dbo.vw_CitasGrooming");
        }

        public IEnumerable<ReporteVacunaPendiente> VacunasPendientes()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteVacunaPendiente>("SELECT * FROM dbo.vw_VacunasPendientes");
        }

        public IEnumerable<ReporteInventario> InventarioActual()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteInventario>("SELECT * FROM dbo.vw_InventarioActual");
        }

        public IEnumerable<ReporteProducto> CatalogoProductos()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteProducto>("SELECT * FROM dbo.vw_CatalogoProductos");
        }

        public IEnumerable<ReporteFactura> FacturacionDetalle()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteFactura>("SELECT * FROM dbo.vw_FacturacionDetalle");
        }

        public IEnumerable<ReporteFacturacionSucursal> FacturacionPorSucursal()
        {
            using var conn = _context.CreateConnection();
            return conn.Query<ReporteFacturacionSucursal>("SELECT * FROM dbo.vw_FacturacionPorSucursal");
        }
    }
}
