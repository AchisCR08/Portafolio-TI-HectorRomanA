USE [Telefonia];
GO

/* ==========================================================
   ALCANCE 2 - PROVEEDOR SQL SERVER

   Este script es incremental: no elimina tablas ni datos.
   Agrega lo necesario para implementar:

   - PROVEEDOR4: registrar lineas telefonicas disponibles.
   - PROVEEDOR5: activar/desactivar lineas vendidas.
   - PROVEEDOR6: calcular facturacion postpago.

   Ejecutar despues del script base del proveedor.
   ========================================================== */

/* ==========================================================
   1. Ampliacion de tabla Cuentas
   ========================================================== */

IF COL_LENGTH('dbo.Cuentas', 'numero_telefono_cifrado') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD numero_telefono_cifrado VARCHAR(300) NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'identificador_telefono') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD identificador_telefono VARCHAR(300) NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'identificador_tarjeta') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD identificador_tarjeta VARCHAR(300) NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'identificacion_duenno') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD identificacion_duenno VARCHAR(50) NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'estado') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD estado VARCHAR(20) NOT NULL
        CONSTRAINT DF_Cuentas_Estado DEFAULT ('ACTIVO');
END
GO

IF COL_LENGTH('dbo.Cuentas', 'fecha_activacion') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD fecha_activacion DATETIME NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'fecha_desactivacion') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD fecha_desactivacion DATETIME NULL;
END
GO

IF COL_LENGTH('dbo.Cuentas', 'saldo_postpago') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD saldo_postpago DECIMAL(14,2) NOT NULL
        CONSTRAINT DF_Cuentas_SaldoPostpago DEFAULT (0.00);
END
GO

IF COL_LENGTH('dbo.Cuentas', 'fecha_maxima_pago') IS NULL
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD fecha_maxima_pago DATE NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE name = 'CK_Cuentas_Estado'
      AND parent_object_id = OBJECT_ID('dbo.Cuentas')
)
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD CONSTRAINT CK_Cuentas_Estado
        CHECK (estado IN ('DISPONIBLE', 'ACTIVO', 'INACTIVO'));
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE name = 'CK_Cuentas_SaldoPostpago_NoNegativo'
      AND parent_object_id = OBJECT_ID('dbo.Cuentas')
)
BEGIN
    ALTER TABLE dbo.Cuentas
    ADD CONSTRAINT CK_Cuentas_SaldoPostpago_NoNegativo
        CHECK (saldo_postpago >= 0);
END
GO

/* ==========================================================
   2. Tabla para facturacion postpago
   ========================================================== */

IF OBJECT_ID('dbo.FacturasPostpago', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FacturasPostpago
    (
        id INT IDENTITY(1,1) PRIMARY KEY,
        numero_telefono VARCHAR(15) NOT NULL,
        fecha_calculo DATE NOT NULL,
        fecha_maxima_pago DATE NOT NULL,
        monto DECIMAL(14,2) NOT NULL,
        estado VARCHAR(20) NOT NULL
            CONSTRAINT DF_FacturasPostpago_Estado DEFAULT ('PENDIENTE'),
        fecha_registro DATETIME NOT NULL
            CONSTRAINT DF_FacturasPostpago_FechaRegistro DEFAULT GETDATE(),

        CONSTRAINT FK_FacturasPostpago_Cuentas
            FOREIGN KEY (numero_telefono)
            REFERENCES dbo.Cuentas(numero_telefono),

        CONSTRAINT CK_FacturasPostpago_Monto_NoNegativo
            CHECK (monto >= 0),

        CONSTRAINT CK_FacturasPostpago_Estado
            CHECK (estado IN ('PENDIENTE', 'PAGADA', 'ANULADA'))
    );
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_FacturasPostpago_Telefono_Fecha'
      AND object_id = OBJECT_ID('dbo.FacturasPostpago')
)
BEGIN
    CREATE UNIQUE INDEX UX_FacturasPostpago_Telefono_Fecha
    ON dbo.FacturasPostpago(numero_telefono, fecha_calculo);
END
GO

/* ==========================================================
   3. Procedimiento almacenado PROVEEDOR6

   Calcula el total facturable para cada cuenta POSTPAGO activa.
   El monto se obtiene de Llamadas.costo_total, donde costo_total
   esta almacenado en centesimas de colon.

   Si ya existe factura para la misma fecha de calculo, la actualiza.
   ========================================================== */

CREATE OR ALTER PROCEDURE dbo.sp_calcular_facturacion_postpago
    @fecha_calculo DATE,
    @fecha_maxima_pago DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF @fecha_calculo IS NULL OR @fecha_maxima_pago IS NULL
    BEGIN
        THROW 50001, 'Fechas incompletas.', 1;
    END;

    IF @fecha_maxima_pago < @fecha_calculo
    BEGIN
        THROW 50002, 'La fecha maxima de pago no puede ser menor a la fecha de calculo.', 1;
    END;

    ;WITH Totales AS
    (
        SELECT
            c.numero_telefono,
            CAST(ISNULL(SUM(l.costo_total), 0) / 100.0 AS DECIMAL(14,2)) AS monto
        FROM dbo.Cuentas c
        LEFT JOIN dbo.Llamadas l
            ON l.telefono_origen = c.numero_telefono
        WHERE c.tipo_servicio = 'POSTPAGO'
          AND c.estado = 'ACTIVO'
        GROUP BY c.numero_telefono
    )
    MERGE dbo.FacturasPostpago AS destino
    USING Totales AS origen
       ON destino.numero_telefono = origen.numero_telefono
      AND destino.fecha_calculo = @fecha_calculo
    WHEN MATCHED THEN
        UPDATE SET
            destino.fecha_maxima_pago = @fecha_maxima_pago,
            destino.monto = origen.monto,
            destino.estado = 'PENDIENTE',
            destino.fecha_registro = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (numero_telefono, fecha_calculo, fecha_maxima_pago, monto, estado)
        VALUES (origen.numero_telefono, @fecha_calculo, @fecha_maxima_pago, origen.monto, 'PENDIENTE');

    UPDATE c
       SET c.saldo_postpago = f.monto,
           c.fecha_maxima_pago = f.fecha_maxima_pago
    FROM dbo.Cuentas c
    INNER JOIN dbo.FacturasPostpago f
        ON f.numero_telefono = c.numero_telefono
       AND f.fecha_calculo = @fecha_calculo
    WHERE c.tipo_servicio = 'POSTPAGO';
END
GO

/* ==========================================================
   4. Datos de prueba para PROVEEDOR4 y PROVEEDOR5

   Linea disponible que puede usarse para probar activacion.
   Los campos cifrados quedan NULL hasta que Java implemente AES.
   ========================================================== */

IF NOT EXISTS (SELECT 1 FROM dbo.Cuentas WHERE numero_telefono = '89990000')
BEGIN
    INSERT INTO dbo.Cuentas
    (
        numero_telefono,
        tipo_servicio,
        saldo,
        proveedor,
        bono_mismo_proveedor,
        estado,
        identificacion_duenno
    )
    VALUES
    (
        '89990000',
        'PREPAGO',
        0.00,
        'XYZ',
        0.00,
        'DISPONIBLE',
        NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Cuentas WHERE numero_telefono = '22223333')
BEGIN
    INSERT INTO dbo.Cuentas
    (
        numero_telefono,
        tipo_servicio,
        saldo,
        proveedor,
        bono_mismo_proveedor,
        estado,
        identificacion_duenno
    )
    VALUES
    (
        '22223333',
        'POSTPAGO',
        0.00,
        'XYZ',
        0.00,
        'DISPONIBLE',
        NULL
    );
END
GO

/* ==========================================================
   5. Consultas de verificacion
   ========================================================== */

SELECT
    numero_telefono,
    tipo_servicio,
    saldo,
    bono_mismo_proveedor,
    proveedor,
    estado,
    identificacion_duenno,
    saldo_postpago,
    fecha_maxima_pago
FROM dbo.Cuentas
ORDER BY numero_telefono;
GO

SELECT
    id,
    numero_telefono,
    fecha_calculo,
    fecha_maxima_pago,
    monto,
    estado,
    fecha_registro
FROM dbo.FacturasPostpago
ORDER BY fecha_registro DESC;
GO

/* ==========================================================
   Ejemplo manual para probar PROVEEDOR6 desde SQL Server:

   EXEC dbo.sp_calcular_facturacion_postpago
        @fecha_calculo = '2026-07-20',
        @fecha_maxima_pago = '2026-07-30';
   ========================================================== */
