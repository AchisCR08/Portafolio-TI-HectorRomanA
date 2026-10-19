/*
  Reconstruido desde la migración inicial y TallerContextModelSnapshot.
  No contiene datos de prueba.
*/
IF DB_ID(N'TallerTodoTerreno') IS NULL
    CREATE DATABASE [TallerTodoTerreno];
GO

USE [TallerTodoTerreno];
GO

IF OBJECT_ID(N'dbo.Duenos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Duenos]
    (
        [DuenoID] INT IDENTITY(1,1) NOT NULL,
        [Identificacion] NVARCHAR(MAX) NOT NULL,
        [Nombre] NVARCHAR(MAX) NOT NULL,
        [Direccion] NVARCHAR(MAX) NOT NULL,
        [Telefono] NVARCHAR(MAX) NOT NULL,
        [Email] NVARCHAR(MAX) NOT NULL,
        [Contrasena] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_Duenos] PRIMARY KEY ([DuenoID])
    );
END;
GO

IF OBJECT_ID(N'dbo.Horarios', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Horarios]
    (
        [HorarioId] INT IDENTITY(1,1) NOT NULL,
        [Fecha] DATETIME2 NOT NULL,
        [HoraInicio] TIME NOT NULL,
        [HoraFin] TIME NOT NULL,
        [CupoMaximo] INT NOT NULL,
        CONSTRAINT [PK_Horarios] PRIMARY KEY ([HorarioId])
    );
END;
GO

IF OBJECT_ID(N'dbo.Vehiculos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Vehiculos]
    (
        [VehiculoId] INT IDENTITY(1,1) NOT NULL,
        [Placa] NVARCHAR(10) NOT NULL,
        [Marca] NVARCHAR(MAX) NOT NULL,
        [Modelo] NVARCHAR(MAX) NOT NULL,
        [Año] INT NOT NULL,
        [TipoTransmision] NVARCHAR(MAX) NOT NULL,
        [DuenoID] INT NOT NULL,
        CONSTRAINT [PK_Vehiculos] PRIMARY KEY ([VehiculoId]),
        CONSTRAINT [FK_Vehiculos_Duenos_DuenoID]
            FOREIGN KEY ([DuenoID]) REFERENCES [dbo].[Duenos] ([DuenoID]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_Vehiculos_DuenoID] ON [dbo].[Vehiculos] ([DuenoID]);
END;
GO

IF OBJECT_ID(N'dbo.Citas', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Citas]
    (
        [CitaId] INT IDENTITY(1,1) NOT NULL,
        [VehiculoId] INT NOT NULL,
        [HorarioId] INT NOT NULL,
        [FechaSolicitud] DATETIME2 NOT NULL,
        [Estado] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_Citas] PRIMARY KEY ([CitaId]),
        CONSTRAINT [FK_Citas_Horarios_HorarioId]
            FOREIGN KEY ([HorarioId]) REFERENCES [dbo].[Horarios] ([HorarioId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Citas_Vehiculos_VehiculoId]
            FOREIGN KEY ([VehiculoId]) REFERENCES [dbo].[Vehiculos] ([VehiculoId]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_Citas_HorarioId] ON [dbo].[Citas] ([HorarioId]);
    CREATE INDEX [IX_Citas_VehiculoId] ON [dbo].[Citas] ([VehiculoId]);
END;
GO
