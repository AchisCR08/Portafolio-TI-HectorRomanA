using System;

namespace Entidades
{
    /// Fila de vw_ClientesActivos
    public class ReporteClienteActivo
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Sucursal { get; set; } = "";
    }

    /// Fila de vw_MascotasDetalle
    public class ReporteMascotaDetalle
    {
        public int IdMascota { get; set; }
        public string Mascota { get; set; } = "";
        public string Sexo { get; set; } = "";
        public decimal? PesoActual { get; set; }
        public string Raza { get; set; } = "";
        public string TipoMascota { get; set; } = "";
        public string Tamano { get; set; } = "";
    }

    /// Fila de vw_DuenosMascotas
    public class ReporteDuenoMascota
    {
        public int IdPersona { get; set; }
        public string Dueno { get; set; } = "";
        public int IdMascota { get; set; }
        public string Mascota { get; set; } = "";
        public bool? EsPrincipal { get; set; }
    }

    /// Fila de vw_CitasVeterinarias
    public class ReporteCitaVeterinaria
    {
        public int IdCitaVet { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Motivo { get; set; } = "";
        public string Mascota { get; set; } = "";
        public string Veterinario { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Sucursal { get; set; } = "";
    }

    /// Fila de vw_CitasGrooming
    public class ReporteCitaGrooming
    {
        public int IdCitaGrooming { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public string Mascota { get; set; } = "";
        public string Servicio { get; set; } = "";
        public string Estado { get; set; } = "";
        public decimal? PrecioFinal { get; set; }
    }

    /// Fila de vw_VacunasPendientes
    public class ReporteVacunaPendiente
    {
        public int IdHistorial { get; set; }
        public string Mascota { get; set; } = "";
        public string Vacuna { get; set; } = "";
        public DateTime? FechaAplicacion { get; set; }
        public DateTime? ProximaDosis { get; set; }
    }

    /// Fila de vw_InventarioActual
    public class ReporteInventario
    {
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; } = "";
        public int IdProducto { get; set; }
        public string Producto { get; set; } = "";
        public int Stock { get; set; }
    }

    /// Fila de vw_CatalogoProductos
    public class ReporteProducto
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; } = "";
        public decimal PrecioVenta { get; set; }
        public string Categoria { get; set; } = "";
        public string Proveedor { get; set; } = "";
    }

    /// Fila de vw_FacturacionDetalle
    public class ReporteFactura
    {
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = "";
        public string Sucursal { get; set; } = "";
        public decimal Total { get; set; }
    }

    /// Fila de vw_FacturacionPorSucursal
    public class ReporteFacturacionSucursal
    {
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; } = "";
        public int CantidadFacturas { get; set; }
        public decimal? TotalFacturado { get; set; }
    }
}
