use MiddleEarth;

-------------------------
-- Drop Tables--------
-------------------------
DROP TABLE Master2Ring;
DROP TABLE Ring;
DROP TABLE Orc;
DROP TABLE Wizard;
DROP TABLE Elf;
DROP TABLE Hobbit;

-------------------------
-- Create Tables--------
-------------------------

CREATE TABLE Hobbit (
  id INTEGER NOT NULL,
  Name VARCHAR(256) NULL DEFAULT NULL,
  Region VARCHAR(256) NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Elf (
  id INTEGER  NOT NULL,
  Name VARCHAR(256) NULL DEFAULT NULL,
  Category VARCHAR(256) NULL DEFAULT NULL,
  Hobbit_Friend INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT  Hobbit_Friend_FK2 FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id)
);

CREATE TABLE Ring (
  id INTEGER  NOT NULL,
  Material VARCHAR(256) NULL DEFAULT NULL,
  Name VARCHAR(256) NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Wizard (
  id INTEGER NOT NULL,
  Name VARCHAR(256) NULL DEFAULT NULL,
  Color VARCHAR(256) NULL DEFAULT NULL,
  Hobbit_Friend INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT Hobbit_Friend_FK FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id)
);

CREATE TABLE Master2Ring (
  Ring_ID INTEGER NULL DEFAULT NULL,
  Master_ID INTEGER NULL DEFAULT NULL,
  CONSTRAINT  Ring_ID_FK FOREIGN KEY (Ring_ID) REFERENCES Ring (id),
  CONSTRAINT  Master_ID_FK FOREIGN KEY (Master_ID) REFERENCES Wizard (id)
);


CREATE TABLE Orc (
  id INTEGER NOT NULL,
  Power FLOAT NULL DEFAULT NULL,
  Master_ID INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT  Master_ID_FK2 FOREIGN KEY (Master_ID) REFERENCES Wizard (id)
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
/*1*/INSERT INTO Ring (id,Material,Name) VALUES (1,'Wood','Aredian');
/*1*/INSERT INTO Ring (id,Material,Name) VALUES (2,'Silver','Turmonil');
/*1*/INSERT INTO Ring (id,Material,Name) VALUES (3,'Wood','Canterbro');
/*2*/INSERT INTO Hobbit (id,Name,Region) VALUES (4,'Denduf','West');
/*2*/INSERT INTO Hobbit (id,Name,Region) VALUES (5,'Arpin','Hills');
/*3*/INSERT INTO Wizard (id,Name,Color,Hobbit_Friend) VALUES (6,'Heirin','Yellow', 4);
/*3*/INSERT INTO Wizard (id,Name,Color,Hobbit_Friend) VALUES (7,'Olsif','Black', 4);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (1,6);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (1,7);
/*4*/INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES (2,7);
/*5*/INSERT INTO Elf (id,Name,Category,Hobbit_Friend) VALUES (8,'Santiras','Forest',5);
/*5*/INSERT INTO Elf (id,Name,Category,Hobbit_Friend) VALUES (9,'Oripon','Sea',5);
/*6*/INSERT INTO Orc (id,Power,Master_ID) VALUES (10,5,7);

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