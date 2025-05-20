



SELECT o.Imię, o.Nazwisko
FROM Osoba o
JOIN Osoba dziecko ON dziecko.MatkaID = o.OsobaID OR dziecko.OjciecID = o.OsobaID
JOIN Osoba wnuczka ON wnuczka.MatkaID = dziecko.OsobaID OR wnuczka.OjciecID = dziecko.OsobaID
WHERE wnuczka.Płeć = 'K'
GROUP BY o.OsobaID
ORDER BY COUNT(wnuczka.OsobaID) DESC
LIMIT 1;

SELECT RodzajZatrudnienia, 
       AVG(LiczbaPracowników) AS ŚredniaPracowników,
       AVG(Zarobki) AS ŚredniaPensja
FROM (
    SELECT z.RodzajZatrudnienia, z.FirmaID, 
           COUNT(*) AS LiczbaPracowników, 
           AVG(z.Zarobki) AS Zarobki
    FROM Zatrudnienie z
    GROUP BY z.RodzajZatrudnienia, z.FirmaID
) AS Podzapytanie
GROUP BY RodzajZatrudnienia;

WITH Rodziny AS (
  SELECT o.OsobaID AS CzłonekRodzinyID,
         COALESCE(małżonek.OsobaID, NULL) AS MałżonekID,
         SUM(z1.Zarobki + COALESCE(z2.Zarobki, 0)) AS DochódRodziny
  FROM Osoba o
  LEFT JOIN Osoba małżonek ON o.MałżonekID = małżonek.OsobaID
  LEFT JOIN Zatrudnienie z1 ON o.OsobaID = z1.OsobaID
  LEFT JOIN Zatrudnienie z2 ON małżonek.OsobaID = z2.OsobaID
  GROUP BY o.OsobaID, małżonek.OsobaID
)
SELECT o.Imię, o.Nazwisko
FROM Rodziny
JOIN Osoba o ON Rodziny.CzłonekRodzinyID = o.OsobaID
ORDER BY DochódRodziny ASC
LIMIT 1;
