// Ejecutar con: mongosh < datos_prueba_mongo_autenticacion.js
// o pegando el contenido dentro de mongosh / MongoDB Compass (shell).
//
// usuario y contrasena estan cifrados con AES/ECB/PKCS7 + Base64,
// clave "REEMPLAZAR_EN_CONFIGURACION_LOCAL" (la misma que usa todo el sistema).
//
// Contiene documentos de prueba con usuario y contraseña cifrados.
// Esos valores se generaron con la clave local usada en la entrega original;
// deben regenerarse si se cambia la clave de cifrado.

use TelefoniaAuth;

db.usuarios.deleteMany({
    _id: { $in: ["118880999", "205550777"] }
});

db.usuarios.insertMany([
  {
    _id: "118880999",
    Nombre: "Hector",
    PrimerApellido: "Perez",
    SegundoApellido: "Vargas",
    CorreoElectronico: "hector.perez@centralgeneral.com",
    Usuario: "kilcIqfWRJ2AvuHkbTwShg==",
    Contrasena: "m01J9mg/m/N+yqxffuVK9Q==",
    Estado: "activo",
    Tipo: "1"
  },
  {
    _id: "205550777",
    Nombre: "Maria",
    PrimerApellido: "Rojas",
    SegundoApellido: "Solano",
    CorreoElectronico: "maria.rojas@correo.com",
    Usuario: "GazXrEuuYvl3+cFPE7qexQ==",
    Contrasena: "CIkdaJ6FMpbHFIr63VQv0Q==",
    Estado: "activo",
    Tipo: "2"
  }
]);

print("Usuarios de prueba insertados en TelefoniaAuth.usuarios");

