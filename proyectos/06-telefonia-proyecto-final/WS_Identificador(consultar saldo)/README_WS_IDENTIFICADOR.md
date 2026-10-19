# WS_Identificador

Servicio WCF SOAP para la historia WS_IDENTIFICADOR1. Su unica operacion es:

```text
ConsultarSaldo(numeroTelefono, origenWeb, tipoTransaccion)
```

`numeroTelefono` debe llegar cifrado. El consumidor debe indicar `origenWeb` como
`WEB` (tambien acepta `true` o `1`) y `tipoTransaccion` como `saldo`.

El servicio reenvia una trama JSON al servidor Python del Identificador en el
puerto 8000. Para solicitudes de origen web, el servidor Python solo exige el
telefono y el tipo de transaccion, tal como define el alcance.

Cuando el proveedor devuelve el saldo, la respuesta SOAP contiene:

```text
Resultado = true
Mensaje = ok
Saldo = 0000000000000125000
```

Ante una solicitud invalida, una conexion fallida o una respuesta no exitosa,
devuelve:

```text
Resultado = false
Mensaje = Usuario ya existe o datos incorrectos o incompletos.
Saldo = null
```

## Ejemplo SOAP

La accion SOAP es:

```text
http://tempuri.org/IIdentificadorService/ConsultarSaldo
```

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
                  xmlns:tem="http://tempuri.org/">
  <soapenv:Header/>
  <soapenv:Body>
    <tem:ConsultarSaldo>
      <tem:numeroTelefono>telefono-cifrado</tem:numeroTelefono>
      <tem:origenWeb>WEB</tem:origenWeb>
      <tem:tipoTransaccion>saldo</tem:tipoTransaccion>
    </tem:ConsultarSaldo>
  </soapenv:Body>
</soapenv:Envelope>
```

La configuracion del socket se encuentra en `Web.config`.
