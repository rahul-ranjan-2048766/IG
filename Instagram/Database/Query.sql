/* Drop tables */
DROP TABLE IF EXISTS Person;

/* Creating tables */
CREATE TABLE Person(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Username VARCHAR(100) NOT NULL UNIQUE
);

