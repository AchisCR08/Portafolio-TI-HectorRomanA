USE Veterinaria1;
GO


/* ---------------------procedimientos para crud PERSONAS-----------------------*/

-- Crear persona
CREATE OR ALTER PROCEDURE dbo.sp_Persona_Crear
    @nombre VARCHAR(100),
    @telefono VARCHAR(20) = NULL,
    @correo VARCHAR(100) = NULL,
    @direccion VARCHAR(200) = NULL,
    @id_distrito INT = NULL,
    @id_sucursal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50001, 'El nombre de la persona es obligatorio.', 1;

    INSERT INTO Persona (nombre, telefono, correo, direccion, id_distrito, id_sucursal)
    VALUES (@nombre, @telefono, @correo, @direccion, @id_distrito, @id_sucursal);

    SELECT SCOPE_IDENTITY() AS id_persona;
END;
GO

-- Actualizar persona
CREATE OR ALTER PROCEDURE dbo.sp_Persona_Actualizar
    @id_persona INT,
    @nombre VARCHAR(100),
    @telefono VARCHAR(20) = NULL,
    @correo VARCHAR(100) = NULL,
    @direccion VARCHAR(200) = NULL,
    @id_distrito INT = NULL,
    @id_sucursal INT = NULL,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50003, 'El nombre de la persona es obligatorio.', 1;

    -- Actualizar persona
    UPDATE Persona
    SET nombre = @nombre,
        telefono = @telefono,
        correo = @correo,
        direccion = @direccion,
        id_distrito = @id_distrito,
        id_sucursal = @id_sucursal,
        activo = @activo
    WHERE id_persona = @id_persona;

    IF @@ROWCOUNT = 0
        THROW 50004, 'La persona indicada no existe.', 1;
END;
GO

-- Desactivar persona (es borrado logico, no cambien a borrado fisico (no pongan delate xd) )
CREATE OR ALTER PROCEDURE dbo.sp_Persona_Eliminar
    @id_persona INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Desactivar persona sin borrar historial
    UPDATE Persona
    SET activo = 0
    WHERE id_persona = @id_persona;

    IF @@ROWCOUNT = 0
        THROW 50005, 'La persona indicada no existe.', 1;
END;
GO

-- Listar personas
CREATE OR ALTER PROCEDURE dbo.sp_Persona_Listar
    @solo_activos BIT = 1  /*solo muestra los usuarios activos*/
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.*
    FROM Persona p
    WHERE @solo_activos = 0 OR p.activo = 1
    ORDER BY p.nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Persona_SinUsuario_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id_persona AS IdPersona,
        p.nombre AS Nombre,
        p.telefono AS Telefono,
        p.correo AS Correo,
        p.direccion AS Direccion,
        p.id_distrito AS IdDistrito,
        p.id_sucursal AS IdSucursal,
        p.activo AS Activo
    FROM Persona p
    LEFT JOIN Usuario u ON u.id_persona = p.id_persona
    WHERE p.activo = 1
      AND u.id_usuario IS NULL
    ORDER BY p.nombre;
END;
GO

/* ------------------Crud USUARIOS-------------------------------------
    */

-- Crear usuario
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Crear
    @id_persona INT,
    @username VARCHAR(50),
    @password_hash VARCHAR(255),
    @id_rol INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que la persona exista y este activa
    IF NOT EXISTS (SELECT 1 FROM Persona WHERE id_persona = @id_persona AND activo = 1)
        THROW 50006, 'La persona del usuario no existe o esta inactiva.', 1;

    -- Validar que la persona no tenga usuario
    IF EXISTS (SELECT 1 FROM Usuario WHERE id_persona = @id_persona)
        THROW 50008, 'La persona ya tiene un usuario asociado.', 1;

    -- Validar que el rol exista y este activo
    IF NOT EXISTS (SELECT 1 FROM Rol WHERE id_rol = @id_rol AND activo = 1)
        THROW 50009, 'El rol indicado no existe o esta inactivo.', 1;

    -- Validar username unico
    IF EXISTS (SELECT 1 FROM Usuario WHERE username = @username)
        THROW 50007, 'El nombre de usuario ya existe.', 1;

    INSERT INTO Usuario (id_persona, id_rol, username, password_hash)
    VALUES (@id_persona, @id_rol, @username, @password_hash);

    SELECT SCOPE_IDENTITY() AS id_usuario;
END;
GO

-- Validar login de usuario
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_ValidarLogin
    @username VARCHAR(50),
    @password_hash VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.id_usuario AS IdUsuario,
        u.username AS Username,
        p.id_persona AS IdPersona,
        p.nombre AS PersonaNombre,
        u.id_rol AS IdRol,
        r.nombre AS RolNombre,
        s.id_sucursal AS IdSucursal,
        s.nombre AS SucursalNombre
    FROM Usuario u
    INNER JOIN Persona p ON p.id_persona = u.id_persona
    INNER JOIN Rol r ON r.id_rol = u.id_rol
    INNER JOIN Sucursal s ON s.id_sucursal = p.id_sucursal
    WHERE u.username = @username
      AND u.password_hash = @password_hash
      AND u.activo = 1
      AND p.activo = 1
      AND r.activo = 1
      AND s.activo = 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.id_usuario AS IdUsuario,
        u.id_persona AS IdPersona,
        u.id_rol AS IdRol,
        u.username AS Username,
        u.activo AS Activo,
        p.nombre AS PersonaNombre,
        r.nombre AS RolNombre,
        s.nombre AS SucursalNombre
    FROM Usuario u
    INNER JOIN Persona p ON p.id_persona = u.id_persona
    INNER JOIN Rol r ON r.id_rol = u.id_rol
    LEFT JOIN Sucursal s ON s.id_sucursal = p.id_sucursal
    WHERE (@solo_activos = 0 OR u.activo = 1)
    ORDER BY p.nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Actualizar
    @id_usuario INT,
    @username VARCHAR(50),
    @password_hash VARCHAR(255) = NULL,
    @id_rol INT,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50010, 'El usuario indicado no existe.', 1;

    IF NOT EXISTS (SELECT 1 FROM Rol WHERE id_rol = @id_rol AND activo = 1)
        THROW 50011, 'El rol indicado no existe o esta inactivo.', 1;

    IF EXISTS (SELECT 1 FROM Usuario WHERE username = @username AND id_usuario <> @id_usuario)
        THROW 50012, 'El nombre de usuario ya existe.', 1;

    UPDATE Usuario
    SET username = @username,
        password_hash = COALESCE(@password_hash, password_hash),
        id_rol = @id_rol,
        activo = @activo
    WHERE id_usuario = @id_usuario;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Desactivar
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET activo = 0
    WHERE id_usuario = @id_usuario;

    IF @@ROWCOUNT = 0
        THROW 50013, 'El usuario indicado no existe.', 1;
END;
GO

/* ============================================================
   MASCOTAS Y DUENOS
   ============================================================ */

-- Crear mascota y asignar dueno principal opcional
CREATE OR ALTER PROCEDURE dbo.sp_Mascota_Crear
    @nombre VARCHAR(100),
    @fecha_nacimiento DATE = NULL,
    @sexo CHAR(1) = NULL,
    @peso_actual DECIMAL(5,2) = NULL,
    @id_raza INT,
    @id_dueno_principal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50008, 'El nombre de la mascota es obligatorio.', 1;

    -- Validar peso positivo
    IF @peso_actual IS NOT NULL AND @peso_actual <= 0
        THROW 50009, 'El peso de la mascota debe ser mayor a 0.', 1;

    -- Validar sexo permitido
    IF @sexo IS NOT NULL AND @sexo NOT IN ('M', 'H')
        THROW 50010, 'El sexo de la mascota debe ser M o H.', 1;

    BEGIN TRANSACTION;

    INSERT INTO Mascota (nombre, fecha_nacimiento, sexo, peso_actual, id_raza)
    VALUES (@nombre, @fecha_nacimiento, @sexo, @peso_actual, @id_raza);

    DECLARE @id_mascota INT = SCOPE_IDENTITY();

    -- Asignar dueno principal si se envio
    IF @id_dueno_principal IS NOT NULL
    BEGIN
        INSERT INTO Duenos_Mascotas (id_persona, id_mascota, es_principal)
        VALUES (@id_dueno_principal, @id_mascota, 1);
    END;

    COMMIT TRANSACTION;

    SELECT @id_mascota AS id_mascota;
END;
GO

-- Actualizar mascota
CREATE OR ALTER PROCEDURE dbo.sp_Mascota_Actualizar
    @id_mascota INT,
    @nombre VARCHAR(100),
    @fecha_nacimiento DATE = NULL,
    @sexo CHAR(1) = NULL,
    @peso_actual DECIMAL(5,2) = NULL,
    @id_raza INT,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar peso positivo
    IF @peso_actual IS NOT NULL AND @peso_actual <= 0
        THROW 50011, 'El peso de la mascota debe ser mayor a 0.', 1;

    -- Actualizar mascota
    UPDATE Mascota
    SET nombre = @nombre,
        fecha_nacimiento = @fecha_nacimiento,
        sexo = @sexo,
        peso_actual = @peso_actual,
        id_raza = @id_raza,
        activo = @activo
    WHERE id_mascota = @id_mascota;

    IF @@ROWCOUNT = 0
        THROW 50012, 'La mascota indicada no existe.', 1;
END;
GO

-- Asignar dueno a mascota
CREATE OR ALTER PROCEDURE dbo.sp_Mascota_AsignarDueno
    @id_persona INT,
    @id_mascota INT,
    @es_principal BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Si sera principal, quitar marca principal anterior
    IF @es_principal = 1
    BEGIN
        UPDATE Duenos_Mascotas
        SET es_principal = 0
        WHERE id_mascota = @id_mascota;
    END;

    IF EXISTS (SELECT 1 FROM Duenos_Mascotas WHERE id_persona = @id_persona AND id_mascota = @id_mascota)
    BEGIN
        UPDATE Duenos_Mascotas
        SET es_principal = @es_principal
        WHERE id_persona = @id_persona AND id_mascota = @id_mascota;
    END
    ELSE
    BEGIN
        INSERT INTO Duenos_Mascotas (id_persona, id_mascota, es_principal)
        VALUES (@id_persona, @id_mascota, @es_principal);
    END;
END;
GO

-- Eliminar o desactivar mascota
CREATE OR ALTER PROCEDURE dbo.sp_Mascota_Eliminar
    @id_mascota INT,
    @eliminacion_fisica BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Eliminacion fisica ,  eliminacion logica solo desactiva
    IF @eliminacion_fisica = 1
        DELETE FROM Mascota WHERE id_mascota = @id_mascota;
    ELSE
    BEGIN
        -- Desactivar mascota sin borrar historial
        UPDATE Mascota SET activo = 0 WHERE id_mascota = @id_mascota;
    END;

    IF @@ROWCOUNT = 0
        THROW 50013, 'La mascota indicada no existe.', 1;
END;
GO

-- Listar mascotas
CREATE OR ALTER PROCEDURE dbo.sp_Mascota_Listar
    @solo_activas BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT m.*, r.nombre AS raza
    FROM Mascota m
    INNER JOIN Raza r ON r.id_raza = m.id_raza
    WHERE @solo_activas = 0 OR m.activo = 1
    ORDER BY m.nombre;
END;
GO

/* ------CITAS VETERINARIAs------------------*/

-- Agendar cita veterinaria
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_Agendar
    @fecha DATE,
    @hora TIME,
    @motivo VARCHAR(200) = NULL,
    @id_mascota INT,
    @id_veterinario INT,
    @id_estado_cita INT,
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar disponibilidad del veterinario
    IF dbo.fn_EstaDisponibleCitaVeterinaria(@fecha, @hora, @id_veterinario, @id_sucursal, NULL) = 0
        THROW 50014, 'El veterinario no esta disponible en esa fecha y hora.', 1;

    INSERT INTO CitaVeterinaria (fecha, hora, motivo, id_mascota, id_veterinario, id_estado_cita, id_sucursal)
    VALUES (@fecha, @hora, @motivo, @id_mascota, @id_veterinario, @id_estado_cita, @id_sucursal);

    SELECT SCOPE_IDENTITY() AS id_cita_vet;
END;
GO

-- Actualizar una cita veterinaria
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_Actualizar
    @id_cita_vet INT, @fecha DATE, @hora TIME, @motivo VARCHAR(200) = NULL,
    @id_mascota INT, @id_veterinario INT, @id_estado_cita INT, @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM CitaVeterinaria WHERE id_cita_vet = @id_cita_vet)
        THROW 50045, 'La cita veterinaria indicada no existe.', 1;

    IF dbo.fn_EstaDisponibleCitaVeterinaria(
        @fecha, @hora, @id_veterinario, @id_sucursal, @id_cita_vet) = 0
        THROW 50046, 'El veterinario no esta disponible en esa fecha y hora.', 1;

    UPDATE CitaVeterinaria
    SET fecha = @fecha, hora = @hora, motivo = @motivo,
        id_mascota = @id_mascota, id_veterinario = @id_veterinario,
        id_estado_cita = @id_estado_cita, id_sucursal = @id_sucursal
    WHERE id_cita_vet = @id_cita_vet;
END;
GO

-- Reprogramar cita veterinaria
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_Reprogramar
    @id_cita_vet INT,
    @fecha DATE,
    @hora TIME,
    @id_veterinario INT,
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar disponibilidad del veterinario
    IF dbo.fn_EstaDisponibleCitaVeterinaria(@fecha, @hora, @id_veterinario, @id_sucursal, @id_cita_vet) = 0
        THROW 50015, 'El veterinario no esta disponible en esa fecha y hora.', 1;

    UPDATE CitaVeterinaria
    SET fecha = @fecha,
        hora = @hora,
        id_veterinario = @id_veterinario,
        id_sucursal = @id_sucursal
    WHERE id_cita_vet = @id_cita_vet;

    IF @@ROWCOUNT = 0
        THROW 50016, 'La cita veterinaria indicada no existe.', 1;
END;
GO

-- Cambiar estado de cita veterinaria
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_CambiarEstado
    @id_cita_vet INT,
    @id_estado_cita INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CitaVeterinaria
    SET id_estado_cita = @id_estado_cita
    WHERE id_cita_vet = @id_cita_vet;

    IF @@ROWCOUNT = 0
        THROW 50017, 'La cita veterinaria indicada no existe.', 1;
END;
GO

-- Listar citas veterinarias
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_Listar
    @fecha DATE = NULL,
    @id_sucursal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT cv.*, m.nombre AS mascota, p.nombre AS veterinario, ec.nombre AS estado
    FROM CitaVeterinaria cv
    INNER JOIN Mascota m ON m.id_mascota = cv.id_mascota
    INNER JOIN Persona p ON p.id_persona = cv.id_veterinario
    INNER JOIN EstadoCita ec ON ec.id_estado_cita = cv.id_estado_cita
    WHERE (@fecha IS NULL OR cv.fecha = @fecha)
      AND (@id_sucursal IS NULL OR cv.id_sucursal = @id_sucursal)
    ORDER BY cv.fecha, cv.hora;
END;
GO

/* ----------------------------CITAS GROOMING---------------------------- */

-- Agendar cita de grooming
CREATE OR ALTER PROCEDURE dbo.sp_CitaGrooming_Agendar
    @fecha DATE,
    @hora TIME,
    @id_mascota INT,
    @id_groomer INT,
    @id_estado_cita INT,
    @id_servicio_grooming INT,
    @id_sucursal INT,
    @precio_final DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar disponibilidad del groomer
    IF dbo.fn_EstaDisponibleCitaGrooming(@fecha, @hora, @id_groomer, @id_sucursal, NULL) = 0
        THROW 50018, 'El groomer no esta disponible en esa fecha y hora.', 1;

    -- Calcular precio si no se envia manualmente
    IF @precio_final IS NULL
        SET @precio_final = dbo.fn_CalcularPrecioGrooming(@id_servicio_grooming, @id_mascota);

    INSERT INTO CitaGrooming (fecha, hora, id_mascota, id_groomer, id_estado_cita, precio_final, id_servicio_grooming, id_sucursal)
    VALUES (@fecha, @hora, @id_mascota, @id_groomer, @id_estado_cita, @precio_final, @id_servicio_grooming, @id_sucursal);

    SELECT SCOPE_IDENTITY() AS id_cita_grooming;
END;
GO

-- Actualizar una cita de grooming
CREATE OR ALTER PROCEDURE dbo.sp_CitaGrooming_Actualizar
    @id_cita_grooming INT, @fecha DATE, @hora TIME, @id_mascota INT,
    @id_groomer INT, @id_estado_cita INT, @id_servicio_grooming INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_sucursal INT;
    SELECT @id_sucursal = id_sucursal
    FROM CitaGrooming
    WHERE id_cita_grooming = @id_cita_grooming;

    IF @id_sucursal IS NULL
        THROW 50047, 'La cita de grooming indicada no existe.', 1;

    IF dbo.fn_EstaDisponibleCitaGrooming(
        @fecha, @hora, @id_groomer, @id_sucursal, @id_cita_grooming) = 0
        THROW 50048, 'El groomer no esta disponible en esa fecha y hora.', 1;

    UPDATE CitaGrooming
    SET fecha = @fecha, hora = @hora, id_mascota = @id_mascota,
        id_groomer = @id_groomer, id_estado_cita = @id_estado_cita,
        id_servicio_grooming = @id_servicio_grooming,
        precio_final = dbo.fn_CalcularPrecioGrooming(@id_servicio_grooming, @id_mascota)
    WHERE id_cita_grooming = @id_cita_grooming;
END;
GO

-- Cambiar estado de cita grooming
CREATE OR ALTER PROCEDURE dbo.sp_CitaGrooming_CambiarEstado
    @id_cita_grooming INT,
    @id_estado_cita INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CitaGrooming
    SET id_estado_cita = @id_estado_cita
    WHERE id_cita_grooming = @id_cita_grooming;

    IF @@ROWCOUNT = 0
        THROW 50019, 'La cita de grooming indicada no existe.', 1;
END;
GO

/* ------------------------------CONSULTAS Y VACUNAS---------------------------------*/

-- Registrar consulta medica
CREATE OR ALTER PROCEDURE dbo.sp_Consulta_Registrar
    @id_cita_vet INT,
    @diagnostico VARCHAR(MAX) = NULL,
    @tratamiento VARCHAR(MAX) = NULL,
    @observaciones VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que la cita no tenga consulta registrada
    IF EXISTS (SELECT 1 FROM Consulta WHERE id_cita_vet = @id_cita_vet)
        THROW 50040, 'La cita veterinaria ya tiene una consulta registrada.', 1;

    INSERT INTO Consulta (id_cita_vet, diagnostico, tratamiento, observaciones)
    VALUES (@id_cita_vet, @diagnostico, @tratamiento, @observaciones);

    SELECT SCOPE_IDENTITY() AS id_consulta;
END;
GO

-- Actualizar consulta medica
CREATE OR ALTER PROCEDURE dbo.sp_Consulta_Actualizar
    @id_consulta INT,
    @diagnostico VARCHAR(MAX) = NULL,
    @tratamiento VARCHAR(MAX) = NULL,
    @observaciones VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Consulta
    SET diagnostico = @diagnostico,
        tratamiento = @tratamiento,
        observaciones = @observaciones
    WHERE id_consulta = @id_consulta;

    IF @@ROWCOUNT = 0
        THROW 50041, 'La consulta indicada no existe.', 1;
END;
GO

-- Listar consultas por mascota
CREATE OR ALTER PROCEDURE dbo.sp_Consulta_ListarPorMascota
    @id_mascota INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.*, cv.fecha, cv.hora, cv.motivo
    FROM Consulta c
    INNER JOIN CitaVeterinaria cv ON cv.id_cita_vet = c.id_cita_vet
    WHERE cv.id_mascota = @id_mascota
    ORDER BY cv.fecha DESC, cv.hora DESC;
END;
GO

-- Crear vacuna
CREATE OR ALTER PROCEDURE dbo.sp_Vacuna_Crear
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50042, 'El nombre de la vacuna es obligatorio.', 1;

    INSERT INTO Vacuna (nombre, descripcion)
    VALUES (@nombre, @descripcion);

    SELECT SCOPE_IDENTITY() AS id_vacuna;
END;
GO

-- Actualizar vacuna
CREATE OR ALTER PROCEDURE dbo.sp_Vacuna_Actualizar
    @id_vacuna INT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Vacuna
    SET nombre = @nombre,
        descripcion = @descripcion,
        activo = @activo
    WHERE id_vacuna = @id_vacuna;

    IF @@ROWCOUNT = 0
        THROW 50043, 'La vacuna indicada no existe.', 1;
END;
GO

-- Listar vacunas
CREATE OR ALTER PROCEDURE dbo.sp_Vacuna_Listar
    @solo_activas BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Vacuna
    WHERE @solo_activas = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- Registrar vacuna aplicada
CREATE OR ALTER PROCEDURE dbo.sp_HistorialVacuna_Registrar
    @id_mascota INT,
    @id_vacuna INT,
    @fecha_aplicacion DATE,
    @proxima_dosis DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar fecha de proxima dosis
    IF @proxima_dosis IS NOT NULL AND @proxima_dosis < @fecha_aplicacion
        THROW 50044, 'La proxima dosis no puede ser anterior a la fecha de aplicacion.', 1;

    INSERT INTO HistorialVacuna (id_mascota, id_vacuna, fecha_aplicacion, proxima_dosis)
    VALUES (@id_mascota, @id_vacuna, @fecha_aplicacion, @proxima_dosis);

    SELECT SCOPE_IDENTITY() AS id_historial;
END;
GO

-- Actualizar una vacuna aplicada en el historial
CREATE OR ALTER PROCEDURE dbo.sp_HistorialVacuna_Actualizar
    @id_historial INT, @id_mascota INT, @id_vacuna INT,
    @fecha_aplicacion DATE, @proxima_dosis DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @proxima_dosis IS NOT NULL AND @proxima_dosis < @fecha_aplicacion
        THROW 50049, 'La proxima dosis no puede ser anterior a la fecha de aplicacion.', 1;

    UPDATE HistorialVacuna
    SET id_mascota = @id_mascota, id_vacuna = @id_vacuna,
        fecha_aplicacion = @fecha_aplicacion, proxima_dosis = @proxima_dosis
    WHERE id_historial = @id_historial;

    IF @@ROWCOUNT = 0
        THROW 50050, 'El registro de vacuna indicado no existe.', 1;
END;
GO

-- Listar historial de vacunas por mascota
CREATE OR ALTER PROCEDURE dbo.sp_HistorialVacuna_ListarPorMascota
    @id_mascota INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT hv.*, v.nombre AS vacuna
    FROM HistorialVacuna hv
    INNER JOIN Vacuna v ON v.id_vacuna = hv.id_vacuna
    WHERE hv.id_mascota = @id_mascota
    ORDER BY hv.fecha_aplicacion DESC;
END;
GO

/* -----------------------------------------INVENTARIO Y CATALOGO----------------------------------- */

-- Crear producto
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Crear
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL,
    @precio_venta DECIMAL(10,2),
    @id_categoria INT,
    @id_proveedor INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar precio de venta positivo
    IF @precio_venta <= 0
        THROW 50020, 'El precio de venta debe ser mayor a 0.', 1;

    INSERT INTO Producto (nombre, descripcion, precio_venta, id_categoria, id_proveedor)
    VALUES (@nombre, @descripcion, @precio_venta, @id_categoria, @id_proveedor);

    SELECT SCOPE_IDENTITY() AS id_producto;
END;
GO

-- Crear categoria
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Crear
    @nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50030, 'El nombre de la categoria es obligatorio.', 1;

    INSERT INTO Categoria (nombre)
    VALUES (@nombre);

    SELECT SCOPE_IDENTITY() AS id_categoria;
END;
GO

-- Actualizar categoria
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Actualizar
    @id_categoria INT,
    @nombre VARCHAR(100),
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Categoria
    SET nombre = @nombre,
        activo = @activo
    WHERE id_categoria = @id_categoria;

    IF @@ROWCOUNT = 0
        THROW 50031, 'La categoria indicada no existe.', 1;
END;
GO

-- Desactivar categoria
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Eliminar
    @id_categoria INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Categoria
    SET activo = 0
    WHERE id_categoria = @id_categoria;

    IF @@ROWCOUNT = 0
        THROW 50032, 'La categoria indicada no existe.', 1;
END;
GO

-- Listar categorias
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Listar
    @solo_activas BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Categoria
    WHERE @solo_activas = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- Crear proveedor
CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Crear
    @nombre VARCHAR(100),
    @telefono VARCHAR(20) = NULL,
    @correo VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar nombre obligatorio
    IF NULLIF(LTRIM(RTRIM(@nombre)), '') IS NULL
        THROW 50033, 'El nombre del proveedor es obligatorio.', 1;

    INSERT INTO Proveedor (nombre, telefono, correo)
    VALUES (@nombre, @telefono, @correo);

    SELECT SCOPE_IDENTITY() AS id_proveedor;
END;
GO

-- Actualizar proveedor
CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Actualizar
    @id_proveedor INT,
    @nombre VARCHAR(100),
    @telefono VARCHAR(20) = NULL,
    @correo VARCHAR(100) = NULL,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Proveedor
    SET nombre = @nombre,
        telefono = @telefono,
        correo = @correo,
        activo = @activo
    WHERE id_proveedor = @id_proveedor;

    IF @@ROWCOUNT = 0
        THROW 50034, 'El proveedor indicado no existe.', 1;
END;
GO

-- Desactivar proveedor
CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Eliminar
    @id_proveedor INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Proveedor
    SET activo = 0
    WHERE id_proveedor = @id_proveedor;

    IF @@ROWCOUNT = 0
        THROW 50035, 'El proveedor indicado no existe.', 1;
END;
GO

-- Listar proveedores
CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Proveedor
    WHERE @solo_activos = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- Crear servicio de grooming
CREATE OR ALTER PROCEDURE dbo.sp_ServicioGrooming_Crear
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL,
    @precio_base DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar precio base positivo
    IF @precio_base <= 0
        THROW 50036, 'El precio base del servicio debe ser mayor a 0.', 1;

    INSERT INTO ServicioGrooming (nombre, descripcion, precio_base)
    VALUES (@nombre, @descripcion, @precio_base);

    SELECT SCOPE_IDENTITY() AS id_servicio_grooming;
END;
GO

-- Actualizar servicio de grooming
CREATE OR ALTER PROCEDURE dbo.sp_ServicioGrooming_Actualizar
    @id_servicio_grooming INT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL,
    @precio_base DECIMAL(10,2),
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar precio base positivo
    IF @precio_base <= 0
        THROW 50037, 'El precio base del servicio debe ser mayor a 0.', 1;

    UPDATE ServicioGrooming
    SET nombre = @nombre,
        descripcion = @descripcion,
        precio_base = @precio_base,
        activo = @activo
    WHERE id_servicio_grooming = @id_servicio_grooming;

    IF @@ROWCOUNT = 0
        THROW 50038, 'El servicio de grooming indicado no existe.', 1;
END;
GO

-- Desactivar servicio de grooming
CREATE OR ALTER PROCEDURE dbo.sp_ServicioGrooming_Eliminar
    @id_servicio_grooming INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ServicioGrooming
    SET activo = 0
    WHERE id_servicio_grooming = @id_servicio_grooming;

    IF @@ROWCOUNT = 0
        THROW 50039, 'El servicio de grooming indicado no existe.', 1;
END;
GO

-- Listar servicios de grooming
CREATE OR ALTER PROCEDURE dbo.sp_ServicioGrooming_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM ServicioGrooming
    WHERE @solo_activos = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- Actualizar producto
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Actualizar
    @id_producto INT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX) = NULL,
    @precio_venta DECIMAL(10,2),
    @id_categoria INT,
    @id_proveedor INT,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar precio de venta positivo
    IF @precio_venta <= 0
        THROW 50021, 'El precio de venta debe ser mayor a 0.', 1;

    -- Desactivar o actualizar producto
    UPDATE Producto
    SET nombre = @nombre,
        descripcion = @descripcion,
        precio_venta = @precio_venta,
        id_categoria = @id_categoria,
        id_proveedor = @id_proveedor,
        activo = @activo
    WHERE id_producto = @id_producto;

    IF @@ROWCOUNT = 0
        THROW 50022, 'El producto indicado no existe.', 1;
END;
GO

-- Desactivar producto
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Eliminar
    @id_producto INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Desactivar o actualizar producto
    UPDATE Producto
    SET activo = 0
    WHERE id_producto = @id_producto;

    IF @@ROWCOUNT = 0
        THROW 50023, 'El producto indicado no existe.', 1;
END;
GO

-- Listar productos
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id_producto AS IdProducto,
        p.nombre AS Nombre,
        p.descripcion AS Descripcion,
        p.precio_venta AS PrecioVenta,
        p.id_categoria AS IdCategoria,
        p.id_proveedor AS IdProveedor,
        p.activo AS Activo,
        c.nombre AS CategoriaNombre,
        pr.nombre AS ProveedorNombre
    FROM Producto p
    INNER JOIN Categoria c ON c.id_categoria = p.id_categoria
    INNER JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
    WHERE @solo_activos = 0 OR p.activo = 1
    ORDER BY p.nombre;
END;
GO

/*----------------------------- COMPRAS --------------------------*/

-- Crear encabezado de compra
CREATE OR ALTER PROCEDURE dbo.sp_Compra_Crear
    @fecha DATE = NULL,
    @id_proveedor INT,
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Compra (fecha, id_proveedor, id_sucursal)
    VALUES (ISNULL(@fecha, CAST(GETDATE() AS DATE)), @id_proveedor, @id_sucursal);

    SELECT SCOPE_IDENTITY() AS id_compra;
END;
GO

-- Agregar producto comprado
CREATE OR ALTER PROCEDURE dbo.sp_Compra_AgregarDetalle
    @id_compra INT,
    @id_producto INT,
    @cantidad INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar cantidad y costo de compra
    IF @cantidad <= 0 OR @precio_unitario <= 0
        THROW 50024, 'La cantidad y el precio de compra deben ser mayores a 0.', 1;

    -- Insertar detalle de compra; trigger aumenta stock
    INSERT INTO DetalleCompra (id_compra, id_producto, cantidad, precio_unitario)
    VALUES (@id_compra, @id_producto, @cantidad, @precio_unitario);

    SELECT SCOPE_IDENTITY() AS id_detalle_compra;
END;
GO

/* ------------------------------FACTURACION Y PAGOS---------------------- */

-- Crear encabezado de factura
CREATE OR ALTER PROCEDURE dbo.sp_Factura_Crear
    @fecha DATE = NULL,
    @id_persona INT,
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Factura (fecha, id_persona, id_sucursal, total)
    VALUES (ISNULL(@fecha, CAST(GETDATE() AS DATE)), @id_persona, @id_sucursal, 0);

    SELECT SCOPE_IDENTITY() AS id_factura;
END;
GO

-- Agregar producto a factura
CREATE OR ALTER PROCEDURE dbo.sp_Factura_AgregarProducto
    @id_factura INT,
    @id_producto INT,
    @cantidad INT,
    @precio_unitario DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar cantidad vendida
    IF @cantidad <= 0
        THROW 50025, 'La cantidad vendida debe ser mayor a 0.', 1;

    -- Tomar precio del producto si no se envia
    IF @precio_unitario IS NULL
        SELECT @precio_unitario = precio_venta FROM Producto WHERE id_producto = @id_producto;

    -- Validar precio unitario positivo
    IF @precio_unitario IS NULL OR @precio_unitario <= 0
        THROW 50026, 'El precio del producto debe ser mayor a 0.', 1;

    -- Insertar detalle de factura; triggers actualizan total y stock si aplica
    INSERT INTO DetalleFactura (id_factura, id_producto, cantidad, precio_unitario)
    VALUES (@id_factura, @id_producto, @cantidad, @precio_unitario);

    SELECT SCOPE_IDENTITY() AS id_detalle_factura;
END;
GO

-- Agregar servicio veterinario a factura
CREATE OR ALTER PROCEDURE dbo.sp_Factura_AgregarCitaVeterinaria
    @id_factura INT,
    @id_cita_vet INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar precio del servicio
    IF @precio_unitario <= 0
        THROW 50027, 'El precio del servicio veterinario debe ser mayor a 0.', 1;

    -- Insertar detalle de factura; triggers actualizan total y stock si aplica
    INSERT INTO DetalleFactura (id_factura, id_cita_vet, cantidad, precio_unitario)
    VALUES (@id_factura, @id_cita_vet, 1, @precio_unitario);

    SELECT SCOPE_IDENTITY() AS id_detalle_factura;
END;
GO

-- Agregar servicio grooming a factura
CREATE OR ALTER PROCEDURE dbo.sp_Factura_AgregarCitaGrooming
    @id_factura INT,
    @id_cita_grooming INT,
    @precio_unitario DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Tomar precio del producto si no se envia
    IF @precio_unitario IS NULL
        SELECT @precio_unitario = precio_final
        FROM CitaGrooming
        WHERE id_cita_grooming = @id_cita_grooming;

    -- Validar precio unitario positivo
    IF @precio_unitario IS NULL OR @precio_unitario <= 0
        THROW 50028, 'El precio del servicio de grooming debe ser mayor a 0.', 1;

    -- Insertar detalle de factura; triggers actualizan total y stock si aplica
    INSERT INTO DetalleFactura (id_factura, id_cita_grooming, cantidad, precio_unitario)
    VALUES (@id_factura, @id_cita_grooming, 1, @precio_unitario);

    SELECT SCOPE_IDENTITY() AS id_detalle_factura;
END;
GO

-- Registrar pago
CREATE OR ALTER PROCEDURE dbo.sp_Pago_Registrar
    @id_factura INT,
    @id_tipo_pago INT,
    @monto DECIMAL(10,2),
    @fecha_pago DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar monto positivo
    IF @monto <= 0
        THROW 50029, 'El monto del pago debe ser mayor a 0.', 1;

    INSERT INTO Pago (id_factura, id_tipo_pago, monto, fecha_pago)
    VALUES (@id_factura, @id_tipo_pago, @monto, ISNULL(@fecha_pago, CAST(GETDATE() AS DATE)));

    SELECT SCOPE_IDENTITY() AS id_pago;
END;
GO

/*CONSULTAR INVENTARIO */

-- Consultar inventario
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Consultar
    @id_sucursal INT = NULL,
    @id_producto INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT inv.id_sucursal,
           s.nombre AS sucursal,
           inv.id_producto,
           p.nombre AS producto,
           inv.stock
    FROM InventarioSucursal inv
    INNER JOIN Sucursal s ON s.id_sucursal = inv.id_sucursal
    INNER JOIN Producto p ON p.id_producto = inv.id_producto
    WHERE (@id_sucursal IS NULL OR inv.id_sucursal = @id_sucursal)
      AND (@id_producto IS NULL OR inv.id_producto = @id_producto)
    ORDER BY s.nombre, p.nombre;
END;
GO

-- Guardar (insertar o actualizar) inventario manualmente
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Guardar
    @id_sucursal INT,
    @id_producto INT,
    @stock INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que la sucursal exista y este activa
    IF NOT EXISTS (SELECT 1 FROM Sucursal WHERE id_sucursal = @id_sucursal AND activo = 1)
        THROW 50050, 'La sucursal indicada no existe o esta inactiva.', 1;

    -- Validar que el producto exista y este activo
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE id_producto = @id_producto AND activo = 1)
        THROW 50051, 'El producto indicado no existe o esta inactivo.', 1;

    -- Validar stock no negativo
    IF @stock < 0
        THROW 50052, 'El stock no puede ser negativo.', 1;

    -- Insertar o actualizar el registro de inventario
    MERGE InventarioSucursal AS target
    USING (SELECT @id_sucursal AS id_sucursal, @id_producto AS id_producto, @stock AS stock) AS source
    ON target.id_sucursal = source.id_sucursal
       AND target.id_producto = source.id_producto
    WHEN MATCHED THEN
        UPDATE SET stock = source.stock
    WHEN NOT MATCHED THEN
        INSERT (id_sucursal, id_producto, stock)
        VALUES (source.id_sucursal, source.id_producto, source.stock);
END;
GO

-- Eliminar registro de inventario de una sucursal
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Eliminar
    @id_sucursal INT,
    @id_producto INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM InventarioSucursal
    WHERE id_sucursal = @id_sucursal
      AND id_producto = @id_producto;

    IF @@ROWCOUNT = 0
        THROW 50053, 'El registro de inventario indicado no existe.', 1;
END;
GO

/* ============================================================
   DASHBOARD
   ============================================================ */


CREATE OR ALTER PROCEDURE dbo.sp_Dashboard_ResumenKPI
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @hoy DATE = CAST(GETDATE() AS DATE);

    -- Total ventas del dia (suma de totales de facturas de hoy en la sucursal)
    DECLARE @totalVentas DECIMAL(10,2) =
        ISNULL((SELECT SUM(f.total) FROM Factura f
                WHERE f.id_sucursal = @id_sucursal AND f.fecha = @hoy), 0);

    -- Total compras del dia (suma de precio_unitario * cantidad en DetalleCompra de hoy)
    DECLARE @totalCompras DECIMAL(10,2) =
        ISNULL((SELECT SUM(dc.cantidad * dc.precio_unitario)
                FROM DetalleCompra dc
                INNER JOIN Compra c ON c.id_compra = dc.id_compra
                WHERE c.id_sucursal = @id_sucursal AND c.fecha = @hoy), 0);

    -- Citas de hoy (veterinarias + grooming) en la sucursal
    DECLARE @citasHoy INT =
        ISNULL((SELECT COUNT(*) FROM CitaVeterinaria
                WHERE id_sucursal = @id_sucursal AND fecha = @hoy), 0)
        +
        ISNULL((SELECT COUNT(*) FROM CitaGrooming
                WHERE id_sucursal = @id_sucursal AND fecha = @hoy), 0);

    -- Total veterinarios activos en la sucursal (personas con rol Veterinario)
    DECLARE @totalVeterinarios INT =
        ISNULL((SELECT COUNT(DISTINCT p.id_persona)
                FROM Persona p
                INNER JOIN Usuario u ON u.id_persona = p.id_persona
                INNER JOIN Rol r ON r.id_rol = u.id_rol
                WHERE p.id_sucursal = @id_sucursal
                  AND p.activo = 1
                  AND u.activo = 1
                  AND r.nombre = 'Veterinario'), 0);

    -- Total mascotas activas registradas en el sistema
    DECLARE @totalMascotas INT =
        ISNULL((SELECT COUNT(*) FROM Mascota WHERE activo = 1), 0);

    SELECT
        @totalVentas   AS TotalVentas,
        @totalCompras  AS TotalCompras,
        @citasHoy      AS CitasHoy,
        @totalVeterinarios AS TotalVeterinarios,
        @totalMascotas AS TotalMascotas;
END;
GO

-- Citas veterinarias de hoy para una sucursal
CREATE OR ALTER PROCEDURE dbo.sp_Dashboard_CitasHoy
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @hoy DATE = CAST(GETDATE() AS DATE);

    SELECT
        cv.hora                  AS Hora,
        m.nombre                 AS Paciente,
        p.nombre                 AS Veterinario,
        cv.motivo                AS Motivo,
        ec.nombre                AS Estado
    FROM CitaVeterinaria cv
    INNER JOIN Mascota m  ON m.id_mascota  = cv.id_mascota
    INNER JOIN Persona p  ON p.id_persona  = cv.id_veterinario
    INNER JOIN EstadoCita ec ON ec.id_estado_cita = cv.id_estado_cita
    WHERE cv.id_sucursal = @id_sucursal
      AND cv.fecha = @hoy
    ORDER BY cv.hora;
END;
GO

/* ============================================================
   FACTURACION — SPs auxiliares
   ============================================================ */

-- Listar tipos de pago activos
CREATE OR ALTER PROCEDURE dbo.sp_TipoPago_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tipo_pago AS IdTipoPago, nombre AS Nombre
    FROM TipoPago
    WHERE @solo_activos = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- Listar citas veterinarias pendientes de cobro en una sucursal
-- (que existan y NO tengan ya un detalle de factura asociado)
CREATE OR ALTER PROCEDURE dbo.sp_CitaVeterinaria_PendientesCobro
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        cv.id_cita_vet      AS IdCitaVet,
        m.nombre            AS MascotaNombre,
        p.nombre            AS VeterinarioNombre,
        cv.fecha            AS Fecha,
        cv.hora             AS Hora,
        ec.nombre           AS Estado
    FROM CitaVeterinaria cv
    INNER JOIN Mascota m      ON m.id_mascota     = cv.id_mascota
    INNER JOIN Persona p      ON p.id_persona     = cv.id_veterinario
    INNER JOIN EstadoCita ec  ON ec.id_estado_cita = cv.id_estado_cita
    WHERE cv.id_sucursal = @id_sucursal
      AND NOT EXISTS (
          SELECT 1 FROM DetalleFactura df WHERE df.id_cita_vet = cv.id_cita_vet
      )
    ORDER BY cv.fecha DESC, cv.hora DESC;
END;
GO

-- Listar citas grooming pendientes de cobro en una sucursal (creo que no voy a usarlo, lo pueden omitir si quieren)
CREATE OR ALTER PROCEDURE dbo.sp_CitaGrooming_PendientesCobro
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        cg.id_cita_grooming AS IdCitaGrooming,
        m.nombre            AS MascotaNombre,
        sg.nombre           AS ServicioNombre,
        cg.fecha            AS Fecha,
        cg.hora             AS Hora,
        cg.precio_final     AS PrecioFinal,
        ec.nombre           AS Estado
    FROM CitaGrooming cg
    INNER JOIN Mascota m          ON m.id_mascota         = cg.id_mascota
    INNER JOIN ServicioGrooming sg ON sg.id_servicio_grooming = cg.id_servicio_grooming
    INNER JOIN EstadoCita ec      ON ec.id_estado_cita    = cg.id_estado_cita
    WHERE cg.id_sucursal = @id_sucursal
      AND NOT EXISTS (
          SELECT 1 FROM DetalleFactura df WHERE df.id_cita_grooming = cg.id_cita_grooming
      )
    ORDER BY cg.fecha DESC, cg.hora DESC;
END;
GO

--auxiliares 
 

-- 1. Listar roles activos 
CREATE OR ALTER PROCEDURE dbo.sp_Rol_Listar
    @solo_activos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_rol AS IdRol, nombre AS Nombre, activo AS Activo
    FROM Rol
    WHERE @solo_activos = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- 2. Listar sucursales activas 
CREATE OR ALTER PROCEDURE dbo.sp_Sucursal_Listar
    @solo_activas BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_sucursal AS IdSucursal,
        nombre      AS Nombre,
        telefono    AS Telefono,
        correo      AS Correo,
        direccion   AS Direccion,
        id_distrito AS IdDistrito,
        activo      AS Activo
    FROM Sucursal
    WHERE @solo_activas = 0 OR activo = 1
    ORDER BY nombre;
END;
GO

-- 3. Listar personas activas filtradas por sucursal 
CREATE OR ALTER PROCEDURE dbo.sp_Persona_ListarPorSucursal
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        id_persona  AS IdPersona,
        nombre      AS Nombre,
        telefono    AS Telefono,
        correo      AS Correo,
        id_sucursal AS IdSucursal,
        activo      AS Activo
    FROM Persona
    WHERE id_sucursal = @id_sucursal
      AND activo = 1
    ORDER BY nombre;
END;
GO

-- 4. Listar citas grooming 
CREATE OR ALTER PROCEDURE dbo.sp_CitaGrooming_Listar
    @fecha      DATE = NULL,
    @id_sucursal INT  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        cg.*,
        m.nombre  AS mascota,
        p.nombre  AS groomer,
        sg.nombre AS servicio,
        ec.nombre AS estado
    FROM CitaGrooming cg
    INNER JOIN Mascota          m  ON m.id_mascota             = cg.id_mascota
    INNER JOIN Persona          p  ON p.id_persona             = cg.id_groomer
    INNER JOIN ServicioGrooming sg ON sg.id_servicio_grooming  = cg.id_servicio_grooming
    INNER JOIN EstadoCita       ec ON ec.id_estado_cita        = cg.id_estado_cita
    WHERE (@fecha      IS NULL OR cg.fecha      = @fecha)
      AND (@id_sucursal IS NULL OR cg.id_sucursal = @id_sucursal)
    ORDER BY cg.fecha, cg.hora;
END;
GO

--  CORRECCIÓN
CREATE OR ALTER PROCEDURE dbo.sp_Dashboard_CitasHoy
    @id_sucursal INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @hoy DATE = CAST(GETDATE() AS DATE);

    -- Citas veterinarias del día
    SELECT
        cv.hora     AS Hora,
        m.nombre    AS Paciente,
        p.nombre    AS Veterinario,
        cv.motivo   AS Motivo,
        ec.nombre   AS Estado,
        'Veterinaria' AS TipoCita
    FROM CitaVeterinaria cv
    INNER JOIN Mascota    m  ON m.id_mascota        = cv.id_mascota
    INNER JOIN Persona    p  ON p.id_persona        = cv.id_veterinario
    INNER JOIN EstadoCita ec ON ec.id_estado_cita   = cv.id_estado_cita
    WHERE cv.id_sucursal = @id_sucursal
      AND cv.fecha = @hoy

    UNION ALL

    -- Citas grooming del día
    SELECT
        cg.hora         AS Hora,
        m.nombre        AS Paciente,
        p.nombre        AS Veterinario,  -- groomer en la misma columna
        sg.nombre       AS Motivo,       -- nombre del servicio como motivo
        ec.nombre       AS Estado,
        'Grooming'      AS TipoCita
    FROM CitaGrooming cg
    INNER JOIN Mascota          m  ON m.id_mascota              = cg.id_mascota
    INNER JOIN Persona          p  ON p.id_persona              = cg.id_groomer
    INNER JOIN ServicioGrooming sg ON sg.id_servicio_grooming   = cg.id_servicio_grooming
    INNER JOIN EstadoCita       ec ON ec.id_estado_cita         = cg.id_estado_cita
    WHERE cg.id_sucursal = @id_sucursal
      AND cg.fecha = @hoy

    ORDER BY Hora;
END;
GO

-- 6. Listar facturas de una sucursal 
CREATE OR ALTER PROCEDURE dbo.sp_Factura_Listar
    @id_sucursal  INT  = NULL,
    @fecha_inicio DATE = NULL,
    @fecha_fin    DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        f.id_factura    AS IdFactura,
        f.fecha         AS Fecha,
        f.total         AS Total,
        f.id_persona    AS IdPersona,
        p.nombre        AS PersonaNombre,
        f.id_sucursal   AS IdSucursal,
        s.nombre        AS SucursalNombre
    FROM Factura f
    INNER JOIN Persona  p ON p.id_persona  = f.id_persona
    INNER JOIN Sucursal s ON s.id_sucursal = f.id_sucursal
    WHERE (@id_sucursal  IS NULL OR f.id_sucursal = @id_sucursal)
      AND (@fecha_inicio IS NULL OR f.fecha >= @fecha_inicio)
      AND (@fecha_fin    IS NULL OR f.fecha <= @fecha_fin)
    ORDER BY f.fecha DESC, f.id_factura DESC;
END;
GO

-- 7. Listar pagos de una factura específica
CREATE OR ALTER PROCEDURE dbo.sp_Pago_ListarPorFactura
    @id_factura INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pg.id_pago      AS IdPago,
        pg.id_factura   AS IdFactura,
        pg.monto        AS Monto,
        pg.fecha_pago   AS FechaPago,
        pg.id_tipo_pago AS IdTipoPago,
        tp.nombre       AS TipoPagoNombre
    FROM Pago pg
    INNER JOIN TipoPago tp ON tp.id_tipo_pago = pg.id_tipo_pago
    WHERE pg.id_factura = @id_factura
    ORDER BY pg.id_pago;
END;
GO

-- 8. Obtener lineas de detalle de una factura especifica
--    Resuelve la descripción según el tipo de línea (Producto / Cita Vet. / Grooming)
CREATE OR ALTER PROCEDURE dbo.sp_Factura_ObtenerDetalle
    @id_factura INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        df.id_detalle_factura                       AS IdDetalleFactura,
        df.id_factura                               AS IdFactura,

        -- Tipo de línea
        CASE
            WHEN df.id_producto      IS NOT NULL THEN 'Producto'
            WHEN df.id_cita_vet      IS NOT NULL THEN 'Cita Veterinaria'
            WHEN df.id_cita_grooming IS NOT NULL THEN 'Grooming'
        END                                         AS TipoLinea,

        -- Descripcion segun el tipo
        CASE
            WHEN df.id_producto IS NOT NULL THEN
                p.nombre

            WHEN df.id_cita_vet IS NOT NULL THEN
                'Consulta: ' + m_vet.nombre
                + ' — Vet: ' + per_vet.nombre
                + ' (' + CONVERT(VARCHAR(10), cv.fecha, 103) + ')'

            WHEN df.id_cita_grooming IS NOT NULL THEN
                'Grooming: ' + m_grm.nombre
                + ' — ' + sg.nombre
                + ' (' + CONVERT(VARCHAR(10), cg.fecha, 103) + ')'
        END                                         AS Descripcion,

        ISNULL(df.cantidad, 1)                      AS Cantidad,
        df.precio_unitario                          AS PrecioUnitario,
        -- subtotal es columna calculada en la tabla; si cantidad es NULL (servicios) = precio_unitario
        ISNULL(df.subtotal, df.precio_unitario)     AS Subtotal,

        -- IDs originales (utiles si en el futuro se quiere navegar al detalle)
        df.id_producto                              AS IdProducto,
        df.id_cita_vet                              AS IdCitaVet,
        df.id_cita_grooming                         AS IdCitaGrooming

    FROM DetalleFactura df

    -- Producto
    LEFT JOIN Producto p
        ON p.id_producto = df.id_producto

    -- Cita veterinaria y sus referencias
    LEFT JOIN CitaVeterinaria cv
        ON cv.id_cita_vet = df.id_cita_vet
    LEFT JOIN Mascota m_vet
        ON m_vet.id_mascota = cv.id_mascota
    LEFT JOIN Persona per_vet
        ON per_vet.id_persona = cv.id_veterinario

    -- Cita grooming y sus referencias
    LEFT JOIN CitaGrooming cg
        ON cg.id_cita_grooming = df.id_cita_grooming
    LEFT JOIN Mascota m_grm
        ON m_grm.id_mascota = cg.id_mascota
    LEFT JOIN ServicioGrooming sg
        ON sg.id_servicio_grooming = cg.id_servicio_grooming

    WHERE df.id_factura = @id_factura
    ORDER BY df.id_detalle_factura;
END;
GO

/* actualizacion*/

-- 1. Registrar encabezado de compra — devuelve el id_compra creado
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

-- 3. Listar compras de una sucursal con filtro opcional de fechas
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
        -- Total calculado sumando líneas de detalle
        ISNULL((
            SELECT SUM(dc.cantidad * dc.precio_unitario)
            FROM DetalleCompra dc
            WHERE dc.id_compra = c.id_compra
        ), 0)           AS Total
    FROM Compra c
    INNER JOIN Proveedor p ON p.id_proveedor = c.id_proveedor
    INNER JOIN Sucursal  s ON s.id_sucursal  = c.id_sucursal
    WHERE (@id_sucursal  IS NULL OR c.id_sucursal = @id_sucursal)
      AND (@fecha_inicio IS NULL OR c.fecha >= @fecha_inicio)
      AND (@fecha_fin    IS NULL OR c.fecha <= @fecha_fin)
    ORDER BY c.fecha DESC, c.id_compra DESC;
END;
GO

-- 4. Obtener líneas de detalle de una compra específica
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
