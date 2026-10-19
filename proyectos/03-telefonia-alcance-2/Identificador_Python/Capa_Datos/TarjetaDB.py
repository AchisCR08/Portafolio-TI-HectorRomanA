from Capa_Datos.Conexion import Conexion
from Capa_Negocio.Bitacora import Bitacora


class TarjetaDB:

    def buscarTarjeta(self, telefono):
        try:
            conexion = Conexion().conectar()
            cursor = conexion.cursor()

            sql = """
            SELECT
                t.numero,
                ta.identificador_telefono,
                ta.identificador_tarjeta
            FROM telefono t
            INNER JOIN tarjeta ta
                ON t.id = ta.telefono_id
            WHERE t.numero = %s
            """

            cursor.execute(sql, (telefono,))
            resultado = cursor.fetchone()

            cursor.close()
            conexion.close()

            return resultado
        except Exception as error:
            Bitacora.registrar("ERROR", {
                "componente": "TarjetaDB",
                "mensaje": str(error)
            })
            raise

    def guardarTarjeta(self, telefono, identificadorTelefono, identificadorTarjeta):
        """
        Usado por IDENTIFICADOR6. Inserta o actualiza el identificador de
        telefono/tarjeta asociado a la linea que se esta activando.
        """
        try:
            conexion = Conexion().conectar()
            cursor = conexion.cursor()

            cursor.execute(
                "SELECT id FROM telefono WHERE numero = %s",
                (telefono,)
            )
            telefonoFila = cursor.fetchone()

            if telefonoFila is None:
                cursor.close()
                conexion.close()
                raise ValueError("Telefono no registrado, no se puede guardar la tarjeta.")

            telefonoId = telefonoFila[0]

            cursor.execute(
                "SELECT id FROM tarjeta WHERE telefono_id = %s",
                (telefonoId,)
            )
            tarjetaFila = cursor.fetchone()

            if tarjetaFila is None:
                cursor.execute(
                    """
                    INSERT INTO tarjeta(telefono_id, identificador_telefono, identificador_tarjeta)
                    VALUES (%s, %s, %s)
                    """,
                    (telefonoId, identificadorTelefono, identificadorTarjeta)
                )
            else:
                cursor.execute(
                    """
                    UPDATE tarjeta
                    SET identificador_telefono = %s, identificador_tarjeta = %s
                    WHERE telefono_id = %s
                    """,
                    (identificadorTelefono, identificadorTarjeta, telefonoId)
                )

            conexion.commit()
            cursor.close()
            conexion.close()

            return True
        except Exception as error:
            Bitacora.registrar("ERROR", {
                "componente": "TarjetaDB",
                "mensaje": str(error)
            })
            raise
