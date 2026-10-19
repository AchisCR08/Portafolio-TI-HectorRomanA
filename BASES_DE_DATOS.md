# Bases de datos: instalación local

Esta guía indica qué motor utiliza cada proyecto y dónde están sus archivos para crear o preparar las bases locales.

| Proyecto | Motor | Archivo(s) |
|---|---|---|
| API de seguridad | SQL Server | `proyectos/01-api-seguridad-rest/Database/01_create_security_database.sql` |
| Telefonía: servicios y portal | MongoDB | `proyectos/02-telefonia-servicios-y-portal/Database/init_mongodb.js` |
| Telefonía: Alcance 2 | MongoDB | `proyectos/03-telefonia-alcance-2/Database/init_mongodb.js` |
| Taller Todo Terreno | SQL Server | `proyectos/04-taller-todo-terreno/Database/01_create_taller_database.sql` |
| Sistema veterinario | SQL Server | `proyectos/05-sistema-veterinaria/Scripts/` |
| Telefonía: proyecto final | SQL Server, MySQL y MongoDB | `proyectos/06-telefonia-proyecto-final/Scripts DB/` |

## SQL Server

Abre los archivos `.sql` en SQL Server Management Studio y ejecútalos contra una instancia local. Los scripts de seguridad y taller crean tablas vacías. Para veterinaria y telefonía final, consulta el README de cada proyecto porque hay que ejecutar varios archivos en el orden indicado.

## MongoDB

Inicia el servicio de MongoDB y ejecuta el inicializador correspondiente desde la carpeta del proyecto:

```powershell
mongosh --file .\Database\init_mongodb.js
```

Para el proyecto final, el archivo está en `Scripts DB`:

```powershell
mongosh --file ".\Scripts DB\init_mongodb.js"
```

Los servicios de autenticación usan la base `TelefoniaAuth` y la colección `usuarios`. MongoDB también puede crear esos objetos cuando se guarda el primer documento.

## MySQL

En el proyecto final, ejecuta primero `script_mysql_identificador.sql` y después `datos_prueba_identificador_mysql.sql`.

## Límites

Los scripts de API y taller reconstruyen solo el esquema desde el código y las migraciones disponibles; no son respaldos de las bases originales ni recuperan sus registros. Los scripts que ya venían con veterinaria y telefonía final pueden contener datos de prueba; revísalos antes de ejecutarlos.
