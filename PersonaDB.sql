-- ==============================================
-- CREAR BASE DE DATOS
-- ==============================================
IF DB_ID('PersonaDB') IS NOT NULL
    DROP DATABASE PersonaDB;
GO

CREATE DATABASE PersonaDB;
GO

USE PersonaDB;
GO

-- ==============================================
-- CREACIÓN DE TABLAS
-- ==============================================

IF OBJECT_ID('Departamento') IS NOT NULL DROP TABLE FacturaDetalle;
IF OBJECT_ID('Puesto') IS NOT NULL DROP TABLE Facturas;
IF OBJECT_ID('Persona') IS NOT NULL DROP TABLE Articulos;

GO

CREATE TABLE Departamento (
    iddepartamento INT PRIMARY KEY,
    descripcion NVARCHAR(100) NOT NULL
);

GO

CREATE TABLE Puesto (
    idpuesto INT PRIMARY KEY,
    descripcion NVARCHAR(100) NOT NULL
);

GO

CREATE TABLE Persona (
    idpersona INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    edad INT CHECK (edad >= 0),
    correo NVARCHAR(100),
    idpuesto INT,
    iddepartamento INT,
    FOREIGN KEY (idpuesto) REFERENCES Puesto(idpuesto),
    FOREIGN KEY (iddepartamento) REFERENCES Departamento(iddepartamento)
);

INSERT INTO Departamento (iddepartamento, descripcion) VALUES
(1, 'Recursos Humanos'),
(2, 'Finanzas'),
(3, 'Marketing'),
(4, 'Ventas'),
(5, 'Tecnología'),
(6, 'Logística'),
(7, 'Atención al Cliente'),
(8, 'Legal'),
(9, 'Compras'),
(10, 'Producción'),
(11, 'Calidad'),
(12, 'Investigación y Desarrollo'),
(13, 'Seguridad'),
(14, 'Administración'),
(15, 'Comunicación'),
(16, 'Sistemas'),
(17, 'Contabilidad'),
(18, 'Proyectos'),
(19, 'Innovación'),
(20, 'Operaciones');

INSERT INTO Puesto (idpuesto, descripcion) VALUES
(1, 'Analista de Datos'),
(2, 'Gerente de Proyecto'),
(3, 'Desarrollador Web'),
(4, 'Diseñador Gráfico'),
(5, 'Soporte Técnico'),
(6, 'Ejecutivo de Ventas'),
(7, 'Coordinador de Marketing'),
(8, 'Asistente Administrativo'),
(9, 'Especialista en Recursos Humanos'),
(10, 'Contador'),
(11, 'Ingeniero de Calidad'),
(12, 'Jefe de Producción'),
(13, 'Consultor Legal'),
(14, 'Investigador de Mercado'),
(15, 'Ingeniero de Software'),
(16, 'Técnico en Logística'),
(17, 'Redactor Creativo'),
(18, 'Desarrollador Backend'),
(19, 'Scrum Master'),
(20, 'Gerente General');

Go

CREATE PROCEDURE InsertarPersona
    @nombre NVARCHAR(100),
    @edad INT,
    @correo NVARCHAR(100),
    @idpuesto INT,
    @iddepartamento INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Persona (nombre, edad, correo, idpuesto, iddepartamento)
    VALUES (@nombre, @edad, @correo, @idpuesto, @iddepartamento);
END;

Go 

CREATE PROCEDURE BuscarPersona
    @nombrePersona NVARCHAR(100) = NULL,
    @descripcionDepartamento NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.idpersona,
        p.nombre,
        p.edad,
        p.correo,
        p.iddepartamento,
        p.idpuesto,
        pu.descripcion AS puesto,
        d.descripcion AS departamento
    FROM Persona p
    LEFT JOIN Puesto pu ON p.idpuesto = pu.idpuesto
    LEFT JOIN Departamento d ON p.iddepartamento = d.iddepartamento
    WHERE 
        (@nombrePersona IS NULL OR p.nombre LIKE '%' + @nombrePersona + '%') OR
        (@descripcionDepartamento IS NULL OR d.descripcion LIKE '%' + @descripcionDepartamento + '%');
END;

SELECT iddepartamento, descripcion FROM Departamento;
SELECT idpuesto, descripcion FROM Puesto;
SELECT * FROM Persona;