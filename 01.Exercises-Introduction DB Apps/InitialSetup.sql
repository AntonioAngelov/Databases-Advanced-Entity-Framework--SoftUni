CREATE DATABASE MinionsDB

USE MinionsDB

CREATE TABLE Countries
(
Id int IDENTITY(1, 1) PRIMARY KEY,
Name varchar(25) UNIQUE
)

CREATE TABLE Towns
(
Id int IDENTITY(1, 1) PRIMARY KEY,
Name varchar(25),
CountryId int,
CONSTRAINT FK_Towns_Countries
FOREIGN KEY (CountryId)
REFERENCES Countries(Id)
ON DELETE CASCADE
)

CREATE TABLE Minions
(
Id int IDENTITY(1, 1) PRIMARY KEY,
Name varchar(25),
Age int CHECK(Age > 0),
TownId int,
CONSTRAINT FK_Minions_Towns
FOREIGN KEY (TownId)
REFERENCES Towns(Id)
ON DELETE CASCADE
)

CREATE TABLE Villains
(
Id int IDENTITY(1, 1) PRIMARY KEY,
Name varchar(25),
Evilness varchar(11) CHECK(Evilness IN ('good', 'bad', 'evil', 'super evil'))
)

CREATE TABLE VillainsMinions
(
VillainId int,
MinionsId int,
CONSTRAINT FK_VillainsMinions_Minions
FOREIGN KEY (MinionsId)
REFERENCES Minions(Id)
ON DELETE CASCADE,
CONSTRAINT FK_VillainsMinions_Villains
FOREIGN KEY (VillainId)
REFERENCES Villains(Id)
ON DELETE CASCADE,
CONSTRAINT PK_VillainId_MinionsId
PRIMARY KEY (VillainId, MinionsId)
)


INSERT INTO Countries(Name)
VALUES ('Bulgaria'),
       ('USA'),
	   ('France'),
	   ('China'),
	   ('India')

INSERT INTO Towns(Name, CountryId)
VALUES ('Sofia', 1),
       ('Los Angeles', 2),
	   ('Paris', 3),
	   ('Beijing', 4),
	   ('Mumbai', 5)

INSERT INTO Minions(Name, Age, TownId)
VALUES ('Lala', 235, 1),
       ('Burush', 300, 2),
	   ('Miko', 100, 3),
	   ('Chinchao', 567, 4),
	   ('Habib', 334, 5)


INSERT INTO Villains(Name, Evilness)
VALUES ('GRU', 'good'),
       ('Hitler', 'good'),
	   ('Musollini', 'evil'),
	   ('Kuci', 'bad'),
	   ('Stanishev', 'super evil')

INSERT INTO VillainsMinions(VillainId, MinionsId)
VALUES (1, 1),
       (1, 2),
	   (2, 1),
	   (4, 5),
	   (3, 4)