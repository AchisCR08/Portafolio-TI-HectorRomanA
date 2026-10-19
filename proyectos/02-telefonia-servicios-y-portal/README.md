# Sistema de telefonía: servicios y portal

La carpeta contiene un servicio WCF de autenticación y parte del portal web de clientes. El servicio consulta y registra usuarios en MongoDB; los documentos guardan identificación, nombre, correo, usuario, contraseña, estado y tipo. Sus operaciones permiten autenticar, crear y modificar usuarios, cambiar su estado y consultar datos básicos. El portal cliente consume otros servicios WCF.

## Tecnologías

C#, ASP.NET MVC 5, .NET Framework 4.8, WCF/SOAP y MongoDB.

## Requisitos y preparación

Se necesita Windows, Visual Studio con las herramientas de ASP.NET y el targeting pack de .NET Framework 4.8, NuGet y MongoDB. Inicia MongoDB localmente; el archivo `Database/init_mongodb.js`, que está junto a las carpetas del código, crea la base `TelefoniaAuth` y la colección `usuarios`. Desde PowerShell, ubicado en `02-telefonia-servicios-y-portal`, puedes ejecutarlo así:

```powershell
mongosh --file .\Database\init_mongodb.js
```

MongoDB también crea la base y la colección cuando el servicio guarda el primer usuario. Si tu MongoDB no está en `mongodb://localhost:27017`, edita `Mongo.ConnectionString` en `WS_Autenticacion/Web.config`. Ahí mismo puedes revisar `Mongo.BaseDatos` y `Mongo.Coleccion`.

## Cómo iniciar el servicio de autenticación

1. En Visual Studio, abre `WS_Autenticacion/WS_Autenticacion.sln`. Esta es la solución del servicio; el portal está aparte en `WebCentralCliente/WebCentralCliente.csproj`.
2. Espera a que NuGet restaure las dependencias. Si no ocurre automáticamente, usa **Restaurar paquetes NuGet** desde el menú contextual de la solución.
3. Abre `WS_Autenticacion/Web.config` y cambia `cifrado.clave` por una clave local para AES de 16, 24 o 32 caracteres. Es un secreto de ejecución: no lo publiques.
4. En el Explorador de soluciones, selecciona `WS_Autenticacion` como proyecto de inicio y presiona **Iniciar IIS Express**. Visual Studio abrirá la dirección local donde queda activo el servicio.

Para ver los nombres y parámetros de las operaciones, abre `WS_Autenticacion/IAutenticacionService.cs`. La autenticación utiliza documentos guardados en MongoDB; una colección vacía no contiene usuarios con los que iniciar sesión.

## Sobre el portal

`WebCentralCliente` contiene el sitio web y sus vistas. El código hace referencia a servicios de proveedor, identificador y consultas que no están incluidos en esta carpeta. Por eso, el servicio de autenticación se puede ejecutar por separado, pero el flujo completo del portal requiere recuperar esos servicios y configurar sus direcciones.
