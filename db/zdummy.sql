-- Dane słownikowe
INSERT INTO "Categories" ("Name") VALUES 
('Służbowy'),
('Prywatny'),
('Inny');

INSERT INTO "Subcategories" ("Name", "CategoryId") VALUES 
('Szef', 1),
('Klient', 1),
('Współpracownik', 1);

INSERT INTO "Users" ("Email", "PasswordHash") VALUES
('test@example.com', '$2a$11$1SrGIszxtPbCIYaLY5NBvuqq4Lk/b8alRAW7ude4zO5Dy.XjANUg2');
