USE Veterinaria1;
GO

/* =====================================================================
   2.9 OPERACIONES CRUD
   Proyecto Integrador Final - Fundamentos de Bases de Datos
   Base de datos: Veterinaria1

   Este script demuestra las operaciones basicas de manipulacion de
   datos (INSERT, UPDATE, DELETE, SELECT) directamente sobre las
   tablas de la base de datos, tal como lo solicita el apartado 2.9
   del proyecto.
   ===================================================================== */


/* ---------------------------------------------------------------------
   1. INSERT - Anadir nuevos registros
   --------------------------------------------------------------------- */

-- Nueva provincia, canton, distrito y sucursal (para tener datos de prueba)
INSERT INTO Provincia (nombre) VALUES ('Cartago');
INSERT INTO Canton (nombre, id_provincia)
VALUES ('Oreamuno', (SELECT id_provincia FROM Provincia WHERE nombre = 'Cartago'));
INSERT INTO Distrito (nombre, id_canton)
VALUES ('Cot', (SELECT id_canton FROM Canton WHERE nombre = 'Oreamuno'));

INSERT INTO Sucursal (nombre, telefono, correo, direccion, id_distrito, activo)
VALUES ('Veterinaria1 - Sede Cot', '2591-0000', 'cot@veterinaria1.com',
        '200 m norte de la plaza de deportes',
        (SELECT id_distrito FROM Distrito WHERE nombre = 'Cot'), 1);

-- Nuevo rol
INSERT INTO Rol (nombre, activo) VALUES ('Recepcionista', 1);

-- Nueva persona (cliente) y su mascota
INSERT INTO Persona (nombre, telefono, correo, direccion, id_distrito, id_sucursal, activo)
VALUES ('Mariana Solano Vega', '8888-1234', 'mariana.solano@correo.com',
        'Residencial Los Alamos, casa 12',
        (SELECT id_distrito FROM Distrito WHERE nombre = 'Cot'),
        (SELECT id_sucursal FROM Sucursal WHERE nombre = 'Veterinaria1 - Sede Cot'), 1);

INSERT INTO Mascota (nombre, fecha_nacimiento, sexo, peso_actual, id_raza, activo)
VALUES ('Rocky', '2022-03-15', 'M', 8.50,
        (SELECT TOP 1 id_raza FROM Raza ORDER BY id_raza), 1);

INSERT INTO Duenos_Mascotas (id_persona, id_mascota, es_principal)
VALUES (
    (SELECT id_persona FROM Persona WHERE correo = 'mariana.solano@correo.com'),
    (SELECT id_mascota FROM Mascota WHERE nombre = 'Rocky'),
    1
);

-- Nuevo producto de inventario
INSERT INTO Producto (nombre, descripcion, precio_venta, id_categoria, id_proveedor, activo)
VALUES ('Shampoo Antipulgas 500ml', 'Shampoo medicado para control de pulgas y garrapatas',
        6500.00,
        (SELECT TOP 1 id_categoria FROM Categoria ORDER BY id_categoria),
        (SELECT TOP 1 id_proveedor FROM Proveedor ORDER BY id_proveedor), 1);

-- Nueva cita veterinaria para la mascota recien creada
INSERT INTO CitaVeterinaria (fecha, hora, motivo, id_mascota, id_veterinario, id_estado_cita, id_sucursal)
VALUES (
    DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '10:30:00', 'Control de rutina y vacunacion',
    (SELECT id_mascota FROM Mascota WHERE nombre = 'Rocky'),
    (SELECT TOP 1 id_persona FROM Persona ORDER BY id_persona),
    (SELECT TOP 1 id_estado_cita FROM EstadoCita ORDER BY id_estado_cita),
    (SELECT id_sucursal FROM Sucursal WHERE nombre = 'Veterinaria1 - Sede Cot')
);
GO


/* ---------------------------------------------------------------------
   2. UPDATE - Modificar registros existentes (minimo 10 operaciones)
   --------------------------------------------------------------------- */

-- 1) Actualizar telefono y correo de la clienta recien creada
UPDATE Persona
SET telefono = '8888-5678', correo = 'mariana.solano@correo.com'
WHERE correo = 'mariana.solano@correo.com';

-- 2) Actualizar peso de la mascota tras un control
UPDATE Mascota
SET peso_actual = 9.10
WHERE nombre = 'Rocky';

-- 3) Marcar la mascota principal en la relacion Duenos_Mascotas
UPDATE Duenos_Mascotas
SET es_principal = 1
WHERE id_persona = (SELECT id_persona FROM Persona WHERE correo = 'mariana.solano@correo.com')
  AND id_mascota = (SELECT id_mascota FROM Mascota WHERE nombre = 'Rocky');

-- 4) Ajustar el precio de venta de un producto
UPDATE Producto
SET precio_venta = 6900.00
WHERE nombre = 'Shampoo Antipulgas 500ml';

-- 5) Desactivar un producto que ya no se vende
UPDATE Producto
SET activo = 0
WHERE nombre = 'Shampoo Antipulgas 500ml' AND precio_venta = 6900.00;

-- 6) Reactivarlo nuevamente (ejemplo de update de estado)
UPDATE Producto
SET activo = 1
WHERE nombre = 'Shampoo Antipulgas 500ml';

-- 7) Cambiar el estado de una cita veterinaria a "Confirmada" (usa la primera cita disponible)
UPDATE CitaVeterinaria
SET id_estado_cita = (SELECT TOP 1 id_estado_cita FROM EstadoCita ORDER BY id_estado_cita)
WHERE id_cita_vet = (SELECT TOP 1 id_cita_vet FROM CitaVeterinaria ORDER BY id_cita_vet DESC);

-- 8) Actualizar el motivo de esa misma cita
UPDATE CitaVeterinaria
SET motivo = 'Control de rutina, vacunacion y desparasitacion'
WHERE id_cita_vet = (SELECT TOP 1 id_cita_vet FROM CitaVeterinaria ORDER BY id_cita_vet DESC);

-- 9) Actualizar el stock de un producto en una sucursal (si existe inventario registrado)
UPDATE InventarioSucursal
SET stock = stock + 20
WHERE id_producto = (SELECT TOP 1 id_producto FROM Producto ORDER BY id_producto)
  AND id_sucursal = (SELECT TOP 1 id_sucursal FROM Sucursal ORDER BY id_sucursal);

-- 10) Actualizar el nombre de la sucursal recien creada
UPDATE Sucursal
SET nombre = 'Veterinaria1 - Sede Cot Centro'
WHERE nombre = 'Veterinaria1 - Sede Cot';

-- 11) Actualizar la direccion de dicha sucursal
UPDATE Sucursal
SET direccion = '100 m este de la iglesia catolica de Cot'
WHERE nombre = 'Veterinaria1 - Sede Cot Centro';

-- 12) Actualizar el nombre del rol creado
UPDATE Rol
SET nombre = 'Recepcionista de Sucursal'
WHERE nombre = 'Recepcionista';
GO


/* ---------------------------------------------------------------------
   3. DELETE - Eliminar registros de forma segura (minimo 5 operaciones)
   --------------------------------------------------------------------- */

-- 1) Eliminar la cita veterinaria de prueba creada en el INSERT
DELETE FROM CitaVeterinaria
WHERE id_cita_vet = (SELECT TOP 1 id_cita_vet FROM CitaVeterinaria
                      WHERE motivo LIKE 'Control de rutina%' ORDER BY id_cita_vet DESC);

-- 2) Eliminar la relacion dueno-mascota de prueba
DELETE FROM Duenos_Mascotas
WHERE id_persona = (SELECT id_persona FROM Persona WHERE correo = 'mariana.solano@correo.com')
  AND id_mascota = (SELECT id_mascota FROM Mascota WHERE nombre = 'Rocky');

-- 3) Eliminar la mascota de prueba (ya sin citas ni duenos asociados)
DELETE FROM Mascota
WHERE nombre = 'Rocky';

-- 4) Eliminar el producto de prueba
DELETE FROM Producto
WHERE nombre = 'Shampoo Antipulgas 500ml';

-- 5) Eliminar la persona (cliente) de prueba
DELETE FROM Persona
WHERE correo = 'mariana.solano@correo.com';

-- 6) Eliminar el rol de prueba creado
DELETE FROM Rol
WHERE nombre = 'Recepcionista de Sucursal';

-- 7) Eliminar la sucursal, distrito, canton y provincia de prueba (en orden por las llaves foraneas)
DELETE FROM Sucursal WHERE nombre = 'Veterinaria1 - Sede Cot Centro';
DELETE FROM Distrito WHERE nombre = 'Cot';
DELETE FROM Canton WHERE nombre = 'Oreamuno';
DELETE FROM Provincia WHERE nombre = 'Cartago';
GO


/* ---------------------------------------------------------------------
   4. SELECT - Consultar y extraer datos (minimo 10 consultas)
   --------------------------------------------------------------------- */

-- 1) Listado de clientes activos con su sucursal asignada
SELECT p.id_persona, p.nombre, p.telefono, p.correo, s.nombre AS sucursal
FROM Persona p
LEFT JOIN Sucursal s ON s.id_sucursal = p.id_sucursal
WHERE p.activo = 1
ORDER BY p.nombre;

-- 2) Mascotas registradas junto con su raza, tipo y tamano
SELECT m.id_mascota, m.nombre AS mascota, r.nombre AS raza,
       tm.nombre AS tipo_mascota, ta.nombre AS tamano
FROM Mascota m
INNER JOIN Raza r ON r.id_raza = m.id_raza
INNER JOIN TipoMascota tm ON tm.id_tipo_mascota = r.id_tipo_mascota
INNER JOIN TamanoMascota ta ON ta.id_tamano = r.id_tamano
WHERE m.activo = 1;

-- 3) Duenos y sus mascotas (relacion muchos a muchos)
SELECT p.nombre AS dueno, m.nombre AS mascota, dm.es_principal
FROM Duenos_Mascotas dm
INNER JOIN Persona p ON p.id_persona = dm.id_persona
INNER JOIN Mascota m ON m.id_mascota = dm.id_mascota
ORDER BY p.nombre;

-- 4) Citas veterinarias programadas con datos de mascota, veterinario y estado
SELECT cv.id_cita_vet, cv.fecha, cv.hora, m.nombre AS mascota,
       v.nombre AS veterinario, ec.nombre AS estado, suc.nombre AS sucursal
FROM CitaVeterinaria cv
INNER JOIN Mascota m ON m.id_mascota = cv.id_mascota
INNER JOIN Persona v ON v.id_persona = cv.id_veterinario
INNER JOIN EstadoCita ec ON ec.id_estado_cita = cv.id_estado_cita
INNER JOIN Sucursal suc ON suc.id_sucursal = cv.id_sucursal
ORDER BY cv.fecha, cv.hora;

-- 5) Citas de grooming con el servicio y precio final
SELECT cg.id_cita_grooming, cg.fecha, m.nombre AS mascota,
       sg.nombre AS servicio, cg.precio_final
FROM CitaGrooming cg
INNER JOIN Mascota m ON m.id_mascota = cg.id_mascota
INNER JOIN ServicioGrooming sg ON sg.id_servicio_grooming = cg.id_servicio_grooming
ORDER BY cg.fecha DESC;

-- 6) Historial de vacunas por mascota, con proxima dosis pendiente
SELECT m.nombre AS mascota, v.nombre AS vacuna,
       hv.fecha_aplicacion, hv.proxima_dosis
FROM HistorialVacuna hv
INNER JOIN Mascota m ON m.id_mascota = hv.id_mascota
INNER JOIN Vacuna v ON v.id_vacuna = hv.id_vacuna
WHERE hv.proxima_dosis >= CAST(GETDATE() AS DATE)
ORDER BY hv.proxima_dosis;

-- 7) Inventario actual de productos por sucursal, ordenado por stock mas bajo
SELECT suc.nombre AS sucursal, pr.nombre AS producto, inv.stock
FROM InventarioSucursal inv
INNER JOIN Sucursal suc ON suc.id_sucursal = inv.id_sucursal
INNER JOIN Producto pr ON pr.id_producto = inv.id_producto
ORDER BY inv.stock ASC;

-- 8) Productos con su categoria y proveedor
SELECT pr.nombre AS producto, pr.precio_venta, c.nombre AS categoria, pv.nombre AS proveedor
FROM Producto pr
INNER JOIN Categoria c ON c.id_categoria = pr.id_categoria
INNER JOIN Proveedor pv ON pv.id_proveedor = pr.id_proveedor
WHERE pr.activo = 1;

-- 9) Facturas emitidas con el total y el cliente asociado
SELECT f.id_factura, f.fecha, p.nombre AS cliente, s.nombre AS sucursal, f.total
FROM Factura f
INNER JOIN Persona p ON p.id_persona = f.id_persona
INNER JOIN Sucursal s ON s.id_sucursal = f.id_sucursal
ORDER BY f.fecha DESC;

-- 10) Detalle de una factura: productos, servicios y subtotales
SELECT df.id_detalle_factura, f.id_factura, pr.nombre AS producto,
       df.cantidad, df.precio_unitario, df.subtotal
FROM DetalleFactura df
INNER JOIN Factura f ON f.id_factura = df.id_factura
LEFT JOIN Producto pr ON pr.id_producto = df.id_producto;

-- 11) Total facturado por sucursal (agregacion con SUM y GROUP BY)
SELECT s.nombre AS sucursal, COUNT(f.id_factura) AS cantidad_facturas, SUM(f.total) AS total_facturado
FROM Factura f
INNER JOIN Sucursal s ON s.id_sucursal = f.id_sucursal
GROUP BY s.nombre
ORDER BY total_facturado DESC;

-- 12) Pagos recibidos por tipo de pago
SELECT tp.nombre AS tipo_pago, COUNT(*) AS cantidad_pagos, SUM(pg.monto) AS monto_total
FROM Pago pg
INNER JOIN TipoPago tp ON tp.id_tipo_pago = pg.id_tipo_pago
GROUP BY tp.nombre
ORDER BY monto_total DESC;

-- 13) Compras realizadas a proveedores, con el detalle de productos
SELECT c.id_compra, c.fecha, pv.nombre AS proveedor, pr.nombre AS producto,
       dc.cantidad, dc.precio_unitario
FROM Compra c
INNER JOIN Proveedor pv ON pv.id_proveedor = c.id_proveedor
INNER JOIN DetalleCompra dc ON dc.id_compra = c.id_compra
INNER JOIN Producto pr ON pr.id_producto = dc.id_producto
ORDER BY c.fecha DESC;

-- 14) Usuarios del sistema con su rol y sucursal
SELECT u.username, per.nombre AS empleado, r.nombre AS rol, per.nombre
FROM Usuario u
INNER JOIN Persona per ON per.id_persona = u.id_persona
INNER JOIN Rol r ON r.id_rol = u.id_rol
WHERE u.activo = 1;
GO
