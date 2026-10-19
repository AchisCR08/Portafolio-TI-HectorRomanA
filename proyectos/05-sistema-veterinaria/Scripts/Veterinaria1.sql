
CREATE DATABASE Veterinaria1;
GO

USE Veterinaria1;
GO

CREATE TABLE Provincia (
    id_provincia INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Canton (
    id_canton INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_provincia INT NOT NULL,
    FOREIGN KEY (id_provincia) REFERENCES Provincia(id_provincia)
);

CREATE TABLE Distrito (
    id_distrito INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_canton INT NOT NULL,
    FOREIGN KEY (id_canton) REFERENCES Canton(id_canton)
);

CREATE TABLE Sucursal (
    id_sucursal INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100),
    direccion VARCHAR(200),
    id_distrito INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_distrito) REFERENCES Distrito(id_distrito)
);

CREATE TABLE Rol (
    id_rol INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Persona (
    id_persona INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100),
    direccion VARCHAR(200),
    id_distrito INT NULL,
    id_sucursal INT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_distrito) REFERENCES Distrito(id_distrito),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE Usuario (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    id_persona INT NOT NULL UNIQUE,
    id_rol INT NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_persona) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_rol) REFERENCES Rol(id_rol)
);

CREATE TABLE TipoMascota (
    id_tipo_mascota INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE TamanoMascota (
    id_tamano INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    factor_precio DECIMAL(4,2),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE TipoPelaje (
    id_tipo_pelaje INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    factor_precio DECIMAL(4,2),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Raza (
    id_raza INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_tipo_mascota INT NOT NULL,
    id_tamano INT NOT NULL,
    id_tipo_pelaje INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_tipo_mascota) REFERENCES TipoMascota(id_tipo_mascota),
    FOREIGN KEY (id_tamano) REFERENCES TamanoMascota(id_tamano),
    FOREIGN KEY (id_tipo_pelaje) REFERENCES TipoPelaje(id_tipo_pelaje)
);

CREATE TABLE Mascota (
    id_mascota INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    fecha_nacimiento DATE NULL,
    sexo CHAR(1) NULL,
    peso_actual DECIMAL(5,2) NULL,
    id_raza INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,

    FOREIGN KEY (id_raza) REFERENCES Raza(id_raza),

    -- macho o hembra 
    CONSTRAINT CK_Mascota_Sexo 
        CHECK (sexo IN ('M','H')),

    -- para que el peso no sea negativo
    CONSTRAINT CK_Mascota_Peso 
        CHECK (peso_actual IS NULL OR peso_actual >= 0)
);

CREATE TABLE Duenos_Mascotas (
    id_persona INT NOT NULL,
    id_mascota INT NOT NULL,
    es_principal BIT,
    PRIMARY KEY (id_persona,id_mascota),
    FOREIGN KEY (id_persona) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_mascota) REFERENCES Mascota(id_mascota)
);

CREATE TABLE EstadoCita (
    id_estado_cita INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE ServicioGrooming (
    id_servicio_grooming INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    descripcion VARCHAR(MAX),
    precio_base DECIMAL(10,2),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE CitaVeterinaria (
    id_cita_vet INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    motivo VARCHAR(200),
    id_mascota INT NOT NULL,
    id_veterinario INT NOT NULL,
    id_estado_cita INT NOT NULL,
    id_sucursal INT NOT NULL,
    FOREIGN KEY (id_mascota) REFERENCES Mascota(id_mascota),
    FOREIGN KEY (id_veterinario) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_estado_cita) REFERENCES EstadoCita(id_estado_cita),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE CitaGrooming (
    id_cita_grooming INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE,
    hora TIME,
    id_mascota INT NOT NULL,
    id_groomer INT NOT NULL,
    id_estado_cita INT NOT NULL,
    precio_final DECIMAL(10,2),
    id_servicio_grooming INT NOT NULL,
    id_sucursal INT NOT NULL,
    FOREIGN KEY (id_mascota) REFERENCES Mascota(id_mascota),
    FOREIGN KEY (id_groomer) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_estado_cita) REFERENCES EstadoCita(id_estado_cita),
    FOREIGN KEY (id_servicio_grooming) REFERENCES ServicioGrooming(id_servicio_grooming),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE Consulta (
    id_consulta INT IDENTITY(1,1) PRIMARY KEY,
    id_cita_vet INT NOT NULL UNIQUE,
    diagnostico VARCHAR(MAX),
    tratamiento VARCHAR(MAX),
    observaciones VARCHAR(MAX),
    FOREIGN KEY (id_cita_vet) REFERENCES CitaVeterinaria(id_cita_vet)
);

CREATE TABLE Vacuna (
    id_vacuna INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    descripcion VARCHAR(MAX),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE HistorialVacuna (
    id_historial INT IDENTITY(1,1) PRIMARY KEY,
    id_mascota INT NOT NULL,
    id_vacuna INT NOT NULL,
    fecha_aplicacion DATE,
    proxima_dosis DATE,
    FOREIGN KEY (id_mascota) REFERENCES Mascota(id_mascota),
    FOREIGN KEY (id_vacuna) REFERENCES Vacuna(id_vacuna)
);

CREATE TABLE Categoria (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Proveedor (
    id_proveedor INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    telefono VARCHAR(20),
    correo VARCHAR(100),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Producto (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    descripcion VARCHAR(MAX),
    precio_venta DECIMAL(10,2),
    id_categoria INT NOT NULL,
    id_proveedor INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria),
    FOREIGN KEY (id_proveedor) REFERENCES Proveedor(id_proveedor)
);

CREATE TABLE InventarioSucursal (
    id_sucursal INT NOT NULL,
    id_producto INT NOT NULL,
    stock INT NOT NULL,
    PRIMARY KEY (id_sucursal,id_producto),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);

CREATE TABLE Compra (
    id_compra INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE,
    id_proveedor INT NOT NULL,
    id_sucursal INT NOT NULL,
    FOREIGN KEY (id_proveedor) REFERENCES Proveedor(id_proveedor),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE DetalleCompra (
    id_detalle_compra INT IDENTITY(1,1) PRIMARY KEY,
    id_compra INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT,
    precio_unitario DECIMAL(10,2),
    FOREIGN KEY (id_compra) REFERENCES Compra(id_compra),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);

CREATE TABLE Factura (
    id_factura INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE,
    id_persona INT NOT NULL,
    id_sucursal INT NOT NULL,
    total DECIMAL(10,2),
    FOREIGN KEY (id_persona) REFERENCES Persona(id_persona),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE DetalleFactura (
    id_detalle_factura INT IDENTITY(1,1) PRIMARY KEY,
    id_factura INT NOT NULL,

    id_producto INT NULL,
    id_cita_vet INT NULL,
    id_cita_grooming INT NULL,

    cantidad INT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal AS (cantidad * precio_unitario) PERSISTED,

    FOREIGN KEY (id_factura) REFERENCES Factura(id_factura),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto),
    FOREIGN KEY (id_cita_vet) REFERENCES CitaVeterinaria(id_cita_vet),
    FOREIGN KEY (id_cita_grooming) REFERENCES CitaGrooming(id_cita_grooming),

    /*que tenga una por lo menos porque puede quedar vacio xd*/
    CONSTRAINT CK_DetalleFactura_Origen
    CHECK (
        id_producto IS NOT NULL OR
        id_cita_vet IS NOT NULL OR
        id_cita_grooming IS NOT NULL
    ),

    /*Cantidad mayor a 0*/
    CONSTRAINT CK_DetalleFactura_Cantidad
    CHECK (cantidad IS NULL OR cantidad > 0)
);

CREATE TABLE TipoPago (
    id_tipo_pago INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50),
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Pago (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    id_factura INT NOT NULL,
    id_tipo_pago INT NOT NULL,
    monto DECIMAL(10,2),
    fecha_pago DATE,
    FOREIGN KEY (id_factura) REFERENCES Factura(id_factura),
    FOREIGN KEY (id_tipo_pago) REFERENCES TipoPago(id_tipo_pago)
);
