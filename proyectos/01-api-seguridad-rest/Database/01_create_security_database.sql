/*
  Reconstruido desde SeguridadDbContext y los modelos incluidos en este proyecto.
  No contiene datos personales ni usuarios de prueba.
  Ejecutar en SQL Server con una cuenta que pueda crear bases de datos.
*/
IF DB_ID(N'Seguridad') IS NULL
    CREATE DATABASE [Seguridad];
GO

USE [Seguridad];
GO

IF SCHEMA_ID(N'seguridad') IS NULL EXEC(N'CREATE SCHEMA [seguridad]');
IF SCHEMA_ID(N'general') IS NULL EXEC(N'CREATE SCHEMA [general]');
GO

IF OBJECT_ID(N'seguridad.rol', N'U') IS NULL
BEGIN
    CREATE TABLE [seguridad].[rol]
    (
        [id_rol] NVARCHAR(450) NOT NULL,
        [nombre_rol] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_rol] PRIMARY KEY ([id_rol])
    );
END;
GO

IF OBJECT_ID(N'seguridad.parametro', N'U') IS NULL
BEGIN
    CREATE TABLE [seguridad].[parametro]
    (
        [id_parametro] NVARCHAR(450) NOT NULL,
        [valor] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_parametro] PRIMARY KEY ([id_parametro])
    );
END;
GO

IF OBJECT_ID(N'seguridad.modulo', N'U') IS NULL
BEGIN
    CREATE TABLE [seguridad].[modulo]
    (
        [id_modulo] NVARCHAR(450) NOT NULL,
        [nombre_modulo] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_modulo] PRIMARY KEY ([id_modulo])
    );
END;
GO

IF OBJECT_ID(N'seguridad.usuario', N'U') IS NULL
BEGIN
    CREATE TABLE [seguridad].[usuario]
    (
        [email] NVARCHAR(450) NOT NULL,
        [tipo_identificacion] NVARCHAR(MAX) NOT NULL,
        [identificacion] NVARCHAR(MAX) NOT NULL,
        [nombre] NVARCHAR(MAX) NOT NULL,
        [id_rol] NVARCHAR(MAX) NOT NULL,
        [password_hash] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_usuario] PRIMARY KEY ([email])
    );
END;
GO

IF OBJECT_ID(N'seguridad.refresh_token', N'U') IS NULL
BEGIN
    CREATE TABLE [seguridad].[refresh_token]
    (
        [id_token] INT IDENTITY(1,1) NOT NULL,
        [email_usuario] NVARCHAR(MAX) NOT NULL,
        [refresh_token] NVARCHAR(MAX) NOT NULL,
        [fecha_creacion] DATETIME2 NOT NULL,
        [fecha_expiracion] DATETIME2 NOT NULL,
        [revocado] BIT NOT NULL,
        CONSTRAINT [PK_refresh_token] PRIMARY KEY ([id_token])
    );
END;
GO

IF OBJECT_ID(N'general.bitacora', N'U') IS NULL
BEGIN
    CREATE TABLE [general].[bitacora]
    (
        [id_bitacora] BIGINT IDENTITY(1,1) NOT NULL,
        [usuario] NVARCHAR(MAX) NOT NULL,
        [descripcion] NVARCHAR(MAX) NOT NULL,
        [fecha_hora] DATETIME2 NOT NULL,
        CONSTRAINT [PK_bitacora] PRIMARY KEY ([id_bitacora])
    );
END;
GO
