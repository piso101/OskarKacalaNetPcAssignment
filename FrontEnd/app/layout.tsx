import Header from './components/Header'; // popraw ścieżkę jeśli trzeba

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pl">
      <body
        style={{
          maxWidth: 800,
          margin: '20px auto',
          padding: '0 10px',
          fontFamily: 'Arial, sans-serif',
        }}
      >
        <Header />
        <main>{children}</main>
        <footer
          style={{
            borderTop: '1px solid #ccc',
            marginTop: 40,
            paddingTop: 10,
            textAlign: 'center',
            color: '#666',
          }}
        >
          © 2025 NetPC
        </footer>
      </body>
    </html>
  );
}
