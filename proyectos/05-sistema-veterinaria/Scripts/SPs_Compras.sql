USE Veterinaria1;
GO

-- ============================================================
-- EJECUTAR SOLO ESTO si los SPs de compras aun no existen
-- ============================================================

CREATE OR ALTER PROCEDURE dbo.sp_Compra_Registrar
    @fecha        DATE,
    @id_proveedor INT,
    @id_sucursal  INT,
    @id_compra    INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Compra (fecha, id_proveedor, id_sucursal)
    VALUES (@fecha, @id_proveedor, @id_sucursal);
    SET @id_compra = SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Compra_AgregarDetalle
    @id_compra       INT,
    @id_producto     INT,
    @cantidad        INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DetalleCompra (id_compra, id_producto, cantidad, precio_unitario)
    VALUES (@id_compra, @id_producto, @cantidad, @precio_unitario);
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Compra_Listar
    @id_sucursal  INT  = NULL,
    @fecha_inicio DATE = NULL,
    @fecha_fin    DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        c.id_compra     AS IdCompra,
        c.fecha         AS Fecha,
        c.id_proveedor  AS IdProveedor,
        p.nombre        AS ProveedorNombre,
        c.id_sucursal   AS IdSucursal,
        s.nombre        AS SucursalNombre,
        ISNULL((
            SELECT SUM(dc.cantidad * dc.precio_unitario)
            FROM DetalleCompra dc
            WHERE dc.id_compra = c.id_compra
        ), 0) AS Total
    FROM Compra c
    INNER JOIN Proveedor p ON p.id_proveedor = c.id_proveedor
    INNER JOIN Sucursal  s ON s.id_sucursal  = c.id_sucursal
    WHERE (@id_sucursal  IS NULL OR c.id_sucursal = @id_sucursal)
      AND (@fecha_inicio IS NULL OR c.fecha >= @fecha_inicio)
      AND (@fecha_fin    IS NULL OR c.fecha <= @fecha_fin)
    ORDER BY c.fecha DESC, c.id_compra DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Compra_ObtenerDetalle
    @id_compra INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        dc.id_detalle_compra AS IdDetalleCompra,
        dc.id_compra         AS IdCompra,
        dc.id_producto       AS IdProducto,
        pr.nombre            AS ProductoNombre,
        dc.cantidad          AS Cantidad,
        dc.precio_unitario   AS PrecioUnitario,
        dc.cantidad * dc.precio_unitario AS Subtotal
    FROM DetalleCompra dc
    INNER JOIN Producto pr ON pr.id_producto = dc.id_producto
    WHERE dc.id_compra = @id_compra
    ORDER BY dc.id_detalle_compra;
END;
GO

PRINT '✅ SPs de Compras creados correctamente en Veterinaria1';
