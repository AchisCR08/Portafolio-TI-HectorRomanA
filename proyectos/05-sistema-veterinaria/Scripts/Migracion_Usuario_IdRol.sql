USE Veterinaria1;
GO

/* Ejecutar una vez si la base ya existia con id_rol en Persona. */

IF COL_LENGTH('dbo.Usuario', 'id_rol') IS NULL
BEGIN
    ALTER TABLE dbo.Usuario ADD id_rol INT NULL;
END;
GO

IF COL_LENGTH('dbo.Persona', 'id_rol') IS NOT NULL
BEGIN
    EXEC('
        UPDATE u
        SET id_rol = p.id_rol
        FROM dbo.Usuario u
        INNER JOIN dbo.Persona p ON p.id_persona = u.id_persona
        WHERE u.id_rol IS NULL
    ');
END;
GO

IF EXISTS (SELECT 1 FROM dbo.Usuario WHERE id_rol IS NULL)
BEGIN
    THROW 50100, 'Hay usuarios sin id_rol. Asigne un rol antes de poner la columna como NOT NULL.', 1;
END;
GO

IF COL_LENGTH('dbo.Usuario', 'id_rol') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Usuario ALTER COLUMN id_rol INT NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_Usuario_Rol'
      AND parent_object_id = OBJECT_ID('dbo.Usuario')
)
BEGIN
    ALTER TABLE dbo.Usuario
    ADD CONSTRAINT FK_Usuario_Rol
    FOREIGN KEY (id_rol) REFERENCES dbo.Rol(id_rol);
END;
GO

IF COL_LENGTH('dbo.Persona', 'id_rol') IS NOT NULL
BEGIN
    DECLARE @fkName SYSNAME;

    SELECT TOP 1 @fkName = fk.name
    FROM sys.foreign_keys fk
    INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    INNER JOIN sys.columns c ON c.object_id = fkc.parent_object_id
        AND c.column_id = fkc.parent_column_id
    WHERE fk.parent_object_id = OBJECT_ID('dbo.Persona')
      AND c.name = 'id_rol';

    IF @fkName IS NOT NULL
    BEGIN
        EXEC('ALTER TABLE dbo.Persona DROP CONSTRAINT ' + QUOTENAME(@fkName));
    END;

    ALTER TABLE dbo.Persona DROP COLUMN id_rol;
END;
GO
