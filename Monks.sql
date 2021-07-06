DROP DATABASE IF EXISTS BuckfastAbbey;
CREATE DATABASE BuckfastAbbey;
USE BuckfastAbbey;
DROP TABLE IF EXISTS Monk;
CREATE TABLE Monk
(monk_id INT AUTO_INCREMENT PRIMARY KEY,
first_name VARCHAR(40) NOT NULL,
last_name VARCHAR(40) NOT NULL,
age INT NOT NULL,
job VARCHAR(40) NOT NULL);

INSERT INTO Monk VALUES(1, 'William', 'Shrewsbury', 62, 'Abbot');
INSERT INTO Monk VALUES(2, 'Jacques', 'Cabot', 37, 'Head Cook');
INSERT INTO Monk VALUES(3, 'Jacob', 'Smith', 54, 'Librarian');
INSERT INTO Monk VALUES(4, 'Robert', 'Barnes', 38, 'Priest');
INSERT INTO Monk VALUES(5, 'Timothy', 'Feldman', 29, 'Gardener');

COMMIT;
