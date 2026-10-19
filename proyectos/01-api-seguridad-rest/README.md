# API REST de seguridad

API para administrar roles, módulos, parámetros y registros de bitácora. Incluye inicio de sesión con JWT y refresh tokens; las contraseñas se almacenan como hashes BCrypt. Swagger permite consultar y probar los endpoints.

## Tecnologías

C#, .NET 8, ASP.NET Core Web API, SQL Server, Entity Framework Core, JWT, BCrypt, Swagger y Postman.

## Requisitos

- .NET SDK 8.
- SQL Server Express, LocalDB o una instancia compatible.
- Visual Studio o terminal de PowerShell.

## Crear la base de datos

Abre SQL Server Management Studio (SSMS), conéctate a tu instancia local y abre `Database/01_create_security_database.sql`, que está al mismo nivel que `NuevoAvatarSeguridad.sln`. Ejecuta el archivo completo. Crea la base `Seguridad`, dos esquemas (`seguridad` y `general`) y las tablas vacías que utiliza la API; no incluye usuarios ni datos de prueba.

El esquema se reconstruyó desde `SeguridadDbContext` y los modelos presentes en el proyecto, porque la copia no incluía el script original ni las migraciones. Por eso, los tipos de texto siguen los valores de Entity Framework Core cuando el código no define una longitud.

## Configuración y ejecución

En Visual Studio, abre `NuevoAvatarSeguridad.sln` desde esta carpeta. También puedes seguir estos pasos en PowerShell, ubicándote primero en `proyectos/01-api-seguridad-rest`:

1. La cadena de conexión debe apuntar a la misma instancia donde ejecutaste el SQL. El ejemplo de abajo asume SQL Server local con autenticación integrada; para SQL Express, cambia `Server=localhost` por el nombre de tu instancia, por ejemplo `localhost\SQLEXPRESS`.
2. Guarda la cadena y una clave privada de JWT en User Secrets. El proyecto ya tiene configurado el identificador de User Secrets, por lo que no hay que inicializarlo de nuevo. Estos valores se guardan fuera de los archivos que subirías al repositorio.
3. Restaura los paquetes NuGet y ejecuta el proyecto web `NuevoAvatarSeguridad`:

```powershell
dotnet user-secrets set "ConnectionStrings:SeguridadDb" "Server=localhost;Database=Seguridad;Trusted_Connection=True;TrustServerCertificate=True;" --project .\NuevoAvatarSeguridad\NuevoAvatarSeguridad.csproj
dotnet user-secrets set "Jwt:Key" "usa-una-clave-local-de-al-menos-32-caracteres" --project .\NuevoAvatarSeguridad\NuevoAvatarSeguridad.csproj
dotnet restore .\NuevoAvatarSeguridad.sln
```

La terminal muestra la dirección local donde quedó escuchando la API. Abre esa dirección y agrega `/swagger` para ver las rutas, los modelos de entrada y salida y probar los endpoints. Para probar `/login` hace falta insertar antes un usuario con contraseña cifrada mediante BCrypt; la copia no tiene endpoint de registro de usuarios ni un usuario inicial. Los errores de conexión suelen indicar que el servidor o nombre de base de datos de User Secrets no coincide con el de SQL Server.
