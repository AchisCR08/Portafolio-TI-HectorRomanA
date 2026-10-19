# Taller Todo Terreno

Aplicación web para administrar dueños y sus vehículos, mantener horarios del taller y solicitar o cancelar citas. La información se guarda en SQL Server mediante Entity Framework Core. El acceso de los dueños utiliza inicio de sesión con cookies.

## Requisitos

- .NET SDK 8.
- SQL Server Express, LocalDB o una instancia compatible.
- Visual Studio o PowerShell.

## 1. Crear la base de datos

La carpeta `Database`, ubicada junto a `Taller_Todo_Terreno.sln`, contiene `01_create_taller_database.sql`. Abre el archivo en SQL Server Management Studio (SSMS), conéctate a tu instancia local y ejecútalo completo. Crea la base `TallerTodoTerreno` y las tablas vacías para dueños, vehículos, horarios y citas.

El SQL se reconstruyó desde las migraciones y el modelo de Entity Framework incluidos en la entrega; no es un respaldo de la base original. Como alternativa, el proyecto tiene migraciones: se pueden ejecutar con `dotnet ef database update` desde la carpeta `Taller_Todo_Terreno`, pero elige ese método o el script SQL para crear la base, no ambos.

La base queda sin datos. La aplicación permite registrar un dueño y sus vehículos, pero para reservar una cita también deben existir horarios disponibles. No hay una pantalla para administrar horarios, así que hay que agregarlos a la tabla `Horarios` en SSMS antes de probar las reservas.

## 2. Configurar la conexión

Abre `Taller_Todo_Terreno.sln` en Visual Studio para ver el proyecto web. El archivo `Taller_Todo_Terreno/appsettings.json` no trae la cadena de conexión; el proyecto usa User Secrets para guardar ese dato fuera del código. Si utilizas PowerShell, ubícate en la carpeta principal `04-taller-todo-terreno` y define la conexión al mismo servidor donde creaste la base. El ejemplo usa SQL Server local; para SQL Express, cambia `localhost` por el nombre de tu instancia, como `localhost\SQLEXPRESS`.

```powershell
dotnet user-secrets set "ConnectionStrings:DatabaseConnection" "Server=localhost;Database=TallerTodoTerreno;Trusted_Connection=True;TrustServerCertificate=True;" --project .\Taller_Todo_Terreno\Taller_Todo_Terreno.csproj
```

## 3. Iniciar y recorrer la aplicación

En la misma terminal, restaura paquetes y arranca el proyecto web:

```powershell
dotnet restore .\Taller_Todo_Terreno\Taller_Todo_Terreno.csproj
dotnet run --project .\Taller_Todo_Terreno\Taller_Todo_Terreno.csproj
```

Cuando la terminal indique que el servidor está escuchando, abre esa dirección en el navegador. Registra un dueño, inicia sesión, agrega un vehículo y luego prueba la reserva de cita después de cargar horarios en la base. Si no conecta, comprueba que SQL Server esté activo, que la instancia sea la correcta y que la base `TallerTodoTerreno` exista.
