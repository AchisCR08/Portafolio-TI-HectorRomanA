using System;
using System.Linq;
using System.ServiceModel.Activation;
using WS_ConsultasProveedor.Datos;
using WS_ConsultasProveedor.Infraestructura;
using WS_ConsultasProveedor.Modelos;
using WS_ConsultasProveedor.Utilidades;

namespace WS_ConsultasProveedor
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConsultasProveedorService : IConsultasProveedorService
    {
        public LineaDisponibleDTO[] ListarLineasDisponibles()
        {
            using (var ctx = new TelefoniaContext())
            {
                return ctx.Cuentas
                    .Where(c => c.Estado == "DISPONIBLE")
                    .AsEnumerable()
                    .Select(c => new LineaDisponibleDTO
                    {
                        NumeroTelefono = c.NumeroTelefono,
                        IdentificadorTarjeta = CifradorAES.DescifrarSiEsPosible(c.IdentificadorTarjeta),
                        IdentificadorTelefono = CifradorAES.DescifrarSiEsPosible(c.IdentificadorTelefono),
                        TipoServicio = c.TipoServicio
                    })
                    .ToArray();
            }
        }

        public RespuestaOperacion EliminarLineaDisponible(string numeroTelefono)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroTelefono))
                {
                    return new RespuestaOperacion
                    {
                        Resultado = false,
                        Mensaje = "Número de teléfono requerido."
                    };
                }

                string telefonoDescifrado = CifradorAES.DescifrarSiEsPosible(numeroTelefono);

                using (var ctx = new TelefoniaContext())
                {
                    var cuenta = ctx.Cuentas
                        .FirstOrDefault(c => c.NumeroTelefono == telefonoDescifrado
                                          && c.Estado == "DISPONIBLE");

                    if (cuenta == null)
                    {
                        return new RespuestaOperacion
                        {
                            Resultado = false,
                            Mensaje = "Línea no encontrada o no está disponible."
                        };
                    }

                    ctx.Cuentas.Remove(cuenta);
                    ctx.SaveChanges();

                    return new RespuestaOperacion { Resultado = true, Mensaje = "Exitoso" };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaOperacion { Resultado = false, Mensaje = ex.Message };
            }
        }

        public LineaEnUsoDTO[] ListarLineasEnUso()
        {
            using (var ctx = new TelefoniaContext())
            {
                return ctx.Cuentas
                    .Where(c => c.Estado == "ACTIVO")
                    .AsEnumerable()
                    .Select(c => CrearLineaEnUso(c))
                    .ToArray();
            }
        }

        private static LineaEnUsoDTO CrearLineaEnUso(Cuenta cuenta)
        {
            string idTarjeta = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificadorTarjeta);
            string idTelefono = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificadorTelefono);
            string cedula = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificacionDuenno);

            return new LineaEnUsoDTO
            {
                NumeroTelefono = cuenta.NumeroTelefono,
                IdentificadorTarjeta = idTarjeta,
                IdentificadorTelefono = idTelefono,
                IdentificacionCliente = cedula,
                IdentificacionClienteCifrado = cuenta.IdentificacionDuenno,
                NombreCliente = ObtenerNombreCliente(cedula),
                TipoServicio = cuenta.TipoServicio,
                NumeroTelefonoCifrado = cuenta.NumeroTelefonoCifrado
                    ?? CifradorAES.Cifrar(cuenta.NumeroTelefono),
                IdentificadorTarjetaCifrado = cuenta.IdentificadorTarjeta
                    ?? (idTarjeta != null ? CifradorAES.Cifrar(idTarjeta) : null),
                IdentificadorTelefonoCifrado = cuenta.IdentificadorTelefono
                    ?? (idTelefono != null ? CifradorAES.Cifrar(idTelefono) : null)
            };
        }

        private static string ObtenerNombreCliente(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                return "Sin identificar";
            }

            try
            {
                var cliente = new AutenticacionClient();
                RespuestaObtenerUsuario resp = cliente.ObtenerUsuario(identificacion);

                if (resp.Resultado && resp.Usuario != null)
                {
                    return string.Format("{0} {1} {2}",
                        resp.Usuario.Nombre,
                        resp.Usuario.PrimerApellido,
                        resp.Usuario.SegundoApellido).Trim();
                }

                return "Sin registrar";
            }
            catch
            {
                return "Sin registrar";
            }
        }

        public LineaClienteDTO[] ListarLineasPorCliente(string identificacionCliente)
        {
            if (string.IsNullOrWhiteSpace(identificacionCliente))
            {
                return new LineaClienteDTO[0];
            }

            string cedula = identificacionCliente.Trim();

            using (var ctx = new TelefoniaContext())
            {
                // la cedula puede estar normal o cifrada, si esta cifrada  se convierte en memoria
                return ctx.Cuentas
                    .Where(c => c.Estado == "ACTIVO" && c.IdentificacionDuenno != null)
                    .AsEnumerable()
                    .Where(c => CifradorAES.DescifrarSiEsPosible(c.IdentificacionDuenno) == cedula)
                    .Select(c => CrearLineaCliente(c))
                    .ToArray();
            }
        }

        private static LineaClienteDTO CrearLineaCliente(Cuenta cuenta)
        {
            string idTarjeta = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificadorTarjeta);
            string idTelefono = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificadorTelefono);
            string cedula = CifradorAES.DescifrarSiEsPosible(cuenta.IdentificacionDuenno);

            return new LineaClienteDTO
            {
                NumeroTelefono = cuenta.NumeroTelefono,
                TipoServicio = cuenta.TipoServicio,
                Saldo = cuenta.TipoServicio == "PREPAGO" ? cuenta.Saldo : 0,
                MontoFacturaPendiente = cuenta.TipoServicio == "POSTPAGO" ? cuenta.SaldoPostpago : 0,
                NumeroTelefonoCifrado = cuenta.NumeroTelefonoCifrado
                    ?? CifradorAES.Cifrar(cuenta.NumeroTelefono),
                IdentificadorTarjeta = idTarjeta,
                IdentificadorTarjetaCifrado = cuenta.IdentificadorTarjeta
                    ?? (idTarjeta != null ? CifradorAES.Cifrar(idTarjeta) : null),
                IdentificadorTelefono = idTelefono,
                IdentificadorTelefonoCifrado = cuenta.IdentificadorTelefono
                    ?? (idTelefono != null ? CifradorAES.Cifrar(idTelefono) : null),
                IdentificacionCliente = cedula,
                IdentificacionClienteCifrado = cuenta.IdentificacionDuenno
            };
        }

        public FacturacionDTO UltimaFacturacion()
        {
            using (var ctx = new TelefoniaContext())
            {
                var ultima = ctx.FacturasPostpago
                    .OrderByDescending(f => f.FechaCalculo)
                    .FirstOrDefault();

                if (ultima == null)
                {
                    return new FacturacionDTO { Existe = false };
                }

                return new FacturacionDTO
                {
                    FechaCalculo = ultima.FechaCalculo.ToString("yyyy-MM-dd"),
                    FechaMaximaPago = ultima.FechaMaximaPago.ToString("yyyy-MM-dd"),
                    Existe = true
                };
            }
        }

        public RespuestaOperacion CargarSaldo(string numeroTelefono, decimal monto)
        {
            try
            {
                if (monto <= 0 || (monto % 1) != 0)
                {
                    return new RespuestaOperacion
                    {
                        Resultado = false,
                        Mensaje = "El monto debe ser un entero positivo."
                    };
                }

                string telefonoDescifrado = CifradorAES.DescifrarSiEsPosible(numeroTelefono);

                using (var ctx = new TelefoniaContext())
                {
                    var cuenta = ctx.Cuentas
                        .FirstOrDefault(c => c.NumeroTelefono == telefonoDescifrado
                                          && c.TipoServicio == "PREPAGO"
                                          && c.Estado == "ACTIVO");

                    if (cuenta == null)
                    {
                        return new RespuestaOperacion
                        {
                            Resultado = false,
                            Mensaje = "Línea no encontrada, no es prepago o no está activa."
                        };
                    }

                    cuenta.Saldo += monto;
                    ctx.SaveChanges();

                    return new RespuestaOperacion { Resultado = true, Mensaje = "Exitoso" };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaOperacion { Resultado = false, Mensaje = ex.Message };
            }
        }

        public RespuestaOperacion PagarFactura(string numeroTelefono)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroTelefono))
                {
                    return new RespuestaOperacion
                    {
                        Resultado = false,
                        Mensaje = "Número de teléfono requerido."
                    };
                }

                string telefonoDescifrado = CifradorAES.DescifrarSiEsPosible(numeroTelefono);

                using (var ctx = new TelefoniaContext())
                {
                    var factura = ctx.FacturasPostpago
                        .FirstOrDefault(f => f.NumeroTelefono == telefonoDescifrado
                                          && f.Estado == "PENDIENTE");

                    if (factura == null)
                    {
                        return new RespuestaOperacion
                        {
                            Resultado = false,
                            Mensaje = "No hay factura pendiente para esta línea."
                        };
                    }

                    factura.Estado = "PAGADA";

                    var cuenta = ctx.Cuentas
                        .FirstOrDefault(c => c.NumeroTelefono == telefonoDescifrado);

                    if (cuenta != null)
                    {
                        cuenta.SaldoPostpago = 0;
                        cuenta.FechaMaximaPago = null;
                    }

                    ctx.SaveChanges();

                    return new RespuestaOperacion { Resultado = true, Mensaje = "Exitoso" };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaOperacion { Resultado = false, Mensaje = ex.Message };
            }
        }
    }
}
