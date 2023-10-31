-- noinspection SpellCheckingInspectionForFile

-- Use master and recreate the database --
USE [master];

DROP DATABASE IF EXISTS [Escola];
GO;

CREATE DATABASE [Escola];
GO;

USE [Escola];

-- Create an Alunos table that should contain the school's students. --
CREATE TABLE [Alunos] (
	NumeroInterno INT IDENTITY (1,1) NOT NULL,
	Nome VARCHAR(50) NOT NULL,
	Idade INT NOT NULL,
	Localidade VARCHAR(50) NOT NULL,

	PRIMARY KEY(NumeroInterno)
)

INSERT INTO [Alunos] (Nome, Idade, Localidade) VALUES
('Alice', 15, 'Forte da Casa'),
(N'António', 17, N'Póvoa de santa Íria'),
(N'Gonçalo', 15, 'Forte da Casa'),
('Maria', 17, N'Póvoa de santa Íria'),
(N'João', 15, 'Forte da Casa'),
('Ana Maria', 17, N'Póvoa de santa Íria'),
('Zulmira', 17, N'Póvoa de santa Íria'),
('Daniel', 18, 'Vialonga');
GO;

-- Create a Turmas table that should contain the school's classes. --
CREATE TABLE [Turmas] (
	Turma VARCHAR(50) NOT NULL,
	Ano INT NOT NULL,

	PRIMARY KEY(Turma, Ano)
)

INSERT INTO [Turmas] (Turma, Ano) VALUES
('PGPS',10),
('PT',10),
('PGPS',11);
GO;

-- Create a Disciplinas table that should contain the school's subjects. --
CREATE TABLE [Disciplinas] (
	CodigoDisciplina INT IDENTITY(1,1) NOT NULL,
	Descricao VARCHAR(50) NOT NULL,

	PRIMARY KEY(CodigoDisciplina)
)

INSERT INTO [Disciplinas] (Descricao) VALUES
(N'Português A'),
(N'Inglês'),
(N'Educação Física'),
('PSI'),
('AQC'),
('OTET');
GO;

-- Create a Matriculas table that should contain the school's enrollments. --
CREATE TABLE [Matriculas] (
	NumeroInterno INT NOT NULL,
	Turma VARCHAR(50) NOT NULL,
	Ano INT NOT NULL,
	NumeroTurma INT NOT NULL,

	PRIMARY KEY(NumeroInterno, Turma, Ano),
	FOREIGN KEY(NumeroInterno) REFERENCES [Alunos](NumeroInterno),
	FOREIGN KEY(Turma, Ano) REFERENCES [Turmas](Turma, Ano)
)

INSERT INTO [Matriculas] (NumeroInterno, Turma, Ano, NumeroTurma) VALUES
(1, 'PT', 10, 15),
(2, 'PGPS', 10, 2),
(3, 'PT', 10, 11),
(4, 'PGPS', 10, 11),
(5, 'PGPS', 10, 20),
(6, 'PGPS', 11, 22);
GO;

-- Create a TurmasDisciplinas table that should contain the school's subjects per class. --
CREATE TABLE [TurmasDisciplinas] (
	Turma VARCHAR(50) NOT NULL,
	Ano INT NOT NULL,
	CodigoDisciplina INT NOT NULL,

	PRIMARY KEY(Turma, Ano, CodigoDisciplina),
	FOREIGN KEY(Turma, Ano) REFERENCES [Turmas](Turma, Ano),
	FOREIGN KEY(CodigoDisciplina) REFERENCES [Disciplinas](CodigoDisciplina)
)

INSERT INTO [TurmasDisciplinas] VALUES
('PT', '10', 1),
('PT', '10', 2),
('PT', '10', 3),
('PT', '10', 6),
('PGPS', '10', 1),
('PGPS', '10', 2),
('PGPS', '10', 3),
('PGPS', '10', 4),
('PGPS', '10',5),
('PGPS', '11', 1),
('PGPS', '11', 2),
('PGPS', '11', 3),
('PGPS', '11', 4),
('PGPS', '11', 5);
GO;

-- Get the names of all the students in the school in alphabetical order. --
SELECT Nome FROM [Alunos] ORDER BY Nome;

-- Get the names and ages of all the students ordered by name --
SELECT Nome, Idade FROM [Alunos] ORDER BY Nome;

-- Get the names, year and classes of all the students ordered by name --
SELECT Nome, Ano, Turma FROM [Alunos]
	INNER JOIN [Matriculas]
	ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno ORDER BY Nome;

-- Get the age means ordered by year and class --
SELECT Ano, Turma, AVG(Idade) AS [Media] FROM [Alunos]
	INNER JOIN [Matriculas]
	ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno
	GROUP BY Ano, Turma;

-- Get the amount of students enrolled in a year and class --
SELECT Ano, Turma, COUNT(Nome) AS [Numero] FROM [Alunos]
	INNER JOIN [Matriculas] ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno
	GROUP BY Ano, Turma

-- Get all the students with ages between 15 and 17 --
SELECT Nome, Idade FROM [Alunos] WHERE Idade BETWEEN 15 AND 17;

-- Get all the students with ages equal to 15 and 17 --
SELECT Nome, Idade FROM [Alunos] WHERE IDADE IN (15, 17);

-- Get all the students with ages not between from 15 and 17 --
SELECT Nome, Idade FROM [Alunos] WHERE Idade NOT BETWEEN 15 AND 17;

-- Get all the students that have the 'Português A' subject --
SELECT Nome FROM [Alunos]
	INNER JOIN [Matriculas] ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno
	INNER JOIN [TurmasDisciplinas] ON [Matriculas].Turma = [TurmasDisciplinas].Turma
            AND [Matriculas].Ano = [TurmasDisciplinas].Ano
	WHERE CodigoDisciplina = 1;

-- Get the description of the classes by year and class --
SELECT Ano, Turma, Descricao FROM [Disciplinas]
	INNER JOIN [TurmasDisciplinas] ON [Disciplinas].CodigoDisciplina = [TurmasDisciplinas].CodigoDisciplina
	ORDER BY Ano, Turma;

-- Get the count of subjects by year and class
SELECT Ano, Turma, COUNT(CodigoDisciplina) FROM [TurmasDisciplinas]
	GROUP BY Ano, Turma;

-- Get the count of subjects by year and class, only if the count is greater than 6 --
SELECT Ano, Turma, COUNT(CodigoDisciplina) FROM [TurmasDisciplinas]
	GROUP BY Ano, Turma
	HAVING COUNT(CodigoDisciplina) > 6;

-- Get the name of the students that live in Vialonga --
SELECT Nome FROM [Alunos] WHERE Localidade = 'Vialonga';

-- Get the amount of students by locality --
SELECT Localidade, COUNT(Nome) FROM [Alunos]
	GROUP BY Localidade;

-- Get the locality with the most students --
SELECT TOP 1 Localidade, COUNT(NumeroInterno)  FROM [Alunos]
	GROUP BY Localidade
	ORDER BY COUNT(NumeroInterno) DESC;

-- Get the amount of students per year --
SELECT Ano, COUNT(Nome) FROM [Alunos]
	INNER JOIN [Matriculas] ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno
	GROUP BY Ano;

-- Get the name and locality of the year 11 students --
SELECT Nome, Localidade FROM [Alunos]
	INNER JOIN [Matriculas] ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno
	WHERE Ano = 11;

-- Update the age of the Forte da Casa 10PGPS Students to 16 years old --
UPDATE [Alunos] SET Idade = 16
	WHERE NumeroInterno IN (SELECT NumeroInterno FROM [Matriculas]
		WHERE Turma = 'PGPS' AND Ano = 10 AND Localidade = 'Forte da Casa');

-- Delete PE Class--
DELETE FROM [TurmasDisciplinas] WHERE CodigoDisciplina = 3;
DELETE [Disciplinas] WHERE CodigoDisciplina = 3;