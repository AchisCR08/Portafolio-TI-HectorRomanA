# Sistema de telefonía: Alcance 2

Entrega parcial de un sistema de telefonía. Incluye el servicio WCF de autenticación conectado a MongoDB y algunos componentes Python del identificador de llamadas. El código del identificador contempla comunicación TCP y consultas de información de telefonía, pero faltan módulos necesarios para ejecutarlo.

## Componentes incluidos

- `WS_Autenticacion`: autenticación y operaciones de consulta, creación, modificación y cambio de estado de usuarios.
- `Identificador_Python`: fragmentos de capas de datos, negocio y presentación.
- `Scripts DB`: datos de ejemplo de la entrega.

## Requisitos

Para probar el servicio WCF se requiere Windows, Visual Studio con las herramientas de ASP.NET/.NET Framework 4.8, NuGet y MongoDB. Para el identificador Python también se necesitan Python, el motor SQL que indique su configuración y servicios externos que esta copia no trae.

## Preparar MongoDB y ejecutar autenticación

1. Inicia el servicio de MongoDB en tu computadora. La configuración incluida espera `mongodb://localhost:27017`.
2. Desde PowerShell, ubicado en `03-telefonia-alcance-2`, ejecuta `Database/init_mongodb.js`. Está en la carpeta `Database`, aparte del código, y crea la base vacía `TelefoniaAuth` y la colección `usuarios`:

```powershell
mongosh --file .\Database\init_mongodb.js
```

3. Opcionalmente, carga los documentos de ejemplo con `mongosh --file ".\Scripts DB\datos_prueba_mongo_autenticacion.js"`, también desde la carpeta principal. Ese archivo está en `Scripts DB` y agrega dos usuarios de demostración.
4. Abre `WS_Autenticacion/WS_Autenticacion.sln` en Visual Studio y espera a que se restauren los paquetes NuGet.
5. En `WS_Autenticacion/Web.config`, comprueba `Mongo.ConnectionString`, `Mongo.BaseDatos` y `Mongo.Coleccion`. Configura también `cifrado.clave` con una clave local para AES de 16, 24 o 32 caracteres; no subas una clave real.
6. En el Explorador de soluciones, establece `WS_Autenticacion` como proyecto de inicio y ejecútalo con **Iniciar IIS Express**. Las operaciones disponibles están descritas en `WS_Autenticacion/IAutenticacionService.cs`.

Los datos de prueba están cifrados con la clave de la entrega original. La copia no conserva esa clave, así que para iniciar sesión con esos usuarios hay que cifrar de nuevo sus valores con la clave local elegida. El script carga documentos, pero no crea una clave ni revela credenciales.

## Qué no se puede ejecutar con esta copia

El componente Python importa módulos ausentes, entre ellos `Conexion`, `ProveedorSocket`, `CifradorAES` y `Bitacora`. También requiere la configuración de SQL y de otros servicios. Por eso, los pasos anteriores permiten revisar el servicio de autenticación, pero no levantar el sistema integrado. Para hacerlo habría que recuperar los archivos faltantes y sus instrucciones de configuración.
