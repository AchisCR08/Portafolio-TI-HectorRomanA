USE Veterinaria1;
GO

-- Funcion: identificar si una cita libera agenda
CREATE OR ALTER FUNCTION dbo.fn_EsEstadoCitaFinal
(
    @id_estado_cita INT
)
RETURNS BIT
AS
BEGIN
    DECLARE @es_final BIT = 0;

    -- Validar si el estado deja libre el espacio de agenda
    SELECT @es_final =
        CASE
            WHEN UPPER(nombre) IN ('CANCELADA', 'CANCELADO', 'COMPLETADA', 'COMPLETADO') THEN 1
            ELSE 0
        END
    FROM EstadoCita
    WHERE id_estado_cita = @id_estado_cita;

    RETURN ISNULL(@es_final, 0);
END;
GO

-- Funcion: validar disponibilidad de veterinario
CREATE OR ALTER FUNCTION dbo.fn_EstaDisponibleCitaVeterinaria
(
    @fecha DATE,
    @hora TIME,
    @id_veterinario INT,
    @id_sucursal INT,
    @id_cita_vet_excluir INT = NULL
)
RETURNS BIT
AS
BEGIN
    DECLARE @disponible BIT = 1;

    -- Buscar otra cita veterinaria activa con el mismo veterinario, fecha, hora y sucursal
    IF EXISTS (
        SELECT 1
        FROM CitaVeterinaria cv
        INNER JOIN EstadoCita ec ON ec.id_estado_cita = cv.id_estado_cita
        WHERE cv.fecha = @fecha
          AND cv.hora = @hora
          AND cv.id_veterinario = @id_veterinario
          AND cv.id_sucursal = @id_sucursal
          AND (@id_cita_vet_excluir IS NULL OR cv.id_cita_vet <> @id_cita_vet_excluir)
          AND UPPER(ec.nombre) NOT IN ('CANCELADA', 'CANCELADO', 'COMPLETADA', 'COMPLETADO')
    )
    BEGIN
        SET @disponible = 0;
    END;

    RETURN @disponible;
END;
GO

-- Funcion: validar disponibilidad de groomer
CREATE OR ALTER FUNCTION dbo.fn_EstaDisponibleCitaGrooming
(
    @fecha DATE,
    @hora TIME,
    @id_groomer INT,
    @id_sucursal INT,
    @id_cita_grooming_excluir INT = NULL
)
RETURNS BIT
AS
BEGIN
    DECLARE @disponible BIT = 1;

    -- Buscar otra cita de grooming activa con el mismo groomer, fecha, hora y sucursal
    IF EXISTS (
        SELECT 1
        FROM CitaGrooming cg
        INNER JOIN EstadoCita ec ON ec.id_estado_cita = cg.id_estado_cita
        WHERE cg.fecha = @fecha
          AND cg.hora = @hora
          AND cg.id_groomer = @id_groomer
          AND cg.id_sucursal = @id_sucursal
          AND (@id_cita_grooming_excluir IS NULL OR cg.id_cita_grooming <> @id_cita_grooming_excluir)
          AND UPPER(ec.nombre) NOT IN ('CANCELADA', 'CANCELADO', 'COMPLETADA', 'COMPLETADO')
    )
    BEGIN
        SET @disponible = 0;
    END;

    RETURN @disponible;
END;
GO

-- Funcion: consultar stock disponible
CREATE OR ALTER FUNCTION dbo.fn_StockDisponible
(
    @id_sucursal INT,
    @id_producto INT
)
RETURNS INT
AS
BEGIN
    DECLARE @stock INT = 0;

    -- Obtener stock actual del producto en una sucursal
    SELECT @stock = stock
    FROM InventarioSucursal
    WHERE id_sucursal = @id_sucursal
      AND id_producto = @id_producto;

    RETURN ISNULL(@stock, 0);
END;
GO

-- Funcion: calcular precio de grooming
CREATE OR ALTER FUNCTION dbo.fn_CalcularPrecioGrooming
(
    @id_servicio_grooming INT,
    @id_mascota INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @precio DECIMAL(10,2);

    -- Precio base ajustado por tamano y tipo de pelaje de la mascota
    SELECT @precio =
        ISNULL(sg.precio_base, 0) *
        ISNULL(tm.factor_precio, 1) *
        ISNULL(tp.factor_precio, 1)
    FROM ServicioGrooming sg
    CROSS JOIN Mascota m
    INNER JOIN Raza r ON r.id_raza = m.id_raza
    INNER JOIN TamanoMascota tm ON tm.id_tamano = r.id_tamano
    INNER JOIN TipoPelaje tp ON tp.id_tipo_pelaje = r.id_tipo_pelaje
    WHERE sg.id_servicio_grooming = @id_servicio_grooming
      AND m.id_mascota = @id_mascota;

    RETURN ISNULL(@precio, 0);
END;
GO

-- Funcion: calcular edad de la mascota en anos
CREATE OR ALTER FUNCTION dbo.fn_EdadMascota
(
    @id_mascota INT
)
RETURNS INT
AS
BEGIN
    DECLARE @edad INT;

    -- Calcular anos completos desde la fecha de nacimiento
    SELECT @edad = DATEDIFF(YEAR, m.fecha_nacimiento, GETDATE())
        - CASE
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, m.fecha_nacimiento, GETDATE()), m.fecha_nacimiento) > GETDATE()
            THEN 1 ELSE 0
          END
    FROM Mascota m
    WHERE m.id_mascota = @id_mascota
      AND m.fecha_nacimiento IS NOT NULL;

    RETURN @edad;
END;
GO

-- Funcion: obtener la proxima dosis de vacuna pendiente de una mascota
CREATE OR ALTER FUNCTION dbo.fn_ProximaDosisPendiente
(
    @id_mascota INT
)
RETURNS DATE
AS
BEGIN
    DECLARE @proxima_dosis DATE;

    -- Buscar la dosis pendiente mas cercana a partir de hoy
    SELECT @proxima_dosis = MIN(hv.proxima_dosis)
    FROM HistorialVacuna hv
    WHERE hv.id_mascota = @id_mascota
      AND hv.proxima_dosis >= CAST(GETDATE() AS DATE);

    RETURN @proxima_dosis;
END;
GO

-- Funcion: calcular el total de una factura a partir de su detalle
CREATE OR ALTER FUNCTION dbo.fn_TotalFactura
(
    @id_factura INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total DECIMAL(10,2);

    -- Sumar los subtotales de todas las lineas de la factura
    SELECT @total = SUM(df.subtotal)
    FROM DetalleFactura df
    WHERE df.id_factura = @id_factura;

    RETURN ISNULL(@total, 0);
END;
GO

-- Funcion: dias transcurridos desde la ultima visita veterinaria de una mascota
CREATE OR ALTER FUNCTION dbo.fn_DiasDesdeUltimaVisita
(
    @id_mascota INT
)
RETURNS INT
AS
BEGIN
    DECLARE @ultima_fecha DATE;

    -- Buscar la fecha de la cita veterinaria mas reciente
    SELECT @ultima_fecha = MAX(cv.fecha)
    FROM CitaVeterinaria cv
    WHERE cv.id_mascota = @id_mascota;

    RETURN DATEDIFF(DAY, @ultima_fecha, CAST(GETDATE() AS DATE));
END;
GO

-- Funcion: calcular el total historico gastado por un cliente
CREATE OR ALTER FUNCTION dbo.fn_TotalGastadoCliente
(
    @id_persona INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total DECIMAL(10,2);

    -- Sumar el total de todas las facturas del cliente
    SELECT @total = SUM(f.total)
    FROM Factura f
    WHERE f.id_persona = @id_persona;

    RETURN ISNULL(@total, 0);
END;
GO

