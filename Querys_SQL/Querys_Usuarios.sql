
--Crear la tabla de usuarios
CREATE TABLE Usuarios(
	IdUsuario TINYINT PRIMARY KEY IDENTITY,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Password CHAR(60) NOT NULL
);
GO

INSERT INTO Usuarios VALUES ('correo@correo.com', '123456');
GO