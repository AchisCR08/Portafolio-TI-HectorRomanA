USE Veterinaria1;
GO

/* =====================================================================
   2.6 VISTAS
   Proyecto Integrador Final - Fundamentos de Bases de Datos
   Base de datos: Veterinaria1

   Estas vistas alimentan el modulo "Reportes" del sistema (FrmReportes).
   ===================================================================== */

-- Vista: clientes activos con su sucursal asignada
CREATE OR ALTER VIEW dbo.vw_ClientesActivos
AS
SELECT
    p.id_persona,
    p.nombre,
    p.telefono,
    p.correo,
    p.direccion,
    s.nombre AS sucursal
FROM Persona p
LEFT JOIN Sucursal s ON s.id_sucursal = p.id_sucursal
WHERE p.activo = 1;
GO

-- Vista: mascotas con raza, tipo y tamano
CREATE OR ALTER VIEW dbo.vw_MascotasDetalle
AS
SELECT
    m.id_mascota,
    m.nombre AS mascota,
    m.sexo,
    m.peso_actual,
    r.nombre AS raza,
    tm.nombre AS tipo_mascota,
    ta.nombre AS tamano
FROM Mascota m
INNER JOIN Raza r ON r.id_raza = m.id_raza
INNER JOIN TipoMascota tm ON tm.id_tipo_mascota = r.id_tipo_mascota
INNER JOIN TamanoMascota ta ON ta.id_tamano = r.id_tamano
WHERE m.activo = 1;
GO

-- Vista: relacion entre duenos y mascotas
CREATE OR ALTER VIEW dbo.vw_DuenosMascotas
AS
SELECT
    p.id_persona,
    p.nombre AS dueno,
    m.id_mascota,
    m.nombre AS mascota,
    dm.es_principal
FROM Duenos_Mascotas dm
INNER JOIN Persona p ON p.id_persona = dm.id_persona
INNER JOIN Mascota m ON m.id_mascota = dm.id_mascota;
GO

-- Vista: citas veterinarias con mascota, veterinario, estado y sucursal
CREATE OR ALTER VIEW dbo.vw_CitasVeterinarias
AS
SELECT
    cv.id_cita_vet,
    cv.fecha,
    cv.hora,
    cv.motivo,
    m.nombre AS mascota,
    v.nombre AS veterinario,
    ec.nombre AS estado,
    suc.nombre AS sucursal
FROM CitaVeterinaria cv
INNER JOIN Mascota m ON m.id_mascota = cv.id_mascota
INNER JOIN Persona v ON v.id_persona = cv.id_veterinario
INNER JOIN EstadoCita ec ON ec.id_estado_cita = cv.id_estado_cita
INNER JOIN Sucursal suc ON suc.id_sucursal = cv.id_sucursal;
GO

-- Vista: citas de grooming con servicio y precio final
CREATE OR ALTER VIEW dbo.vw_CitasGrooming
AS
SELECT
    cg.id_cita_grooming,
    cg.fecha,
    cg.hora,
    m.nombre AS mascota,
    sg.nombre AS servicio,
    ec.nombre AS estado,
    cg.precio_final
FROM CitaGrooming cg
INNER JOIN Mascota m ON m.id_mascota = cg.id_mascota
INNER JOIN ServicioGrooming sg ON sg.id_servicio_grooming = cg.id_servicio_grooming
INNER JOIN EstadoCita ec ON ec.id_estado_cita = cg.id_estado_cita;
GO

-- Vista: vacunas con proxima dosis pendiente (a partir de hoy)
CREATE OR ALTER VIEW dbo.vw_VacunasPendientes
AS
SELECT
    hv.id_historial,
    m.nombre AS mascota,
    v.nombre AS vacuna,
    hv.fecha_aplicacion,
    hv.proxima_dosis
FROM HistorialVacuna hv
INNER JOIN Mascota m ON m.id_mascota = hv.id_mascota
INNER JOIN Vacuna v ON v.id_vacuna = hv.id_vacuna
WHERE hv.proxima_dosis >= CAST(GETDATE() AS DATE);
GO

-- Vista: inventario actual por sucursal
CREATE OR ALTER VIEW dbo.vw_InventarioActual
AS
SELECT
    suc.id_sucursal,
    suc.nombre AS sucursal,
    pr.id_producto,
    pr.nombre AS producto,
    inv.stock
FROM InventarioSucursal inv
INNER JOIN Sucursal suc ON suc.id_sucursal = inv.id_sucursal
INNER JOIN Producto pr ON pr.id_producto = inv.id_producto;
GO

-- Vista: catalogo de productos con categoria y proveedor
CREATE OR ALTER VIEW dbo.vw_CatalogoProductos
AS
SELECT
    pr.id_producto,
    pr.nombre AS producto,
    pr.precio_venta,
    c.nombre AS categoria,
    pv.nombre AS proveedor
FROM Producto pr
INNER JOIN Categoria c ON c.id_categoria = pr.id_categoria
INNER JOIN Proveedor pv ON pv.id_proveedor = pr.id_proveedor
WHERE pr.activo = 1;
GO

-- Vista: facturas con cliente y sucursal
CREATE OR ALTER VIEW dbo.vw_FacturacionDetalle
AS
SELECT
    f.id_factura,
    f.fecha,
    p.nombre AS cliente,
    s.nombre AS sucursal,
    f.total
FROM Factura f
INNER JOIN Persona p ON p.id_persona = f.id_persona
INNER JOIN Sucursal s ON s.id_sucursal = f.id_sucursal;
GO

-- Vista: total facturado agrupado por sucursal
CREATE OR ALTER VIEW dbo.vw_FacturacionPorSucursal
AS
SELECT
    s.id_sucursal,
    s.nombre AS sucursal,
    COUNT(f.id_factura) AS cantidad_facturas,
    SUM(f.total) AS total_facturado
FROM Factura f
INNER JOIN Sucursal s ON s.id_sucursal = f.id_sucursal
GROUP BY s.id_sucursal, s.nombre;
GO
