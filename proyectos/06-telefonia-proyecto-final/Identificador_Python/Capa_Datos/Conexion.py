import os

import mysql.connector


class Conexion:

    

    def conectar(self):
        return mysql.connector.connect(
            host="localhost",
            user="root",         #poner el usuario de mysql
            password="REEMPLAZAR_EN_CONFIGURACION_LOCAL", #poner la contraseña de mysql
            database="IdentificadorDB"
        )
        
