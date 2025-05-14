-- Dane słownikowe: Kategorie
INSERT INTO Categories (Name) VALUES 
('Służbowy'),
('Prywatny'),
('Inny');

-- Dane słownikowe: Podkategorie dla "Służbowy"
INSERT INTO Subcategories (Name, CategoryId) VALUES 
('Szef', 1),
('Klient', 1),
('Współpracownik', 1);