# Sistema veterinario

Aplicación de escritorio para administrar procesos de una veterinaria. La solución contiene formularios para citas de clínica y grooming, historial médico, vacunas, clientes, usuarios, inventario, compras, facturación y reportes. La aplicación principal usa Dapper para consultar SQL Server.

## Requisitos

- Windows y .NET SDK 8.
- SQL Server Express o una instancia compatible.
- SQL Server Management Studio (SSMS) para crear y revisar la base.
- Visual Studio para abrir la solución de Windows Forms.

## 1. Crear y preparar la base

Los archivos están dentro de `Scripts`, al mismo nivel que `Veterinaria1.sln`. En SSMS, conéctate a tu instancia y abre cada archivo desde **Archivo > Abrir > Archivo**. Ejecuta en este orden:

1. `Veterinaria1.sql` crea la base `Veterinaria1` y sus tablas.
2. `Funciones.sql` crea funciones que usan las consultas de base de datos.
3. `Procedimientos.sql` y `SPs_Compras.sql` crean procedimientos almacenados.
4. `Vistas.sql` y `Triggers.sql` agregan las vistas y automatizaciones de la base.

`Operaciones_CRUD.sql` es una demostración de inserción, modificación, eliminación y consulta de datos. No es necesario para crear la estructura y modifica registros; ejecútalo únicamente en una base local de prueba. `Migracion_Usuario_IdRol.sql` es una actualización para una base que ya existía, no se usa para una instalación desde cero.

La base inicial no contiene una cuenta lista para iniciar sesión. Los scripts que venían en la carpeta tampoco incluyen credenciales de acceso.

## 2. Ajustar la conexión

Abre `appsettings.json`, que está junto a `Veterinaria1.sln`. La clave `ConnectionStrings:VeterinariaDB` indica a qué servidor y base se conecta la aplicación. El valor incluido apunta a `localhost\SQLEXPRESS` y a `Veterinaria1`; cambia el servidor si tu instancia tiene otro nombre. No agregues contraseñas personales antes de publicar el repositorio.

## 3. Abrir y ejecutar la aplicación

En Visual Studio, abre `Veterinaria1.sln` desde la carpeta principal del proyecto. La solución incluye `Veterinaria1` (la aplicación), `Repositorio` (acceso a SQL Server) y `Entidades` (modelos compartidos). Espera la restauración de dependencias y establece `Veterinaria1` como proyecto de inicio. También puedes iniciarlo en PowerShell desde la carpeta del proyecto:

```powershell
dotnet run --project .\\Veterinaria1.csproj
```

Al arrancar se muestra el formulario de inicio de sesión y, tras autenticarse, el menú principal. Para demostrarlo con navegación completa hace falta contar con una cuenta válida en la base; la copia no proporciona una. Si aparece un error de conexión, revisa que SQL Server esté activo, que la cadena de `appsettings.json` nombre la instancia correcta y que hayas ejecutado los scripts de estructura y objetos SQL.

## Organización académica

El sistema se realizó en grupo. La sección clínica descrita en el portafolio incluye interfaz, citas, historial, vacunas y objetos de base de datos relacionados.
