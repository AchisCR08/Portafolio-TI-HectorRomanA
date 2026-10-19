// Inicializa la base/colección usadas por WS_Autenticacion.
// MongoDB crea la base al insertar el primer documento; este script la crea explícitamente.
db = db.getSiblingDB("TelefoniaAuth");
if (!db.getCollectionNames().includes("usuarios")) {
  db.createCollection("usuarios");
}
