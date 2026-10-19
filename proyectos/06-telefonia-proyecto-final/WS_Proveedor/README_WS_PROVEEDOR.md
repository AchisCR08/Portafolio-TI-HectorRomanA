# WS_Proveedor

Servicio WCF para conectar los servicios web del alcance 2 con el proveedor Java.

## WS_PROVEEDOR1

Operacion implementada:

```text
RegistrarServicioDisponible
```

Recibe:

```text
numeroTelefono
identificadorTelefono
identificadorTarjeta
tipoServicio
estado
```

Segun el enunciado, los primeros tres datos deben venir cifrados desde quien consuma el servicio.
El WCF no descifra esos datos; los envia al proveedor Java en la trama JSON.

## Trama enviada a Java

```json
{
  "tipo": "registrar_linea",
  "telefono": "numeroTelefono",
  "identificadorTelefono": "identificadorTelefono",
  "identificadorTarjeta": "identificadorTarjeta",
  "tipoServicio": "PREPAGO",
  "estado": "DISPONIBLE"
}
```

## Respuesta del WCF

Si Java responde `OK`:

```text
Resultado = true
Mensaje = Exitoso
```

Si Java responde cualquier otro estado:

```text
Resultado = false
Mensaje = Problemas al incluir la informacion.
```

## WS_PROVEEDOR2

Operacion: `ActivarDesactivarLinea`.

Recibe `numeroTelefono`, `identificadorTelefono`, `identificadorTarjeta`,
`tipoServicio`, `identificacionDuenno` y `estado`. Los datos sensibles se
reenvian cifrados al proveedor Java en una trama de tipo `cambiar_estado_linea`.
El estado debe ser `ACTIVAR` o `DESACTIVAR`.

Si el proveedor responde `OK`, devuelve `Resultado = true` y `Mensaje = Exitoso`.
En cualquier otro caso devuelve `Resultado = false` y
`Mensaje = Problemas al activar/desactivar la linea.`

## WS_PROVEEDOR3

Operacion: `CalcularFacturacionPostpago`.

Recibe `fechaCalculo` y `fechaMaximaPago`, y las envia al proveedor Java con el
tipo `calcular_facturacion_postpago`.

Si el proveedor responde `OK`, devuelve `Resultado = true` y `Mensaje = Exitoso`.
En cualquier otro caso devuelve `Resultado = false` y
`Mensaje = Problemas al realizar el calculo.`

## Como probar

1. Abrir `WS_Proveedor.sln` en Visual Studio 2022.
2. Ejecutar primero el proveedor Java (`servidor.Main`) en el puerto 6000.
3. Ejecutar el servicio WCF.
4. Abrir WCF Test Client.
5. Consumir la operacion `RegistrarServicioDisponible`.

La configuracion del socket Java esta en `Web.config`:

```xml
<add key="ProveedorJava.Host" value="localhost" />
<add key="ProveedorJava.Puerto" value="6000" />
<add key="ProveedorJava.TimeoutMs" value="5000" />
```
