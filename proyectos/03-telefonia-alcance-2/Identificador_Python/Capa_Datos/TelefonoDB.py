from Capa_Datos.Conexion import Conexion
from Capa_Negocio.Bitacora import Bitacora


class TelefonoDB:

    def buscarTelefono(self, numero):
        try:
            conexion = Conexion().conectar()
            cursor = conexion.cursor()

            sql = """
            SELECT id, numero, activo, proveedor_id
            FROM telefono
            WHERE numero = %s
            """

            cursor.execute(sql, (numero,))
            resultado = cursor.fetchone()

            cursor.close()
            conexion.close()

            return resultado
        except Exception as error:
            Bitacora.registrar("ERROR", {
                "componente": "TelefonoDB",
                "mensaje": str(error)
            })
            raise

    def buscarProveedorId(self, numero):
        telefono = self.buscarTelefono(numero)

        if telefono is None:
            return None

        return telefono[3]

    def buscarProveedorIdPorNombre(self, nombreProveedor):
        try:
            conexion = Conexion().conectar()
            cursor = conexion.cursor()

            cursor.execute(
                "SELECT id FROM proveedor WHERE nombre = %s",
                (nombreProveedor,)
            )
            resultado = cursor.fetchone()

            if resultado is None:
                cursor.execute(
                    "INSERT INTO proveedor(nombre) VALUES (%s)",
                    (nombreProveedor,)
                )
                conexion.commit()
                proveedorId = cursor.lastrowid
            else:
                proveedorId = resultado[0]

            cursor.close()
            conexion.close()

            return proveedorId
        except Exception as error:
            Bitacora.registrar("ERROR", {
                "componente": "TelefonoDB",
                "mensaje": str(error)
            })
            raise

    def activarOInactivarTelefono(self, numero, activar, nombreProveedor="XYZ"):
        """
        Usado por IDENTIFICADOR6. Si el telefono no existe en la base del
        identificador lo crea (linea nueva del proveedor), si ya existe
        solo actualiza el estado activo/inactivo.
        """
        try:
            proveedorId = self.buscarProveedorIdPorNombre(nombreProveedor)

            conexion = Conexion().conectar()
            cursor = conexion.cursor()

            cursor.execute(
                "SELECT id FROM telefono WHERE numero = %s",
                (numero,)
            )
            existente = cursor.fetchone()

            if existente is None:
                cursor.execute(
                    "INSERT INTO telefono(numero, activo, proveedor_id) VALUES (%s, %s, %s)",
                    (numero, activar, proveedorId)
                )
            else:
                cursor.execute(
                    "UPDATE telefono SET activo = %s, proveedor_id = %s WHERE numero = %s",
                    (activar, proveedorId, numero)
                )

            conexion.commit()
            cursor.close()
            conexion.close()

            return True
        except Exception as error:
            Bitacora.registrar("ERROR", {
                "componente": "TelefonoDB",
                "mensaje": str(error)
            })
            raise
