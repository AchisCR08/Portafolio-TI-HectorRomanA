# Sistema de telefonía: proyecto final de Programación IV

Sistema académico formado por páginas web, servicios de integración y programas que intercambian información. El portal administrativo y el portal de clientes se conectan con servicios WCF. El servicio WCF de proveedor comunica esos pedidos con un servidor Java por sockets; el identificador de llamadas usa otro servidor Python. La autenticación guarda usuarios en MongoDB, mientras los demás componentes consultan SQL Server y MySQL.

## Componentes: dónde está cada parte

- `WebCentralAdmin` y `WebCentralCliente`: portales MVC para personal y clientes.
- `WS_Autenticacion` y `WS_ConsultasProveedor`: servicios WCF incluidos en la solución principal.
- `WS_Proveedor`: servicio WCF que se comunica con el servidor Java. Tiene su propia solución `WS_Proveedor/WS_Proveedor.sln`.
- `WS_Identificador(consultar saldo)`: servicio WCF que reenvía consultas al servidor Python. Tiene su propia solución `WS_Identificador(consultar saldo)/WS_Identificador.sln`.
- `Proveedor-java`: servidor de proveedor en Java; su configuración está en `Proveedor-java/src/main/resources/config.properties` y su entrada es `servidor.Main`.
- `Identificador_Python`: servidor TCP en `Capa_Presentacion/ServidorIdentificador.py`, con el puerto configurado en ese archivo.
- `Scripts DB`: scripts de SQL Server y MySQL. El inicializador de MongoDB también está aquí.

La solución `TelefoniaWeb.sln`, en la raíz, abre los portales y algunos servicios .NET. No incluye el servidor Java, el servidor Python ni todas las soluciones WCF independientes; esos componentes deben abrirse y arrancarse por separado.

## Requisitos

Windows, Visual Studio con ASP.NET y el targeting pack de .NET Framework 4.8, IIS Express, SQL Server, MySQL, MongoDB, Python, Maven y un JDK 24 (el `pom.xml` solicita Java release 24). Python utiliza los paquetes listados en `Identificador_Python/requirements.txt`.

## 1. Preparar las bases de datos

Usa SQL Server Management Studio para abrir los archivos SQL Server y MySQL Workbench (o el cliente `mysql`) para los archivos MySQL. Los scripts están en `Scripts DB`, en la raíz de esta carpeta.

Para SQL Server, crea primero la base con `scrip_sqlServer proveedor 1.sql`. **Este script elimina y vuelve a crear algunos objetos para pruebas**, así que ejecútalo únicamente en una base local desechable. Luego abre `NUEVO SQL SERVER EJECUTAR.sql`: amplía la base con funciones de alcance 2 y requiere que el script base ya se haya ejecutado. Por último, `datos_prueba_proveedor_sqlserver.sql` modifica catálogos y agrega datos de ejemplo; úsalo solo si quieres probar con información de demostración.

Para MySQL, ejecuta primero `script_mysql_identificador.sql`, que crea `IdentificadorDB` y sus tablas. Después puedes ejecutar `datos_prueba_identificador_mysql.sql` para cargar registros de ejemplo. No ejecutes los scripts de muestra sobre una base real.

Para MongoDB, inicia el servicio y desde PowerShell, ubicado en la raíz de este proyecto, ejecuta:

```powershell
mongosh --file ".\Scripts DB\init_mongodb.js"
```

Esto crea la base `TelefoniaAuth` y la colección `usuarios` que utiliza `WS_Autenticacion`. No carga usuarios de prueba.

## 2. Configurar las conexiones y claves

Antes de iniciar los componentes, abre los archivos de configuración siguientes y comprueba que apunten a tus servicios locales:

- `WS_Autenticacion/Web.config`: servidor, base y colección de MongoDB.
- `WS_ConsultasProveedor/Web.config`: conexión a SQL Server, URL del servicio de autenticación y clave de cifrado.
- `WS_Proveedor/Web.config`: host y puerto del servidor Java (por defecto, puerto 6000).
- `WS_Identificador(consultar saldo)/Web.config`: host y puerto del servidor Python (por defecto, puerto 8000).
- `Proveedor-java/src/main/resources/config.properties`: SQL Server, usuario del proveedor y puertos Java/Python.
- `Identificador_Python/Capa_Datos/Conexion.py`: conexión local a MySQL.

Reemplaza los marcadores `REEMPLAZAR_EN_CONFIGURACION_LOCAL` con credenciales y claves propias solo en tu computadora. No subas contraseñas, claves AES/JWT ni otros secretos al repositorio. Si cambias un puerto, actualízalo también en el componente que se conecta a él.

## 3. Iniciar los servidores de sockets

El servidor Java escucha en el puerto 6000 y el servicio Python del identificador en el 8000 según la configuración incluida. Deben estar activos antes de probar los WCF que los consumen.

Para Python, abre PowerShell en `Identificador_Python`, crea un entorno e instala las dependencias:

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install -r requirements.txt
python .\Capa_Presentacion\ServidorIdentificador.py
```

La terminal debe mostrar el host y puerto donde quedó escuchando el identificador. Para Java, abre `Proveedor-java` en un IDE Java, configura `servidor.Main` como clase de inicio y ejecuta el proyecto. `mvn clean package` compila el código, pero por sí solo no deja el servidor ejecutándose.

## 4. Iniciar servicios WCF y portales

Primero, en Visual Studio, abre y ejecuta los servicios WCF que están en sus soluciones individuales (`WS_Proveedor.sln` y `WS_Identificador.sln`). Después abre `TelefoniaWeb.sln` desde esta carpeta y restaura los paquetes NuGet. Desde el Explorador de soluciones, ejecuta los proyectos `WS_Autenticacion` y `WS_ConsultasProveedor`; luego inicia `WebCentralAdmin` o `WebCentralCliente`.

IIS Express muestra una dirección local para cada servicio. Compara esas direcciones con las URL de los `Web.config`; si no coinciden, actualiza la configuración del consumidor antes de probar. Los portales solo pueden completar las operaciones que tengan sus servicios y bases de datos correspondientes activos.

## Problemas comunes y límites

- **Error al conectar con SQL Server o MySQL:** confirma que el motor esté iniciado, que la base se haya creado y que la cadena de conexión tenga el nombre de servidor correcto.
- **Error de socket o timeout:** inicia Java/Python primero y verifica que los puertos 6000 y 8000 coincidan en ambos extremos.
- **Error al llamar un WCF:** comprueba que el servicio esté activo en IIS Express y que la URL configurada en el consumidor sea la misma que muestra Visual Studio.
- **Inicio de sesión sin usuarios:** MongoDB queda vacío tras ejecutar su inicializador; se necesitan usuarios válidos para probar la autenticación.

Esta carpeta contiene varios proyectos y sus dependencias no se levantan desde una sola solución. El arranque integrado no está validado en una instalación limpia; si una operación no responde, revisa la configuración y los servicios dependientes antes de concluir que el componente está dañado.
