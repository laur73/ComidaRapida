CREATE DATABASE ComidaRapida;

USE ComidaRapida;

CREATE TABLE Roles(
  IdRol INT IDENTITY PRIMARY KEY,
  Role VARCHAR(13) NOT NULL
);

CREATE TABLE Usuarios(
  IdUsuario INT IDENTITY PRIMARY KEY,
  Nombre VARCHAR(100) NOT NULL,
  Usuario VARCHAR(20) NOT NULL,
  Pwd VARCHAR(20) NOT NULL,
  FechaAlta DATETIME NOT NULL,
  RoleId INT NOT NULL,
  CONSTRAINT fk_usuario_role FOREIGN KEY (RoleId) REFERENCES Roles(IdRol)
);

CREATE TABLE Pedidos(
  IdPedido INT IDENTITY PRIMARY KEY,
  NombrePlatillo VARCHAR(50) NOT NULL,
  Costo DECIMAL(18,2) NOT NULL,
  Cantidad INT NOT NULL,
  Estatus VARCHAR(20) NOT NULL DEFAULT 'Recibido',
  Cliente VARCHAR(100),
  Direccion VARCHAR(255),
  TipoPago VARCHAR(50),
  Vendedor INT NOT NULL,
  Repartidor INT,
  FechaAlta DATETIME NOT NULL,
  FechaFinalizado DATETIME,
  CONSTRAINT fk_vendedor_usuario FOREIGN KEY (Vendedor) REFERENCES Usuarios(IdUsuario),
  CONSTRAINT fk_repartidor_usuario FOREIGN KEY (Repartidor) REFERENCES Usuarios(IdUsuario)
);

INSERT INTO Roles (role) VALUES ('Administrador');
INSERT INTO Roles (role) VALUES ('Vendedor');
INSERT INTO Roles (role) VALUES ('Repartidor');


INSERT INTO Usuarios  VALUES ('Oswaldo Salazar Aburto', 'admin','admin',GETDATE(), 1);
INSERT INTO Usuarios VALUES ('Aldo Imanol Huerta Falfan', 'vendedor','vendedor',GETDATE(), 2);
INSERT INTO Usuarios VALUES ('Alexis Clemente Ángel', 'repartidor','repartidor',GETDATE(), 3);

SELECT * FROM Roles
SELECT * FROM Usuarios
SELECT * FROM Pedidos

SELECT 1 FROM Usuarios WHERE Nombre = 'Oswaldo Salazar Aburto' AND Usuario = 'admin'

--Vendedores (usuario 5)
SELECT IdUsuario, Nombre, Usuario, FechaAlta FROM Usuarios WHERE roleId = 2

--Repartidores (usuario 3)
SELECT IdUsuario, Nombre, Usuario, FechaAlta FROM Usuarios WHERE roleId = 3

INSERT INTO Pedidos VALUES ('Pizza', 40, 3, 'Recibido', 'Josue', 'La Magdalena','Efectivo', 5, 3, GETDATE(),Null)

INSERT INTO Pedidos (NombrePlatillo, Costo, Cantidad, Estatus, Cliente, Direccion, TipoPago, Vendedor, FechaAlta, FechaFinalizado)
VALUES ('Tamales', 10, 5, 'Recibido', 'Oner', 'Bussan', 'Efectivo', 5, GETDATE(),'')


DELETE FROM Pedidos

SELECT * FROM Pedidos

SELECT IdPedido, NombrePlatillo, Costo, Cantidad, Cliente, Direccion, TipoPago, v.Nombre AS Vendedor, r.Nombre AS Repartidor,
Pedidos.FechaAlta, FechaFinalizado, Estatus
FROM Pedidos
INNER JOIN Usuarios v
ON v.IdUsuario = Pedidos.Vendedor
LEFT JOIN Usuarios r
ON r.IdUsuario = Pedidos.Repartidor

SELECT GETDATE() AS Fecha


