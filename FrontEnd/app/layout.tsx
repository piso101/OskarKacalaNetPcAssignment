import Header from './components/Header';

/// <summary>
/// Główny layout aplikacji Next.js dla projektu NetPC.
/// Obejmuje nagłówek, stopkę oraz kontener na zawartość (children).
/// Ustawia bazowy styl strony (max width, padding, font, itd.).
/// </summary>
export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pl">
      <body
        style={{
          maxWidth: 800,             // Maksymalna szerokość strony (czytelność na desktopie)
          margin: '20px auto',       // Wycentrowanie strony z marginesem górnym/dolnym
          padding: '0 10px',         // Drobne wcięcia boczne
          fontFamily: 'Arial, sans-serif', // Uniwersalna czcionka
        }}
      >
        {/* Wspólny nagłówek dla wszystkich podstron */}
        <Header />

        {/* Główna zawartość dynamiczna (np. komponenty stron) */}
        <main>{children}</main>

        {/* Stopka projektu – widoczna na każdej podstronie */}
        <footer
          style={{
            borderTop: '1px solid #ccc', // Subtelna linia oddzielająca stopkę
            marginTop: 40,               // Przestrzeń nad stopką
            paddingTop: 10,              // Padding wewnętrzny stopki
            textAlign: 'center',         // Wyśrodkowany tekst
            color: '#666',               // Stonowany kolor
          }}
        >
          © 2025 NetPC
        </footer>
      </body>
    </html>
  );
}