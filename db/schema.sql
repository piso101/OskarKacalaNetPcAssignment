-- Użytkownicy systemu
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL
);

-- Kategorie kontaktów
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL
);

-- Podkategorie dla wybranych kategorii
CREATE TABLE Subcategories (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Kontakty
CREATE TABLE Contacts (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20),
    DateOfBirth DATE,
    CategoryId INT NOT NULL,
    SubcategoryId INT NULL,
    CustomSubcategory NVARCHAR(100) NULL,
    UserId INT NOT NULL,

    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (SubcategoryId) REFERENCES Subcategories(Id)
);
