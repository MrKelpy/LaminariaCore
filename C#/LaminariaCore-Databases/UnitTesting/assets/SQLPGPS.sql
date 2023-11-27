use master
go

create database CursoPGPS
go
use CursoPGPS
go

create table Alunos 
(Nprocesso int,
Nome varchar (50) not null,
Pin int not null,
constraint PKAluno primary key(Nprocesso))

go

create table Disciplinas 
(IdDisciplina varchar (5),
Disciplina varchar (50) not null,
TotalModulos int not null,
constraint PKDisciplina primary key (IdDisciplina))
go

create table Classificacoes
(Nprocesso int,
IdDisciplina varchar(5),
Modulo int,
Classificacao int not null,
constraint PKClassificacao primary key (Nprocesso, IdDisciplina,Modulo),
constraint FKAluno foreign key (Nprocesso) references Alunos(Nprocesso),
constraint FKDisciplina foreign key (IdDisciplina) references Disciplinas(IdDisciplina))
go

insert into Disciplinas values
('POR', 'Portugu�s', 9),
('ING','Ingl�s',9),
('ARI','�rea de Integra��o',6),
('TIC','Tecnologias de Informa��o Comunica��o',3),
('EDF','Educa��o Fisica',16),
('MAT','Mat�matica',9),
('FSQ','Fisica-Quimica',14),
('AQC','Arquitetura de Computadores',5),
('SOP','Sistermas Operativos',5),
('RDC','Redes de Comunica��o',8),
('PSI','Programa��o e Sistemas de Comunica��o',19)
go


UPDATE CursoPGPS.dbo.Classificacoes SET Classificacao = 15 WHERE Nprocesso = 21206 AND IdDisciplina = 'AQC' AND Modulo = 2