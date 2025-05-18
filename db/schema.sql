CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(255) NOT NULL
);

CREATE TABLE "Categories" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);

CREATE TABLE "Subcategories" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL,
    "CategoryId" INT NOT NULL,
    FOREIGN KEY ("CategoryId") REFERENCES "Categories"("Id")
);

CREATE TABLE "Contacts" (
    "Id" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "PhoneNumber" VARCHAR(20),
    "DateOfBirth" DATE,
    "CategoryId" INT NOT NULL,
    "SubcategoryId" INT NULL,
    "CustomSubcategory" VARCHAR(100),
    "UserId" INT NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES "Users"("Id"),
    FOREIGN KEY ("CategoryId") REFERENCES "Categories"("Id"),
    FOREIGN KEY ("SubcategoryId") REFERENCES "Subcategories"("Id")
);

