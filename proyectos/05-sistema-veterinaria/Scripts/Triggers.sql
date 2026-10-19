USE Veterinaria1;
GO

-- Trigger: validar peso positivo de mascota
CREATE OR ALTER TRIGGER dbo.trg_Mascota_ValidarPeso
ON Mascota
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar peso positivo al insertar o actualizar mascota
    IF EXISTS (SELECT 1 FROM inserted WHERE peso_actual IS NOT NULL AND peso_actual <= 0)
    BEGIN
        RAISERROR('El peso de la mascota debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

-- Trigger: eliminar dependencias de mascota
CREATE OR ALTER TRIGGER dbo.trg_Mascota_EliminarDependencias
ON Mascota
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Eliminar historial de vacunas de la mascota
    DELETE hv
    FROM HistorialVacuna hv
    INNER JOIN deleted d ON d.id_mascota = hv.id_mascota;

    -- Eliminar relacion entre mascota y duenos
    DELETE dm
    FROM Duenos_Mascotas dm
    INNER JOIN deleted d ON d.id_mascota = dm.id_mascota;

    -- Eliminar consultas asociadas a citas veterinarias de la mascota
    DELETE c
    FROM Consulta c
    INNER JOIN CitaVeterinaria cv ON cv.id_cita_vet = c.id_cita_vet
    INNER JOIN deleted d ON d.id_mascota = cv.id_mascota;

    -- Eliminar detalles de factura asociados a citas veterinarias
    DELETE df
    FROM DetalleFactura df
    INNER JOIN CitaVeterinaria cv ON cv.id_cita_vet = df.id_cita_vet
    INNER JOIN deleted d ON d.id_mascota = cv.id_mascota;

    -- Eliminar detalles de factura asociados a citas grooming
    DELETE df
    FROM DetalleFactura df
    INNER JOIN CitaGrooming cg ON cg.id_cita_grooming = df.id_cita_grooming
    INNER JOIN deleted d ON d.id_mascota = cg.id_mascota;

    -- Eliminar citas veterinarias de la mascota
    DELETE cv
    FROM CitaVeterinaria cv
    INNER JOIN deleted d ON d.id_mascota = cv.id_mascota;

    -- Eliminar citas grooming de la mascota
    DELETE cg
    FROM CitaGrooming cg
    INNER JOIN deleted d ON d.id_mascota = cg.id_mascota;

    -- Finalmente eliminar la mascota
    DELETE m
    FROM Mascota m
    INNER JOIN deleted d ON d.id_mascota = m.id_mascota;
END;
GO

-- Trigger: evitar choque de cita veterinaria
CREATE OR ALTER TRIGGER dbo.trg_CitaVeterinaria_ValidarDisponibilidad
ON CitaVeterinaria
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que no exista otra cita activa para el mismo veterinario en el mismo horario
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE dbo.fn_EsEstadoCitaFinal(i.id_estado_cita) = 0
          AND dbo.fn_EstaDisponibleCitaVeterinaria(i.fecha, i.hora, i.id_veterinario, i.id_sucursal, i.id_cita_vet) = 0
    )
    BEGIN
        RAISERROR('El veterinario no esta disponible en esa fecha y hora.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

-- Trigger: evitar choque de cita grooming y calcular precio
CREATE OR ALTER TRIGGER dbo.trg_CitaGrooming_ValidarDisponibilidad
ON CitaGrooming
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que no exista otra cita activa para el mismo groomer en el mismo horario
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE i.fecha IS NOT NULL
          AND i.hora IS NOT NULL
          AND dbo.fn_EsEstadoCitaFinal(i.id_estado_cita) = 0
          AND dbo.fn_EstaDisponibleCitaGrooming(i.fecha, i.hora, i.id_groomer, i.id_sucursal, i.id_cita_grooming) = 0
    )
    BEGIN
        RAISERROR('El groomer no esta disponible en esa fecha y hora.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Calcular precio de grooming si no se envio precio final
    UPDATE cg
    SET precio_final = dbo.fn_CalcularPrecioGrooming(i.id_servicio_grooming, i.id_mascota)
    FROM CitaGrooming cg
    INNER JOIN inserted i ON i.id_cita_grooming = cg.id_cita_grooming
    WHERE cg.precio_final IS NULL OR cg.precio_final <= 0;
END;
GO

-- Trigger: validar compra y aumentar stock
CREATE OR ALTER TRIGGER dbo.trg_DetalleCompra_ValidarYActualizarStock
ON DetalleCompra
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar cantidad y precio antes de aumentar inventario
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE ISNULL(cantidad, 0) <= 0
           OR ISNULL(precio_unitario, 0) <= 0
    )
    BEGIN
        RAISERROR('La cantidad y el precio de compra deben ser mayores a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Aumentar stock en la sucursal de la compra
    MERGE InventarioSucursal AS target
    USING (
        SELECT c.id_sucursal, i.id_producto, SUM(i.cantidad) AS cantidad
        FROM inserted i
        INNER JOIN Compra c ON c.id_compra = i.id_compra
        GROUP BY c.id_sucursal, i.id_producto
    ) AS source
    ON target.id_sucursal = source.id_sucursal
       AND target.id_producto = source.id_producto
    WHEN MATCHED THEN
        UPDATE SET stock = target.stock + source.cantidad
    WHEN NOT MATCHED THEN
        INSERT (id_sucursal, id_producto, stock)
        VALUES (source.id_sucursal, source.id_producto, source.cantidad);
END;
GO

-- Trigger: validar venta y rebajar stock
CREATE OR ALTER TRIGGER dbo.trg_DetalleFactura_ValidarVentaProducto
ON DetalleFactura
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar cantidad cuando el detalle vendido es un producto
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE id_producto IS NOT NULL
          AND ISNULL(cantidad, 0) <= 0
    )
    BEGIN
        RAISERROR('La cantidad vendida de un producto debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Validar precio unitario positivo para productos y servicios
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE precio_unitario <= 0
    )
    BEGIN
        RAISERROR('El precio unitario debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Rebajar stock por productos vendidos y revertir diferencia si se actualizo el detalle
    ;WITH Movimientos AS (
        SELECT f.id_sucursal, i.id_producto, -SUM(i.cantidad) AS cantidad
        FROM inserted i
        INNER JOIN Factura f ON f.id_factura = i.id_factura
        WHERE i.id_producto IS NOT NULL
        GROUP BY f.id_sucursal, i.id_producto

        UNION ALL

        SELECT f.id_sucursal, d.id_producto, SUM(d.cantidad) AS cantidad
        FROM deleted d
        INNER JOIN Factura f ON f.id_factura = d.id_factura
        WHERE d.id_producto IS NOT NULL
        GROUP BY f.id_sucursal, d.id_producto
    ),
    Totales AS (
        SELECT id_sucursal, id_producto, SUM(cantidad) AS cantidad
        FROM Movimientos
        GROUP BY id_sucursal, id_producto
    )
    UPDATE inv
    SET stock = inv.stock + t.cantidad
    FROM InventarioSucursal inv
    INNER JOIN Totales t
        ON t.id_sucursal = inv.id_sucursal
       AND t.id_producto = inv.id_producto;

    -- Validar que el stock no quede negativo despues de la venta
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Factura f ON f.id_factura = i.id_factura
        LEFT JOIN InventarioSucursal inv
            ON inv.id_sucursal = f.id_sucursal
           AND inv.id_producto = i.id_producto
        WHERE i.id_producto IS NOT NULL
          AND (inv.id_producto IS NULL OR inv.stock < 0)
    )
    BEGIN
        RAISERROR('No hay stock suficiente para vender el producto.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

-- Trigger: restaurar stock al eliminar detalle de factura
CREATE OR ALTER TRIGGER dbo.trg_DetalleFactura_RestaurarStock
ON DetalleFactura
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Devolver stock si se elimina un producto de una factura
    UPDATE inv
    SET stock = inv.stock + d.cantidad
    FROM InventarioSucursal inv
    INNER JOIN Factura f ON f.id_sucursal = inv.id_sucursal
    INNER JOIN deleted d
        ON d.id_factura = f.id_factura
       AND d.id_producto = inv.id_producto
    WHERE d.id_producto IS NOT NULL;
END;
GO

-- Trigger: recalcular total de factura
CREATE OR ALTER TRIGGER dbo.trg_DetalleFactura_ActualizarTotal
ON DetalleFactura
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Recalcular total de facturas afectadas por cambios en detalles
    ;WITH FacturasAfectadas AS (
        SELECT id_factura FROM inserted
        UNION
        SELECT id_factura FROM deleted
    )
    UPDATE f
    SET total = ISNULL(t.total, 0)
    FROM Factura f
    INNER JOIN FacturasAfectadas fa ON fa.id_factura = f.id_factura
    OUTER APPLY (
        SELECT SUM(CASE WHEN df.cantidad IS NULL THEN df.precio_unitario ELSE df.subtotal END) AS total
        FROM DetalleFactura df
        WHERE df.id_factura = f.id_factura
    ) t;
END;
GO

-- Trigger: validar monto de pago
CREATE OR ALTER TRIGGER dbo.trg_Pago_ValidarMonto
ON Pago
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar monto positivo al registrar o actualizar pagos
    IF EXISTS (SELECT 1 FROM inserted WHERE ISNULL(monto, 0) <= 0)
    BEGIN
        RAISERROR('El monto del pago debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

/* ============================================================
   ACTUALIZACIÓN 2026-07-06 — Triggers faltantes y correcciones
   Ejecutar sólo este bloque si la base ya tiene los triggers anteriores.
   ============================================================ */

-- CORRECCIÓN: trg_DetalleFactura_ValidarVentaProducto
-- Se agrega un guard al inicio para saltear la lógica de stock
-- cuando inserted viene vacío (operación DELETE pura).
-- El DELETE ya lo maneja trg_DetalleFactura_RestaurarStock.
CREATE OR ALTER TRIGGER dbo.trg_DetalleFactura_ValidarVentaProducto
ON DetalleFactura
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Guard: si no hay filas en inserted no hay nada que validar (DELETE puro)
    IF NOT EXISTS (SELECT 1 FROM inserted)
        RETURN;

    -- Validar cantidad cuando el detalle vendido es un producto
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE id_producto IS NOT NULL
          AND ISNULL(cantidad, 0) <= 0
    )
    BEGIN
        RAISERROR('La cantidad vendida de un producto debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Validar precio unitario positivo
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE precio_unitario <= 0
    )
    BEGIN
        RAISERROR('El precio unitario debe ser mayor a 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Rebajar stock por productos vendidos
    -- (en UPDATE se revierte la cantidad anterior y se aplica la nueva)
    ;WITH Movimientos AS (
        SELECT f.id_sucursal, i.id_producto, -SUM(i.cantidad) AS cantidad
        FROM inserted i
        INNER JOIN Factura f ON f.id_factura = i.id_factura
        WHERE i.id_producto IS NOT NULL
        GROUP BY f.id_sucursal, i.id_producto

        UNION ALL

        SELECT f.id_sucursal, d.id_producto, SUM(d.cantidad) AS cantidad
        FROM deleted d
        INNER JOIN Factura f ON f.id_factura = d.id_factura
        WHERE d.id_producto IS NOT NULL
        GROUP BY f.id_sucursal, d.id_producto
    ),
    Totales AS (
        SELECT id_sucursal, id_producto, SUM(cantidad) AS cantidad
        FROM Movimientos
        GROUP BY id_sucursal, id_producto
    )
    UPDATE inv
    SET stock = inv.stock + t.cantidad
    FROM InventarioSucursal inv
    INNER JOIN Totales t
        ON t.id_sucursal = inv.id_sucursal
       AND t.id_producto = inv.id_producto;

    -- Validar que el stock no quede negativo después de la venta
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Factura f ON f.id_factura = i.id_factura
        LEFT JOIN InventarioSucursal inv
            ON inv.id_sucursal = f.id_sucursal
           AND inv.id_producto = i.id_producto
        WHERE i.id_producto IS NOT NULL
          AND (inv.id_producto IS NULL OR inv.stock < 0)
    )
    BEGIN
        RAISERROR('No hay stock suficiente para vender el producto.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

-- NUEVO: evitar que la misma cita (veterinaria o grooming) se facture dos veces
CREATE OR ALTER TRIGGER dbo.trg_DetalleFactura_ValidarCitaUnica
ON DetalleFactura
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Cita veterinaria ya facturada en otra factura distinta
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN DetalleFactura df
            ON df.id_cita_vet = i.id_cita_vet
           AND df.id_detalle_factura <> i.id_detalle_factura
        WHERE i.id_cita_vet IS NOT NULL
    )
    BEGIN
        RAISERROR('La cita veterinaria ya fue facturada en otra factura.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Cita grooming ya facturada en otra factura distinta
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN DetalleFactura df
            ON df.id_cita_grooming = i.id_cita_grooming
           AND df.id_detalle_factura <> i.id_detalle_factura
        WHERE i.id_cita_grooming IS NOT NULL
    )
    BEGIN
        RAISERROR('La cita de grooming ya fue facturada en otra factura.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO
