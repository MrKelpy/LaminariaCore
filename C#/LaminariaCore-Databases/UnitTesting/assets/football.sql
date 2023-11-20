DROP DATABASE IF EXISTS Futebol;
GO
CREATE DATABASE Futebol
GO
USE Futebol
GO

CREATE TABLE Equipa
(
    codEquipa     INT IDENTITY (1,1) PRIMARY KEY,
    nome          VARCHAR(100) NOT NULL,
    Nacionalidade VARCHAR(100) NOT NULL,
    AnoFundacao   INT          NOT NULL
)
GO

CREATE TABLE paises
(
    pais VARCHAR(100) PRIMARY KEY
)
GO

INSERT INTO paises
VALUES ('Portugal'),
       ('Espanha'),
       ('França'),
       ('Itália'),
       ('Turquia'),
       ('Inglaterra')
GO

INSERT INTO Equipa
VALUES ('Sport Lisboa e Olivais', 'Portugal', 1999),
       ('Monaco Futebol Club', 'França', 1934),
       ('Juventus', 'Itália', 1930)
