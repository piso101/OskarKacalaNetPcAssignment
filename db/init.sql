-- Sprawdzenie i usunięcie istniejącej bazy danych
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'MyDatabase')
BEGIN
    ALTER DATABASE MyDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE MyDatabase;
END
GO

-- Tworzenie nowej bazy danych
CREATE DATABASE MyDatabase;
GO

-- Używanie nowo utworzonej bazy danych
USE MyDatabase;
GO