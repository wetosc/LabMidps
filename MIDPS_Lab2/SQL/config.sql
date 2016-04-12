use MiddleEarth;

-------------------------
-- Create Tables--------
-------------------------

CREATE TABLE Ring (
  id INTEGER IDENTITY NOT NULL,
  Material VARCHAR(50) NULL DEFAULT NULL,
  Name VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (id)
);
CREATE TABLE Wizard (
  id INTEGER IDENTITY NOT NULL,
  Name VARCHAR(50) NULL DEFAULT NULL,
  Color VARCHAR(50) NULL DEFAULT NULL,
  Hobbit_Friend INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Master2Ring (
  Ring_ID INTEGER NULL DEFAULT NULL,
  Master_ID INTEGER NULL DEFAULT NULL,
);

CREATE TABLE Hobbit (
  id INTEGER IDENTITY NOT NULL,
  Name VARCHAR(50) NULL DEFAULT NULL,
  Region VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Elf (
  id INTEGER IDENTITY NOT NULL,
  Name VARCHAR(50) NULL DEFAULT NULL,
  Category VARCHAR(50) NULL DEFAULT NULL,
  Hobbit_Friend INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Orc (
  id INTEGER IDENTITY NOT NULL,
  Power FLOAT NULL DEFAULT NULL,
  Master_ID INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

-------------------------
-- Create Foreign keys --
-------------------------

ALTER TABLE Wizard ADD CONSTRAINT Hobbit_Friend_FK FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id);
ALTER TABLE Master2Ring ADD CONSTRAINT  Ring_ID_FK FOREIGN KEY (Ring_ID) REFERENCES Ring (id);
ALTER TABLE Master2Ring ADD CONSTRAINT  Master_ID_FK FOREIGN KEY (Master_ID) REFERENCES Wizard (id);
ALTER TABLE Elf ADD CONSTRAINT  Hobbit_Friend_FK2 FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id);
ALTER TABLE Orc ADD CONSTRAINT  Master_ID_FK2 FOREIGN KEY (Master_ID) REFERENCES Wizard (id);

-------------------------
-- Add values for test --
-------------------------
-- Order:
/*1*/INSERT INTO Ring (Material,Name) VALUES ('Wood','Aredian');
/*1*/INSERT INTO Ring (Material,Name) VALUES ('Silver','Turmonil');
/*1*/INSERT INTO Ring (Material,Name) VALUES ('Wood','Canterbro');
/*2*/INSERT INTO Hobbit (Name,Region) VALUES ('Denduf','West');
/*2*/INSERT INTO Hobbit (Name,Region) VALUES ('Arpin','Hills');
/*3*/INSERT INTO Wizard (Name,Color,Hobbit_Friend) VALUES ('Heirin','Yellow', 1);
/*3*/INSERT INTO Wizard (Name,Color,Hobbit_Friend) VALUES ('Olsif','Black', 1);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (1,1);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (1,2);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (2,2);
/*5*/INSERT INTO Elf (Name,Category,Hobbit_Friend) VALUES ('Santiras','Forest',2);
/*6*/INSERT INTO Orc (Power,Master_ID) VALUES (5,2);

-------------------------
-- Show table values ----
-------------------------

SELECT * FROM Ring;
SELECT * FROM Hobbit;
SELECT * FROM Wizard;
SELECT * FROM Master2Ring;
SELECT * FROM Elf;
SELECT * FROM Orc;



-------------------------
-- Delete table values --
-------------------------
DELETE Orc;
DELETE Elf;
DELETE Master2Ring;
DELETE Wizard;
DELETE Hobbit;
DELETE Ring;
DBCC CHECKIDENT (Orc, RESEED, 0); --(if you delete something and want to restart identity)
DBCC CHECKIDENT (Elf, RESEED, 0); --(if you delete something and want to restart identity)
DBCC CHECKIDENT (Wizard, RESEED, 0); --(if you delete something and want to restart identity)
DBCC CHECKIDENT (Hobbit, RESEED, 0); --(if you delete something and want to restart identity)
DBCC CHECKIDENT (Ring, RESEED, 0); --(if you delete something and want to restart identity)