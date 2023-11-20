-- noinspection SpellCheckingInspectionForFile

-- Use master and recreate the database --
USE [master];

DROP DATABASE IF EXISTS [Escola];
GO;

CREATE DATABASE [Escola];
GO;

USE [Escola];

-- Create an Alunos table that should contain the school's students. --
CREATE TABLE [Alunos]
(
    NumeroInterno INT IDENTITY (1,1) NOT NULL,
    Nome          VARCHAR(50)        NOT NULL,
    Idade         INT                NOT NULL,
    Localidade    VARCHAR(50)        NOT NULL,

    PRIMARY KEY (NumeroInterno)
)

INSERT INTO [Alunos] (Nome, Idade, Localidade)
VALUES ('Alice', 15, 'Forte da Casa'),
       (N'António', 17, N'Póvoa de santa Íria'),
       (N'Gonçalo', 15, 'Forte da Casa'),
       ('Maria', 17, N'Póvoa de santa Íria'),
       (N'João', 15, 'Forte da Casa'),
       ('Ana Maria', 17, N'Póvoa de santa Íria'),
       ('Zulmira', 17, N'Póvoa de santa Íria'),
       ('Daniel', 18, 'Vialonga');
GO;

-- Create a Turmas table that should contain the school's classes. --
CREATE TABLE [Turmas]
(
    Turma VARCHAR(50) NOT NULL,
    Ano   INT         NOT NULL,

    PRIMARY KEY (Turma, Ano)
)

INSERT INTO [Turmas] (Turma, Ano)
VALUES ('PGPS', 10),
       ('PT', 10),
       ('PGPS', 11);
GO;

-- Create a Disciplinas table that should contain the school's subjects. --
CREATE TABLE [Disciplinas]
(
    CodigoDisciplina INT IDENTITY (1,1) NOT NULL,
    Descricao        VARCHAR(50)        NOT NULL,

    PRIMARY KEY (CodigoDisciplina)
)

INSERT INTO [Disciplinas] (Descricao)
VALUES (N'Português A'),
       (N'Inglês'),
       (N'Educação Física'),
       ('PSI'),
       ('AQC'),
       ('OTET');
GO;

-- Create a Matriculas table that should contain the school's enrollments. --
CREATE TABLE [Matriculas]
(
    NumeroInterno INT         NOT NULL,
    Turma         VARCHAR(50) NOT NULL,
    Ano           INT         NOT NULL,
    NumeroTurma   INT         NOT NULL,

    PRIMARY KEY (NumeroInterno, Turma, Ano),
    FOREIGN KEY (NumeroInterno) REFERENCES [Alunos] (NumeroInterno),
    FOREIGN KEY (Turma, Ano) REFERENCES [Turmas] (Turma, Ano)
)

INSERT INTO [Matriculas] (NumeroInterno, Turma, Ano, NumeroTurma)
VALUES (1, 'PT', 10, 15),
       (2, 'PGPS', 10, 2),
       (3, 'PT', 10, 11),
       (4, 'PGPS', 10, 11),
       (5, 'PGPS', 10, 20),
       (6, 'PGPS', 11, 22);
GO;

-- Create a TurmasDisciplinas table that should contain the school's subjects per class. --
CREATE TABLE [TurmasDisciplinas]
(
    Turma            VARCHAR(50) NOT NULL,
    Ano              INT         NOT NULL,
    CodigoDisciplina INT         NOT NULL,

    PRIMARY KEY (Turma, Ano, CodigoDisciplina),
    FOREIGN KEY (Turma, Ano) REFERENCES [Turmas] (Turma, Ano),
    FOREIGN KEY (CodigoDisciplina) REFERENCES [Disciplinas] (CodigoDisciplina)
)

INSERT INTO [TurmasDisciplinas]
VALUES ('PT', '10', 1),
       ('PT', '10', 2),
       ('PT', '10', 3),
       ('PT', '10', 6),
       ('PGPS', '10', 1),
       ('PGPS', '10', 2),
       ('PGPS', '10', 3),
       ('PGPS', '10', 4),
       ('PGPS', '10', 5),
       ('PGPS', '11', 1),
       ('PGPS', '11', 2),
       ('PGPS', '11', 3),
       ('PGPS', '11', 4),
       ('PGPS', '11', 5);
GO;